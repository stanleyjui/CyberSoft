using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Cybersoft.Data;
using Cybersoft.Data.DAL;
using Cybersoft.Log;
using Cybersoft.Base;
using System.IO;
using System.Globalization;
using System.Text.RegularExpressions;

namespace Cybersoft.Biz.Cust
{
    /// <summary>
    /// 代繳資料基本檢核並找出授權卡號
    /// 程式可RERUN, 將內部檢核失敗及內部檢核成功但未送至NCCC授權之資料重新檢核並找出授權卡號
    /// </summary>
    public class PBBCHK002
    {
        private string JobID;
        public string strJobID
        {
            get { return JobID; }
            set { JobID = value; }
        }
        private string RunCode;
        public string strRunCode
        {
            get { return RunCode; }
            set { RunCode = value; }
        }

        #region 宣告TABLE

        //宣告PUBLIC_HIST
        String PUBLIC_HIST_RC = "";
        PUBLIC_HISTDao PUBLIC_HIST = new PUBLIC_HISTDao();
        DataTable PUBLIC_HIST_DataTable = null;
        DataTable CARD_INF_DataTable = null;

        ////宣告BATCH_AUTH
        //String BATCH_AUTH_RC = "";
        //BATCH_AUTHDao BATCH_AUTH = new BATCH_AUTHDao();
        //int BATCH_AUTH_Count_Insert = 0;
        //int BATCH_AUTH_Count_Delete = 0;

        #endregion

        #region 宣告共用變數

        //批次日期
        DateTime TODAY_PROCESS_DTE = new DateTime();

        //PUBLIC_HIST 欄位
        String PAY_NBR = "";
        String BU = "";
        String CARD_PRODUCT = "";
        String CARD_GROUP = "";
        String ACCT_NBR = "";
        String CUST_SEQ = "";
        String CARD_NBR = "";           //實際扣款卡號
        String PAY_SEQ = "";
        Decimal PAY_AMT = 0;
        Decimal CREDIT_AVAIL = 0;
        String PAY_RESULT = "";
        String ERR_FLAG = "";
        String CTL_CODE = "";

        //CARD_INF 欄位
        DateTime CARD_EXPIR_DTE = new DateTime();
        DateTime CARD_EXPIR_DTE_LAST = new DateTime();

        //筆數
        int PUBLIC_HIST_QUERY_Count = 0;  //PUBLIC_HIST 需處理筆數
        int PUBLIC_HIST_UPDATE_Count = 0; //PUBLIC_HIST 更新筆數
        int CHECK_OK_Count = 0;     //檢核成功筆數
        int CHECK_ERROR_Count = 0;  //檢核失敗筆數       
        int i = 0;  //PUBLIC_HIST
        //int x = 0;  //BATCH_AUTH

        #endregion

