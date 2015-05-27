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
using System.Text.RegularExpressions;
using Cybersoft.ExportDocument;

namespace Cybersoft.Biz.Cust
{
    /// <summary>
    /// 程式說明:產出公共事業代繳批次授權檔(中華電信、台電、省水、市水)
    /// ABEND處理:不可RERUN;主要是以須進行批次COLA授權
    /// 撈出待授權(N001) 更新為 授權中(N002)
    /// UPDATE : 2014.8.8 - CB1431_信用卡繳稅及時轉批次備援作業
    /// </summary>
    public class PBBAUP003
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

        //暫存報表TABLE
        DataTable REPORT_TABLE = new DataTable();
        #endregion

        #region 宣告檔案路徑
        //XML放置路徑 
        String strXML_Layout = "";
        //寫出檔案名稱
        String strOutFileName = "";
        //寫出檔案路徑
        String FILE_PATH = "";
        #endregion

        #region 設定檔案編碼
        Encoding BIG5 = Encoding.GetEncoding("big5");
        #endregion

        #region 宣告共用變數
        const string strJobName = "PBBAUP003";
        //批次日期        
        DateTime TODAY_PROCESS_DTE = new DateTime();
        DateTime NEXT_PROCESS_DTE = new DateTime();

        //PUBLIC_HIST 欄位
        String TRANS_DTE = "";
        String PAY_TYPE = "";
        String PAY_CARD_NBR = "";
        String EXPIR_DTE = "";
        Decimal PAY_AMT = 0;
        String PAY_NBR = "";
        String PAY_SEQ = "";
        DateTime PAY_DTE = new DateTime();
        String PAY_RESULT = "";
        String CUST_ID = "";
        String FILLER = "";
        String strBank_name = "";
        //授權回應碼相關欄位
        string AUTH_CODE = "";
        string RETURN_CODE = "";

        //報表欄位
        String ERROR_REASON = "";
        String SEQ = "";

        //筆數
        int PUBLIC_HIST_Query_Count = 0;
        int i = 0; //控制PUBLIC_HIST
        int y = 0; //控制報表Table
        int PUBLIC_HIST_NoMatch_Count = 0;



        //DAO 控制參數
        String FLAG = "T";  //T:當日需授權  A:所有需授權
       
        #endregion

