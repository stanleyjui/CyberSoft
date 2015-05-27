using System;
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

namespace Cybersoft.Biz.Cust
{
    /// <summary>
    /// 收公共事業代繳扣款檔(財金格式)，將扣款資料寫入代繳紀錄檔中
    /// 20140717 更新：增加台電換號檢核功能，若有換號狀況，預先檢核新電號是否已有申請紀錄，若有則改以新電號扣款，若無則以舊電號扣款
    /// ABEND 處理:
    /// </summary>
    public class PBBFII001
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

        #region 宣告檔案路徑

        //XML放置路徑 
        String strXmlURL = "";
        //寫出檔案路徑
        String FILE_PATH = "";

        #endregion

        #region 宣告TABLE
        //宣告PUBLIC_HIST
        String PUBLIC_HIST_RC = "";
        String PUBLIC_HIST_chk_RC = "";
        PUBLIC_HISTDao PUBLIC_HIST = new PUBLIC_HISTDao();
        PUBLIC_HISTDao PUBLIC_HIST_chk = new PUBLIC_HISTDao();

        //宣告PUBLIC_HIST_T        
        PUBLIC_HISTDao PUBLIC_HIST_T = new PUBLIC_HISTDao();

        //宣告PUBLIC_LIST
        String PUBLIC_LIST_RC = "";
        PUBLIC_LISTDao PUBLIC_LIST = new PUBLIC_LISTDao();

        //宣告PUBLIC_LIST_T       
        PUBLIC_LISTDao PUBLIC_LIST_T = new PUBLIC_LISTDao();

        //宣告CARD_INF
        String CARD_INF_RC = "";
        CARD_INFDao CARD_INF = new CARD_INFDao();

        //宣告SETUP_PRODUCT
        String SETUP_PRODUCT_RC = "";
        SETUP_PRODUCTDao SETUP_PRODUCT = new SETUP_PRODUCTDao();

        //宣告SETUP_PUBLIC
        String SETUP_PUBLIC_RC = "";
        SETUP_PUBLICDao SETUP_PUBLIC = new SETUP_PUBLICDao();
        DataTable SETUP_PUBLIC_DATATABLE = new DataTable();

        //
        DataTable CHANGE_AREA_DataTable = new DataTable();

        //宣告PUBLIC_APPLY
        String PUBLIC_APPLY_RC = "";
        PUBLIC_APPLYDao PUBLIC_APPLY = new PUBLIC_APPLYDao();
        #endregion

        #region 宣告常數
        string strJobName = "PBBFII001";        
        const int intDataLength = 150;  //檔案Detail長度
        bool onetime = false;
        #endregion

        #region 宣告共用變數

        //批次日期
        DateTime PREV_PROCESS_DTE = new DateTime();
        DateTime TODAY_PROCESS_DTE = new DateTime();
        DateTime NEXT_WORKING_DTE = new DateTime();

        //序號組合
        String TRANS_DTE = "";
        String PAY_TYPE = "";      //依SETUP_PUBLIC而定
        String WK_BATCH = "";      //參數帶入
        String SEQ_PAY_TYPE = "";
        StringBuilder WK_SEQ = new StringBuilder();

        //處理註記
        String PROCESS_FLAG = "N";

        //主檔欄位
        String WK_PAY_DTE = "";
        DateTime PAY_DTE = new DateTime();

        String PAY_NBR = "";
        String BIN = "";
        String ACCT_NBR = "";
        String PAY_ACCT_NBR = "";
        String CUST_SEQ = "";
        String BU = "";
        String CARD_PRODUCT = "";
        String CARD_NBR = "";
        String strOLD_PAY_NBR = "";
        String strCTL_CODE = "";

        //檔尾資料
        Decimal TOTAL_AMT = 0;
        Decimal TOTAL_CNT = 0;

        int intReadInfDataCount = 0;
        int i = 0;  //PUBLIC_LIST_T COUNT
        int x = 0;  //PUBLIC_HIST_T COUNT
        Decimal WK_TOTAL_AMT = 0;
        Decimal WK_TOTAL_CNT = 0;
        int PUBLIC_HIST_Query_Count = 0;
        int PUBLIC_HIST_Delete_Count = 0;
        int PUBLIC_LIST_Delete_Count = 0;
        int PUBLIC_HIST_T_Insert_Count = 0;
        int PUBLIC_LIST_T_Insert_Count = 0;
        int SETUP_PUBLIC_Query_Count = 0;
        string temp_yyymmdd = "";
        string strFlag = "";
        string strFileName = "";
        string strRecNAME = "";
        string strPayCardTrans = "";
        string strPayType = "";
        string strSEND_UNIT = "";
        string strRECV_UNIT = "";
        string strTRANSFER_UNIT = "";

        #endregion

