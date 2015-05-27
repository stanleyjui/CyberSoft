using System;
using System.Collections;
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
using Cybersoft.ExportDocument;
using Cybersoft.Data.DAL.MBS;

namespace Cybersoft.Biz.Cust
{
    /// <summary>
    /// PBBMBS002 產生今日公共事業_商家入扣檔(特店帳簿異動紀錄)
    /// 執行時間：每日執行 
    /// </summary>

    public class PBBMBS002
    {
        private string JobID;
        public string strJobID
        {
            get { return JobID; }
            set { JobID = value; }
        }
        private string JOBNAME;
        public string strJOBNAME
        {
            get { return JOBNAME; }
            set { JOBNAME = value; }
        }


        #region 宣告TABLE
        //宣告PUBLIC_HIST
        string PUBLIC_HIST_RC = string.Empty;
        PUBLIC_HISTDao PUBLIC_HIST = new PUBLIC_HISTDao();
        int PUBLIC_HIST_query_cnt = 0;


        //宣告MERCHANT_BOOK_UPDATE
        string MERCHANT_BOOK_UPDATE_RC = string.Empty;
        MERCHANT_BOOK_UPDATEDao MERCHANT_BOOK_UPDATE = new MERCHANT_BOOK_UPDATEDao();
        DataTable MERCHANT_BOOK_UPDATE_DataTable = new DataTable();
        int MERCHANT_BOOK_UPDATE_Count_Insert = 0;
        int MERCHANT_BOOK_UPDATE_Count_Delete = 0;

        #endregion

        #region 宣告常數
        const string MNT_USER = "PBBMBS002";
        DateTime dtNow = System.DateTime.Now;
        #endregion

        #region 宣告變數
        DateTime PREV_PROCESS_DTE = new DateTime();
        DateTime TODAY_PROCESS_DTE = new DateTime();
        DateTime NEXT_PROCESS_DTE = new DateTime();
        int intSEQ = 0;
        #endregion

        #region 宣告共用類別
        static readonly Cybersoft.Coca.Log.Logger logger = Cybersoft.Log.LogManager<Cybersoft.Coca.Log.Logger>.GetLogger(System.Reflection.MethodInfo.GetCurrentMethod().ReflectedType.Name);
        #endregion

