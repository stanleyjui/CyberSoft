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
using System.Net;

namespace Cybersoft.Biz.Cust
{
    /// <summary>
    /// 產生ACH約定檔
    /// </summary>
    public class PBBAHC001
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

        //宣告SETUP_PUBLIC
        SETUP_PUBLICDao SETUP_PUBLIC = new SETUP_PUBLICDao();
        DataTable SETUP_PUBLIC_DATATABLE = new DataTable();

        //發送email
        DataTable REPORT_DataTable_MAIL = new DataTable();

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
        String TODAY_YYYYMMDD = "";
        String TODAY_CH_DTE = "";
        String strBANK_NBR = "";

        //筆數&金額
        int PUBLIC_APPLY_Update_Count = 0;        
        int PUBLIC_APPLY_Update_Count2 = 0;
        int PUBLIC_APPLY_Update_Count3 = 0;
        int PUBLIC_APPLY_Query_Count = 0;
        int ACH_TXT_Count = 0;
        int SETUP_PUBLIC_Query_Count = 0;
        int PUBLIC_APPLY_RECOV_Count = 0;

        //ACH約定檔欄位
        String[] ACH_CHGOUT = null;
        //String TRANS_NBR = "585";
        String CHANGE_TYPE = "0";
        //String CUST_ID = "";
        //String ADDR_BRANCH = "";
        //String DATA_DATE = "";
        //String TRANS_TYPE = "N";
        String RUT_CODE = new String(' ', 1);
        String USER_FILLER = new String(' ', 12);
        String R_BANK = new String(' ', 7);
        //String P_BAMK = "0095185";
        String NOTE = new String(' ', 20);
        //int TRANS_SEQ = 1;
        int intFD_CNT = 0;
        //SETUP_PUBLIC參數設定
        string strCP_TIX = "";
        //string strCP_CID = "";
        string strCP_RBANK = "";
        string strCP_PBANK = "";
        string strWriteFD = "";

        //PUBLIC_APPLY欄位
        String PAY_CARD_NBR = "";
        //String PAY_ACCT_NBR = "";
        String PAY_NBR = "";
        String SEQ = "";
        String EXPIR_DTE = "";
        String APPLY_DTE = "";
        //String REPLY_FLAG = "";
        //String CUST_SEQ = "";
        //String MNT_USER = "";
        //String MNT_DTE = "";
        //String tempPAY_NBR = "";

        DataRow[] PUBLIC_APPLY_DataRow = null;
        DataTable PUBLIC_APPLY_DT = null;
                           
        //檔案長度
        const int intDataLength = 85;

        String strInPAY_TYPE = "ACH";
        String strRecNAME = "";
        String strPAY_TYPE = "";
        String strXmlURL = "";
        string strJobName = "PBBAHC001";
        bool PAY_CARD_TRANS = false;
        //email
        String strEmailBody = "";

        #endregion

        #region 宣告檔案路徑

        //寫出檔案名稱
        String strOutFileName = "";
        String strFileName = "";
        //寫出檔案路徑
        String strFilePath = "";
        //檔案格式
        DataTable FH_xml_DataTable = new DataTable();
        DataTable FD_xml_DataTable = new DataTable();
        DataTable FT_xml_DataTable = new DataTable();
        DataTable FH_DataTable = new DataTable();
        DataTable FD_DataTable = new DataTable();
        DataTable FT_DataTable = new DataTable();
        FileParseByXml xml = new FileParseByXml();

        #endregion

