using System;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cybersoft.Data.DAL;
using Cybersoft.Log;
using Cybersoft.Base;
using Cybersoft.ExportDocument;
using System.IO;
using System.Globalization;
using System.Text.RegularExpressions;


namespace Cybersoft.Biz.Cust
{
    /// <summary>
    /// PBBELR003-信用卡代繳電費(台電)扣帳資料(BXD0012C.LST)
    /// </summary>
    public class PBBELR003
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

        #region 宣告table
        //宣告PUBLIC_HIST
        String PUBLIC_HIST_RC = "";
        PUBLIC_HISTDao PUBLIC_HIST = new PUBLIC_HISTDao();
        int PUBLIC_HIST_Count_Query = 0;

        DataTable inSubtotTable = new DataTable();
        DataTable PUBLISC_HIST_T_TABLE = new DataTable();
        DataTable PUBLISC_HIST_DATATABLE = new DataTable();
        #endregion

        #region 宣告共用變數
        #region 批次日期
        DateTime dtPREV_PROCESS_DTE = new DateTime();
        DateTime dtTODAY_PROCESS_DTE = new DateTime();
        DateTime dtNEXT_PROCESS_DTE = new DateTime();
        DateTime dtBANK_NEXT_PROCESS_DTE = new DateTime();
        #endregion
        #region 筆數
        #endregion
        #region index
        int i = 0;
        int t = 0;
        #endregion
        string KEY = string.Empty;
        string POS_DTE = string.Empty;
        string BR_NO = string.Empty;
        Int32 intsub_succ = 0;
        Int32 intsub_succ_amt = 0;
        Int32 intsub_fail = 0;
        Int32 intsub_fail_amt = 0;
        Int32 intsub_fee_amt = 0;
        
        #endregion

        #region 設定檔案編碼
        Encoding BIG5 = Encoding.GetEncoding("big5");
        #endregion
 
