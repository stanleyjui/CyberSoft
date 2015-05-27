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
    /// PBBGSR001-信用卡代繳(陽明/欣高/竹名)瓦斯費用 成功/失敗報表 (CAD0001A/B/D.txt)
    /// </summary>
    public class PBBGSR001
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
        int PUBLIC_HIST_Count_Query_A = 0;
        int PUBLIC_HIST_Count_Query_B = 0;
        int PUBLIC_HIST_Count_Query_D = 0;

        DataTable inSubtotTable = new DataTable();
        DataTable PUBLISC_HIST_DATATABLE = new DataTable();
        DataTable PUBLISC_HIST_DATATABLE_I = new DataTable();

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
        #endregion
        string KEY = string.Empty;
        string POS_DTE = string.Empty;
        string strBANK_NBR = "";
        string strPAY_TYPE_A = "0008";
        string strPAY_TYPE_B = "0016";
        string strPAY_TYPE_D = "0035";

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
            logger.strJOBNAME = "PBBGSR001";
            logger.dtRunDate = DateTime.Now;
            #endregion
            try
            {
                #region 取得系統資訊(前次批次日、本日批次日)
                CMCSYS001 SYSINF = new CMCSYS001();
                SYSINF.strJobID = JobID;
                SYSINF.strJobName = "PBBGSR001";
                String SYSINF_RC = SYSINF.getSYSINF();
                dtPREV_PROCESS_DTE = SYSINF.datetimePREV_PROCESS_DTE;
                dtTODAY_PROCESS_DTE = SYSINF.datetimeTODAY_PROCESS_DTE;
                dtNEXT_PROCESS_DTE = SYSINF.datetimeNEXT_PROCESS_DTE;
                dtBANK_NEXT_PROCESS_DTE = SYSINF.datetimeBANK_NEXT_PROCESS_DTE;

                //for test vvvv
                //dtTODAY_PROCESS_DTE = DateTime.Parse("2012-07-12");
                //strPAY_TYPE_A = "0003";
                //strBANK_NBR = "";
                //for test ^^^^

                #endregion

                #region 定義小計subtotal

                #region ##SUBHED##

                inSubtotTable.Columns.Add("KEY", typeof(string));
                inSubtotTable.Columns["KEY"].DefaultValue = "";

                inSubtotTable.Columns.Add("POS_DTE_YYYYMMDD", typeof(string));
                inSubtotTable.Columns["POS_DTE_YYYYMMDD"].DefaultValue = dtTODAY_PROCESS_DTE.ToString("yyyy/MM/dd");

                inSubtotTable.Columns.Add("BANK_NBR", typeof(string));
                inSubtotTable.Columns["BANK_NBR"].DefaultValue = "";

                inSubtotTable.Columns.Add("PAY_TYPE", typeof(string));
                inSubtotTable.Columns["PAY_TYPE"].DefaultValue = "";

                #endregion

                #region ##SUBTOT##
                inSubtotTable.Columns.Add("SUB_CNT", typeof(string));
                inSubtotTable.Columns["SUB_CNT"].DefaultValue = "";

                inSubtotTable.Columns.Add("SUB_SUCC_CNT", typeof(string));
                inSubtotTable.Columns["SUB_SUCC_CNT"].DefaultValue = "";

                inSubtotTable.Columns.Add("SUB_FAIL_CNT", typeof(string));
                inSubtotTable.Columns["SUB_FAIL_CNT"].DefaultValue = "";

                inSubtotTable.Columns.Add("SUB_AMT", typeof(string));
                inSubtotTable.Columns["SUB_AMT"].DefaultValue = "";

                inSubtotTable.Columns.Add("SUB_SUCC_AMT", typeof(string));
                inSubtotTable.Columns["SUB_SUCC_AMT"].DefaultValue = "";

                inSubtotTable.Columns.Add("SUB_FAIL_AMT", typeof(string));
                inSubtotTable.Columns["SUB_FAIL_AMT"].DefaultValue = "";

                inSubtotTable.Columns.Add("SUB_FEE_AMT", typeof(string));
                inSubtotTable.Columns["SUB_FEE_AMT"].DefaultValue = "";

                inSubtotTable.Columns.Add("SUB_NET_AMT", typeof(string));
                inSubtotTable.Columns["SUB_NET_AMT"].DefaultValue = "";
                #endregion

                #endregion
                //================================================================================
                #region 陽明瓦斯(A)
                PUBLISC_HIST_DATATABLE = null;
                PUBLISC_HIST_DATATABLE_I = null;
                inSubtotTable.Rows.Add(inSubtotTable.NewRow());

                #region 信用卡代繳(陽明)瓦斯費用明細
                PUBLIC_HIST.table_define();
                PUBLIC_HIST.init();
                PUBLIC_HIST.strWherePAY_TYPE = strPAY_TYPE_A;
                PUBLIC_HIST.strWhereTRANS_DTE = dtTODAY_PROCESS_DTE.ToString("yyyyMMdd");

                PUBLIC_HIST_RC = PUBLIC_HIST.query_for_CAD0001ABD(); //明細

                switch (PUBLIC_HIST_RC)
                {
                    case "S0000": //查詢成功
                        PUBLIC_HIST_Count_Query_A = PUBLIC_HIST.resultTable.Rows.Count;
                        PUBLISC_HIST_DATATABLE = PUBLIC_HIST.resultTable; //明細
                        logger.strJobQueue = "PUBLIC_HIST.query_for_CAD0001ABD() 代繳碼:" + PUBLIC_HIST.strWherePAY_TYPE + " 筆數: " + PUBLIC_HIST_Count_Query_A.ToString("###,###,##0").PadLeft(11, ' ');
                        logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                        break;

                    case "F0023": //查無資料
                        logger.strJobQueue = PUBLIC_HIST_RC + " 本日無(代繳碼:" + PUBLIC_HIST.strWherePAY_TYPE + ")信用卡代繳(A:陽明/B:欣高/D:竹名)瓦斯費用 成功/失敗報表";
                        logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                        break;

                    default: //資料庫錯誤
                        logger.strJobQueue = "PUBLIC_HIST.query_for_CAD0001ABD()  (代繳碼:" + PUBLIC_HIST.strWherePAY_TYPE + ")錯誤 " + PUBLIC_HIST_RC;
                        logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                        return "B0016" + logger.strJobQueue;
                }
                #endregion

                #region 信用卡代繳(陽明)瓦斯費用成功/失敗報表

                PUBLIC_HIST_RC = PUBLIC_HIST.query_for_CAD0001ABD_I();//成功/失敗

                switch (PUBLIC_HIST_RC)
                {
                    case "S0000": //查詢成功
                        PUBLISC_HIST_DATATABLE_I = PUBLIC_HIST.resultTable;//成功/失敗
                        logger.strJobQueue = "PUBLIC_HIST.query_for_CAD0001ABD_I() 代繳碼:" + PUBLIC_HIST.strWherePAY_TYPE;
                        logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                        break;

                    case "F0023": //查無資料
                        logger.strJobQueue = PUBLIC_HIST_RC + " 本日無(代繳碼:" + PUBLIC_HIST.strWherePAY_TYPE + ")信用卡代繳(A:陽明/B:欣高/D:竹名)瓦斯費用 成功/失敗報表";
                        logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                        break;

                    default: //資料庫錯誤
                        logger.strJobQueue = "PUBLIC_HIST.query_for_CAD0001ABD_I()  (代繳碼:" + PUBLIC_HIST.strWherePAY_TYPE + ")錯誤 " + PUBLIC_HIST_RC;
                        logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                        return "B0016" + logger.strJobQueue;
                }
                #endregion

                #region MBS (陽明)自動扣繳帳號

                PUBLIC_HIST_RC = PUBLIC_HIST.query_for_MBS_BankNbr();//成功/失敗

                switch (PUBLIC_HIST_RC)
                {
                    case "S0000": //查詢成功
                        strBANK_NBR = PUBLIC_HIST.resultTable.Rows[0]["BANK_NBR"].ToString().Trim();
                        logger.strJobQueue = "PUBLIC_HIST.query_for_MBS_BankNbr() 代繳碼:" + PUBLIC_HIST.strWherePAY_TYPE;
                        logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                        break;

                    default: //資料庫錯誤
                        logger.strJobQueue = "PUBLIC_HIST.query_for_MBS_BankNbr()  (代繳碼:" + PUBLIC_HIST.strWherePAY_TYPE + ")錯誤 " + PUBLIC_HIST_RC;
                        logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                        return "B0016" + logger.strJobQueue;
                }
                #endregion

                #region 轉出內容明細
                if (PUBLISC_HIST_DATATABLE_I != null)
                {
                    //##SUBTOT##
                    inSubtotTable.Rows[0]["SUB_CNT"] = Convert.ToDecimal(PUBLISC_HIST_DATATABLE_I.Rows[0]["SUB_CNT"]).ToString("###,##0").PadLeft(7, ' ');
                    inSubtotTable.Rows[0]["SUB_SUCC_CNT"] = Convert.ToDecimal(PUBLISC_HIST_DATATABLE_I.Rows[0]["SUB_SUCC_CNT"]).ToString("###,##0").PadLeft(7, ' ');
                    inSubtotTable.Rows[0]["SUB_FAIL_CNT"] = Convert.ToDecimal(PUBLISC_HIST_DATATABLE_I.Rows[0]["SUB_FAIL_CNT"]).ToString("###,##0").PadLeft(7, ' ');
                    inSubtotTable.Rows[0]["SUB_AMT"] = Convert.ToDecimal(PUBLISC_HIST_DATATABLE_I.Rows[0]["SUB_AMT"]).ToString("###,###,##0").PadLeft(11, ' ');
                    inSubtotTable.Rows[0]["SUB_SUCC_AMT"] = Convert.ToDecimal(PUBLISC_HIST_DATATABLE_I.Rows[0]["SUB_SUCC_AMT"]).ToString("###,###,##0").PadLeft(11, ' ');
                    inSubtotTable.Rows[0]["SUB_FAIL_AMT"] = Convert.ToDecimal(PUBLISC_HIST_DATATABLE_I.Rows[0]["SUB_FAIL_AMT"]).ToString("###,###,##0").PadLeft(11, ' ');
                    inSubtotTable.Rows[0]["SUB_FEE_AMT"] = Convert.ToDecimal(PUBLISC_HIST_DATATABLE_I.Rows[0]["SUB_FEE_AMT"]).ToString("###,##0").PadLeft(7, ' ');
                    inSubtotTable.Rows[0]["SUB_NET_AMT"] = Convert.ToDecimal(PUBLISC_HIST_DATATABLE_I.Rows[0]["SUB_NET_AMT"]).ToString("###,###,##0").PadLeft(11, ' ');
                    //##SUBHED##
                    inSubtotTable.Rows[0]["KEY"] = Convert.ToString(PUBLISC_HIST_DATATABLE_I.Rows[0]["KEY"]).PadLeft(4, ' ');
                    inSubtotTable.Rows[0]["PAY_TYPE"] = Convert.ToString(PUBLISC_HIST_DATATABLE_I.Rows[0]["PAY_TYPE"]).PadLeft(4, ' ');
                    inSubtotTable.Rows[0]["BANK_NBR"] = strBANK_NBR.PadLeft(16, ' ');
                }       
                #endregion

                #region 信用卡代繳(陽明)瓦斯費用 成功/失敗報表
                if (PUBLIC_HIST_Count_Query_A > 0)
                {
                    writeReportCAD0001ABD(PUBLISC_HIST_DATATABLE, "CAD0001A");
                }

                #endregion

                #endregion

                #region 欣高瓦斯(B)
                PUBLISC_HIST_DATATABLE = null;
                PUBLISC_HIST_DATATABLE_I = null;

                #region 信用卡代繳(欣高)瓦斯費用明細
                PUBLIC_HIST.table_define();
                PUBLIC_HIST.init();
                PUBLIC_HIST.strWherePAY_TYPE = strPAY_TYPE_B;
                PUBLIC_HIST.strWhereTRANS_DTE = dtTODAY_PROCESS_DTE.ToString("yyyyMMdd");

                PUBLIC_HIST_RC = PUBLIC_HIST.query_for_CAD0001ABD(); //明細

                switch (PUBLIC_HIST_RC)
                {
                    case "S0000": //查詢成功
                        PUBLIC_HIST_Count_Query_B = PUBLIC_HIST.resultTable.Rows.Count;
                        PUBLISC_HIST_DATATABLE = PUBLIC_HIST.resultTable; //明細
                        logger.strJobQueue = "PUBLIC_HIST.query_for_CAD0001ABD() 代繳碼:" + PUBLIC_HIST.strWherePAY_TYPE + " 筆數: " + PUBLIC_HIST_Count_Query_B.ToString("###,###,##0").PadLeft(11, ' ');
                        logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                        break;

                    case "F0023": //查無資料
                        logger.strJobQueue = PUBLIC_HIST_RC + " 本日無(代繳碼:" + PUBLIC_HIST.strWherePAY_TYPE + ")信用卡代繳(A:陽明/B:欣高/D:竹名)瓦斯費用 成功/失敗報表";
                        logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                        break;

                    default: //資料庫錯誤
                        logger.strJobQueue = "PUBLIC_HIST.query_for_CAD0001ABD()  (代繳碼:" + PUBLIC_HIST.strWherePAY_TYPE + ")錯誤 " + PUBLIC_HIST_RC;
                        logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                        return "B0016" + logger.strJobQueue;
                }
                #endregion

                #region 信用卡代繳(欣高)瓦斯費用成功/失敗報表
                if (PUBLIC_HIST_Count_Query_B > 0)
                {
                    PUBLIC_HIST_RC = PUBLIC_HIST.query_for_CAD0001ABD_I();//成功/失敗

                    switch (PUBLIC_HIST_RC)
                    {
                        case "S0000": //查詢成功
                            PUBLISC_HIST_DATATABLE_I = PUBLIC_HIST.resultTable;//成功/失敗
                            logger.strJobQueue = "PUBLIC_HIST.query_for_CAD0001ABD_I() 代繳碼:" + PUBLIC_HIST.strWherePAY_TYPE;
                            logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                            break;

                        case "F0023": //查無資料
                            logger.strJobQueue = PUBLIC_HIST_RC + " 本日無(代繳碼:" + PUBLIC_HIST.strWherePAY_TYPE + ")信用卡代繳(A:陽明/B:欣高/D:竹名)瓦斯費用 成功/失敗報表";
                            logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                            break;

                        default: //資料庫錯誤
                            logger.strJobQueue = "PUBLIC_HIST.query_for_CAD0001ABD_I()  (代繳碼:" + PUBLIC_HIST.strWherePAY_TYPE + ")錯誤 " + PUBLIC_HIST_RC;
                            logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                            return "B0016" + logger.strJobQueue;
                    }
                }
                #endregion

                #region MBS (欣高)自動扣繳帳號
                if (PUBLIC_HIST_Count_Query_B > 0)
                {
                    PUBLIC_HIST_RC = PUBLIC_HIST.query_for_MBS_BankNbr();//成功/失敗

                    switch (PUBLIC_HIST_RC)
                    {
                        case "S0000": //查詢成功
                            strBANK_NBR = PUBLIC_HIST.resultTable.Rows[0]["BANK_NBR"].ToString().Trim();
                            logger.strJobQueue = "PUBLIC_HIST.query_for_MBS_BankNbr() 代繳碼:" + PUBLIC_HIST.strWherePAY_TYPE;
                            logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                            break;

                        default: //資料庫錯誤
                            logger.strJobQueue = "PUBLIC_HIST.query_for_MBS_BankNbr()  (代繳碼:" + PUBLIC_HIST.strWherePAY_TYPE + ")錯誤 " + PUBLIC_HIST_RC;
                            logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                            return "B0016" + logger.strJobQueue;
                    }
                }
                #endregion

                #region 轉出內容明細
                if (PUBLISC_HIST_DATATABLE_I != null && PUBLIC_HIST_Count_Query_B > 0)
                {
                    //##SUBTOT##
                    inSubtotTable.Rows[0]["SUB_CNT"] = Convert.ToDecimal(PUBLISC_HIST_DATATABLE_I.Rows[0]["SUB_CNT"]).ToString("###,##0").PadLeft(7, ' ');
                    inSubtotTable.Rows[0]["SUB_SUCC_CNT"] = Convert.ToDecimal(PUBLISC_HIST_DATATABLE_I.Rows[0]["SUB_SUCC_CNT"]).ToString("###,##0").PadLeft(7, ' ');
                    inSubtotTable.Rows[0]["SUB_FAIL_CNT"] = Convert.ToDecimal(PUBLISC_HIST_DATATABLE_I.Rows[0]["SUB_FAIL_CNT"]).ToString("###,##0").PadLeft(7, ' ');
                    inSubtotTable.Rows[0]["SUB_AMT"] = Convert.ToDecimal(PUBLISC_HIST_DATATABLE_I.Rows[0]["SUB_AMT"]).ToString("###,###,##0").PadLeft(11, ' ');
                    inSubtotTable.Rows[0]["SUB_SUCC_AMT"] = Convert.ToDecimal(PUBLISC_HIST_DATATABLE_I.Rows[0]["SUB_SUCC_AMT"]).ToString("###,###,##0").PadLeft(11, ' ');
                    inSubtotTable.Rows[0]["SUB_FAIL_AMT"] = Convert.ToDecimal(PUBLISC_HIST_DATATABLE_I.Rows[0]["SUB_FAIL_AMT"]).ToString("###,###,##0").PadLeft(11, ' ');
                    inSubtotTable.Rows[0]["SUB_FEE_AMT"] = Convert.ToDecimal(PUBLISC_HIST_DATATABLE_I.Rows[0]["SUB_FEE_AMT"]).ToString("###,##0").PadLeft(7, ' ');
                    inSubtotTable.Rows[0]["SUB_NET_AMT"] = Convert.ToDecimal(PUBLISC_HIST_DATATABLE_I.Rows[0]["SUB_NET_AMT"]).ToString("###,###,##0").PadLeft(11, ' ');
                    //##SUBHED##
                    inSubtotTable.Rows[0]["KEY"] = Convert.ToString(PUBLISC_HIST_DATATABLE_I.Rows[0]["KEY"]).PadLeft(4, ' ');
                    inSubtotTable.Rows[0]["PAY_TYPE"] = Convert.ToString(PUBLISC_HIST_DATATABLE_I.Rows[0]["PAY_TYPE"]).PadLeft(4, ' ');
                    inSubtotTable.Rows[0]["BANK_NBR"] = strBANK_NBR.PadLeft(16, ' ');
                }
                else
                {
                    //##SUBTOT##
                    inSubtotTable.Rows[0]["SUB_CNT"] = 0.ToString("###,##0").PadLeft(7, ' ');
                    inSubtotTable.Rows[0]["SUB_SUCC_CNT"] = 0.ToString("###,##0").PadLeft(7, ' ');
                    inSubtotTable.Rows[0]["SUB_FAIL_CNT"] = 0.ToString("###,##0").PadLeft(7, ' ');
                    inSubtotTable.Rows[0]["SUB_AMT"] = 0.ToString("###,###,##0").PadLeft(11, ' ');
                    inSubtotTable.Rows[0]["SUB_SUCC_AMT"] = 0.ToString("###,###,##0").PadLeft(11, ' ');
                    inSubtotTable.Rows[0]["SUB_FAIL_AMT"] = 0.ToString("###,###,##0").PadLeft(11, ' ');
                    inSubtotTable.Rows[0]["SUB_FEE_AMT"] = 0.ToString("###,##0").PadLeft(7, ' ');
                    inSubtotTable.Rows[0]["SUB_NET_AMT"] = 0.ToString("###,###,##0").PadLeft(11, ' ');
                    //##SUBHED##
                    inSubtotTable.Rows[0]["KEY"] = "".PadLeft(4, ' ');
                    inSubtotTable.Rows[0]["PAY_TYPE"] = "".PadLeft(4, ' ');
                    inSubtotTable.Rows[0]["BANK_NBR"] = strBANK_NBR.PadLeft(16, ' ');

                }
                #endregion

                #region 信用卡代繳(欣高)瓦斯費用 成功/失敗報表
                if (PUBLIC_HIST_Count_Query_B > 0)
                {
                    writeReportCAD0001ABD(PUBLISC_HIST_DATATABLE, "CAD0001B");
                }

                #endregion

                #endregion

                #region 竹名瓦斯(D)
                PUBLISC_HIST_DATATABLE = null;
                PUBLISC_HIST_DATATABLE_I = null;

                #region 信用卡代繳(竹名)瓦斯費用明細
                PUBLIC_HIST.table_define();
                PUBLIC_HIST.init();
                PUBLIC_HIST.strWherePAY_TYPE = strPAY_TYPE_D;
                PUBLIC_HIST.strWhereTRANS_DTE = dtTODAY_PROCESS_DTE.ToString("yyyyMMdd");

                PUBLIC_HIST_RC = PUBLIC_HIST.query_for_CAD0001ABD(); //明細

                switch (PUBLIC_HIST_RC)
                {
                    case "S0000": //查詢成功
                        PUBLIC_HIST_Count_Query_D = PUBLIC_HIST.resultTable.Rows.Count;
                        PUBLISC_HIST_DATATABLE = PUBLIC_HIST.resultTable; //明細
                        logger.strJobQueue = "PUBLIC_HIST.query_for_CAD0001ABD() 代繳碼:" + PUBLIC_HIST.strWherePAY_TYPE + " 筆數: " + PUBLIC_HIST_Count_Query_D.ToString("###,###,##0").PadLeft(11, ' ');
                        logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                        break;

                    case "F0023": //查無資料
                        logger.strJobQueue = PUBLIC_HIST_RC + " 本日無(代繳碼:" + PUBLIC_HIST.strWherePAY_TYPE + ")信用卡代繳(A:陽明/B:竹名/D:竹名)瓦斯費用 成功/失敗報表";
                        logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                        break;

                    default: //資料庫錯誤
                        logger.strJobQueue = "PUBLIC_HIST.query_for_CAD0001ABD()  (代繳碼:" + PUBLIC_HIST.strWherePAY_TYPE + ")錯誤 " + PUBLIC_HIST_RC;
                        logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                        return "B0016" + logger.strJobQueue;
                }
                #endregion

                #region 信用卡代繳(竹名)瓦斯費用成功/失敗報表
                if (PUBLIC_HIST_Count_Query_D > 0)
                {
                    PUBLIC_HIST_RC = PUBLIC_HIST.query_for_CAD0001ABD_I();//成功/失敗

                    switch (PUBLIC_HIST_RC)
                    {
                        case "S0000": //查詢成功
                            PUBLISC_HIST_DATATABLE_I = PUBLIC_HIST.resultTable;//成功/失敗
                            logger.strJobQueue = "PUBLIC_HIST.query_for_CAD0001ABD_I() 代繳碼:" + PUBLIC_HIST.strWherePAY_TYPE;
                            logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                            break;

                        case "F0023": //查無資料
                            logger.strJobQueue = PUBLIC_HIST_RC + " 本日無(代繳碼:" + PUBLIC_HIST.strWherePAY_TYPE + ")信用卡代繳(A:陽明/B:竹名/D:竹名)瓦斯費用 成功/失敗報表";
                            logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                            break;

                        default: //資料庫錯誤
                            logger.strJobQueue = "PUBLIC_HIST.query_for_CAD0001ABD_I()  (代繳碼:" + PUBLIC_HIST.strWherePAY_TYPE + ")錯誤 " + PUBLIC_HIST_RC;
                            logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                            return "B0016" + logger.strJobQueue;
                    }
                }
                #endregion

                #region MBS (竹名)自動扣繳帳號
                if (PUBLIC_HIST_Count_Query_D > 0)
                {
                    PUBLIC_HIST_RC = PUBLIC_HIST.query_for_MBS_BankNbr();//成功/失敗

                    switch (PUBLIC_HIST_RC)
                    {
                        case "S0000": //查詢成功
                            strBANK_NBR = PUBLIC_HIST.resultTable.Rows[0]["BANK_NBR"].ToString().Trim();
                            logger.strJobQueue = "PUBLIC_HIST.query_for_MBS_BankNbr() 代繳碼:" + PUBLIC_HIST.strWherePAY_TYPE;
                            logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                            break;

                        default: //資料庫錯誤
                            logger.strJobQueue = "PUBLIC_HIST.query_for_MBS_BankNbr()  (代繳碼:" + PUBLIC_HIST.strWherePAY_TYPE + ")錯誤 " + PUBLIC_HIST_RC;
                            logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                            return "B0016" + logger.strJobQueue;
                    }
                }
                #endregion

                #region 轉出內容明細
                if (PUBLISC_HIST_DATATABLE_I != null && PUBLIC_HIST_Count_Query_D > 0)
                {
                    //##SUBTOT##
                    inSubtotTable.Rows[0]["SUB_CNT"] = Convert.ToDecimal(PUBLISC_HIST_DATATABLE_I.Rows[0]["SUB_CNT"]).ToString("###,##0").PadLeft(7, ' ');
                    inSubtotTable.Rows[0]["SUB_SUCC_CNT"] = Convert.ToDecimal(PUBLISC_HIST_DATATABLE_I.Rows[0]["SUB_SUCC_CNT"]).ToString("###,##0").PadLeft(7, ' ');
                    inSubtotTable.Rows[0]["SUB_FAIL_CNT"] = Convert.ToDecimal(PUBLISC_HIST_DATATABLE_I.Rows[0]["SUB_FAIL_CNT"]).ToString("###,##0").PadLeft(7, ' ');
                    inSubtotTable.Rows[0]["SUB_AMT"] = Convert.ToDecimal(PUBLISC_HIST_DATATABLE_I.Rows[0]["SUB_AMT"]).ToString("###,###,##0").PadLeft(11, ' ');
                    inSubtotTable.Rows[0]["SUB_SUCC_AMT"] = Convert.ToDecimal(PUBLISC_HIST_DATATABLE_I.Rows[0]["SUB_SUCC_AMT"]).ToString("###,###,##0").PadLeft(11, ' ');
                    inSubtotTable.Rows[0]["SUB_FAIL_AMT"] = Convert.ToDecimal(PUBLISC_HIST_DATATABLE_I.Rows[0]["SUB_FAIL_AMT"]).ToString("###,###,##0").PadLeft(11, ' ');
                    inSubtotTable.Rows[0]["SUB_FEE_AMT"] = Convert.ToDecimal(PUBLISC_HIST_DATATABLE_I.Rows[0]["SUB_FEE_AMT"]).ToString("###,##0").PadLeft(7, ' ');
                    inSubtotTable.Rows[0]["SUB_NET_AMT"] = Convert.ToDecimal(PUBLISC_HIST_DATATABLE_I.Rows[0]["SUB_NET_AMT"]).ToString("###,###,##0").PadLeft(11, ' ');
                    //##SUBHED##
                    inSubtotTable.Rows[0]["KEY"] = Convert.ToString(PUBLISC_HIST_DATATABLE_I.Rows[0]["KEY"]).PadLeft(4, ' ');
                    inSubtotTable.Rows[0]["PAY_TYPE"] = Convert.ToString(PUBLISC_HIST_DATATABLE_I.Rows[0]["PAY_TYPE"]).PadLeft(4, ' ');
                    inSubtotTable.Rows[0]["BANK_NBR"] = strBANK_NBR.PadLeft(16, ' ');
                }
                else
                {
                    //##SUBTOT##
                    inSubtotTable.Rows[0]["SUB_CNT"] = 0.ToString("###,##0").PadLeft(7, ' ');
                    inSubtotTable.Rows[0]["SUB_SUCC_CNT"] = 0.ToString("###,##0").PadLeft(7, ' ');
                    inSubtotTable.Rows[0]["SUB_FAIL_CNT"] = 0.ToString("###,##0").PadLeft(7, ' ');
                    inSubtotTable.Rows[0]["SUB_AMT"] = 0.ToString("###,###,##0").PadLeft(11, ' ');
                    inSubtotTable.Rows[0]["SUB_SUCC_AMT"] = 0.ToString("###,###,##0").PadLeft(11, ' ');
                    inSubtotTable.Rows[0]["SUB_FAIL_AMT"] = 0.ToString("###,###,##0").PadLeft(11, ' ');
                    inSubtotTable.Rows[0]["SUB_FEE_AMT"] = 0.ToString("###,##0").PadLeft(7, ' ');
                    inSubtotTable.Rows[0]["SUB_NET_AMT"] = 0.ToString("###,###,##0").PadLeft(11, ' ');
                    //##SUBHED##
                    inSubtotTable.Rows[0]["KEY"] = "".PadLeft(4, ' ');
                    inSubtotTable.Rows[0]["PAY_TYPE"] = "".PadLeft(4, ' ');
                    inSubtotTable.Rows[0]["BANK_NBR"] = strBANK_NBR.PadLeft(16, ' ');

                }
                #endregion

                #region 信用卡代繳(竹名)瓦斯費用 成功/失敗報表
                if (PUBLIC_HIST_Count_Query_D > 0)
                {
                    writeReportCAD0001ABD(PUBLISC_HIST_DATATABLE, "CAD0001D");
                }

                #endregion

                #endregion

                //================================================================================ 
                writeDisplay();
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
        #region 寫出 信用卡代繳(陽明/欣高/竹名)瓦斯費用 成功/失敗報表
        void writeReportCAD0001ABD(DataTable inTable, string fileName)
        {
            CMCRPT002 CMCRPT002 = new CMCRPT002();
            CMCRPT002.TemplateFileName = fileName;  //範本檔名
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
            string rc = CMCRPT002.Output(inTable, alHeaderData, null, inSubtotTable, fileName, dtTODAY_PROCESS_DTE);
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
            logger.strMEMO = "陽明瓦斯";
            logger.intTBL_COUNT = PUBLIC_HIST_Count_Query_A;
            logger.writeCounter();

            logger.strTBL_NAME = "PUBLIC_HIST";
            logger.strTBL_ACCESS = "Q";
            logger.strMEMO = "欣高瓦斯";
            logger.intTBL_COUNT = PUBLIC_HIST_Count_Query_B;
            logger.writeCounter();

            logger.strTBL_NAME = "PUBLIC_HIST";
            logger.strTBL_ACCESS = "Q";
            logger.strMEMO = "竹名瓦斯";
            logger.intTBL_COUNT = PUBLIC_HIST_Count_Query_D;
            logger.writeCounter();

            logger.DisplayCounter(Cybersoft.Coca.Log.LevelEnum.Info);
        }
        #endregion


    }
}