        static readonly Cybersoft.Coca.Log.Logger logger = Cybersoft.Log.LogManager<Cybersoft.Coca.Log.Logger>.GetLogger(System.Reflection.MethodInfo.GetCurrentMethod().ReflectedType.Name);
        #region 程式主邏輯【MAIN Routine】
        //public string RUN(string strInPAY_TYPE)   
        public string RUN()
        {
            #region 準備MSGLG.SYSOUT CLASS
            logger.strJobID = JobID;
            logger.strJOBNAME = "PBBAHC001";
            logger.dtRunDate = DateTime.Now;
            #endregion

            try
            {
                #region 取得系統資訊(下次批次日)
                CMCSYS001 SYSINF = new CMCSYS001();
                SYSINF.strJobID = JobID;
                SYSINF.strJobName = "PBBAHC001";
                String SYSINF_RC = SYSINF.getSYSINF();
                TODAY_PROCESS_DTE = SYSINF.datetimeTODAY_PROCESS_DTE;
                TODAY_YYYYMMDD = TODAY_PROCESS_DTE.ToString("yyyyMMdd");
                TODAY_CH_DTE = (Convert.ToString(Convert.ToInt64(TODAY_PROCESS_DTE.ToString("yyyy")) - 1911) + TODAY_PROCESS_DTE.ToString("MMdd")).PadLeft(8, '0');
                strBANK_NBR = SYSINF.strBANK_NBR;
                #endregion

                #region 讀取公共事業參數(KEY:交換單位)
                SETUP_PUBLIC.init();

                SETUP_PUBLIC.strWhereFILE_TRANSFER_UNIT = "ACH";
                //SETUP_PUBLIC.strWhereFILE_FORMAT = "ACH";  //格式名稱user自行設定
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
                        SETUP_PUBLIC_DATATABLE = SETUP_PUBLIC.resultTable;
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

                #endregion

                for (int i = 0; i < SETUP_PUBLIC_Query_Count; i++)
                {
                    strPAY_TYPE = SETUP_PUBLIC_DATATABLE.Rows[i]["PAY_TYPE"].ToString().Trim();
                    #region (for RE-RUN)復原PUBLIC_APPLY_ACH檔案
                    PUBLIC_APPLY.init();
                    //更新欄位
                    PUBLIC_APPLY.strVAILD_FLAG = "";
                    PUBLIC_APPLY.strREPLY_FLAG = "";
                    PUBLIC_APPLY.strMNT_USER = "";
                    PUBLIC_APPLY.datetimeMNT_DT = TODAY_PROCESS_DTE;
                    //條件
                    PUBLIC_APPLY.strWherePAY_TYPE = strPAY_TYPE;
                    PUBLIC_APPLY.strWhereREPLY_FLAG = "Y";
                    PUBLIC_APPLY.strWhereREPLY_DTE = TODAY_PROCESS_DTE.ToString("yyyyMMdd");
                    PUBLIC_APPLY.strWhereMNT_USER = strJobName;

                    //執行
                    PUBLIC_APPLY_RC = PUBLIC_APPLY.update();
                    switch (PUBLIC_APPLY_RC)
                    {
                        case "S0000": //更新成功   
                            PUBLIC_APPLY_RECOV_Count = PUBLIC_APPLY.intUptCnt;
                            logger.strJobQueue = "(For Rerun)回覆PUBLIC_APPLY原狀態(PAY_TYPE = " + strPAY_TYPE + "),筆數為：" + PUBLIC_APPLY_RECOV_Count;
                            logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                            break;

                        case "F0023": //查無資料
                            logger.strJobQueue = "(For Rerun)更新PUBLIC_APPLY 無資料,RC=" + PUBLIC_APPLY_RC;
                            logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                            break;

                        default: //資料庫錯誤
                            logger.strJobQueue = "(For Rerun)更新PUBLIC_APPLY 資料錯誤:" + PUBLIC_APPLY_RC;
                            logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                            return "B0016:" + logger.strJobQueue;
                    }
                    #endregion

                    #region 終止資料：到期日<=今日&生效註記="Y"，維護傳送註記&傳送日期為空白
                    PUBLIC_APPLY.init();
                    //條件
                    PUBLIC_APPLY.strWherePAY_TYPE = strPAY_TYPE;
                    PUBLIC_APPLY.strWhereEXPIR_DTE = TODAY_PROCESS_DTE.ToString("yyyyMMdd");
                    PUBLIC_APPLY.strWhereVAILD_FLAG = "Y";
                    //更新REPLY_FLAG, REPLY_DATE為空白
                    PUBLIC_APPLY.strMNT_USER = strJobName;
                    PUBLIC_APPLY.datetimeMNT_DT = TODAY_PROCESS_DTE;
                    //
                    PUBLIC_APPLY_RC = PUBLIC_APPLY.update_reply_flag(" ");
                    switch (PUBLIC_APPLY_RC)
                    {
                        case "S0000": //查詢成功
                            PUBLIC_APPLY_Update_Count3 += PUBLIC_APPLY.intUptCnt;
                            logger.strJobQueue = "今日到期終止資料:" + PUBLIC_APPLY.intUptCnt + "筆";
                            logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                            break;

                        case "F0023": //無需處理資料
                            logger.strJobQueue = "今日無到期終止資料需產生;";
                            logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                            break;

                        default: //資料庫錯誤
                            logger.strJobQueue = "查詢PUBLIC_APPLY 到期終止資料錯誤:" + PUBLIC_APPLY_RC;
                            logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                            return "B0016:" + logger.strJobQueue;
                    }
                    #endregion
                }

                #region 宣告檔案路徑
                //寫出檔案路徑                
                if (strRecNAME != "")
                {
                    #region 宣告檔案路徑
                    Cybersoft.Base.CMCURL001 CMCURL = new CMCURL001();

                    //取得 XML格式
                    strXmlURL = CMCURL.getURL(strRecNAME + ".xml");
                    
                    //檔案路徑 strFilePath
                    strFilePath = CMCURL.getPATH(strRecNAME + "_CHGOUT");
                    if (strFilePath == "")
                    {
                        logger.strJobQueue = "路徑取得錯誤!!!  <PATH> 未設定" + strRecNAME + "_CHGOUT";
                        logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                        return "B0099:" + logger.strJobQueue;
                    }
                    //檔案名稱 strFileName
                    strFileName = CMCURL.getFILE_NAME(strRecNAME + "_CHGOUT");
                    if (strFileName == "")
                    {
                        logger.strJobQueue = "檔名取得錯誤!!!  <FILE_NAME> 未設定 : " + strRecNAME + "_CHGOUT";
                        logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                        return "B0099:" + logger.strJobQueue;
                    }

                    //附檔名
                    String strEXT = CMCURL.getEXT(strRecNAME + "_CHGOUT");
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
                        #region Layout (轉入XML的FH格式)
                        FH_xml_DataTable = xml.Xml2DataTable(strXmlURL, "ACHP02_FH");
                        if (xml.strMSG.Length > 0)
                        {
                            logger.strJobQueue = "[Xml2DataTable(ACHP02_FH)] - " + xml.strMSG.ToString().Trim();
                            logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                            return "B0099:" + logger.strJobQueue;
                        }

                        #endregion
                        //BODY
                        #region Layout (轉入XML的FD格式)
                        FD_xml_DataTable = xml.Xml2DataTable(strXmlURL, "ACHP02_FD");
                        if (xml.strMSG.Length > 0)
                        {
                            logger.strJobQueue = "[Xml2DataTable(ACHP02_FD)] - " + xml.strMSG.ToString().Trim();
                            logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                            return "B0099:" + logger.strJobQueue;
                        }
                        #endregion
                        //TRAILER
                        #region Layout (轉入XML的FT格式)
                        FT_xml_DataTable = xml.Xml2DataTable(strXmlURL, "ACHP02_FT");
                        if (xml.strMSG.Length > 0)
                        {
                            logger.strJobQueue = "[Xml2DataTable(ACHP02_FT)] - " + xml.strMSG.ToString().Trim();
                            logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                            return "B0099:" + logger.strJobQueue;
                        }
                        #endregion

                        //使用XML定義的LAYOUT定對暫存TABLE
                        FH_DataTable = xml.dtXML2DataTable(FH_xml_DataTable);
                        FD_DataTable = xml.dtXML2DataTable(FD_xml_DataTable);
                        FT_DataTable = xml.dtXML2DataTable(FT_xml_DataTable);
                    }
                    #endregion
                }
                else
                {
                    logger.strJobQueue = "參數檔未設定檔案格式 : 格式名稱(" + strRecNAME + ")";
                    logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                    return "B0099:" + logger.strJobQueue;
                }
                #endregion

                #region 定義EMAIL明細欄位
                REPORT_DataTable_MAIL = new DataTable();
                REPORT_DataTable_MAIL.Columns.Add("PAY_TYPE", typeof(string));
                REPORT_DataTable_MAIL.Columns.Add("FILE_NAME", typeof(string));
                REPORT_DataTable_MAIL.Columns.Add("CNT", typeof(string));
                #endregion

                #region 擷取今日需送約定檔之ACH資料
                PUBLIC_APPLY.init();
                PUBLIC_APPLY_RC = PUBLIC_APPLY.query_for_PBAPPLY("ACH");
                switch (PUBLIC_APPLY_RC)
                {
                    case "S0000": //    查詢成功
                        PUBLIC_APPLY_Query_Count = PUBLIC_APPLY.resultTable.Rows.Count;
                        PUBLIC_APPLY_DT = PUBLIC_APPLY.resultTable.Clone();	    
                        for (int h = 0; h < PUBLIC_APPLY_Query_Count; h++)
	                    {
	                        PUBLIC_APPLY_DT.ImportRow(PUBLIC_APPLY.resultTable.Rows[h]);
	                    }
                        ACH_CHGOUT = new string[PUBLIC_APPLY_Query_Count];
                        logger.strJobQueue = "今日公用事業約定檔需產生，筆數為=" + PUBLIC_APPLY_Query_Count;
                        logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                        break;

                    case "F0023": //無需處理資料
                        logger.strJobQueue = "今日無ACH約定檔需產生;";
                        logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                        break;

                    default: //資料庫錯誤
                        logger.strJobQueue = "查詢PUBLIC_APPLY 資料錯誤:" + PUBLIC_APPLY_RC;
                        logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                        return "B0016:" + logger.strJobQueue;
                }
                #endregion

                for (int i=0; i < SETUP_PUBLIC_Query_Count; i++)
                {
                    //定義一個空的字串來擺放FD
                    String[] strDATA_FH = null;
                    String[] strDATA_FD = null;
                    String[] strDATA_FT = null;
                    string strVAILD_FLAG = "";
                    string strTRANS_TYPE = "";

                    #region  參數設定
                    strPAY_TYPE = SETUP_PUBLIC_DATATABLE.Rows[i]["PAY_TYPE"].ToString().Trim();
                    //提出行代號, 提回行代號
                    strCP_PBANK = SETUP_PUBLIC_DATATABLE.Rows[i]["SEND_UNIT"].ToString();
                    strCP_RBANK = SETUP_PUBLIC_DATATABLE.Rows[i]["RECV_UNIT"].ToString();
                    strTRANS_TYPE = SETUP_PUBLIC_DATATABLE.Rows[i]["FILE_TRANSFER_TYPE"].ToString().Trim();
                    //交易代號
                    if (SETUP_PUBLIC_DATATABLE.Rows[i]["FILE_TRANSFER_TYPE"].ToString().Trim() == "")
                    {
                        logger.strJobQueue = "SETUP_PUBLIC 參數設定不完整: FILE_TRANSFER_TYPE = '" + SETUP_PUBLIC_DATATABLE.Rows[i]["FILE_TRANSFER_TYPE"].ToString() + "'";
                        logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                        return "B0016" + logger.strJobQueue;
                    }
                    else
                    {
                        strCP_TIX = SETUP_PUBLIC_DATATABLE.Rows[i]["FILE_TRANSFER_TYPE"].ToString();
                    }                    
                    //卡號轉換
                    if (SETUP_PUBLIC_DATATABLE.Rows[i]["PAY_CARD_TRANS"].ToString() == "Y")
                    {
                        PAY_CARD_TRANS = true;
                    }
                    #endregion
                    
                    #region 產生檔案內容 & 更新PUBLIC_APPLY

                    //*** HEAD ***
                    moveDATA_FH();
                    strDATA_FH = xml.DataTable2FileStrArray(BIG5, FH_DataTable, FH_xml_DataTable, "");  

                    //*** BODY ***
                    #region 依代繳類別建立該類約定檔Table
                    FD_DataTable.Clear();
                    if (PUBLIC_APPLY_Query_Count > 0)
                    {
                        PUBLIC_APPLY_DataRow = PUBLIC_APPLY_DT.Select("PAY_TYPE='" + strPAY_TYPE + "'");
                        if (PUBLIC_APPLY_DataRow.Length > 0)
                        {
                            for (int j = 0; j < PUBLIC_APPLY_DataRow.Length; j++)
                            {
                                strWriteFD = "";
                                //組明細
                                #region 判斷異動種類
                                APPLY_DTE  = PUBLIC_APPLY_DataRow[j]["APPLY_DTE"].ToString();
                                EXPIR_DTE = PUBLIC_APPLY_DataRow[j]["EXPIR_DTE"].ToString();
                                ///判斷異動種類：APPLY_DTE >= TODAY → 新增，代碼"H" 
                                ///                              EXPIR_DTE   == TODAY → 終止，代碼"E"
                                if (APPLY_DTE == EXPIR_DTE)
                                {
                                    #region 當天申請當天終止的資料, 不送至公用事業單位約定, 直接更新PUBLIC_APPLY
                                    PUBLIC_APPLY.init();
                                    //條件(key)
                                    PUBLIC_APPLY.strWherePAY_TYPE = strPAY_TYPE;
                                    PUBLIC_APPLY.strWhereSEQ = PUBLIC_APPLY_DataRow[j]["SEQ"].ToString().Trim();
                                    PUBLIC_APPLY.strWherePAY_NBR = PUBLIC_APPLY_DataRow[j]["PAY_NBR"].ToString().Trim();
                                    PUBLIC_APPLY.strWherePAY_CARD_NBR = PUBLIC_APPLY_DataRow[j]["PAY_CARD_NB"].ToString().Trim();
                                    //更新
                                    PUBLIC_APPLY.strREPLY_FLAG = "Y";
                                    PUBLIC_APPLY.strREPLY_DTE = TODAY_YYYYMMDD;
                                    PUBLIC_APPLY.strMNT_USER = strJobName;
                                    PUBLIC_APPLY.datetimeMNT_DT = TODAY_PROCESS_DTE;
                                    PUBLIC_APPLY_RC = PUBLIC_APPLY.update();
                                    switch (PUBLIC_APPLY_RC)
                                    {
                                        case "S0000": //更新成功   
                                            PUBLIC_APPLY_Update_Count2 = PUBLIC_APPLY_Update_Count2 + PUBLIC_APPLY.intUptCnt;
                                            if (PUBLIC_APPLY.intUptCnt == 0)
                                            {
                                                logger.strJobQueue = "更新PUBLIC_APPLY 資料錯誤: F0023, PAY_CARD_NBR = " + PAY_NBR + " SEQ = " + SEQ;
                                                logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                                                return "F0023" + logger.strJobQueue;
                                            }
                                            logger.strJobQueue = "該客戶當天申請並終止, 故不送公用事業單位, 用戶編號: " + PAY_CARD_NBR + " , 代繳卡號 = " + PAY_CARD_NBR;
                                            logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                                            break;

                                        default: //資料庫錯誤
                                            logger.strJobQueue = "更新PUBLIC_APPLY 資料錯誤:" + PUBLIC_APPLY_RC;
                                            logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                                            return "B0016:" + logger.strJobQueue;
                                    }
                                    #endregion
                                    continue;
                                }
                                else
                                {
                                    if ((Convert.ToInt32(APPLY_DTE) <= Convert.ToInt32(TODAY_YYYYMMDD)) &&
                                         (Convert.ToInt32(EXPIR_DTE) > Convert.ToInt32(TODAY_YYYYMMDD)               ))
                                    {
                                        CHANGE_TYPE = "H";
                                        strVAILD_FLAG = "Y";
                                    }
                                    else
                                        if (Convert.ToInt32(EXPIR_DTE) <= Convert.ToInt32(TODAY_YYYYMMDD))
                                        {
                                            CHANGE_TYPE = "E";
                                            strVAILD_FLAG = "N";
                                            //CHANGE_DTE = (DateTime.ParseExact(EXPIR_DTE, "yyyyMMdd", null)).AddYears(-1911).ToString("yyyyMMdd").Substring(1, 7);
                                        }
                                        else
                                        {
                                            logger.strJobQueue = "第" + (j + 1) + "筆資料有誤, PAY_NBR = " + PAY_NBR + ", APPLY_DTE = " + APPLY_DTE + ", EXPIR_DTE = " + EXPIR_DTE + ", 請確認!!";
                                            logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                                            return "B0016";
                                        }
                                }
                                #endregion

                                moveDATA_FD(j, strPAY_TYPE, CHANGE_TYPE);
                                //寫出約定檔才逐筆更新主檔生效註記
                                if (strWriteFD == "Y")
                                {
                                    #region 更新PUBLIC_APPLY 傳送公用事業單位日期及註記
                                    PUBLIC_APPLY.init();
                                    //更新欄位
                                    PUBLIC_APPLY.strREPLY_FLAG = "Y";
                                    PUBLIC_APPLY.strVAILD_FLAG = strVAILD_FLAG;
                                    PUBLIC_APPLY.strREPLY_DTE = TODAY_PROCESS_DTE.ToString("yyyyMMdd");
                                    PUBLIC_APPLY.strMNT_USER = strJobName;
                                    PUBLIC_APPLY.datetimeMNT_DT = TODAY_PROCESS_DTE;
                                    //條件
                                    PUBLIC_APPLY.strWherePAY_TYPE = strPAY_TYPE;
                                    PUBLIC_APPLY.strWhereSEQ = PUBLIC_APPLY_DataRow[j]["SEQ"].ToString();
                                    PUBLIC_APPLY.strWherePAY_NBR = PUBLIC_APPLY_DataRow[j]["PAY_NBR"].ToString();
                                    PUBLIC_APPLY.strWherePAY_CARD_NBR = PUBLIC_APPLY_DataRow[j]["PAY_CARD_NBR"].ToString();
                                    //執行
                                    PUBLIC_APPLY_RC = PUBLIC_APPLY.update();
                                    switch (PUBLIC_APPLY_RC)
                                    {
                                        case "S0000": //更新成功   
                                            PUBLIC_APPLY_Update_Count = PUBLIC_APPLY_Update_Count + PUBLIC_APPLY.intUptCnt;
                                            if (PUBLIC_APPLY.intUptCnt == 0)
                                            {
                                                logger.strJobQueue = "更新PUBLIC_APPLY 資料錯誤: F0023, PAY_CARD_NBR = " + PAY_CARD_NBR + " SEQ = " + SEQ;
                                                logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                                                return "F0023" + logger.strJobQueue;
                                            }
                                            break;

                                        default: //資料庫錯誤
                                            logger.strJobQueue = "更新PUBLIC_APPLY 資料錯誤:" + PUBLIC_APPLY_RC;
                                            logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                                            return "B0016:" + logger.strJobQueue;
                                    }
                                    #endregion
                                }
                            }
                            strDATA_FD = xml.DataTable2FileStrArray(BIG5, FD_DataTable, FD_xml_DataTable, "");  // 帶有資料的欄位TABLE，與產出字串的XML MAPPING，組出FD的字串
                        }

                        //logger.strJobQueue = "今日產出約定" + strDESCR + "筆數為 : " + PUBLIC_APPLY_DataRow.Length;
                        //logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);

                    }

                    #endregion

                    //*** TRAIL ***
                    moveDATA_FT();
                    strDATA_FT = xml.DataTable2FileStrArray(BIG5, FT_DataTable, FT_xml_DataTable, "");  // 帶有資料的欄位TABLE，與產出字串的XML MAPPING，組出FT的字串

                    #endregion

                    #region 產出檔案，依序寫出FH --> FD --> FT
                    Cybersoft.Base.CMCURL001 CMCURL = new CMCURL001();
                    strOutFileName = strFilePath + strTRANS_TYPE + strFileName;
                    strOutFileName = CMCURL.ReplaceVarDateFormat(strOutFileName, TODAY_PROCESS_DTE);
                    FileStream fsOutFile = new FileStream(strOutFileName, FileMode.Create, FileAccess.Write, FileShare.None);
                    //寫出PMT_DD_APPLY Header檔
                    //strFileName = strFilePath + strPAY_TYPE + TODAY_CH_DTE;
                    //FileStream fsOutFile = new FileStream(strFileName, FileMode.Create, FileAccess.Write, FileShare.None);
                    using (StreamWriter srOutFile = new StreamWriter(fsOutFile, BIG5))
                    {
                        //1.先寫FH
                        srOutFile.Write(strDATA_FH[0]);
                        srOutFile.Write("\r\n");
                        srOutFile.Flush();

                        //2.逐筆寫出FD資料
                        for (int k = 0; k < intFD_CNT; k++)
                        {
                            srOutFile.Write(strDATA_FD[k]);
                            srOutFile.Write("\r\n");
                            srOutFile.Flush();
                        }
                        //3.最後寫FT
                        srOutFile.Write(strDATA_FT[0]);
                        srOutFile.Write("\r\n");
                        srOutFile.Flush();

                        srOutFile.Close();
                    }
                    fsOutFile.Close();
                    #endregion

                    #region 確認有無產出檔案
                    if (!File.Exists(strOutFileName))
                    {
                        logger.strJobQueue = "今日-公共事業約定檔產出有誤(" + strOutFileName + ")，請與系統負責人確認!!";
                        logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                        return "B0012：" + logger.strJobQueue;
                    }
                    #endregion

                    #region 搬出email資訊
                    DataRow DR = REPORT_DataTable_MAIL.NewRow();
                    //DR["PAY_TYPE"] = strPAY_TYPE + "-" + strDESCR;
                    //DR["FILE_NAME"] = strTRANS_TYPE + TODAY_CH_DTE;
                    //DR["CNT"] = intFD_CNT.ToString("#,###,###,##0").PadLeft(13, ' ');
                    //REPORT_DataTable_MAIL.Rows.Add(DR);
                    #endregion
                }

                #region 判別更新筆數是否相同
                if ((PUBLIC_APPLY_Update_Count + PUBLIC_APPLY_Update_Count2) != PUBLIC_APPLY.resultTable.Rows.Count)
                {
                    logger.strJobQueue = "更新 PUBLIC_APPLY 時筆數異常,原筆數 : " + PUBLIC_APPLY.resultTable.Rows.Count + " 實際筆數: " + PUBLIC_APPLY_Update_Count;
                    logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                    return "B0012" + logger.strJobQueue;
                }

                logger.strJobQueue = "更新 PUBLIC_APPLY 成功筆數 =" + (PUBLIC_APPLY_Update_Count + PUBLIC_APPLY_Update_Count2);
                logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);

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