        static readonly Cybersoft.Coca.Log.Logger logger = Cybersoft.Log.LogManager<Cybersoft.Coca.Log.Logger>.GetLogger(System.Reflection.MethodInfo.GetCurrentMethod().ReflectedType.Name);
        //========================================================================================================
        #region 程式主邏輯【MAIN Routine】
        public string RUN()
        {
            #region 準備MSGLG.SYSOUT CLASS
            logger.strJobID = JobID;
            logger.strJOBNAME = "PBBELR003";
            logger.dtRunDate = DateTime.Now;
            #endregion
            try
            {
                #region 取得系統資訊(前次批次日、本日批次日)
                CMCSYS001 SYSINF = new CMCSYS001();
                SYSINF.strJobID = JobID;
                SYSINF.strJobName = "ODBRPT001";
                String SYSINF_RC = SYSINF.getSYSINF();
                dtPREV_PROCESS_DTE = SYSINF.datetimePREV_PROCESS_DTE;
                dtTODAY_PROCESS_DTE = SYSINF.datetimeTODAY_PROCESS_DTE;
                dtNEXT_PROCESS_DTE = SYSINF.datetimeNEXT_PROCESS_DTE;
                dtBANK_NEXT_PROCESS_DTE = SYSINF.datetimeBANK_NEXT_PROCESS_DTE;
                #endregion
                //================================================================================
                #region 信用卡代繳電費(台電)扣帳資料
                PUBLIC_HIST.table_define();
                PUBLIC_HIST.init();
                PUBLIC_HIST.strWherePAY_TYPE = "0004";
                PUBLIC_HIST.DateTimeWherePAY_DTE = dtTODAY_PROCESS_DTE;
                PUBLIC_HIST_RC = PUBLIC_HIST.query_for_BXD0012C();
                switch (PUBLIC_HIST_RC)
                {
                    case "S0000": //查詢成功
                        PUBLIC_HIST_Count_Query = PUBLIC_HIST.resultTable.Rows.Count;
                        logger.strJobQueue = "PUBLIC_HIST.query_for_BXD0012C() finish 筆數: " + PUBLIC_HIST_Count_Query.ToString("###,###,##0").PadLeft(11, ' ');
                        logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                        break;

                    case "F0023": //查無資料
                        logger.strJobQueue = PUBLIC_HIST_RC + " 本日無信用卡代繳電費(台電)扣帳資料";
                        logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                        return "B0000" + logger.strJobQueue;

                    default: //資料庫錯誤
                        logger.strJobQueue = "PUBLIC_HIST.query_for_BXD0012C()  錯誤 " + PUBLIC_HIST_RC;
                        logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                        return "B0016" + logger.strJobQueue;
                }
                #endregion
                if (PUBLIC_HIST_Count_Query > 0)
                {
                    PUBLISC_HIST_DATATABLE = PUBLIC_HIST.resultTable.Clone();
                    PUBLISC_HIST_DATATABLE = PUBLIC_HIST.resultTable;
                    PUBLISC_HIST_DATATABLE.DefaultView.Sort = "KEY asc ";
                    PUBLISC_HIST_DATATABLE = PUBLISC_HIST_DATATABLE.DefaultView.ToTable();

                    #region 定義明細
                    PUBLISC_HIST_T_TABLE.Columns.Add("BR_NO", typeof(string));
                    PUBLISC_HIST_T_TABLE.Columns.Add("ADDR_BRANCH", typeof(string));
                    PUBLISC_HIST_T_TABLE.Columns.Add("KEY", typeof(string));
                    PUBLISC_HIST_T_TABLE.Columns.Add("TRAN_TYPE", typeof(string));
                    PUBLISC_HIST_T_TABLE.Columns.Add("POS_DTE", typeof(string));
                    PUBLISC_HIST_T_TABLE.Columns.Add("SUCC_CNT", typeof(string));
                    PUBLISC_HIST_T_TABLE.Columns.Add("SUCC_AMT", typeof(string));
                    PUBLISC_HIST_T_TABLE.Columns.Add("FEE_AMT", typeof(string));
                    PUBLISC_HIST_T_TABLE.Columns.Add("FAIL_CNT", typeof(string));
                    PUBLISC_HIST_T_TABLE.Columns.Add("FAIL_AMT", typeof(string));
                    #endregion

                    #region 定義小計subtotal
                    inSubtotTable.Columns.Add("RPT_DTE_YYYYMMDD", typeof(string));
                    inSubtotTable.Columns["RPT_DTE_YYYYMMDD"].DefaultValue = DateTime.Now.ToString("yyyy/MM/dd");

                    inSubtotTable.Columns.Add("PROC_DTE_YYYYMMDD", typeof(string));
                    inSubtotTable.Columns["PROC_DTE_YYYYMMDD"].DefaultValue = dtBANK_NEXT_PROCESS_DTE.ToString("yyyy/MM/dd");

                    inSubtotTable.Columns.Add("POS_DTE_YYYYMMDD", typeof(string));
                    inSubtotTable.Columns["POS_DTE_YYYYMMDD"].DefaultValue = dtTODAY_PROCESS_DTE.ToString("yyyy/MM/dd");

                    inSubtotTable.Columns.Add("KEY", typeof(string));
                    inSubtotTable.Columns["KEY"].DefaultValue = " ";

                    inSubtotTable.Columns.Add("SUB_SUCC_CNT", typeof(string));
                    inSubtotTable.Columns["SUB_SUCC_CNT"].DefaultValue = "";

                    inSubtotTable.Columns.Add("SUB_SUCC_AMT", typeof(string));
                    inSubtotTable.Columns["SUB_SUCC_AMT"].DefaultValue = "";

                    inSubtotTable.Columns.Add("SUB_FEE_AMT", typeof(string));
                    inSubtotTable.Columns["SUB_FEE_AMT"].DefaultValue = "";

                    inSubtotTable.Columns.Add("SUB_FAIL_CNT", typeof(string));
                    inSubtotTable.Columns["SUB_FAIL_CNT"].DefaultValue = "";

                    inSubtotTable.Columns.Add("SUB_FAIL_AMT", typeof(string));
                    inSubtotTable.Columns["SUB_FAIL_AMT"].DefaultValue = "";
                    #endregion

                    #region 暫存第一筆
                    inSubtotTable.Rows.Add(inSubtotTable.NewRow());
                    t = inSubtotTable.Rows.Count - 1;
                    KEY = PUBLISC_HIST_DATATABLE.Rows[0]["KEY"].ToString();
                    //POS_DTE = Convert.ToDateTime(PUBLISC_HIST_DATATABLE.Rows[0]["PAY_DTE"]).ToString("yyyy/MM/dd"); 
                    POS_DTE = dtTODAY_PROCESS_DTE.ToString("yyyy/MM/dd"); 
                    #endregion
                }

                #region 轉出內容明細
                for (i = 0; i < PUBLISC_HIST_DATATABLE.Rows.Count; i++)
                {
                    #region 搬入報表detail
                    PUBLISC_HIST_T_TABLE.Rows.Add();
                    int z = PUBLISC_HIST_T_TABLE.Rows.Count - 1;
                    PUBLISC_HIST_T_TABLE.Rows[z]["BR_NO"] = PUBLISC_HIST_DATATABLE.Rows[i]["BR_NO"];
                    PUBLISC_HIST_T_TABLE.Rows[z]["ADDR_BRANCH"] = PUBLISC_HIST_DATATABLE.Rows[i]["ADDR_BRANCH"];
                    PUBLISC_HIST_T_TABLE.Rows[z]["KEY"] = PUBLISC_HIST_DATATABLE.Rows[i]["KEY"];
                    if (PUBLISC_HIST_DATATABLE.Rows[i]["ADDR_BRANCH"].ToString().Trim() == "")
                    {
                        PUBLISC_HIST_T_TABLE.Rows[z]["TRAN_TYPE"] = "";
                        PUBLISC_HIST_T_TABLE.Rows[z]["POS_DTE"] = "";
                    }
                    else
                    {
                        PUBLISC_HIST_T_TABLE.Rows[z]["TRAN_TYPE"] = "2";
                        //PUBLISC_HIST_T_TABLE.Rows[z]["POS_DTE"] = Convert.ToDateTime(PUBLISC_HIST_DATATABLE.Rows[i]["PAY_DTE"]).ToString("yyyy/MM/dd"); 
                        PUBLISC_HIST_T_TABLE.Rows[z]["POS_DTE"] = dtTODAY_PROCESS_DTE.ToString("yyyy/MM/dd"); 
                    }
                    PUBLISC_HIST_T_TABLE.Rows[z]["SUCC_CNT"] = Convert.ToInt32(PUBLISC_HIST_DATATABLE.Rows[i]["SUCC_CNT"]).ToString("###,##0").PadLeft(7, ' ');
                    PUBLISC_HIST_T_TABLE.Rows[z]["SUCC_AMT"] = Convert.ToInt32(PUBLISC_HIST_DATATABLE.Rows[i]["SUCC_AMT"]).ToString("###,###,##0").PadLeft(11, ' ');
                    PUBLISC_HIST_T_TABLE.Rows[z]["FEE_AMT"] = Convert.ToInt32(PUBLISC_HIST_DATATABLE.Rows[i]["FEE_AMT"]).ToString("###,###,##0").PadLeft(11, ' ');
                    PUBLISC_HIST_T_TABLE.Rows[z]["FAIL_CNT"] = Convert.ToInt32(PUBLISC_HIST_DATATABLE.Rows[i]["FAIL_CNT"]).ToString("###,##0").PadLeft(7, ' ');
                    PUBLISC_HIST_T_TABLE.Rows[z]["FAIL_AMT"] = Convert.ToInt32(PUBLISC_HIST_DATATABLE.Rows[i]["FAIL_AMT"]).ToString("###,###,##0").PadLeft(11, ' ');
                    #endregion

                    #region 加總筆數和金額
                    if (KEY != PUBLISC_HIST_DATATABLE.Rows[i]["KEY"].ToString())
                    {
                        inSubtotTable.Rows[t]["KEY"] = KEY;
                        inSubtotTable.Rows[t]["POS_DTE_YYYYMMDD"] = POS_DTE;
                        inSubtotTable.Rows[t]["SUB_SUCC_CNT"] = intsub_succ.ToString("###,##0").PadLeft(7, ' ');
                        inSubtotTable.Rows[t]["SUB_SUCC_AMT"] = intsub_succ_amt.ToString("###,###,##0").PadLeft(11, ' ');
                        inSubtotTable.Rows[t]["SUB_FEE_AMT"] = intsub_fee_amt.ToString("###,###,##0").PadLeft(11, ' ');
                        inSubtotTable.Rows[t]["SUB_FAIL_CNT"] = intsub_fail.ToString("###,##0").PadLeft(7, ' ');
                        inSubtotTable.Rows[t]["SUB_FAIL_AMT"] = intsub_fail_amt.ToString("###,###,##0").PadLeft(11, ' ');
                        //換KEY
                        inSubtotTable.Rows.Add(inSubtotTable.NewRow());
                        t = inSubtotTable.Rows.Count - 1;

                        KEY = PUBLISC_HIST_DATATABLE.Rows[i]["KEY"].ToString();
                        POS_DTE = Convert.ToDateTime(PUBLISC_HIST_DATATABLE.Rows[i]["PAY_DTE"]).ToString("yyyy/MM/dd");
                        POS_DTE = dtTODAY_PROCESS_DTE.ToString("yyyy/MM/dd");
                        intsub_succ = 0;
                        intsub_succ_amt = 0;
                        intsub_fee_amt = 0;
                        intsub_fail = 0;
                        intsub_fail_amt = 0;

                        if (Convert.ToInt32(PUBLISC_HIST_DATATABLE.Rows[i]["SUCC_CNT"]) > 0)
                        {
                            intsub_succ += Convert.ToInt32(PUBLISC_HIST_DATATABLE.Rows[i]["SUCC_CNT"]);
                            intsub_succ_amt += Convert.ToInt32(PUBLISC_HIST_DATATABLE.Rows[i]["SUCC_AMT"]);
                        }
                        if (Convert.ToInt32(PUBLISC_HIST_DATATABLE.Rows[i]["FEE_AMT"]) > 0)
                        {
                            intsub_fee_amt += Convert.ToInt32(PUBLISC_HIST_DATATABLE.Rows[i]["FEE_AMT"]);
                        }
                        if (Convert.ToInt32(PUBLISC_HIST_DATATABLE.Rows[i]["FAIL_CNT"]) > 0)
                        {
                            intsub_fail += Convert.ToInt32(PUBLISC_HIST_DATATABLE.Rows[i]["FAIL_CNT"]);
                            intsub_fail_amt += Convert.ToInt32(PUBLISC_HIST_DATATABLE.Rows[i]["FAIL_AMT"]);
                        }
                        inSubtotTable.Rows[t]["KEY"] = KEY;
                        inSubtotTable.Rows[t]["POS_DTE_YYYYMMDD"] = POS_DTE;
                        inSubtotTable.Rows[t]["SUB_SUCC_CNT"] = intsub_succ.ToString("###,##0").PadLeft(7, ' ');
                        inSubtotTable.Rows[t]["SUB_SUCC_AMT"] = intsub_succ_amt.ToString("###,###,##0").PadLeft(11, ' ');
                        inSubtotTable.Rows[t]["SUB_FEE_AMT"] = intsub_fee_amt.ToString("###,###,##0").PadLeft(11, ' ');
                        inSubtotTable.Rows[t]["SUB_FAIL_CNT"] = intsub_fail.ToString("###,##0").PadLeft(7, ' ');
                        inSubtotTable.Rows[t]["SUB_FAIL_AMT"] = intsub_fail_amt.ToString("###,###,##0").PadLeft(11, ' ');

                    }
                    else
                    {
                        if (Convert.ToInt32(PUBLISC_HIST_DATATABLE.Rows[i]["SUCC_CNT"]) > 0)
                        {
                            intsub_succ += Convert.ToInt32(PUBLISC_HIST_DATATABLE.Rows[i]["SUCC_CNT"]);
                            intsub_succ_amt += Convert.ToInt32(PUBLISC_HIST_DATATABLE.Rows[i]["SUCC_AMT"]);
                        }
                        if (Convert.ToInt32(PUBLISC_HIST_DATATABLE.Rows[i]["FEE_AMT"]) > 0)
                        {
                            intsub_fee_amt += Convert.ToInt32(PUBLISC_HIST_DATATABLE.Rows[i]["FEE_AMT"]);
                        }
                        if (Convert.ToInt32(PUBLISC_HIST_DATATABLE.Rows[i]["FAIL_CNT"]) > 0)
                        {
                            intsub_fail += Convert.ToInt32(PUBLISC_HIST_DATATABLE.Rows[i]["FAIL_CNT"]);
                            intsub_fail_amt += Convert.ToInt32(PUBLISC_HIST_DATATABLE.Rows[i]["FAIL_AMT"]);
                        }
                        inSubtotTable.Rows[t]["KEY"] = KEY;
                        inSubtotTable.Rows[t]["POS_DTE_YYYYMMDD"] = POS_DTE;
                        inSubtotTable.Rows[t]["SUB_SUCC_CNT"] = intsub_succ.ToString("###,##0").PadLeft(7, ' ');
                        inSubtotTable.Rows[t]["SUB_SUCC_AMT"] = intsub_succ_amt.ToString("###,###,##0").PadLeft(11, ' ');
                        inSubtotTable.Rows[t]["SUB_FEE_AMT"] = intsub_fee_amt.ToString("###,###,##0").PadLeft(11, ' ');
                        inSubtotTable.Rows[t]["SUB_FAIL_CNT"] = intsub_fail.ToString("###,##0").PadLeft(7, ' ');
                        inSubtotTable.Rows[t]["SUB_FAIL_AMT"] = intsub_fail_amt.ToString("###,###,##0").PadLeft(11, ' ');

                    }
                    #endregion
                }
                #endregion

                #region 寫出信用卡代繳電費(台電)扣帳資料
                writeReportBXD0012C(PUBLISC_HIST_T_TABLE);
                #endregion
                //================================================================================ 

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
        #region 寫出信用卡代繳電費(台電)扣帳資料
        void writeReportBXD0012C(DataTable inTable)
        {
            CMCRPT002 CMCRPT002 = new CMCRPT002();
            CMCRPT002.TemplateFileName = "BXD0012C";  //範本檔名
            CMCRPT002.isOverWrite = "Y";  //產出是否覆蓋既有檔案
            CMCRPT002.MaxPageCount = 50;  //產出報表每幾筆換一頁
            //設定表頭欄位資料
            ArrayList alHeaderData = new ArrayList();
            alHeaderData.Add(new ListItem("RPT_DTE_YYYYMMDD", DateTime.Now.ToString("yyyy/MM/dd")));
            alHeaderData.Add(new ListItem("RPT_DTE_HHmmss", DateTime.Now.ToString("HH:mm:ss")));
            //設定表尾欄位資料
            ArrayList alTrailerData = new ArrayList();
            //設定小計資料表

            //產出報表(TXT)
            //string rc = CMCRPT002.Output(inTable, alHeaderData, null, inSubtotTable, "CAD0012C", dtTODAY_PROCESS_DTE);
            string rc = CMCRPT002.Output(inTable, alHeaderData, null, inSubtotTable, "BXD0012C", dtTODAY_PROCESS_DTE);
            if (rc != "")
            {
                logger.strJobQueue = rc;
                logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
            }
        }
        #endregion
        //========================================================================================================      
        #region Display
        void writeDisplay()
        {
            //帳簿資料JOIN帳戶資訊
            logger.strTBL_NAME = "PUBLIC_HIST";
            logger.strTBL_ACCESS = "Q";
            logger.strMEMO = "信用卡代繳電費(台電)扣帳資料分行報表";
            logger.intTBL_COUNT = PUBLIC_HIST_Count_Query;
            logger.writeCounter();

            logger.DisplayCounter(Cybersoft.Coca.Log.LevelEnum.Info);
        }
        #endregion


    }
}
