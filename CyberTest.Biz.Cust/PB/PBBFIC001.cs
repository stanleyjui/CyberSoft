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
    /// 產生財金格式約定檔(台電、省水、市水)
    /// 執行週期：每週第一個營業日撈取尚未申請/終止之資料
    ///           營業日10:00am前送檔
    /// </summary>
    public class PBBFIC001
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
        PUBLIC_APPLYDao PUBLIC_APPLY_U = new PUBLIC_APPLYDao();

        DataTable PUBLIC_APPLY_DATATABLE = new DataTable();
        DataTable PUBLIC_APPLY_DT = new DataTable();

        //宣告SETUP_PUBLIC
        SETUP_PUBLICDao SETUP_PUBLIC = new SETUP_PUBLICDao();
        DataTable SETUP_PUBLIC_DATATABLE = new DataTable();
        //發送email
        DataTable REPORT_DataTable_MAIL = new DataTable();
        #endregion

        #region 設定檔案編碼
        Encoding BIG5 = Encoding.GetEncoding("big5");
        #endregion

        #region 宣告共用變數
        //批次日期
        DateTime TODAY_PROCESS_DTE = new DateTime();
        DateTime BANK_PREV_PROCESS_DTE = new DateTime();
        DateTime dtStart = new DateTime(1900, 1, 1);

        String WEEK_START = "";
        String  TODAY_CH_DTE = "";
        //筆數&金額
        int PUBLIC_APPLY_Update_Count = 0;
        int PUBLIC_APPLY_Query_Count = 0;
        int PUBLIC_APPLY_RECOV_Count = 0;
        int SETUP_PUBLIC_Query_Count = 0;
        
        int intFD_CNT = 0;

        //PUBLIC_APPLY欄位
        String PAY_CARD_NBR = "";
        String PAY_NBR = "";
        String SEQ = "";
          
        //暫存變數
        string strVAILD_FLAG = "";
        string PUBLIC_APPLY_U_RC = "";
        string strBANK_NBR = "";
        string strCHANGE_AREA = "";
        string strJobName = "PBBFIC001";
        string strWriteFD = "";
        
        String strEmailBody = "";

        #endregion

        #region 宣告檔案路徑

        //寫出檔案名稱 (轉帳類別+民國YYYMMDD))
        String strOutFileName = "";
        //String strRecNAME = "PUBLIC_FISC";
        String strRecNAME = "";
        String strPAY_TYPE = "";
        //String strTRANSFER_TYPE = "";

        //寫出檔案路徑
        //String strXmlURL = "";
        //String strDestPATH = "";
        String strFileName = "";
        String strFilePath = "";

        DataTable FH_xml_DataTable = new DataTable();
        DataTable FD_xml_DataTable = new DataTable();
        DataTable FT_xml_DataTable = new DataTable();
        DataTable FH_DataTable = new DataTable();
        DataTable FD_DataTable = new DataTable();
        DataTable FT_DataTable = new DataTable();
        DataRow[] PUBLIC_APPLY_DataRow = null;

        FileParseByXml xml = new FileParseByXml();
        #endregion

        static readonly Cybersoft.Coca.Log.Logger logger = Cybersoft.Log.LogManager<Cybersoft.Coca.Log.Logger>.GetLogger(System.Reflection.MethodInfo.GetCurrentMethod().ReflectedType.Name);
        //========================================================================================================
        #region 程式主邏輯【MAIN Routine】
        
        public string RUN()
        {
            #region 準備MSGLG.SYSOUT CLASS
            logger.strJobID = JobID;
            logger.strJOBNAME = "PBBFIC001";
            logger.dtRunDate = DateTime.Now;
            #endregion

            try
            {
                #region 取得系統資訊
                CMCSYS001 SYSINF = new CMCSYS001();
                SYSINF.strJobID = JobID;
                SYSINF.strJobName = strJobName;
                String SYSINF_RC = SYSINF.getSYSINF();
                BANK_PREV_PROCESS_DTE = SYSINF.datetimeBANK_PREV_PROCESS_DTE;
                TODAY_PROCESS_DTE = SYSINF.datetimeTODAY_PROCESS_DTE;
                TODAY_CH_DTE = (Convert.ToString(Convert.ToInt64(TODAY_PROCESS_DTE.ToString("yyyy")) - 1911) + TODAY_PROCESS_DTE.ToString("MMdd")).PadLeft(7, '0');
                WEEK_START = TODAY_PROCESS_DTE.AddDays(-7).ToString("yyyyMMdd"); //System.DateTime.Now.AddDays(-7).ToString("yyyyMMdd");
                strBANK_NBR = SYSINF.strBANK_NBR;
                #endregion

                #region 計算本週第一個營業日 (客製)
                //int TODAY_WEEK = Convert.ToInt32(TODAY_PROCESS_DTE.DayOfWeek.ToString("d"));  //取得今日為本週第幾天
                //DateTime startWeek = TODAY_PROCESS_DTE.AddDays(1 - TODAY_WEEK);  //計算出本週週一的日期
                //DateTime dayofmondy = new DateTime(1900, 01, 01);
                ////計算本週第一個營業日(星期一營業日，遇假日順延)
                //string RC = SYSINF.getNextWorkingDate(startWeek);
                //if (RC =="S0000")
                //{
                //     dayofmondy = SYSINF.datetimeNEXT_WORKING_DTE;
                //}
                
                //if (dayofmondy == TODAY_PROCESS_DTE)
                //{
                //    logger.strJobQueue = "本日 " + TODAY_PROCESS_DTE.ToString("yyyy/MM/dd") + "週：" + TODAY_WEEK + " ，為本週第一個銀行營業日，應處理約定檔案作業。";
                //    logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                //}
                // else
                //{
                //    logger.strJobQueue = "本日 " + TODAY_PROCESS_DTE.ToString("yyyy/MM/dd") + "週：" + TODAY_WEEK + " ，非本週第一個銀行營業日(" + dayofmondy + ")，不處理約定檔案作業，程式結束";
                //    logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                //    return "B0000";
                //}
                #endregion

                #region 讀取公共事業參數
                SETUP_PUBLIC.init();
                SETUP_PUBLIC.strWhereFILE_FORMAT = "FISC";
                SETUP_PUBLIC.strWherePOST_RESULT = "00";
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
                        logger.strJobQueue = "公用事業參數檔中無設定需報送財金格式的公共事業單位，不執行。";
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
                    strRecNAME = SETUP_PUBLIC_DATATABLE.Rows[i]["FILE_FORMAT"].ToString().Trim();
                    
                    #region (for RE-RUN)復原PUBLIC_APPLY_財金檔案

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
                            int PUBLIC_APPLY_Upd_ReplyCNT = PUBLIC_APPLY.intUptCnt;
                            logger.strJobQueue = "今日到期終止資料產生:" + PUBLIC_APPLY_Upd_ReplyCNT + "筆";
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

                if (strRecNAME != "")
                {
                    #region 宣告檔案路徑
                    Cybersoft.Base.CMCURL001 CMCURL = new CMCURL001();

                    //檔案格式 strXmlURL
                    string strXmlURL = CMCURL.getURL("FISC_CARD2BANK");
                    if (strXmlURL == "")
                    {
                        logger.strJobQueue = "格式取得錯誤!!! FISC_CARD2BANK <URL> 未設定";
                        logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                        return "B0099:" + logger.strJobQueue;
                    }
                    
                    //檔案路徑 
                    strFilePath = CMCURL.getPATH("FISC_CARD2BANK");
                    if (strFilePath == "")
                    {
                        logger.strJobQueue = "路徑取得錯誤!!! FISC_CARD2BANK <PATH> 未設定";
                        logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                        return "B0099:" + logger.strJobQueue;
                    }
                    //檔案名稱 strFileName
                    strFileName = CMCURL.getFILE_NAME("FISC_CARD2BANK");
                    if (strFileName == "")
                    {
                        logger.strJobQueue = "檔名取得錯誤!!! FISC_CARD2BANK <FILE_NAME> 未設定";
                        logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                        return "B0099:" + logger.strJobQueue;
                    }
                    
                    //附檔名
                    String strEXT = CMCURL.getEXT("FISC_CARD2BANK");
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
                        #region BATCHTX06(FH) Layout (轉入BATCHTX06.xml的FH格式)
                        FH_xml_DataTable = xml.Xml2DataTable(strXmlURL, "BATCHTX06_H");
                        if (xml.strMSG.Length > 0)
                        {
                            logger.strJobQueue = "[Xml2DataTable(" + strRecNAME + "_FH)] - " + xml.strMSG.ToString().Trim();
                            logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                            return "B0099:" + logger.strJobQueue;
                        }

                        #endregion
                        //BODY
                        #region BATCHTX06(FD) Layout (轉入BATCHTX06.xml的FD格式)
                        FD_xml_DataTable = xml.Xml2DataTable(strXmlURL, "BATCHTX06");
                        if (xml.strMSG.Length > 0)
                        {
                            logger.strJobQueue = "[Xml2DataTable(" + strRecNAME + "_FD)] - " + xml.strMSG.ToString().Trim();
                            logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                            return "B0099:" + logger.strJobQueue;
                        }
                        #endregion
                        //TRAILER
                        #region BATCHTX06(FT) Layout (轉入BATCHTX06.xml的FT格式)
                        FT_xml_DataTable = xml.Xml2DataTable(strXmlURL, "BATCHTX06_T");
                        if (xml.strMSG.Length > 0)
                        {
                            logger.strJobQueue = "[Xml2DataTable(" + strRecNAME + "_FT)] - " + xml.strMSG.ToString().Trim();
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
                    logger.strJobQueue = "參數檔未設定檔案格式 : 機構代碼(" + strPAY_TYPE + ") 格式名稱(" + strRecNAME + ")";
                    logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                    return "B0099:" + logger.strJobQueue;
                }

                #region 定義EMAIL明細欄位
                REPORT_DataTable_MAIL = new DataTable();
                REPORT_DataTable_MAIL.Columns.Add("PAY_TYPE", typeof(string));
                REPORT_DataTable_MAIL.Columns.Add("FILE_NAME", typeof(string));
                REPORT_DataTable_MAIL.Columns.Add("CNT", typeof(string));
                #endregion

                #region 擷取今日需送約定檔之台電,省水,市水
                PUBLIC_APPLY.init();
                PUBLIC_APPLY_RC = PUBLIC_APPLY.query_for_fisc();
                switch (PUBLIC_APPLY_RC)
                {
                    case "S0000": //查詢成功
                        PUBLIC_APPLY_Query_Count = PUBLIC_APPLY.resultTable.Rows.Count;
                        PUBLIC_APPLY_DATATABLE = PUBLIC_APPLY.resultTable;
                        logger.strJobQueue = "今日公用事業約定檔需產生，筆數為=" + PUBLIC_APPLY_Query_Count;
                        logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                        break;

                    case "F0023": //無需處理資料
                        logger.strJobQueue = "今日公用事業約定檔無資料需做約定，RC=" + PUBLIC_APPLY_RC;
                        logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                        break;

                    default: //資料庫錯誤
                        logger.strJobQueue = "查詢PUBLIC_APPLY 資料錯誤:" + PUBLIC_APPLY_RC;
                        logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                        return "B0016:" + logger.strJobQueue;
                }
                #endregion
                                              
                //寫暫存table
                for (int i = 0; i < SETUP_PUBLIC_Query_Count; i++)
                {
                    intFD_CNT = 0;
                    //定義一個空的字串來擺放FD
                    String[] strDATA_FH = null;
                    String[] strDATA_FD = null;
                    String[] strDATA_FT = null;
                            
                    string strPAY_TYPE = SETUP_PUBLIC_DATATABLE.Rows[i]["PAY_TYPE"].ToString().Trim();
                    string strTRANS_TYPE = SETUP_PUBLIC_DATATABLE.Rows[i]["FILE_TRANSFER_TYPE"].ToString().Trim() + "1"; //最末碼="1",表示異動帳號
                    string strDESCR = SETUP_PUBLIC_DATATABLE.Rows[i]["DESCR"].ToString().Trim();

                    #region 產生檔案內容 & 更新PUBLIC_APPLY

                    //*** HEAD ***
                    moveDATA_FH(strTRANS_TYPE);
                    strDATA_FH = xml.DataTable2FileStrArray(BIG5, FH_DataTable, FH_xml_DataTable, "");  // 帶有資料的欄位TABLE，與產出字串的XML MAPPING，組出FH的字串

                    //*** BODY ***
                    #region 依代繳類別建立該類約定檔Table
                    FD_DataTable.Clear();
                    if (PUBLIC_APPLY_Query_Count > 0)
                    {
                        PUBLIC_APPLY_DataRow = PUBLIC_APPLY_DATATABLE.Select("PAY_TYPE='" + strPAY_TYPE + "'");
                        if (PUBLIC_APPLY_DataRow.Length > 0)
                        {
                            for (int j = 0; j < PUBLIC_APPLY_DataRow.Length; j++)
                            {
                                strWriteFD = "";
                                //組明細
                                moveDATA_FD(strTRANS_TYPE, j);
                                //寫出約定檔才逐筆更新主檔生效註記
                                if (strWriteFD == "Y")
                                {
                                    #region 更新PUBLIC_APPLY 傳送公用事業單位日期及註記
                                    PUBLIC_APPLY_U.init();
                                    //更新欄位
                                    PUBLIC_APPLY_U.strREPLY_FLAG = "Y";
                                    PUBLIC_APPLY_U.strVAILD_FLAG = strVAILD_FLAG;
                                    PUBLIC_APPLY_U.strREPLY_DTE = TODAY_PROCESS_DTE.ToString("yyyyMMdd");
                                    PUBLIC_APPLY_U.strMNT_USER = strJobName;
                                    PUBLIC_APPLY_U.datetimeMNT_DT = TODAY_PROCESS_DTE;
                                    //條件
                                    PUBLIC_APPLY_U.strWherePAY_TYPE = strPAY_TYPE;
                                    PUBLIC_APPLY_U.strWhereSEQ = PUBLIC_APPLY_DataRow[j]["SEQ"].ToString();
                                    PUBLIC_APPLY_U.strWherePAY_NBR = PUBLIC_APPLY_DataRow[j]["PAY_NBR"].ToString();
                                    PUBLIC_APPLY_U.strWherePAY_CARD_NBR = PUBLIC_APPLY_DataRow[j]["PAY_CARD_NBR"].ToString();
                                    //執行
                                    PUBLIC_APPLY_U_RC = PUBLIC_APPLY_U.update();
                                    switch (PUBLIC_APPLY_U_RC)
                                    {
                                        case "S0000": //更新成功   
                                            PUBLIC_APPLY_Update_Count = PUBLIC_APPLY_Update_Count + PUBLIC_APPLY_U.intUptCnt;
                                            if (PUBLIC_APPLY_U.intUptCnt == 0)
                                            {
                                                logger.strJobQueue = "更新PUBLIC_APPLY 資料錯誤: F0023, PAY_CARD_NBR = " + PAY_CARD_NBR + " SEQ = " + SEQ;
                                                logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                                                return "F0023" + logger.strJobQueue;
                                            }
                                            break;

                                        default: //資料庫錯誤
                                            logger.strJobQueue = "更新PUBLIC_APPLY 資料錯誤:" + PUBLIC_APPLY_U_RC;
                                            logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                                            return "B0016:" + logger.strJobQueue;
                                    }
                                    #endregion
                                }
                            }
                            strDATA_FD = xml.DataTable2FileStrArray(BIG5, FD_DataTable, FD_xml_DataTable, "");  // 帶有資料的欄位TABLE，與產出字串的XML MAPPING，組出FD的字串
                        }

                        logger.strJobQueue = "今日產出約定" + strDESCR + "筆數為 : " + PUBLIC_APPLY_DataRow.Length;
                        logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);

                    }                

                    #endregion
                   
                    //*** TRAIL ***
                    moveDATA_FT(strTRANS_TYPE);
                    strDATA_FT = xml.DataTable2FileStrArray(BIG5, FT_DataTable, FT_xml_DataTable, "");  // 帶有資料的欄位TABLE，與產出字串的XML MAPPING，組出FT的字串

                    #endregion
                    
                    #region 產出檔案，依序寫出FH --> FD --> FT

                    //寫出PMT_DD_APPLY Header檔
                    Cybersoft.Base.CMCURL001 CMCURL = new CMCURL001();
                    strOutFileName = strFilePath + strTRANS_TYPE + strFileName;
                    strOutFileName = CMCURL.ReplaceVarDateFormat(strOutFileName, TODAY_PROCESS_DTE);
                    FileStream fsOutFile = new FileStream(strOutFileName, FileMode.Create, FileAccess.Write, FileShare.None);
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
                        logger.strJobQueue = "今日財金-公共事業約定檔產出有誤(" + strOutFileName + ")，請與系統負責人確認!!";
                        logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                        return "B0012：" + logger.strJobQueue;
                    }
                    #endregion

                    #region 搬出email資訊
                    DataRow DR = REPORT_DataTable_MAIL.NewRow();
                    DR["PAY_TYPE"] = strPAY_TYPE + "-" + strDESCR;
                    DR["FILE_NAME"] = strTRANS_TYPE + TODAY_CH_DTE;
                    DR["CNT"] = intFD_CNT.ToString("#,###,###,##0").PadLeft(13, ' ');
                    REPORT_DataTable_MAIL.Rows.Add(DR);
                    #endregion

                }

                #region 判別更新筆數是否相同
                if (PUBLIC_APPLY_Update_Count != PUBLIC_APPLY_DATATABLE.Rows.Count)
                {
                    logger.strJobQueue = "更新 PUBLIC_APPLY 時筆數異常,原筆數 : " + PUBLIC_APPLY_DATATABLE.Rows.Count + " 實際筆數: " + PUBLIC_APPLY_Update_Count;
                    logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                    //return "B0012" + logger.strJobQueue;
                }
                else
                {
                    logger.strJobQueue = "更新 PUBLIC_APPLY 成功筆數 =" + PUBLIC_APPLY_Update_Count;
                    logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                }
                #endregion

                #region email通知
                email();
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

        #region writeDisplay
        void writeDisplay()
        {
            logger.strJobID = "00002";
            //擷取台電需產出約定檔的資料
            logger.strTBL_NAME = "PUBLIC_APPLY";
            logger.strTBL_ACCESS = "Q";
            logger.intTBL_COUNT = PUBLIC_APPLY_Query_Count;
            logger.writeCounter();
                        
            //更新PUBLIC_APPLY
            logger.strTBL_NAME = "PUBLIC_APPLY";
            logger.strTBL_ACCESS = "U";
            logger.intTBL_COUNT = PUBLIC_APPLY_Update_Count;
            logger.writeCounter();

            logger.DisplayCounter(Cybersoft.Coca.Log.LevelEnum.Info);
        }
        #endregion
                
        #region 組下傳檔
        #region 組FH
        private void moveDATA_FH(string strTRANS_TYPE)
        {
            FH_DataTable.Clear();
            FH_DataTable.Rows.Add();

            //區別碼(首筆為"1")
            FH_DataTable.Rows[0]["REC_TYPE_H"] = "1";
            //異動類別
            FH_DataTable.Rows[0]["TRANSFER_TYPE_H"] = strTRANS_TYPE;
            //交易日期  (民國YYYMMDD)
            FH_DataTable.Rows[0]["TRANSFER_DTE_H"] = TODAY_CH_DTE;
            //發送單位代號(通知:"1"  /  結果:"2")
            FH_DataTable.Rows[0]["TYPE"] = "1";

        }
        #endregion

        #region 組FD
        private void moveDATA_FD(string strTRANS_TYPE, int j)
        {
            #region 判斷異動原因前一周新增或終止
            //初始值
            strVAILD_FLAG = "";
            string CHANGE_FLAG = "0";
            string CHANGE_DTE = dtStart.ToString("yyyyMMdd");
            string APPLY_DTE = PUBLIC_APPLY_DataRow[j]["APPLY_DTE"].ToString().Trim();
            string EXPIR_DTE = PUBLIC_APPLY_DataRow[j]["EXPIR_DTE"].ToString().Trim();
            string PAY_ACCT_NBR = PUBLIC_APPLY_DataRow[j]["PAY_ACCT_NBR"].ToString().Trim();
            string PAY_ACCT_NBR_PREV = PUBLIC_APPLY_DataRow[j]["PAY_ACCT_NBR_PREV"].ToString().Trim();
            CHANGE_DTE = TODAY_PROCESS_DTE.AddYears(-1911).ToString("yyyyMMdd").Substring(1, 7);
            //依申請日與到期日判斷異動原因(1:新增  2:終止 3:修改-僅市水帳號有此功能) 
            if ((strTRANS_TYPE == "011201") && (PAY_ACCT_NBR != PAY_ACCT_NBR_PREV) && (PAY_ACCT_NBR_PREV != "") ) //市水
            {
                CHANGE_FLAG = "3";
                strVAILD_FLAG = "Y";
            }
            else
                if ((Convert.ToInt32(APPLY_DTE) <=  Convert.ToInt32(TODAY_PROCESS_DTE.ToString("yyyyMMdd")))&&
                    (Convert.ToInt32(EXPIR_DTE) >  Convert.ToInt32(TODAY_PROCESS_DTE.ToString("yyyyMMdd"))))
                {
                    CHANGE_FLAG = "1";
                    strVAILD_FLAG = "Y";
                    //CHANGE_DTE = (DateTime.ParseExact(TODAY_PROCESS_DTE, "yyyyMMdd", null)).AddYears(-1911).ToString("yyyyMMdd").Substring(1, 7);
                }
                else
                    if (Convert.ToInt32(EXPIR_DTE) <=  Convert.ToInt32(TODAY_PROCESS_DTE.ToString("yyyyMMdd")))
                    {
                        CHANGE_FLAG = "2";
                        strVAILD_FLAG = "N";
                        //CHANGE_DTE = (DateTime.ParseExact(EXPIR_DTE, "yyyyMMdd", null)).AddYears(-1911).ToString("yyyyMMdd").Substring(1, 7);
                    }
                    else
                    {
                        strVAILD_FLAG = "Y";
                        logger.strJobQueue = "第" + (j + 1) + "筆資料有誤, PAY_NBR = " + PAY_NBR + ", APPLY_DTE = " + APPLY_DTE + ", EXPIR_DTE = " + EXPIR_DTE + ", 請確認!!";
                        logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                        return;
                    }
            #endregion

            //FD_DataTable.Clear();
            FD_DataTable.Rows.Add();
            int i = FD_DataTable.Rows.Count -1;
            string strRECV_UNIT = string.Empty;
            //區別碼(明細為"2")
            FD_DataTable.Rows[i]["REC_TYPE"] = "2";
            //異動類別
            FD_DataTable.Rows[i]["TRANSFER_TYPE"] = strTRANS_TYPE;
            //交易日期  (民國YYYYMMDD)
            FD_DataTable.Rows[i]["TRANSFER_DTE"] = TODAY_CH_DTE;

            //發件單位
            FD_DataTable.Rows[i]["SEND_UNIT"] = strBANK_NBR.PadRight(8, ' ');
            //收件單位
            FD_DataTable.Rows[i]["RECV_UNIT"] = "99900002".PadRight(8, ' ');
            //委託單位
            FD_DataTable.Rows[i]["TRANSFER_UNIT"] = PUBLIC_APPLY_DataRow[j]["TRANSFER_UNIT"].ToString().Trim().PadRight(8, ' ');
            //交易序號
            FD_DataTable.Rows[i]["SEQ"] = "000B" + FD_DataTable.Rows.Count.ToString().PadLeft(6, '0');
            //銀行代碼
            FD_DataTable.Rows[i]["BANK_NBR"] = strBANK_NBR.PadRight(7, ' ');
            //帳號(16碼帳號)
            FD_DataTable.Rows[i]["PAY_ACCT_NBR"] = PUBLIC_APPLY_DataRow[j]["PAY_ACCT_NBR"].ToString().PadRight(16, ' ');
            //帳戶ID
            FD_DataTable.Rows[i]["CUST_ID"] = PUBLIC_APPLY_DataRow[j]["CUST_ID"].ToString().Trim().PadRight(10, ' ');
            //異動日期
            FD_DataTable.Rows[i]["CHANGE_DTE"] = CHANGE_DTE;
            //異動原因
            FD_DataTable.Rows[i]["CHANGE_FLAG"] = CHANGE_FLAG;
            //銀行專用區
            FD_DataTable.Rows[i]["BANK_AREA"] = string.Empty;
            //異動資料區
            switch (strTRANS_TYPE)
            {
                case "010101":  //台灣自來水
                    strCHANGE_AREA = PUBLIC_APPLY_DataRow[j]["PAY_NBR"].ToString().Trim() + "AP";
                    //收件單位
                    FD_DataTable.Rows[i]["RECV_UNIT"] = PUBLIC_APPLY_DataRow[j]["TRANSFER_UNIT"].ToString().Trim().PadRight(8, ' ');
                    //委託單位
                    FD_DataTable.Rows[i]["TRANSFER_UNIT"] = PUBLIC_APPLY_DataRow[j]["TRANSFER_UNIT"].ToString().Trim().PadRight(8, ' ');
                    //帳號(16碼帳號)
                    if (FD_DataTable.Rows[i]["PAY_ACCT_NBR"].ToString().Substring(0, 2) == "96")  //若PRODUCT_SERVICE_3為空白則繳款帳號=卡號
                    {
                        FD_DataTable.Rows[i]["PAY_ACCT_NBR"] = "00" + FD_DataTable.Rows[i]["PAY_ACCT_NBR"];
                    }

                    break;
                case "010201":  //台電
                    strCHANGE_AREA = PUBLIC_APPLY_DataRow[j]["PAY_NBR"].ToString().Trim();
                    //收件單位
                    FD_DataTable.Rows[i]["RECV_UNIT"] = "99900002".PadRight(8, ' ');
                    //委託單位
                    FD_DataTable.Rows[i]["TRANSFER_UNIT"] = PUBLIC_APPLY_DataRow[j]["TRANSFER_UNIT"].ToString().Trim().PadRight(8, ' ');
                    //帳號(16碼帳號)
                    if (FD_DataTable.Rows[i]["PAY_ACCT_NBR"].ToString().Substring(0, 2) == "96")  //若PRODUCT_SERVICE_3為空白則繳款帳號=卡號
                    {
                        FD_DataTable.Rows[i]["PAY_ACCT_NBR"] = "00" + FD_DataTable.Rows[i]["PAY_ACCT_NBR"];
                    }
                    break;
                case "011201":  //台北市自來水
                    strCHANGE_AREA = PUBLIC_APPLY_DataRow[j]["PAY_NBR"].ToString().Trim();
                    //收件單位
                    FD_DataTable.Rows[i]["RECV_UNIT"] = PUBLIC_APPLY_DataRow[j]["TRANSFER_UNIT"].ToString().Trim().PadRight(8, ' ');
                    //委託單位
                    FD_DataTable.Rows[i]["TRANSFER_UNIT"] = PUBLIC_APPLY_DataRow[j]["TRANSFER_UNIT"].ToString().Trim().PadRight(8, ' ');
                    break;
                default:
                    strCHANGE_AREA = PUBLIC_APPLY_DataRow[j]["PAY_NBR"].ToString().Trim();
                    break;
            }
            FD_DataTable.Rows[i]["CHANGE_AREA"] = strCHANGE_AREA;

            ++intFD_CNT;
            strWriteFD = "Y";
        }
        
        #endregion

        #region 組FT
        private void moveDATA_FT(string strTRANS_TYPE)
        {
            FT_DataTable.Clear();
            FT_DataTable.Rows.Add();
            
            //區別碼(尾筆為"3")
            FT_DataTable.Rows[0]["REC_TYPE_T"] = "3";
            //異動類別
            FT_DataTable.Rows[0]["TRANSFER_TYPE_T"] = strTRANS_TYPE;
            //交易日期  (民國YYYYMMDD)
            FT_DataTable.Rows[0]["TRANSFER_DTE_T"] = TODAY_CH_DTE;
            //交易總筆數
            FT_DataTable.Rows[0]["TOT_CNT"] = FD_DataTable.Rows.Count.ToString().PadLeft(10, '0');
            //成功總筆數
            FT_DataTable.Rows[0]["SUCC_CNT"] = "0000000000";
            //失敗總筆數
            FT_DataTable.Rows[0]["UNSUCC_CNT"] = "0000000000";

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
            email_subject = "[INFO][Batch]" + TODAY_PROCESS_DTE.ToString("yyyy-MM-dd") + "公用事業-財金約定檔已產生";
            //Email內容
            #region Email內容
            strEmailBody = "今日財金-公用事業約定檔已產出!";
            email_body += "</br> " + strEmailBody + "</br>";
            email_body += "<table style='border:2px solid black;font-size:12px;'>";
            email_body += "<caption>" + TODAY_PROCESS_DTE.ToString("yyyy-MM-dd") + " 財金約定檔作業</caption>";
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