        static readonly Cybersoft.Coca.Log.Logger logger = Cybersoft.Log.LogManager<Cybersoft.Coca.Log.Logger>.GetLogger(System.Reflection.MethodInfo.GetCurrentMethod().ReflectedType.Name);
        public string RUN()
        {
            #region 準備MSGLG.SYSOUT CLASS
            logger.strJobID = JobID;
            logger.strJOBNAME = "PBBCHK002";
            logger.dtRunDate = DateTime.Now;
            #endregion

            try
            {
                #region 取得系統資訊(前次批次日、本日批次日)
                CMCSYS001 SYSINF = new CMCSYS001();
                SYSINF.strJobID = JobID;
                SYSINF.strJobName = "PBBCHK002";
                String SYSINF_RC = SYSINF.getSYSINF();
                TODAY_PROCESS_DTE = SYSINF.datetimeTODAY_PROCESS_DTE;
                #endregion

                #region 清除BATCH_AUTH資料
                //BATCH_AUTH.table_define();
                //BATCH_AUTH.init();
                //BATCH_AUTH_RC = BATCH_AUTH.delete();
                //switch (BATCH_AUTH_RC)
                //{
                //    case "S0000": //刪除成功
                //        logger.strJobQueue = "BATCH_AUTH.delete finish 筆數:" + BATCH_AUTH_Count_Delete.ToString("###,###,##0").PadLeft(11, ' ');
                //        logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                //        break;

                //    default: //資料庫錯誤
                //        logger.strJobQueue = "刪除BATCH_AUTH 資料錯誤:" + PUBLIC_HIST_RC;
                //        logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                //        return "B0016:" + logger.strJobQueue;
                //}
                #endregion

                #region 撈出今日需檢核之代繳資料
                PUBLIC_HIST.init();
                PUBLIC_HIST.strWhereTRANS_DTE = TODAY_PROCESS_DTE.ToString("yyyyMMdd");
                PUBLIC_HIST_RC = PUBLIC_HIST.query_parking();
                switch (PUBLIC_HIST_RC)
                {
                    case "S0000": //查詢成功
                        PUBLIC_HIST_QUERY_Count = PUBLIC_HIST.resultTable.Rows.Count;
                        PUBLIC_HIST_DataTable = PUBLIC_HIST.resultTable;
                        break;

                    case "F0023": //無需處理資料
                        logger.strJobQueue = "今日無資料需檢核:" + PUBLIC_HIST_RC;
                        logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                        break;

                    default: //資料庫錯誤
                        logger.strJobQueue = "查詢PUBLIC_HIST 資料錯誤:" + PUBLIC_HIST_RC;
                        logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                        return "B0016:" + logger.strJobQueue;
                }
                #endregion

                //有資料才需執行
                if (PUBLIC_HIST_QUERY_Count > 0)
                {
                    #region 循序檢核代繳資料並更新扣款結果
                    for (i = 0; i < PUBLIC_HIST_DataTable.Rows.Count; i++)
                    {

                        BU = Convert.ToString(PUBLIC_HIST_DataTable.Rows[i]["BU"]);
                        CARD_PRODUCT = Convert.ToString(PUBLIC_HIST_DataTable.Rows[i]["CARD_PRODUCT"]);
                        CARD_GROUP = Convert.ToString(PUBLIC_HIST_DataTable.Rows[i]["CARD_GROUP"]);
                        ACCT_NBR = Convert.ToString(PUBLIC_HIST_DataTable.Rows[i]["ACCT_NBR"]);
                        CARD_NBR = Convert.ToString(PUBLIC_HIST_DataTable.Rows[i]["PAY_CARD_NBR"]);
                        CARD_EXPIR_DTE = Convert.ToDateTime(PUBLIC_HIST_DataTable.Rows[i]["CARD_EXPIR_DTE"]);
                        CARD_EXPIR_DTE_LAST = Convert.ToDateTime(PUBLIC_HIST_DataTable.Rows[i]["CARD_EXPIR_DTE_LAST"]);

                        //CARD_NBR_APPLY = "";    //原申請卡號
                        PAY_RESULT = "";
                        ERR_FLAG = "";
                        PAY_NBR = Convert.ToString(PUBLIC_HIST_DataTable.Rows[i]["PAY_NBR"]);
                        PAY_SEQ = Convert.ToString(PUBLIC_HIST_DataTable.Rows[i]["PAY_SEQ"]);
                        PAY_AMT = Convert.ToDecimal(PUBLIC_HIST_DataTable.Rows[i]["PAY_AMT"]);
                        CREDIT_AVAIL = Convert.ToDecimal(PUBLIC_HIST_DataTable.Rows[i]["CREDIT_AVAIL"]);
                        CUST_SEQ = Convert.ToString(PUBLIC_HIST_DataTable.Rows[i]["CUST_SEQ"]);
                        CTL_CODE = Convert.ToString(PUBLIC_HIST_DataTable.Rows[i]["CTL_CODE"]);

                        #region 代繳資料進行內部檢核並找出扣款卡號

                        if (Convert.ToString(PUBLIC_HIST_DataTable.Rows[i]["CARD_VALID"]) == "Y")
                        {
                            if (CARD_EXPIR_DTE < TODAY_PROCESS_DTE)
                            {
                                //卡片已到期
                                PAY_RESULT = "I007";
                                ERR_FLAG = "Y";
                            }
                            else
                            {
                                if (((CARD_EXPIR_DTE >= TODAY_PROCESS_DTE) && (PUBLIC_HIST_DataTable.Rows[i]["OPEN_FLAG"].ToString() != "0")) ||
                                    ((CARD_EXPIR_DTE_LAST >= TODAY_PROCESS_DTE) && (PUBLIC_HIST_DataTable.Rows[i]["PREV_OPEN"].ToString() != "0")))
                                {
                                    //組批次授權檔
                                    PAY_RESULT = "S000";
                                }
                                else
                                {
                                    //卡片未開卡
                                    PAY_RESULT = "I004";
                                    ERR_FLAG = "Y";
                                }
                            }
                        }
                        else
                        {
                            PAY_RESULT = "I003";
                            ERR_FLAG = "Y";
                        }

                        if (ERR_FLAG == "Y")
                        {
                            ERR_FLAG = "";
                            //找出該ID下是否有同卡別其它正卡可扣款
                            PUBLIC_HIST_RC = PUBLIC_HIST.query_card_inf(BU, CARD_PRODUCT, ACCT_NBR, CARD_NBR);
                            switch (PUBLIC_HIST_RC)
                            {
                                case "S0000": //查詢成功
                                    CARD_INF_DataTable = PUBLIC_HIST.resultTable;
                                    if (Convert.ToDateTime(CARD_INF_DataTable.Rows[0]["EXPIR_DTE"]) >= TODAY_PROCESS_DTE)
                                    {
                                        if (Convert.ToString(CARD_INF_DataTable.Rows[0]["OPEN_FLAG"]) == "0")
                                        {
                                            //卡片未開卡
                                            PAY_RESULT = "I004";
                                            ERR_FLAG = "Y";
                                        }
                                        else
                                        {
                                            CREDIT_AVAIL = Convert.ToDecimal(CARD_INF_DataTable.Rows[0]["CREDIT_AVAIL"]);
                                            CUST_SEQ = Convert.ToString(CARD_INF_DataTable.Rows[0]["CUST_NBR"]);
                                            CARD_NBR = Convert.ToString(CARD_INF_DataTable.Rows[0]["CARD_NBR"]);
                                            CARD_EXPIR_DTE = Convert.ToDateTime(CARD_INF_DataTable.Rows[0]["EXPIR_DTE"]);
                                            //組批次授權檔
                                            PAY_RESULT = "S000";
                                        }
                                    }
                                    
                                    if (ERR_FLAG == "Y")
                                    {
                                        if (Convert.ToDateTime(CARD_INF_DataTable.Rows[0]["EXPIR_DTE_LAST"]) >= TODAY_PROCESS_DTE)
                                        {
                                            if (Convert.ToString(CARD_INF_DataTable.Rows[0]["PREV_OPEN"]) == "0")
                                            {
                                                //卡片未開卡
                                                PAY_RESULT = "I004";
                                            }
                                            else
                                            {
                                                CREDIT_AVAIL = Convert.ToDecimal(CARD_INF_DataTable.Rows[0]["CREDIT_AVAIL"]);
                                                CUST_SEQ = Convert.ToString(CARD_INF_DataTable.Rows[0]["CUST_NBR"]);
                                                CARD_NBR = Convert.ToString(CARD_INF_DataTable.Rows[0]["CARD_NBR"]);
                                                CARD_EXPIR_DTE = Convert.ToDateTime(CARD_INF_DataTable.Rows[0]["EXPIR_DTE_LAST"]);
                                                //組批次授權檔
                                                PAY_RESULT = "S000";
                                            }
                                        }
                                    }
                                    break;

                                case "F0023": //無需處理資料
                                    break;

                                default: //資料庫錯誤
                                    logger.strJobQueue = "Query_card_inf 資料錯誤:" + PUBLIC_HIST_RC;
                                    logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                                    return "B0016:" + logger.strJobQueue;
                            }
                        }

                        //判斷可用餘額是否足夠
                        if (PAY_RESULT == "S000")
                        {
                            if (PAY_AMT > CREDIT_AVAIL)
                            {
                                PAY_RESULT = "I006";
                            }
                        }

                        #endregion

                        #region 更新PUBLIC_HIST中的扣款結果
                        PUBLIC_HIST.strWherePAY_SEQ = PAY_SEQ;
                        PUBLIC_HIST.strPAY_CARD_NBR = CARD_NBR;
                        PUBLIC_HIST.strEXPIR_DTE = CARD_EXPIR_DTE.ToString("yyyyMMdd");
                        PUBLIC_HIST.strPAY_RESULT = PAY_RESULT;
                        PUBLIC_HIST.strCTL_CODE = CTL_CODE;
                        PUBLIC_HIST.datetimeMNT_DT = TODAY_PROCESS_DTE;
                        PUBLIC_HIST.strMNT_USER = "PBBCHK002";
                        PUBLIC_HIST_RC = PUBLIC_HIST.update();
                        switch (PUBLIC_HIST_RC)
                        {
                            case "S0000": //更新成功   
                                PUBLIC_HIST_UPDATE_Count = PUBLIC_HIST_UPDATE_Count + PUBLIC_HIST.intUptCnt;
                                if (PUBLIC_HIST.intUptCnt == 0)
                                {
                                    logger.strJobQueue = "更新PUBLIC_HIST 資料錯誤: F0023, CARD_NBR = " + CARD_NBR + " CARD_NBR_APPLY = " + PAY_SEQ;
                                    logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                                    return "F0023" + logger.strJobQueue;
                                }
                                break;

                            default: //資料庫錯誤
                                logger.strJobQueue = "更新PUBLIC_HIST 資料錯誤:" + PUBLIC_HIST_RC;
                                logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                                return "B0016:" + logger.strJobQueue;
                        }
                        #endregion
                    }
                    #endregion
                }

                #region 撈出今日需檢核之退費代繳資料
                PUBLIC_HIST_QUERY_Count = 0;
                PUBLIC_HIST.init();
                PUBLIC_HIST.strWhereTRANS_DTE = TODAY_PROCESS_DTE.ToString("yyyyMMdd");
                PUBLIC_HIST_RC = PUBLIC_HIST.query_parking_RTN();
                switch (PUBLIC_HIST_RC)
                {
                    case "S0000": //查詢成功
                        PUBLIC_HIST_QUERY_Count = PUBLIC_HIST.resultTable.Rows.Count;
                        PUBLIC_HIST_DataTable = PUBLIC_HIST.resultTable;
                        break;

                    case "F0023": //無需處理資料
                        logger.strJobQueue = "今日無資料需檢核:" + PUBLIC_HIST_RC;
                        logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                        break;

                    default: //資料庫錯誤
                        logger.strJobQueue = "查詢PUBLIC_HIST 資料錯誤:" + PUBLIC_HIST_RC;
                        logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                        return "B0016:" + logger.strJobQueue;
                }
                #endregion

                //有退貨資料才需執行
                if (PUBLIC_HIST_QUERY_Count > 0)
                {
                    #region 循序檢核代繳資料並更新扣款結果
                    for (i = 0; i < PUBLIC_HIST_DataTable.Rows.Count; i++)
                    {
                        if (Convert.ToString(PUBLIC_HIST_DataTable.Rows[i]["CARD_PRODUCT"]) == "")
                        {
                            PAY_SEQ = Convert.ToString(PUBLIC_HIST_DataTable.Rows[i]["PAY_SEQ"]);
                            CARD_EXPIR_DTE = Convert.ToDateTime(PUBLIC_HIST_DataTable.Rows[i]["CARD_EXPIR_DTE"]);
                            CTL_CODE = Convert.ToString(PUBLIC_HIST_DataTable.Rows[i]["CTL_CODE"]);
                            PAY_RESULT = "I005";
                        }
                        else
                        {
                            PAY_SEQ = Convert.ToString(PUBLIC_HIST_DataTable.Rows[i]["PAY_SEQ"]);
                            CARD_EXPIR_DTE = Convert.ToDateTime(PUBLIC_HIST_DataTable.Rows[i]["CARD_EXPIR_DTE"]);
                            CTL_CODE = Convert.ToString(PUBLIC_HIST_DataTable.Rows[i]["CTL_CODE"]);
                            PAY_RESULT = "S000";
                        }

                        #region 更新PUBLIC_HIST中的扣款結果
                        PUBLIC_HIST.strWherePAY_SEQ = PAY_SEQ;
                        PUBLIC_HIST.strEXPIR_DTE = CARD_EXPIR_DTE.ToString("yyyyMMdd");
                        PUBLIC_HIST.strPAY_RESULT = PAY_RESULT;
                        PUBLIC_HIST.strCTL_CODE = CTL_CODE;
                        PUBLIC_HIST.datetimeMNT_DT = TODAY_PROCESS_DTE;
                        PUBLIC_HIST.strMNT_USER = "PBBCHK002";
                        PUBLIC_HIST_RC = PUBLIC_HIST.update();
                        switch (PUBLIC_HIST_RC)
                        {
                            case "S0000": //更新成功   
                                PUBLIC_HIST_UPDATE_Count = PUBLIC_HIST_UPDATE_Count + PUBLIC_HIST.intUptCnt;
                                if (PUBLIC_HIST.intUptCnt == 0)
                                {
                                    logger.strJobQueue = "更新PUBLIC_HIST 資料錯誤: F0023, CARD_NBR = " + CARD_NBR + " CARD_NBR_APPLY = " + PAY_SEQ;
                                    logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                                    return "F0023" + logger.strJobQueue;
                                }
                                break;

                            default: //資料庫錯誤
                                logger.strJobQueue = "更新PUBLIC_HIST 資料錯誤:" + PUBLIC_HIST_RC;
                                logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                                return "B0016:" + logger.strJobQueue;
                        }
                        #endregion
                    }
                    #endregion
                }

                #region 整批新增 BATCH_AUTH
                ////先紀錄筆數
                //BATCH_AUTH_Count_Insert = BATCH_AUTH.resultTable.Rows.Count;
                //BATCH_AUTH.insert_by_DT();

                ////判別回傳筆數是否相同
                //if (BATCH_AUTH_Count_Insert != BATCH_AUTH.intInsCnt)
                //{
                //    logger.strJobQueue = "整批新增PUBLIC_APPLY時筆數異常,原筆數 : " + BATCH_AUTH_Count_Insert + " 實際筆數: " + BATCH_AUTH.intInsCnt;
                //    logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                //    return "B0012：" + logger.strJobQueue;
                //}
                #endregion

                #region DISPLAY
                writeDisplay();
                #endregion

                return "B0000";
            }
            catch (Exception e)
            {
                logger.strJobQueue = e.ToString();
                logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);

                return "B0099:" + e.ToString();
            }
        }

