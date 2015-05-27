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
    /// PBBELR001-代繳台電公司電費無法扣繳清單(BXD0090C.LST)
    /// </summary>
    public class PBBELR001
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
        #endregion
        #region 筆數
        #endregion
        #region index
        int i = 0;
        int z = 0;
        int t = 0;
        #endregion
        string KEY = string.Empty;
        string BR_NO = string.Empty;
        int intsub_tot = 0;
        int intsub_amt = 0;
        
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
            logger.strJOBNAME = "PBBELR001";
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
                #endregion
                //================================================================================
                #region 代繳台電公司電費無法扣繳清單
                PUBLIC_HIST.table_define();
                PUBLIC_HIST.init();
                PUBLIC_HIST.strWherePAY_RESULT = "0000";
                PUBLIC_HIST.strWherePAY_TYPE = "0004";
                PUBLIC_HIST.DateTimeWherePAY_DTE = dtTODAY_PROCESS_DTE;
                PUBLIC_HIST_RC = PUBLIC_HIST.query_for_BXD0090C();
                switch (PUBLIC_HIST_RC)
                {
                    case "S0000": //查詢成功
                        PUBLIC_HIST_Count_Query = PUBLIC_HIST.resultTable.Rows.Count;
                        logger.strJobQueue = "PUBLIC_HIST.query_for_BXD0090C() finish 筆數: " + PUBLIC_HIST_Count_Query.ToString("###,###,##0").PadLeft(11, ' ');
                        logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                        break;

                    case "F0023": //查無資料
                        logger.strJobQueue = PUBLIC_HIST_RC + " 本日無代繳台電公司電費無法扣繳清單資料";
                        logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                        return "B0000" + logger.strJobQueue;

                    default: //資料庫錯誤
                        logger.strJobQueue = "PUBLIC_HIST.query_for_BXD0090C()  錯誤 " + PUBLIC_HIST_RC;
                        logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                        return "B0016" + logger.strJobQueue;
                }
                #endregion
                if (PUBLIC_HIST_Count_Query > 0)
                {
                    PUBLISC_HIST_DATATABLE = PUBLIC_HIST.resultTable.Clone();
                    PUBLISC_HIST_T_TABLE = PUBLIC_HIST.resultTable.Clone();
                    PUBLISC_HIST_DATATABLE = PUBLIC_HIST.resultTable;
                    PUBLISC_HIST_DATATABLE.DefaultView.Sort = "KEY asc ";
                    PUBLISC_HIST_DATATABLE = PUBLISC_HIST_DATATABLE.DefaultView.ToTable();

                    #region 定義小計subtotal
                    inSubtotTable.Columns.Add("DATE_YYYY", typeof(string));
                    inSubtotTable.Columns["DATE_YYYY"].DefaultValue = dtTODAY_PROCESS_DTE.ToString("yyyy");
                    inSubtotTable.Columns.Add("DATE_MM", typeof(string));
                    inSubtotTable.Columns["DATE_MM"].DefaultValue = dtTODAY_PROCESS_DTE.ToString("MM").PadLeft(2, '0');
                    inSubtotTable.Columns.Add("DATE_DD", typeof(string));
                    inSubtotTable.Columns["DATE_DD"].DefaultValue = dtTODAY_PROCESS_DTE.ToString("dd").PadLeft(2, '0');
                    inSubtotTable.Columns.Add("RPT_DTE_YYYYMMDD", typeof(string));
                    inSubtotTable.Columns["RPT_DTE_YYYYMMDD"].DefaultValue = dtTODAY_PROCESS_DTE.ToString("yyyy/MM/dd");
                    inSubtotTable.Columns.Add("KEY", typeof(string));
                    inSubtotTable.Columns["KEY"].DefaultValue = " ";
                    inSubtotTable.Columns.Add("BR_NO", typeof(string));
                    inSubtotTable.Columns["BR_NO"].DefaultValue = " ";
                    inSubtotTable.Columns.Add("SUB_TOT_CNT", typeof(int));
                    inSubtotTable.Columns["SUB_TOT_CNT"].DefaultValue = 0;
                    inSubtotTable.Columns.Add("SUB_TOT_AMT", typeof(int));
                    inSubtotTable.Columns["SUB_TOT_AMT"].DefaultValue = 0;
                    #endregion

                    #region 暫存第一筆
                    inSubtotTable.Rows.Add(inSubtotTable.NewRow());
                    t = inSubtotTable.Rows.Count - 1;
                    KEY = PUBLISC_HIST_DATATABLE.Rows[0]["KEY"].ToString();
                    BR_NO = PUBLISC_HIST_DATATABLE.Rows[0]["BR_NO"].ToString();
                    #endregion
                }

                #region 轉出失敗原因
                for (i = 0; i < PUBLISC_HIST_DATATABLE.Rows.Count; i++)
                {
                  
                    #region 轉換失敗原因
                    string strPAY_RESULT = PUBLISC_HIST_DATATABLE.Rows[i]["PAY_RESULT"].ToString();
                    string strPAY_RESULT_descr = "";
                    switch (strPAY_RESULT)
                    {
                        case "S000":
                            strPAY_RESULT_descr = "00 - 入/扣帳成功";
                            break;

                        case "I001":
                            strPAY_RESULT_descr = "02 - 非委託代繳代發戶";
                            break;

                        case "I002":
                            strPAY_RESULT_descr = "03 - 已中止委託代繳代發戶";
                            break;

                        case "I003":
                        case "I007":
                            strPAY_RESULT_descr = "06 - 帳號已結清銷戶";
                            break;

                        case "I004":
                            strPAY_RESULT_descr = "98 - 其他";
                            break;

                        case "I005":
                            strPAY_RESULT_descr = "05 - 無此帳號";
                            break;

                        case "I006":
                            strPAY_RESULT_descr = "08 - 信用卡代繳戶額度無法代繳";
                            break;

                        default:
                            if (PUBLISC_HIST_DATATABLE.Rows[i]["PAY_NBR"].ToString() != "==無該當==")
                            {
                                strPAY_RESULT_descr = "98 - 其他";
                            }
                            break;
                    }
                    #endregion

                    
                    PUBLISC_HIST_T_TABLE.ImportRow(PUBLISC_HIST_DATATABLE.Rows[i]);
                    z = PUBLISC_HIST_T_TABLE.Rows.Count - 1;
                    PUBLISC_HIST_T_TABLE.Rows[z]["PAY_RESULT"] = strPAY_RESULT_descr;
                    
                    #region 加總筆數和金額
                    if (KEY != PUBLISC_HIST_DATATABLE.Rows[i]["KEY"].ToString())
                    {
                        
                        inSubtotTable.Rows[t]["KEY"] = KEY;
                        inSubtotTable.Rows[t]["BR_NO"] = BR_NO;
                        inSubtotTable.Rows[t]["SUB_TOT_CNT"] = intsub_tot;
                        inSubtotTable.Rows[t]["SUB_TOT_AMT"] = intsub_amt;

                        //換KEY
                        inSubtotTable.Rows.Add(inSubtotTable.NewRow());
                        t = inSubtotTable.Rows.Count - 1;

                        KEY = PUBLISC_HIST_DATATABLE.Rows[i]["KEY"].ToString();
                        BR_NO = PUBLISC_HIST_DATATABLE.Rows[i]["BR_NO"].ToString();

                        intsub_tot = 0;
                        intsub_amt = 0;

                        if (PUBLISC_HIST_DATATABLE.Rows[i]["PAY_NBR"].ToString() != "==無該當==")
                        {
                            intsub_tot++;
                            intsub_amt = Convert.ToInt32(PUBLISC_HIST_DATATABLE.Rows[i]["PAY_AMT"]);
                            inSubtotTable.Rows[t]["SUB_TOT_CNT"] = intsub_tot.ToString().PadLeft(5, ' ');
                            inSubtotTable.Rows[t]["SUB_TOT_AMT"] = intsub_amt.ToString().PadLeft(9, ' '); 
                        }
                    }
                    else
                    {
                        if (PUBLISC_HIST_DATATABLE.Rows[i]["PAY_NBR"].ToString() != "==無該當==")
                        {
                            intsub_tot++;
                            intsub_amt += Convert.ToInt32(PUBLISC_HIST_DATATABLE.Rows[i]["PAY_AMT"]);
                            inSubtotTable.Rows[t]["SUB_TOT_CNT"] = intsub_tot.ToString().PadLeft(5,' ');
                            inSubtotTable.Rows[t]["SUB_TOT_AMT"] = intsub_amt.ToString().PadLeft(9, ' '); 
                        }
                    }
                    #endregion
                }
                #endregion

                #region 寫出代繳台電公司電費無法扣繳清單
                writeReportBXD0090C(PUBLISC_HIST_T_TABLE);
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
        #region 寫出代繳台電公司電費無法扣繳清單
        void writeReportBXD0090C(DataTable inTable)
        {
            CMCRPT002 CMCRPT002 = new CMCRPT002();
            CMCRPT002.TemplateFileName = "BXD0090C";  //範本檔名
            CMCRPT002.isOverWrite = "Y";  //產出是否覆蓋既有檔案
            CMCRPT002.MaxPageCount = 50;  //產出報表每幾筆換一頁
            //設定表頭欄位資料
            ArrayList alHeaderData = new ArrayList();
            //設定表尾欄位資料
            ArrayList alTrailerData = new ArrayList();
            //設定小計資料表

            //產出報表(TXT)
            //string rc = CMCRPT002.Output(inTable, alHeaderData, null, inSubtotTable, "CAD0090C", dtTODAY_PROCESS_DTE);
            string rc = CMCRPT002.Output(inTable, alHeaderData, null, inSubtotTable, "BXD0090C", dtTODAY_PROCESS_DTE);
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
            logger.strMEMO = "代繳台電公司電費無法扣繳清單分行報表";
            logger.intTBL_COUNT = PUBLIC_HIST_Count_Query;
            logger.writeCounter();

            logger.DisplayCounter(Cybersoft.Coca.Log.LevelEnum.Info);
        }
        #endregion


    }
}
