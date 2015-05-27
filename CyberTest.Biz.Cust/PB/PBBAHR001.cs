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


namespace Cybersoft.Biz.Cust
{
    /// <summary>
    /// 讀ACH約定回覆檔，判斷約定失敗的資料，依此更新PUBLIC_APPLY，並寫出信用卡代繳 ACH 異動失敗明細表(CAD0002A.txt)
    /// </summary>
    public class PBBAHR001
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
        //宣告PUBLIC_APPLY
        String PUBLIC_APPLY_RC = "";
        PUBLIC_APPLYDao PUBLIC_APPLY = new PUBLIC_APPLYDao();

        //暫存報表TABLE
        DataTable REPORT_TABLE = new DataTable();
        DataTable REPORT_TABLE_SORT = new DataTable();
        DataTable inSubtotTable = new DataTable();

        //宣告SETUP_PUBLIC
        SETUP_PUBLICDao SETUP_PUBLIC = new SETUP_PUBLICDao();
        DataTable SETUP_PUBLIC_DT = new DataTable();

        //宣告SETUP_PUBLIC
        SETUP_REJECTDao SETUP_REJECT = new SETUP_REJECTDao();
        DataTable SETUP_REJECT_DT = new DataTable();

        //檔案格式
        DataTable FH_xml_DataTable = new DataTable();
        DataTable FD_xml_DataTable = new DataTable();
        DataTable FT_xml_DataTable = new DataTable();
        //DataTable FH_DataTable = new DataTable();
        DataTable FD_DataTable = new DataTable();
        //DataTable FT_DataTable = new DataTable();

        FileParseByXml xml = new FileParseByXml();
        #endregion

        #region 設定檔案編碼
        Encoding BIG5 = Encoding.GetEncoding("big5");
        #endregion

        #region 副程式
        CMCNBR001 CMCNBR001 = new CMCNBR001();
        CMCURL001 CMCURL = new CMCURL001();
        #endregion

        #region 宣告共用變數

        //批次日期
        DateTime PREV_PROCESS_DTE = new DateTime();
        DateTime TODAY_PROCESS_DTE = new DateTime();
        DateTime NEXT_PROCESS_DTE = new DateTime();
        String TODAY_YYYYMMDD = "";

        //筆數&金額
        int PUBLIC_APPLY_Update_Count = 0;
        int k = 0; //REPORT_TABLE
        int intSuccessCount = 0;
        int intFailCount = 0;
        int intReadInfDataCount = 0;
        int SETUP_PUBLIC_Query_Count = 0;
        int SETUP_REJECT_Count = 0;

        //ACH約定回覆檔欄位
        String REPLY_DTE = "";
        String BR_NO = "";
        String BR_NAME = "";
        String PAY_TYPE = "";
        String TRANS_SEQ = "";
        String PAY_ACCT_NBR = "";
        String PAY_NBR = "";
        String CHANGE_TYPE = "";
        String CUST_ID = "";
        String DATA_DATE = "";
        String RUT_CODE = "";
        String strDESCR = "";

        //檔案長度
        const int intDataLength = 120;

        //參數
        String strRecNAME = "";        
        String strXmlURL = "";
        #endregion
        string strInPAY_TYPE = "";
        #region 宣告檔案路徑

        //寫出檔案名稱
        String strOutFileName = "";
        //寫出檔案路徑
        String FILE_PATH = "";


        #endregion