        //========================================================================================================
        #region MAIN  
        public string RUN()
        {
            #region //準備MSGLG.SYSOUT CLASS
            logger.strJobID = JobID;
            logger.strJOBNAME = MNT_USER;
            logger.dtRunDate = DateTime.Now;
            #endregion

            try
            {
                #region  取得系統資訊(前次批次日、本日批次日)
                CMCSYS001 SYSINF = new CMCSYS001();
                SYSINF.strJobID = JobID;
                SYSINF.strJobName = strJOBNAME;
                String SYSINF_RC = SYSINF.getSYSINF();
                PREV_PROCESS_DTE = SYSINF.datetimePREV_PROCESS_DTE;
                TODAY_PROCESS_DTE = SYSINF.datetimeTODAY_PROCESS_DTE;
                NEXT_PROCESS_DTE = SYSINF.datetimeBANK_NEXT_PROCESS_DTE;
                #endregion

                #region 初始化 DataTable
                PUBLIC_HIST.table_define();
                MERCHANT_BOOK_UPDATE.table_define();
                #endregion

                #region RERUN機制 刪除MERCHANT_BOOK_UPDATE資料
                MERCHANT_BOOK_UPDATE.init();
                MERCHANT_BOOK_UPDATE.resultTable.Clear();
                // 條件
                //MERCHANT_BOOK_UPDATE.strWherePOST_RESULT = "";
                MERCHANT_BOOK_UPDATE.strWhereBATCH_SEQ = "900001";
                //MERCHANT_BOOK_UPDATE.DateTimeWherePOSTING_DTE = NEXT_PROCESS_DTE;
                MERCHANT_BOOK_UPDATE_RC = MERCHANT_BOOK_UPDATE.delete();
                switch (MERCHANT_BOOK_UPDATE_RC.Substring(0, 5))
                {
                    case "S0000":  //修改成功
                        break;
                    default:       //資料庫錯誤
                        logger.strJobQueue = "[MBS_PAYMENT.delete()]：" + MERCHANT_BOOK_UPDATE_RC;
                        logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                        return "B0016:" + logger.strJobQueue;
                }
                MERCHANT_BOOK_UPDATE_Count_Delete = MERCHANT_BOOK_UPDATE.intDelCnt;
                logger.strJobQueue = "刪除 MERCHANT_BOOK_UPDATE 共" + MERCHANT_BOOK_UPDATE_Count_Delete + "筆。";
                logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                #endregion

                #region 取得今日公用事業代扣資料
                PUBLIC_HIST.init();
                PUBLIC_HIST.strWhereTRANS_DTE = TODAY_PROCESS_DTE.ToString("yyyyMMdd");
                PUBLIC_HIST.strWherePAY_RESULT = "0000";
                PUBLIC_HIST_RC = PUBLIC_HIST.query_for_MERCHANT_BOOK_UPDATE();
                switch (PUBLIC_HIST_RC)
                {
                    case "S0000": //查詢成功
                        PUBLIC_HIST_query_cnt = PUBLIC_HIST.resultTable.Rows.Count;
                        break;
                    case "F0023": //查無資料                       
                        logger.strJobQueue = "本日無公用事業代扣資料!!";
                        logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                        return "B0000" + logger.strJobQueue;
                    default: //資料庫錯誤
                        logger.strJobQueue = "PUBLIC_HIST.query_for_MERCHANT_BOOK_UPDATE 錯誤 *** " + PUBLIC_HIST_RC;
                        logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                        return "B0016" + logger.strJobQueue;
                }
                #endregion

                #region 組 MERCHANT_BOOK_UPDATE
                for (int i = 0; i < PUBLIC_HIST.resultTable.Rows.Count; i++)
                {
                    intSEQ++;
                    MERCHANT_BOOK_UPDATE.init();
                    MERCHANT_BOOK_UPDATE.resultTable.Clear();
                    MERCHANT_BOOK_UPDATE.initInsert_row();
                    //事業單位 BU
                    MERCHANT_BOOK_UPDATE.strBU = "000";
                    //特店代號
                    MERCHANT_BOOK_UPDATE.strMERCHANT_NBR = PUBLIC_HIST.resultTable.Rows[i]["UT_MER_NO"].ToString();
                    //產品碼
                    MERCHANT_BOOK_UPDATE.strPRODUCT = "000";
                    //幣別
                    MERCHANT_BOOK_UPDATE.strCURRENCY = "901";
                    //付款模式
                    MERCHANT_BOOK_UPDATE.strMERCHANT_STATUS = "1";
                    //銀行帳戶號碼
                    MERCHANT_BOOK_UPDATE.strBANK_ACCT_NBR = PUBLIC_HIST.resultTable.Rows[i]["BANK_ACCT_NBR"].ToString();
                    //收單行
                    MERCHANT_BOOK_UPDATE.strCARD_PRODUCT = PUBLIC_HIST.resultTable.Rows[i]["PAY_TYPE"].ToString().PadLeft(4, '0'); ;
                    //請款日期??
                    MERCHANT_BOOK_UPDATE.datetimeVALUE_DTE = PREV_PROCESS_DTE;
                    //交易日期
                    MERCHANT_BOOK_UPDATE.datetimeEFF_DTE = NEXT_PROCESS_DTE;
                    //入帳日期
                    MERCHANT_BOOK_UPDATE.datetimePOSTING_DTE = NEXT_PROCESS_DTE;  
                    //結帳日??
                    MERCHANT_BOOK_UPDATE.datetimeSTMT_DTE = PREV_PROCESS_DTE;  
                    //批次號碼
                    MERCHANT_BOOK_UPDATE.strBATCH_SEQ = "900001";
                    //序號
                    MERCHANT_BOOK_UPDATE.strSEQ = intSEQ.ToString().PadLeft(5,'0');
                    //帳務群組
                    MERCHANT_BOOK_UPDATE.strACCT_GROUP = "";
                    //特店名稱
                    MERCHANT_BOOK_UPDATE.strDESCR = PUBLIC_HIST.resultTable.Rows[i]["DESCR"].ToString();
                    //交易碼
                    if (PUBLIC_HIST.resultTable.Rows[i]["USER_FIELD_1"].ToString().Trim() !="R")
                    {
                        MERCHANT_BOOK_UPDATE.strCODE = "6202";
                    }
                    else
                    {
                        MERCHANT_BOOK_UPDATE.strCODE = "6201";
                    }
                    //來源碼
                    MERCHANT_BOOK_UPDATE.strSOURCE_CODE = "MBS0000001";
                    //特店手續費
                    MERCHANT_BOOK_UPDATE.decTX_RATE = 0;
                    //金額
                    decimal decPAY_AMT_TOT = Convert.ToDecimal(PUBLIC_HIST.resultTable.Rows[i]["PAY_AMT_TOT"]);
                    MERCHANT_BOOK_UPDATE.decAMT = decPAY_AMT_TOT;
                    //處理結果
                    MERCHANT_BOOK_UPDATE.strPOST_RESULT = "00";
                    MERCHANT_BOOK_UPDATE_RC = MERCHANT_BOOK_UPDATE.insert();
                    switch (MERCHANT_BOOK_UPDATE_RC) 
                    {
                        case "S0000":
                            MERCHANT_BOOK_UPDATE_Count_Insert++;
                            break;

                        case "F0022":
                            logger.strJobQueue = "新增特店付款檔資料重複 *** _KEY值為:" + PUBLIC_HIST.resultTable.Rows[i]["SEQ"].ToString() + " RC = " + MERCHANT_BOOK_UPDATE_RC;
                            logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                            return "B0016" + logger.strJobQueue;

                        default: //資料庫錯誤
                            logger.strJobQueue = "新增特店付款檔資料 錯誤 *** " + MERCHANT_BOOK_UPDATE_RC;
                            logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                            return "B0016" + logger.strJobQueue;
                    }
                    
                }
                #endregion


                //========================================================================================================
                // CLOSE-RTN
                writeDisplay();
                return "B0000";

            }
            catch (Exception e)
            {
                logger.strJobQueue = strJOBNAME + " *** " + e.ToString();
                logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                return "B0099:" + e.ToString();
            }
        }
        #endregion
        
        //========================================================================================================

        #region  SYSOUT
        void writeDisplay()
        {
            //刪除特店付款檔(RERUN)
            logger.strTBL_NAME = "MBS_PAYMENT";
            logger.strTBL_ACCESS = "D";
            logger.strMEMO = "刪除特店付款檔(RERUN)";
            logger.intTBL_COUNT = MERCHANT_BOOK_UPDATE_Count_Delete;
            logger.writeCounter();

            //取得公用事業代扣名單
            logger.strTBL_NAME = "PUBLIC_HIST";
            logger.strTBL_ACCESS = "Q";
            logger.strMEMO = "公用事業代扣名單";
            logger.intTBL_COUNT = PUBLIC_HIST_query_cnt;
            logger.writeCounter();

            //寫入特店付款檔
            logger.strTBL_NAME = "MBS_PAYMENT";
            logger.strTBL_ACCESS = "I";
            logger.strMEMO = "寫入特店付款檔";
            logger.intTBL_COUNT = MERCHANT_BOOK_UPDATE_Count_Insert;
            logger.writeCounter();

            
            logger.DisplayCounter(Cybersoft.Coca.Log.LevelEnum.Info);
        }
        #endregion

    }
}
