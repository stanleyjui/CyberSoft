using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cybersoft.Data.DAL;
using System.Data;
using Cybersoft.Base;
using System.IO;
using Cybersoft.Data;
using System.Collections;
using Cybersoft.ExportDocument;
using System.Globalization;

namespace Cybersoft.Biz.Cust
{
    /// <summary>
    /// PBBAHR002，
    /// ACH信用卡代繳費用解繳彙總表CAARZ626
    /// 
    /// </summary>
    public class PBBAHR002
    {
        private string JobID;
        public string strJobID
        {
            get { return JobID; }
            set { JobID = value; }
        }

        #region 宣告table
        //宣告 PUBLIC_HIST
        string PUBLIC_HIST_RC = "";
        PUBLIC_HISTDao PUBLIC_HIST = new PUBLIC_HISTDao();
        DataTable PUBLIC_HIST_DataTable = new DataTable();
        int PUBLIC_HIST_Count_Query = 0;
        #endregion

        #region 宣告共用常數

        #endregion

        #region 宣告共用變數

        DateTime PREV_PROCESS_DTE = new DateTime();
        DateTime TODAY_PROCESS_DTE = new DateTime();
        DateTime NEXT_PROCESS_DTE = new DateTime();
        DateTime BANK_TODAY_PROCESS_DTE = new DateTime();
        DateTime BANK_NEXT_PROCESS_DTE = new DateTime();
        string strRSP_NO = string.Empty;

        int intACH_TOT_CNT = 0;
        int intACH_TOT_AMT = 0;
        int intACTIVE_TOT_CNT = 0;
        int intACTIVE_TOT_AMT = 0;
        int intPASSIVE_TOT_CNT = 0;
        int intPASSIVE_TOT_AMT = 0;
        
        int i = 0;
        #endregion

        #region 宣告共用類別
        #endregion

        #region 設定檔案編碼
        Encoding BIG5 = Encoding.GetEncoding("big5");
        #endregion

