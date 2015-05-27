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
    /// 產生市水扣帳回應檔,並將入帳交易先寫至TX_WAREHOUSE,待下一營業日入帳
    /// </summary>
    public class PBBPWO001
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
        int TPWATER_TOTAL_CNT = 0;
        Decimal TPWATER_TOTAL_AMT = 0;
        int TX_WAREHOUSE_T_Count_Insert = 0;
        int TPWATER_TXT_Count = 0;
        string temp_mmdd = "";      //檔名

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
        String temp_PAY_DTE = "";   //扣款日轉為民國年月日

        //市水回應檔欄位
        String TPWATER_H_STMTOUT = "";
        String[] TPWATER_STMTOUT = null;
        String PAY_RESULT_TPWATER = "";
        Decimal TPWATER_SUCCESS_AMT = 0;
        Decimal TPWATER_FAIL_AMT = 0;
        int TPWATER_SUCCESS_CNT = 0;
        int TPWATER_FAIL_CNT = 0;

        //市水遮罩欄位
        //String MASK_TYPE = "0000000**00000000000";  //第8~9碼躲起來

        //檔案長度
        const int intDataLength = 85;

        #endregion

        #region 宣告檔案路徑

        //XML放置路徑 
        String strXML_Layout = "";
        //寫出檔案名稱
        String strOutFileName = "";
        //寫出檔案路徑
        String FILE_PATH_H = "";
        String FILE_PATH_D = "";

        #endregion

        static readonly Cybersoft.Coca.Log.Logger logger = Cybersoft.Log.LogManager<Cybersoft.Coca.Log.Logger>.GetLogger(System.Reflection.MethodInfo.GetCurrentMethod().ReflectedType.Name);
        public string RUN()
        {
            #region 準備MSGLG.SYSOUT CLASS
            logger.strJobID = JobID;
            logger.strJOBNAME = "PBBPWO001";
            logger.dtRunDate = DateTime.Now;
            #endregion

            try
            {
                #region 取得系統資訊(前次批次日、本日批次日)
                CMCSYS001 SYSINF = new CMCSYS001();
                SYSINF.strJobID = JobID;
                SYSINF.strJobName = "PBBPWO001";
                String SYSINF_RC = SYSINF.getSYSINF();
                PREV_PROCESS_DTE = SYSINF.datetimePREV_PROCESS_DTE;
                TODAY_PROCESS_DTE = SYSINF.datetimeTODAY_PROCESS_DTE;
                NEXT_PROCESS_DTE = SYSINF.datetimeBANK_NEXT_PROCESS_DTE;
                temp_mmdd = TODAY_PROCESS_DTE.ToString("MMdd");
                strToday = TODAY_PROCESS_DTE.ToString("yyyyMMdd");
                #endregion

                #region 宣告檔案路徑
                Cybersoft.Base.CMCURL001 CMCURL = new CMCURL001();
                //XML名稱
                strXML_Layout = CMCURL.getURL("PUBLIC_TPWATER.xml");
                //寫出檔案路徑(Detail)
                FILE_PATH_D = CMCURL.getPATH("TPWATER_STMTOUT_D");

                if (strXML_Layout == "" || FILE_PATH_D == "")
                {
                    logger.strJobQueue = "路徑取得錯誤!!! PUBLIC_TPWATER.xml路徑為 " + strXML_Layout + " TPWATER_STMTOUT路徑為 " + FILE_PATH_D;
                    logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                    return "B0099:" + logger.strJobQueue;
                }
                else
                {
                    FILE_PATH_D = FILE_PATH_D.Replace("yyyymmdd", TODAY_PROCESS_DTE.ToString("yyyyMMdd"));
                    logger.strJobQueue = "PUBLIC_TPWATER.xml路徑為 " + strXML_Layout + " TPWATER_STMTOUT路徑為 " + FILE_PATH_D;
                    logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                }

                //寫出檔案路徑(Header)
                FILE_PATH_H = CMCURL.getPATH("TPWATER_STMTOUT_H");

                if (strXML_Layout == "" || FILE_PATH_H == "")
                {
                    logger.strJobQueue = "路徑取得錯誤!!! PUBLIC_TPWATER.xml路徑為 " + strXML_Layout + " TPWATER_STMTOUT路徑為 " + FILE_PATH_H;
                    logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                    return "B0099:" + logger.strJobQueue;
                }
                else
                {
                    FILE_PATH_H = FILE_PATH_H.Replace("yyyymmdd", TODAY_PROCESS_DTE.ToString("yyyyMMdd"));
                    logger.strJobQueue = "PUBLIC_TPWATER.xml路徑為 " + strXML_Layout + " TPWATER_STMTOUT路徑為 " + FILE_PATH_H;
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

                #region 擷取今日需送回應檔之市水資料
                PUBLIC_LIST.init();
                PUBLIC_LIST.strWherePAY_TYPE = "0002";
                PUBLIC_LIST_RC = PUBLIC_LIST.query_for_out(strToday, "");
                switch (PUBLIC_LIST_RC)
                {
                    case "S0000": //查詢成功
                        PUBLIC_LIST_Query_Count = PUBLIC_LIST.resultTable.Rows.Count;
                        TPWATER_STMTOUT = new string[PUBLIC_LIST_Query_Count];
                        break;

                    case "F0023": //無需處理資料
                        logger.strJobQueue = "今日無市水回應檔需產生;今日需送回應檔時間為大於 " + PREV_PROCESS_DTE + "且小於等於" + TODAY_PROCESS_DTE;
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

                #endregion

                for (i = 0; i < PUBLIC_LIST.resultTable.Rows.Count; i++)
                {
                    PAY_DATA_AREA = Convert.ToString(PUBLIC_LIST.resultTable.Rows[i]["PAY_DATA_AREA"]);
                    DataTable InfData_DataTable = new DataTable();
                    if (i == 0)
                    {
                        intREC_HCount++;

                        #region  組檔頭
                        TPWATER_H_STMTOUT = PAY_DATA_AREA.Substring(0, 43);
                        #endregion

                        #region 刪除TX_WAREHOUSE,RERUN 機制
                        TX_WAREHOUSE.init();
                        TX_WAREHOUSE.strWhereSOURCE_CODE = "PU00000002";
                        TX_WAREHOUSE.DateTimeWhereMNT_DT = TODAY_PROCESS_DTE;
                        TX_WAREHOUSE.strWhereMNT_USER = "PBBPWO001";
                        TX_WAREHOUSE_RC = TX_WAREHOUSE.delete();
                        logger.strJobQueue = "刪除TX_WAREHOUSE 筆數 " + TX_WAREHOUSE.intDelCnt;
                        logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                        #endregion

                        continue;
                    }

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
                            PAY_NBR = Convert.ToString(PUBLIC_HIST.resultTable.Rows[0]["PAY_NBR"]);
                            PAY_SEQ = Convert.ToString(PUBLIC_HIST.resultTable.Rows[0]["PAY_SEQ"]);
                            PAY_DTE = Convert.ToDateTime(PUBLIC_HIST.resultTable.Rows[0]["PAY_DTE"]);
                            temp_PAY_DTE = Convert.ToString(Convert.ToInt32(PAY_DTE.ToString("yyyy")) - 1911) + PAY_DTE.ToString("MMdd");
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
                    BILL_DTE = Convert.ToString(InfData_DataTable.Rows[0]["POST_DTE"]);

                    #endregion

                    #region 組明細及市水回應碼
                    //回應中心代碼說明 2:內部檢核有誤 8:授權失敗 9:處理成功

                    //O000~O009都表示成功
                    //if (PAY_RESULT == "S000" || PAY_RESULT == "O000" || PAY_RESULT == "O001" || PAY_RESULT == "O002" || PAY_RESULT == "O003" || PAY_RESULT == "O004" || PAY_RESULT == "O005" || PAY_RESULT == "O006" || PAY_RESULT == "O007" || PAY_RESULT == "O008" || PAY_RESULT == "O009")
                    //20110915調整回應碼
                    if (PAY_RESULT == "S000")
                    {
                        PAY_RESULT_TPWATER = "05";  //扣帳成功
                        TPWATER_SUCCESS_CNT = TPWATER_SUCCESS_CNT + 1;
                        TPWATER_SUCCESS_AMT = TPWATER_SUCCESS_AMT + TX_AMT;
                        TPWATER_STMTOUT[i - 1] = PAY_DATA_AREA.Substring(0, 98) + PAY_RESULT_TPWATER + temp_PAY_DTE;

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
                            TX_WAREHOUSE_T.resultTable.Rows[x]["SOURCE_CODE"] = "PU00000002";
                            TX_WAREHOUSE_T.resultTable.Rows[x]["MCC_CODE"] = "4900";
                            TX_WAREHOUSE_T.resultTable.Rows[x]["AUTH_CODE"] = AUTH_CODE;
                            TX_WAREHOUSE_T.resultTable.Rows[x]["SEQ"] = x.ToString().PadLeft(10, '0');
                            //偷偷把水號遮住
                            //TPWATER_MASK_STRING = CMCNBR001.GET_MASK(PAY_NBR, MASK_TYPE);
                            TX_WAREHOUSE_T.resultTable.Rows[x]["DESCR"] = Strings.StrConv("台北市自來水公司" + PAY_NBR, VbStrConv.Wide);
                            TX_WAREHOUSE_T.resultTable.Rows[x]["MT_TYPE"] = "D";
                            TX_WAREHOUSE_T.resultTable.Rows[x]["AMT"] = TX_AMT;
                            TX_WAREHOUSE_T.resultTable.Rows[x]["MNT_DT"] = TODAY_PROCESS_DTE;
                            TX_WAREHOUSE_T.resultTable.Rows[x]["MNT_USER"] = "PBBPWO001";
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
                        PUBLIC_HIST.strMNT_USER = "PBBPWO001";
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

                        continue;

                    }
                    //I001: 未申請
                    if (PAY_RESULT == "I001")
                    {
                        PAY_RESULT_TPWATER = "04";   //未申請代扣
                        TPWATER_FAIL_CNT = TPWATER_FAIL_CNT + 1;
                        TPWATER_FAIL_AMT = TPWATER_FAIL_AMT + TX_AMT;
                        TPWATER_STMTOUT[i - 1] = PAY_DATA_AREA.Substring(0, 98) + PAY_RESULT_TPWATER + temp_PAY_DTE;
                        //寫出成功/失敗明細報表
                        insertReport_TABLE();
                        continue;
                    }
                    //I002: 已終止
                    if (PAY_RESULT == "I002")
                    {
                        PAY_RESULT_TPWATER = "02";   //中止代繳
                        TPWATER_FAIL_CNT = TPWATER_FAIL_CNT + 1;
                        TPWATER_FAIL_AMT = TPWATER_FAIL_AMT + TX_AMT;
                        TPWATER_STMTOUT[i - 1] = PAY_DATA_AREA.Substring(0, 98) + PAY_RESULT_TPWATER + temp_PAY_DTE;
                        //寫出成功/失敗明細報表
                        insertReport_TABLE();
                        continue;
                    }
                    //I003無有效卡  I007: 卡片已到期
                    if (PAY_RESULT == "I003" || PAY_RESULT == "I007")
                    {
                        PAY_RESULT_TPWATER = "03";   //已終止代扣
                        TPWATER_FAIL_CNT = TPWATER_FAIL_CNT + 1;
                        TPWATER_FAIL_AMT = TPWATER_FAIL_AMT + TX_AMT;
                        TPWATER_STMTOUT[i - 1] = PAY_DATA_AREA.Substring(0, 98) + PAY_RESULT_TPWATER + temp_PAY_DTE;
                        //寫出成功/失敗明細報表
                        insertReport_TABLE();
                        continue;
                    }
                    //I004: 卡片未開卡
                    if (PAY_RESULT == "I004")
                    {
                        PAY_RESULT_TPWATER = "08";   //未開卡
                        TPWATER_FAIL_CNT = TPWATER_FAIL_CNT + 1;
                        TPWATER_FAIL_AMT = TPWATER_FAIL_AMT + TX_AMT;
                        TPWATER_STMTOUT[i - 1] = PAY_DATA_AREA.Substring(0, 98) + PAY_RESULT_TPWATER + temp_PAY_DTE;
                        //寫出成功/失敗明細報表
                        insertReport_TABLE();
                        continue;
                    }
                    //I006: 信用額度不足
                    if (PAY_RESULT == "I006")
                    {
                        PAY_RESULT_TPWATER = "01";   //信用額度不足
                        TPWATER_FAIL_CNT = TPWATER_FAIL_CNT + 1;
                        TPWATER_FAIL_AMT = TPWATER_FAIL_AMT + TX_AMT;
                        TPWATER_STMTOUT[i - 1] = PAY_DATA_AREA.Substring(0, 98) + PAY_RESULT_TPWATER + temp_PAY_DTE;
                        //寫出成功/失敗明細報表
                        insertReport_TABLE();
                        continue;
                    }
                    //其他錯誤
                    if (PAY_RESULT != "S000")
                    {
                        PAY_RESULT_TPWATER = "10";  //其他
                        TPWATER_FAIL_CNT = TPWATER_FAIL_CNT + 1;
                        TPWATER_FAIL_AMT = TPWATER_FAIL_AMT + TX_AMT;
                        TPWATER_STMTOUT[i - 1] = PAY_DATA_AREA.Substring(0, 98) + PAY_RESULT_TPWATER + temp_PAY_DTE;
                        //寫出成功/失敗明細報表
                        insertReport_TABLE();
                        continue;
                    }

                    #endregion


                }

                #region 產生回應檔 陣列 --> STMTOUT.TXT
                //產出檔案名稱(Header)
                strOutFileName = FILE_PATH_H;
                FileStream fsOutFile = new FileStream(strOutFileName, FileMode.Create, FileAccess.Write, FileShare.None);
                using (StreamWriter srOutFile = new StreamWriter(fsOutFile, BIG5))
                {
                    srOutFile.Write(TPWATER_H_STMTOUT);
                    srOutFile.Write("\r\n");
                    srOutFile.Flush();
                    srOutFile.Close();
                }
                fsOutFile.Close();

                //設定產出檔案名稱                
                strOutFileName = FILE_PATH_D;
                fsOutFile = new FileStream(strOutFileName, FileMode.Create, FileAccess.Write, FileShare.None);
                using (StreamWriter srOutFile = new StreamWriter(fsOutFile, BIG5))
                {
                    //逐筆寫出資料
                    for (int k = 0; k < PUBLIC_LIST.resultTable.Rows.Count - 1; k++)
                    {
                        srOutFile.Write(TPWATER_STMTOUT[k]);
                        srOutFile.Write("\r\n");
                        srOutFile.Flush();
                        TPWATER_TXT_Count = TPWATER_TXT_Count + 1;
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
            Decimal TPWATER_SUCCESS_FEE = 0;
            Decimal TPWATER_SUCCESS_NET_AMT = 0;

            TPWATER_TOTAL_CNT = TPWATER_SUCCESS_CNT + TPWATER_FAIL_CNT;
            TPWATER_TOTAL_AMT = TPWATER_SUCCESS_AMT + TPWATER_FAIL_AMT;

            TPWATER_SUCCESS_FEE = Convert.ToDecimal(TPWATER_SUCCESS_CNT * 3);
            TPWATER_SUCCESS_NET_AMT = TPWATER_SUCCESS_AMT - TPWATER_SUCCESS_FEE;

            //設定特殊欄位資料
            ArrayList alSumData = new ArrayList();
            alSumData.Add(new ExcelFactory.ListItem("#RPT_DATE", TODAY_PROCESS_DTE.ToString("yyyy/MM/dd")));
            alSumData.Add(new ExcelFactory.ListItem("#TOTAL_CNT", TPWATER_TOTAL_CNT + " 筆"));
            alSumData.Add(new ExcelFactory.ListItem("#TOTAL_AMT", TPWATER_TOTAL_AMT));
            alSumData.Add(new ExcelFactory.ListItem("#SUCCESS_CNT", TPWATER_SUCCESS_CNT + " 筆"));
            alSumData.Add(new ExcelFactory.ListItem("#SUCCESS_AMT", TPWATER_SUCCESS_AMT));
            alSumData.Add(new ExcelFactory.ListItem("#FAIL_CNT", TPWATER_FAIL_CNT + " 筆"));
            alSumData.Add(new ExcelFactory.ListItem("#FAIL_AMT", TPWATER_FAIL_AMT));
            alSumData.Add(new ExcelFactory.ListItem("#SUCCESS_FEE", TPWATER_SUCCESS_FEE.ToString("0")));
            alSumData.Add(new ExcelFactory.ListItem("#NET_SUCC_AMT", TPWATER_SUCCESS_NET_AMT.ToString("0")));
            //設定合併欄位資料
            ArrayList alMegData = new ArrayList();

            //產出報表
            CMCRPT001.Output(REPORT_TABLE, alSumData, alMegData, "PBRPWO001(台北市自來水扣繳成功失敗報表)", "PBRPWO001", "Sheet1", "Sheet1", TODAY_PROCESS_DTE, true);
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
            logger.strTBL_NAME = "TPWATER_STMTOUT";
            logger.strTBL_ACCESS = "I";
            logger.intTBL_COUNT = TPWATER_TXT_Count;
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