        #region insertBATCH_AUTH()
        //void insertBATCH_AUTH()
        //{
        //    BATCH_AUTH.initInsert_row();
        //    x = BATCH_AUTH.resultTable.Rows.Count - 1;

        //    BATCH_AUTH.resultTable.Rows[x]["BU"] = PUBLIC_HIST_DataTable.Rows[i]["BU"];
        //    BATCH_AUTH.resultTable.Rows[x]["ACCT_NBR"] = ACCT_NBR;
        //    BATCH_AUTH.resultTable.Rows[x]["PRODUCT"] = PUBLIC_HIST_DataTable.Rows[i]["PRODUCT"];
        //    BATCH_AUTH.resultTable.Rows[x]["CURRENCY"] = "001";
        //    BATCH_AUTH.resultTable.Rows[x]["CARD_NBR"] = CARD_NBR;
        //    BATCH_AUTH.resultTable.Rows[x]["CARD_PRODUCT"] = CARD_GROUP;
        //    BATCH_AUTH.resultTable.Rows[x]["EXPIR_DTE"] = "";
        //    BATCH_AUTH.resultTable.Rows[x]["PAY_AMT"] = PUBLIC_HIST_DataTable.Rows[i]["PAY_AMT"];
        //    BATCH_AUTH.resultTable.Rows[x]["MCC_CODE"] = "4900";
        //    BATCH_AUTH.resultTable.Rows[x]["TX_DATE"] = DateTime.Now;
        //    BATCH_AUTH.resultTable.Rows[x]["PAY_SEQ"] = PAY_SEQ;
        //    BATCH_AUTH.resultTable.Rows[x]["FUNCTION_CODE"] = "M";
        //    BATCH_AUTH.resultTable.Rows[x]["TX_INDEX"] = "RT";
        //    BATCH_AUTH.resultTable.Rows[x]["MNT_USER"] = "PBBCHK002";
        //}
        #endregion