        static readonly Cybersoft.Coca.Log.Logger logger = Cybersoft.Log.LogManager<Cybersoft.Coca.Log.Logger>.GetLogger(System.Reflection.MethodInfo.GetCurrentMethod().ReflectedType.Name);
        //========================================================================================================
        public string RUN()
        {
            #region 準備MSGLG.SYSOUT CLASS
            logger.strJobID = JobID;
            logger.strJOBNAME = "PBBAHR002";
            logger.dtRunDate = DateTime.Now;
            #endregion
            try
            {
                #region 取得系統資訊(前次批次日、本日批次日)
                CMCSYS001 SYSINF = new CMCSYS001();
                SYSINF.strJobID = JobID;
                SYSINF.strJobName = "PBBAHR002";
                String SYSINF_RC = SYSINF.getSYSINF();
                PREV_PROCESS_DTE = SYSINF.datetimePREV_PROCESS_DTE;
                TODAY_PROCESS_DTE = SYSINF.datetimeTODAY_PROCESS_DTE;
                NEXT_PROCESS_DTE = SYSINF.datetimeNEXT_PROCESS_DTE;
                BANK_NEXT_PROCESS_DTE = SYSINF.datetimeBANK_NEXT_PROCESS_DTE;
                BANK_TODAY_PROCESS_DTE = SYSINF.datetimeBANK_TODAY_PROCESS_DTE;
                
                #endregion

                #region 定義明細
                PUBLIC_HIST_DataTable.Columns.Add("UT_NAME", typeof(string));
                PUBLIC_HIST_DataTable.Columns.Add("CNT", typeof(string));
                PUBLIC_HIST_DataTable.Columns.Add("PAY_AMT", typeof(string));
                PUBLIC_HIST_DataTable.Columns.Add("BUS_BANK_ACCOUNT", typeof(string));
                #endregion

                #region 撈取本次的清單
                PUBLIC_HIST.init();
                PUBLIC_HIST_RC = PUBLIC_HIST.query_for_ARZ626(TODAY_PROCESS_DTE, NEXT_PROCESS_DTE);
                switch (PUBLIC_HIST_RC.Substring(0, 5))
                {
                    case "S0000":  //查詢成功
                        PUBLIC_HIST_DataTable = PUBLIC_HIST.resultTable.Clone();
                        PUBLIC_HIST_Count_Query = PUBLIC_HIST.resultTable.Rows.Count;
                        break;
                    case "F0023":  //查無資料
                        PUBLIC_HIST_DataTable = PUBLIC_HIST.resultTable.Clone();
                        PUBLIC_HIST_Count_Query = 0;
                        logger.strJobQueue = "本日無ACH信用卡代繳費用解繳資料：" + PUBLIC_HIST_RC;
                        logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                        break;
                        //return "B0000:" + logger.strJobQueue;
                    default:       //資料庫錯誤
                        logger.strJobQueue = "[PUBLIC_HIST.query_for_ARZ626() ERROR ]：" + PUBLIC_HIST_RC;
                        logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                        return "B0016:" + logger.strJobQueue;
                }
                logger.strJobQueue = "讀取本日ACH信用卡代繳費用解繳資料共 " + PUBLIC_HIST.resultTable.Rows.Count + " 筆。";
                logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                #endregion

                #region 
                for (i = 0; i < PUBLIC_HIST_Count_Query; i++)
                {
                    PUBLIC_HIST_DataTable.Rows.Add();
                    int z = PUBLIC_HIST_DataTable.Rows.Count - 1;
                    PUBLIC_HIST_DataTable.Rows[z]["CNT"] = Convert.ToInt32(PUBLIC_HIST.resultTable.Rows[i]["CNT"]).ToString("###,##0").PadLeft(7, ' ');
                    PUBLIC_HIST_DataTable.Rows[z]["PAY_AMT"] = Convert.ToInt32(PUBLIC_HIST.resultTable.Rows[i]["PAY_AMT"]).ToString("###,###,##0").PadLeft(11, ' ');
                    PUBLIC_HIST_DataTable.Rows[z]["BUS_BANK_ACCOUNT"] = PUBLIC_HIST.resultTable.Rows[i]["BUS_BANK_ACCOUNT"].ToString().Trim();
                   //統計
                    intACH_TOT_CNT = intACH_TOT_CNT + Convert.ToInt32(PUBLIC_HIST.resultTable.Rows[i]["CNT"]);
                    intACH_TOT_AMT = intACH_TOT_AMT + Convert.ToInt32(PUBLIC_HIST.resultTable.Rows[i]["PAY_AMT"]);
                    if (PUBLIC_HIST.resultTable.Rows[i]["PAY_TYPE"].ToString().Trim() == "0031")
                    {
                        PUBLIC_HIST_DataTable.Rows[z]["UT_NAME"] = PUBLIC_HIST.resultTable.Rows[i]["UT_NAME"].ToString().Trim() + "(主動行)";
                        intACTIVE_TOT_CNT = intACTIVE_TOT_CNT + Convert.ToInt32(PUBLIC_HIST.resultTable.Rows[i]["CNT"]);
                        intACTIVE_TOT_AMT = intACTIVE_TOT_AMT + Convert.ToInt32(PUBLIC_HIST.resultTable.Rows[i]["PAY_AMT"]);
                    }
                    else
                    {
                        PUBLIC_HIST_DataTable.Rows[z]["UT_NAME"] = PUBLIC_HIST.resultTable.Rows[i]["UT_NAME"].ToString().Trim() + "(被動行)";
                        intPASSIVE_TOT_CNT = intPASSIVE_TOT_CNT + Convert.ToInt32(PUBLIC_HIST.resultTable.Rows[i]["CNT"]);
                        intPASSIVE_TOT_AMT = intPASSIVE_TOT_AMT + Convert.ToInt32(PUBLIC_HIST.resultTable.Rows[i]["PAY_AMT"]);
                    }
                    
                }
                #endregion

                #region 產生檔案CAARZ626.TXT
                writeReportARZ626(PUBLIC_HIST_DataTable);
                #endregion

                #region writeDisplay
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
        //========================================================================================================

        #region 寫出ACH信用卡代繳費用解繳彙總表清單
        void writeReportARZ626(DataTable inTable)
        {
            CMCRPT002 CMCRPT002 = new CMCRPT002();
            CMCRPT002.TemplateFileName = "CAARZ626";  //範本檔名
            CMCRPT002.isOverWrite = "Y";  //產出是否覆蓋既有檔案
            CMCRPT002.MaxPageCount = 50;  //產出報表每幾筆換一頁
            //設定表頭欄位資料
            ArrayList alHeaderData = new ArrayList();
            alHeaderData.Add(new ListItem("RPT_DTE_YYYYMMDD", DateTime.Now.ToString("yyyy/MM/dd")));
            alHeaderData.Add(new ListItem("PROC_DTE_YYYYMMDD", TODAY_PROCESS_DTE.ToString("yyyy/MM/dd")));
            //設定表尾欄位資料
            ArrayList alTrailerData = new ArrayList();
            alTrailerData.Add(new ListItem("ACH_TOT_CNT", intACH_TOT_CNT.ToString("N0"), "L"));
            alTrailerData.Add(new ListItem("ACH_TOT_AMT", intACH_TOT_AMT.ToString("N0"), "L"));
            alTrailerData.Add(new ListItem("ACTIVE_CNT", intACTIVE_TOT_CNT.ToString("N0"), "L"));
            alTrailerData.Add(new ListItem("ACTIVE_AMT", intACTIVE_TOT_AMT.ToString("N0"), "L"));
            alTrailerData.Add(new ListItem("PASSIVE_CNT", intPASSIVE_TOT_CNT.ToString("N0"), "L"));
            alTrailerData.Add(new ListItem("PASSIVE_AMT", intPASSIVE_TOT_AMT.ToString("N0"), "L"));
            
            //產出報表(TXT)
            string rc = CMCRPT002.Output(inTable, alHeaderData, alTrailerData, "CAARZ626", TODAY_PROCESS_DTE);
            if (rc != "")
            {
                logger.strJobQueue = rc;
                logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
            }
        }
        #endregion

        #region Display
        void writeDisplay()
        {
            logger.strTBL_NAME = "PUBLIC_HIST";
            logger.strTBL_ACCESS = "Q";
            logger.intTBL_COUNT = PUBLIC_HIST_Count_Query;
            logger.writeCounter();

            logger.strTBL_NAME = "ARZ626";
            logger.strTBL_ACCESS = "I";
            logger.intTBL_COUNT = intACH_TOT_CNT;
            logger.writeCounter();

            logger.DisplayCounter(Cybersoft.Coca.Log.LevelEnum.Info);
        }
        #endregion
    }
}