        #region Display
        void writeDisplay()
        {
            logger.strJobID = "00002";

            //擷取ACH需產出約定檔的資料
            logger.strTBL_NAME = "SETUP_PUBLIC";
            logger.strTBL_ACCESS = "Q";
            logger.intTBL_COUNT = SETUP_PUBLIC_Query_Count;
            logger.strMEMO = "讀取公用事業(ACH)參數筆數";
            logger.writeCounter();

            //擷取ACH需產出約定檔的資料
            logger.strTBL_NAME = "PUBLIC_APPLY";
            logger.strTBL_ACCESS = "Q";
            logger.intTBL_COUNT = PUBLIC_APPLY_Query_Count;
            logger.writeCounter();

            //ACH約定檔筆數(含頭尾)
            logger.strTBL_NAME = "ACH_CHGOUT";
            logger.strTBL_ACCESS = "I";
            logger.intTBL_COUNT = ACH_TXT_Count;
            logger.writeCounter();

            //更新PUBLIC_APPLY
            logger.strTBL_NAME = "PUBLIC_APPLY";
            logger.strTBL_ACCESS = "U";
            logger.intTBL_COUNT = PUBLIC_APPLY_Update_Count;
            logger.strMEMO = "當天異動資料";
            logger.writeCounter();

            //更新PUBLIC_APPLY
            logger.strTBL_NAME = "PUBLIC_APPLY";
            logger.strTBL_ACCESS = "U";
            logger.intTBL_COUNT = PUBLIC_APPLY_Update_Count2;
            logger.strMEMO = "當天新增並終止";
            logger.writeCounter();

            //更新PUBLIC_APPLY
            logger.strTBL_NAME = "PUBLIC_APPLY";
            logger.strTBL_ACCESS = "U";
            logger.intTBL_COUNT = PUBLIC_APPLY_Update_Count3;
            logger.strMEMO = "當天到期";
            logger.writeCounter();

            logger.DisplayCounter(Cybersoft.Coca.Log.LevelEnum.Info);
        }
        #endregion