        #region Display
        void writeDisplay()
        {
            logger.strJobID = "00002";
            //擷取今日需處理之PUBLIC_HIST資料
            logger.strTBL_NAME = "PUBLIC_HIST";
            logger.strTBL_ACCESS = "Q";
            logger.intTBL_COUNT = PUBLIC_HIST_QUERY_Count;
            logger.writeCounter();

            //PUBLIC_HIST檢核成功資料
            logger.strTBL_NAME = "PUBLIC_HIST";
            logger.strTBL_ACCESS = "Q";
            logger.strMEMO = "CHECK OK";
            logger.intTBL_COUNT = CHECK_OK_Count;
            logger.writeCounter();

            //PUBLIC_HIST檢核成功資料
            logger.strTBL_NAME = "PUBLIC_HIST";
            logger.strTBL_ACCESS = "Q";
            logger.strMEMO = "CHECK ERROR";
            logger.intTBL_COUNT = CHECK_ERROR_Count;
            logger.writeCounter();

            //更新PUBLIC_HIST資料
            logger.strTBL_NAME = "PUBLIC_HIST";
            logger.strTBL_ACCESS = "U";
            logger.intTBL_COUNT = PUBLIC_HIST_UPDATE_Count;
            logger.writeCounter();

            logger.DisplayCounter(Cybersoft.Coca.Log.LevelEnum.Info);
        }
        #endregion
    }
}