        static readonly Cybersoft.Coca.Log.Logger logger = Cybersoft.Log.LogManager<Cybersoft.Coca.Log.Logger>.GetLogger(System.Reflection.MethodInfo.GetCurrentMethod().ReflectedType.Name);
        //========================================================================================================
        #region 程式主邏輯【MAIN Routine】
        public string RUN(string strfile_name)
        {
            #region 準備MSGLG.SYSOUT CLASS
            logger.strJobID = JobID;
            logger.strJOBNAME = "PBBAUP003";
            logger.dtRunDate = DateTime.Now;
            #endregion

            try
            {
                #region 取得系統資訊(前次批次日、本日批次日)
                CMCSYS001 SYSINF = new CMCSYS001();
                SYSINF.strJobID = JobID;
                SYSINF.strJobName = strJobName;
                String SYSINF_RC = SYSINF.getSYSINF();
                strBank_name = SYSINF.strREPORT_TITLE;

                //原程式
                TODAY_PROCESS_DTE = SYSINF.datetimeTODAY_PROCESS_DTE;
                NEXT_PROCESS_DTE = SYSINF.datetimeNEXT_PROCESS_DTE;
                #endregion

                #region 定義欄位
                REPORT_TABLE.Columns.Add("PAY_CARD_NBR", typeof(string));
                REPORT_TABLE.Columns.Add("EXPIR_DTE", typeof(string));
                REPORT_TABLE.Columns.Add("MCC", typeof(string));
                REPORT_TABLE.Columns.Add("CVV2", typeof(string));
                REPORT_TABLE.Columns.Add("MERCH_NBR", typeof(string));
                REPORT_TABLE.Columns.Add("BIN", typeof(string));
                REPORT_TABLE.Columns.Add("AMT", typeof(string));
                REPORT_TABLE.Columns.Add("COUNTRY", typeof(string));
                REPORT_TABLE.Columns.Add("CURRENCY", typeof(string));
                REPORT_TABLE.Columns.Add("ORIG_AMT", typeof(string));
                REPORT_TABLE.Columns.Add("UPD_DATE", typeof(string));
                REPORT_TABLE.Columns.Add("UPD_USER", typeof(string));
                REPORT_TABLE.Columns.Add("TX_DTE", typeof(string));
                REPORT_TABLE.Columns.Add("TX_TIME", typeof(string));
                REPORT_TABLE.Columns.Add("AUTH_FLAG", typeof(string));
                REPORT_TABLE.Columns.Add("CUST_NBR", typeof(string));
                REPORT_TABLE.Columns.Add("AUTH_CODE", typeof(string));
                REPORT_TABLE.Columns.Add("ENTRY_MODE", typeof(string));
                REPORT_TABLE.Columns.Add("REF_NO", typeof(string));
                #endregion

                #region 擷取今日需產生授權的代繳資料
                TRANS_DTE = TODAY_PROCESS_DTE.ToString("yyyyMMdd");
                PUBLIC_HIST_RC = PUBLIC_HIST.query_trans_dte(FLAG, TRANS_DTE);
                switch (PUBLIC_HIST_RC)
                {
                    case "S0000": //查詢成功
                        PUBLIC_HIST_Query_Count = PUBLIC_HIST.resultTable.Rows.Count;
                        break;

                    case "F0023": //無需處理資料
                        logger.strJobQueue = "今日無產生授權的代繳資料,請確認";
                        logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                        break;

                    default: //資料庫錯誤
                        logger.strJobQueue = "查詢PUBLIC_HIST 資料錯誤:" + PUBLIC_HIST_RC;
                        logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                        return "B0016:" + logger.strJobQueue;
                }
                #endregion

                #region 循序處理代繳資料
                for (i = 0; i < PUBLIC_HIST.resultTable.Rows.Count; i++)
                {
                        //逐筆寫入批次授權檔(excel)
                        REPORT_TABLE.Rows.Add();
                        y = REPORT_TABLE.Rows.Count - 1;
                        REPORT_TABLE.Rows[y]["PAY_CARD_NBR"] = Convert.ToString(PUBLIC_HIST.resultTable.Rows[i]["PAY_CARD_NBR"]);
                        REPORT_TABLE.Rows[y]["EXPIR_DTE"] = Convert.ToString(PUBLIC_HIST.resultTable.Rows[i]["EXPIR_DTE"]).Substring(2, 4);
                        REPORT_TABLE.Rows[y]["MCC"] = "4900";
                        REPORT_TABLE.Rows[y]["CVV2"] = "000";
                        REPORT_TABLE.Rows[y]["MERCH_NBR"] = string.Empty;
                        REPORT_TABLE.Rows[y]["BIN"] = Convert.ToString(PUBLIC_HIST.resultTable.Rows[i]["PAY_CARD_NBR"]).Substring(0,6);
                        REPORT_TABLE.Rows[y]["AMT"] = Convert.ToInt64(PUBLIC_HIST.resultTable.Rows[i]["PAY_AMT"]);
                        REPORT_TABLE.Rows[y]["COUNTRY"] = "158";
                        REPORT_TABLE.Rows[y]["CURRENCY"] = "901";
                        REPORT_TABLE.Rows[y]["ORIG_AMT"] = Convert.ToInt64(PUBLIC_HIST.resultTable.Rows[i]["PAY_AMT"]);
                        REPORT_TABLE.Rows[y]["UPD_DATE"] = TODAY_PROCESS_DTE.ToString("yyyyMMdd");
                        REPORT_TABLE.Rows[y]["UPD_USER"] = strJobName;
                        REPORT_TABLE.Rows[y]["TX_DTE"] = DateTime.Now.Date.ToString("yyyyMMdd");
                        REPORT_TABLE.Rows[y]["TX_TIME"] = DateTime.Now.ToString("HHmmss");
                        REPORT_TABLE.Rows[y]["AUTH_FLAG"] = "A";
                        REPORT_TABLE.Rows[y]["CUST_NBR"] = "";
                        REPORT_TABLE.Rows[y]["AUTH_CODE"] = "";
                        REPORT_TABLE.Rows[y]["ENTRY_MODE"] = "00";
                        REPORT_TABLE.Rows[y]["REF_NO"] = "";

                        #region 回寫授權結果
                        PUBLIC_HIST.init();
                        PUBLIC_HIST.strWherePAY_SEQ = Convert.ToString(PUBLIC_HIST.resultTable.Rows[i]["PAY_SEQ"]);
                        PUBLIC_HIST.strWherePAY_NBR = Convert.ToString(PUBLIC_HIST.resultTable.Rows[i]["PAY_NBR"]);
                        PUBLIC_HIST.strWherePAY_TYPE = Convert.ToString(PUBLIC_HIST.resultTable.Rows[i]["PAY_TYPE"]);
                        PUBLIC_HIST.strWherePAY_CARD_NBR = Convert.ToString(PUBLIC_HIST.resultTable.Rows[i]["PAY_CARD_NBR"]);
                        PUBLIC_HIST.strWherePAY_RESULT = "N001";  //待授權
                        PUBLIC_HIST.strPAY_RESULT = "N002";  //授權中;
                        PUBLIC_HIST.strMNT_USER = strJobName;
                        PUBLIC_HIST.datetimeMNT_DT = TODAY_PROCESS_DTE;
                        PUBLIC_HIST_RC = PUBLIC_HIST.update();
                        switch (PUBLIC_HIST_RC)
                        {
                            case "S0000": //更新成功
                                if (PUBLIC_HIST.intUptCnt == 0)
                                {
                                    PUBLIC_HIST_NoMatch_Count = PUBLIC_HIST_NoMatch_Count + 1;
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

                #region 將批次授權excel檔產出
                writeReport(REPORT_TABLE, strfile_name);
                #endregion

                #region  設定放檔路徑
                Cybersoft.Base.CMCURL001 CMCURL = new CMCURL001();
                // 由報表路徑將檔案搬至指定路徑
                string strSourPath = CMCURL.getPATH("REPORT");
                string strDestPath = CMCURL.getPATH("AUTHUPLOAD");
                string strSourFile_name = strfile_name + '_' + TODAY_PROCESS_DTE.ToString("yyyyMMdd") + ".xls";
                strSourPath = strSourPath + TODAY_PROCESS_DTE.ToString("yyyyMMdd") + "\\";
                #endregion
                #region 將批次授權excel檔搬移至指定位置
                string CMCURL_RC = CMCURL.MoveFile(strSourPath, strSourFile_name, strDestPath, "N", "Y", "Y");
                switch (CMCURL_RC.Substring(0, 5))
                {
                    case "S0000":  //修改成功
                        break;
                    default:       //資料庫錯誤
                        logger.strJobQueue = "[CMCURL.MoveFile()]：" + CMCURL_RC;
                        logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                        return "B0099:" + logger.strJobQueue;
                }
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
        
        #region 寫出至報表
        void writeReport(DataTable inTable, string strfile_name)
        {
            CMCRPT001 CMCRPT001 = new CMCRPT001();
            //設定特殊欄位資料
            ArrayList alSumData = new ArrayList();

            //設定合併欄位資料
            ArrayList alMegData = new ArrayList();
            string inRPTName = strfile_name + "_" + TODAY_PROCESS_DTE.ToString("yyyyMMdd");

            //產出報表
            CMCRPT001.Output(inTable, alSumData, alMegData, inRPTName, "CABAUP01", "工作表1", "工作表1", TODAY_PROCESS_DTE);
        }
        #endregion

        #region Display
        void writeDisplay()
        {
            logger.strJobID = "00002";
            //擷取今日PUBLIC_HIST資料
            logger.strTBL_NAME = "PUBLIC_HIST";
            logger.strTBL_ACCESS = "Q";
            logger.intTBL_COUNT = PUBLIC_HIST_Query_Count;
            logger.strMEMO = "預計PUBLIC_HIST授權筆數：";
            logger.writeCounter();

            //更新今日PUBLIC_HIST資料
            logger.strTBL_NAME = "PUBLIC_HIST";
            logger.strTBL_ACCESS = "U";
            logger.intTBL_COUNT = PUBLIC_HIST_NoMatch_Count;
            logger.strMEMO = "更新PUBLIC_HIST失敗筆數：";
            logger.writeCounter();

            logger.DisplayCounter(Cybersoft.Coca.Log.LevelEnum.Info);
        }
        #endregion

        
    }
}

