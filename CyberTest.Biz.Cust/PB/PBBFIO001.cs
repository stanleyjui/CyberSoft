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
    /// 產生扣帳回應檔財金格式,並將入帳交易先寫至TX_WAREHOUSE,待下一營業日入帳
    /// 產出檔名：
    /// 省水：01010YYYMMDD[s]_R
    /// 台電：01020YYYMMDD[s]_R
    /// 省水：01120YYYMMDD[s]_R

    /// </summary>
    public class PBBFIO001
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
        //宣告SETUP_PUBLIC
        String SETUP_PUBLIC_RC = "";
        SETUP_PUBLICDao SETUP_PUBLIC = new SETUP_PUBLICDao();

        //宣告SETUP_GL
        String SETUP_GL_RC = "";
        SETUP_GLDao SETUP_GL = new SETUP_GLDao();

        //宣告SETUP_REJECT
        String SETUP_REJECT_RC = "";
        SETUP_REJECTDao SETUP_REJECT = new SETUP_REJECTDao();
        DataTable SETUP_REJECT_DataTable = new DataTable();

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
        String FISC_MASK_STRING = "";
        #endregion

        #region 宣告共用變數

        //批次日期
        DateTime PREV_PROCESS_DTE = new DateTime();
        DateTime TODAY_PROCESS_DTE = new DateTime();
        DateTime NEXT_PROCESS_DTE = new DateTime();
        
        //table
        DataTable FT_xml_DataTable = new DataTable();
        DataTable FD_xml_DataTable = new DataTable();
        DataTable CHANGE_AREA_DataTable = new DataTable();
        //
        string strRecNAME = "";
        string strBank_name = "";
        string strUSER_CHAR_2 = "";
        string strPay_Type = "";
        string strMT_TYPE = "";
        String strMASK_TYPE = "";
        String strPay_range = "";
        decimal decFeeAmt = 0;
        string strSheet = "";
        string strPAY_RESULT_DESCR = "";
        string strCharge_dte = "";
        string strOLD_PAY_NBR = "";
        string strData_Type = "";
        string strError_code = "";
        string strOrig_Charge_dte = "";
        string temp_yyymmdd = "";
        string strFlag = "";

        //筆數&金額
        int PUBLIC_LIST_Query_Count = 0;
        int PUBLIC_HIST_Update_Count = 0;
        int i = 0;
        int x = 0; //控制TX_WAREHOUSE_T
        int k = 0; //REPORT_TABLE
        int intREC_HCount = 0;
        int intRECCount = 0;
        int intREC_TCount = 0;
        int FISC_TOTAL_CNT = 0;
        Decimal FISC_TOTAL_AMT = 0;
        int TX_WAREHOUSE_T_Count_Insert = 0;
        int FISC_TXT_Count = 0;
        string temp_mmdd = "";      //檔名
        string strSOURCE_CODE = "";
        string strxml_NODE = "";
        //PUBLIC_LIST欄位
        String PAY_DATA_AREA = "";
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
        String temp_PAY_DTE = "";   //扣款日轉為民國年月日
        DateTime dtTX_EFF_DTE = new DateTime(1900, 01, 01);
        //回應檔欄位
        String FISC_H_STMTOUT = "";
        String FISC_T_STMTOUT = "";
        String[] FISC_STMTOUT = null;
        String strDescr = "";
        String PAY_RESULT_FISC = "";
        Decimal FISC_SUCCESS_AMT = 0;
        Decimal FISC_FAIL_AMT = 0;
        int FISC_SUCCESS_CNT = 0;
        int FISC_FAIL_CNT = 0;
        String TEMP_SUCCESS_AMT = "";
        String TEMP_FAIL_AMT = "";
        String strTX_CODE = "";
        int SETUP_PUBLIC_Query_Count = 0;
        string strJobname = "PBBFIO001";
        string strToday = "";
        #endregion

        #region 宣告檔案路徑

        //XML放置路徑 
        String strXmlURL = "";
        //寫出檔案名稱
        String strOutFileName = "";
        //寫出檔案路徑
        String FILE_PATH = "";
       

        #endregion

        static readonly Cybersoft.Coca.Log.Logger logger = Cybersoft.Log.LogManager<Cybersoft.Coca.Log.Logger>.GetLogger(System.Reflection.MethodInfo.GetCurrentMethod().ReflectedType.Name);
        //========================================================================================================
        #region 程式主邏輯【MAIN Routine】
        public string RUN(string strTran_Type, string BATCH_NO)
        {
            #region 準備MSGLG.SYSOUT CLASS
            logger.strJobID = JobID;
            logger.strJOBNAME = strJobname;
            logger.dtRunDate = DateTime.Now;
            #endregion

            try
            {
                #region 取得系統資訊(前次批次日、本日批次日)
                CMCSYS001 SYSINF = new CMCSYS001();
                SYSINF.strJobID = JobID;
                SYSINF.strJobName = strJobname;
                String SYSINF_RC = SYSINF.getSYSINF();
                PREV_PROCESS_DTE = SYSINF.datetimePREV_PROCESS_DTE;
                TODAY_PROCESS_DTE = SYSINF.datetimeTODAY_PROCESS_DTE;
                NEXT_PROCESS_DTE = SYSINF.datetimeBANK_NEXT_PROCESS_DTE;
                temp_mmdd = TODAY_PROCESS_DTE.ToString("MMdd");
                strBank_name = SYSINF.strREPORT_TITLE;
                strToday = TODAY_PROCESS_DTE.ToString("yyyyMMdd");
                temp_yyymmdd = (Convert.ToString(Convert.ToInt64(TODAY_PROCESS_DTE.ToString("yyyy")) - 1911) + TODAY_PROCESS_DTE.ToString("MMdd")).PadLeft(7, '0');
                #endregion

                switch (strTran_Type)
                {
                    case "01010":  //省水
                        strUSER_CHAR_2 = strTran_Type;
                        strMT_TYPE = "D";
                        strTX_CODE = "0001";
                        strxml_NODE = "CHANGE_AREA_01010";
                        strSheet = "Sheet1";
                        define_REPORT_TABLE_sheet1();
                        break;
                    case "01020":  //台電
                        strUSER_CHAR_2 = strTran_Type;
                        strMT_TYPE = "D";
                        strTX_CODE = "0001";
                        strxml_NODE = "CHANGE_AREA_01020";
                        strSheet = "Sheet2";
                        define_REPORT_TABLE_sheet2();
                        break;
                    case "01120":  //市水
                        strUSER_CHAR_2 = strTran_Type;
                        strMT_TYPE = "D";
                        strTX_CODE = "0001";
                        strxml_NODE = "CHANGE_AREA_01120";
                        strSheet = "Sheet3";
                        define_REPORT_TABLE_sheet3();
                        break;
                    case "02120":  //市水退款
                        strUSER_CHAR_2 = "01120";
                        strMT_TYPE = "C";
                        strTX_CODE = "0002";
                        strxml_NODE = "CHANGE_AREA_02120";
                        strSheet = "Sheet4";
                        define_REPORT_TABLE_sheet4();
                        break;
                }
                #region 補執行例外處理     
                BATCH_NO = BATCH_NO.ToUpper();

                if (BATCH_NO.Length > 1 &&
                     "T".Equals(BATCH_NO.Substring(BATCH_NO.Length - 1, 1)))    //補執行時，批號最後須加"T"。ex."1T"
                {
                    strFlag = "T";
                    strJobname = strJobname + "T";
                    dtTX_EFF_DTE = TODAY_PROCESS_DTE;    //當天入帳
                }
                else
                {
                    dtTX_EFF_DTE = NEXT_PROCESS_DTE;     //隔天入帳
                }
                #endregion
                
                #region 擷取公共事業單位參數檔
                SETUP_PUBLIC.init();
                //SETUP_PUBLIC.strWhereUSER_CHAR_1 = "FISC";
                //SETUP_PUBLIC.strWhereUSER_CHAR_2 = strUSER_CHAR_2;
                SETUP_PUBLIC.strWhereFILE_FORMAT = "FISC";
                SETUP_PUBLIC.strWherePOST_RESULT = "00";
                SETUP_PUBLIC.strWhereFILE_TRANSFER_TYPE = strUSER_CHAR_2;
                SETUP_PUBLIC_RC = SETUP_PUBLIC.query();
                switch (SETUP_PUBLIC_RC)
                {
                    case "S0000": //查詢成功
                        SETUP_PUBLIC_Query_Count = SETUP_PUBLIC.resultTable.Rows.Count;
                        if (SETUP_PUBLIC_Query_Count == 1)
                        {
                            strSOURCE_CODE = SETUP_PUBLIC.resultTable.Rows[0]["SOURCE_CODE"].ToString();
                            strPay_Type = SETUP_PUBLIC.resultTable.Rows[0]["PAY_TYPE"].ToString();
                            strDescr = SETUP_PUBLIC.resultTable.Rows[0]["DESCR"].ToString();
                            strMASK_TYPE = SETUP_PUBLIC.resultTable.Rows[0]["PAY_NBR_DIS_BYTE"].ToString();  //遮罩欄位
                            strRecNAME = SETUP_PUBLIC.resultTable.Rows[0]["FILE_FORMAT"].ToString().Trim();
                            strTran_Type = SETUP_PUBLIC.resultTable.Rows[0]["TRANSFER_TYPE"].ToString().Trim();
                        }
                        else
                        {
                            logger.strJobQueue = "公共事業單位類別參數 = " + strTran_Type + "設定筆數超過1筆";
                            logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                            return "B0016";
                        }
                        break;

                    case "F0023": //無需處理資料
                        logger.strJobQueue = "查無此公共事業單位類別的參數：轉帳類別 = " + strTran_Type;
                        logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                        return "B0000";

                    default: //資料庫錯誤
                        logger.strJobQueue = "查詢SETUP_PUBLIC 資料錯誤:" + SETUP_PUBLIC_RC;
                        logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                        return "B0016:" + logger.strJobQueue;
                }
                #endregion

                #region 擷取公共事業單位手續費成本
                SETUP_GL.init();
                SETUP_GL.strWhereCODE = strTX_CODE;
                SETUP_GL.strWhereSOURCE_CODE = strSOURCE_CODE;
                SETUP_GL.strWhereMT_TYPE = strMT_TYPE;
                SETUP_GL_RC = SETUP_GL.query();
                switch (SETUP_GL_RC)
                {
                    case "S0000": //查詢成功
                        decFeeAmt = Convert.ToDecimal(SETUP_GL.resultTable.Rows[0]["COST_AMT"]);
                        break;

                    case "F0023": //無需處理資料
                        logger.strJobQueue = "查無此公共事業單位類別的參數：轉帳類別 = " + strTran_Type;
                        logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                        return "B0000";

                    default: //資料庫錯誤
                        logger.strJobQueue = "查詢SETUP_GL 資料錯誤:" + SETUP_GL_RC;
                        logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                        return "B0016:" + logger.strJobQueue;
                }
                #endregion

                #region 撈出PUBLIC的拒絕代碼
                SETUP_REJECT.init();
                SETUP_REJECT.strWhereREJECT_GROUP = "PUBLIC";
                SETUP_REJECT_RC = SETUP_REJECT.query();
                switch (SETUP_REJECT_RC)
                {
                    case "S0000": //查詢成功
                        SETUP_REJECT_DataTable = SETUP_REJECT.resultTable;
                        break;

                    case "F0023": //無需處理資料
                        logger.strJobQueue = "查無PUBLIC 拒絕代碼資料。";
                        logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                        break;

                    default: //資料庫錯誤
                        logger.strJobQueue = "查詢SETUP_REJECT 資料錯誤:" + SETUP_REJECT_RC;
                        logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                        return "B0016:" + logger.strJobQueue;
                }
                #endregion

                if (strRecNAME != "")
                {
                    #region 宣告檔案路徑
                    Cybersoft.Base.CMCURL001 CMCURL = new CMCURL001();

                    //檔案格式 strXmlURL
                    strXmlURL = CMCURL.getURL("FISC_PMT_CARD2BANK");
                    if (strXmlURL == "")
                    {
                        logger.strJobQueue = "格式取得錯誤!!! FISC_PMT_CARD2BANK <URL> 未設定";
                        logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                        return "B0099:" + logger.strJobQueue;
                    }

                    //檔案路徑 strDestPATH
                    string strFilePath = CMCURL.getPATH("FISC_PMT_CARD2BANK");
                    if (strFilePath == "")
                    {
                        logger.strJobQueue = "路徑取得錯誤!!! FISC_PMT_CARD2BANK <PATH> 未設定";
                        logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                        return "B0099:" + logger.strJobQueue;
                    }
                    //檔案名稱 strFileName
                    string strFileName = CMCURL.getFILE_NAME("FISC_PMT_CARD2BANK");
                    if (strFileName == "")
                    {
                        logger.strJobQueue = "檔名取得錯誤!!! FISC_PMT_CARD2BANK <FILE_NAME> 未設定";
                        logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                        return "B0099:" + logger.strJobQueue;
                    }

                    //附檔名
                    String strEXT = CMCURL.getEXT("FISC_PMT_CARD2BANK");
                    if (strEXT != "")
                    {
                        strFileName = strFileName + strEXT;
                    }
                    FILE_PATH = strFilePath + strTran_Type + strFileName;  //扣款檔命名規則
                    FILE_PATH = CMCURL.ReplaceVarDateFormat(FILE_PATH, TODAY_PROCESS_DTE);
                    #endregion
                }
                else
                {
                    logger.strJobQueue = "參數檔未設定檔案格式 : 機構代碼(" + strPay_Type + ") 格式名稱(" + strRecNAME + ")";
                    logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                    return "B0099:" + logger.strJobQueue;
                }

                #region 載入檔案格式資訊
                FileParseByXml xml = new FileParseByXml();
                //BODY
                #region BATCHTX02(FD) Layout (轉入BATCHTX02.xml的FD格式)
                FD_xml_DataTable = xml.Xml2DataTable(strXmlURL, "BATCHTX02");
                if (xml.strMSG.Length > 0)
                {
                    logger.strJobQueue = "[Xml2DataTable(" + strRecNAME + ")] - " + xml.strMSG.ToString().Trim();
                    logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                    return "B0099:" + logger.strJobQueue;
                }
                #endregion
                //TRAILER
                #region BATCHTX02(FT) Layout (轉入BATCHTX02.xml的FT格式)
                FT_xml_DataTable = xml.Xml2DataTable(strXmlURL, "BATCHTX02_T");
                if (xml.strMSG.Length > 0)
                {
                    logger.strJobQueue = "[Xml2DataTable(" + strRecNAME + "T)] - " + xml.strMSG.ToString().Trim();
                    logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                    return "B0099:" + logger.strJobQueue;
                }
                #endregion
                //CHANGE_AREA異動資料區
                #region CHANGE_AREA異動資料區
                CHANGE_AREA_DataTable = xml.Xml2DataTable(strXmlURL, strxml_NODE);

                if (xml.strMSG.Length > 0)
                {
                    logger.strJobQueue = "[Xml2DataTable(" + strxml_NODE + ")] - " + xml.strMSG.ToString().Trim();
                    logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                    return "B0099:" + logger.strJobQueue;
                }
                #endregion
                #endregion

                #region 複製TX_WAREHOUSE_T的Table定義
                TX_WAREHOUSE_T.table_define();
                #endregion

                #region 擷取今日需送回應檔之資料
                PUBLIC_LIST.init();
                PUBLIC_LIST.strWherePAY_TYPE = strPay_Type;

                PUBLIC_LIST_RC = PUBLIC_LIST.query_for_out(strToday, strFlag);
                switch (PUBLIC_LIST_RC)
                {
                    case "S0000": //查詢成功
                        PUBLIC_LIST_Query_Count = PUBLIC_LIST.resultTable.Rows.Count;
                        FISC_STMTOUT = new string[PUBLIC_LIST_Query_Count];
                        break;

                    case "F0023": //無需處理資料
                        logger.strJobQueue = "今日無回應檔需產生;今日需送回應檔時間為大於 " + PREV_PROCESS_DTE + "且小於等於" + TODAY_PROCESS_DTE;
                        logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                        return "B0000";

                    default: //資料庫錯誤
                        logger.strJobQueue = "查詢PUBLIC_LIST 資料錯誤:" + PUBLIC_LIST_RC;
                        logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                        return "B0016:" + logger.strJobQueue;
                }
                #endregion

                for (i = 0; i < PUBLIC_LIST.resultTable.Rows.Count; i++)
                {
                    PAY_DATA_AREA = Convert.ToString(PUBLIC_LIST.resultTable.Rows[i]["PAY_DATA_AREA"]);
                    DataTable InfData_DataTable = new DataTable();
                    string strTag = PAY_DATA_AREA.Substring(0, 1).ToString().Trim();
                    switch (strTag)
                    {
                        case "1":
                            intREC_HCount++;

                            #region  組結果檔檔頭
                            FISC_H_STMTOUT = PAY_DATA_AREA.Substring(0, 29) + "2" + PAY_DATA_AREA.Substring(30, 2) + string.Empty.PadRight(78, ' ');
                            #endregion

                            #region 刪除TX_WAREHOUSE,RERUN 機制
                            TX_WAREHOUSE.init();
                            TX_WAREHOUSE.strWhereSOURCE_CODE = strSOURCE_CODE;
                            TX_WAREHOUSE.DateTimeWhereMNT_DT = TODAY_PROCESS_DTE;
                            TX_WAREHOUSE.strWhereMNT_USER = strJobname;
                            TX_WAREHOUSE_RC = TX_WAREHOUSE.delete();
                            logger.strJobQueue = "刪除TX_WAREHOUSE 筆數 " + TX_WAREHOUSE.intDelCnt;
                            logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                            #endregion
                            break;
                        case "2":
                            //處理Detail資料
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
                                    if ((PUBLIC_HIST.resultTable.Rows[0]["CHANGE_NBR_NEW"]).ToString() == "")
                                    {
                                        PAY_NBR = Convert.ToString(PUBLIC_HIST.resultTable.Rows[0]["PAY_NBR"]);
                                    }
                                    else
                                    {
                                        PAY_NBR = Convert.ToString(PUBLIC_HIST.resultTable.Rows[0]["CHANGE_NBR_NEW"]);  //取得換號後的新號碼
                                    }
                                    
                                    PAY_SEQ = Convert.ToString(PUBLIC_HIST.resultTable.Rows[0]["PAY_SEQ"]);
                                    PAY_DTE = Convert.ToDateTime(PUBLIC_HIST.resultTable.Rows[0]["PAY_DTE"]);
                                    //if (strTran_Type != "01020") //台電
                                    //{
                                    //    temp_PAY_DTE = Convert.ToString(Convert.ToInt32(PAY_DTE.ToString("yyyy")) - 1911) + PAY_DTE.ToString("MMdd");
                                    //}
                                    //else
                                    //{
                                    //    temp_PAY_DTE = "";
                                    //}
                                    temp_PAY_DTE = Convert.ToString(Convert.ToInt32(PAY_DTE.ToString("yyyy")) - 1911) + PAY_DTE.ToString("MMdd");

                                    PAY_RESULT = Convert.ToString(PUBLIC_HIST.resultTable.Rows[0]["PAY_RESULT"]);
                                    AUTH_CODE = Convert.ToString(PUBLIC_HIST.resultTable.Rows[0]["AUTH_CODE"]);
                                    TX_AMT = Convert.ToDecimal(PUBLIC_HIST.resultTable.Rows[0]["PAY_AMT"]);
                                    NAME = Convert.ToString(PUBLIC_HIST.resultTable.Rows[0]["NAME"]);
                                    CTL_CODE = Convert.ToString(PUBLIC_HIST.resultTable.Rows[0]["CTL_CODE"]);
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
                            #region 依 Layout 拆解資料_明細，入扣帳日期
                            InfData_DataTable = xml.FileLine2DataTable(BIG5, PAY_DATA_AREA, FD_xml_DataTable);
                            if (xml.strMSG.Length > 0)
                            {
                                logger.strJobQueue = "[FileLine2DataTable(REC)] (L" + i + 1 + ") - " + xml.strMSG.ToString().Trim();
                                logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                                return "B0099:" + logger.strJobQueue;
                            }
                            #endregion 
                            //BILL_DTE = Convert.ToString(InfData_DataTable.Rows[0]["CHARGE_DTE"]);
                            #region 依 Layout 拆解資料_異動資料區
                            string strChange_aera = InfData_DataTable.Rows[0]["CHANGE_AREA"].ToString().PadRight(21,' ');
                            DataTable CHANGE_AREAData_DataTable = xml.FileLine2DataTable(BIG5, strChange_aera, CHANGE_AREA_DataTable);
                            if (xml.strMSG.Length > 0)
                            {
                                logger.strJobQueue = "[FileLine2DataTable(CHANGE_AREA)] (L" + k + ") - " + xml.strMSG.ToString().Trim();
                                logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                                throw new System.Exception("B0099:" + logger.strJobQueue);
                            }
                            #endregion
                            switch (strTran_Type)
                            {
                                case "01010":  //省水
                                    strPay_range = (CHANGE_AREAData_DataTable.Rows[0]["PAY_RANGE"].ToString().Trim().PadLeft(5, '0')).Substring(0, 3)
                                                    + "/"
                                                    + (CHANGE_AREAData_DataTable.Rows[0]["PAY_RANGE"].ToString().Trim().PadLeft(5, '0')).Substring(3, 2);
                                    break;
                                case "01020":  //台電
                                    strPay_range = (CHANGE_AREAData_DataTable.Rows[0]["CHARGE_DTE"].ToString().Trim().PadLeft(6, '0')).Substring(0, 2)
                                                   + "/"
                                                   + (CHANGE_AREAData_DataTable.Rows[0]["CHARGE_DTE"].ToString().Trim().PadLeft(6, '0')).Substring(2, 2)
                                                   +"/"
                                                  + (CHANGE_AREAData_DataTable.Rows[0]["CHARGE_DTE"].ToString().Trim().PadLeft(6, '0')).Substring(4, 2);
                                    strCharge_dte = CHANGE_AREAData_DataTable.Rows[0]["CHARGE_DTE"].ToString();
                                    strOLD_PAY_NBR = CHANGE_AREAData_DataTable.Rows[0]["OLD_PAY_NBR"].ToString();
                                    if (strOLD_PAY_NBR != "")
                                    {
                                        strOLD_PAY_NBR = PAY_NBR.Substring(0, 2) + strOLD_PAY_NBR;
                                    }
                                    break;
                                case "01120":  //市水
                                    strPay_range = (CHANGE_AREAData_DataTable.Rows[0]["PAY_RANGE"].ToString().Trim().PadLeft(8, '0')).Substring(0, 3)
                                                    + "/"
                                                    + (CHANGE_AREAData_DataTable.Rows[0]["PAY_RANGE"].ToString().Trim().PadLeft(8, '0')).Substring(3, 5);
                                    strCharge_dte = CHANGE_AREAData_DataTable.Rows[0]["CHARGE_DTE"].ToString();
                                    strData_Type = CHANGE_AREAData_DataTable.Rows[0]["DATA_TYPE"].ToString();
                                    break;
                                case "02120":  //市水退款
                                    strPay_range = (CHANGE_AREAData_DataTable.Rows[0]["PAY_RANGE"].ToString().Trim().PadLeft(5, '0')).Substring(0, 3)
                                                    + "/"
                                                    + (CHANGE_AREAData_DataTable.Rows[0]["PAY_RANGE"].ToString().Trim().PadLeft(5, '0')).Substring(3, 2);
                                    strOrig_Charge_dte = CHANGE_AREAData_DataTable.Rows[0]["ORIG_CHARGE_DTE"].ToString();
                                    strError_code = CHANGE_AREAData_DataTable.Rows[0]["ERROR_CODE"].ToString();
                                    break;
                            }
                            #endregion

                            #region 組明細及回應碼
                            if (PAY_RESULT == "S000" || PAY_RESULT =="0000" )
                            {
                                PAY_RESULT_FISC = "4001";
                                FISC_SUCCESS_CNT = FISC_SUCCESS_CNT + 1;
                                FISC_SUCCESS_AMT = FISC_SUCCESS_AMT + TX_AMT;

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
                                    TX_WAREHOUSE_T.resultTable.Rows[x]["EFF_DTE"] = dtTX_EFF_DTE;        //補執行時此為當日，其他為隔日
                                    TX_WAREHOUSE_T.resultTable.Rows[x]["POSTING_DTE"] = PAY_DTE;
                                    TX_WAREHOUSE_T.resultTable.Rows[x]["ARN"] = PAY_NBR;
                                    TX_WAREHOUSE_T.resultTable.Rows[x]["ORIG_DTE"] = PAY_DTE;            //實際交易日=指定扣帳日
                                    TX_WAREHOUSE_T.resultTable.Rows[x]["SOURCE_CODE"] = strSOURCE_CODE;
                                    TX_WAREHOUSE_T.resultTable.Rows[x]["MCC_CODE"] = "4900";
                                    TX_WAREHOUSE_T.resultTable.Rows[x]["AUTH_CODE"] = AUTH_CODE;
                                    //補執行時key會與當日DUP，以SEQ來區分
                                    if (strFlag == "T")        
                                    {
                                        TX_WAREHOUSE_T.resultTable.Rows[x]["SEQ"] = "1" + x.ToString().PadLeft(9, '0');
                                    }
                                    else
                                    {
                                        TX_WAREHOUSE_T.resultTable.Rows[x]["SEQ"] = x.ToString().PadLeft(10, '0');
                                    }
                                    //TX_WAREHOUSE_T.resultTable.Rows[x]["SEQ"] = "1" + x.ToString().PadLeft(9, '0');
                                    //部分遮蓋
                                    FISC_MASK_STRING = CMCNBR001.GET_MASK(PAY_NBR, strMASK_TYPE.Substring(0, PAY_NBR.Length));
                                    //轉全形
                                    TX_WAREHOUSE_T.resultTable.Rows[x]["DESCR"] = Strings.StrConv(strDescr + FISC_MASK_STRING + "_" + strPay_range, VbStrConv.Wide);
                                    TX_WAREHOUSE_T.resultTable.Rows[x]["CODE"] = strTX_CODE;
                                    TX_WAREHOUSE_T.resultTable.Rows[x]["MT_TYPE"] = strMT_TYPE;
                                    TX_WAREHOUSE_T.resultTable.Rows[x]["AMT"] = TX_AMT;
                                    TX_WAREHOUSE_T.resultTable.Rows[x]["MNT_DT"] = TODAY_PROCESS_DTE;
                                    TX_WAREHOUSE_T.resultTable.Rows[x]["MNT_USER"] = strJobname;
                                }
                                #endregion

                                #region 異動結果代碼(S000->0000)
                                PUBLIC_HIST.init();
                                PUBLIC_HIST.strWhereBU = BU;
                                PUBLIC_HIST.strWhereACCT_NBR = ACCT_NBR;
                                //PUBLIC_HIST.strWherePAY_NBR = PAY_NBR;
                                PUBLIC_HIST.strWherePAY_SEQ = PAY_SEQ;
                                PUBLIC_HIST.strWhereAUTH_CODE = AUTH_CODE;
                                PUBLIC_HIST.strWherePAY_RESULT = "S000";
                                if (strOLD_PAY_NBR != "")
                                {
                                    PUBLIC_HIST.strWherePAY_NBR = strOLD_PAY_NBR;
                                }
                                else
                                {
                                    PUBLIC_HIST.strWherePAY_NBR = PAY_NBR;
                                }
                                PUBLIC_HIST.strPAY_RESULT = "0000";
                                PUBLIC_HIST.strMNT_USER = strJobname;
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
                            }
                            else
                            {
                                #region 處理結果代碼轉換為財金處理結果代碼
                                switch (PAY_RESULT)
                                {
                                    //I001: 未申請
                                    case "I001":
                                        PAY_RESULT_FISC = "4508";   //未申請代扣
                                        break;
                                    //I002: 已終止
                                    case "I002":
                                        PAY_RESULT_FISC = "4508";   //中止代繳
                                        break;
                                    //I003無有效卡  I007: 卡片已到期
                                    case "I003":
                                    case "I007":
                                        PAY_RESULT_FISC = "4508";   //已終止代扣
                                        break;
                                    //I004: 卡片未開卡
                                    case "I004":
                                        PAY_RESULT_FISC = "4405";   //未開卡
                                        break;
                                    //卡片不存在
                                    case "I005":
                                        PAY_RESULT_FISC = "4808";   //卡片不存在
                                        break;
                                    //信用額度不足
                                    case "I006":
                                        PAY_RESULT_FISC = "4405";   //信用額度不足
                                        break;
                                    //公共事業單位通知，此次暫停扣款
                                    case "I008":
                                        PAY_RESULT_FISC = "4705";   //剔除不轉帳
                                        break;
                                    default:
                                        PAY_RESULT_FISC = "2999";  //其他
                                        break;
                                }
                                #endregion
                                FISC_FAIL_CNT = FISC_FAIL_CNT + 1;
                                FISC_FAIL_AMT = FISC_FAIL_AMT + TX_AMT;
                            }
                            //組出回饋檔明細
                            FISC_STMTOUT[i - 1] = PAY_DATA_AREA.Substring(0, 52) + PAY_DATA_AREA.Substring(60, 23) +
                                                      PAY_RESULT_FISC + string.Empty.PadRight(20, ' ') + temp_PAY_DTE.PadRight(11, ' ');
                            //寫出成功/失敗明細報表
                            insertReport_TABLE(strTran_Type);

                            #endregion
                            break;
                        case "3":
                            intREC_TCount++;

                            #region 依 Layout 拆解原來扣款檔資料
                            InfData_DataTable = xml.FileLine2DataTable(BIG5, PAY_DATA_AREA, FT_xml_DataTable);
                            if (xml.strMSG.Length > 0)
                            {
                                logger.strJobQueue = "[FileLine2DataTable(REC_T)] (L" + i + 1 + ") - " + xml.strMSG.ToString().Trim();
                                logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                                return "B0099:" + logger.strJobQueue;
                            }
                            TOTAL_AMT = Convert.ToDecimal(InfData_DataTable.Rows[0]["TOT_AMT"]);
                            TOTAL_CNT = Convert.ToDecimal(InfData_DataTable.Rows[0]["TOT_CNT"]);

                            FISC_TOTAL_AMT = FISC_FAIL_AMT + FISC_SUCCESS_AMT;
                            FISC_TOTAL_CNT = FISC_FAIL_CNT + FISC_SUCCESS_CNT;

                            //檢查金額
                            if (TOTAL_AMT != FISC_TOTAL_AMT)
                            {
                                logger.strJobQueue = "回應檔總金額和原始檔不合,請確認!!!";
                                logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                                logger.strJobQueue = "原始檔總金額 = " + TOTAL_AMT + "回應檔總金額 = " + FISC_TOTAL_AMT;
                                logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                                return "B0099:" + logger.strJobQueue;
                            }

                            //檢查筆數
                            if (TOTAL_CNT != FISC_TOTAL_CNT)
                            {
                                logger.strJobQueue = "回應檔總筆數和原始檔不合,請確認!!!";
                                logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                                logger.strJobQueue = "原始檔總筆數 = " + TOTAL_CNT + "回應檔總筆數 = " + FISC_TOTAL_CNT;
                                logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                                return "B0099:" + logger.strJobQueue;
                            }

                            #endregion

                            #region 組檔尾
                            TEMP_SUCCESS_AMT = Convert.ToInt32(FISC_SUCCESS_AMT * 100).ToString().PadLeft(15, '0');
                            TEMP_FAIL_AMT = Convert.ToInt32(FISC_FAIL_AMT * 100).ToString().PadLeft(15, '0');

                            FISC_T_STMTOUT  = PAY_DATA_AREA.Substring(0, 54) + 
                                              TEMP_SUCCESS_AMT + FISC_SUCCESS_CNT.ToString().PadLeft(10, '0') +
                                              TEMP_FAIL_AMT + FISC_FAIL_CNT.ToString().PadLeft(10, '0') + 
                                              string.Empty.PadRight(6,' ');
                            #endregion
                            break;
                     }
                }

                #region 產生扣款回應檔txt

                //設定產出檔案名稱                
                //strOutFileName = FILE_PATH + strTran_Type +TODAY_PROCESS_DTE.ToString("yyyyMMdd")+"1"+"_R";                
                strOutFileName = FILE_PATH;
                FileStream fsOutFile = new FileStream(strOutFileName, FileMode.Create, FileAccess.Write, FileShare.None);
                using (StreamWriter srOutFile = new StreamWriter(fsOutFile, BIG5))
                {
                    //1.先寫FH
                    srOutFile.Write(FISC_H_STMTOUT);
                    srOutFile.Write("\r\n");
                    srOutFile.Flush();

                    //2.逐筆寫出FD資料
                    for (int k = 0; k < intRECCount; k++)
                    {
                        srOutFile.Write(FISC_STMTOUT[k]);
                        srOutFile.Write("\r\n");
                        srOutFile.Flush();
                        FISC_TXT_Count = FISC_TXT_Count + 1;
                    }

                    //3.最後寫FT
                    srOutFile.Write(FISC_T_STMTOUT);
                    srOutFile.Write("\r\n");
                    srOutFile.Flush();

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

                #region 寫出報表
                writeReport();
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
       
        #region 定義REPORT_TABLE
        void define_REPORT_TABLE_sheet1()  //台灣自來水公司
        {
            REPORT_TABLE.Columns.Add("RPT_SEQ", typeof(string));
            REPORT_TABLE.Columns.Add("PAY_NBR", typeof(string));
            REPORT_TABLE.Columns.Add("PAY_YYMM", typeof(string));
            REPORT_TABLE.Columns.Add("PAY_CARD_NBR", typeof(string));
            REPORT_TABLE.Columns.Add("NAME", typeof(string));
            REPORT_TABLE.Columns.Add("PAY_AMT", typeof(decimal));
            REPORT_TABLE.Columns.Add("PAY_DTE", typeof(string));
            REPORT_TABLE.Columns.Add("PAY_RESULT", typeof(string));
        }
        void define_REPORT_TABLE_sheet2()  //台灣電力公司
        {
            REPORT_TABLE.Columns.Add("RPT_SEQ", typeof(string));
            REPORT_TABLE.Columns.Add("PAY_NBR", typeof(string));
            REPORT_TABLE.Columns.Add("OLD_PAY_NBR", typeof(string));
            REPORT_TABLE.Columns.Add("PAY_YYMM", typeof(string));
            REPORT_TABLE.Columns.Add("PAY_CARD_NBR", typeof(string));
            REPORT_TABLE.Columns.Add("NAME", typeof(string));
            REPORT_TABLE.Columns.Add("PAY_AMT", typeof(decimal));
            REPORT_TABLE.Columns.Add("PAY_DTE", typeof(string));
            REPORT_TABLE.Columns.Add("PAY_RESULT", typeof(string));
        }
        void define_REPORT_TABLE_sheet3()  //台北市自來水公司
        {
            REPORT_TABLE.Columns.Add("RPT_SEQ", typeof(string));
            REPORT_TABLE.Columns.Add("PAY_NBR", typeof(string));
            REPORT_TABLE.Columns.Add("PAY_YYMM", typeof(string));
            REPORT_TABLE.Columns.Add("PAY_CARD_NBR", typeof(string));
            REPORT_TABLE.Columns.Add("NAME", typeof(string));
            REPORT_TABLE.Columns.Add("PAY_AMT", typeof(decimal));
            REPORT_TABLE.Columns.Add("PAY_DTE", typeof(string));
            REPORT_TABLE.Columns.Add("PAY_RESULT", typeof(string));
            REPORT_TABLE.Columns.Add("CHARGE_DTE", typeof(string));
            REPORT_TABLE.Columns.Add("DATA_TYPE", typeof(string));
        }
        void define_REPORT_TABLE_sheet4()  //台北市自來水公司-代退
        {
            REPORT_TABLE.Columns.Add("RPT_SEQ", typeof(string));
            REPORT_TABLE.Columns.Add("PAY_NBR", typeof(string));
            REPORT_TABLE.Columns.Add("PAY_YYMM", typeof(string));
            REPORT_TABLE.Columns.Add("PAY_CARD_NBR", typeof(string));
            REPORT_TABLE.Columns.Add("NAME", typeof(string));
            REPORT_TABLE.Columns.Add("PAY_AMT", typeof(decimal));
            REPORT_TABLE.Columns.Add("PAY_DTE", typeof(string));
            REPORT_TABLE.Columns.Add("PAY_RESULT", typeof(string));
            REPORT_TABLE.Columns.Add("ORIG_CHARGE_DTE", typeof(string));
            REPORT_TABLE.Columns.Add("ERROR_CODE", typeof(string));
        }
        #endregion

        #region insertReport_TABLE
        void insertReport_TABLE(string strTran_Type)
        {
            REPORT_TABLE.Rows.Add();
            k = REPORT_TABLE.Rows.Count - 1;
            REPORT_TABLE.Rows[k]["RPT_SEQ"] = REPORT_TABLE.Rows.Count.ToString().PadLeft(7, '0');
            REPORT_TABLE.Rows[k]["PAY_NBR"] = PAY_NBR;
            REPORT_TABLE.Rows[k]["PAY_YYMM"] = strPay_range;
            REPORT_TABLE.Rows[k]["PAY_CARD_NBR"] = PAY_CARD_NBR;
            REPORT_TABLE.Rows[k]["NAME"] = NAME;
            REPORT_TABLE.Rows[k]["PAY_AMT"] = TX_AMT;
            REPORT_TABLE.Rows[k]["PAY_DTE"] = PAY_DTE.ToString("yyyy/MM/dd");
            PAY_RESULT_Descr(PAY_RESULT);
            REPORT_TABLE.Rows[k]["PAY_RESULT"] = strPAY_RESULT_DESCR;
            switch (strTran_Type)
            {
                case "01010":  //省水
                    break;
                case "01020":  //台電
                    REPORT_TABLE.Rows[k]["OLD_PAY_NBR"] = strOLD_PAY_NBR;
                    break;
                case "01120":  //市水
                    REPORT_TABLE.Rows[k]["CHARGE_DTE"] = strCharge_dte;
                    REPORT_TABLE.Rows[k]["DATA_TYPE"] = strData_Type;
                    break;
                case "02120":  //市水退款
                    REPORT_TABLE.Rows[k]["ERROR_CODE"] = strError_code;
                    REPORT_TABLE.Rows[k]["ORIG_CHARGE_DTE"] = strOrig_Charge_dte;
                    break;
            }
           
        }
        #endregion

        #region 處理結果說明
        void PAY_RESULT_Descr(string strPAY_RESULT)
        {
            //錯誤原因說明
            DataRow[] DR = SETUP_REJECT_DataTable.Select(" REJECT_CODE ='" + strPAY_RESULT+ "'");
            if (DR.Length > 0)
            {
                strPAY_RESULT_DESCR = PAY_RESULT + "- " + Convert.ToString(DR[0]["DESCR"]);
            }
            else
            {
                strPAY_RESULT_DESCR = PAY_RESULT + "-" + " 其他錯誤";
            }
            if (PAY_RESULT == "I003")
            {
                strPAY_RESULT_DESCR = strPAY_RESULT_DESCR + "(" + CTL_CODE + ")";
            }
        }
        #endregion 

        #region writeReport
        void writeReport()
        {
            //排序
            REPORT_TABLE.DefaultView.Sort = "PAY_RESULT desc";
            REPORT_TABLE = REPORT_TABLE.DefaultView.ToTable();

            CMCRPT001 CMCRPT001 = new CMCRPT001();
            Decimal FISC_SUCCESS_FEE = 0;
            Decimal FISC_SUCCESS_NET_AMT = 0;

            FISC_TOTAL_CNT = FISC_SUCCESS_CNT + FISC_FAIL_CNT;
            FISC_TOTAL_AMT = FISC_SUCCESS_AMT + FISC_FAIL_AMT;

            FISC_SUCCESS_FEE = Convert.ToDecimal(FISC_SUCCESS_CNT * decFeeAmt);
            FISC_SUCCESS_NET_AMT = FISC_SUCCESS_AMT - FISC_SUCCESS_FEE;

            //設定特殊欄位資料
            ArrayList alSumData = new ArrayList();
            alSumData.Add(new ExcelFactory.ListItem("#RPT_BANK_NAME", strBank_name));
            alSumData.Add(new ExcelFactory.ListItem("#RPT_FEE", "手續費("+decFeeAmt.ToString("#0.00")+")"));
            alSumData.Add(new ExcelFactory.ListItem("#RPT_DATE", TODAY_PROCESS_DTE.ToString("yyyy/MM/dd")));
            alSumData.Add(new ExcelFactory.ListItem("#TOTAL_CNT", FISC_TOTAL_CNT + " 筆"));
            alSumData.Add(new ExcelFactory.ListItem("#TOTAL_AMT", "$" + FISC_TOTAL_AMT.ToString("###,###,##0.00")));
            alSumData.Add(new ExcelFactory.ListItem("#SUCCESS_CNT", FISC_SUCCESS_CNT + " 筆"));
            alSumData.Add(new ExcelFactory.ListItem("#SUCCESS_AMT", "$" + FISC_SUCCESS_AMT.ToString("###,###,##0.00")));
            alSumData.Add(new ExcelFactory.ListItem("#FAIL_CNT", FISC_FAIL_CNT + " 筆"));
            alSumData.Add(new ExcelFactory.ListItem("#FAIL_AMT", "$" + FISC_FAIL_AMT.ToString("###,###,##0.00")));
            alSumData.Add(new ExcelFactory.ListItem("#SUCCESS_FEE", "$"+FISC_SUCCESS_FEE.ToString("###,###,##0.00")));
            alSumData.Add(new ExcelFactory.ListItem("#NET_SUCC_AMT", "$"+FISC_SUCCESS_NET_AMT.ToString("###,###,##0.00")));
            //設定合併欄位資料
            ArrayList alMegData = new ArrayList();
            string strFile_Name = "PBRFIR002-扣繳成功失敗報表_"+ strDescr;
            //產出報表
            CMCRPT001.Output(REPORT_TABLE, alSumData, alMegData,strFile_Name , "PBRFIR002", strSheet, strSheet, TODAY_PROCESS_DTE, true);
        }
        #endregion

        #region Display
        void writeDisplay()
        {
            logger.strJobID = "00002";
            //擷取市水需回應資料
            logger.strTBL_NAME = "PUBLIC_LIST";
            logger.strTBL_ACCESS = "Q";
            logger.intTBL_COUNT = PUBLIC_LIST_Query_Count;
            logger.writeCounter();

            //市水回應檔筆數(含頭尾)
            logger.strTBL_NAME = "FISC_STMTOUT";
            logger.strTBL_ACCESS = "I";
            logger.intTBL_COUNT = FISC_TXT_Count;
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

