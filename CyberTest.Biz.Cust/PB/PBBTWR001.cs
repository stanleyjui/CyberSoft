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
    /// PBBTWR001-信用卡代繳水費(台灣省自來水)手續費清單(CAD0001C.txt)
    /// </summary>
    public class PBBTWR001
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
        Int32 intsub_succ_cnt = 0;
        Int32 intsub_succ_amt = 0;
        Int32 intsub_fail_cnt = 0;
        Int32 intsub_fail_amt = 0;
        Int32 intsub_fee_amt = 0;
        Int32 intsub_net_amt = 0;
        
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
            logger.strJOBNAME = "PBBTWR001";
            logger.dtRunDate = DateTime.Now;
            #endregion
            try
            {
                #region 取得系統資訊(前次批次日、本日批次日)
                CMCSYS001 SYSINF = new CMCSYS001();
                SYSINF.strJobID = JobID;
                SYSINF.strJobName = "PBBTWR001";
                String SYSINF_RC = SYSINF.getSYSINF();
                dtPREV_PROCESS_DTE = SYSINF.datetimePREV_PROCESS_DTE;
                dtTODAY_PROCESS_DTE = SYSINF.datetimeTODAY_PROCESS_DTE;
                dtNEXT_PROCESS_DTE = SYSINF.datetimeNEXT_PROCESS_DTE;
                dtBANK_NEXT_PROCESS_DTE = SYSINF.datetimeBANK_NEXT_PROCESS_DTE;
                //for test
                //dtTODAY_PROCESS_DTE = DateTime.Parse("2012-07-12");
                //dtTODAY_PROCESS_DTE = DateTime.Parse("2012-09-17");
                #endregion
                //================================================================================

                #region 取得站所編碼第一碼
                PUBLIC_HIST_RC = PUBLIC_HIST.query_for_PAY_NBR("0003", dtTODAY_PROCESS_DTE.ToString("yyyyMMdd"));
                switch (PUBLIC_HIST_RC)
                {
                    case "S0000": //查詢成功
                        break;

                    case "F0023": //查無資料
                        logger.strJobQueue = PUBLIC_HIST_RC + " 本日無信用卡代繳水費(省水)手續費清單";
                        logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                        return "B0000" + logger.strJobQueue;

                    default: //資料庫錯誤
                        logger.strJobQueue = "PUBLIC_HIST.query_for_PAY_NBR()  錯誤 " + PUBLIC_HIST_RC;
                        logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                        return "B0016" + logger.strJobQueue;
                }

                int intCNT_PAY_NBR = PUBLIC_HIST.resultTable.Rows.Count;
                string[] aryPAY_NBR_S = new string[intCNT_PAY_NBR];
                for (int i = 0; i < intCNT_PAY_NBR; i++)
                {
                    if (PUBLIC_HIST.resultTable.Rows[i]["PAY_NBR_S"].ToString().Trim() != "")
                    {
                        aryPAY_NBR_S[i] = PUBLIC_HIST.resultTable.Rows[i]["PAY_NBR_S"].ToString().Trim();
                    }
                }

                #endregion

                #region 信用卡代繳水費(省水)手續費清單(依站所第一碼排序)
                PUBLIC_HIST.table_define();
                PUBLIC_HIST.init();
                PUBLIC_HIST.strWherePAY_TYPE = "0003";
                PUBLIC_HIST.strWhereTRANS_DTE = dtTODAY_PROCESS_DTE.ToString("yyyyMMdd");

                for (int i = 0; i < intCNT_PAY_NBR; i++)
                {
                    PUBLIC_HIST_RC = PUBLIC_HIST.query_for_CAD0001C(aryPAY_NBR_S[i]);//aryPAY_NBR_S[i]: 站所編號第一碼

                    switch (PUBLIC_HIST_RC)
                    {
                        case "S0000": //查詢成功
                            PUBLIC_HIST_Count_Query = PUBLIC_HIST.resultTable.Rows.Count;
                            logger.strJobQueue = "PUBLIC_HIST.query_for_CAD0001C() 站台:" + aryPAY_NBR_S[i] + " 筆數: " + PUBLIC_HIST_Count_Query.ToString("###,###,##0").PadLeft(11, ' ');
                            logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                            break;

                        case "F0023": //查無資料
                            logger.strJobQueue = PUBLIC_HIST_RC + " 本日無(站台:" + aryPAY_NBR_S[i] + ")信用卡代繳水費(省水)手續費清單";
                            logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                            return "B0000" + logger.strJobQueue;

                        default: //資料庫錯誤
                            logger.strJobQueue = "PUBLIC_HIST.query_for_CAD0001C()  (站台:" + aryPAY_NBR_S[i] + ")錯誤 " + PUBLIC_HIST_RC;
                            logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                            return "B0016" + logger.strJobQueue;
                    }

                    if (PUBLIC_HIST_Count_Query > 0)
                    {
                        if (i == 0)
                        {
                            PUBLISC_HIST_DATATABLE = PUBLIC_HIST.resultTable.Clone();//複製結構
                        }
                        for (int g = 0; g < PUBLIC_HIST_Count_Query; g++)
                        {
                            PUBLISC_HIST_DATATABLE.ImportRow(PUBLIC_HIST.resultTable.Rows[g]); //依站台組出 GROUP BY 後的 TABLE
                        }
                    }

                }
                #endregion

                #region 定義小計subtotal

                inSubtotTable.Columns.Add("POS_DTE_YYYYMMDD", typeof(string));
                inSubtotTable.Columns["POS_DTE_YYYYMMDD"].DefaultValue = dtTODAY_PROCESS_DTE.ToString("yyyy/MM/dd");

                inSubtotTable.Columns.Add("KEY", typeof(string));
                inSubtotTable.Columns["KEY"].DefaultValue = "";

                inSubtotTable.Columns.Add("SUB_SUCC_CNT", typeof(string));
                inSubtotTable.Columns["SUB_SUCC_CNT"].DefaultValue = "";

                inSubtotTable.Columns.Add("SUB_SUCC_AMT", typeof(string));
                inSubtotTable.Columns["SUB_SUCC_AMT"].DefaultValue = "";

                inSubtotTable.Columns.Add("SUB_FAIL_CNT", typeof(string));
                inSubtotTable.Columns["SUB_FAIL_CNT"].DefaultValue = "";

                inSubtotTable.Columns.Add("SUB_FAIL_AMT", typeof(string));
                inSubtotTable.Columns["SUB_FAIL_AMT"].DefaultValue = "";

                inSubtotTable.Columns.Add("SUB_FEE_AMT", typeof(string));
                inSubtotTable.Columns["SUB_FEE_AMT"].DefaultValue = "";

                inSubtotTable.Columns.Add("SUB_NET_AMT", typeof(string));
                inSubtotTable.Columns["SUB_NET_AMT"].DefaultValue = "";

                inSubtotTable.Columns.Add("SUB_CNT", typeof(string));
                inSubtotTable.Columns["SUB_CNT"].DefaultValue = "";

                #endregion

                #region 轉出內容明細
                string strPAY_NBR_SS = "";
                bool IsEnd = false;
                for (i = 0; i < PUBLISC_HIST_DATATABLE.Rows.Count; i++)
                {
                    #region 加總筆數和金額(BY 相同站所)
                    strPAY_NBR_SS = PUBLISC_HIST_DATATABLE.Rows[i]["PAY_NBR_SS"].ToString().PadRight(2, ' ');
                    if (strPAY_NBR_SS.Substring(0, 1) == aryPAY_NBR_S[t])
                    {
                        intsub_succ_cnt += Convert.ToInt32(PUBLISC_HIST_DATATABLE.Rows[i]["SUCC_CNT"]);
                        intsub_succ_amt += Convert.ToInt32(PUBLISC_HIST_DATATABLE.Rows[i]["SUCC_AMT"]);
                        intsub_fail_cnt += Convert.ToInt32(PUBLISC_HIST_DATATABLE.Rows[i]["FAIL_CNT"]);
                        intsub_fail_amt += Convert.ToInt32(PUBLISC_HIST_DATATABLE.Rows[i]["FAIL_AMT"]);
                        intsub_fee_amt += Convert.ToInt32(PUBLISC_HIST_DATATABLE.Rows[i]["FEE_AMT"]);//成功手續費
                        intsub_net_amt += Convert.ToInt32(PUBLISC_HIST_DATATABLE.Rows[i]["NET_AMT"]);//成功解繳金額

                        #region 累計 站所最後一筆資料
                        if (i == PUBLISC_HIST_DATATABLE.Rows.Count -1)
                        {
                            IsEnd = true;

                            inSubtotTable.Rows.Add(inSubtotTable.NewRow());

                            inSubtotTable.Rows[t]["KEY"] = aryPAY_NBR_S[t];
                            inSubtotTable.Rows[t]["SUB_SUCC_CNT"] = intsub_succ_cnt.ToString("###,##0").PadLeft(7, ' ');
                            inSubtotTable.Rows[t]["SUB_SUCC_AMT"] = intsub_succ_amt.ToString("###,###,##0").PadLeft(11, ' ');
                            inSubtotTable.Rows[t]["SUB_FAIL_CNT"] = intsub_fail_cnt.ToString("###,##0").PadLeft(7, ' ');
                            inSubtotTable.Rows[t]["SUB_FAIL_AMT"] = intsub_fail_amt.ToString("###,###,##0").PadLeft(11, ' ');
                            inSubtotTable.Rows[t]["SUB_FEE_AMT"] = intsub_fee_amt.ToString("###,###,##0").PadLeft(11, ' ');
                            inSubtotTable.Rows[t]["SUB_NET_AMT"] = intsub_net_amt.ToString("###,###,##0").PadLeft(11, ' ');

                            inSubtotTable.Rows[t]["SUB_CNT"] = (t+1).ToString("##0").PadLeft(3, ' ');
                        }
                        #endregion
                    }
                    else
                    {
                        inSubtotTable.Rows.Add(inSubtotTable.NewRow());

                        inSubtotTable.Rows[t]["KEY"] = aryPAY_NBR_S[t];
                        inSubtotTable.Rows[t]["SUB_SUCC_CNT"] = intsub_succ_cnt.ToString("###,##0").PadLeft(7, ' ');
                        inSubtotTable.Rows[t]["SUB_SUCC_AMT"] = intsub_succ_amt.ToString("###,###,##0").PadLeft(11, ' ');
                        inSubtotTable.Rows[t]["SUB_FAIL_CNT"] = intsub_fail_cnt.ToString("###,##0").PadLeft(7, ' ');
                        inSubtotTable.Rows[t]["SUB_FAIL_AMT"] = intsub_fail_amt.ToString("###,###,##0").PadLeft(11, ' ');
                        inSubtotTable.Rows[t]["SUB_FEE_AMT"] = intsub_fee_amt.ToString("###,###,##0").PadLeft(11, ' ');
                        inSubtotTable.Rows[t]["SUB_NET_AMT"] = intsub_net_amt.ToString("###,###,##0").PadLeft(11, ' ');
                        inSubtotTable.Rows[t]["SUB_CNT"] = (t+1).ToString("##0").PadLeft(3, ' '); //頁數

                        t = t + 1;
                        

                        #region 累計 站所第一筆資料
                        intsub_succ_cnt = 0;
                        intsub_succ_amt = 0;
                        intsub_fail_cnt = 0;
                        intsub_fail_amt = 0;
                        intsub_fee_amt = 0;
                        intsub_net_amt = 0;
                        intsub_succ_cnt += Convert.ToInt32(PUBLISC_HIST_DATATABLE.Rows[i]["SUCC_CNT"]);
                        intsub_succ_amt += Convert.ToInt32(PUBLISC_HIST_DATATABLE.Rows[i]["SUCC_AMT"]);
                        intsub_fail_cnt += Convert.ToInt32(PUBLISC_HIST_DATATABLE.Rows[i]["FAIL_CNT"]);
                        intsub_fail_amt += Convert.ToInt32(PUBLISC_HIST_DATATABLE.Rows[i]["FAIL_AMT"]);
                        intsub_fee_amt += Convert.ToInt32(PUBLISC_HIST_DATATABLE.Rows[i]["FEE_AMT"]);//成功手續費
                        intsub_net_amt += Convert.ToInt32(PUBLISC_HIST_DATATABLE.Rows[i]["NET_AMT"]);//成功解繳金額
                        #endregion

                    }
                    #endregion
                }

                if (IsEnd == false)
                {
                    inSubtotTable.Rows.Add(inSubtotTable.NewRow());

                    inSubtotTable.Rows[t]["KEY"] = aryPAY_NBR_S[t];
                    inSubtotTable.Rows[t]["SUB_SUCC_CNT"] = intsub_succ_cnt.ToString("###,##0").PadLeft(7, ' ');
                    inSubtotTable.Rows[t]["SUB_SUCC_AMT"] = intsub_succ_amt.ToString("###,###,##0").PadLeft(11, ' ');
                    inSubtotTable.Rows[t]["SUB_FAIL_CNT"] = intsub_fail_cnt.ToString("###,##0").PadLeft(7, ' ');
                    inSubtotTable.Rows[t]["SUB_FAIL_AMT"] = intsub_fail_amt.ToString("###,###,##0").PadLeft(11, ' ');
                    inSubtotTable.Rows[t]["SUB_FEE_AMT"] = intsub_fee_amt.ToString("###,###,##0").PadLeft(11, ' ');
                    inSubtotTable.Rows[t]["SUB_NET_AMT"] = intsub_net_amt.ToString("###,###,##0").PadLeft(11, ' ');
                    inSubtotTable.Rows[t]["SUB_CNT"] = (t + 1).ToString("##0").PadLeft(3, ' '); //頁數
                }

                #endregion

                #region 寫出信用卡代繳水費(省水)手續費清單

                writeReportBXD0012C(PUBLISC_HIST_DATATABLE);

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
        #region 寫出信用卡代繳水費(省水)手續費清單
        void writeReportBXD0012C(DataTable inTable)
        {
            CMCRPT002 CMCRPT002 = new CMCRPT002();
            CMCRPT002.TemplateFileName = "CAD0001C";  //範本檔名
            CMCRPT002.isOverWrite = "Y";  //產出是否覆蓋既有檔案
            CMCRPT002.MaxPageCount = 50;  //產出報表每幾筆換一頁 vvv
            //設定表頭欄位資料
            ArrayList alHeaderData = new ArrayList();
            alHeaderData.Add(new ListItem("RPT_DTE_YYYYMMDD", DateTime.Now.ToString("yyyy/MM/dd")));
            alHeaderData.Add(new ListItem("RPT_DTE_HHmm", DateTime.Now.ToString("HH:mm")));
            //設定表尾欄位資料
            ArrayList alTrailerData = new ArrayList();
            //設定小計資料表

            //產出報表(TXT)
            string rc = CMCRPT002.Output(inTable, alHeaderData, null, inSubtotTable, "CAD0001C", dtTODAY_PROCESS_DTE);
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
            logger.strMEMO = "信用卡代繳水費(省水)手續費清單分行報表";
            logger.intTBL_COUNT = PUBLIC_HIST_Count_Query;
            logger.writeCounter();

            logger.DisplayCounter(Cybersoft.Coca.Log.LevelEnum.Info);
        }
        #endregion


    }
}
