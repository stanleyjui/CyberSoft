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
    /// 產生ACH扣帳回應檔,並將入帳交易先寫至TX_WAREHOUSE,待下一營業日入帳
    /// </summary>
    public class PBBAHO001
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

        //宣告SETUP_CODE
        String SETUP_CODE_RC = "";
        SETUP_CODEDao SETUP_CODE = new SETUP_CODEDao();

        //宣告SETUP_PUBLIC
        String SETUP_PUBLIC_RC = "";
        SETUP_PUBLICDao SETUP_PUBLIC = new SETUP_PUBLICDao();
        DataTable SETUP_PUBLIC_DATATABLE = new DataTable();

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

        //筆數&金額
        int SETUP_PUBLIC_Query_Count = 0;
        int PUBLIC_LIST_Query_Count = 0;
        int PUBLIC_HIST_Update_Count = 0;
        int i = 0;
        int x = 0; //控制TX_WAREHOUSE_T
        int k = 0; //REPORT_TABLE
        int intRECCount = 0;
        int TX_WAREHOUSE_T_Count_Insert = 0;
        int ACH_TXT_Count = 0;
        string temp_yyymmdd = "";

        //PUBLIC_LIST欄位
        String PAY_DATA_AREA = "";
        String BILL_DTE = "";
        Decimal TX_AMT = 0;
        String NAME = "";
        String QUERY_PAY_TYPE = "";
        String PAY_TYPE = "";

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
        String SOURCE_CODE = "";
        String ACH_DESC = "";
        String REPORT_TITLE = "";
        String REPORT_NAME = "";
        String MNT_USER = "";
        String CTL_CODE = "";

        //ACH回應檔欄位
        String[] ACH_STMTOUT = null;
        String PAY_RESULT_ACH = "";
        Decimal ACH_TOTAL_AMT = 0;
        Decimal ACH_SUCCESS_AMT = 0;
        Decimal ACH_FAIL_AMT = 0;
        int ACH_TOTAL_CNT = 0;
        int ACH_SUCCESS_CNT = 0;
        int ACH_FAIL_CNT = 0;

        //ACH遮罩欄位
        //String MASK_TYPE = "0000000**00000000000";  //第8~9碼躲起來

        //檔案長度
        const int intDataLength = 85;

        //參數設定
        string strPayCardTrans = "";
        string strFileName = "";
        string strRecNAME = "";
        string strPayType = "";
        string strSEND_UNIT = "";
        string strRECV_UNIT = "";
        string strTRANSFER_UNIT = "";
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
        public string RUN(String strSOURCE)
        {
            #region 準備MSGLG.SYSOUT CLASS
            logger.strJobID = JobID;
            logger.strJOBNAME = "PBBAHO001";
            logger.dtRunDate = DateTime.Now;
            #endregion

            try
            {
                #region 取得系統資訊(前次批次日、本日批次日)
                CMCSYS001 SYSINF = new CMCSYS001();
                SYSINF.strJobID = JobID;
                SYSINF.strJobName = "PBBAHO001";
                String SYSINF_RC = SYSINF.getSYSINF();
                PREV_PROCESS_DTE = SYSINF.datetimePREV_PROCESS_DTE;
                TODAY_PROCESS_DTE = SYSINF.datetimeTODAY_PROCESS_DTE;
                NEXT_PROCESS_DTE = SYSINF.datetimeBANK_NEXT_PROCESS_DTE;
                temp_yyymmdd = Convert.ToString(Convert.ToInt32(TODAY_PROCESS_DTE.ToString("yyyy")) - 1911) + TODAY_PROCESS_DTE.ToString("MMdd");
                #endregion

                #region 讀取公共事業參數取得該類別參數設定 (KEY:TRANSFER_TYPE)
                SETUP_PUBLIC.init();

                #region 讀參數檔
                //條件
                SETUP_PUBLIC.strWhereFILE_TRANSFER_TYPE = strSOURCE;
                SETUP_PUBLIC.strWhereFILE_FORMAT = "ACH";
                //執行
                SETUP_PUBLIC_RC = SETUP_PUBLIC.query();
                switch (SETUP_PUBLIC_RC)
                {
                    case "S0000": //查詢成功
                        SETUP_PUBLIC_Query_Count = SETUP_PUBLIC.resultTable.Rows.Count;
                        break;

                    case "F0023": //無需處理資料
                        SETUP_PUBLIC_Query_Count = 0;
                        break;

                    default: //資料庫錯誤
                        logger.strJobQueue = "查詢SETUP_PUBLIC-ACH參數設定有誤:" + SETUP_PUBLIC_RC;
                        logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                        return "B0016:" + logger.strJobQueue;
                }
                #endregion

                //非設定之公用事業參數則結束處理程序
                if (SETUP_PUBLIC_Query_Count == 0)
                {
                    logger.strJobQueue = "公用事業參數檔中無設定需報送ACH格式的公用事業單位，不執行。";
                    logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                    return "B0000:" + logger.strJobQueue;
                }
                else if (SETUP_PUBLIC_Query_Count == 1)
                {
                    //卡號轉換
                    strPayCardTrans = SETUP_PUBLIC.resultTable.Rows[0]["PAY_CARD_TRANS"].ToString();
                    //檔案名稱
                    strFileName = SETUP_PUBLIC.resultTable.Rows[0]["TRANSFER_TYPE"].ToString();
                    strRecNAME = SETUP_PUBLIC.resultTable.Rows[0]["FILE_FORMAT"].ToString();
                    //扣繳類別
                    strPayType = SETUP_PUBLIC.resultTable.Rows[0]["PAY_TYPE"].ToString();
                    //ACH檔案參數
                    strSEND_UNIT = SETUP_PUBLIC.resultTable.Rows[0]["SEND_UNIT"].ToString();
                    strRECV_UNIT = SETUP_PUBLIC.resultTable.Rows[0]["RECV_UNIT"].ToString();
                    strTRANSFER_UNIT = SETUP_PUBLIC.resultTable.Rows[0]["TRANSFER_UNIT"].ToString();
                }
                else
                {
                    logger.strJobQueue = "公用事業參數檔中無設定需報送ACH格式的公用事業單位，不執行。";
                    logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                    return "B0016:" + logger.strJobQueue;
                }
                #endregion

                #region 宣告檔案路徑
                Cybersoft.Base.CMCURL001 CMCURL = new CMCURL001();
                //XML名稱
                strXML_Layout = CMCURL.getURL("PUBLIC_ACH.xml");

                switch (strSOURCE.ToString().Trim())
                {
                    case "ACTIVE":      //主動行
                        //收取檔案路徑
                        FILE_PATH = CMCURL.getPATH("ACH_STMTOUT_ACTIVE");
                        QUERY_PAY_TYPE = "0031";
                        MNT_USER = "PBBAHO001A";
                        REPORT_TITLE = "ACH代扣繳成功失敗報表_主動行";
                        REPORT_NAME = "PBRACH001(ACH代扣繳成功失敗報表_主動行)";
                        break;

                    case "PASSIVE":     //被動行
                        //收取檔案路徑
                        FILE_PATH = CMCURL.getPATH("ACH_STMTOUT_PASSIVE");
                        QUERY_PAY_TYPE = "'0005','0007','0009','0010','0011','0012','0013','0014','0015','0017','0018','0024','0025','0027','0028','0032','0033','0034','0036'";
                        MNT_USER = "PBBAHO001P";
                        REPORT_TITLE = "ACH代扣繳成功失敗報表_被動行";
                        REPORT_NAME = "PBRACH001(ACH代扣繳成功失敗報表_被動行)";
                        break;

                    default:
                        logger.strJobQueue = "扣繳來源有誤, 請確認!! " + strSOURCE.ToString().Trim();
                        logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                        return "B0099:" + logger.strJobQueue;
                }

                if (strXML_Layout == "" || FILE_PATH == "")
                {
                    logger.strJobQueue = "路徑取得錯誤!!! PUBLIC_ACH.xml路徑為 " + strXML_Layout + " ACH_STMTOUT路徑為 " + FILE_PATH;
                    logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                    return "B0099:" + logger.strJobQueue;
                }
                else
                {
                    FILE_PATH = FILE_PATH.Replace("yyyymmdd", TODAY_PROCESS_DTE.ToString("yyyyMMdd"));
                    logger.strJobQueue = "PUBLIC_ACH.xml路徑為 " + strXML_Layout + " ACH_STMTOUT路徑為 " + FILE_PATH;
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

                #region 刪除TX_WAREHOUSE,RERUN 機制
                TX_WAREHOUSE.DateTimeWhereMNT_DT = TODAY_PROCESS_DTE;
                TX_WAREHOUSE.strWhereMNT_USER = MNT_USER;
                TX_WAREHOUSE_RC = TX_WAREHOUSE.delete();
                logger.strJobQueue = "刪除TX_WAREHOUSE 筆數 " + TX_WAREHOUSE.intDelCnt;
                logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                #endregion

                #region 擷取今日需送回應檔之ACH資料
                PUBLIC_LIST.init();
                PUBLIC_LIST_RC = PUBLIC_LIST.query_for_ACH(QUERY_PAY_TYPE, TODAY_PROCESS_DTE);
                switch (PUBLIC_LIST_RC)
                {
                    case "S0000": //查詢成功
                        PUBLIC_LIST_Query_Count = PUBLIC_LIST.resultTable.Rows.Count;
                        ACH_STMTOUT = new string[PUBLIC_LIST_Query_Count];
                        break;

                    case "F0023": //無需處理資料
                        logger.strJobQueue = "今日無ACH回應檔需產生;今日需送回應檔時間為大於 " + PREV_PROCESS_DTE + "且小於等於" + TODAY_PROCESS_DTE;
                        logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                        return "B0000";

                    default: //資料庫錯誤
                        logger.strJobQueue = "查詢PUBLIC_LIST 資料錯誤:" + PUBLIC_LIST_RC;
                        logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                        return "B0016：" + logger.strJobQueue;
                }
                #endregion

                MAIN_ROUTINE();

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

        #region MAIN_ROUTINE()
        void MAIN_ROUTINE()
        {

            #region 載入XML
            FileParseByXml xml = new FileParseByXml();

            //REC Layout
            DataTable REC_DataTable = xml.Xml2DataTable(strXML_Layout, "REC");

            if (xml.strMSG.Length > 0)
            {
                logger.strJobQueue = "[Xml2DataTable(REC)] - " + xml.strMSG.ToString().Trim();
                logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                throw new System.Exception("B0099：" + logger.strJobQueue);
            }

            #endregion

            for (i = 0; i < PUBLIC_LIST.resultTable.Rows.Count; i++)
            {
                PAY_DATA_AREA = Convert.ToString(PUBLIC_LIST.resultTable.Rows[i]["PAY_DATA_AREA"]);
                DataTable InfData_DataTable = new DataTable();
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
                        throw new System.Exception("F0023：" + logger.strJobQueue);

                    default: //資料庫錯誤
                        logger.strJobQueue = "查詢PUBLIC_HIST 資料錯誤:" + PUBLIC_HIST_RC;
                        logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                        throw new System.Exception("B0016：" + logger.strJobQueue);
                }

                #endregion

                #region 依 Layout 拆解資料
                InfData_DataTable = xml.FileLine2DataTable(BIG5, PAY_DATA_AREA, REC_DataTable);

                if (xml.strMSG.Length > 0)
                {
                    logger.strJobQueue = "[FileLine2DataTable(REC)] (L" + i + 1 + ") - " + xml.strMSG.ToString().Trim();
                    logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                    throw new System.Exception("B0099：" + logger.strJobQueue);
                }

                PAY_TYPE = Convert.ToString(InfData_DataTable.Rows[0]["CID"]).Substring(0, 4);
                SOURCE_CODE = "PU0000" + PAY_TYPE;
                BILL_DTE = Convert.ToString(InfData_DataTable.Rows[0]["PDATE"]).Substring(1, 5);

                #endregion

                #region 組明細及ACH回應碼
                //回應中心代碼說明 2:內部檢核有誤 8:授權失敗 9:處理成功

                //O000~O009都表示成功
                //if (PAY_RESULT == "S000" || PAY_RESULT == "O000" || PAY_RESULT == "O001" || PAY_RESULT == "O002" || PAY_RESULT == "O003" || PAY_RESULT == "O004" || PAY_RESULT == "O005" || PAY_RESULT == "O006" || PAY_RESULT == "O007" || PAY_RESULT == "O008" || PAY_RESULT == "O009")
                //20110915調整回應碼
                if (PAY_RESULT == "S000")
                {
                    PAY_RESULT_ACH = "00";  //扣帳成功
                    ACH_SUCCESS_CNT = ACH_SUCCESS_CNT + 1;
                    ACH_SUCCESS_AMT = ACH_SUCCESS_AMT + TX_AMT;
                    ACH_STMTOUT[i] = PAY_DATA_AREA.Substring(0, 64) + PAY_RESULT_ACH + PAY_DATA_AREA.Substring(66, 94);

                    #region 取得該SOURCE_CODE對應的中文說明
                    SETUP_CODE.init();
                    SETUP_CODE.strWhereTYPE = "TX_SOURCE";
                    SETUP_CODE.strWhereCODE = SOURCE_CODE;
                    SETUP_CODE_RC = SETUP_CODE.query();
                    switch (SETUP_CODE_RC)
                    {
                        case "S0000": //查詢成功
                            ACH_DESC = SETUP_CODE.resultTable.Rows[0]["DESCR"].ToString().Trim();
                            break;

                        case "F0023": //查無資料
                            ACH_DESC = "ACH代扣款";
                            logger.strJobQueue = "查無對應TX_SOURCE, CODE = " + SOURCE_CODE;
                            logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                            break;

                        default: //資料庫錯誤
                            logger.strJobQueue = "查詢SETUP_CODE 資料錯誤:" + SETUP_CODE_RC;
                            logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                            throw new System.Exception("B0016：" + logger.strJobQueue);
                    }
                    #endregion

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
                        TX_WAREHOUSE_T.resultTable.Rows[x]["SOURCE_CODE"] = SOURCE_CODE;
                        TX_WAREHOUSE_T.resultTable.Rows[x]["MCC_CODE"] = "4900";
                        TX_WAREHOUSE_T.resultTable.Rows[x]["AUTH_CODE"] = AUTH_CODE;
                        TX_WAREHOUSE_T.resultTable.Rows[x]["SEQ"] = x.ToString().PadLeft(10, '0');
                        //偷偷把用戶編號遮住
                        //ACH_MASK_STRING = CMCNBR001.GET_MASK(PAY_NBR, MASK_TYPE);
                        //TX_WAREHOUSE_T.resultTable.Rows[x]["DESCR"] = Strings.StrConv(ACH_DESC + ACH_MASK_STRING + " " + BILL_DTE.Substring(0, 3) + "年" + BILL_DTE.Substring(3, 2) + "月", VbStrConv.Wide);
                        TX_WAREHOUSE_T.resultTable.Rows[x]["DESCR"] = Strings.StrConv(ACH_DESC + PAY_NBR, VbStrConv.Wide);
                        TX_WAREHOUSE_T.resultTable.Rows[x]["MT_TYPE"] = "D";
                        TX_WAREHOUSE_T.resultTable.Rows[x]["AMT"] = TX_AMT;
                        TX_WAREHOUSE_T.resultTable.Rows[x]["MNT_DT"] = TODAY_PROCESS_DTE;
                        TX_WAREHOUSE_T.resultTable.Rows[x]["MNT_USER"] = MNT_USER;
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
                    PUBLIC_HIST.strMNT_USER = "PBBAHO001";
                    PUBLIC_HIST_RC = PUBLIC_HIST.update();
                    switch (PUBLIC_HIST_RC)
                    {
                        case "S0000": //查詢成功
                            PUBLIC_HIST_Update_Count = PUBLIC_HIST_Update_Count + PUBLIC_HIST.intUptCnt;
                            break;

                        default: //資料庫錯誤
                            logger.strJobQueue = "異動 PUBLIC_HIST.PAY_RESULT(" + PAY_SEQ + ") 資料錯誤:" + PUBLIC_HIST_RC ;
                            logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                            throw new System.Exception("B0016：" + logger.strJobQueue);
                    }
                    #endregion

                    continue;

                }
                //I001: 未申請
                if (PAY_RESULT == "I001")
                {
                    PAY_RESULT_ACH = "02";   //未申請代扣
                    ACH_FAIL_CNT = ACH_FAIL_CNT + 1;
                    ACH_FAIL_AMT = ACH_FAIL_AMT + TX_AMT;
                    ACH_STMTOUT[i] = PAY_DATA_AREA.Substring(0, 64) + PAY_RESULT_ACH + PAY_DATA_AREA.Substring(66, 94);
                    //寫出成功/失敗明細報表
                    insertReport_TABLE();
                    continue;
                }
                //I002: 已終止 I003無有效卡
                if (PAY_RESULT == "I002" || PAY_RESULT == "I003")
                {
                    PAY_RESULT_ACH = "03";   //已終止代扣
                    ACH_FAIL_CNT = ACH_FAIL_CNT + 1;
                    ACH_FAIL_AMT = ACH_FAIL_AMT + TX_AMT;
                    ACH_STMTOUT[i] = PAY_DATA_AREA.Substring(0, 64) + PAY_RESULT_ACH + PAY_DATA_AREA.Substring(66, 94);
                    //寫出成功/失敗明細報表
                    insertReport_TABLE();
                    continue;
                }
                //I004: 卡片未開卡
                if (PAY_RESULT == "I004")
                {
                    PAY_RESULT_ACH = "09";   //未開卡
                    ACH_FAIL_CNT = ACH_FAIL_CNT + 1;
                    ACH_FAIL_AMT = ACH_FAIL_AMT + TX_AMT;
                    ACH_STMTOUT[i] = PAY_DATA_AREA.Substring(0, 64) + PAY_RESULT_ACH + PAY_DATA_AREA.Substring(66, 94);
                    //寫出成功/失敗明細報表
                    insertReport_TABLE();

                    continue;
                }
                //I006: 信用額度不足
                if (PAY_RESULT == "I006")
                {
                    PAY_RESULT_ACH = "08";   //信用額度不足
                    ACH_FAIL_CNT = ACH_FAIL_CNT + 1;
                    ACH_FAIL_AMT = ACH_FAIL_AMT + TX_AMT;
                    ACH_STMTOUT[i] = PAY_DATA_AREA.Substring(0, 64) + PAY_RESULT_ACH + PAY_DATA_AREA.Substring(66, 94);
                    //寫出成功/失敗明細報表
                    insertReport_TABLE();

                    continue;
                }
                //I008: 公用事業單位止扣
                if (PAY_RESULT == "I008")
                {
                    PAY_RESULT_ACH = CTL_CODE;   //公用事業單位止扣
                    ACH_FAIL_CNT = ACH_FAIL_CNT + 1;
                    ACH_FAIL_AMT = ACH_FAIL_AMT + TX_AMT;
                    ACH_STMTOUT[i] = PAY_DATA_AREA.Substring(0, 64) + PAY_RESULT_ACH + PAY_DATA_AREA.Substring(66, 94);
                    //寫出成功/失敗明細報表
                    insertReport_TABLE();
                    continue;
                }
                //AXXX:授權失敗
                if (PAY_RESULT != "S000")
                {
                    PAY_RESULT_ACH = "99";  //其他錯誤
                    ACH_FAIL_CNT = ACH_FAIL_CNT + 1;
                    ACH_FAIL_AMT = ACH_FAIL_AMT + TX_AMT;
                    ACH_STMTOUT[i] = PAY_DATA_AREA.Substring(0, 64) + PAY_RESULT_ACH + PAY_DATA_AREA.Substring(66, 94);
                    //寫出成功/失敗明細報表
                    insertReport_TABLE();
                    continue;
                }

                #endregion

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
                    srOutFile.Write(ACH_STMTOUT[k]);
                    srOutFile.Write("\r\n");
                    srOutFile.Flush();
                    ACH_TXT_Count = ACH_TXT_Count + 1;
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
                throw new System.Exception("B0012：" + logger.strJobQueue);
            }

            logger.strJobQueue = "整批新增至 TX_WAREHOUSE 成功筆數 =" + TX_WAREHOUSE_T.intInsCnt;
            logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);

            #endregion
        }
        #endregion

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
            Decimal ACH_SUCCESS_FEE = 0;

            ACH_TOTAL_CNT = ACH_SUCCESS_CNT + ACH_FAIL_CNT;
            ACH_TOTAL_AMT = ACH_SUCCESS_AMT + ACH_FAIL_AMT;

            ACH_SUCCESS_FEE = Convert.ToDecimal(ACH_SUCCESS_CNT * 3);

            //設定特殊欄位資料
            ArrayList alSumData = new ArrayList();
            alSumData.Add(new ExcelFactory.ListItem("#REPORT_NAME", REPORT_TITLE));
            alSumData.Add(new ExcelFactory.ListItem("#RPT_DATE", TODAY_PROCESS_DTE.ToString("yyyy/MM/dd")));
            alSumData.Add(new ExcelFactory.ListItem("#TOTAL_CNT", ACH_TOTAL_CNT + " 筆"));
            alSumData.Add(new ExcelFactory.ListItem("#TOTAL_AMT", ACH_TOTAL_AMT));
            alSumData.Add(new ExcelFactory.ListItem("#SUCCESS_CNT", ACH_SUCCESS_CNT + " 筆"));
            alSumData.Add(new ExcelFactory.ListItem("#SUCCESS_AMT", ACH_SUCCESS_AMT));
            alSumData.Add(new ExcelFactory.ListItem("#FAIL_CNT", ACH_FAIL_CNT + " 筆"));
            alSumData.Add(new ExcelFactory.ListItem("#FAIL_AMT", ACH_FAIL_AMT));
            //alSumData.Add(new ExcelFactory.ListItem("#SUCCESS_FEE", ACH_SUCCESS_FEE.ToString("0")));
            //設定合併欄位資料
            ArrayList alMegData = new ArrayList();

            //產出報表
            CMCRPT001.Output(REPORT_TABLE, alSumData, alMegData, REPORT_NAME, "PBRACH001", "Sheet1", "Sheet1", TODAY_PROCESS_DTE, true);
        }
        #endregion

        #region Display
        void writeDisplay()
        {
            logger.strJobID = "00002";
            //擷取ACH需回應資料
            logger.strTBL_NAME = "PUBLIC_LIST";
            logger.strTBL_ACCESS = "Q";
            logger.intTBL_COUNT = PUBLIC_LIST_Query_Count;
            logger.writeCounter();

            //ACH回應檔筆數(含頭尾)
            logger.strTBL_NAME = "ACH_STMTOUT";
            logger.strTBL_ACCESS = "I";
            logger.intTBL_COUNT = ACH_TXT_Count;
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

