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
    /// 產生停車費(台北市、台北縣、台南市、高雄市)扣帳回應檔,並將入帳交易先寫至TX_WAREHOUSE,待下一營業日入帳
    /// </summary>
    public class PBBPKO001
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
        DateTime TODAY_PROCESS_DTE = new DateTime();
        DateTime NEXT_PROCESS_DTE = new DateTime();
        DateTime NEXT_WORKING_DTE = new DateTime();
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
        int CAR_TOTAL_CNT = 0;
        Decimal CAR_TOTAL_AMT = 0;
        int TX_WAREHOUSE_T_Count_Insert = 0;
        int CAR_TXT_Count = 0;
        string PAY_TYPE = "";
        string SOURCE_CODE = "";
        string CAR_DESC = "";
        String REPORT_NAME = "";
        string TEMP_PROCESS_YYYMMDD = "";

        //PUBLIC_LIST欄位
        String PAY_DATA_AREA = "";
        String BILL_DTE = "";
        Decimal TX_AMT = 0;
        String NAME = "";
        String CUST_ID = "";
        String CAR_BILL_NBR = "";

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

        //停車費回應檔欄位
        String[] CAR_STMTOUT = null;
        String PAY_RESULT_CAR = "";
        Decimal CAR_SUCCESS_AMT = 0;
        Decimal CAR_FAIL_AMT = 0;
        int CAR_SUCCESS_CNT = 0;
        int CAR_FAIL_CNT = 0;
        String TEMP_SUCCESS_AMT = "";
        String TEMP_FAIL_AMT = "";

        //台電遮罩欄位
        //String MASK_TYPE = "00***000000000000000";  //第3~5碼躲起來

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
        public string RUN(String strSOURCE)
        {
            #region 準備MSGLG.SYSOUT CLASS
            logger.strJobID = JobID;
            logger.strJOBNAME = "PBBPKO001";
            logger.dtRunDate = DateTime.Now;
            #endregion

            try
            {
                #region 取得系統資訊(前次批次日、本日批次日)
                CMCSYS001 SYSINF = new CMCSYS001();
                SYSINF.strJobID = JobID;
                SYSINF.strJobName = "PBBPKO001";
                String SYSINF_RC = SYSINF.getSYSINF();
                TODAY_PROCESS_DTE = SYSINF.datetimeTODAY_PROCESS_DTE;
                NEXT_PROCESS_DTE = SYSINF.datetimeNEXT_PROCESS_DTE;
                NEXT_WORKING_DTE = SYSINF.datetimeBANK_NEXT_PROCESS_DTE;
                //PROCESS_DATE = DateTime.Now;
                TEMP_PROCESS_YYYMMDD = Convert.ToString(Convert.ToInt32(TODAY_PROCESS_DTE.ToString("yyyy")) - 1911) + TODAY_PROCESS_DTE.ToString("MMdd");
                strToday = TODAY_PROCESS_DTE.ToString("yyyyMMdd");
                #endregion

                #region 宣告檔案路徑
                Cybersoft.Base.CMCURL001 CMCURL = new CMCURL001();
                //XML名稱
                strXML_Layout = CMCURL.getURL("PUBLIC_PARKING.xml");

                switch (strSOURCE.ToString().Trim())
                {
                    case "TPE":     //台北市停車費
                        //收取檔案路徑
                        FILE_PATH = CMCURL.getPATH("TPE_STMTOUT");
                        PAY_TYPE = "0023";
                        SOURCE_CODE = "PU00000023";
                        CAR_DESC = "台北市停車費";
                        REPORT_NAME = "PBRTPE001(台北市停車費扣繳成功失敗報表)";
                        break;

                    case "TPC":     //台北縣停車費
                        //收取檔案路徑
                        FILE_PATH = CMCURL.getPATH("TPC_STMTOUT");
                        PAY_TYPE = "0021";
                        SOURCE_CODE = "PU00000021";
                        CAR_DESC = "台北縣停車費";
                        REPORT_NAME = "PBRTPC001(台北縣停車費扣繳成功失敗報表)";
                        break;

                    case "PRK":     //台南市停車費
                        //收取檔案路徑
                        FILE_PATH = CMCURL.getPATH("PRK_STMTOUT");
                        PAY_TYPE = "0006";
                        SOURCE_CODE = "PU00000006";
                        CAR_DESC = "台南市停車費";
                        REPORT_NAME = "PBRPRK001(台南市停車費扣繳成功失敗報表)";
                        break;

                    case "KAO":     //高雄市停車費
                        //收取檔案路徑
                        FILE_PATH = CMCURL.getPATH("KAO_STMTOUT");
                        PAY_TYPE = "0022";
                        SOURCE_CODE = "PU00000022";
                        CAR_DESC = "高雄市停車費";
                        REPORT_NAME = "PBRKAO001(高雄市停車費扣繳成功失敗報表)";
                        break;

                    default:
                        strXML_Layout = "";
                        FILE_PATH = "";
                        break;
                }

                if (strXML_Layout == "" || FILE_PATH == "")
                {
                    logger.strJobQueue = "路徑取得錯誤!!! PUBLIC_CAR.xml路徑為 " + strXML_Layout + " CAR_STMTOUT路徑為 " + FILE_PATH;
                    logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                    return "B0099:" + logger.strJobQueue;
                }
                else
                {
                    FILE_PATH = FILE_PATH.Replace("yyyymmdd", TODAY_PROCESS_DTE.ToString("yyyyMMdd"));
                    logger.strJobQueue = "PUBLIC_CAR.xml路徑為 " + strXML_Layout + " CAR_STMTOUT路徑為 " + FILE_PATH;
                    logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                }
                #endregion

                #region 定義報表Table
                REPORT_TABLE.Columns.Add("RPT_SEQ", typeof(string));
                REPORT_TABLE.Columns.Add("PAY_NBR", typeof(string));
                REPORT_TABLE.Columns.Add("PAY_CARD_NBR", typeof(string));
                REPORT_TABLE.Columns.Add("CUST_ID", typeof(string));
                REPORT_TABLE.Columns.Add("NAME", typeof(string));
                REPORT_TABLE.Columns.Add("PAY_AMT", typeof(decimal));
                REPORT_TABLE.Columns.Add("PAY_DTE", typeof(string));
                REPORT_TABLE.Columns.Add("PAY_RESULT", typeof(string));
                #endregion

                MAIN_ROUTINE(strSOURCE);

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

        #region MAIN_ROUTINE
        void MAIN_ROUTINE(String strSOURCE)
        {
            #region 複製TX_WAREHOUSE_T的Table定義
            TX_WAREHOUSE_T.table_define();
            #endregion

            #region 擷取今日需送回應檔之停車費資料
            PUBLIC_LIST.init();
            PUBLIC_LIST.strWherePAY_TYPE = PAY_TYPE;
            PUBLIC_LIST_RC = PUBLIC_LIST.query_for_out(strToday, "");
            switch (PUBLIC_LIST_RC)
            {
                case "S0000": //查詢成功
                    PUBLIC_LIST_Query_Count = PUBLIC_LIST.resultTable.Rows.Count;
                    CAR_STMTOUT = new string[PUBLIC_LIST_Query_Count];
                    break;

                case "F0023": //無需處理資料
                    logger.strJobQueue = "今日無停車費回應檔需產生;今日需送回應檔時間為大於 " + TODAY_PROCESS_DTE + "且小於等於" + NEXT_PROCESS_DTE;
                    logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                    break;

                default: //資料庫錯誤
                    logger.strJobQueue = "查詢PUBLIC_LIST 資料錯誤:" + PUBLIC_LIST_RC;
                    logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                    throw new System.Exception("B0016：" + logger.strJobQueue);
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
                throw new System.Exception("B0099：" + logger.strJobQueue);
            }

            //REC Layout
            DataTable REC_DataTable = xml.Xml2DataTable(strXML_Layout, "REC");

            if (xml.strMSG.Length > 0)
            {
                logger.strJobQueue = "[Xml2DataTable(REC)] - " + xml.strMSG.ToString().Trim();
                logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                throw new System.Exception("B0099：" + logger.strJobQueue);
            }

            //REC_T Layout
            DataTable REC_T_DataTable = xml.Xml2DataTable(strXML_Layout, "REC_T");

            if (xml.strMSG.Length > 0)
            {
                logger.strJobQueue = "[Xml2DataTable(REC_T)] - " + xml.strMSG.ToString().Trim();
                logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                throw new System.Exception("B0099：" + logger.strJobQueue);
            }
            #endregion

            for (i = 0; i < PUBLIC_LIST_Query_Count; i++)
            {

                PAY_DATA_AREA = Convert.ToString(PUBLIC_LIST.resultTable.Rows[i]["PAY_DATA_AREA"]);
                DataTable InfData_DataTable = new DataTable();
                string strTag = PAY_DATA_AREA.Substring(0, 1).ToString().Trim();
                switch (strTag)
                {
                    case "1":  //檔頭資料       
                        intREC_HCount++;

                        #region  組檔頭
                        CAR_STMTOUT[i] = PAY_DATA_AREA.Substring(0, 39) + TEMP_PROCESS_YYYMMDD + "009" + new string(' ', 81);
                        #endregion

                        #region 刪除TX_WAREHOUSE,RERUN 機制
                        TX_WAREHOUSE.init();
                        TX_WAREHOUSE.DateTimeWhereEFF_DTE = PAY_DTE;
                        TX_WAREHOUSE.strWhereSOURCE_CODE = SOURCE_CODE;
                        TX_WAREHOUSE.DateTimeWhereMNT_DT = TODAY_PROCESS_DTE;
                        TX_WAREHOUSE.strWhereMNT_USER = "PBBPKO001";
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
                                CUST_ID = Convert.ToString(PUBLIC_HIST.resultTable.Rows[0]["CUST_ID"]);
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
                        CUST_ID = InfData_DataTable.Rows[0]["IDCARD"].ToString().Trim();
                        CAR_BILL_NBR = InfData_DataTable.Rows[0]["CAR_BILL_NBR"].ToString().Trim();
                        #endregion

                        #region 組明細及停車費回應碼

                        //S000 表示成功
                        if (PAY_RESULT == "S000")
                        {
                            PAY_RESULT_CAR = "00";  //扣帳成功
                            CAR_SUCCESS_CNT = CAR_SUCCESS_CNT + 1;
                            CAR_SUCCESS_AMT = CAR_SUCCESS_AMT + TX_AMT;
                            CAR_STMTOUT[i] = PAY_DATA_AREA.Substring(0, 50) + new string(' ', 1) + PAY_DATA_AREA.Substring(77, 13)
                                           + PAY_RESULT_CAR + PAY_DATA_AREA.Substring(93, 37) + new string(' ', 25);

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
                                TX_WAREHOUSE_T.resultTable.Rows[x]["EFF_DTE"] = NEXT_WORKING_DTE;
                                TX_WAREHOUSE_T.resultTable.Rows[x]["POSTING_DTE"] = PAY_DTE;
                                TX_WAREHOUSE_T.resultTable.Rows[x]["ARN"] = PAY_TYPE + PAY_NBR;
                                TX_WAREHOUSE_T.resultTable.Rows[x]["CODE"] = "0001";
                                TX_WAREHOUSE_T.resultTable.Rows[x]["SOURCE_CODE"] = SOURCE_CODE;
                                TX_WAREHOUSE_T.resultTable.Rows[x]["MCC_CODE"] = "4900";
                                TX_WAREHOUSE_T.resultTable.Rows[x]["AUTH_CODE"] = AUTH_CODE;
                                TX_WAREHOUSE_T.resultTable.Rows[x]["SEQ"] = x.ToString().PadLeft(10, '0');
                                //偷偷把停車費號碼遮住
                                //CAR_MASK_STRING = CMCNBR001.GET_MASK(PAY_NBR, MASK_TYPE);
                                BILL_DTE = Convert.ToString(InfData_DataTable.Rows[0]["BILL_DTE"]);
                                //TX_WAREHOUSE_T.resultTable.Rows[x]["DESCR"] = CAR_DESC + "代扣繳" + CAR_MASK_STRING + " " + BILL_DTE.Substring(0, 3) + "年" + BILL_DTE.Substring(3, 2) + "月";
                                TX_WAREHOUSE_T.resultTable.Rows[x]["DESCR"] = Strings.StrConv(CAR_DESC + PAY_NBR + CAR_BILL_NBR, VbStrConv.Wide);
                                TX_WAREHOUSE_T.resultTable.Rows[x]["MT_TYPE"] = "D";
                                TX_WAREHOUSE_T.resultTable.Rows[x]["AMT"] = TX_AMT;
                                TX_WAREHOUSE_T.resultTable.Rows[x]["MNT_DT"] = TODAY_PROCESS_DTE;
                                TX_WAREHOUSE_T.resultTable.Rows[x]["MNT_USER"] = "PBBPKO001";
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
                            PUBLIC_HIST.strMNT_USER = "PBBPKO001";
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
                        //I003 無有效卡
                        if (PAY_RESULT == "I003")
                        {
                            PAY_RESULT_CAR = "06";   //帳號已結清銷戶
                            CAR_FAIL_CNT = CAR_FAIL_CNT + 1;
                            CAR_FAIL_AMT = CAR_FAIL_AMT + TX_AMT;
                            CAR_STMTOUT[i] = PAY_DATA_AREA.Substring(0, 50) + new string(' ', 1) + PAY_DATA_AREA.Substring(77, 13)
                                           + PAY_RESULT_CAR + PAY_DATA_AREA.Substring(93, 37) + new string(' ', 25);

                            //寫出成功/失敗明細報表
                            insertReport_TABLE();

                            break;
                        }
                        //I005 代扣卡號不存在主檔中
                        if (PAY_RESULT == "I005")
                        {
                            PAY_RESULT_CAR = "05";   //無此帳號
                            CAR_FAIL_CNT = CAR_FAIL_CNT + 1;
                            CAR_FAIL_AMT = CAR_FAIL_AMT + TX_AMT;
                            CAR_STMTOUT[i] = PAY_DATA_AREA.Substring(0, 50) + new string(' ', 1) + PAY_DATA_AREA.Substring(77, 13)
                                           + PAY_RESULT_CAR + PAY_DATA_AREA.Substring(93, 37) + new string(' ', 25);

                            //寫出成功/失敗明細報表
                            insertReport_TABLE();

                            break;
                        }
                        //I006 代扣卡號不存在主檔中
                        if (PAY_RESULT == "I006")
                        {
                            PAY_RESULT_CAR = "09";   //信用額度不足
                            CAR_FAIL_CNT = CAR_FAIL_CNT + 1;
                            CAR_FAIL_AMT = CAR_FAIL_AMT + TX_AMT;
                            CAR_STMTOUT[i] = PAY_DATA_AREA.Substring(0, 50) + new string(' ', 1) + PAY_DATA_AREA.Substring(77, 13)
                                           + PAY_RESULT_CAR + PAY_DATA_AREA.Substring(93, 37) + new string(' ', 25);

                            //寫出成功/失敗明細報表
                            insertReport_TABLE();

                            break;
                        }
                        //其它錯誤
                        if (PAY_RESULT != "S000")
                        {
                            PAY_RESULT_CAR = "08";  //其他錯誤
                            CAR_FAIL_CNT = CAR_FAIL_CNT + 1;
                            CAR_FAIL_AMT = CAR_FAIL_AMT + TX_AMT;
                            CAR_STMTOUT[i] = PAY_DATA_AREA.Substring(0, 50) + new string(' ', 1) + PAY_DATA_AREA.Substring(77, 13)
                                           + PAY_RESULT_CAR + PAY_DATA_AREA.Substring(93, 37) + new string(' ', 25);

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
                            throw new System.Exception("B0099：" + logger.strJobQueue);
                        }
                        TOTAL_AMT = Convert.ToDecimal(InfData_DataTable.Rows[0]["TOTAL_AMT"]);
                        TOTAL_CNT = Convert.ToDecimal(InfData_DataTable.Rows[0]["TOTAL_CNT"]);

                        CAR_TOTAL_AMT = CAR_FAIL_AMT + CAR_SUCCESS_AMT;
                        CAR_TOTAL_CNT = CAR_FAIL_CNT + CAR_SUCCESS_CNT;

                        //檢查金額
                        if (TOTAL_AMT != CAR_TOTAL_AMT)
                        {
                            logger.strJobQueue = "回應檔總金額和原始檔不合,請確認!!!";
                            logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                            logger.strJobQueue = "原始檔總金額 = " + TOTAL_AMT + "回應檔總金額 = " + CAR_TOTAL_AMT;
                            logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                            throw new System.Exception("B0099：" + logger.strJobQueue);
                        }

                        //檢查筆數
                        if (TOTAL_CNT != CAR_TOTAL_CNT)
                        {
                            logger.strJobQueue = "回應檔總筆數和原始檔不合,請確認!!!";
                            logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                            logger.strJobQueue = "原始檔總筆數 = " + TOTAL_CNT + "回應檔總筆數 = " + CAR_TOTAL_CNT;
                            logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                            throw new System.Exception("B0099：" + logger.strJobQueue);
                        }

                        #endregion

                        #region 組檔尾
                        TEMP_SUCCESS_AMT = Convert.ToInt32(CAR_SUCCESS_AMT).ToString().PadLeft(13, '0');
                        TEMP_FAIL_AMT = Convert.ToInt32(CAR_FAIL_AMT).ToString().PadLeft(13, '0');

                        CAR_STMTOUT[i] = '3' + TEMP_SUCCESS_AMT + CAR_SUCCESS_CNT.ToString().PadLeft(8, '0')
                                       + TEMP_FAIL_AMT + CAR_FAIL_CNT.ToString().PadLeft(8, '0') + new string(' ', 87);
                        #endregion

                        break;

                    default: //資料庫錯誤
                        throw new System.Exception("B0016：" + "停車費代扣繳 REC_TYPE ERROR " + strTag + logger.strJobQueue);

                }
            }

            #region 產生回應檔 陣列 --> STMTOUT.TXT

            //設定產出檔案名稱                
            if (PUBLIC_LIST_Query_Count > 0)
            {
                strOutFileName = FILE_PATH;
                FileStream fsOutFile = new FileStream(strOutFileName, FileMode.Create, FileAccess.Write, FileShare.None);
                using (StreamWriter srOutFile = new StreamWriter(fsOutFile, BIG5))
                {
                    //逐筆寫出資料
                    for (int k = 0; k < PUBLIC_LIST.resultTable.Rows.Count; k++)
                    {
                        srOutFile.Write(CAR_STMTOUT[k]);
                        srOutFile.Write("\r\n");
                        srOutFile.Flush();
                        CAR_TXT_Count = CAR_TXT_Count + 1;
                    }
                    srOutFile.Close();
                }
                fsOutFile.Close();
            }


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
            REPORT_TABLE.Rows[k]["PAY_CARD_NBR"] = PAY_CARD_NBR;
            REPORT_TABLE.Rows[k]["CUST_ID"] = CUST_ID;
            REPORT_TABLE.Rows[k]["NAME"] = NAME;
            REPORT_TABLE.Rows[k]["PAY_AMT"] = TX_AMT;
            REPORT_TABLE.Rows[k]["PAY_DTE"] = TODAY_PROCESS_DTE.ToString("yyyy/MM/dd");

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

            //設定特殊欄位資料
            ArrayList alSumData = new ArrayList();
            alSumData.Add(new ExcelFactory.ListItem("#REPORT_NAME", CAR_DESC + "扣繳成功/失敗報表"));
            alSumData.Add(new ExcelFactory.ListItem("#RPT_DATE", TODAY_PROCESS_DTE.ToString("yyyy/MM/dd")));
            alSumData.Add(new ExcelFactory.ListItem("#TOTAL_CNT", CAR_TOTAL_CNT + " 筆"));
            alSumData.Add(new ExcelFactory.ListItem("#TOTAL_AMT", CAR_TOTAL_AMT));
            alSumData.Add(new ExcelFactory.ListItem("#SUCCESS_CNT", CAR_SUCCESS_CNT + " 筆"));
            alSumData.Add(new ExcelFactory.ListItem("#SUCCESS_AMT", CAR_SUCCESS_AMT));
            alSumData.Add(new ExcelFactory.ListItem("#FAIL_CNT", CAR_FAIL_CNT + " 筆"));
            alSumData.Add(new ExcelFactory.ListItem("#FAIL_AMT", CAR_FAIL_AMT));
            //設定合併欄位資料
            ArrayList alMegData = new ArrayList();

            //產出報表
            CMCRPT001.Output(REPORT_TABLE, alSumData, alMegData, REPORT_NAME, "PBRCAR001", "Sheet1", "Sheet1", TODAY_PROCESS_DTE, true);
        }
        #endregion

        #region Display
        void writeDisplay()
        {
            logger.strJobID = "00002";
            //擷取停車費需回應資料
            logger.strTBL_NAME = "PUBLIC_LIST";
            logger.strTBL_ACCESS = "Q";
            logger.intTBL_COUNT = PUBLIC_LIST_Query_Count;
            logger.writeCounter();

            //停車費回應檔筆數(含頭尾)
            logger.strTBL_NAME = "CAR_STMTOUT";
            logger.strTBL_ACCESS = "I";
            logger.intTBL_COUNT = CAR_TXT_Count;
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