        static readonly Cybersoft.Coca.Log.Logger logger = Cybersoft.Log.LogManager<Cybersoft.Coca.Log.Logger>.GetLogger(System.Reflection.MethodInfo.GetCurrentMethod().ReflectedType.Name);
        //========================================================================================================
        #region 程式主邏輯【MAIN Routine】
        public string RUN()
        //public string RUN(string strInPAY_TYPE)
        {
            #region 準備MSGLG.SYSOUT CLASS
            logger.strJobID = JobID;
            logger.strJOBNAME = "PBBAHR001";
            logger.dtRunDate = DateTime.Now;
            #endregion

            try
            {
                #region 取得系統資訊(前次批次日、本日批次日)
                CMCSYS001 SYSINF = new CMCSYS001();
                SYSINF.strJobID = JobID;
                SYSINF.strJobName = "PBBAHR001";
                String SYSINF_RC = SYSINF.getSYSINF();
                PREV_PROCESS_DTE = SYSINF.datetimePREV_PROCESS_DTE;
                TODAY_PROCESS_DTE = SYSINF.datetimeTODAY_PROCESS_DTE;
                NEXT_PROCESS_DTE = SYSINF.datetimeNEXT_PROCESS_DTE;
                TODAY_YYYYMMDD = TODAY_PROCESS_DTE.ToString("yyyyMMdd");
                //for test
                //TODAY_PROCESS_DTE = DateTime.Parse("2012-09-18");
                //TODAY_YYYYMMDD = TODAY_PROCESS_DTE.ToString("yyyyMMdd");

                #endregion

                #region 讀取公共事業參數(KEY:交換單位)
                SETUP_PUBLIC.init();

                SETUP_PUBLIC.strWhereFILE_TRANSFER_UNIT = "ACH";
                SETUP_PUBLIC.strWherePOST_RESULT = "00";
                #region 可單獨執行指定PAY_TYPE，若參數值空白則分別產生全部ACH約定檔
                if (strInPAY_TYPE != "")
                {
                    SETUP_PUBLIC.strWherePAY_TYPE = strInPAY_TYPE;
                }
                #endregion
                string SETUP_PUBLIC_RC = SETUP_PUBLIC.query();
                switch (SETUP_PUBLIC_RC)
                {
                    case "S0000": //查詢成功
                        SETUP_PUBLIC_Query_Count = SETUP_PUBLIC.resultTable.Rows.Count;
                        //排序
                        SETUP_PUBLIC.resultTable.DefaultView.Sort = "PAY_TYPE ASC";
                        SETUP_PUBLIC.resultTable = SETUP_PUBLIC.resultTable.DefaultView.ToTable();
                        SETUP_PUBLIC_DT = SETUP_PUBLIC.resultTable;
                        break;

                    case "F0023": //無需處理資料
                        SETUP_PUBLIC_Query_Count = 0;
                        logger.strJobQueue = "公用事業參數檔中無設定需報送格式的公共事業單位，不執行。";
                        logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                        return "B0000:" + logger.strJobQueue;

                    default: //資料庫錯誤
                        logger.strJobQueue = "查詢SETUP_PUBLIC 資料錯誤:" + SETUP_PUBLIC_RC;
                        logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                        return "B0016:" + logger.strJobQueue;
                }
                
                // 同一傳送單位，檔案格式名稱需相同
                DataTable dtGroupSETUP_PUBLIC = SETUP_PUBLIC_DT.DefaultView.ToTable(true, "FILE_FORMAT");
                strRecNAME = dtGroupSETUP_PUBLIC.Rows[0]["FILE_FORMAT"].ToString();
                if (dtGroupSETUP_PUBLIC.Rows.Count > 1)
                {
                    logger.strJobQueue = "傳送同一單位之代扣格式，檔案格式名稱不同" + strRecNAME;
                    logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                    return "B0099:" + logger.strJobQueue;
                }

                #endregion
                
                #region 回覆代碼
                SETUP_REJECT.init();
                SETUP_REJECT.initWhere();
                //
                SETUP_REJECT.strWhereREJECT_GROUP = "PUBLIC";
                SETUP_REJECT.strWhereREJECT_CODE = "ACHR02";                
                string SETUP_REJECT_RC = SETUP_REJECT.query();
                switch (SETUP_REJECT_RC)
                {
                    case "S0000": //查詢成功
                        SETUP_REJECT_Count = SETUP_REJECT.resultTable.Rows.Count;                        
                        SETUP_REJECT_DT = SETUP_REJECT.resultTable;
                        break;

                    case "F0023": //無需處理資料
                        logger.strJobQueue = "未設定拒絕代碼";
                        logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                        break;

                    default: //資料庫錯誤
                        logger.strJobQueue = "查詢SETUP_REJECT 資料錯誤:" + SETUP_REJECT_RC;
                        logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                        return "B0016:" + logger.strJobQueue;
                }
                #endregion

                #region 宣告檔案路徑
                //寫出檔案路徑                
                if (strRecNAME != "")
                {
                    #region 宣告檔案路徑
                    Cybersoft.Base.CMCURL001 CMCURL = new CMCURL001();

                    //取得 XML格式
                    strXmlURL = CMCURL.getURL(strRecNAME + ".xml");

                    //檔案路徑 strFilePath
                    string strFilePath = CMCURL.getPATH(strRecNAME + "_CHGIN");
                    if (strFilePath == "")
                    {
                        logger.strJobQueue = "路徑取得錯誤!!!  <PATH> 未設定" + strRecNAME + "_CHGIN";
                        logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                        return "B0099:" + logger.strJobQueue;
                    }
                    //檔案名稱 strFileName
                    string strFileName = CMCURL.getFILE_NAME(strRecNAME + "_CHGIN");
                    if (strFileName == "")
                    {
                        logger.strJobQueue = "檔名取得錯誤!!!  <FILE_NAME> 未設定 : " + strRecNAME + "_CHGIN";
                        logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                        return "B0099:" + logger.strJobQueue;
                    }

                    //附檔名
                    String strEXT = CMCURL.getEXT(strRecNAME + "_CHGIN");
                    if (strEXT != "")
                    {
                        strFileName = strFileName + strEXT;
                    }

                    #endregion

                    #region 載入檔案格式資訊
                    if (strXmlURL == "")
                    {
                        logger.strJobQueue = "路徑取得錯誤!!! xml 路徑為 " + strXmlURL;
                        logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                        return "B0099:" + logger.strJobQueue;
                    }
                    else
                    {
                        //HADER
                        #region Layout (轉入XML的FH格式) ... 取消
                        //FH_xml_DataTable = xml.Xml2DataTable(strXmlURL, "ACHR02_FH");
                        //if (xml.strMSG.Length > 0)
                        //{
                        //    logger.strJobQueue = "[Xml2DataTable(ACHR02_FH)] - " + xml.strMSG.ToString().Trim();
                        //    logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                        //    return "B0099:" + logger.strJobQueue;
                        //}

                        #endregion
                        //BODY
                        #region Layout (轉入XML的FD格式)
                        FD_xml_DataTable = xml.Xml2DataTable(strXmlURL, "ACHR02_FD");
                        if (xml.strMSG.Length > 0)
                        {
                            logger.strJobQueue = "[Xml2DataTable(ACHR02_FD)] - " + xml.strMSG.ToString().Trim();
                            logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                            return "B0099:" + logger.strJobQueue;
                        }
                        #endregion
                        //TRAILER
                        #region Layout (轉入XML的FT格式) ... 取消
                        //FT_xml_DataTable = xml.Xml2DataTable(strXmlURL, "ACHR02_FT");
                        //if (xml.strMSG.Length > 0)
                        //{
                        //    logger.strJobQueue = "[Xml2DataTable(ACHR02_FT)] - " + xml.strMSG.ToString().Trim();
                        //    logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                        //    return "B0099:" + logger.strJobQueue;
                        //}
                        #endregion

                        //使用XML定義的LAYOUT定對暫存TABLE
                        //FH_DataTable = xml.dtXML2DataTable(FH_xml_DataTable);
                        FD_DataTable = xml.dtXML2DataTable(FD_xml_DataTable);
                        //FT_DataTable = xml.dtXML2DataTable(FT_xml_DataTable);
                    }
                    #endregion

                    string Check_RC = CMCURL.isFileExists(FILE_PATH).ToString();
                    if (Check_RC.Substring(0, 5) != "S0000")
                    {
                        logger.strJobQueue = Check_RC.Trim();
                        logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                        return "B0000";
                    }
                }
                else
                {
                    logger.strJobQueue = "參數檔未設定檔案格式";
                    logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                    return "B0099:" + logger.strJobQueue;
                }
                                
                #endregion

                #region 定義報表Table
                REPORT_TABLE.Columns.Add("KEY", typeof(string));
                REPORT_TABLE.Columns.Add("BR_NAME", typeof(string));
                REPORT_TABLE.Columns.Add("REPLY_DTE", typeof(string));
                REPORT_TABLE.Columns.Add("RPT_SEQ", typeof(string));
                REPORT_TABLE.Columns.Add("TRANS_SEQ", typeof(string));
                REPORT_TABLE.Columns.Add("PAY_NBR", typeof(string));
                REPORT_TABLE.Columns.Add("PAY_ACCT_NBR", typeof(string));
                REPORT_TABLE.Columns.Add("CUST_ID", typeof(string));
                REPORT_TABLE.Columns.Add("CHANGE_TYPE", typeof(string));
                REPORT_TABLE.Columns.Add("DATA_DATE", typeof(string));
                REPORT_TABLE.Columns.Add("RUT_CODE", typeof(string));
                #endregion

                #region 定義小計subtotal

                inSubtotTable.Columns.Add("POS_DTE_YYYYMMDD", typeof(string));
                inSubtotTable.Columns["POS_DTE_YYYYMMDD"].DefaultValue = TODAY_PROCESS_DTE.ToString("yyyy/MM/dd");

                inSubtotTable.Columns.Add("KEY", typeof(string));
                inSubtotTable.Columns["KEY"].DefaultValue = "";

                inSubtotTable.Columns.Add("BR_NAME", typeof(string));
                inSubtotTable.Columns["BR_NAME"].DefaultValue = "";

                inSubtotTable.Columns.Add("SUB_FAIL_CNT", typeof(string));
                inSubtotTable.Columns["SUB_FAIL_CNT"].DefaultValue = "";

                //inSubtotTable.Columns.Add("SUB_CNT", typeof(string));
                //inSubtotTable.Columns["SUB_CNT"].DefaultValue = "";


                #endregion

                #region 設定檔案編碼
                Encoding BIG5 = Encoding.GetEncoding("big5");
                #endregion

                #region 將資料轉至DATA_TABLE
                strOutFileName = FILE_PATH;
                using (StreamReader srInFile = new StreamReader(strOutFileName, BIG5))
                {
                    string strInfData;

                    while ((strInfData = srInFile.ReadLine()) != null)
                    {
                        intReadInfDataCount++;

                        #region 檔案格式檢核(筆資料長度)

                        if (strInfData.Length != intDataLength)
                        {
                            logger.strJobQueue = "ACH約定回覆檔中第" + intReadInfDataCount + "筆的長度有誤，請通知系統人員! 該筆實際長度為 " + strInfData.Length;
                            logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                            return "B0099:" + logger.strJobQueue;
                        }

                        #endregion

                        DataTable InfData_DataTable = new DataTable();

                        #region 依 Layout 拆解資料
                        InfData_DataTable = xml.FileLine2DataTable(BIG5, strInfData, FD_xml_DataTable);
                        if (xml.strMSG.Length > 0)
                        {
                            logger.strJobQueue = "[FileLine2DataTable(REC)] (L" + intReadInfDataCount + ") - " + xml.strMSG.ToString().Trim();
                            logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                            return "B0099:" + logger.strJobQueue;
                        }
                        #endregion

                        #region 檢核明細資料

                        PAY_TYPE = Convert.ToString(InfData_DataTable.Rows[0]["C_ID"]);
                        TRANS_SEQ = Convert.ToString(InfData_DataTable.Rows[0]["TRANS_SEQ"]);
                        PAY_NBR = Convert.ToString(InfData_DataTable.Rows[0]["USER_NO"]);
                        PAY_ACCT_NBR = Convert.ToString(InfData_DataTable.Rows[0]["R_CLNO"]);
                        CUST_ID = Convert.ToString(InfData_DataTable.Rows[0]["R_ID"]);
                        CHANGE_TYPE = Convert.ToString(InfData_DataTable.Rows[0]["AD_MARK"]);
                        DATA_DATE = Convert.ToString(InfData_DataTable.Rows[0]["DATA_DATE"]);
                        RUT_CODE = Convert.ToString(InfData_DataTable.Rows[0]["RUT_CODE"]);

                        #endregion

                        #region 將約定失敗的資料寫入報表Table & 更新PUBLIC_APPLY

                        if (RUT_CODE != "0")
                        {
                            intFailCount++;

                            #region 更新PUBLIC_APPLY 並取得 REPLY_DTE
                            PUBLIC_APPLY.init();
                            PUBLIC_APPLY.strWherePAY_TYPE = PAY_TYPE;
                            PUBLIC_APPLY.strWherePAY_NBR = "%" + PAY_NBR.Trim().Substring(2, PAY_NBR.Trim().Length - 2) + "%";
                            PUBLIC_APPLY.strWherePAY_ACCT_NBR = PAY_ACCT_NBR;
                            PUBLIC_APPLY.strWhereREPLY_FLAG = "Y";

                            #region 取得 REPLY_DTE、管理行代號、管理行名稱
                            PUBLIC_APPLY_RC = PUBLIC_APPLY.query_BRANCH_INF(PAY_ACCT_NBR, "PAY_ACCT_NBR");
                            switch (PUBLIC_APPLY_RC)
                            {
                                case "S0000": //查詢成功   
                                    int intYYY = Convert.ToInt32(Convert.ToString(PUBLIC_APPLY.resultTable.Rows[0]["REPLY_DTE"]).PadRight(8, ' ').Substring(0, 4)) - 1911; //民國年
                                    REPLY_DTE = Convert.ToString(intYYY) + Convert.ToString(PUBLIC_APPLY.resultTable.Rows[0]["REPLY_DTE"]).PadRight(8, ' ').Substring(4, 4);
                                    BR_NO = Convert.ToString(PUBLIC_APPLY.resultTable.Rows[0]["BR_NO"]);
                                    BR_NAME = Convert.ToString(PUBLIC_APPLY.resultTable.Rows[0]["BR_NAME"]);
                                    break;
                                case "F0023":
                                    REPLY_DTE = "";
                                    BR_NO = "0000";
                                    BR_NAME = "";
                                    break;
                                default: //資料庫錯誤
                                    logger.strJobQueue = "查詢PUBLIC_APPLY.query_BRANCH_INF() 資料錯誤:" + PUBLIC_APPLY_RC;
                                    logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                                    return "B0016:" + logger.strJobQueue;
                            }
                            #endregion

                            PUBLIC_APPLY.strERROR_REASON = strDESCR;
                            PUBLIC_APPLY.strERROR_REASON_DT = TODAY_PROCESS_DTE.ToString("yyyyMMdd");
                            PUBLIC_APPLY_RC = PUBLIC_APPLY.update_ACH_APPLY_RESULT();
                            switch (PUBLIC_APPLY_RC)
                            {
                                case "S0000": //更新成功   
                                    PUBLIC_APPLY_Update_Count = PUBLIC_APPLY_Update_Count + PUBLIC_APPLY.intUptCnt;
                                    if (PUBLIC_APPLY.intUptCnt == 0)
                                    {
                                        logger.strJobQueue = "更新PUBLIC_APPLY 資料錯誤: F0023, PAY_ACCT_NBR = " + PAY_ACCT_NBR + " PAY_NBR = " + PAY_NBR;
                                        logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                                        //return "F0023" + logger.strJobQueue;
                                    }
                                    break;

                                default: //資料庫錯誤
                                    logger.strJobQueue = "更新PUBLIC_APPLY 資料錯誤:" + PUBLIC_APPLY_RC;
                                    logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                                    return "B0016:" + logger.strJobQueue;
                            }

                            #endregion

                            insertReport_TABLE(); //失敗資料
                        }
                        else
                        {
                            intSuccessCount++;
                            //insertReport_TABLE();
                        }
                        #endregion

                    }
                }
                #endregion

                #region REPORT_TABLE 排序
                if (intFailCount > 0)
                {
                    REPORT_TABLE.DefaultView.Sort = "KEY, REPLY_DTE asc"; //KEY = BR_NO
                    REPORT_TABLE_SORT = REPORT_TABLE.DefaultView.ToTable();

                    string thisKEY = "";
                    string nextKEY = "";
                    int FAIL_CNT = 0;
                    int PAGE_CNT = 1;
                    int index = 0;
                    for (int s = 0; s < REPORT_TABLE_SORT.Rows.Count; s++)
                    {
                        thisKEY = REPORT_TABLE_SORT.Rows[s]["KEY"].ToString().Trim();
                        FAIL_CNT = FAIL_CNT + 1;
                        if (s < REPORT_TABLE_SORT.Rows.Count - 1)
                        {
                            nextKEY = REPORT_TABLE_SORT.Rows[s + 1]["KEY"].ToString().Trim();
                            if (thisKEY != nextKEY)
                            {
                                inSubtotTable.Rows.Add(inSubtotTable.NewRow());
                                inSubtotTable.Rows[index]["KEY"] = thisKEY;
                                inSubtotTable.Rows[index]["BR_NAME"] = REPORT_TABLE_SORT.Rows[s]["BR_NAME"].ToString().Trim();
                                inSubtotTable.Rows[index]["SUB_FAIL_CNT"] = FAIL_CNT.ToString("###,##0").PadLeft(7, ' ');
                                //inSubtotTable.Rows[index]["SUB_CNT"] = PAGE_CNT.ToString("##0").PadLeft(3, ' '); //頁數
                                FAIL_CNT = 0;
                                index = index + 1;
                                PAGE_CNT = PAGE_CNT + 1;
                            }
                        }
                        else
                        {
                            inSubtotTable.Rows.Add(inSubtotTable.NewRow());
                            inSubtotTable.Rows[index]["KEY"] = thisKEY;
                            inSubtotTable.Rows[index]["BR_NAME"] = REPORT_TABLE_SORT.Rows[s]["BR_NAME"].ToString().Trim();
                            inSubtotTable.Rows[index]["SUB_FAIL_CNT"] = FAIL_CNT.ToString("###,##0").PadLeft(7, ' ');
                            //inSubtotTable.Rows[index]["SUB_CNT"] = PAGE_CNT.ToString("##0").PadLeft(3, ' '); //頁數
                        }
                    }
                }
                #endregion

                if (intFailCount > 0)
                {
                    writeReport_CAD0002A(REPORT_TABLE_SORT);
                }

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
        #region insertReport_TABLE
        void insertReport_TABLE()
        {
            REPORT_TABLE.Rows.Add();
            k = REPORT_TABLE.Rows.Count - 1;//REPLY_DTE
            REPORT_TABLE.Rows[k]["KEY"] = BR_NO;
            REPORT_TABLE.Rows[k]["BR_NAME"] = BR_NAME;
            REPORT_TABLE.Rows[k]["REPLY_DTE"] = REPLY_DTE;
            REPORT_TABLE.Rows[k]["RPT_SEQ"] = REPORT_TABLE.Rows.Count.ToString().PadLeft(7, '0');
            REPORT_TABLE.Rows[k]["TRANS_SEQ"] = TRANS_SEQ.ToString().PadLeft(7, '0'); ;
            REPORT_TABLE.Rows[k]["PAY_NBR"] = PAY_NBR;
            REPORT_TABLE.Rows[k]["PAY_ACCT_NBR"] = PAY_ACCT_NBR;
            REPORT_TABLE.Rows[k]["CUST_ID"] = CUST_ID;
            REPORT_TABLE.Rows[k]["CHANGE_TYPE"] = CHANGE_TYPE;
            REPORT_TABLE.Rows[k]["DATA_DATE"] = DATA_DATE;

            #region 異動類別
            if (CHANGE_TYPE == "H")
                REPORT_TABLE.Rows[k]["CHANGE_TYPE"] = CHANGE_TYPE + "-新增";
            else if (CHANGE_TYPE == "E")
                REPORT_TABLE.Rows[k]["CHANGE_TYPE"] = CHANGE_TYPE + "-取消";
            else
                REPORT_TABLE.Rows[k]["CHANGE_TYPE"] = CHANGE_TYPE + "-異動類別不明";

            #endregion

            #region 回覆訊息
            //switch (RUT_CODE)
            //{
            //    case "0":
            //        if (CHANGE_TYPE == "E")
            //        {
            //            REPORT_TABLE.Rows[k]["RUT_CODE"] = RUT_CODE + "-取消授權成功";
            //        }
            //        else
            //        {
            //            REPORT_TABLE.Rows[k]["RUT_CODE"] = RUT_CODE + "-約定成功";
            //        }
            //        break;

            //    case "1":
            //        REPORT_TABLE.Rows[k]["RUT_CODE"] = RUT_CODE + "-印鑑不符";
            //        break;

            //    case "2":
            //        REPORT_TABLE.Rows[k]["RUT_CODE"] = RUT_CODE + "-無此帳號";
            //        break;

            //    case "3":
            //        REPORT_TABLE.Rows[k]["RUT_CODE"] = RUT_CODE + "-委繳戶統一編號不符";
            //        break;

            //    case "4":
            //        REPORT_TABLE.Rows[k]["RUT_CODE"] = RUT_CODE + "-已核印成功在案";
            //        break;

            //    case "5":
            //        REPORT_TABLE.Rows[k]["RUT_CODE"] = RUT_CODE + "-原交易不存在(取消)";
            //        break;

            //    case "6":
            //        REPORT_TABLE.Rows[k]["RUT_CODE"] = RUT_CODE + "-電子資料與授權書內容不符";
            //        break;

            //    case "7":
            //        REPORT_TABLE.Rows[k]["RUT_CODE"] = RUT_CODE + "-帳戶已結清";
            //        break;

            //    case "8":
            //        REPORT_TABLE.Rows[k]["RUT_CODE"] = RUT_CODE + "-印鑑不清";
            //        break;

            //    case "9":
            //        REPORT_TABLE.Rows[k]["RUT_CODE"] = RUT_CODE + "-其他";
            //        break;

            //    case "A":
            //        REPORT_TABLE.Rows[k]["RUT_CODE"] = RUT_CODE + "-未收到授權書";
            //        break;

            //    case "B":
            //        REPORT_TABLE.Rows[k]["RUT_CODE"] = RUT_CODE + "-用戶號碼錯誤";
            //        break;

            //    case "C":
            //        REPORT_TABLE.Rows[k]["RUT_CODE"] = RUT_CODE + "-靜止戶";
            //        break;

            //    case "D":
            //        REPORT_TABLE.Rows[k]["RUT_CODE"] = RUT_CODE + "-未收到聲明書";
            //        break;

            //    case "E":
            //        REPORT_TABLE.Rows[k]["RUT_CODE"] = RUT_CODE + "-授權書資料不全";
            //        break;

            //    case "F":
            //        REPORT_TABLE.Rows[k]["RUT_CODE"] = RUT_CODE + "-警示戶";
            //        break;

            //    case "G":
            //        REPORT_TABLE.Rows[k]["RUT_CODE"] = RUT_CODE + "-本帳戶不適用授權扣繳";
            //        break;

            //    case "H":
            //        REPORT_TABLE.Rows[k]["RUT_CODE"] = RUT_CODE + "-已於他行授權扣款";
            //        break;

            //    case "I":
            //        REPORT_TABLE.Rows[k]["RUT_CODE"] = RUT_CODE + "-該用戶已死亡";
            //        break;

            //    case "Z":
            //        REPORT_TABLE.Rows[k]["RUT_CODE"] = RUT_CODE + "-未交易或匯入失敗資料";
            //        break;

            //    default:
            //        REPORT_TABLE.Rows[k]["RUT_CODE"] = RUT_CODE + "-RUT_CODE不明";
            //        break;
            //}
            //拒絕代碼
            strDESCR = "";
            if (SETUP_REJECT_Count > 0)
	        {
                DataRow[] drREJECT = SETUP_REJECT_DT.Select(" REJECT_IND='" + RUT_CODE + "' ");
                if (drREJECT.Length == 1)
                {
                    REPORT_TABLE.Rows[k]["RUT_CODE"] = RUT_CODE + drREJECT[0]["DESCR"];
                }
                else
                {
                    REPORT_TABLE.Rows[k]["RUT_CODE"] = RUT_CODE + drREJECT[0]["DESCR"];
                    logger.strJobQueue = "代碼設定重複，請確認：" + RUT_CODE;
                    logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                }
	        }

            strDESCR = REPORT_TABLE.Rows[k]["RUT_CODE"].ToString();

            #endregion
        }
        #endregion

        #region writeReport_CAD0002A
        void writeReport_CAD0002A(DataTable inTable)
        {
            CMCRPT002 CMCRPT002 = new CMCRPT002();
            CMCRPT002.TemplateFileName = "CAD0002A";  //範本檔名
            CMCRPT002.isOverWrite = "Y";  //產出是否覆蓋既有檔案
            CMCRPT002.MaxPageCount = 50;  //產出報表每幾筆換一頁 
            //設定表頭欄位資料
            ArrayList alHeaderData = new ArrayList();
            alHeaderData.Add(new ListItem("RPT_DTE_YYYYMMDD", DateTime.Now.ToString("yyyy/MM/dd")));
            alHeaderData.Add(new ListItem("RPT_DTE_HHmm", DateTime.Now.ToString("HH:mm")));
            //設定表尾欄位資料
            ArrayList alTrailerData = new ArrayList();
            //設定小計資料表

            //產出報表(TXT)
            string rc = CMCRPT002.Output(inTable, alHeaderData, null, inSubtotTable, "CAD0002A", TODAY_PROCESS_DTE);
            if (rc != "")
            {
                logger.strJobQueue = rc;
                logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
            }
        }
        #endregion

        #region Display
        void writeDisplay()
        {
            logger.strJobID = "00002";

            //ACH約定回應檔筆數(含頭尾)
            logger.strTBL_NAME = "ACH_CHGIN";
            logger.strTBL_ACCESS = "I";
            logger.intTBL_COUNT = intReadInfDataCount;
            logger.writeCounter();

            //更新PUBLIC_APPLY
            logger.strTBL_NAME = "PUBLIC_APPLY";
            logger.strTBL_ACCESS = "U";
            logger.intTBL_COUNT = PUBLIC_APPLY_Update_Count;
            logger.writeCounter();

            logger.DisplayCounter(Cybersoft.Coca.Log.LevelEnum.Info);
        }
        #endregion
    }
}

