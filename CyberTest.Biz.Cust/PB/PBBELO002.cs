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
using Microsoft.VisualBasic;

namespace Cybersoft.Biz.Cust
{
    /// <summary>
    /// 產生台電(高壓電)扣帳回應檔,並將入帳交易先寫至TX_WAREHOUSE,待下一營業日入帳
    /// </summary>
    public class PBBELO002
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

        //宣告PUBLIC_LIST
        String PUBLIC_LIST_RC = "";
        PUBLIC_LISTDao PUBLIC_LIST = new PUBLIC_LISTDao();

        //宣告 TX_WAREHOUSE
        String TX_WAREHOUSE_RC = "";
        TX_WAREHOUSEDao TX_WAREHOUSE = new TX_WAREHOUSEDao();

        //宣告 TX_WAREHOUSE_T
        TX_WAREHOUSEDao TX_WAREHOUSE_T = new TX_WAREHOUSEDao();

        //暫存報表TABLE
        DataTable REPORT_TABLE = new DataTable();

        #endregion

        #region 設定檔案編碼
        Encoding BIG5 = Encoding.GetEncoding("big5");
        #endregion

        #region 副程式
        CMCNBR001 CMCNBR001 = new CMCNBR001();
        #endregion

        #region 宣告共用變數

        //批次日期
        DateTime PREV_PROCESS_DTE = new DateTime();
        DateTime TODAY_PROCESS_DTE = new DateTime();
        DateTime NEXT_PROCESS_DTE = new DateTime();
        string strToday = "";
        //筆數&金額
        int PUBLIC_LIST_Query_Count = 0;
        int PUBLIC_HIST_Update_Count = 0;
        int i = 0;
        int x = 0; //控制TX_WAREHOUSE_T
        int k = 0; //REPORT_TABLE
        int intREC_HCount = 0;
        int intRECCount = 0;
        int intREC_TCount = 0;
        int ELECT_TOTAL_CNT = 0;
        Decimal ELECT_TOTAL_AMT = 0;
        int TX_WAREHOUSE_T_Count_Insert = 0;
        int ELECT_TXT_Count = 0;
        string ELECT_PAY_DTE = "20120120,20120220,20120320,20120420," +
                               "20120518,20120620,20120720,20120820," +
                               "20120920,20121019,20121120,20121220";

        //PUBLIC_LIST欄位
        String PAY_DATA_AREA = "";
        String BILL_DTE = "";
        Decimal TX_AMT = 0;
        String NAME = "";

        //PUBLIC_HIST欄位
        String BU = "";
        String ACCT_NBR = "";
        String PRODUCT = "";
        String CARD_PRODUCT = "";
        String CURRENCY = "";
        String CUST_SEQ = "";
        String PAY_CARD_NBR = "";
        String PAY_NBR = "";
        String PAY_SEQ = "";
        DateTime PAY_DTE = new DateTime();
        String PAY_RESULT = "";
        String AUTH_CODE = "";
        String CTL_CODE = "";
        Decimal TOTAL_AMT = 0;
        Decimal TOTAL_CNT = 0;

        //台電(高壓電)回應檔欄位
        String[] ELECT_STMTOUT = null;
        String PAY_RESULT_ELECT = "";
        Decimal ELECT_SUCCESS_AMT = 0;
        Decimal ELECT_FAIL_AMT = 0;
        int ELECT_SUCCESS_CNT = 0;
        int ELECT_FAIL_CNT = 0;
        String TEMP_SUCCESS_AMT = "";
        String TEMP_FAIL_AMT = "";

        //台電遮罩欄位
        //String MASK_TYPE = "00000000******000000";  //第9~14碼躲起來

        //檔案長度
        const int intDataLength = 85;

        #endregion

        #region 宣告檔案路徑

        //XML放置路徑 
        String strXML_Layout = "";
        //寫出檔案名稱
        String strOutFileName = "";
        //寫出檔案路徑
        String FILE_PATH = "";

        #endregion

