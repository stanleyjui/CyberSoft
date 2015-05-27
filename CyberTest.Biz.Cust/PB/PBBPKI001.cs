﻿using System;
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
    /// 將停車費(台北市、台北縣、台南市、高雄市)資料寫入代繳紀錄檔中(營業日執行)
    /// ABEND 處理:修正問題後可重新執行,若一日有兩批需處理,需改批號;但若該批資料已至授權階段將不可再執行
    /// </summary>
    public class PBBPKI001
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
        SETUP_PRODUCTDao SETUP_PRODUCT = new SETUP_PRODUCTDao();
        #endregion

        #region 宣告常數
        const string strJOBNAME = "PBBPKI001";
        #endregion

        #region 宣告共用變數
        //批次日期        
        DateTime TODAY_PROCESS_DTE = new DateTime();
        DateTime NEXT_PROCESS_DTE = new DateTime();

        //序號組合
        String TRANS_DTE = "";
        String PAY_TYPE = "";
        String WK_BATCH = "1";     //固定為1,若當天要處理兩批,第二批請改2
        StringBuilder WK_SEQ = new StringBuilder();

        //處理註記
        String PROCESS_FLAG = "Y";

        //主檔欄位
        String WK_PAY_DTE = "";
        DateTime PAY_DTE = new DateTime();

        String PAY_NBR = "";
        String BU = "";
        String ACCT_NBR = "";
        String CUST_SEQ = "";
        String CARD_PRODUCT = "";    //換號FLAG
        String CARD_NBR = "";

        //檔尾資料
        Decimal TOTAL_AMT = 0;
        Decimal TOTAL_CNT = 0;

        const int intHeadLength = 130;  //檔案HEADER長度
        const int intDataLength = 132;  //檔案Detail長度
        const int intTrailLength = 130; //檔案Trailer長度
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

        #endregion

        static readonly Cybersoft.Coca.Log.Logger logger = Cybersoft.Log.LogManager<Cybersoft.Coca.Log.Logger>.GetLogger(System.Reflection.MethodInfo.GetCurrentMethod().ReflectedType.Name);
        public string RUN(String strSOURCE, string BATCH_NO)
        {
            #region 準備MSGLG.SYSOUT CLASS
            logger.strJobID = JobID;
            logger.strJOBNAME = "PBBPKI001";
            logger.dtRunDate = DateTime.Now;
            #endregion

            try
            {
                #region 取得系統資訊(前次批次日、本日批次日)
                CMCSYS001 SYSINF = new CMCSYS001();
                SYSINF.strJobID = JobID;
                SYSINF.strJobName = "PBBPKI001";
                String SYSINF_RC = SYSINF.getSYSINF();
                TODAY_PROCESS_DTE = SYSINF.datetimeTODAY_PROCESS_DTE;
                NEXT_PROCESS_DTE = SYSINF.datetimeBANK_NEXT_PROCESS_DTE;
                TRANS_DTE = TODAY_PROCESS_DTE.ToString("yyMMdd");
                temp_yyyymmdd = TODAY_PROCESS_DTE.ToString("yyyyMMdd");
                #endregion

                WK_BATCH = BATCH_NO;

                #region 取得檔案路徑
                Cybersoft.Base.CMCURL001 CMCURL = new CMCURL001();
                //XML名稱
                strXML_Layout = CMCURL.getURL("PUBLIC_PARKING.xml");

                switch (strSOURCE.ToString().Trim())
                {
                    case "TPE":     //台北市停車費
                        //收取檔案路徑
                        FILE_PATH = CMCURL.getPATH("TPE_STMTIN");
                        PAY_TYPE = "0023";
                        break;

                    case "TPC":     //台北縣停車費
                        //收取檔案路徑
                        FILE_PATH = CMCURL.getPATH("TPC_STMTIN");
                        PAY_TYPE = "0021";
                        break;

                    case "PRK":     //台南市停車費
                        //收取檔案路徑
                        FILE_PATH = CMCURL.getPATH("PRK_STMTIN");
                        PAY_TYPE = "0006";
                        break;

                    case "KAO":     //高雄市停車費
                        //收取檔案路徑
                        FILE_PATH = CMCURL.getPATH("KAO_STMTIN");
                        PAY_TYPE = "0022";
                        break;

                    default:
                        strXML_Layout = "";
                        FILE_PATH = "";
                        break;
                }

                if (strXML_Layout == "" || FILE_PATH == "")
                {
                    logger.strJobQueue = "路徑取得錯誤!!! PUBLIC_" + strSOURCE + ".xml路徑為 " + strXML_Layout + " ," + strSOURCE + "_STMTIN路徑為 " + FILE_PATH;
                    logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                    return "B0099:" + logger.strJobQueue;
                }
                else
                {
                    FILE_PATH = FILE_PATH.Replace("yyyymmdd", TODAY_PROCESS_DTE.ToString("yyyyMMdd"));
                    logger.strJobQueue = "PUBLIC_" + strSOURCE + ".xml路徑為 " + strXML_Layout + " ," + strSOURCE + "_STMTIN路徑為 " + FILE_PATH;
                    logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                }

                string Check_RC = CMCURL.isFileExists(FILE_PATH).ToString();
                if (Check_RC.Substring(0, 5) != "S0000")
                {
                    logger.strJobQueue = Check_RC.Trim();
                    logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                    return "B0000";
                }
                #endregion

                MAIN_ROUTINE(strSOURCE);

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

        #region MAIN_ROUTINE()  主邏輯段
        void MAIN_ROUTINE(String strSOURCE)
        {
            #region 宣告檔案路徑
            #region 刪除今日新增之停車費代繳資料(FOR RERUN)
            String WK_SEQ_11 = TODAY_PROCESS_DTE.ToString("yyMMdd") + PAY_TYPE + WK_BATCH;
            PUBLIC_HIST.strWhereMNT_USER = strJOBNAME;
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

            #region 複製Table定義
            PUBLIC_HIST_T.table_define();
            PUBLIC_LIST_T.table_define();
            #endregion

            #region 載入檔案格式資訊
            FileParseByXml xml = new FileParseByXml();

            // REC_H Layout
            DataTable REC_H_DataTable = xml.Xml2DataTable(strXML_Layout, "REC_H");

            if (xml.strMSG.Length > 0)
            {
                logger.strJobQueue = "[Xml2DataTable(REC_H)] - " + xml.strMSG.ToString().Trim();
                logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                throw new System.Exception("B0099：" + logger.strJobQueue);
            }

            // REC Layout
            DataTable REC_DataTable = xml.Xml2DataTable(strXML_Layout, "REC");

            if (xml.strMSG.Length > 0)
            {
                logger.strJobQueue = "[Xml2DataTable(REC)] - " + xml.strMSG.ToString().Trim();
                logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                throw new System.Exception("B0099：" + logger.strJobQueue);
            }

            // REC_T Layout
            DataTable REC_T_DataTable = xml.Xml2DataTable(strXML_Layout, "REC_T");

            if (xml.strMSG.Length > 0)
            {
                logger.strJobQueue = "[Xml2DataTable(REC_T)] - " + xml.strMSG.ToString().Trim();
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

                int intRECHCount = 0;
                int intRECCount = 0;
                int intRECTCount = 0;

                while ((strInfData = srInFile.ReadLine()) != null && PROCESS_FLAG == "Y")
                {
                    intReadInfDataCount++;

                    #region 檔案格式檢核(筆資料長度)

                    if (!((strInfData.Length == intHeadLength) ||
                          (strInfData.Length == intDataLength) ||
                          (strInfData.Length == intTrailLength)))
                    {
                        logger.strJobQueue = "停車費檔中第" + intReadInfDataCount + "筆的長度有誤，請通知系統人員! 該筆實際長度為 " + strInfData.Length;
                        logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                        throw new System.Exception("B0099：" + logger.strJobQueue);
                    }

                    #endregion

                    DataTable InfData_DataTable = new DataTable();

                    string strTag = strInfData.Substring(0, 1).ToString().Trim();

                    switch (strTag)
                    {
                        case "1":  //檔頭資料
                            intRECHCount++;

                            #region 依 Layout 拆解資料
                            InfData_DataTable = xml.FileLine2DataTable(BIG5, strInfData, REC_H_DataTable);
                            if (xml.strMSG.Length > 0)
                            {
                                logger.strJobQueue = "[FileLine2DataTable(REC_H)] (L" + intReadInfDataCount + ") - " + xml.strMSG.ToString().Trim();
                                logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                                throw new System.Exception("B0099：" + logger.strJobQueue);
                            }
                            #endregion

                            #region 檔頭檢核
                            //處理日期必須大於今日批次日,若等於今日批次日需人工介入
                            WK_PAY_DTE = Convert.ToString(Convert.ToInt16(InfData_DataTable.Rows[0]["PAY_DTE"].ToString().Substring(0, 3)) + 1911) + InfData_DataTable.Rows[0]["PAY_DTE"].ToString().Substring(3, 4);
                            PAY_DTE = DateTime.ParseExact(WK_PAY_DTE, "yyyyMMdd", null, System.Globalization.DateTimeStyles.AllowWhiteSpaces);
                            if (PAY_DTE == NEXT_PROCESS_DTE)
                            {
                                PROCESS_FLAG = "Y";
                                logger.strJobQueue = "指定入扣帳日為" + PAY_DTE + "等於下個批次日" + NEXT_PROCESS_DTE + ", 符合處理日期區間!!!";
                                logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                            }
                            else
                            {
                                PROCESS_FLAG = "N";
                                logger.strJobQueue = "指定入扣帳日為" + PAY_DTE + "不等於下個批次日" + NEXT_PROCESS_DTE + ", 不符合處理日期區間!!!";
                                logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                            }
                            #endregion

                            if (PROCESS_FLAG == "Y")
                            {

                                #region 刪除今日新增之停車費代繳清單(rerun準備)
                                PUBLIC_LIST.init();
                                PUBLIC_LIST.strWherePROCESS_DTE = TODAY_PROCESS_DTE.ToString("yyyyMMdd");
                                PUBLIC_LIST.strWhereRETURN_DTE = TODAY_PROCESS_DTE.ToString("yyyyMMdd");
                                PUBLIC_LIST.strWherePAY_TYPE = PAY_TYPE;
                                PUBLIC_LIST.strWhereMNT_USER = strJOBNAME;
                                PUBLIC_LIST_RC = PUBLIC_LIST.delete();

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
                                        throw new System.Exception("B0016：" + logger.strJobQueue);
                                }
                                #endregion

                                #region 檢核該批停車費是否已處理過
                                PUBLIC_LIST.init();
                                PUBLIC_LIST.strWhereRETURN_DTE = PAY_DTE.ToString("yyyyMMdd");
                                PUBLIC_LIST.strWherePAY_TYPE = PAY_TYPE;
                                PUBLIC_LIST.strWhereMNT_USER = strJOBNAME;
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
                                        throw new System.Exception("B0016：" + logger.strJobQueue);
                                }

                                #endregion

                            }

                            break;

                        case "2":  //明細資料
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
                            PAY_NBR = Convert.ToString(InfData_DataTable.Rows[0]["PAY_NBR"]).PadRight(7,' ').Substring(0, 7);
                            CARD_NBR = Convert.ToString(InfData_DataTable.Rows[0]["CARD_NBR"]);

                            //將結果寫入代繳暫存檔中
                            PUBLIC_HIST_T.initInsert_row();
                            x = PUBLIC_HIST_T.resultTable.Rows.Count - 1;

                            //組扣款序號 檔案傳送日期(6碼,年為西元後兩碼)+代繳類別(4碼)+批號(1碼)+序號(5碼) = 16碼
                            WK_SEQ = new StringBuilder();
                            WK_SEQ.Append(TRANS_DTE);
                            WK_SEQ.Append(PAY_TYPE);
                            WK_SEQ.Append(WK_BATCH);
                            WK_SEQ.Append(intRECCount.ToString().PadLeft(5, '0'));

                            //找出客戶ID序號
                            BU = "";
                            CARD_PRODUCT = "";
                            ACCT_NBR = "";
                            CUST_SEQ = "";
                            CARD_INF.init();
                            CARD_INF.strWhereCARD_NBR = CARD_NBR;
                            CARD_INF_RC = CARD_INF.query();
                            switch (CARD_INF_RC)
                            {
                                case "S0000": //查詢成功
                                    BU = Convert.ToString(CARD_INF.resultTable.Rows[0]["BU"]);
                                    CARD_PRODUCT = Convert.ToString(CARD_INF.resultTable.Rows[0]["CARD_PRODUCT"]);
                                    ACCT_NBR = Convert.ToString(CARD_INF.resultTable.Rows[0]["ACCT_NBR"]);
                                    CUST_SEQ = Convert.ToString(CARD_INF.resultTable.Rows[0]["CUST_NBR"]);
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
                            PUBLIC_HIST_T.resultTable.Rows[x]["PAY_CARD_NBR"] = CARD_NBR;
                            PUBLIC_HIST_T.resultTable.Rows[x]["EXPIR_DTE"] = "";
                            PUBLIC_HIST_T.resultTable.Rows[x]["CUST_SEQ"] = CUST_SEQ;
                            PUBLIC_HIST_T.resultTable.Rows[x]["PAY_NBR"] = PAY_NBR;
                            PUBLIC_HIST_T.resultTable.Rows[x]["PAY_AMT"] = Convert.ToDecimal(InfData_DataTable.Rows[0]["AMOUNT"]);
                            PUBLIC_HIST_T.resultTable.Rows[x]["PAY_DTE"] = PAY_DTE;
                            PUBLIC_HIST_T.resultTable.Rows[x]["PAY_SEQ"] = WK_SEQ.ToString();
                            PUBLIC_HIST_T.resultTable.Rows[x]["MNT_DT"] = TODAY_PROCESS_DTE;
                            PUBLIC_HIST_T.resultTable.Rows[x]["MNT_USER"] = strJOBNAME;
                            

                            //統計筆數及金額
                            WK_TOTAL_CNT = WK_TOTAL_CNT + 1;
                            WK_TOTAL_AMT = WK_TOTAL_AMT + Convert.ToDecimal(InfData_DataTable.Rows[0]["AMOUNT"]);

                            #endregion

                            break;

                        case "3": //檔尾資料
                            intRECTCount++;

                            #region 依 Layout 拆解資料
                            InfData_DataTable = xml.FileLine2DataTable(BIG5, strInfData, REC_T_DataTable);
                            if (xml.strMSG.Length > 0)
                            {
                                logger.strJobQueue = "[FileLine2DataTable(REC_T)] (L" + intReadInfDataCount + ") - " + xml.strMSG.ToString().Trim();
                                logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                                throw new System.Exception("B0099：" + logger.strJobQueue);
                            }
                            #endregion

                            #region 檢核檔尾資料
                            TOTAL_AMT = Convert.ToDecimal(InfData_DataTable.Rows[0]["TOTAL_AMT"]);
                            TOTAL_CNT = Convert.ToDecimal(InfData_DataTable.Rows[0]["TOTAL_CNT"]);

                            //檢核筆數
                            if (TOTAL_CNT != WK_TOTAL_CNT)
                            {
                                PROCESS_FLAG = "N";
                                logger.strJobQueue = "明細實際筆數 " + WK_TOTAL_CNT + " 和檔尾筆數 " + TOTAL_CNT + "不合 !!!";
                                logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                                //break;
                                throw new System.Exception("B0099：" + logger.strJobQueue);
                            }

                            //檢核金額
                            if (TOTAL_AMT != WK_TOTAL_AMT)
                            {
                                PROCESS_FLAG = "N";
                                logger.strJobQueue = "明細實際金額 " + WK_TOTAL_AMT + " 和檔尾金額 " + TOTAL_AMT + "不合 !!!";
                                logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                                //break;
                                throw new System.Exception("B0099：" + logger.strJobQueue);
                            }

                            #endregion

                            break;

                        default: //資料庫錯誤
                            throw new System.Exception("B0016：" + "停車費代扣繳 REC_TYPE ERROR " + strTag + logger.strJobQueue);

                    }

                    #region 將資料寫至PUBLIC_LIST
                    if (PROCESS_FLAG == "Y")
                    {
                        PUBLIC_LIST_T.initInsert_row();
                        i = PUBLIC_LIST_T.resultTable.Rows.Count - 1;

                        // 組扣款序號 檔案傳送日期(6碼,年為西元後兩碼)+代繳類別(4碼)+批號(1碼)+序號(5碼) = 16碼
                        WK_SEQ = new StringBuilder();
                        WK_SEQ.Append(TRANS_DTE);
                        WK_SEQ.Append(PAY_TYPE);
                        WK_SEQ.Append(WK_BATCH);
                        WK_SEQ.Append(i.ToString().PadLeft(5, '0'));

                        PUBLIC_LIST_T.resultTable.Rows[i]["PROCESS_DTE"] = TODAY_PROCESS_DTE.ToString("yyyyMMdd");
                        PUBLIC_LIST_T.resultTable.Rows[i]["RETURN_DTE"] = PAY_DTE.ToString("yyyyMMdd");
                        PUBLIC_LIST_T.resultTable.Rows[i]["PAY_TYPE"] = PAY_TYPE;
                        PUBLIC_LIST_T.resultTable.Rows[i]["PAY_SEQ"] = WK_SEQ.ToString();
                        PUBLIC_LIST_T.resultTable.Rows[i]["MNT_USER"] = strJOBNAME;
                        PUBLIC_LIST_T.resultTable.Rows[i]["MNT_DT"] = TODAY_PROCESS_DTE;
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



            #endregion
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

            //今日處理之停車費資料
            logger.strTBL_NAME = "GAS_STMTIN";
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