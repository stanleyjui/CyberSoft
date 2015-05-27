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
    /// 產生中華電信扣帳回應檔,並將入帳交易先寫至TX_WAREHOUSE,待下一營業日入帳
    /// 執行週期: 每營業日, 有扣款檔才產生扣款回應檔
    /// </summary>
    public class PBBPHO001
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
        //宣告SETUP_GL
        String SETUP_GL_RC = "";
        SETUP_GLDao SETUP_GL = new SETUP_GLDao();

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

        //宣告SETUP_PUBLIC
        String SETUP_PUBLIC_RC = "";
        SETUP_PUBLICDao SETUP_PUBLIC = new SETUP_PUBLICDao();
        DataTable SETUP_PUBLIC_DataTable = new DataTable();

        //暫存報表TABLE
        DataTable REPORT_TABLE = new DataTable();
        
        //檔案格式
        DataTable REC_H_DataTable = new DataTable();
        DataTable REC_DataTable = new DataTable();
        DataTable REC_T_DataTable = new DataTable();
        #endregion

        #region 設定檔案編碼
        Encoding BIG5 = Encoding.GetEncoding("big5");
        #endregion

        #region 副程式
        CMCNBR001 CMCNBR001 = new CMCNBR001();
        String PHONE_MASK_STRING = "";
        #endregion

        #region 宣告共用變數
        string strPAY_TYPE = "0001";
        //批次日期
        DateTime PREV_PROCESS_DTE = new DateTime();
        DateTime TODAY_PROCESS_DTE = new DateTime();
        DateTime NEXT_PROCESS_DTE = new DateTime();

        //筆數&金額
        int PUBLIC_LIST_Query_Count = 0;
        int PUBLIC_HIST_Update_Count = 0;
        int i = 0;
        int x = 0; //控制TX_WAREHOUSE_T
        int k = 0; //REPORT_TABLE
        int intREC_HCount = 0;
        int intRECCount = 0;
        int intREC_TCount = 0;
        int PHONE_TOTAL_CNT = 0;
        Decimal PHONE_TOTAL_AMT = 0;
        int TX_WAREHOUSE_T_Count_Insert = 0;
        int PHONE_TXT_Count = 0;
        string temp_yyymmdd = "";
        string strToday = "";
        string strSOURCE_CODE = "PU00000001";
        string strMT_TYPE = "D";
        decimal decFeeAmt = 0;

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
        String ERROR_REASON = "";
        Decimal TOTAL_AMT = 0;
        Decimal TOTAL_CNT = 0;

        //中華電信回應檔欄位
        String[] PHONE_STMTOUT = null;
        String PAY_RESULT_PHONE = "";
        Decimal PHONE_SUCCESS_AMT = 0;
        Decimal PHONE_FAIL_AMT = 0;
        int PHONE_SUCCESS_CNT = 0;
        int PHONE_FAIL_CNT = 0;
        String TEMP_SUCCESS_AMT = "";
        String TEMP_FAIL_AMT = "";
        String srBANK = "";

        //中華電信遮罩欄位
        //String MASK_TYPE = "00000000******000000";  //第14~16碼躲起來
        String MASK_TYPE = "";

        //檔案長度
        const int intDataLength = 85;

        #endregion

        #region 宣告檔案路徑
        String strRecNAME = "";
        //XML放置路徑 
        String strXML_Layout = "";
        //寫出檔案名稱
        String strOutFileName = "";
        //寫出檔案路徑
        String FILE_PATH = "";
        String strFILE_NAME = "";
        #endregion

        static readonly Cybersoft.Coca.Log.Logger logger = Cybersoft.Log.LogManager<Cybersoft.Coca.Log.Logger>.GetLogger(System.Reflection.MethodInfo.GetCurrentMethod().ReflectedType.Name);
        public string RUN()
        //public string RUN(string strPAY_TYPE)
        {
            #region 準備MSGLG.SYSOUT CLASS
            logger.strJobID = JobID;
            logger.strJOBNAME = "PBBPHO001";
            logger.dtRunDate = DateTime.Now;
            #endregion

            try
            {
                #region 取得系統資訊(前次批次日、本日批次日)
                CMCSYS001 SYSINF = new CMCSYS001();
                SYSINF.strJobID = JobID;
                SYSINF.strJobName = "PBBPHO001";
                String SYSINF_RC = SYSINF.getSYSINF();
                PREV_PROCESS_DTE = SYSINF.datetimePREV_PROCESS_DTE;
                TODAY_PROCESS_DTE = SYSINF.datetimeTODAY_PROCESS_DTE;
                NEXT_PROCESS_DTE = SYSINF.datetimeBANK_NEXT_PROCESS_DTE;
                temp_yyymmdd = Convert.ToString(Convert.ToInt32(TODAY_PROCESS_DTE.ToString("yyyy")) - 1911) + TODAY_PROCESS_DTE.ToString("MMdd");
                strToday = TODAY_PROCESS_DTE.ToString("yyyyMMdd");
                srBANK = SYSINF.strBANK_NBR;
                #endregion

                #region Table定義
                //複製TX_WAREHOUSE_T的Table定義
                TX_WAREHOUSE_T.table_define();

                // 定義報表Table
                REPORT_TABLE.Columns.Add("RPT_SEQ", typeof(string));
                REPORT_TABLE.Columns.Add("PAY_NBR", typeof(string));
                REPORT_TABLE.Columns.Add("PAY_YYMM", typeof(string));
                REPORT_TABLE.Columns.Add("PAY_CARD_NBR", typeof(string));
                REPORT_TABLE.Columns.Add("NAME", typeof(string));
                REPORT_TABLE.Columns.Add("PAY_AMT", typeof(decimal));
                REPORT_TABLE.Columns.Add("PAY_DTE", typeof(string));
                REPORT_TABLE.Columns.Add("PAY_RESULT", typeof(string));
                #endregion

                #region 擷取公共事業單位手續費成本
                SETUP_GL.init();
                SETUP_GL.strWhereCODE = "0001";
                SETUP_GL.strWhereSOURCE_CODE = strSOURCE_CODE;
                SETUP_GL.strWhereMT_TYPE = strMT_TYPE;
                SETUP_GL_RC = SETUP_GL.query();
                switch (SETUP_GL_RC)
                {
                    case "S0000": //查詢成功
                        decFeeAmt = Convert.ToDecimal(SETUP_GL.resultTable.Rows[0]["COST_AMT"]);
                        break;

                    case "F0023": //無需處理資料
                        logger.strJobQueue = "查無此公共事業單位類別的參數：轉帳類別：中華電信";
                        logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                        return "B0000";

                    default: //資料庫錯誤
                        logger.strJobQueue = "查詢SETUP_GL 資料錯誤:" + SETUP_GL_RC;
                        logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                        return "B0016:" + logger.strJobQueue;
                }
                #endregion

                #region 擷取公共事業單位相關設定 (KEY:PAY_TYPE)
                SETUP_PUBLIC.init();
                SETUP_PUBLIC.strWherePAY_TYPE = strPAY_TYPE;
                SETUP_PUBLIC_RC = SETUP_PUBLIC.query();
                switch (SETUP_PUBLIC_RC)
                {
                    case "S0000": //查詢成功
                        int SETUP_PUBLIC_Query_Count = SETUP_PUBLIC.resultTable.Rows.Count;
                        //取得參數內容(檔案名稱)
                        if (SETUP_PUBLIC_Query_Count == 1)
                        {
                            MASK_TYPE = Convert.ToString(SETUP_PUBLIC.resultTable.Rows[0]["PAY_NBR_DIS_BYTE"]);
                            //XML檔案格式名稱
                            strRecNAME = SETUP_PUBLIC.resultTable.Rows[0]["FILE_FORMAT"].ToString().Trim();
                            strFILE_NAME  = SETUP_PUBLIC.resultTable.Rows[0]["FILE_NAME"].ToString().Trim();
                        }
                        else
                        {
                            logger.strJobQueue = "**機構代碼設定筆數有誤(相同類別代碼設定超過1筆)**";
                            logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                            return "B0016:" + logger.strJobQueue;
                        }
                        break;

                    case "F0023": //無需處理資料
                        logger.strJobQueue = "查無此公共事業單位類別的參數：轉帳類別：中華電信";
                        logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                        return "B0000";

                    default: //資料庫錯誤
                        logger.strJobQueue = "查詢SETUP_PUBLIC 資料錯誤:" + SETUP_PUBLIC_RC;
                        logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                        return "B0016:" + logger.strJobQueue;
                }
                #endregion

                #region 宣告檔案路徑 & 讀入檔案格式(XML)
                Cybersoft.Base.CMCURL001 CMCURL = new CMCURL001();
                if (strRecNAME != "")
                {
                    #region XML路徑
                    //取得 FISC XML格式
                    strXML_Layout = CMCURL.getURL(strRecNAME + ".xml" );
                    
                    if (strXML_Layout == "")
                    {
                        logger.strJobQueue = "File.xml 路徑取得錯誤!!! 路徑為：" + strXML_Layout;
                        logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                        return "B0099:" + logger.strJobQueue;
                    }
                    else
                    {
                        logger.strJobQueue = " File.xml 路徑取得錯誤!!! 路徑為："  + strXML_Layout;
                        logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                    }
                    #endregion

                    #region 載入檔案格式資訊(XML)
                    FileParseByXml xml = new FileParseByXml();

                    // REC_H Layout
                    REC_H_DataTable = xml.Xml2DataTable(strXML_Layout, "STMTOUT_H");

                    if (xml.strMSG.Length > 0)
                    {
                        logger.strJobQueue = "[Xml2DataTable(STMTOUT_H)] - " + xml.strMSG.ToString().Trim();
                        logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                        return "B0099:" + logger.strJobQueue;
                    }

                    // REC Layout
                    REC_DataTable = xml.Xml2DataTable(strXML_Layout, "STMTOUT");

                    if (xml.strMSG.Length > 0)
                    {
                        logger.strJobQueue = "[Xml2DataTable(STMTOUT)] - " + xml.strMSG.ToString().Trim();
                        logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                        return "B0099:" + logger.strJobQueue;
                    }

                    // REC_T Layout
                    REC_T_DataTable = xml.Xml2DataTable(strXML_Layout, "STMTOUT_T");

                    if (xml.strMSG.Length > 0)
                    {
                        logger.strJobQueue = "[Xml2DataTable(STMTOUT_T)] - " + xml.strMSG.ToString().Trim();
                        logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                        return "B0099:" + logger.strJobQueue;
                    }

                    #endregion
                }
                else
                {
                    logger.strJobQueue = "參數檔未設定檔案格式 : 機構代碼(" + strPAY_TYPE + ") 格式名稱(" + strRecNAME + ")";
                    logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                    return "B0099:" + logger.strJobQueue;
                }
                #region 產出檔案路徑(參數設定檔名，則以該設定為outfile檔名)
                if (strFILE_NAME != "")
                {
                    FILE_PATH = CMCURL.getURL(strRecNAME + "_STMTOUT") + strFILE_NAME;
                }
                else
                {
                    FILE_PATH = CMCURL.ReplaceVarDateFormat(CMCURL.getPATH(strRecNAME + "_STMTOUT"), TODAY_PROCESS_DTE);
                }
                #endregion
                
                #endregion

                #region 擷取今日需送回應檔之中華電信資料
                PUBLIC_LIST.init();
                PUBLIC_LIST.strWherePAY_TYPE = strPAY_TYPE;

                PUBLIC_LIST_RC = PUBLIC_LIST.query_for_out(strToday, "");
                switch (PUBLIC_LIST_RC)
                {
                    case "S0000": //查詢成功
                        PUBLIC_LIST_Query_Count = PUBLIC_LIST.resultTable.Rows.Count;
                        PHONE_STMTOUT = new string[PUBLIC_LIST_Query_Count];
                        break;

                    case "F0023": //無需處理資料
                        logger.strJobQueue = "今日無中華電信回應檔需產生;今日需送回應檔時間為大於 " + PREV_PROCESS_DTE + "且小於等於" + TODAY_PROCESS_DTE;
                        logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                        return "B0000";

                    default: //資料庫錯誤
                        logger.strJobQueue = "查詢PUBLIC_LIST 資料錯誤:" + PUBLIC_LIST_RC;
                        logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                        return "B0016:" + logger.strJobQueue;
                }
                #endregion

                #region 中華電信回應檔
                for (i = 0; i < PUBLIC_LIST.resultTable.Rows.Count; i++)
                {

                    PAY_DATA_AREA = Convert.ToString(PUBLIC_LIST.resultTable.Rows[i]["PAY_DATA_AREA"]);
                    DataTable InfData_DataTable = new DataTable();
                    FileParseByXml xml = new FileParseByXml();
                    string strTag = PAY_DATA_AREA.Substring(0, 1).ToString().Trim();
                    switch (strTag)
                    {
                        case "1":  //檔頭資料       
                            intREC_HCount++;                            
                            #region  組檔頭
                            PHONE_STMTOUT[i] = PAY_DATA_AREA.Substring(0, 39) + temp_yyymmdd + srBANK + new string(' ', 56);
                            #endregion
                            #region 刪除TX_WAREHOUSE,RERUN 機制
                            TX_WAREHOUSE.DateTimeWhereEFF_DTE = PAY_DTE;
                            TX_WAREHOUSE.strWhereSOURCE_CODE = strSOURCE_CODE;
                            TX_WAREHOUSE.DateTimeWhereMNT_DT = TODAY_PROCESS_DTE;
                            TX_WAREHOUSE.strWhereMNT_USER = "PBBPHO001";
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
                                    ERROR_REASON = Convert.ToString(PUBLIC_HIST.resultTable.Rows[0]["ERROR_REASON"]);
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
                            #region 組明細及中華電信回應碼

                            if (PAY_RESULT == "S000" ||
                                 PAY_RESULT == "0000")
                            {
                                PAY_RESULT_PHONE = "  ";  //扣帳成功
                                PHONE_SUCCESS_CNT = PHONE_SUCCESS_CNT + 1;
                                PHONE_SUCCESS_AMT = PHONE_SUCCESS_AMT + TX_AMT;
                                PHONE_STMTOUT[i] = PAY_DATA_AREA.Substring(0, 53) + PAY_DATA_AREA.Substring(81, 13)
                                                 + PAY_RESULT_PHONE + PAY_DATA_AREA.Substring(97, 1) + new string(' ', 36);
                                //PHONE_STMTOUT[i] = PAY_DATA_AREA.Substring(0, 53) + PAY_DATA_AREA.Substring(82, 13)
                                //                 + PAY_RESULT_PHONE + PAY_DATA_AREA.Substring(98, 1) + new string(' ', 36);

                                #region 組TX_WAREHOUSE
                                if (TX_AMT > 0)
                                {
                                    TX_WAREHOUSE_T.initInsert_row();
                                    x = TX_WAREHOUSE_T.resultTable.Rows.Count - 1;
                                    TX_WAREHOUSE_T.resultTable.Rows[x]["BU"] = BU;
                                    TX_WAREHOUSE_T.resultTable.Rows[x]["ACCT_NBR"] = ACCT_NBR;
                                    TX_WAREHOUSE_T.resultTable.Rows[x]["CARD_PRODUCT"] = CARD_PRODUCT;
                                    TX_WAREHOUSE_T.resultTable.Rows[x]["PRODUCT"] = PRODUCT;
                                    TX_WAREHOUSE_T.resultTable.Rows[x]["CURRENCY"] = CURRENCY;
                                    TX_WAREHOUSE_T.resultTable.Rows[x]["CARD_NBR"] = PAY_CARD_NBR;
                                    TX_WAREHOUSE_T.resultTable.Rows[x]["EFF_DTE"] = NEXT_PROCESS_DTE;           //次營業日執行入帳作業
                                    TX_WAREHOUSE_T.resultTable.Rows[x]["POSTING_DTE"] = PAY_DTE;
                                    TX_WAREHOUSE_T.resultTable.Rows[x]["ARN"] = PAY_NBR;
                                    TX_WAREHOUSE_T.resultTable.Rows[x]["CODE"] = "0001";
                                    TX_WAREHOUSE_T.resultTable.Rows[x]["SOURCE_CODE"] = strSOURCE_CODE;
                                    TX_WAREHOUSE_T.resultTable.Rows[x]["MCC_CODE"] = "4900";
                                    TX_WAREHOUSE_T.resultTable.Rows[x]["AUTH_CODE"] = AUTH_CODE;
                                    TX_WAREHOUSE_T.resultTable.Rows[x]["SEQ"] = x.ToString().PadLeft(10, '0');
                                    TX_WAREHOUSE_T.resultTable.Rows[x]["ORIG_DTE"] = PAY_DTE;                   //實際交易日=指定扣帳日
                                    //偷偷把電話遮住
                                    PHONE_MASK_STRING = CMCNBR001.GET_MASK(PAY_NBR, MASK_TYPE);
                                    //交易說明加上帳單年月
                                    TX_WAREHOUSE_T.resultTable.Rows[x]["DESCR"] = Strings.StrConv("中華電信" + PHONE_MASK_STRING.Substring(4, 12).Trim() + "-" + BILL_DTE.Substring(3, 2) + "月", VbStrConv.Wide);
                                    TX_WAREHOUSE_T.resultTable.Rows[x]["MT_TYPE"] = strMT_TYPE;
                                    TX_WAREHOUSE_T.resultTable.Rows[x]["AMT"] = TX_AMT;
                                    TX_WAREHOUSE_T.resultTable.Rows[x]["MNT_DT"] = TODAY_PROCESS_DTE;
                                    TX_WAREHOUSE_T.resultTable.Rows[x]["MNT_USER"] = "PBBPHO001";
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
                                PUBLIC_HIST.strMNT_USER = "PBBPHO001";
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
                                PAY_RESULT_PHONE = "02";   //未申請代扣
                                PHONE_FAIL_CNT = PHONE_FAIL_CNT + 1;
                                PHONE_FAIL_AMT = PHONE_FAIL_AMT + TX_AMT;
                                PHONE_STMTOUT[i] = PAY_DATA_AREA.Substring(0, 53) + PAY_DATA_AREA.Substring(81, 13)
                                                 + PAY_RESULT_PHONE + PAY_DATA_AREA.Substring(97, 1) + new string(' ', 36);
                                //PHONE_STMTOUT[i] = PAY_DATA_AREA.Substring(0, 53) + PAY_DATA_AREA.Substring(82, 13)
                                //                 + PAY_RESULT_PHONE + PAY_DATA_AREA.Substring(98, 1) + new string(' ', 36);

                                //寫出成功/失敗明細報表
                                insertReport_TABLE();

                                break;
                            }
                            //I002: 已終止
                            if (PAY_RESULT == "I002")
                            {
                                PAY_RESULT_PHONE = "03";   //已終止代扣
                                PHONE_FAIL_CNT = PHONE_FAIL_CNT + 1;
                                PHONE_FAIL_AMT = PHONE_FAIL_AMT + TX_AMT;
                                PHONE_STMTOUT[i] = PAY_DATA_AREA.Substring(0, 53) + PAY_DATA_AREA.Substring(81, 13)
                                                 + PAY_RESULT_PHONE + PAY_DATA_AREA.Substring(97, 1) + new string(' ', 36);
                                //PHONE_STMTOUT[i] = PAY_DATA_AREA.Substring(0, 53) + PAY_DATA_AREA.Substring(82, 13)
                                //                 + PAY_RESULT_PHONE + PAY_DATA_AREA.Substring(98, 1) + new string(' ', 36);

                                //寫出成功/失敗明細報表
                                insertReport_TABLE();

                                break;
                            }
                            //I003:無有效卡 I007:卡片已到期
                            if (PAY_RESULT == "I003" || PAY_RESULT == "I007")
                            {
                                PAY_RESULT_PHONE = "06";   //帳號中止
                                PHONE_FAIL_CNT = PHONE_FAIL_CNT + 1;
                                PHONE_FAIL_AMT = PHONE_FAIL_AMT + TX_AMT;
                                PHONE_STMTOUT[i] = PAY_DATA_AREA.Substring(0, 53) + PAY_DATA_AREA.Substring(81, 13)
                                                 + PAY_RESULT_PHONE + PAY_DATA_AREA.Substring(97, 1) + new string(' ', 36);
                                //PHONE_STMTOUT[i] = PAY_DATA_AREA.Substring(0, 53) + PAY_DATA_AREA.Substring(82, 13)
                                //                 + PAY_RESULT_PHONE + PAY_DATA_AREA.Substring(98, 1) + new string(' ', 36);

                                //寫出成功/失敗明細報表
                                insertReport_TABLE();

                                break;
                            }
                            //I004: 卡片未開卡
                            if (PAY_RESULT == "I004")
                            {
                                PAY_RESULT_PHONE = "08";   //其他扣帳不成功原因
                                PHONE_FAIL_CNT = PHONE_FAIL_CNT + 1;
                                PHONE_FAIL_AMT = PHONE_FAIL_AMT + TX_AMT;
                                PHONE_STMTOUT[i] = PAY_DATA_AREA.Substring(0, 53) + PAY_DATA_AREA.Substring(81, 13)
                                                 + PAY_RESULT_PHONE + PAY_DATA_AREA.Substring(97, 1) + new string(' ', 36);
                                //PHONE_STMTOUT[i] = PAY_DATA_AREA.Substring(0, 53) + PAY_DATA_AREA.Substring(82, 13)
                                //                 + PAY_RESULT_PHONE + PAY_DATA_AREA.Substring(98, 1) + new string(' ', 36);

                                //寫出成功/失敗明細報表
                                insertReport_TABLE();

                                break;
                            }
                            //I006: 信用額度不足
                            if (PAY_RESULT == "I006")
                            {
                                PAY_RESULT_PHONE = "09";   //信用額度不足
                                PHONE_FAIL_CNT = PHONE_FAIL_CNT + 1;
                                PHONE_FAIL_AMT = PHONE_FAIL_AMT + TX_AMT;
                                PHONE_STMTOUT[i] = PAY_DATA_AREA.Substring(0, 53) + PAY_DATA_AREA.Substring(81, 13)
                                                 + PAY_RESULT_PHONE + PAY_DATA_AREA.Substring(97, 1) + new string(' ', 36);
                                //PHONE_STMTOUT[i] = PAY_DATA_AREA.Substring(0, 53) + PAY_DATA_AREA.Substring(82, 13)
                                //                 + PAY_RESULT_PHONE + PAY_DATA_AREA.Substring(98, 1) + new string(' ', 36);

                                //寫出成功/失敗明細報表
                                insertReport_TABLE();

                                break;
                            }
                            //其它錯誤
                            if (PAY_RESULT != "S000")
                            {
                                PAY_RESULT_PHONE = "08";  //其他扣帳不成功原因
                                PHONE_FAIL_CNT = PHONE_FAIL_CNT + 1;
                                PHONE_FAIL_AMT = PHONE_FAIL_AMT + TX_AMT;
                                PHONE_STMTOUT[i] = PAY_DATA_AREA.Substring(0, 53) + PAY_DATA_AREA.Substring(81, 13)
                                                 + PAY_RESULT_PHONE + PAY_DATA_AREA.Substring(97, 1) + new string(' ', 36);
                                //PHONE_STMTOUT[i] = PAY_DATA_AREA.Substring(0, 53) + PAY_DATA_AREA.Substring(82, 13)
                                //                 + PAY_RESULT_PHONE + PAY_DATA_AREA.Substring(98, 1) + new string(' ', 36);

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
                            TOTAL_AMT = Convert.ToDecimal(InfData_DataTable.Rows[0]["TOTAL_AMT"]);
                            TOTAL_CNT = Convert.ToDecimal(InfData_DataTable.Rows[0]["TOTAL_CNT"]);

                            PHONE_TOTAL_AMT = PHONE_FAIL_AMT + PHONE_SUCCESS_AMT;
                            PHONE_TOTAL_CNT = PHONE_FAIL_CNT + PHONE_SUCCESS_CNT;

                            //檢查金額
                            if (TOTAL_AMT != PHONE_TOTAL_AMT)
                            {
                                logger.strJobQueue = "回應檔總金額和原始檔不合,請確認!!!";
                                logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                                logger.strJobQueue = "原始檔總金額 = " + TOTAL_AMT + "回應檔總金額 = " + PHONE_TOTAL_AMT;
                                logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                                return "B0099:" + logger.strJobQueue;
                            }

                            //檢查筆數
                            if (TOTAL_CNT != PHONE_TOTAL_CNT)
                            {
                                logger.strJobQueue = "回應檔總筆數和原始檔不合,請確認!!!";
                                logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                                logger.strJobQueue = "原始檔總筆數 = " + TOTAL_CNT + "回應檔總筆數 = " + PHONE_TOTAL_CNT;
                                logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                                return "B0099:" + logger.strJobQueue;
                            }

                            #endregion
                            #region 組檔尾
                            TEMP_SUCCESS_AMT = Convert.ToInt32(PHONE_SUCCESS_AMT).ToString().PadLeft(13, '0');
                            TEMP_FAIL_AMT = Convert.ToInt32(PHONE_FAIL_AMT).ToString().PadLeft(13, '0');

                            PHONE_STMTOUT[i] = '3' + TEMP_SUCCESS_AMT + PHONE_SUCCESS_CNT.ToString().PadLeft(8, '0')
                                             + TEMP_FAIL_AMT + PHONE_FAIL_CNT.ToString().PadLeft(8, '0') + new string(' ', 62);
                            #endregion
                            break;

                        default: //資料庫錯誤
                            return "B0016:" + "PUBLIC_LIST中華電信代扣繳 REC_TYPE ERROR " + strTag;

                    }
                }
                #endregion

                #region 產生回應檔 陣列 --> STMTOUT.TXT

                //設定產出檔案名稱                
                strOutFileName = FILE_PATH;
                FileStream fsOutFile = new FileStream(strOutFileName, FileMode.Create, FileAccess.Write, FileShare.None);
                using (StreamWriter srOutFile = new StreamWriter(fsOutFile, BIG5))
                {
                    //逐筆寫出資料
                    for (int k = 0; k < PUBLIC_LIST.resultTable.Rows.Count; k++)
                    {
                        srOutFile.Write(PHONE_STMTOUT[k]);
                        srOutFile.Write("\r\n");
                        srOutFile.Flush();
                        PHONE_TXT_Count = PHONE_TXT_Count + 1;
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
            REPORT_TABLE.Rows[k]["PAY_RESULT"] = PAY_RESULT + "-" + ERROR_REASON;
            if (PAY_RESULT == "I003")
            {
                REPORT_TABLE.Rows[k]["PAY_RESULT"] = PAY_RESULT + "-" + ERROR_REASON + "(" + CTL_CODE + ")";
            }
        }
        #endregion

        #region writeReport
        void writeReport()
        {
            CMCRPT001 CMCRPT001 = new CMCRPT001();
            Decimal PHONE_TOTAL_FEE = 0;
            Decimal PHONE_SUCCESS_FEE = 0;
            Decimal PHONE_FAIL_FEE = 0;

            PHONE_TOTAL_FEE = Convert.ToDecimal(PHONE_TOTAL_CNT * decFeeAmt);
            //四捨五入
            PHONE_SUCCESS_FEE = Math.Round(Convert.ToDecimal(PHONE_SUCCESS_CNT * decFeeAmt), 0, MidpointRounding.AwayFromZero);
            PHONE_FAIL_FEE = Math.Round(Convert.ToDecimal(PHONE_FAIL_CNT * decFeeAmt), 0, MidpointRounding.AwayFromZero);

            //設定特殊欄位資料
            ArrayList alSumData = new ArrayList();
            alSumData.Add(new ExcelFactory.ListItem("#RPT_DATE", TODAY_PROCESS_DTE.ToString("yyyy/MM/dd")));
            alSumData.Add(new ExcelFactory.ListItem("#TOTAL_CNT", PHONE_TOTAL_CNT + " 筆"));
            alSumData.Add(new ExcelFactory.ListItem("#TOTAL_AMT", PHONE_TOTAL_AMT));
            alSumData.Add(new ExcelFactory.ListItem("#SUCCESS_CNT", PHONE_SUCCESS_CNT + " 筆"));
            alSumData.Add(new ExcelFactory.ListItem("#SUCCESS_AMT", PHONE_SUCCESS_AMT));
            alSumData.Add(new ExcelFactory.ListItem("#FAIL_CNT", PHONE_FAIL_CNT + " 筆"));
            alSumData.Add(new ExcelFactory.ListItem("#FAIL_AMT", PHONE_FAIL_AMT));
            alSumData.Add(new ExcelFactory.ListItem("#TOTAL_FEE", PHONE_TOTAL_FEE.ToString("0")));
            alSumData.Add(new ExcelFactory.ListItem("#SUCCESS_FEE", PHONE_SUCCESS_FEE.ToString("0")));
            alSumData.Add(new ExcelFactory.ListItem("#FAIL_FEE", PHONE_FAIL_FEE.ToString("0")));
            alSumData.Add(new ExcelFactory.ListItem("#NET_TOT_AMT", (PHONE_SUCCESS_AMT - PHONE_SUCCESS_FEE).ToString("0")));
            //設定合併欄位資料
            ArrayList alMegData = new ArrayList();

            //產出報表
            CMCRPT001.Output(REPORT_TABLE, alSumData, alMegData, "PBRPHO001(中華電信扣繳成功失敗報表)", "PBRPHO001", "Sheet1", "Sheet1", TODAY_PROCESS_DTE, true);
        }
        #endregion

        #region Display
        void writeDisplay()
        {
            logger.strJobID = "00002";
            //擷取中華電信需回應資料
            logger.strTBL_NAME = "PUBLIC_LIST";
            logger.strTBL_ACCESS = "Q";
            logger.intTBL_COUNT = PUBLIC_LIST_Query_Count;
            logger.writeCounter();

            //中華電信回應檔筆數(含頭尾)
            logger.strTBL_NAME = "PHONE_STMTOUT";
            logger.strTBL_ACCESS = "I";
            logger.intTBL_COUNT = PHONE_TXT_Count;
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