        static readonly Cybersoft.Coca.Log.Logger logger = Cybersoft.Log.LogManager<Cybersoft.Coca.Log.Logger>.GetLogger(System.Reflection.MethodInfo.GetCurrentMethod().ReflectedType.Name);
        public string RUN()
        {
            #region 準備MSGLG.SYSOUT CLASS
            logger.strJobID = JobID;
            logger.strJOBNAME = "PBBELO002";
            logger.dtRunDate = DateTime.Now;
            #endregion

            try
            {
                #region 取得系統資訊(前次批次日、本日批次日)
                CMCSYS001 SYSINF = new CMCSYS001();
                SYSINF.strJobID = JobID;
                SYSINF.strJobName = "PBBELO002";
                String SYSINF_RC = SYSINF.getSYSINF();
                PREV_PROCESS_DTE = SYSINF.datetimePREV_PROCESS_DTE;
                TODAY_PROCESS_DTE = SYSINF.datetimeTODAY_PROCESS_DTE;
                NEXT_PROCESS_DTE = SYSINF.datetimeBANK_NEXT_PROCESS_DTE;
                strToday = TODAY_PROCESS_DTE.ToString("yyyyMMdd");
                //為台電約定扣款日才執行
                if (!ELECT_PAY_DTE.Contains(TODAY_PROCESS_DTE.ToString("yyyyMMdd")))
                {
                    logger.strJobQueue = "今天 (" + TODAY_PROCESS_DTE.ToString("yyyy/MM/dd") + ") 非台電(高壓電)扣款日, 故不執行";
                    logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                    return "B0000:" + logger.strJobQueue;
                }
                #endregion

                #region 宣告檔案路徑
                Cybersoft.Base.CMCURL001 CMCURL = new CMCURL001();
                //XML名稱
                strXML_Layout = CMCURL.getURL("PUBLIC_ELECT.xml");
                //寫出檔案路徑
                FILE_PATH = CMCURL.getPATH("ELECT_STMTOUT");

                if (strXML_Layout == "" || FILE_PATH == "")
                {
                    logger.strJobQueue = "路徑取得錯誤!!! PUBLIC_ELECT.xml路徑為 " + strXML_Layout + " ELECT_STMTOUT路徑為 " + FILE_PATH;
                    logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                    return "B0099:" + logger.strJobQueue;
                }
                else
                {
                    FILE_PATH = FILE_PATH.Replace("yyyymmdd", TODAY_PROCESS_DTE.ToString("yyyyMMdd"));
                    logger.strJobQueue = "PUBLIC_ELECT.xml路徑為 " + strXML_Layout + " ELECT_STMTOUT路徑為 " + FILE_PATH;
                    logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                }
                #endregion

                #region 複製TX_WAREHOUSE_T的Table定義
                TX_WAREHOUSE_T.table_define();
                #endregion

                #region 定義報表Table
                REPORT_TABLE.Columns.Add("RPT_SEQ", typeof(string));
                REPORT_TABLE.Columns.Add("PAY_NBR", typeof(string));
                REPORT_TABLE.Columns.Add("PAY_YYMM", typeof(string));
                REPORT_TABLE.Columns.Add("PAY_CARD_NBR", typeof(string));
                REPORT_TABLE.Columns.Add("NAME", typeof(string));
                REPORT_TABLE.Columns.Add("PAY_AMT", typeof(decimal));
                REPORT_TABLE.Columns.Add("PAY_DTE", typeof(string));
                REPORT_TABLE.Columns.Add("PAY_RESULT", typeof(string));
                #endregion

                #region 擷取今日需送回應檔之台電(高壓電)資料
                PUBLIC_LIST.init();
                PUBLIC_LIST.strWherePAY_TYPE = "0004";
                PUBLIC_LIST_RC = PUBLIC_LIST.query_for_out(strToday, "");
                switch (PUBLIC_LIST_RC)
                {
                    case "S0000": //查詢成功
                        PUBLIC_LIST_Query_Count = PUBLIC_LIST.resultTable.Rows.Count;
                        ELECT_STMTOUT = new string[PUBLIC_LIST_Query_Count];
                        break;

                    case "F0023": //無需處理資料
                        logger.strJobQueue = "今日無台電(高壓電)回應檔需產生;今日需送回應檔時間為大於 " + PREV_PROCESS_DTE + "且小於等於" + TODAY_PROCESS_DTE;
                        logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                        return "B0000";

                    default: //資料庫錯誤
                        logger.strJobQueue = "查詢PUBLIC_LIST 資料錯誤:" + PUBLIC_LIST_RC;
                        logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                        return "B0016:" + logger.strJobQueue;
                }
                #endregion

                #region 載入XML
                FileParseByXml xml = new FileParseByXml();

                //REC_H Layout
                DataTable REC_H_DataTable = xml.Xml2DataTable(strXML_Layout, "REC_H");

                if (xml.strMSG.Length > 0)
                {
                    logger.strJobQueue = "[Xml2DataTable(REC_H)] - " + xml.strMSG.ToString().Trim();
                    logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                    return "B0099:" + logger.strJobQueue;
                }

                //REC Layout
                DataTable REC_DataTable = xml.Xml2DataTable(strXML_Layout, "REC");

                if (xml.strMSG.Length > 0)
                {
                    logger.strJobQueue = "[Xml2DataTable(REC)] - " + xml.strMSG.ToString().Trim();
                    logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                    return "B0099:" + logger.strJobQueue;
                }

                //REC_T Layout
                DataTable REC_T_DataTable = xml.Xml2DataTable(strXML_Layout, "REC_T");

                if (xml.strMSG.Length > 0)
                {
                    logger.strJobQueue = "[Xml2DataTable(REC_T)] - " + xml.strMSG.ToString().Trim();
                    logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                    return "B0099:" + logger.strJobQueue;
                }
                #endregion

                for (i = 0; i < PUBLIC_LIST.resultTable.Rows.Count; i++)
                {

                    PAY_DATA_AREA = Convert.ToString(PUBLIC_LIST.resultTable.Rows[i]["PAY_DATA_AREA"]);
                    DataTable InfData_DataTable = new DataTable();
                    string strTag = PAY_DATA_AREA.Substring(0, 1).ToString().Trim();
                    switch (strTag)
                    {
                        case "1":  //檔頭資料       
                            intREC_HCount++;

                            #region  組檔頭
                            ELECT_STMTOUT[i] = PAY_DATA_AREA.Substring(0, 120);
                            #endregion

                            #region 刪除TX_WAREHOUSE,RERUN 機制
                            TX_WAREHOUSE.strWhereSOURCE_CODE = "PU00000004";
                            TX_WAREHOUSE.DateTimeWhereMNT_DT = TODAY_PROCESS_DTE;
                            TX_WAREHOUSE.strWhereMNT_USER = "PBBELO002";
                            TX_WAREHOUSE_RC = TX_WAREHOUSE.delete();
                            logger.strJobQueue = "刪除TX_WAREHOUSE 筆數 " + TX_WAREHOUSE.intDelCnt;
                            logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                            #endregion

                            break;

                        case "2":  //明細資料
                            intRECCount++;

                            #region 讀取PUBLIC_HIST找出扣款結果及TX_WAREHOUSE所需欄位
                            PUBLIC_HIST.init();
                            PUBLIC_HIST.strWherePAY_SEQ = Convert.ToString(PUBLIC_LIST.resultTable.Rows[i]["PAY_SEQ"]);
                            PUBLIC_HIST_RC = PUBLIC_HIST.query_for_REPORT();
                            switch (PUBLIC_HIST_RC)
                            {
                                case "S0000": //查詢成功
                                    BU = Convert.ToString(PUBLIC_HIST.resultTable.Rows[0]["BU"]);
                                    ACCT_NBR = Convert.ToString(PUBLIC_HIST.resultTable.Rows[0]["ACCT_NBR"]);
                                    PRODUCT = Convert.ToString(PUBLIC_HIST.resultTable.Rows[0]["PRODUCT"]);
                                    CARD_PRODUCT = Convert.ToString(PUBLIC_HIST.resultTable.Rows[0]["CARD_PRODUCT"]);
                                    CURRENCY = Convert.ToString(PUBLIC_HIST.resultTable.Rows[0]["CURRENCY"]);
                                    CUST_SEQ = Convert.ToString(PUBLIC_HIST.resultTable.Rows[0]["CUST_SEQ"]);
                                    PAY_CARD_NBR = Convert.ToString(PUBLIC_HIST.resultTable.Rows[0]["PAY_CARD_NBR"]);
                                    PAY_NBR = Convert.ToString(PUBLIC_HIST.resultTable.Rows[0]["PAY_NBR"]);
                                    PAY_SEQ = Convert.ToString(PUBLIC_HIST.resultTable.Rows[0]["PAY_SEQ"]);
                                    PAY_DTE = Convert.ToDateTime(PUBLIC_HIST.resultTable.Rows[0]["PAY_DTE"]);
                                    PAY_RESULT = Convert.ToString(PUBLIC_HIST.resultTable.Rows[0]["PAY_RESULT"]);
                                    AUTH_CODE = Convert.ToString(PUBLIC_HIST.resultTable.Rows[0]["AUTH_CODE"]);
                                    TX_AMT = Convert.ToDecimal(PUBLIC_HIST.resultTable.Rows[0]["PAY_AMT"]);
                                    NAME = Convert.ToString(PUBLIC_HIST.resultTable.Rows[0]["NAME"]);
                                    CTL_CODE = Convert.ToString(PUBLIC_HIST.resultTable.Rows[0]["USER_FIELD_2"]);
                                    break;

                                case "F0023": //查無該筆資料
                                    logger.strJobQueue = "查無該筆資料PAY_SEQ = " + PUBLIC_HIST.strWherePAY_SEQ;
                                    logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                                    return "F0023:" + logger.strJobQueue;

                                default: //資料庫錯誤
                                    logger.strJobQueue = "查詢PUBLIC_HIST 資料錯誤:" + PUBLIC_HIST_RC;
                                    logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                                    return "B0016:" + logger.strJobQueue;
                            }

                            #endregion

                            #region 依 Layout 拆解資料
                            InfData_DataTable = xml.FileLine2DataTable(BIG5, PAY_DATA_AREA, REC_DataTable);
                            if (xml.strMSG.Length > 0)
                            {
                                logger.strJobQueue = "[FileLine2DataTable(REC)] (L" + i + 1 + ") - " + xml.strMSG.ToString().Trim();
                                logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                                return "B0099:" + logger.strJobQueue;
                            }
                            BILL_DTE = Convert.ToString(InfData_DataTable.Rows[0]["BILL_DTE"]);

                            #endregion

                            #region 組明細及台電(高壓電)回應碼
                            //回應中心代碼說明 2:內部檢核有誤 8:授權失敗 9:處理成功

                            //O000~O009都表示成功
                            //if (PAY_RESULT == "S000" || PAY_RESULT == "O000" || PAY_RESULT == "O001" || PAY_RESULT == "O002" || PAY_RESULT == "O003" || PAY_RESULT == "O004" || PAY_RESULT == "O005" || PAY_RESULT == "O006" || PAY_RESULT == "O007" || PAY_RESULT == "O008" || PAY_RESULT == "O009")
                            //20110915調整回應碼
                            if (PAY_RESULT == "S000")
                            {
                                PAY_RESULT_ELECT = "00";  //扣帳成功
                                ELECT_SUCCESS_CNT = ELECT_SUCCESS_CNT + 1;
                                ELECT_SUCCESS_AMT = ELECT_SUCCESS_AMT + TX_AMT;
                                ELECT_STMTOUT[i] = PAY_DATA_AREA.Substring(0, 63) + PAY_RESULT_ELECT + PAY_DATA_AREA.Substring(65, 55);

                                #region 組TX_WAREHOUSE
                                if (TX_AMT > 0)
                                {
                                    TX_WAREHOUSE_T.initInsert_row();
                                    x = TX_WAREHOUSE_T.resultTable.Rows.Count - 1;
                                    TX_WAREHOUSE_T.resultTable.Rows[x]["BU"] = BU;
                                    TX_WAREHOUSE_T.resultTable.Rows[x]["ACCT_NBR"] = CUST_SEQ;
                                    TX_WAREHOUSE_T.resultTable.Rows[x]["CARD_PRODUCT"] = CARD_PRODUCT;
                                    TX_WAREHOUSE_T.resultTable.Rows[x]["PRODUCT"] = PRODUCT;
                                    TX_WAREHOUSE_T.resultTable.Rows[x]["CURRENCY"] = CURRENCY;
                                    TX_WAREHOUSE_T.resultTable.Rows[x]["CARD_NBR"] = PAY_CARD_NBR;
                                    TX_WAREHOUSE_T.resultTable.Rows[x]["EFF_DTE"] = NEXT_PROCESS_DTE;
                                    TX_WAREHOUSE_T.resultTable.Rows[x]["POSTING_DTE"] = PAY_DTE;
                                    TX_WAREHOUSE_T.resultTable.Rows[x]["ARN"] = PAY_NBR;
                                    TX_WAREHOUSE_T.resultTable.Rows[x]["CODE"] = "0001";
                                    TX_WAREHOUSE_T.resultTable.Rows[x]["SOURCE_CODE"] = "PU00000004";
                                    TX_WAREHOUSE_T.resultTable.Rows[x]["MCC_CODE"] = "4900";
                                    TX_WAREHOUSE_T.resultTable.Rows[x]["AUTH_CODE"] = AUTH_CODE;
                                    TX_WAREHOUSE_T.resultTable.Rows[x]["SEQ"] = x.ToString().PadLeft(10, '0');
                                    //偷偷把電號遮住
                                    //ELECT_MASK_STRING = CMCNBR001.GET_MASK(PAY_NBR, MASK_TYPE);
                                    TX_WAREHOUSE_T.resultTable.Rows[x]["DESCR"] = Strings.StrConv("台灣電力公司 " + PAY_NBR, VbStrConv.Wide);
                                    TX_WAREHOUSE_T.resultTable.Rows[x]["MT_TYPE"] = "D";
                                    TX_WAREHOUSE_T.resultTable.Rows[x]["AMT"] = TX_AMT;
                                    TX_WAREHOUSE_T.resultTable.Rows[x]["MNT_DT"] = TODAY_PROCESS_DTE;
                                    TX_WAREHOUSE_T.resultTable.Rows[x]["MNT_USER"] = "PBBELO002";
                                }
                                #endregion

                                //寫出成功/失敗明細報表
                                insertReport_TABLE();

                                #region 異動結果代碼(S000->0000)
                                PUBLIC_HIST.init();
                                PUBLIC_HIST.strWhereBU = BU;
                                PUBLIC_HIST.strWhereACCT_NBR = ACCT_NBR;
                                PUBLIC_HIST.strWherePAY_NBR = PAY_NBR;
                                PUBLIC_HIST.strWherePAY_SEQ = PAY_SEQ;
                                PUBLIC_HIST.strWhereAUTH_CODE = AUTH_CODE;
                                PUBLIC_HIST.strWherePAY_RESULT = "S000";
                                PUBLIC_HIST.strPAY_RESULT = "0000";
                                PUBLIC_HIST.strMNT_USER = "PBBELO002";
                                PUBLIC_HIST_RC = PUBLIC_HIST.update();
                                switch (PUBLIC_HIST_RC)
                                {
                                    case "S0000": //查詢成功
                                        PUBLIC_HIST_Update_Count = PUBLIC_HIST_Update_Count + PUBLIC_HIST.intUptCnt;
                                        break;

                                    default: //資料庫錯誤
                                        logger.strJobQueue = "異動 PUBLIC_HIST.PAY_RESULT(" + PAY_SEQ + ") 資料錯誤:" + PUBLIC_HIST_RC;
                                        logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                                        throw new System.Exception("B0016：" + logger.strJobQueue);
                                }
                                #endregion
                                break;

                            }
                            //I001: 未申請
                            if (PAY_RESULT == "I001")
                            {
                                PAY_RESULT_ELECT = "02";   //未申請代扣
                                ELECT_FAIL_CNT = ELECT_FAIL_CNT + 1;
                                ELECT_FAIL_AMT = ELECT_FAIL_AMT + TX_AMT;
                                ELECT_STMTOUT[i] = PAY_DATA_AREA.Substring(0, 63) + PAY_RESULT_ELECT + PAY_DATA_AREA.Substring(65, 55);
                                //寫出成功/失敗明細報表
                                insertReport_TABLE();
                                break;
                            }
                            //I002: 已終止
                            if (PAY_RESULT == "I002")
                            {
                                PAY_RESULT_ELECT = "03";   //已終止代扣
                                ELECT_FAIL_CNT = ELECT_FAIL_CNT + 1;
                                ELECT_FAIL_AMT = ELECT_FAIL_AMT + TX_AMT;
                                ELECT_STMTOUT[i] = PAY_DATA_AREA.Substring(0, 63) + PAY_RESULT_ELECT + PAY_DATA_AREA.Substring(65, 55);
                                //寫出成功/失敗明細報表
                                insertReport_TABLE();
                                break;
                            }
                            //I003:無有效卡 I007:卡片已到期
                            if (PAY_RESULT == "I003" || PAY_RESULT == "I007")
                            {
                                PAY_RESULT_ELECT = "06";   //帳號中止
                                ELECT_FAIL_CNT = ELECT_FAIL_CNT + 1;
                                ELECT_FAIL_AMT = ELECT_FAIL_AMT + TX_AMT;
                                ELECT_STMTOUT[i] = PAY_DATA_AREA.Substring(0, 63) + PAY_RESULT_ELECT + PAY_DATA_AREA.Substring(65, 55);
                                //寫出成功/失敗明細報表
                                insertReport_TABLE();
                                break;
                            }
                            //I004: 卡片未開卡
                            if (PAY_RESULT == "I004")
                            {
                                //PAY_RESULT_ELECT = "08";   //其他扣帳不成功原因
                                PAY_RESULT_ELECT = "98";   //其他扣帳不成功原因
                                ELECT_FAIL_CNT = ELECT_FAIL_CNT + 1;
                                ELECT_FAIL_AMT = ELECT_FAIL_AMT + TX_AMT;
                                ELECT_STMTOUT[i] = PAY_DATA_AREA.Substring(0, 63) + PAY_RESULT_ELECT + PAY_DATA_AREA.Substring(65, 55);
                                //寫出成功/失敗明細報表
                                insertReport_TABLE();
                                break;
                            }
                            //I006: 信用額度不足
                            if (PAY_RESULT == "I006")
                            {
                                PAY_RESULT_ELECT = "01";   //信用額度不足
                                ELECT_FAIL_CNT = ELECT_FAIL_CNT + 1;
                                ELECT_FAIL_AMT = ELECT_FAIL_AMT + TX_AMT;
                                ELECT_STMTOUT[i] = PAY_DATA_AREA.Substring(0, 63) + PAY_RESULT_ELECT + PAY_DATA_AREA.Substring(65, 55);
                                //寫出成功/失敗明細報表
                                insertReport_TABLE();
                                break;
                            }
                            //I008: 公用事業單位止扣
                            if (PAY_RESULT == "I008")
                            {
                                PAY_RESULT_ELECT = CTL_CODE;  //公用事業單位止扣
                                ELECT_FAIL_CNT = ELECT_FAIL_CNT + 1;
                                ELECT_FAIL_AMT = ELECT_FAIL_AMT + TX_AMT;
                                ELECT_STMTOUT[i] = PAY_DATA_AREA.Substring(0, 63) + PAY_RESULT_ELECT + PAY_DATA_AREA.Substring(65, 55);
                                //寫出成功/失敗明細報表
                                insertReport_TABLE();
                                break;
                            }
                            //其它錯誤
                            if (PAY_RESULT != "S000")
                            {
                                PAY_RESULT_ELECT = "98";  //其他扣帳不成功原因
                                ELECT_FAIL_CNT = ELECT_FAIL_CNT + 1;
                                ELECT_FAIL_AMT = ELECT_FAIL_AMT + TX_AMT;
                                ELECT_STMTOUT[i] = PAY_DATA_AREA.Substring(0, 63) + PAY_RESULT_ELECT + PAY_DATA_AREA.Substring(65, 55);
                                //寫出成功/失敗明細報表
                                insertReport_TABLE();
                                break;
                            }

                            #endregion

                            break;

                        case "3": //檔尾資料
                            intREC_TCount++;

                            #region 依 Layout 拆解資料
                            InfData_DataTable = xml.FileLine2DataTable(BIG5, PAY_DATA_AREA, REC_T_DataTable);
                            if (xml.strMSG.Length > 0)
                            {
                                logger.strJobQueue = "[FileLine2DataTable(REC_T)] (L" + i + 1 + ") - " + xml.strMSG.ToString().Trim();
                                logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                                return "B0099:" + logger.strJobQueue;
                            }
                            TOTAL_AMT = Convert.ToDecimal(InfData_DataTable.Rows[0]["SUCC_AMT"]);
                            TOTAL_CNT = Convert.ToDecimal(InfData_DataTable.Rows[0]["SUCC_CNT"]);

                            ELECT_TOTAL_AMT = ELECT_FAIL_AMT + ELECT_SUCCESS_AMT;
                            ELECT_TOTAL_CNT = ELECT_FAIL_CNT + ELECT_SUCCESS_CNT;

                            //檢查金額
                            if (TOTAL_AMT != ELECT_TOTAL_AMT)
                            {
                                logger.strJobQueue = "回應檔總金額和原始檔不合,請確認!!!";
                                logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                                logger.strJobQueue = "原始檔總金額 = " + TOTAL_AMT + "回應檔總金額 = " + ELECT_TOTAL_AMT;
                                logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                                return "B0099:" + logger.strJobQueue;
                            }

                            //檢查筆數
                            if (TOTAL_CNT != ELECT_TOTAL_CNT)
                            {
                                logger.strJobQueue = "回應檔總筆數和原始檔不合,請確認!!!";
                                logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                                logger.strJobQueue = "原始檔總筆數 = " + TOTAL_CNT + "回應檔總筆數 = " + ELECT_TOTAL_CNT;
                                logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                                return "B0099:" + logger.strJobQueue;
                            }

                            #endregion

                            #region 組檔尾
                            TEMP_SUCCESS_AMT = Convert.ToInt32(ELECT_SUCCESS_AMT * 100).ToString().PadLeft(16, '0');
                            TEMP_FAIL_AMT = Convert.ToInt32(ELECT_FAIL_AMT * 100).ToString().PadLeft(16, '0');

                            ELECT_STMTOUT[i] = PAY_DATA_AREA.Substring(0, 27) + TEMP_SUCCESS_AMT + ELECT_SUCCESS_CNT.ToString().PadLeft(10, '0')
                                             + TEMP_FAIL_AMT + ELECT_FAIL_CNT.ToString().PadLeft(10, '0') + "000" + PAY_DATA_AREA.Substring(82, 38);
                            #endregion

                            break;

                        default: //資料庫錯誤
                            return "B0016:" + "台電(高壓電)代扣繳 REC_TYPE ERROR " + strTag;

                    }
                }

                #region 產生回應檔 陣列 --> STMTOUT.TXT

                //設定產出檔案名稱                
                strOutFileName = FILE_PATH;
                FileStream fsOutFile = new FileStream(strOutFileName, FileMode.Create, FileAccess.Write, FileShare.None);
                using (StreamWriter srOutFile = new StreamWriter(fsOutFile, BIG5))
                {
                    //逐筆寫出資料
                    for (int k = 0; k < PUBLIC_LIST.resultTable.Rows.Count; k++)
                    {
                        srOutFile.Write(ELECT_STMTOUT[k]);
                        srOutFile.Write("\r\n");
                        srOutFile.Flush();
                        ELECT_TXT_Count = ELECT_TXT_Count + 1;
                    }
                    srOutFile.Close();
                }
                fsOutFile.Close();


                #endregion

                #region 整批新增TX_WAREHOUSE

                //先紀錄筆數
                TX_WAREHOUSE_T_Count_Insert = TX_WAREHOUSE_T.resultTable.Rows.Count;
                TX_WAREHOUSE_T.insert_by_DT();

                //判別回傳筆數是否相同
                if (TX_WAREHOUSE_T_Count_Insert != TX_WAREHOUSE_T.intInsCnt)
                {
                    logger.strJobQueue = "整批新增TX_WAREHOUSE_T時筆數異常,原筆數 : " + TX_WAREHOUSE_T_Count_Insert + " 實際筆數: " + TX_WAREHOUSE_T.intInsCnt;
                    logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                    return "B0012" + logger.strJobQueue;
                }

                logger.strJobQueue = "整批新增至 TX_WAREHOUSE 成功筆數 =" + TX_WAREHOUSE_T.intInsCnt;
                logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);

                #endregion

                writeReport();

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

        #region insertReport_TABLE
        void insertReport_TABLE()
        {
            REPORT_TABLE.Rows.Add();
            k = REPORT_TABLE.Rows.Count - 1;
            REPORT_TABLE.Rows[k]["RPT_SEQ"] = REPORT_TABLE.Rows.Count.ToString().PadLeft(7, '0');
            REPORT_TABLE.Rows[k]["PAY_NBR"] = PAY_NBR;
            REPORT_TABLE.Rows[k]["PAY_YYMM"] = (BILL_DTE.PadLeft(5, '0')).Substring(0, 3) + "-" + (BILL_DTE.PadLeft(5, '0')).Substring(3, 2);
            REPORT_TABLE.Rows[k]["PAY_CARD_NBR"] = PAY_CARD_NBR;
            REPORT_TABLE.Rows[k]["NAME"] = NAME;
            REPORT_TABLE.Rows[k]["PAY_AMT"] = TX_AMT;
            REPORT_TABLE.Rows[k]["PAY_DTE"] = PAY_DTE.ToString("yyyy/MM/dd");

            switch (PAY_RESULT)
            {
                case "S000":
                    REPORT_TABLE.Rows[k]["PAY_RESULT"] = PAY_RESULT + " - 扣繳成功";
                    break;

                case "I001":
                    REPORT_TABLE.Rows[k]["PAY_RESULT"] = PAY_RESULT + " - 未申請代扣";
                    break;

                case "I002":
                    REPORT_TABLE.Rows[k]["PAY_RESULT"] = PAY_RESULT + " - 有申請代繳但已終止";
                    break;

                case "I003":
                    REPORT_TABLE.Rows[k]["PAY_RESULT"] = PAY_RESULT + " - CTL_CODE有誤(" + CTL_CODE + ")";
                    break;

                case "I004":
                    REPORT_TABLE.Rows[k]["PAY_RESULT"] = PAY_RESULT + " - 卡片未開卡";
                    break;

                case "I005":
                    REPORT_TABLE.Rows[k]["PAY_RESULT"] = PAY_RESULT + " - 卡片不存在主檔";
                    break;

                case "I006":
                    REPORT_TABLE.Rows[k]["PAY_RESULT"] = PAY_RESULT + " - 信用額度不足";
                    break;

                case "I007":
                    REPORT_TABLE.Rows[k]["PAY_RESULT"] = PAY_RESULT + " - 卡片已過期";
                    break;

                default:
                    REPORT_TABLE.Rows[k]["PAY_RESULT"] = PAY_RESULT + " - 其他錯誤";
                    break;
            }
        }
        #endregion

        #region writeReport
        void writeReport()
        {
            CMCRPT001 CMCRPT001 = new CMCRPT001();
            Decimal ELECT_SUCCESS_FEE = 0;
            Decimal ELECT_SUCCESS_NET_AMT = 0;

            ELECT_SUCCESS_FEE = Convert.ToDecimal(ELECT_SUCCESS_CNT * 3);
            ELECT_SUCCESS_NET_AMT = ELECT_SUCCESS_AMT - ELECT_SUCCESS_FEE;

            //設定特殊欄位資料
            ArrayList alSumData = new ArrayList();
            alSumData.Add(new ExcelFactory.ListItem("#RPT_DATE", TODAY_PROCESS_DTE.ToString("yyyy/MM/dd")));
            alSumData.Add(new ExcelFactory.ListItem("#TOTAL_CNT", ELECT_TOTAL_CNT + " 筆"));
            alSumData.Add(new ExcelFactory.ListItem("#TOTAL_AMT", ELECT_TOTAL_AMT));
            alSumData.Add(new ExcelFactory.ListItem("#SUCCESS_CNT", ELECT_SUCCESS_CNT + " 筆"));
            alSumData.Add(new ExcelFactory.ListItem("#SUCCESS_AMT", ELECT_SUCCESS_AMT));
            alSumData.Add(new ExcelFactory.ListItem("#FAIL_CNT", ELECT_FAIL_CNT + " 筆"));
            alSumData.Add(new ExcelFactory.ListItem("#FAIL_AMT", ELECT_FAIL_AMT));
            alSumData.Add(new ExcelFactory.ListItem("#SUCCESS_FEE", ELECT_SUCCESS_FEE.ToString("0")));
            alSumData.Add(new ExcelFactory.ListItem("#NET_SUCC_AMT", ELECT_SUCCESS_NET_AMT.ToString("0")));
            //設定合併欄位資料
            ArrayList alMegData = new ArrayList();

            //產出報表
            CMCRPT001.Output(REPORT_TABLE, alSumData, alMegData, "PBRELO001(台電扣繳成功失敗報表_高壓電)", "PBRELO001", "Sheet1", "Sheet1", TODAY_PROCESS_DTE, true);
        }
        #endregion

        #region Display
        void writeDisplay()
        {
            logger.strJobID = "00002";
            //擷取台電(高壓電)需回應資料
            logger.strTBL_NAME = "PUBLIC_LIST";
            logger.strTBL_ACCESS = "Q";
            logger.intTBL_COUNT = PUBLIC_LIST_Query_Count;
            logger.writeCounter();

            //台電(高壓電)回應檔筆數(含頭尾)
            logger.strTBL_NAME = "ELECT_STMTOUT";
            logger.strTBL_ACCESS = "I";
            logger.intTBL_COUNT = ELECT_TXT_Count;
            logger.writeCounter();

            //寫入TX_WAREHOUSE筆數
            logger.strTBL_NAME = "TX_WAREHOUSE";
            logger.strTBL_ACCESS = "I";
            logger.intTBL_COUNT = TX_WAREHOUSE_T_Count_Insert;
            logger.writeCounter();

            //更新PUBLIC_HIST
            logger.strTBL_NAME = "PUBLIC_HIST";
            logger.strTBL_ACCESS = "U";
            logger.intTBL_COUNT = PUBLIC_HIST_Update_Count;
            logger.writeCounter();

            logger.DisplayCounter(Cybersoft.Coca.Log.LevelEnum.Info);
        }
        #endregion
    }
}

