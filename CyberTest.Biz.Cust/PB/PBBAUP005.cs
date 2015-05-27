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
    /// 程式說明:公共事業代繳 - PBBCHK001授權檢核結果一覽表(中華電信、台電、省水、市水)
    /// ABEND處理: 可ReRun
    /// 授權錯誤代碼：select * from COLA_CODE where TYPE = 'REASON'
    /// </summary>
    public class PBBAUP005
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
        DataTable PUBLIC_HIST_TABLE = new DataTable();
        
        //暫存報表TABLE
        DataTable REPORT_TABLE = new DataTable();
        #endregion

        #region 設定檔案編碼
        Encoding BIG5 = Encoding.GetEncoding("big5");
        #endregion

        #region 宣告共用變數
        string strJobName = "PBBAUP005";
        //批次日期        
        DateTime TODAY_PROCESS_DTE = new DateTime();
        DateTime NEXT_PROCESS_DTE = new DateTime();
        DateTime Bank_NEXT_PROCESS_DTE = new DateTime();

        //PUBLIC_HIST 欄位
        String PAY_TYPE_DESCR = "";
        String TRANS_DTE = "";
        String PAY_TYPE = "";
        String PAY_CARD_NBR = "";
        //String EXPIR_DTE = "";
        Decimal PAY_AMT = 0;
        String PAY_NBR = "";
        //String PAY_SEQ = "";
        DateTime PAY_DTE = new DateTime();
        String PAY_RESULT = "";
        String CUST_ID = "";
        //String FILLER = "";
        String strBank_name = "";
        //授權回應碼相關欄位
        string AUTH_CODE = "";
        //string RETURN_CODE = "";

        //報表欄位
        String ERROR_REASON = "";
        //String strSEQ = "";
        DateTime PAY_DTE_PHONE = new DateTime();
        DateTime PAY_DTE_ELECT = new DateTime();
        DateTime PAY_DTE_TWWATER = new DateTime();
        DateTime PAY_DTE_ELECT_H = new DateTime();
        DateTime PAY_DTE_TPWATER = new DateTime();

        //筆數
        int PUBLIC_HIST_Query_Count = 0;
        int i = 0; //控制PUBLIC_HIST
        //int x = 0; //控制VPOS_T
        int y = 0; //控制報表Table
        int NOMATCH_CNT = 0;
        int CHK_ERR_Count = 0;
        int CHK_OK_Count = 0;
        int CHK_ERR_PHONE_Count = 0;
        int CHK_ERR_ELECT_Count = 0;
        int CHK_ERR_ELECT_H_Count = 0;
        int CHK_ERR_TWWATER_Count = 0;
        int CHK_ERR_TPWATER_Count = 0;
        int CHK_OK_PHONE_Count = 0;
        int CHK_OK_ELECT_Count = 0;
        int CHK_OK_ELECT_H_Count = 0;
        int CHK_OK_TWWATER_Count = 0;
        int CHK_OK_TPWATER_Count = 0;
        int PHONE_Count = 0;
        int ELECT_Count = 0;
        int ELECT_H_Count = 0;
        int TWWATER_Count = 0;
        int TPWATER_Count = 0;
        //int VPOS_TXT_Count = 0;
        //int PUBLIC_HIST_NoMatch_Count = 0;
        int AUTH_APPROVE_Count = 0;
        int AUTH_REJECT_Count = 0;

        //DAO 控制參數
        String FLAG = "T";  //T:當日需授權  A:所有需授權

        #endregion

        static readonly Cybersoft.Coca.Log.Logger logger = Cybersoft.Log.LogManager<Cybersoft.Coca.Log.Logger>.GetLogger(System.Reflection.MethodInfo.GetCurrentMethod().ReflectedType.Name);
        //========================================================================================================
        #region 程式主邏輯【MAIN Routine】
        public string RUN()
        {
            #region 準備MSGLG.SYSOUT CLASS
            logger.strJobID = JobID;
            logger.strJOBNAME = strJobName;
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

                TODAY_PROCESS_DTE = SYSINF.datetimeTODAY_PROCESS_DTE;
                NEXT_PROCESS_DTE = SYSINF.datetimeNEXT_PROCESS_DTE;
                Bank_NEXT_PROCESS_DTE = SYSINF.datetimeBANK_NEXT_PROCESS_DTE;

                DateTime dtNow = DateTime.Now;
                #endregion

                #region 定義報表Table--公用事業授權一覽表
                REPORT_TABLE.Columns.Add("PAY_TYPE", typeof(string));
                REPORT_TABLE.Columns.Add("ID", typeof(string));
                REPORT_TABLE.Columns.Add("PAY_CARD_NBR", typeof(string));
                REPORT_TABLE.Columns.Add("PAY_NBR", typeof(string));
                REPORT_TABLE.Columns.Add("AMT", typeof(string));
                REPORT_TABLE.Columns.Add("PAY_RESULT", typeof(string));
                REPORT_TABLE.Columns.Add("AUTH_CODE", typeof(string));
                REPORT_TABLE.Columns.Add("ERROR_REASON", typeof(String));
                #endregion

                #region 撈出COLA授權後的錯誤代碼 (DEL)
                //COLA_CODE.init();
                //COLA_CODE.strWhereTYPE = "REASON";
                //COLA_CODE_RC = COLA_CODE.query();
                //switch (COLA_CODE_RC)
                //{
                //    case "S0000": //查詢成功
                //        COLA_CODE_DataTable = COLA_CODE.resultTable;
                //        break;

                //    case "F0023": //無需處理資料
                //        logger.strJobQueue = "查無COLA 授權錯誤代碼資料。";
                //        logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                //        break;

                //    default: //資料庫錯誤
                //        logger.strJobQueue = "查詢COLA_CODE 資料錯誤:" + COLA_CODE_RC;
                //        logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                //        return "B0016:" + logger.strJobQueue;
                //}
                #endregion

                #region 擷取今日公用事業授權結果
                TRANS_DTE = TODAY_PROCESS_DTE.ToString("yyyyMMdd");
                PUBLIC_HIST_RC = PUBLIC_HIST.query_AUTH_RPT(FLAG, TRANS_DTE);
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

                #region 循序產出報表
                for (i = 0; i < PUBLIC_HIST.resultTable.Rows.Count; i++)
                {
                    PAY_TYPE = Convert.ToString(PUBLIC_HIST.resultTable.Rows[i]["PAY_TYPE"]);
                    PAY_TYPE_DESCR = Convert.ToString(PUBLIC_HIST.resultTable.Rows[i]["DESCR"]);
                    CUST_ID = Convert.ToString(PUBLIC_HIST.resultTable.Rows[i]["CUST_ID"]);
                    PAY_CARD_NBR = Convert.ToString(PUBLIC_HIST.resultTable.Rows[i]["PAY_CARD_NBR"]);
                    PAY_NBR = Convert.ToString(PUBLIC_HIST.resultTable.Rows[i]["PAY_NBR"]);
                    PAY_AMT = Convert.ToDecimal(PUBLIC_HIST.resultTable.Rows[i]["PAY_AMT"]);
                    AUTH_CODE = Convert.ToString(PUBLIC_HIST.resultTable.Rows[i]["AUTH_CODE"]);
                    ERROR_REASON = Convert.ToString(PUBLIC_HIST.resultTable.Rows[i]["PAY_RESULT_DESCR"]);
                    PAY_DTE = Convert.ToDateTime(PUBLIC_HIST.resultTable.Rows[i]["PAY_DTE"]);
                    PAY_RESULT = Convert.ToString(PUBLIC_HIST.resultTable.Rows[i]["PAY_RESULT"]);

                    //寫出報表明細
                    insert_Report();
                }
                #endregion

                #region 各公共事業筆數
                PHONE_Count = CHK_ERR_PHONE_Count + CHK_OK_PHONE_Count;
                ELECT_Count = CHK_ERR_ELECT_Count + CHK_OK_ELECT_Count;
                ELECT_H_Count = CHK_ERR_ELECT_H_Count + CHK_OK_ELECT_H_Count;
                TWWATER_Count = CHK_ERR_TWWATER_Count + CHK_OK_TWWATER_Count;
                TPWATER_Count = CHK_ERR_TPWATER_Count + CHK_OK_TPWATER_Count;
                #endregion

                #region 將資料寫入報表中
                writeReport(REPORT_TABLE);
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
        #region 新增至報表
        void insert_Report()
        {
            REPORT_TABLE.Rows.Add();
            y = REPORT_TABLE.Rows.Count - 1;
            REPORT_TABLE.Rows[y]["PAY_TYPE"] = PAY_TYPE_DESCR;
            REPORT_TABLE.Rows[y]["ID"] = CUST_ID;
            REPORT_TABLE.Rows[y]["PAY_CARD_NBR"] = PAY_CARD_NBR;
            REPORT_TABLE.Rows[y]["PAY_NBR"] = PAY_NBR;
            REPORT_TABLE.Rows[y]["AMT"] = PAY_AMT;
            REPORT_TABLE.Rows[y]["AUTH_CODE"] = AUTH_CODE;
            
            switch (PAY_RESULT)
            {
                case "S000":
                    REPORT_TABLE.Rows[y]["PAY_RESULT"] = "授權成功";
                    REPORT_TABLE.Rows[y]["ERROR_REASON"] = "";
                    break;
                case "0000":
                    REPORT_TABLE.Rows[y]["PAY_RESULT"] = "授權成功";
                    REPORT_TABLE.Rows[y]["ERROR_REASON"] = PAY_RESULT + "-" + ERROR_REASON;
                    break;
                default:
                    REPORT_TABLE.Rows[y]["PAY_RESULT"] = "授權失敗";
                    REPORT_TABLE.Rows[y]["ERROR_REASON"] = PAY_RESULT+"-"+ ERROR_REASON;
                    break;
            }
           
            #region 計數
            if (PAY_RESULT == "S000" || PAY_RESULT == "0000")
            {
                switch (PAY_TYPE)
                {
                    case "0001"://中華電信
                        CHK_OK_PHONE_Count = CHK_OK_PHONE_Count + 1;
                        break;
                    case "0002"://市水
                        CHK_OK_TPWATER_Count = CHK_OK_TPWATER_Count + 1;
                        break;
                    case "0003"://省水
                        CHK_OK_TWWATER_Count = CHK_OK_TWWATER_Count + 1;
                        break;
                    case "0004"://台電
                        CHK_OK_ELECT_Count = CHK_OK_ELECT_Count + 1;
                        break;
                    case "H004"://台電
                        CHK_OK_ELECT_H_Count = CHK_OK_ELECT_H_Count + 1;
                        break;
                }
            }
            else 
            {
                switch (PAY_TYPE)
                {
                    case "0001"://中華電信
                        CHK_ERR_PHONE_Count = CHK_ERR_PHONE_Count + 1;
                        break;
                    case "0002"://市水
                        CHK_ERR_TPWATER_Count = CHK_ERR_TPWATER_Count + 1;
                        break;
                    case "0003"://省水
                        CHK_ERR_TWWATER_Count = CHK_ERR_TWWATER_Count + 1;
                        break;
                    case "0004"://台電
                        CHK_ERR_ELECT_Count = CHK_ERR_ELECT_Count + 1;
                        break;
                    case "H004"://台電
                        CHK_ERR_ELECT_H_Count = CHK_ERR_ELECT_H_Count + 1;
                        break;
                }
            }
            #endregion

            #region 為了出報表,將第一筆解繳日期存起來
            //PB1:例行性作業，解繳日為隔日;其他特殊作業，解繳日為當日
            if (CHK_OK_PHONE_Count == 1 || CHK_ERR_PHONE_Count == 1)
            {
                //PAY_DTE_PHONE = PAY_DTE;
                PAY_DTE_PHONE = Bank_NEXT_PROCESS_DTE;
            }
            if (CHK_OK_ELECT_Count == 1 || CHK_ERR_ELECT_Count == 1)
            {
                //PAY_DTE_ELECT = PAY_DTE;
                PAY_DTE_ELECT = Bank_NEXT_PROCESS_DTE;
            }
            if (CHK_OK_ELECT_H_Count == 1 || CHK_ERR_ELECT_H_Count == 1)
            {
                //PAY_DTE_ELECT_H = PAY_DTE;
                PAY_DTE_ELECT_H = Bank_NEXT_PROCESS_DTE;
            }
            if (CHK_OK_TWWATER_Count == 1 || CHK_ERR_TWWATER_Count == 1)
            {
                //PAY_DTE_TWWATER = PAY_DTE;
                PAY_DTE_TWWATER = Bank_NEXT_PROCESS_DTE;
            }
            if (CHK_OK_TPWATER_Count == 1 || CHK_ERR_TPWATER_Count == 1)
            {
                //PAY_DTE_TPWATER = PAY_DTE;
                PAY_DTE_TPWATER = Bank_NEXT_PROCESS_DTE;
            }            
            
            #endregion
        }
        #endregion

        #region 寫出至報表
        void writeReport(DataTable inTable)
        {
            //排序
            inTable.DefaultView.Sort = "AUTH_CODE asc,PAY_TYPE asc,ERROR_REASON asc";
            inTable = inTable.DefaultView.ToTable();

            CMCRPT001 CMCRPT001 = new CMCRPT001();
            //設定特殊欄位資料
            ArrayList alSumData = new ArrayList();

            alSumData.Add(new ExcelFactory.ListItem("#RPT_BANK_NAME", strBank_name));
            alSumData.Add(new ExcelFactory.ListItem("#RPT_DATE", TODAY_PROCESS_DTE.ToShortDateString()));
            if (PHONE_Count > 0)   //表示今天有中華電信
            {
                alSumData.Add(new ExcelFactory.ListItem("#DOWN_DTE_PHONE", TODAY_PROCESS_DTE.ToShortDateString()));
                alSumData.Add(new ExcelFactory.ListItem("#PAY_DTE_PHONE", PAY_DTE_PHONE.ToShortDateString()));
                alSumData.Add(new ExcelFactory.ListItem("#TOTAL_CNT_PHONE", PHONE_Count.ToString() + "筆"));
                alSumData.Add(new ExcelFactory.ListItem("#SUCC_CNT_PHONE", CHK_OK_PHONE_Count.ToString() + "筆"));
                alSumData.Add(new ExcelFactory.ListItem("#FAIL_CNT_PHONE", CHK_ERR_PHONE_Count.ToString() + "筆"));
            }
            else
            {
                alSumData.Add(new ExcelFactory.ListItem("#DOWN_DTE_PHONE", ""));
                alSumData.Add(new ExcelFactory.ListItem("#PAY_DTE_PHONE", ""));
                alSumData.Add(new ExcelFactory.ListItem("#TOTAL_CNT_PHONE", "0 筆"));
                alSumData.Add(new ExcelFactory.ListItem("#SUCC_CNT_PHONE", "0 筆"));
                alSumData.Add(new ExcelFactory.ListItem("#FAIL_CNT_PHONE", "0 筆"));
            }
            if (ELECT_Count > 0)   //表示今天有台電(一般)
            {
                alSumData.Add(new ExcelFactory.ListItem("#DOWN_DTE_ELECT", TODAY_PROCESS_DTE.ToShortDateString()));
                alSumData.Add(new ExcelFactory.ListItem("#PAY_DTE_ELECT", PAY_DTE_ELECT.ToShortDateString()));
                alSumData.Add(new ExcelFactory.ListItem("#TOTAL_CNT_ELECT", ELECT_Count.ToString() + "筆"));
                alSumData.Add(new ExcelFactory.ListItem("#SUCC_CNT_ELECT", CHK_OK_ELECT_Count.ToString() + "筆"));
                alSumData.Add(new ExcelFactory.ListItem("#FAIL_CNT_ELECT", CHK_ERR_ELECT_Count.ToString() + "筆"));
            }
            else
            {
                alSumData.Add(new ExcelFactory.ListItem("#DOWN_DTE_ELECT", ""));
                alSumData.Add(new ExcelFactory.ListItem("#PAY_DTE_ELECT", ""));
                alSumData.Add(new ExcelFactory.ListItem("#TOTAL_CNT_ELECT", "0 筆"));
                alSumData.Add(new ExcelFactory.ListItem("#SUCC_CNT_ELECT", "0 筆"));
                alSumData.Add(new ExcelFactory.ListItem("#FAIL_CNT_ELECT", "0 筆"));
            }
            if (ELECT_H_Count > 0)   //表示今天有台電(高壓)
            {
                alSumData.Add(new ExcelFactory.ListItem("#DOWN_DTE_ELECT_H", TODAY_PROCESS_DTE.ToShortDateString()));
                alSumData.Add(new ExcelFactory.ListItem("#PAY_DTE_ELECT_H", PAY_DTE_ELECT_H.ToShortDateString()));
                alSumData.Add(new ExcelFactory.ListItem("#TOTAL_CNT_ELECT_H", ELECT_H_Count.ToString() + "筆"));
                alSumData.Add(new ExcelFactory.ListItem("#SUCC_CNT_ELECT_H", CHK_OK_ELECT_H_Count.ToString() + "筆"));
                alSumData.Add(new ExcelFactory.ListItem("#FAIL_CNT_ELECT_H", CHK_ERR_ELECT_H_Count.ToString() + "筆"));
            }
            else
            {
                alSumData.Add(new ExcelFactory.ListItem("#DOWN_DTE_ELECT_H", ""));
                alSumData.Add(new ExcelFactory.ListItem("#PAY_DTE_ELECT_H", ""));
                alSumData.Add(new ExcelFactory.ListItem("#TOTAL_CNT_ELECT_H", "0 筆"));
                alSumData.Add(new ExcelFactory.ListItem("#SUCC_CNT_ELECT_H", "0 筆"));
                alSumData.Add(new ExcelFactory.ListItem("#FAIL_CNT_ELECT_H", "0 筆"));
            }
            if (TWWATER_Count > 0)   //表示今天有省水
            {
                alSumData.Add(new ExcelFactory.ListItem("#DOWN_DTE_TWWATER", TODAY_PROCESS_DTE.ToShortDateString()));
                alSumData.Add(new ExcelFactory.ListItem("#PAY_DTE_TWWATER", PAY_DTE_TWWATER.ToShortDateString()));
                alSumData.Add(new ExcelFactory.ListItem("#TOTAL_CNT_TWWATER", TWWATER_Count.ToString() + "筆"));
                alSumData.Add(new ExcelFactory.ListItem("#SUCC_CNT_TWWATER", CHK_OK_TWWATER_Count.ToString() + "筆"));
                alSumData.Add(new ExcelFactory.ListItem("#FAIL_CNT_TWWATER", CHK_ERR_TWWATER_Count.ToString() + "筆"));
            }
            else
            {
                alSumData.Add(new ExcelFactory.ListItem("#DOWN_DTE_TWWATER", ""));
                alSumData.Add(new ExcelFactory.ListItem("#PAY_DTE_TWWATER", ""));
                alSumData.Add(new ExcelFactory.ListItem("#TOTAL_CNT_TWWATER", "0 筆"));
                alSumData.Add(new ExcelFactory.ListItem("#SUCC_CNT_TWWATER", "0 筆"));
                alSumData.Add(new ExcelFactory.ListItem("#FAIL_CNT_TWWATER", "0 筆"));
            }
            if (TPWATER_Count > 0)   //表示今天有市水
            {
                alSumData.Add(new ExcelFactory.ListItem("#DOWN_DTE_TPWATER", TODAY_PROCESS_DTE.ToShortDateString()));
                alSumData.Add(new ExcelFactory.ListItem("#PAY_DTE_TPWATER", PAY_DTE_TPWATER.ToShortDateString()));
                alSumData.Add(new ExcelFactory.ListItem("#TOTAL_CNT_TPWATER", TPWATER_Count.ToString() + "筆"));
                alSumData.Add(new ExcelFactory.ListItem("#SUCC_CNT_TPWATER", CHK_OK_TPWATER_Count.ToString() + "筆"));
                alSumData.Add(new ExcelFactory.ListItem("#FAIL_CNT_TPWATER", CHK_ERR_TPWATER_Count.ToString() + "筆"));
            }
            else
            {
                alSumData.Add(new ExcelFactory.ListItem("#DOWN_DTE_TPWATER", ""));
                alSumData.Add(new ExcelFactory.ListItem("#PAY_DTE_TPWATER", ""));
                alSumData.Add(new ExcelFactory.ListItem("#TOTAL_CNT_TPWATER", "0 筆"));
                alSumData.Add(new ExcelFactory.ListItem("#SUCC_CNT_TPWATER", "0 筆"));
                alSumData.Add(new ExcelFactory.ListItem("#FAIL_CNT_TPWATER", "0 筆"));
            }

            //設定合併欄位資料
            ArrayList alMegData = new ArrayList();

            //產出報表
            CMCRPT001.Output(inTable, alSumData, alMegData, "PBRAUP001(公用事業授權一覽表)", "PBRAUP001", "Sheet1", "Sheet1", TODAY_PROCESS_DTE);

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
            logger.writeCounter();

            //送授權筆數(內部檢核成功)
            logger.strTBL_NAME = "PUBLIC_HIST";
            logger.strTBL_ACCESS = "Q";
            logger.strMEMO = "CHECK OK";
            logger.intTBL_COUNT = CHK_OK_Count;
            logger.writeCounter();

            //內部檢核失敗
            logger.strTBL_NAME = "PUBLIC_HIST";
            logger.strTBL_ACCESS = "Q";
            logger.strMEMO = "CHECK ERROR";
            logger.intTBL_COUNT = CHK_ERR_Count;
            logger.writeCounter();

            //比對失敗
            logger.strTBL_NAME = "COLA_UPD_AUTH";
            logger.strTBL_ACCESS = "Q";
            logger.strMEMO = "match error";
            logger.intTBL_COUNT = NOMATCH_CNT;
            logger.writeCounter();


            //中華電信內部檢核失敗
            logger.strTBL_NAME = "PUBLIC_HIST";
            logger.strTBL_ACCESS = "Q";
            logger.strMEMO = "PHONE ERROR";
            logger.intTBL_COUNT = CHK_ERR_PHONE_Count;
            logger.writeCounter();

            //台電內部檢核失敗(一般)
            logger.strTBL_NAME = "PUBLIC_HIST";
            logger.strTBL_ACCESS = "Q";
            logger.strMEMO = "ELECT ERROR";
            logger.intTBL_COUNT = CHK_ERR_ELECT_Count;
            logger.writeCounter();

            //台電內部檢核失敗(高壓)
            logger.strTBL_NAME = "PUBLIC_HIST";
            logger.strTBL_ACCESS = "Q";
            logger.strMEMO = "ELECT_H ERROR";
            logger.intTBL_COUNT = CHK_ERR_ELECT_H_Count;
            logger.writeCounter();

            //省水內部檢核失敗
            logger.strTBL_NAME = "PUBLIC_HIST";
            logger.strTBL_ACCESS = "Q";
            logger.strMEMO = "TWATER ERROR";
            logger.intTBL_COUNT = CHK_ERR_TWWATER_Count;
            logger.writeCounter();

            //授權成功
            logger.strTBL_NAME = "COLA_TX_AUTH_TODAY";
            logger.strTBL_ACCESS = "I";
            logger.strMEMO = "APPROVE";
            logger.intTBL_COUNT = AUTH_APPROVE_Count;
            logger.writeCounter();

            //授權失敗
            logger.strTBL_NAME = "COLA_TX_AUTH_TODAY";
            logger.strTBL_ACCESS = "I";
            logger.strMEMO = "REJECT";
            logger.intTBL_COUNT = AUTH_REJECT_Count;
            logger.writeCounter();

            logger.DisplayCounter(Cybersoft.Coca.Log.LevelEnum.Info);
        }
        #endregion

    }
}