        static readonly Cybersoft.Coca.Log.Logger logger = Cybersoft.Log.LogManager<Cybersoft.Coca.Log.Logger>.GetLogger(System.Reflection.MethodInfo.GetCurrentMethod().ReflectedType.Name);
        //========================================================================================================
        #region 程式主邏輯【MAIN Routine】
        public string RUN(string strTrans_Type, string BATCH_NO)
        {
            #region 準備MSGLG.SYSOUT CLASS
            logger.strJobID = JobID;
            logger.strJOBNAME = strJobName;
            logger.dtRunDate = DateTime.Now;
            #endregion

            try
            {
                #region 取得系統資訊(前次批次日、本日批次日)
                CMCSYS001 SYSINF = new CMCSYS001();
                SYSINF.strJobID = JobID;
                SYSINF.strJobName = strJobName;
                String SYSINF_RC = SYSINF.getSYSINF();
                PREV_PROCESS_DTE = SYSINF.datetimePREV_PROCESS_DTE;
                TODAY_PROCESS_DTE = SYSINF.datetimeTODAY_PROCESS_DTE;
                NEXT_WORKING_DTE = SYSINF.datetimeBANK_NEXT_PROCESS_DTE;
                TRANS_DTE = TODAY_PROCESS_DTE.ToString("yyMMdd");
                temp_yyymmdd = (Convert.ToString(Convert.ToInt64(TODAY_PROCESS_DTE.ToString("yyyy")) - 1911) + TODAY_PROCESS_DTE.ToString("MMdd")).PadLeft(7, '0');
                //TODAY_PROCESS_DTE.ToString("yyyyMMdd");
                #endregion

                #region PUBLIC_HIST, PUBLIC_LIST - PAY_SEQ判斷
                BATCH_NO = BATCH_NO.ToUpper();

                if (BATCH_NO.Length > 1 &&
                     "T".Equals(BATCH_NO.Substring(BATCH_NO.Length - 1, 1)))    //補執行時，批號最後須加"T"。ex."1T"
                {
                    onetime = true;
                    //WK_BATCH = BATCH_NO.Substring(0,BATCH_NO.Length -1);
                    WK_BATCH = BATCH_NO;
                    strJobName = strJobName + "T";
                    strFlag = "Y";
                }
                else
                {
                    WK_BATCH = BATCH_NO;
                }

                #endregion

                #region 讀取公共事業參數取得該類別參數設定 (KEY:檔案交換單位 & 檔案交易代碼)
                SETUP_PUBLIC.init();
                
                #region 讀參數檔
                //條件
                if (strTrans_Type == "02120")          //[代退北市水]參數同[北市水]參數
                {
                    SETUP_PUBLIC.strWhereFILE_TRANSFER_TYPE = "01120";
                }
                else
                {
                    SETUP_PUBLIC.strWhereFILE_TRANSFER_TYPE = strTrans_Type;
                }
                SETUP_PUBLIC.strWhereFILE_TRANSFER_UNIT = "FISC";
                SETUP_PUBLIC.strWherePOST_RESULT = "00";
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
                        logger.strJobQueue = "查詢SETUP_PUBLIC-FISC參數設定有誤:" + SETUP_PUBLIC_RC;
                        logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                        return "B0016:" + logger.strJobQueue;
                }                
                #endregion
                //非設定之公用事業參數則結束處理程序
                if (SETUP_PUBLIC_Query_Count == 0)
                {
                    logger.strJobQueue = "公用事業參數檔中無設定需報送財金格式的公用事業單位，不執行。";
                    logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                    return "B0000:" + logger.strJobQueue;
                }
                else if (SETUP_PUBLIC_Query_Count == 1)
                {
                    //卡號轉換
                    strPayCardTrans = SETUP_PUBLIC.resultTable.Rows[0]["PAY_CARD_TRANS"].ToString();
                    //檔案名稱
                    strFileName = SETUP_PUBLIC.resultTable.Rows[0]["FILE_TRANSFER_TYPE"].ToString();                    
                    strRecNAME = SETUP_PUBLIC.resultTable.Rows[0]["FILE_FORMAT"].ToString();
                    //扣繳類別
                    strPayType = SETUP_PUBLIC.resultTable.Rows[0]["PAY_TYPE"].ToString();
                    //FISC檔案參數
                    strSEND_UNIT = SETUP_PUBLIC.resultTable.Rows[0]["SEND_UNIT"].ToString();
                    strRECV_UNIT = SETUP_PUBLIC.resultTable.Rows[0]["RECV_UNIT"].ToString();
                    strTRANSFER_UNIT = SETUP_PUBLIC.resultTable.Rows[0]["TRANSFER_UNIT"].ToString();
                }
                else
                {
                    logger.strJobQueue = "公用事業參數檔中無設定需報送財金格式的公用事業單位，不執行。";
                    logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                    return "B0016:" + logger.strJobQueue;
                }
                #endregion
                                
                #region 宣告檔案路徑
                Cybersoft.Base.CMCURL001 CMCURL = new CMCURL001();
                if (strRecNAME != "")
                {                    
                    #region 宣告檔案路徑
                    //檔案格式 strXmlURL
                    strXmlURL = CMCURL.getURL(strRecNAME);
                    //strXmlURL = CMCURL.getURL("FISC_BANK2CARD");
                    if (strXmlURL == "")
                    {
                        logger.strJobQueue = "格式取得錯誤!!! " + strRecNAME + " <URL> 未設定";
                        logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                        return "B0099:" + logger.strJobQueue;
                    }

                    //檔案路徑 strDestPATH
                    string strFilePath = CMCURL.getPATH(strRecNAME);
                    //string strFilePath = CMCURL.getPATH("FISC_BANK2CARD");
                    if (strFilePath == "")
                    {
                        logger.strJobQueue = "路徑取得錯誤!!! " + strRecNAME + " <PATH> 未設定";
                        logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                        return "B0099:" + logger.strJobQueue;
                    }
                    //檔案名稱 strFileName
                    string strFileName = CMCURL.getFILE_NAME(strRecNAME);
                    //string strFileName = CMCURL.getFILE_NAME("FISC_BANK2CARD");
                    if (strFileName == "")
                    {
                        logger.strJobQueue = "檔名取得錯誤!!! " + strRecNAME + " <FILE_NAME> 未設定";
                        logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                        return "B0099:" + logger.strJobQueue;
                    }

                    //附檔名
                    String strEXT = CMCURL.getEXT(strRecNAME);
                    //String strEXT = CMCURL.getEXT("FISC_BANK2CARD");
                    if (strEXT != "")
                    {
                        strFileName = strFileName + strEXT;
                    }

                    FILE_PATH = strFilePath + strTrans_Type + strFileName + "1" + strEXT;  //扣款檔命名規則
                    FILE_PATH = CMCURL.ReplaceVarDateFormat(FILE_PATH, TODAY_PROCESS_DTE);
                    #endregion
                }
                else
                {
                    logger.strJobQueue = "參數檔未設定檔案格式 : 機構代碼(" + strTrans_Type + ") 格式名稱(" + strRecNAME + ")";
                    logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                    return "B0099:" + logger.strJobQueue;
                }
                #endregion
                //若檔案不存在則離開
                string Check_RC = CMCURL.isFileExists(FILE_PATH).ToString();
                if (Check_RC.Substring(0, 5) != "S0000")
                {
                    logger.strJobQueue = "本日無檔案可處理: " + Check_RC.Trim();
                    logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                    return "B0000";
                }

                #region 複製Table定義
                PUBLIC_HIST_T.table_define();
                PUBLIC_LIST_T.table_define();
                #endregion

                #region 載入檔案格式資訊
                FileParseByXml xml = new FileParseByXml();

                // BATCHTX02_H Layout
                DataTable BATCHTX02_H_DataTable = xml.Xml2DataTable(strXmlURL, "BATCHTX02_H");

                if (xml.strMSG.Length > 0)
                {
                    logger.strJobQueue = "[Xml2DataTable(BATCHTX02_H)] - " + xml.strMSG.ToString().Trim();
                    logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                    return "B0099:" + logger.strJobQueue;
                }

                // BATCHTX02 Layout
                DataTable BATCHTX02_DataTable = xml.Xml2DataTable(strXmlURL, "BATCHTX02");

                if (xml.strMSG.Length > 0)
                {
                    logger.strJobQueue = "[Xml2DataTable(BATCHTX02)] - " + xml.strMSG.ToString().Trim();
                    logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                    return "B0099:" + logger.strJobQueue;
                }

                // BATCHTX02_T Layout
                DataTable BATCHTX02_T_DataTable = xml.Xml2DataTable(strXmlURL, "BATCHTX02_T");

                if (xml.strMSG.Length > 0)
                {
                    logger.strJobQueue = "[Xml2DataTable(BATCHTX02_T)] - " + xml.strMSG.ToString().Trim();
                    logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                    return "B0099:" + logger.strJobQueue;
                }

                //台電的專用資料區
                if (strTrans_Type == "01020")
                {
                    CHANGE_AREA_DataTable = xml.Xml2DataTable(strXmlURL, "CHANGE_AREA_01020");

                    if (xml.strMSG.Length > 0)
                    {
                        logger.strJobQueue = "[Xml2DataTable(CHANGE_AREA_01020)] - " + xml.strMSG.ToString().Trim();
                        logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                        return "B0099:" + logger.strJobQueue;
                    }
                }
                #endregion

                #region 設定檔案編碼
                Encoding BIG5 = Encoding.GetEncoding("big5");
                #endregion

                #region 將資料轉至DATA_TABLE
                FileStream fsInFile = new FileStream(FILE_PATH, FileMode.Open, FileAccess.Read, FileShare.None);
                using (StreamReader srInFile = new StreamReader(fsInFile, BIG5))
                {
                    string strInfData;

                    intReadInfDataCount = 0;

                    int intRECHCount = 0;
                    int intRECCount = 0;
                    int intRECTCount = 0;

                    while ((strInfData = srInFile.ReadLine()) != null) // && PROCESS_FLAG == "Y")
                    {
                        intReadInfDataCount++;

                        #region 檔案格式檢核(筆資料長度)
                        if (strInfData.Length != intDataLength)
                        {
                            logger.strJobQueue = "公共事業代扣檔中第" + intReadInfDataCount + "筆的長度有誤，請通知系統人員! 該筆實際長度為 " + strInfData.Length;
                            logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                            return "B0099:" + logger.strJobQueue;
                        }
                        #endregion

                        DataTable InfData_DataTable = new DataTable();

                        string strTag = strInfData.Substring(0, 1).ToString().Trim();

                        switch (strTag)
                        {
                            case "1":  //檔頭資料
                                intRECHCount++;

                                #region 依 Layout 拆解資料
                                InfData_DataTable = xml.FileLine2DataTable(BIG5, strInfData, BATCHTX02_H_DataTable);
                                if (xml.strMSG.Length > 0)
                                {
                                    logger.strJobQueue = "[FileLine2DataTable(BATCHTX02_H)] (L" + intReadInfDataCount + ") - " + xml.strMSG.ToString().Trim();
                                    logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                                    return "B0099:" + logger.strJobQueue;
                                }
                                #endregion

                                #region 檔頭檢核
                                //處理日期必須等於今日批次日,否則需人工介入
                                WK_PAY_DTE = Convert.ToString(Convert.ToInt16(InfData_DataTable.Rows[0]["PAY_DTE_H"].ToString().Substring(0, 3)) + 1911) + InfData_DataTable.Rows[0]["PAY_DTE_H"].ToString().Substring(3, 4);
                                PAY_DTE = DateTime.ParseExact(WK_PAY_DTE, "yyyyMMdd", null, System.Globalization.DateTimeStyles.AllowWhiteSpaces);
                                if (strTrans_Type != "01020")  //台電不檢核檔頭日期
                                {
                                    if (PAY_DTE == TODAY_PROCESS_DTE)
                                    {
                                        PROCESS_FLAG = "Y";
                                        logger.strJobQueue = "指定入扣帳日為" + PAY_DTE + "等於今日批次日" + TODAY_PROCESS_DTE + "符合處理日期區間!!!";
                                        logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                                    }
                                    else
                                    {
                                        logger.strJobQueue = "公共事業代扣檔第 " + intReadInfDataCount + "筆日期有誤! 系統日= " + TODAY_PROCESS_DTE + "/ 檔案日= " + PAY_DTE;
                                        logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                                        return "B0099:" + logger.strJobQueue;
                                    }
                                }
                                else
                                {
                                    PROCESS_FLAG = "Y";
                                }
                                #endregion

                                if (PROCESS_FLAG == "Y")
                                {
                                    #region 轉帳類別代碼判斷
                                    strTrans_Type = InfData_DataTable.Rows[0]["TRANSFER_TYPE_H"].ToString().Trim();

                                    //檢核是否為參數設定代碼
                                    if (strTrans_Type == "02120") //代退市水水費
                                    {
                                        //PAY_TYPE = "0002";
                                        PAY_TYPE = SETUP_PUBLIC.resultTable.Rows[0]["PAY_TYPE"].ToString().Trim();
                                        SEQ_PAY_TYPE = "R002";
                                        PROCESS_FLAG = "Y";
                                    }
                                    else
                                    {
                                        SETUP_PUBLIC.resultTable.DefaultView.RowFilter = "FILE_TRANSFER_TYPE = '" + strTrans_Type + "' ";
                                        SETUP_PUBLIC.resultTable = SETUP_PUBLIC.resultTable.DefaultView.ToTable();
                                        if (SETUP_PUBLIC.resultTable.Rows.Count > 0)
                                        {
                                            PAY_TYPE = SETUP_PUBLIC.resultTable.Rows[0]["PAY_TYPE"].ToString().Trim();
                                            SEQ_PAY_TYPE = SETUP_PUBLIC.resultTable.Rows[0]["PAY_TYPE"].ToString().Trim();
                                            PROCESS_FLAG = "Y";
                                        }
                                        else
                                        {
                                            PAY_TYPE = "";
                                            SEQ_PAY_TYPE = "";
                                            PROCESS_FLAG = "N";
                                        }
                                    }
                                    if (PROCESS_FLAG != "Y")
                                    {
                                        logger.strJobQueue = "公共事業代扣繳中第" + intReadInfDataCount + "筆的轉帳類別有誤，請通知系統人員! " + strTrans_Type;
                                        logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                                        return "B0099:" + logger.strJobQueue;
                                    }
                                    #endregion

                                    #region (rerun準備)刪除今日新增之省水代繳清單
                                    String WK_SEQ_11 = TODAY_PROCESS_DTE.ToString("yyMMdd") + SEQ_PAY_TYPE + WK_BATCH;
                                    PUBLIC_LIST.init();
                                    PUBLIC_LIST.strWherePROCESS_DTE = TODAY_PROCESS_DTE.ToString("yyyyMMdd");
                                    PUBLIC_LIST.strWhereRETURN_DTE = PAY_DTE.ToString("yyyyMMdd");
                                    PUBLIC_LIST.strWherePAY_TYPE = PAY_TYPE;
                                    PUBLIC_LIST.strWhereFILE_TRANSFER_TYPE = strTrans_Type;
                                    PUBLIC_LIST.strWhereMNT_USER = strJobName;

                                    PUBLIC_LIST_RC = PUBLIC_LIST.delete_for_public(WK_SEQ_11, strFlag);
                                    switch (PUBLIC_LIST_RC)
                                    {
                                        case "S0000": //刪除成功
                                            PUBLIC_LIST_Delete_Count = PUBLIC_LIST.intDelCnt;
                                            if (PUBLIC_LIST_Delete_Count > 0)
                                            {
                                                logger.strJobQueue = "PUBLIC_LIST.delete finish 筆數:" + PUBLIC_LIST_Delete_Count.ToString("###,###,##0").PadLeft(11, ' ');
                                                logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                                            }
                                            else
                                            {
                                                logger.strJobQueue = PUBLIC_LIST_RC + " 本日PUBLIC_LIST無資料刪除";
                                                logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                                            }
                                            break;

                                        default: //資料庫錯誤
                                            logger.strJobQueue = "刪除PUBLIC_LIST 資料錯誤:" + PUBLIC_LIST_RC;
                                            logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                                            return "B0016:" + logger.strJobQueue;
                                    }
                                    #endregion

                                    #region (rerun準備)刪除今日新增之財金公共事業代繳資料
                                    //String WK_SEQ_11 = TODAY_PROCESS_DTE.ToString("yyMMdd") + SEQ_PAY_TYPE + WK_BATCH;
                                    PUBLIC_HIST.strWhereMNT_USER = strJobName;
                                    PUBLIC_HIST.strWhereFILE_TRANSFER_TYPE = strTrans_Type;
                                    PUBLIC_HIST_RC = PUBLIC_HIST.delete_for_public(WK_SEQ_11, strFlag);

                                    switch (PUBLIC_HIST_RC)
                                    {
                                        case "S0000": //刪除成功
                                            PUBLIC_HIST_Delete_Count = PUBLIC_HIST.intDelCnt;
                                            if (PUBLIC_HIST_Delete_Count > 0)
                                            {
                                                logger.strJobQueue = "PUBLIC_HIST.delete finish 筆數:" + PUBLIC_HIST_Delete_Count.ToString("###,###,##0").PadLeft(11, ' ');
                                                logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                                            }
                                            else
                                            {
                                                logger.strJobQueue = PUBLIC_HIST_RC + " 本日PUBLIC_HIST無資料刪除";
                                                logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                                            }
                                            break;

                                        default: //資料庫錯誤
                                            logger.strJobQueue = "刪除PUBLIC_HIST 資料錯誤:" + PUBLIC_HIST_RC;
                                            logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                                            //return "B0016:" + logger.strJobQueue;
                                            break;
                                    }
                                    #endregion

                                    #region 檢核該批公共事業代扣檔是否已處理過
                                    PUBLIC_LIST.init();
                                    PUBLIC_LIST.strWhereRETURN_DTE = PAY_DTE.ToString("yyyyMMdd");
                                    PUBLIC_LIST.strWherePAY_TYPE = PAY_TYPE;
                                    PUBLIC_LIST.strWhereFILE_TRANSFER_TYPE = strTrans_Type;
                                    PUBLIC_LIST.strWhereMNT_USER = strJobName;
                                    PUBLIC_LIST_RC = PUBLIC_LIST.query();
                                    switch (PUBLIC_LIST_RC)
                                    {
                                        case "S0000": //查詢成功
                                            if (PUBLIC_LIST.resultTable.Rows.Count > 0)
                                            {
                                                PROCESS_FLAG = "N";
                                                logger.strJobQueue = "處理紀錄檢核,此批資料已於 " + Convert.ToString(PUBLIC_LIST.resultTable.Rows[0]["PROCESS_DTE"]) + " 處理完成不需再處理";
                                                logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                                            }
                                            break;

                                        case "F0023": //查無資料
                                            PROCESS_FLAG = "Y";
                                            logger.strJobQueue = "本批資料未處理,今日需處理";
                                            logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                                            break;

                                        default: //資料庫錯誤
                                            logger.strJobQueue = "查詢PUBLIC_LIST 資料錯誤:" + PUBLIC_LIST_RC;
                                            logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                                            return "B0016:" + logger.strJobQueue;
                                    }

                                    #endregion
                                }
                                break;

                            case "2":  //明細資料
                                intRECCount++;

                                #region 依 Layout 拆解資料
                                InfData_DataTable = xml.FileLine2DataTable(BIG5, strInfData, BATCHTX02_DataTable);
                                if (xml.strMSG.Length > 0)
                                {
                                    logger.strJobQueue = "[FileLine2DataTable(BATCHTX02)] (L" + intReadInfDataCount + ") - " + xml.strMSG.ToString().Trim();
                                    logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                                    return "B0099:" + logger.strJobQueue;
                                }
                                #endregion

                                #region 檢核明細資料;move DATA to PUBLIC_HIST_T

                                switch (strTrans_Type)
                                {
                                    case "01010":  //省水
                                        //PAY_ACCT_NBR = Convert.ToString(InfData_DataTable.Rows[0]["PAY_ACCT_NBR"]).Substring(2, 14);  //客製化
                                        PAY_ACCT_NBR = Convert.ToString(InfData_DataTable.Rows[0]["PAY_ACCT_NBR"]);
                                        PAY_NBR = Convert.ToString(InfData_DataTable.Rows[0]["PAY_NBR"]).Substring(0, 11);
                                        break;
                                    case "01020":  //台電
                                        //PAY_ACCT_NBR = Convert.ToString(InfData_DataTable.Rows[0]["PAY_ACCT_NBR"]).Substring(2, 14);   //客製化
                                        PAY_ACCT_NBR = Convert.ToString(InfData_DataTable.Rows[0]["PAY_ACCT_NBR"]);
                                        PAY_NBR = Convert.ToString(InfData_DataTable.Rows[0]["PAY_NBR"]).Substring(0, 11);
                                        #region 若為台電，確認是否有改號
                                        strOLD_PAY_NBR = "";
                                        if (Convert.ToString(InfData_DataTable.Rows[0]["CHANGE_AREA"]).Length > 6)
                                        {
                                            #region 依 Layout 拆解資料_異動資料區
                                            string strChange_aera = InfData_DataTable.Rows[0]["CHANGE_AREA"].ToString().PadRight(21, ' ');
                                            DataTable CHANGE_AREAData_DataTable = xml.FileLine2DataTable(BIG5, strChange_aera, CHANGE_AREA_DataTable);
                                            if (xml.strMSG.Length > 0)
                                            {
                                                logger.strJobQueue = "[FileLine2DataTable(CHANGE_AREA)] (L) - " + xml.strMSG.ToString().Trim();
                                                logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                                                throw new System.Exception("B0099:" + logger.strJobQueue);
                                            }
                                            #endregion
                                            //舊號
                                            strOLD_PAY_NBR = CHANGE_AREAData_DataTable.Rows[0]["OLD_PAY_NBR"].ToString();
                                        }
                                        #endregion
                                        break;
                                    case "01120":  //市水
                                        PAY_ACCT_NBR = Convert.ToString(InfData_DataTable.Rows[0]["PAY_ACCT_NBR"]);
                                        PAY_NBR = Convert.ToString(InfData_DataTable.Rows[0]["PAY_NBR"]).Substring(0, 10);
                                        break;
                                    default:
                                        PAY_ACCT_NBR = Convert.ToString(InfData_DataTable.Rows[0]["PAY_ACCT_NBR"]).Substring(2, 14);
                                        PAY_NBR = Convert.ToString(InfData_DataTable.Rows[0]["PAY_NBR"]);
                                        break;
                                }

                                //取得信用卡卡號( 客製化 )
                                if (!"".Equals(strPayCardTrans))
                                {
                                    if (PAY_ACCT_NBR.Substring(0, 2) == strPayCardTrans)
                                    {
                                        TRANSFER_PAY_NBR();
                                    }
                                    else
                                    {
                                        CARD_NBR = PAY_ACCT_NBR.ToString();
                                    }
                                }
                                else
                                {
                                    CARD_NBR = PAY_ACCT_NBR.ToString();
                                }
                                #endregion

                                #region 組扣款序號 檔案傳送日期(6碼,年為西元後兩碼)+代繳類別(4碼)+批號(1碼)+序號(5碼) = 16碼
                                WK_SEQ = new StringBuilder();
                                WK_SEQ.Append(TRANS_DTE);
                                WK_SEQ.Append(SEQ_PAY_TYPE);
                                WK_SEQ.Append(WK_BATCH);
                                if (onetime)
                                {
                                    WK_SEQ.Append(intRECCount.ToString().PadLeft(4, '0'));
                                }
                                else
                                {
                                    WK_SEQ.Append(intRECCount.ToString().PadLeft(5, '0'));
                                }
                                #endregion

                                #region 找出客戶ID序號
                                ACCT_NBR = "";
                                CUST_SEQ = "";
                                CARD_INF.init();
                                CARD_INF.strWhereCARD_NBR = CARD_NBR;
                                CARD_INF_RC = CARD_INF.query();
                                switch (CARD_INF_RC)
                                {
                                    case "S0000": //查詢成功
                                        BU = Convert.ToString(CARD_INF.resultTable.Rows[0]["BU"]);
                                        ACCT_NBR = Convert.ToString(CARD_INF.resultTable.Rows[0]["ACCT_NBR"]);
                                        CUST_SEQ = Convert.ToString(CARD_INF.resultTable.Rows[0]["CARDHOLDER_NBR"]);
                                        CARD_PRODUCT = Convert.ToString(CARD_INF.resultTable.Rows[0]["CARD_PRODUCT"]);
                                        strCTL_CODE =Convert.ToString(CARD_INF.resultTable.Rows[0]["CTL_CODE"]);
                                        break;

                                    case "F0023": //查無資料
                                        logger.strJobQueue = "查無此卡號號碼 : " + CARD_NBR;
                                        logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                                        break;

                                    default: //資料庫錯誤
                                        logger.strJobQueue = "查詢 CARD_INF 資料錯誤:" + CARD_INF_RC;
                                        logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                                        return "B0016:" + logger.strJobQueue;
                                }
                                #endregion

                                //將結果寫入代繳暫存檔中
                                PUBLIC_HIST_T.initInsert_row();
                                x = PUBLIC_HIST_T.resultTable.Rows.Count - 1;
                                PUBLIC_HIST_T.resultTable.Rows[x]["TRANS_DTE"] = TODAY_PROCESS_DTE.ToString("yyyyMMdd");
                                PUBLIC_HIST_T.resultTable.Rows[x]["PAY_TYPE"] = PAY_TYPE;
                                PUBLIC_HIST_T.resultTable.Rows[x]["BU"] = BU;
                                PUBLIC_HIST_T.resultTable.Rows[x]["ACCT_NBR"] = ACCT_NBR;
                                PUBLIC_HIST_T.resultTable.Rows[x]["PRODUCT"] = "000"; ;
                                PUBLIC_HIST_T.resultTable.Rows[x]["CARD_PRODUCT"] = CARD_PRODUCT;
                                PUBLIC_HIST_T.resultTable.Rows[x]["CURRENCY"] = "TWD";
                                PUBLIC_HIST_T.resultTable.Rows[x]["PAY_CARD_NBR"] = CARD_NBR;
                                PUBLIC_HIST_T.resultTable.Rows[x]["PAY_CARD_NBR_ORI"] = CARD_NBR;
                                PUBLIC_HIST_T.resultTable.Rows[x]["EXPIR_DTE"] = "";
                                PUBLIC_HIST_T.resultTable.Rows[x]["CUST_SEQ"] = CUST_SEQ;
                                PUBLIC_HIST_T.resultTable.Rows[x]["FILE_TRANSFER_TYPE"] = strTrans_Type;
                                PUBLIC_HIST_T.resultTable.Rows[x]["CTL_CODE"] = strCTL_CODE;
                                PUBLIC_HIST_T.resultTable.Rows[x]["PAY_ACCT_NBR_ORI"] = PAY_ACCT_NBR;
                                //若為台電扣款且有換號，以舊號做扣款
                                if ((strTrans_Type == "01020") & (strOLD_PAY_NBR != ""))
                                {
                                    PUBLIC_HIST_T.resultTable.Rows[x]["PAY_NBR"] = PAY_NBR.Substring(0, 2) + strOLD_PAY_NBR;
                                    PUBLIC_HIST_T.resultTable.Rows[x]["CHANGE_NBR_NEW"] = PAY_NBR;
                                }
                                else
                                {
                                    PUBLIC_HIST_T.resultTable.Rows[x]["PAY_NBR"] = PAY_NBR;
                                }
                                PUBLIC_HIST_T.resultTable.Rows[x]["PAY_AMT"] = Convert.ToDecimal(InfData_DataTable.Rows[0]["AMT"]);
                                PUBLIC_HIST_T.resultTable.Rows[x]["PAY_DTE"] = PAY_DTE;
                                PUBLIC_HIST_T.resultTable.Rows[x]["PAY_SEQ"] = WK_SEQ.ToString();
                                PUBLIC_HIST_T.resultTable.Rows[x]["MNT_DT"] = TODAY_PROCESS_DTE;
                                PUBLIC_HIST_T.resultTable.Rows[x]["MNT_USER"] = strJobName;

                                //統計筆數及金額
                                WK_TOTAL_CNT = WK_TOTAL_CNT + 1;
                                WK_TOTAL_AMT = WK_TOTAL_AMT + Convert.ToDecimal(InfData_DataTable.Rows[0]["AMT"]);

                                break;

                            case "3": //檔尾資料
                                intRECTCount++;

                                #region 依 Layout 拆解資料
                                InfData_DataTable = xml.FileLine2DataTable(BIG5, strInfData, BATCHTX02_T_DataTable);
                                if (xml.strMSG.Length > 0)
                                {
                                    logger.strJobQueue = "[FileLine2DataTable(BATCHTX02_T)] (L" + intReadInfDataCount + ") - " + xml.strMSG.ToString().Trim();
                                    logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                                    return "B0099:" + logger.strJobQueue;
                                }
                                #endregion

                                #region 檢核檔尾資料
                                TOTAL_AMT = Convert.ToDecimal(InfData_DataTable.Rows[0]["TOT_AMT"]);
                                TOTAL_CNT = Convert.ToDecimal(InfData_DataTable.Rows[0]["TOT_CNT"]);

                                //檢核筆數
                                if (TOTAL_CNT != WK_TOTAL_CNT)
                                {
                                    PROCESS_FLAG = "N";
                                    logger.strJobQueue = "明細實際筆數 " + WK_TOTAL_CNT + " 和檔尾筆數 " + TOTAL_CNT + "不合 !!!";
                                    logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                                    return "B0099:" + logger.strJobQueue;
                                }

                                //檢核金額
                                if (TOTAL_AMT != WK_TOTAL_AMT)
                                {
                                    PROCESS_FLAG = "N";
                                    logger.strJobQueue = "明細實際金額 " + WK_TOTAL_AMT + " 和檔尾金額 " + TOTAL_AMT + "不合 !!!";
                                    logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                                    return "B0099:" + logger.strJobQueue;
                                }

                                #endregion

                                break;

                            default: //資料庫錯誤
                                return "B0016:" + "公共事業代扣繳 BATCHTX02_TYPE ERROR " + strTag;
                        }

                        if (PROCESS_FLAG == "Y")
                        {
                            #region move DATA to PUBLIC_LIST_T
                            PUBLIC_LIST_T.initInsert_row();
                            i = PUBLIC_LIST_T.resultTable.Rows.Count - 1;

                            // 組扣款序號 檔案傳送日期(6碼,年為西元後兩碼)+代繳類別(4碼)+批號(1碼)+序號(5碼) = 16碼
                            WK_SEQ = new StringBuilder();
                            WK_SEQ.Append(TRANS_DTE);
                            WK_SEQ.Append(SEQ_PAY_TYPE);
                            WK_SEQ.Append(WK_BATCH);
                            if (onetime)
                            {
                                WK_SEQ.Append(i.ToString().PadLeft(4, '0'));
                            }
                            else
                            {
                                WK_SEQ.Append(i.ToString().PadLeft(5, '0'));
                            }

                            PUBLIC_LIST_T.resultTable.Rows[i]["PROCESS_DTE"] = TODAY_PROCESS_DTE.ToString("yyyyMMdd");
                            PUBLIC_LIST_T.resultTable.Rows[i]["RETURN_DTE"] = PAY_DTE.ToString("yyyyMMdd");
                            PUBLIC_LIST_T.resultTable.Rows[i]["PAY_TYPE"] = PAY_TYPE;
                            PUBLIC_LIST_T.resultTable.Rows[i]["PAY_SEQ"] = WK_SEQ.ToString();
                            PUBLIC_LIST_T.resultTable.Rows[i]["MNT_DT"] = TODAY_PROCESS_DTE;
                            PUBLIC_LIST_T.resultTable.Rows[i]["MNT_USER"] = strJobName;
                            PUBLIC_LIST_T.resultTable.Rows[i]["FILE_TRANSFER_TYPE"] = strTrans_Type;
                            switch (strTag)
                            {
                                case "1":  //檔頭資料
                                    PUBLIC_LIST_T.resultTable.Rows[i]["PAY_DATA_AREA"] = strInfData.ToString();
                                    break;

                                case "2":  //檔頭資料
                                    PUBLIC_LIST_T.resultTable.Rows[i]["PAY_DATA_AREA"] = strInfData.ToString();
                                    break;

                                case "3":  //檔頭資料
                                    PUBLIC_LIST_T.resultTable.Rows[i]["PAY_DATA_AREA"] = strInfData.ToString();
                                    break;

                                default:
                                    break;
                            }
                            #endregion
                        }
                    }
                }
                #endregion

                #region 將資料整批寫入代繳紀錄檔(PUBLIC_HIST)
                if (PUBLIC_HIST_T.resultTable.Rows.Count > 0)
                {
                    //先紀錄筆數
                    PUBLIC_HIST_T_Insert_Count = PUBLIC_HIST_T.resultTable.Rows.Count;
                    PUBLIC_HIST_T.insert_by_DT();

                    //判別回傳筆數是否相同
                    if (PUBLIC_HIST_T_Insert_Count != PUBLIC_HIST_T.intInsCnt)
                    {
                        logger.strJobQueue = "整批新增PUBLIC_HIST_T時筆數異常,原筆數 : " + PUBLIC_HIST_T_Insert_Count + " 實際筆數: " + PUBLIC_HIST_T.intInsCnt;
                        logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                        return "B0012" + logger.strJobQueue;
                    }

                    logger.strJobQueue = "整批新增至 PUBLIC_HIST 成功筆數 = " + PUBLIC_HIST_T.intInsCnt;
                    logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                }
                #endregion

                #region 將資料整批寫入公用事業代繳清單(PUBLIC_LIST)
                if (PUBLIC_LIST_T.resultTable.Rows.Count > 0)
                {
                    //先紀錄筆數
                    PUBLIC_LIST_T_Insert_Count = PUBLIC_LIST_T.resultTable.Rows.Count;
                    PUBLIC_LIST_T.insert_by_DT();

                    //判別回傳筆數是否相同
                    if (PUBLIC_LIST_T_Insert_Count != PUBLIC_LIST_T.intInsCnt)
                    {
                        logger.strJobQueue = "整批新增PUBLIC_LIST_T時筆數異常,原筆數 : " + PUBLIC_LIST_T_Insert_Count + " 實際筆數: " + PUBLIC_LIST_T.intInsCnt;
                        logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                        return "B0012" + logger.strJobQueue;
                    }

                    logger.strJobQueue = "整批新增至 PUBLIC_LIST 成功筆數 =" + PUBLIC_LIST_T.intInsCnt;
                    logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                }
                #endregion

                #region 若今日有台電換號者，預先檢核新電號是否已生效
                PUBLIC_HIST.init();
                //PUBLIC_HIST.strWherePAY_TYPE = "0004";  //台電
                PUBLIC_HIST.strWhereFILE_TRANSFER_TYPE = strTrans_Type;  //改判斷TRANSFER_TYPE，避免PAY_TYPE改變
                PUBLIC_HIST.strWhereTRANS_DTE = TODAY_PROCESS_DTE.ToString("yyyyMMdd");
                PUBLIC_HIST_RC = PUBLIC_HIST.query_for_PAY_NBR_CHANGE();
                switch (PUBLIC_HIST_RC)
                {
                    case "S0000": //查詢成功
                        PUBLIC_HIST_Query_Count = PUBLIC_HIST.resultTable.Rows.Count;
                        break;

                    case "F0023": //無需處理資料
                        PUBLIC_HIST_Query_Count = 0;
                        logger.strJobQueue = "今日無台電換號資料";
                        logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                        break;

                    default: //資料庫錯誤
                        logger.strJobQueue = "查詢 PUBLIC_HIST.query_for_PAY_NBR_CHANGE() 資料錯誤:" + PUBLIC_HIST_RC;
                        logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                        return "B0016:" + logger.strJobQueue;
                }

                if (PUBLIC_HIST_Query_Count > 0)
                {
                    for (int k = 0; k < PUBLIC_HIST_Query_Count; k++)
                    {
                        //確認新電號是否已申請
                        PUBLIC_APPLY.init();
                        PUBLIC_APPLY.strWherePAY_NBR = PUBLIC_HIST.resultTable.Rows[k]["CHANGE_NBR_NEW"].ToString();
                        PUBLIC_APPLY.strWherePAY_TYPE = PUBLIC_HIST.resultTable.Rows[k]["PAY_TYPE"].ToString();
                        PUBLIC_APPLY_RC = PUBLIC_APPLY.query();
                        switch (PUBLIC_APPLY_RC)
                        {
                            case "S0000": //查詢成功
                                #region 執行更新
                                PUBLIC_HIST_chk.init();
                                //KEY
                                PUBLIC_HIST_chk.strWhereTRANS_DTE = PUBLIC_HIST.resultTable.Rows[k]["TRANS_DTE"].ToString();
                                PUBLIC_HIST_chk.strWherePAY_SEQ = PUBLIC_HIST.resultTable.Rows[k]["PAY_SEQ"].ToString();
                                //更新值
                                PUBLIC_HIST_chk.strPAY_NBR = PUBLIC_HIST.resultTable.Rows[k]["CHANGE_NBR_NEW"].ToString();
                                PUBLIC_HIST_chk.strCHANGE_NBR_NEW = "";
                                PUBLIC_HIST_chk_RC = PUBLIC_HIST_chk.update();
                                switch (PUBLIC_HIST_chk_RC)
                                {
                                    case "S0000": //查詢成功
                                    case "F0023": //無需處理資料
                                        break;
                                    default: //資料庫錯誤
                                        logger.strJobQueue = "更新 PUBLIC_HIST_chk.update()(新電號) 資料錯誤:" + PUBLIC_HIST_chk_RC;
                                        logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                                        return "B0016:" + logger.strJobQueue;
                                }
                                #endregion
                                break;
                            case "F0023": //無需處理資料
                                break;
                            default: //資料庫錯誤
                                logger.strJobQueue = "查詢 PUBLIC_APPLY.query(預先查詢新電號) 資料錯誤:" + PUBLIC_APPLY_RC;
                                logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                                return "B0016:" + logger.strJobQueue;

                        }
                    }
                }
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

        #region TRANSFER_PAY_NBR() - 客製化
        void TRANSFER_PAY_NBR()
        {
            //約定繳款帳號(14碼)-->信用卡號(16碼)
            SETUP_PRODUCT.init();
            SETUP_PRODUCT.strWherePRODUCT_SERVICE_3 = PAY_ACCT_NBR.Substring(2, 2);
            SETUP_PRODUCT_RC = SETUP_PRODUCT.query();
            switch (SETUP_PRODUCT_RC)
            {
                case "S0000": //查詢成功
                    BIN = Convert.ToString(SETUP_PRODUCT.resultTable.Rows[0]["BEG_NBR"]).Substring(0, 6);
                    break;

                case "F0023": //查無資料
                    logger.strJobQueue = "查無此對應PRODUCT_SERVICE_3 : " + PAY_ACCT_NBR.Substring(2, 2);
                    logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                    break;

                default: //資料庫錯誤
                    logger.strJobQueue = "查詢 SETUP_PRODUCT 資料錯誤:" + SETUP_PRODUCT_RC;
                    logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                    break;
            }
            //CARD_NBR = BIN + PAY_ACCT_NBR.Substring(6, 10);
            CARD_NBR = BIN + PAY_ACCT_NBR.Substring(4, 10);
        }
        #endregion

        #region Display
        void writeDisplay()
        {
            logger.strJobID = "00002";
            //刪除今日新增之PUBLIC_HIST資料
            logger.strTBL_NAME = "PUBLIC_HIST";
            logger.strTBL_ACCESS = "D";
            logger.intTBL_COUNT = PUBLIC_HIST_Delete_Count;
            logger.writeCounter();

            //刪除今日新增之PUBLIC_LIST資料
            logger.strTBL_NAME = "PUBLIC_LIST";
            logger.strTBL_ACCESS = "D";
            logger.intTBL_COUNT = PUBLIC_LIST_Delete_Count;
            logger.writeCounter();

            //今日處理之省水資料
            logger.strTBL_NAME = "PUBLIC_FISC";
            logger.strTBL_ACCESS = "Q";
            logger.intTBL_COUNT = intReadInfDataCount;
            logger.writeCounter();

            //今日新增之PUBLIC_HIST資料
            logger.strTBL_NAME = "PUBLIC_HIST";
            logger.strTBL_ACCESS = "I";
            logger.intTBL_COUNT = PUBLIC_HIST_T_Insert_Count;
            logger.writeCounter();

            //今日新增之PUBLIC_LIST資料
            logger.strTBL_NAME = "PUBLIC_LIST";
            logger.strTBL_ACCESS = "I";
            logger.intTBL_COUNT = PUBLIC_LIST_T_Insert_Count;
            logger.writeCounter();

            logger.DisplayCounter(Cybersoft.Coca.Log.LevelEnum.Info);
        }
        #endregion
    }
}