        #region 組下傳檔
        #region 組FH
        private void moveDATA_FH()
        {
            FH_DataTable.Clear();
            FH_DataTable.Rows.Add();

            //首錄別(首筆為"1")
            FH_DataTable.Rows[0]["CP_BOF"] = "BOF";
            //資料代號
            FH_DataTable.Rows[0]["CP_CDATA"] = "ACHP02";
            //交易日期  (民國YYYYMMDD)
            FH_DataTable.Rows[0]["CP_TDATE"] = TODAY_CH_DTE;
            //發送單位代號
            FH_DataTable.Rows[0]["CP_SORG"] = strBANK_NBR.PadRight(7, ' '); ;

        }
        #endregion

        #region 組FD
        private void moveDATA_FD(int j, string strPAY_TYPE, string CHANGE_TYPE)
        {            
            FD_DataTable.Rows.Add();
            int i = FD_DataTable.Rows.Count - 1;
            string strRECV_UNIT = string.Empty;

            //1.交易序號(KEY)
            FD_DataTable.Rows[i]["CP_SEQ"] = FD_DataTable.Rows.Count.ToString().PadLeft(6, '0');
            //2.交易代號
            FD_DataTable.Rows[i]["CP_TIX"] = strCP_TIX;
            
            //3.發動者統一編號(KEY)
            FD_DataTable.Rows[i]["CP_CID"] = strPAY_TYPE.PadRight(10, ' ');
            //4.提回行代號
            FD_DataTable.Rows[i]["CP_RBANK"] = strCP_RBANK;
            //5.委繳戶帳號            
            FD_DataTable.Rows[i]["CP_RCLNO"] = PUBLIC_APPLY_DataRow[j]["PAY_ACCT_NBR"].ToString().Trim(); ;
            //6.委繳戶統一編號
            FD_DataTable.Rows[i]["CP_RID"] = PUBLIC_APPLY_DataRow[j]["CUST_ID"].ToString().Trim(); ;
            //7.用戶號碼
            if (PAY_CARD_TRANS)
            {
                FD_DataTable.Rows[i]["CP_USERNO"] = PUBLIC_APPLY_DataRow[j]["PAY_NBR"].ToString().Substring(2, PAY_NBR.Length - 2);
            }
            else
            {
                FD_DataTable.Rows[i]["CP_USERNO"] = PUBLIC_APPLY_DataRow[j]["PAY_NBR"].ToString();
            }
            //8.新增或取消(KEY)
            FD_DataTable.Rows[i]["CP_ADMARK"] = CHANGE_TYPE;
            //9.資料製作日期(KEY)
            FD_DataTable.Rows[i]["CP_DATE"] = TODAY_CH_DTE;
            //10.提出行代號(KEY)...各銀行設定代碼
            FD_DataTable.Rows[i]["CP_PBANK"] = strCP_PBANK;
            //11.發動者專用區
            FD_DataTable.Rows[i]["CP_NOTE"] = PUBLIC_APPLY_DataRow[j]["PAY_NBR"].ToString(); ;
            //12.交易型態(KEY)
            FD_DataTable.Rows[i]["CP_TYPE"] = "N";      //N：提出 R：回覆
            //13.回覆訊息
            FD_DataTable.Rows[i]["CP_RCODE"] = "";
            //14.每筆扣款限額
            FD_DataTable.Rows[i]["CP_LIMITAMT"] = "";
            //15.備用
            FD_DataTable.Rows[i]["FILLER"] = "";

            ++intFD_CNT;
            strWriteFD = "Y";
        }

