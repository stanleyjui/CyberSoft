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
    public class PBBCHK001
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

        //宣告SETUP_REJECT
        String SETUP_REJECT_RC = "";
        SETUP_REJECTDao SETUP_REJECT = new SETUP_REJECTDao();
        DataTable SETUP_REJECT_DataTable = new DataTable();
        
        //宣告SETUP_CODE
        String SETUP_CODE_RC = "";
        SETUP_CODEDao SETUP_CODE = new SETUP_CODEDao();
        DataTable SETUP_CODE_DataTable = new DataTable();

        //宣告CARD_INF
        DataTable CARD_INF_DataTable = null;
        #endregion

        #region 宣告共用變數
        //參數
        String Par_AUTH_FLAG = "";
        //批次日期
        DateTime TODAY_PROCESS_DTE = new DateTime();

        //PUBLIC_HIST 欄位
        String PAY_TYPE = "";
        String PAY_NBR = "";
        String BU = "";
        String CARD_PRODUCT = "";
        String CARD_GROUP = "";
        String ACCT_NBR = "";
        String CUST_SEQ = "";
        String CARD_NBR_APPLY = "";     //原申請卡號
        String CARD_NBR = "";           //實際扣款卡號
        String PAY_SEQ = "";
        Decimal PAY_AMT = 0;
        Decimal CREDIT_AVAIL = 0;
        String PAY_RESULT = "";
        String CTL_CODE = "";
        String ERROR_REASON = "";
        //string ERR_FLAG = "";
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

        String ERR_FLAG = "";
        #endregion

        static readonly Cybersoft.Coca.Log.Logger logger = Cybersoft.Log.LogManager<Cybersoft.Coca.Log.Logger>.GetLogger(System.Reflection.MethodInfo.GetCurrentMethod().ReflectedType.Name);
        //========================================================================================================
        #region 程式主邏輯【MAIN Routine】
        public string RUN()
        {
            #region 準備MSGLG.SYSOUT CLASS
            logger.strJobID = JobID;
            logger.strJOBNAME = System.Reflection.MethodInfo.GetCurrentMethod().ReflectedType.Name;
            logger.dtRunDate = DateTime.Now;
            #endregion

            try
            {
                #region 取得系統資訊(前次批次日、本日批次日)
                CMCSYS001 SYSINF = new CMCSYS001();
                SYSINF.strJobID = JobID;
                SYSINF.strJobName = "PBBCHK001";
                String SYSINF_RC = SYSINF.getSYSINF();
                TODAY_PROCESS_DTE = SYSINF.datetimeTODAY_PROCESS_DTE;
                #endregion

                #region 撈出PUBLIC的錯誤代碼
                SETUP_REJECT.init();
                SETUP_REJECT.strWhereREJECT_GROUP = "PUBLIC";
                SETUP_REJECT_RC = SETUP_REJECT.query();
                switch (SETUP_REJECT_RC)
                {
                    case "S0000": //查詢成功
                        SETUP_REJECT_DataTable = SETUP_REJECT.resultTable;
                        break;

                    case "F0023": //無需處理資料
                        logger.strJobQueue = "查無COLA 授權錯誤代碼資料。";
                        logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                        break;

                    default: //資料庫錯誤
                        logger.strJobQueue = "查詢SETUP_REJECT 資料錯誤:" + SETUP_REJECT_RC;
                        logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                        return "B0016:" + logger.strJobQueue;
                }
                #endregion

                #region 取得授權檢核參數
                SETUP_CODE.init();
                SETUP_CODE.strWhereTYPE = "PUBLIC";
                SETUP_CODE.strWhereTYPE_SUB = "AUTH_CHK";
                SETUP_CODE.strWhereADD_VAL1 = "Y";
                SETUP_CODE_RC = SETUP_CODE.query();
                switch (SETUP_CODE_RC)
                {
                    case "S0000": //查詢成功
                        SETUP_CODE_DataTable = SETUP_CODE.resultTable;
                        //DataRow[] drTMP = SETUP_CODE_DataTable.Select("CODE='00'");
                        //if (drTMP.Length > 0)
                        //{
                        //    Par_AUTH_FLAG = drTMP[0]["ADD_VAL1"].ToString();
                        //}
                        break;

                    case "F0023": //無需處理資料
                        logger.strJobQueue = "SETUP_CODE 查無 信用卡授權檢核設定。";
                        logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                        break;

                    default: //資料庫錯誤
                        logger.strJobQueue = "查詢SETUP_CODE 資料錯誤:" + SETUP_CODE_RC;
                        logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                        return "B0016:" + logger.strJobQueue;
                }
                #endregion

                #region 撈出今日需檢核之代繳資料並join代繳申請檔(PUBLIC_APPLY)
                PUBLIC_HIST.init();
                PUBLIC_HIST.strWhereTRANS_DTE = TODAY_PROCESS_DTE.ToString("yyyyMMdd");
                PUBLIC_HIST_RC = PUBLIC_HIST.join_public_apply();
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
                        BU = "";
                        CARD_PRODUCT = "";
                        CARD_NBR = "";          //扣繳卡號放空白       
                        CARD_NBR_APPLY = "";    //原申請卡號
                        ACCT_NBR = "";
                        PAY_RESULT = "";
                        ERROR_REASON = "";
                        PAY_TYPE = Convert.ToString(PUBLIC_HIST_DataTable.Rows[i]["PAY_TYPE"]);
                        PAY_NBR = Convert.ToString(PUBLIC_HIST_DataTable.Rows[i]["PAY_NBR"]);
                        PAY_SEQ = Convert.ToString(PUBLIC_HIST_DataTable.Rows[i]["PAY_SEQ"]);
                        PAY_AMT = Convert.ToDecimal(PUBLIC_HIST_DataTable.Rows[i]["PAY_AMT"]);
                        CREDIT_AVAIL = Convert.ToDecimal(PUBLIC_HIST_DataTable.Rows[i]["CREDIT_AVAIL"]);
                        CUST_SEQ = Convert.ToString(PUBLIC_HIST_DataTable.Rows[i]["CUST_SEQ"]);
                        CTL_CODE = Convert.ToString(PUBLIC_HIST_DataTable.Rows[i]["CTL_CODE"]);

                        #region 代繳資料進行內部檢核並找出扣款卡號
                        if (Convert.ToString(PUBLIC_HIST_DataTable.Rows[i]["VAILD_FLAG"]) == "")
                        {
                            #region 無申請紀錄(I001)
                            PAY_RESULT = "I001";    //未申請                        
                            #endregion
                        }
                        else
                        {
                            PAY_TYPE = Convert.ToString(PUBLIC_HIST_DataTable.Rows[i]["PAY_TYPE"]);
                            BU = Convert.ToString(PUBLIC_HIST_DataTable.Rows[i]["BU"]);
                            CARD_PRODUCT = Convert.ToString(PUBLIC_HIST_DataTable.Rows[i]["CARD_PRODUCT"]);
                            CARD_GROUP = Convert.ToString(PUBLIC_HIST_DataTable.Rows[i]["CARD_GROUP"]);
                            ACCT_NBR = Convert.ToString(PUBLIC_HIST_DataTable.Rows[i]["ACCT_NBR"]);
                            //PUBLIC_EXPIR_DTE = Convert.ToString(PUBLIC_HIST_DataTable.Rows[i]["EXPIR_DTE"]);
                            CUST_SEQ = Convert.ToString(PUBLIC_HIST_DataTable.Rows[i]["CUST_SEQ"]);
                            CTL_CODE = Convert.ToString(PUBLIC_HIST_DataTable.Rows[i]["CTL_CODE"]);

                            if (Convert.ToString(PUBLIC_HIST_DataTable.Rows[i]["VAILD_FLAG"]) != "Y")
                            {
                                #region 有申請但已終止(I002)
                                PAY_RESULT = "I002";  //有申請但已終止
                                CARD_NBR = Convert.ToString(PUBLIC_HIST_DataTable.Rows[i]["PAY_CARD_NBR_PREV"]);
                                DateTime.TryParseExact(PUBLIC_HIST_DataTable.Rows[i]["CARD_EXPIR_DTE"].ToString().Trim(), "yyyyMMdd", new CultureInfo("zh-TW", true), DateTimeStyles.None, out CARD_EXPIR_DTE);
                                #endregion
                            }
                            else
                            {
                                #region 依參數設定條件進行檢核
                                if (SETUP_CODE_DataTable.Rows.Count > 0)
                                {
                                    //參數設定授權檢核
                                    Par_AUTH_FLAG = "";
                                    for (int chkno = 0; chkno < SETUP_CODE_DataTable.Rows.Count; chkno++)
                                    {
                                        string CHK_CODE = SETUP_CODE_DataTable.Rows[chkno]["CODE"].ToString().Trim();
                                        switch (CHK_CODE)
                                        {
                                            case "00":   //啟用/關閉CyberCOLA批次授權
                                                Par_AUTH_FLAG = SETUP_CODE_DataTable.Rows[chkno]["ADD_VAL1"].ToString().Trim();
                                                break;
                                            case "01":   //檢核卡片是否開卡
                                                break;
                                            case "02":   //檢核卡片控制碼是否有效
                                                break;
                                            case "03":   //檢核卡片額度
                                                PUBLIC_HIST_DataTable.Rows[i]["CREDIT_AVAIL"] = Convert.ToInt32(PUBLIC_HIST_DataTable.Rows[i]["CREDIT_AVAIL"]) - PAY_AMT;
                                                if (Convert.ToInt32(PUBLIC_HIST_DataTable.Rows[i]["CREDIT_AVAIL"]) <= 0)
                                                {
                                                    PAY_RESULT = "I006";
                                                }
                                                break;
                                            default:
                                                logger.strJobQueue = "SETUP_CODE 授權檢核代碼未設定:" + CHK_CODE;
                                                logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                                                return "B0016:" + logger.strJobQueue;
                                        }
                                        if (PAY_RESULT == "")
                                        {
                                            PAY_RESULT = "S000";
                                        }
                                    }
                                }
                                #endregion
                                //Par_AUTH_FLAG == "Y"
                                
                                //COLA批次取授權-前置檢核
                                CARD_NBR_APPLY = Convert.ToString(PUBLIC_HIST_DataTable.Rows[i]["PAY_CARD_NBR"]);
                                CARD_NBR = Convert.ToString(PUBLIC_HIST_DataTable.Rows[i]["PAY_CARD_NBR_PREV"]);
                                DateTime.TryParseExact(PUBLIC_HIST_DataTable.Rows[i]["CARD_EXPIR_DTE"].ToString().Trim(), "yyyyMMdd", new CultureInfo("zh-TW", true), DateTimeStyles.None, out CARD_EXPIR_DTE);
                                //DateTime.TryParseExact(PUBLIC_HIST_DataTable.Rows[i]["EXPIR_DTE_LAST"].ToString().Trim(), "yyyyMMdd", new CultureInfo("zh-TW", true), DateTimeStyles.None, out CARD_EXPIR_DTE_LAST);
                                CARD_EXPIR_DTE_LAST = Convert.ToDateTime(PUBLIC_HIST_DataTable.Rows[i]["EXPIR_DTE_LAST"]);
                                #region 判斷卡片狀態是否正常
                                switch (Convert.ToString(PUBLIC_HIST_DataTable.Rows[i]["CARD_VALID"]))
                                {
                                    case "Y":                                        
                                        if (CARD_EXPIR_DTE < TODAY_PROCESS_DTE)
                                        {
                                            #region 先判斷卡片若已到期(I007)
                                            PAY_RESULT = "I007";
                                            ERR_FLAG = "Y";
                                            #endregion
                                        }
                                        else
                                        {
                                            #region 再判斷新卡已開卡或舊卡尚未過期(N001，可進行批次授權/I004，未開卡)
                                            //判斷新卡已開卡或舊卡尚未過期
                                            if (((CARD_EXPIR_DTE >= TODAY_PROCESS_DTE) && (PUBLIC_HIST_DataTable.Rows[i]["OPEN_FLAG"].ToString() != "0")) ||
                                                ((CARD_EXPIR_DTE_LAST >= TODAY_PROCESS_DTE) && (PUBLIC_HIST_DataTable.Rows[i]["PREV_OPEN"].ToString() != "0")))
                                            {
                                                //可進行批次授權
                                                PAY_RESULT = "N001";
                                                if ((PUBLIC_HIST_DataTable.Rows[i]["OPEN_FLAG"].ToString() == "0") &
                                                    (CARD_EXPIR_DTE_LAST >= TODAY_PROCESS_DTE) && (PUBLIC_HIST_DataTable.Rows[i]["PREV_OPEN"].ToString() != "0"))
                                                {
                                                    CARD_EXPIR_DTE = CARD_EXPIR_DTE_LAST;
                                                }
                                            }
                                            else
                                            {
                                                //卡片未開卡
                                                PAY_RESULT = "I004";
                                                ERR_FLAG = "Y";
                                            }
                                            #endregion
                                        }                                        
                                        break;
                                    case "N":
                                        PAY_RESULT = "I003";
                                        ERR_FLAG = "Y";
                                        break;
                                    default:
                                        PAY_RESULT = "I005";
                                        ERR_FLAG = "Y";
                                        break;
                                }
                                #endregion    
                                #region 若卡號不可授權，則先找該ID下是否有同卡別其它正卡可扣款
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
                                            //CARD_NBR = "";          //扣繳卡號放空白
                                            //CARD_EXPIR_DTE = new DateTime(1900, 1, 1);
                                            break;

                                        default: //資料庫錯誤
                                            logger.strJobQueue = "Query_card_inf 資料錯誤:" + PUBLIC_HIST_RC;
                                            logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                                            return "B0016:" + logger.strJobQueue;
                                    }
                                }
                                #endregion
                                #region 判斷可用餘額是否足夠
                                if (PAY_RESULT == "S000")
                                {
                                    if (PAY_AMT > CREDIT_AVAIL)
                                    {
                                        PAY_RESULT = "I006";
                                    }
                                }
                                #endregion
                                
                            }
                        }                        

                        #region 錯誤原因說明
                        if ((PAY_RESULT.Length > 0) & (PAY_RESULT.Substring(0, 1) == "I"))
                        {
                            DataRow[] DR = SETUP_REJECT_DataTable.Select(" REJECT_CODE ='" + PAY_RESULT + "' ");
                            if (DR.Length > 0)
                            {
                                ERROR_REASON = Convert.ToString(DR[0]["DESCR"]);
                            }
                            else
                            {
                                ERROR_REASON = "";
                            }
                        }
	                    #endregion
                        
                        #region 更新PUBLIC_HIST中的扣款結果
                        PUBLIC_HIST.strWherePAY_SEQ = PAY_SEQ;
                        //PUBLIC_HIST.strBU = BU;
                        //PUBLIC_HIST.strCARD_PRODUCT = CARD_PRODUCT;
                        //PUBLIC_HIST.strACCT_NBR = ACCT_NBR;
                        PUBLIC_HIST.strPAY_CARD_NBR = CARD_NBR;
                        PUBLIC_HIST.strEXPIR_DTE = CARD_EXPIR_DTE.ToString("yyyyMMdd");
                        //PUBLIC_HIST.strCUST_SEQ = CUST_SEQ;
                        PUBLIC_HIST.strPAY_RESULT = PAY_RESULT;
                        PUBLIC_HIST.strCTL_CODE = CTL_CODE;
                        PUBLIC_HIST.datetimeMNT_DT = TODAY_PROCESS_DTE;
                        PUBLIC_HIST.strMNT_USER = "PBBCHK001";
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
        #endregion
        
        //========================================================================================================

        #region insertBATCH_AUTH()
        void insertBATCH_AUTH()
        {
            //BATCH_AUTH.initInsert_row();
            //x = BATCH_AUTH.resultTable.Rows.Count - 1;

            //BATCH_AUTH.resultTable.Rows[x]["BU"] = PUBLIC_HIST_DataTable.Rows[i]["BU"];
            //BATCH_AUTH.resultTable.Rows[x]["ACCT_NBR"] = ACCT_NBR;
            //BATCH_AUTH.resultTable.Rows[x]["PRODUCT"] = PUBLIC_HIST_DataTable.Rows[i]["PRODUCT"];
            //BATCH_AUTH.resultTable.Rows[x]["CURRENCY"] = "001";
            //BATCH_AUTH.resultTable.Rows[x]["CARD_NBR"] = CARD_NBR;
            //BATCH_AUTH.resultTable.Rows[x]["CARD_PRODUCT"] = CARD_GROUP;
            //BATCH_AUTH.resultTable.Rows[x]["EXPIR_DTE"] = "";
            //BATCH_AUTH.resultTable.Rows[x]["PAY_AMT"] = PUBLIC_HIST_DataTable.Rows[i]["PAY_AMT"];
            //BATCH_AUTH.resultTable.Rows[x]["MCC_CODE"] = "4900";
            //BATCH_AUTH.resultTable.Rows[x]["TX_DATE"] = DateTime.Now;
            //BATCH_AUTH.resultTable.Rows[x]["PAY_SEQ"] = PAY_SEQ;
            //BATCH_AUTH.resultTable.Rows[x]["FUNCTION_CODE"] = "M";
            //BATCH_AUTH.resultTable.Rows[x]["TX_INDEX"] = "RT";
            //BATCH_AUTH.resultTable.Rows[x]["MNT_USER"] = "PBBCHK001";
        }
        #endregion

        #region 確認同卡別但不同卡號的其他正卡
        void check_Card_Product()
        {
            int CARD_INF_DataTable_cnt = 0;
            PUBLIC_HIST_RC = PUBLIC_HIST.query_card_inf(BU, CARD_PRODUCT, ACCT_NBR, CARD_NBR);
            switch (PUBLIC_HIST_RC)
            {
                case "S0000": //查詢成功
                    CARD_INF_DataTable = PUBLIC_HIST.resultTable;
                    CARD_INF_DataTable_cnt = PUBLIC_HIST.resultTable.Rows.Count;
                    break;
                case "F0023": //無需處理資料
                    CARD_INF_DataTable_cnt = 0;
                    break;
                default: //資料庫錯誤
                    logger.strJobQueue = "Query_card_inf 資料錯誤:" + PUBLIC_HIST_RC;
                    logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                    throw new System.Exception("B0016：" + logger.strJobQueue);
            }
            if (CARD_INF_DataTable_cnt > 0)
            {
                //檢核本次卡片到期日
                if (Convert.ToDateTime(CARD_INF_DataTable.Rows[0]["EXPIR_DTE"]) >= TODAY_PROCESS_DTE)
                {
                    if (Convert.ToString(CARD_INF_DataTable.Rows[0]["OPEN_FLAG"]) != "0")  //本次卡片到期日未過期且已開卡，可授權卡號
                    {
                        CARD_PRODUCT = Convert.ToString(CARD_INF_DataTable.Rows[0]["CARD_PRODUCT"]);
                        CARD_NBR = Convert.ToString(CARD_INF_DataTable.Rows[0]["CARD_NBR"]);
                        CARD_EXPIR_DTE = Convert.ToDateTime(CARD_INF_DataTable.Rows[0]["EXPIR_DTE"]);
                        //可進行批次授權檔
                        PAY_RESULT = "N001";
                        ERR_FLAG = "";
                    }
                    else
                    {
                        //卡片未開卡
                        PAY_RESULT = "I004";
                        ERR_FLAG = "Y";
                    }
                }
                //檢核前次卡片到期日
                if (ERR_FLAG == "Y")
                {
                    if (Convert.ToDateTime(CARD_INF_DataTable.Rows[0]["EXPIR_DTE_LAST"]) >= TODAY_PROCESS_DTE)
                    {
                        if (Convert.ToString(CARD_INF_DataTable.Rows[0]["PREV_OPEN"]) != "0")  //前次卡片到期日未過期且已開卡，可授權卡號
                        {
                            CARD_PRODUCT = Convert.ToString(CARD_INF_DataTable.Rows[0]["CARD_PRODUCT"]);
                            CARD_NBR = Convert.ToString(CARD_INF_DataTable.Rows[0]["CARD_NBR"]);
                            CARD_EXPIR_DTE = Convert.ToDateTime(CARD_INF_DataTable.Rows[0]["EXPIR_DTE_LAST"]);
                            //組批次授權檔
                            PAY_RESULT = "N001";
                            ERR_FLAG = "";
                        }
                        else
                        {
                            //卡片未開卡
                            PAY_RESULT = "I004";
                            ERR_FLAG = "Y";
                        }
                    }
                }
            }
        }
        #endregion

        #region 確認不同卡別的其他正卡
        void check_Card_Product_other()
        {
            int CARD_INF_DataTable_cnt = 0;
            CARD_INF_DataTable = null;
            PUBLIC_HIST_RC = PUBLIC_HIST.query_PUBLIC_AUTH(BU, CARD_PRODUCT, ACCT_NBR);
            switch (PUBLIC_HIST_RC)
            {
                case "S0000": //查詢成功
                    CARD_INF_DataTable = PUBLIC_HIST.resultTable;
                    CARD_INF_DataTable_cnt = PUBLIC_HIST.resultTable.Rows.Count;
                    break;
                case "F0023": //無需處理資料
                    CARD_INF_DataTable_cnt = 0;
                    break;
                default: //資料庫錯誤
                    logger.strJobQueue = "Query_card_inf 資料錯誤:" + PUBLIC_HIST_RC;
                    logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                    throw new System.Exception("B0016：" + logger.strJobQueue);
            }
            if (CARD_INF_DataTable_cnt > 0)
            {
                for (int i = 0; i < CARD_INF_DataTable_cnt; i++)
                {
                    //檢核本次卡片到期日
                    if (Convert.ToDateTime(CARD_INF_DataTable.Rows[i]["EXPIR_DTE"]) >= TODAY_PROCESS_DTE)
                    {
                        if (Convert.ToString(CARD_INF_DataTable.Rows[i]["OPEN_FLAG"]) != "0")  //本次卡片到期日未過期且已開卡，可授權卡號
                        {
                            CARD_PRODUCT = Convert.ToString(CARD_INF_DataTable.Rows[i]["CARD_PRODUCT"]);
                            CARD_NBR = Convert.ToString(CARD_INF_DataTable.Rows[i]["CARD_NBR"]);
                            CARD_EXPIR_DTE = Convert.ToDateTime(CARD_INF_DataTable.Rows[i]["EXPIR_DTE"]);
                            //可進行批次授權檔
                            PAY_RESULT = "N001";
                            ERR_FLAG = "";
                            return;
                        }
                        else
                        {
                            //卡片未開卡
                            PAY_RESULT = "I004";
                            ERR_FLAG = "Y";
                        }
                    }
                    //檢核前次卡片到期日
                    if (ERR_FLAG == "Y")
                    {
                        if (Convert.ToDateTime(CARD_INF_DataTable.Rows[i]["EXPIR_DTE_LAST"]) >= TODAY_PROCESS_DTE)
                        {
                            if (Convert.ToString(CARD_INF_DataTable.Rows[i]["PREV_OPEN"]) != "0")  //前次卡片到期日未過期且已開卡，可授權卡號
                            {
                                CARD_PRODUCT = Convert.ToString(CARD_INF_DataTable.Rows[i]["CARD_PRODUCT"]);
                                CARD_NBR = Convert.ToString(CARD_INF_DataTable.Rows[i]["CARD_NBR"]);
                                CARD_EXPIR_DTE = Convert.ToDateTime(CARD_INF_DataTable.Rows[i]["EXPIR_DTE_LAST"]);
                                //組批次授權檔
                                PAY_RESULT = "N001";
                                ERR_FLAG = "";
                                return;
                            }
                            else
                            {
                                //卡片未開卡
                                PAY_RESULT = "I004";
                                ERR_FLAG = "Y";
                            }
                        }
                    }
                }
            }
        }
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
