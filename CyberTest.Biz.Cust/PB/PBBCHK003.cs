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
    /// 處理當天多筆公用事業超額資料
    /// 不可RERUN, 若有問題, 重新收檔後再執行( PUBLIC_IN.xml -> PUBLIC_OUT.xml )
    /// </summary>
    public class PBBCHK003
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
        DataTable PUBLIC_HIST_DataTable2 = null;

        #endregion

        #region 宣告共用變數

        //批次日期
        DateTime TODAY_PROCESS_DTE = new DateTime();

        //PUBLIC_HIST 欄位
        String CARD_NBR = "";
        String PAY_SEQ = "";
        Decimal PAY_AMT = 0;

        //筆數
        int PUBLIC_HIST_QUERY_Count = 0;  //PUBLIC_HIST 需處理筆數
        int PUBLIC_HIST_QUERY_Count2 = 0;  //PUBLIC_HIST 需處理筆數
        int PUBLIC_HIST_UPDATE_Count = 0; //PUBLIC_HIST 更新筆數
        int i = 0;  //PUBLIC_HIST
        int j = 0;
        int ROW_COUNT = 0;
        int CURR_CREDIT_AVAIL = 0;      //取得該卡片之目前可用餘額

        #endregion

        static readonly Cybersoft.Coca.Log.Logger logger = Cybersoft.Log.LogManager<Cybersoft.Coca.Log.Logger>.GetLogger(System.Reflection.MethodInfo.GetCurrentMethod().ReflectedType.Name);
        public string RUN()
        {
            #region 準備MSGLG.SYSOUT CLASS
            logger.strJobID = JobID;
            logger.strJOBNAME = "PBBCHK003";
            logger.dtRunDate = DateTime.Now;
            #endregion

            try
            {
                #region 取得系統資訊(前次批次日、本日批次日)
                CMCSYS001 SYSINF = new CMCSYS001();
                SYSINF.strJobID = JobID;
                SYSINF.strJobName = "PBBCHK003";
                String SYSINF_RC = SYSINF.getSYSINF();
                TODAY_PROCESS_DTE = SYSINF.datetimeTODAY_PROCESS_DTE;
                #endregion

                #region 撈出今日超過信用額度之資料
                PUBLIC_HIST.init();
                PUBLIC_HIST.strWhereTRANS_DTE = TODAY_PROCESS_DTE.ToString("yyyyMMdd");
                PUBLIC_HIST_RC = PUBLIC_HIST.query_CURR_AVAIL();
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
                for (i = 0; i < PUBLIC_HIST_QUERY_Count; i++)
                {
                    #region 取得需處理資料之可用額度
                    PUBLIC_HIST.init();
                    PUBLIC_HIST.strWhereTRANS_DTE = TODAY_PROCESS_DTE.ToString("yyyyMMdd");
                    PUBLIC_HIST.strWherePAY_CARD_NBR = PUBLIC_HIST_DataTable.Rows[i]["PAY_CARD_NBR"].ToString();
                    PUBLIC_HIST.strWherePAY_RESULT = "S000";
                    PUBLIC_HIST_RC = PUBLIC_HIST.query();
                    switch (PUBLIC_HIST_RC)
                    {
                        case "S0000": //查詢成功
                            PUBLIC_HIST_QUERY_Count2 = PUBLIC_HIST.resultTable.Rows.Count;
                            PUBLIC_HIST.resultTable.DefaultView.Sort = " PAY_SEQ asc ";
                            PUBLIC_HIST_DataTable2 = PUBLIC_HIST.resultTable.DefaultView.ToTable();
                            break;

                        case "F0023": //無需處理資料
                            logger.strJobQueue = "今日無資料需檢核:" + PUBLIC_HIST_RC;
                            logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                            return "B0000";

                        default: //資料庫錯誤
                            logger.strJobQueue = "查詢PUBLIC_HIST 資料錯誤:" + PUBLIC_HIST_RC;
                            logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                            return "B0016:" + logger.strJobQueue;
                    }
                    #endregion

                    #region 循序檢核代繳資料並更新扣款結果
                    for (j = 0; j < PUBLIC_HIST_QUERY_Count2; j++)
                    {
                        CARD_NBR = Convert.ToString(PUBLIC_HIST_DataTable2.Rows[j]["PAY_CARD_NBR"]);
                        PAY_SEQ = Convert.ToString(PUBLIC_HIST_DataTable2.Rows[j]["PAY_SEQ"]);
                        PAY_AMT = Convert.ToDecimal(PUBLIC_HIST_DataTable2.Rows[j]["PAY_AMT"]);

                        #region 取得該卡人之目前可用餘額
                        DataRow[] drAVAIL = PUBLIC_HIST_DataTable.Select(" PAY_CARD_NBR = '" + CARD_NBR + "'");
                        if (drAVAIL.Length > 0)
                        {
                            CURR_CREDIT_AVAIL = Convert.ToInt32(drAVAIL[0]["CURR_CREDIT_AVAIL"]);
                            ROW_COUNT = Convert.ToInt32(drAVAIL[0]["ROW_COUNT"]) - 1;
                        }
                        else
                        {
                            CURR_CREDIT_AVAIL = 0;
                        }
                        #endregion

                        //判斷是否有足夠點數扣點
                        if (PAY_AMT <= CURR_CREDIT_AVAIL)
                        {
                            PUBLIC_HIST_DataTable.Rows[ROW_COUNT]["CURR_CREDIT_AVAIL"] = CURR_CREDIT_AVAIL - PAY_AMT;
                        }
                        else
                        {
                            #region 更新PUBLIC_HIST中的扣款結果
                            PUBLIC_HIST.strWhereTRANS_DTE = TODAY_PROCESS_DTE.ToString("yyyyMMdd");
                            PUBLIC_HIST.strWherePAY_SEQ = PAY_SEQ;
                            PUBLIC_HIST.strWherePAY_RESULT = "S000";
                            PUBLIC_HIST.strPAY_RESULT = "I006";
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

                    }
                    #endregion
                }

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

        #region Display
        void writeDisplay()
        {
            logger.strJobID = "00002";
            //擷取今日需處理之PUBLIC_HIST資料
            logger.strTBL_NAME = "PUBLIC_HIST";
            logger.strTBL_ACCESS = "Q";
            logger.intTBL_COUNT = PUBLIC_HIST_QUERY_Count;
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