        #endregion

        #region 組FT
        private void moveDATA_FT()
        {
            FT_DataTable.Clear();
            FT_DataTable.Rows.Add();

            //尾錄別
            FT_DataTable.Rows[0]["CP_EOF"] = "EOF";
            //總筆數
            FT_DataTable.Rows[0]["CP_TCOUNT"] = intFD_CNT.ToString().PadLeft(8, '0');
        }
        #endregion
        #endregion

        #region 發送Email
        private void email()
        {

            //發送mail
            Cybersoft.Base.CMCJOB001 CMCJOB001 = new Cybersoft.Base.CMCJOB001();

            //定義table : 取得系統資訊
            SETUP_SYSTEMDao SETUP_SYSTEM = new SETUP_SYSTEMDao();
            ZZ_ACCOUNTDao ZZ_ACCOUNT = new ZZ_ACCOUNTDao();
            //變數
            string email_subject = "";
            string email_body = "";
            StringBuilder email_to = new StringBuilder();
            DataTable ACCT_DataTable = new DataTable();
            //Email主旨
            email_subject = "[INFO][Batch]" + TODAY_PROCESS_DTE.ToString("yyyy-MM-dd") + "公用事業-約定檔已產生";
            //Email內容
            #region Email內容
            strEmailBody = "今日-公用事業約定檔已產出!";
            email_body += "</br> " + strEmailBody + "</br>";
            email_body += "<table style='border:2px solid black;font-size:12px;'>";
            email_body += "<caption>" + TODAY_PROCESS_DTE.ToString("yyyy-MM-dd") + " 約定檔作業</caption>";
            #endregion
            #region 表頭
            email_body += "<tr>";
            email_body += "<td style='border:1px solid black;background:#FFAA33;'>公用事業單位</td>";
            email_body += "<td style='border:1px solid black;background:#FFAA33;'>檔名</td>";
            email_body += "<td style='border:1px solid black;background:#FFAA33;'>筆數</td>";
            email_body += "</tr>";
            #endregion
            #region BODY
            for (int i = 0; i < REPORT_DataTable_MAIL.Rows.Count; i++)
            {
                email_body += "<tr>";
                email_body += "<td style='border:1px solid black;'>" + REPORT_DataTable_MAIL.Rows[i]["PAY_TYPE"].ToString() + "</td>";
                email_body += "<td style='border:1px solid black;align=left;'>" + REPORT_DataTable_MAIL.Rows[i]["FILE_NAME"].ToString() + "</td>";
                email_body += "<td style='border:1px solid black;align=right;'>" + REPORT_DataTable_MAIL.Rows[i]["CNT"].ToString() + "</td>";
                email_body += "</tr>";
            }
            #endregion
            email_body += "</table>";
            #region Email收件者
            ZZ_ACCOUNT.init();
            ZZ_ACCOUNT.strRIGHTS_GROUP = "ACCT";  //帳務群組
            string ZZ_ACCOUNT_RC = ZZ_ACCOUNT.query();
            switch (ZZ_ACCOUNT_RC)
            {
                case "S0000": //查詢成功
                    ACCT_DataTable = ZZ_ACCOUNT.resultTable;
                    for (int i = 0; i < ACCT_DataTable.Rows.Count; i++)
                    {
                        if (Convert.ToString(ACCT_DataTable.Rows[i]["EMAIL"]) != "")
                        {
                            email_to.Append(ACCT_DataTable.Rows[i]["EMAIL"]);
                            email_to.Append(",");
                        }
                    }
                    //移除最後一個逗號
                    if (email_to.ToString().Length > 0)
                    {
                        email_to.Remove(email_to.ToString().Length - 1, 1);
                    }
                    //CMCJOB001.Mail_To = email_to.ToString();
                    logger.strJobQueue = "ZZ_ACCOUNT.query finish 筆數: " + ACCT_DataTable.Rows.Count.ToString("###,###,##0").PadLeft(11, ' ');
                    logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                    break;
                case "F0023": //查無資料
                    logger.strJobQueue = "無符合之ZZ_ACCOUNT資料";
                    logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                    break;
                default: //資料庫錯誤
                    logger.strJobQueue = "ZZ_ACCOUNT.query 錯誤 " + ZZ_ACCOUNT_RC;
                    logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                    break;
            }
            #endregion
            #region Email發送結果,訊息處理
            CMCJOB001.strJobEnv = Dns.GetHostName();
            string resultMessage = CMCJOB001.processMAIL(email_subject, email_body, false);

            //Email訊息處理                
            if (!"S0000".Equals(resultMessage))
            {
                logger.strJobQueue = "===發送Email失敗===";
                logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                logger.strJobQueue = resultMessage;
                logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
            }
            #endregion
        }
        #endregion
               
    }
}

