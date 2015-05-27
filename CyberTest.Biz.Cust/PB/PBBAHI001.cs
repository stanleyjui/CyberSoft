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
    /// 將ACH資料寫入代繳紀錄檔中(營業日執行)
    /// 收公共事業代繳扣款檔(ACH格式)，將扣款資料寫入代繳紀錄檔中
    /// ABEND 處理:修正問題後可重新執行,若一日有兩批需處理,需改批號;但若該批資料已至授權階段將不可再執行
    /// </summary>
    public class PBBAHI001
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
        String strXML_Layout = "";
        //寫出檔案名稱
        String strOutFileName = "";
        //寫出檔案路徑
        String FILE_PATH = "";

        #endregion

        #region 宣告TABLE
        //宣告PUBLIC_HIST
        String PUBLIC_HIST_RC = "";
        PUBLIC_HISTDao PUBLIC_HIST = new PUBLIC_HISTDao();

        //宣告PUBLIC_HIST_T        
        PUBLIC_HISTDao PUBLIC_HIST_T = new PUBLIC_HISTDao();

        //宣告PUBLIC_LIST
        String PUBLIC_LIST_RC = "";
        PUBLIC_LISTDao PUBLIC_LIST = new PUBLIC_LISTDao();

        //宣告PUBLIC_LIST_T       
        PUBLIC_LISTDao PUBLIC_LIST_T = new PUBLIC_LISTDao();

        //宣告ACCT_LINK
        ACCT_LINKDao ACCT_LINK = new ACCT_LINKDao();

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

        #endregion

        #region 宣告共用變數
        //批次日期
        DateTime TODAY_PROCESS_DTE = new DateTime();

        //序號組合
        String TRANS_DTE = "";
        String PAY_TYPE = "";
        String WK_PAY_TYPE = "";
        String WK_BATCH = "1";     //固定為1,若當天要處理兩批,第二批請改2
        StringBuilder WK_SEQ = new StringBuilder();

        //處理註記
        String PROCESS_FLAG = "Y";

        //主檔欄位

        String PAY_NBR = "";
        String BIN = "";
        String ACCT_NBR = "";
        String PAY_ACCT_NBR = "";
        String BU = "";
        String CUST_SEQ = "";
        String CARD_NBR = "";
        String CARD_PRODUCT = "";

        //檔尾資料

        //const int intDataLength = 166;  //檔案Detail長度
        const int intDataLength = 160;  //檔案Detail長度
        int intReadInfDataCount = 0;
        int i = 0;  //PUBLIC_LIST_T COUNT
        int x = 0;  //PUBLIC_HIST_T COUNT
        Decimal WK_TOTAL_AMT = 0;
        Decimal WK_TOTAL_CNT = 0;
        int PUBLIC_HIST_Delete_Count = 0;
        int PUBLIC_LIST_Delete_Count = 0;
        int PUBLIC_HIST_T_Insert_Count = 0;
        int PUBLIC_LIST_T_Insert_Count = 0;
        string temp_yyyymmdd = "";

        //參數設定
        int SETUP_PUBLIC_Query_Count = 0;
        string strPayCardTrans = "";
        string strFileName = "";
        string strRecNAME = "";
        string strPayType = "";
        string strSEND_UNIT = "";
        string strRECV_UNIT = "";
        string strTRANSFER_UNIT = "";

        #endregion
        string strTRAN_TYPE = "ACTIVE";
        static readonly Cybersoft.Coca.Log.Logger logger = Cybersoft.Log.LogManager<Cybersoft.Coca.Log.Logger>.GetLogger(System.Reflection.MethodInfo.GetCurrentMethod().ReflectedType.Name);
        //========================================================================================================        
        #region 程式主邏輯【MAIN Routine】
        public string RUN(string strInPAY_TYPE, string BATCH_NO)
        //public string RUN(string strTRAN_TYPE, string strInPAY_TYPE, string BATCH_NO)
        {
            #region 準備MSGLG.SYSOUT CLASS
            logger.strJobID = JobID;
            logger.strJOBNAME = "PBBAHI001";
            logger.dtRunDate = DateTime.Now;
            #endregion

            try
            {
                #region 取得系統資訊(前次批次日、本日批次日)
                CMCSYS001 SYSINF = new CMCSYS001();
                SYSINF.strJobID = JobID;
                SYSINF.strJobName = "PBBAHI001";
                String SYSINF_RC = SYSINF.getSYSINF();
                TODAY_PROCESS_DTE = SYSINF.datetimeTODAY_PROCESS_DTE;
                TRANS_DTE = TODAY_PROCESS_DTE.ToString("yyMMdd");
                temp_yyyymmdd = TODAY_PROCESS_DTE.ToString("yyyyMMdd");
                #endregion

                WK_BATCH = BATCH_NO;

                #region 讀取公共事業參數取得該類別參數設定 (KEY:交換單位)
                SETUP_PUBLIC.init();

                #region 讀參數檔
                //條件
                SETUP_PUBLIC.strWhereFILE_TRANSFER_UNIT = "ACH";
                if (strInPAY_TYPE != "")
                {
                    SETUP_PUBLIC.strWherePAY_TYPE = strInPAY_TYPE;
                }
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
                        logger.strJobQueue = "查詢SETUP_PUBLIC-ACH參數設定有誤:" + SETUP_PUBLIC_RC;
                        logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                        return "B0016:" + logger.strJobQueue;
                }
                #endregion

                //非設定之公用事業參數則結束處理程序
                if (SETUP_PUBLIC_Query_Count == 0)
                {
                    logger.strJobQueue = "公用事業參數檔中無設定需報送ACH的公用事業單位，不執行。";
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
                    logger.strJobQueue = "公用事業參數檔中無設定需報送財金格式的公用事業單位，不執行。";
                    logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                    return "B0016:" + logger.strJobQueue;
                }
                #endregion
                #region 宣告檔案路徑
                Cybersoft.Base.CMCURL001 CMCURL = new CMCURL001();
                //XML名稱
                strXML_Layout = CMCURL.getURL(strRecNAME + ".xml");

                switch (strTRAN_TYPE.ToString().Trim())
                {
                    case "ACTIVE":      //主動行
                        //收取檔案路徑
                        FILE_PATH = CMCURL.getPATH(strRecNAME + "_STMTIN_ACTIVE");
                        WK_PAY_TYPE = "ACHA";
                        //PAY_TYPE = "0031";
                        //SOURCE_CODE = "'0031'";
                        //ACH_DESC = "陽明山瓦斯";
                        break;

                    case "PASSIVE":     //被動行
                        //收取檔案路徑
                        FILE_PATH = CMCURL.getPATH(strRecNAME + "_STMTIN_PASSIVE");
                        WK_PAY_TYPE = "ACHP";
                        //PAY_TYPE = "'0005','0007','0009','0010','0011','0012','0013','0014','0015','0017','0018','0024','0025','0027','0028','0031','0032','0033','0034','0036'";
                        //SOURCE_CODE = "PU00000016";
                        //ACH_DESC = "ACH瓦斯";
                        break;

                    default:
                        logger.strJobQueue = "扣繳來源有誤, 請確認!! " + strInPAY_TYPE.ToString().Trim();
                        logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                        return "B0099:" + logger.strJobQueue;
                }

                if (strXML_Layout == "" || FILE_PATH == "")
                {
                    logger.strJobQueue = "路徑取得錯誤!!! PUBLIC_ACH.xml路徑為 " + strXML_Layout + " ACH_STMTIN路徑為 " + FILE_PATH;
                    logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                    return "B0099:" + logger.strJobQueue;
                }
                else
                {
                    FILE_PATH = FILE_PATH.Replace("yyyymmdd", temp_yyyymmdd);
                    logger.strJobQueue = "PUBLIC_ACH.xml路徑為 " + strXML_Layout + " ACH_STMTIN路徑為 " + FILE_PATH;
                    logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
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

                MAIN_ROUTINE();

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
        void MAIN_ROUTINE()
        {

            #region 刪除今日新增之ACH代繳資料
            String WK_SEQ_11 = TODAY_PROCESS_DTE.ToString("yyMMdd") + WK_PAY_TYPE + WK_BATCH;
            PUBLIC_HIST.strWhereMNT_USER = "PBBAHI001";
            PUBLIC_HIST_RC = PUBLIC_HIST.delete_for_public(WK_SEQ_11);

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
                    throw new System.Exception("B0016：" + logger.strJobQueue);
            }
            #endregion

            #region 刪除今日新增之ACH代繳資料
            PUBLIC_LIST_RC = PUBLIC_LIST.delete_for_public(WK_SEQ_11, "");

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
                        logger.strJobQueue = PUBLIC_LIST_RC + " 本日PUBLIC_LIST FOR ACH 無資料刪除";
                        logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                    }
                    break;

                default: //資料庫錯誤
                    logger.strJobQueue = "刪除PUBLIC_LIST FOR ACH 資料錯誤:" + PUBLIC_LIST_RC;
                    logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                    throw new System.Exception("B0016：" + logger.strJobQueue);
            }
            #endregion

            #region 複製Table定義
            PUBLIC_HIST_T.table_define();
            PUBLIC_LIST_T.table_define();
            #endregion

            #region 載入檔案格式資訊
            FileParseByXml xml = new FileParseByXml();

            // REC Layout
            DataTable REC_DataTable = xml.Xml2DataTable(strXML_Layout, "ACH_STMTIN");

            if (xml.strMSG.Length > 0)
            {
                logger.strJobQueue = "[Xml2DataTable(REC)] - " + xml.strMSG.ToString().Trim();
                logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                throw new System.Exception("B0099：" + logger.strJobQueue);
            }

            #endregion

            #region 設定檔案編碼
            Encoding BIG5 = Encoding.GetEncoding("big5");
            #endregion

            #region 將資料轉至DATA_TABLE
            strOutFileName = FILE_PATH;
            using (StreamReader srInFile = new StreamReader(strOutFileName, BIG5))
            {
                string strInfData;

                intReadInfDataCount = 0;
                
                int intRECCount = 0;

                while ((strInfData = srInFile.ReadLine()) != null && PROCESS_FLAG == "Y")
                {
                    intReadInfDataCount++;

                    #region 檔案格式檢核(筆資料長度)

                    if (strInfData.Length != intDataLength)
                    {
                        logger.strJobQueue = "ACH檔中第" + intReadInfDataCount + "筆的長度有誤，請通知系統人員! 該筆實際長度為 " + strInfData.Length;
                        logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                        throw new System.Exception("B0099：" + logger.strJobQueue);
                    }

                    #endregion

                    DataTable InfData_DataTable = new DataTable();

                    intRECCount++;

                    #region 依 Layout 拆解資料
                    InfData_DataTable = xml.FileLine2DataTable(BIG5, strInfData, REC_DataTable);
                    if (xml.strMSG.Length > 0)
                    {
                        logger.strJobQueue = "[FileLine2DataTable(REC)] (L" + intReadInfDataCount + ") - " + xml.strMSG.ToString().Trim();
                        logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                        throw new System.Exception("B0099：" + logger.strJobQueue);
                    }
                    #endregion

                    #region 檢核明細資料
                    PAY_TYPE = Convert.ToString(InfData_DataTable.Rows[0]["CID"]).Substring(0, 4);
                    PAY_NBR = Convert.ToString(InfData_DataTable.Rows[0]["USER_NBR"]);
                    PAY_ACCT_NBR = Convert.ToString(InfData_DataTable.Rows[0]["PAY_ACCT_NBR"]);
                    //CARD_NBR = Convert.ToString(InfData_DataTable.Rows[0]["ACCT_NBR"]);
                    //取得信用卡卡號
                    if ("Y".Equals(strPayCardTrans))
                    {
                        TRANSFER_PAY_NBR();
                    }
                    else
                    {
                        CARD_NBR = PAY_ACCT_NBR;
                    }

                    //將結果寫入代繳暫存檔中
                    PUBLIC_HIST_T.initInsert_row();
                    x = PUBLIC_HIST_T.resultTable.Rows.Count - 1;

                    //組扣款序號 檔案傳送日期(6碼,年為西元後兩碼)+代繳類別(4碼)+批號(1碼)+序號(5碼) = 16碼
                    WK_SEQ = new StringBuilder();
                    WK_SEQ.Append(TRANS_DTE);
                    WK_SEQ.Append(WK_PAY_TYPE);
                    WK_SEQ.Append(WK_BATCH);
                    WK_SEQ.Append(intRECCount.ToString().PadLeft(5, '0'));

                    //找出客戶ID序號
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
                            CUST_SEQ = Convert.ToString(CARD_INF.resultTable.Rows[0]["CUST_NBR"]);
                            CARD_PRODUCT = Convert.ToString(CARD_INF.resultTable.Rows[0]["CARD_PRODUCT"]);
                            break;

                        case "F0023": //查無資料
                            logger.strJobQueue = "查無此徵信號碼 : " + CARD_NBR;
                            logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                            break;

                        default: //資料庫錯誤
                            logger.strJobQueue = "查詢 CARD_INF 資料錯誤:" + CARD_INF_RC;
                            logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                            throw new System.Exception("B0016：" + logger.strJobQueue);
                    }

                    PUBLIC_HIST_T.resultTable.Rows[x]["TRANS_DTE"] = TODAY_PROCESS_DTE.ToString("yyyyMMdd");
                    PUBLIC_HIST_T.resultTable.Rows[x]["PAY_TYPE"] = PAY_TYPE;
                    PUBLIC_HIST_T.resultTable.Rows[x]["BU"] = BU;
                    PUBLIC_HIST_T.resultTable.Rows[x]["ACCT_NBR"] = ACCT_NBR;
                    PUBLIC_HIST_T.resultTable.Rows[x]["PRODUCT"] = "000";
                    PUBLIC_HIST_T.resultTable.Rows[x]["CARD_PRODUCT"] = CARD_PRODUCT;
                    PUBLIC_HIST_T.resultTable.Rows[x]["CURRENCY"] = "TWD";
                    //PUBLIC_HIST_T.resultTable.Rows[x]["PAY_CARD_NBR"] = "";
                    PUBLIC_HIST_T.resultTable.Rows[x]["PAY_CARD_NBR"] = CARD_NBR;
                    PUBLIC_HIST_T.resultTable.Rows[x]["EXPIR_DTE"] = "";
                    PUBLIC_HIST_T.resultTable.Rows[x]["CUST_SEQ"] = CUST_SEQ;
                    PUBLIC_HIST_T.resultTable.Rows[x]["PAY_NBR"] = PAY_NBR;
                    PUBLIC_HIST_T.resultTable.Rows[x]["PAY_AMT"] = Convert.ToDecimal(InfData_DataTable.Rows[0]["TX_AMT"]);
                    PUBLIC_HIST_T.resultTable.Rows[x]["PAY_DTE"] = TODAY_PROCESS_DTE; ;
                    PUBLIC_HIST_T.resultTable.Rows[x]["PAY_SEQ"] = WK_SEQ.ToString();
                    if (Convert.ToString(InfData_DataTable.Rows[0]["REJECT_CODE"]) != "00")   //公用事業單位通知止扣
                    {
                        PUBLIC_HIST_T.resultTable.Rows[x]["PAY_RESULT"] = "I008";
                        PUBLIC_HIST_T.resultTable.Rows[x]["USER_FIELD_2"] = Convert.ToString(InfData_DataTable.Rows[0]["REJECT_CODE"]);
                    }
                    PUBLIC_HIST_T.resultTable.Rows[x]["MNT_DT"] = TODAY_PROCESS_DTE;
                    PUBLIC_HIST_T.resultTable.Rows[x]["MNT_USER"] = "PBBAHI001";

                    //統計筆數及金額
                    WK_TOTAL_CNT = WK_TOTAL_CNT + 1;
                    WK_TOTAL_AMT = WK_TOTAL_AMT + Convert.ToDecimal(InfData_DataTable.Rows[0]["TX_AMT"]);
                    #endregion

                    #region 將資料寫至PUBLIC_LIST
                    if (PROCESS_FLAG == "Y")
                    {
                        PUBLIC_LIST_T.initInsert_row();
                        i = PUBLIC_LIST_T.resultTable.Rows.Count - 1;

                        // 組扣款序號 檔案傳送日期(6碼,年為西元後兩碼)+代繳類別(4碼)+批號(1碼)+序號(5碼) = 16碼
                        WK_SEQ = new StringBuilder();
                        WK_SEQ.Append(TRANS_DTE);
                        WK_SEQ.Append(WK_PAY_TYPE);
                        WK_SEQ.Append(WK_BATCH);
                        WK_SEQ.Append(PUBLIC_LIST_T.resultTable.Rows.Count.ToString().PadLeft(5, '0'));

                        PUBLIC_LIST_T.resultTable.Rows[i]["PROCESS_DTE"] = TODAY_PROCESS_DTE.ToString("yyyyMMdd");
                        PUBLIC_LIST_T.resultTable.Rows[i]["RETURN_DTE"] = TODAY_PROCESS_DTE.ToString("yyyyMMdd");
                        PUBLIC_LIST_T.resultTable.Rows[i]["PAY_TYPE"] = PAY_TYPE;
                        PUBLIC_LIST_T.resultTable.Rows[i]["PAY_SEQ"] = WK_SEQ.ToString();
                        PUBLIC_LIST_T.resultTable.Rows[i]["PAY_DATA_AREA"] = strInfData.Substring(0, intDataLength).ToString();
                        PUBLIC_LIST_T.resultTable.Rows[i]["MNT_DT"] = TODAY_PROCESS_DTE;
                        PUBLIC_LIST_T.resultTable.Rows[i]["MNT_USER"] = "PBBAHI001";

                    }
                    #endregion
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
                    throw new System.Exception("B0012：" + logger.strJobQueue);
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
                    throw new System.Exception("B0012：" + logger.strJobQueue);
                }

                logger.strJobQueue = "整批新增至 PUBLIC_LIST 成功筆數 =" + PUBLIC_LIST_T.intInsCnt;
                logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
            }
            #endregion
        }

        #region TRANSFER_PAY_NBR()
        void TRANSFER_PAY_NBR()
        {
            //約定繳款帳號(14碼)-->信用卡號(16碼)
            SETUP_PRODUCT.init();
            SETUP_PRODUCT.strWherePRODUCT_SERVICE_3 = PAY_ACCT_NBR.Substring(2, 2);
            SETUP_PRODUCT_RC = SETUP_PRODUCT.query();
            switch (SETUP_PRODUCT_RC)
            {
                case "S0000": //查詢成功
                    BIN = Convert.ToString(SETUP_PRODUCT.resultTable.Rows[0]["BEG_NBR"]).Substring(0,6);
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
            CARD_NBR = BIN + PAY_ACCT_NBR.Substring(4,10);

            //ACH PAY_NBR轉換
            switch (PAY_TYPE)
            {
                case "0005":  //ACH大台北瓦斯費
                    PAY_NBR = "01" + PAY_NBR;
                    break;

                case "0007":  //ACH欣湖瓦斯費
                    PAY_NBR = "18" + PAY_NBR;
                    break;

                case "0009":  //ACH欣桃瓦斯費
                    PAY_NBR = "17" + PAY_NBR;
                    break;

                case "0010":  //ACH欣泰瓦斯費
                    PAY_NBR = "07" + PAY_NBR;
                    break;

                case "0011":  //ACH欣欣瓦斯費
                    PAY_NBR = "16" + PAY_NBR;
                    break;

                case "0012":  //ACH新海瓦斯費
                    PAY_NBR = "02" + PAY_NBR;
                    break;

                case "0013":  //ACH欣中瓦斯費
                    PAY_NBR = "04" + PAY_NBR;
                    break;

                case "0014":  //ACH欣彰瓦斯費
                    PAY_NBR = "19" + PAY_NBR;
                    break;

                case "0015":  //ACH欣南瓦斯費
                    PAY_NBR = "14" + PAY_NBR;
                    break;

                case "0017":  //ACH新竹縣瓦斯費
                    PAY_NBR = "13" + PAY_NBR;
                    break;

                case "0018":  //ACH新竹縣瓦斯費
                    PAY_NBR = "20" + PAY_NBR;
                    break;

                case "0024":  //ACH欣芝瓦斯費
                    PAY_NBR = "24" + PAY_NBR;
                    break;

                case "0025":  //ACH南鎮瓦斯費
                    PAY_NBR = "25" + PAY_NBR;
                    break;

                case "0027":  //ACH竹建瓦斯費
                    PAY_NBR = "27" + PAY_NBR;
                    break;

                case "0028":  //ACH欣嘉瓦斯費
                    PAY_NBR = "28" + PAY_NBR;
                    break;

                case "0031":  //ACH欣林瓦斯費
                    PAY_NBR = "09" + PAY_NBR;
                    break;

                case "0032":  //ACH欣雲瓦斯費
                    PAY_NBR = "10" + PAY_NBR;
                    break;

                case "0033":  //ACH欣營瓦斯費
                    PAY_NBR = "11" + PAY_NBR;
                    break;

                case "0034":  //ACH中油瓦斯費
                    PAY_NBR = "22" + PAY_NBR;
                    break;

                case "0036":  //ACH裕苗瓦斯費
                    PAY_NBR = "23" + PAY_NBR;
                    break;

                default: //資料庫錯誤
                    logger.strJobQueue = "PAY_TYPE = " + PAY_TYPE + ", 無轉換對應欄位";
                    logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                    break;
            }
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

            //今日處理之ACH資料
            logger.strTBL_NAME = "ACH_STMTIN";
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