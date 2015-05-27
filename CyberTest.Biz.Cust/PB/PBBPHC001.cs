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
    /// 產生中華電信約定檔
    /// 
    /// </summary>
    public class PBBPHC001
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
        //String SETUP_PUBLIC_RC = "";
        SETUP_PUBLICDao SETUP_PUBLIC = new SETUP_PUBLICDao();
        DataTable SETUP_PUBLIC_DATATABLE = new DataTable();

        DataTable FD_DataTable = new DataTable();
        #endregion

        #region 設定檔案編碼
        Encoding BIG5 = Encoding.GetEncoding("big5");
        #endregion

        #region 副程式 (Del)
        //CMCNBR001 CMCNBR001 = new CMCNBR001();  //中銀客製
        //String PHONE_MASK_STRING = "";
        #endregion

        #region 宣告共用變數
        string strJobName = "";
        //批次日期
        DateTime TODAY_PROCESS_DTE = new DateTime();
        String TODAY_YYYYMMDD = "";
        String TODAY_YYYMMDD = "";

        //筆數&金額
        int PUBLIC_APPLY_Update_Count = 0;
        int PUBLIC_APPLY_Update_Count2 = 0;
        int PUBLIC_APPLY_Query_Count = 0;
        int PUBLIC_APPLY_Upd_ReplyCNT = 0;
        
        int PHONE_TXT_Count = 0;

        //中華電信約定檔欄位
        String CHANGE_TYPE = "";

        //PUBLIC_APPLY欄位
        //String PAY_CARD_NBR = "";
        //String PAY_TYPE = "";
        //String PAY_NBR = "";
        //String SEQ = "";
        //String EXPIR_DTE = "";
        //String APPLY_DTE = "";
        //String REPLY_FLAG = "";
        String[] strDATA_FD = null;

        //檔案長度
        const int intDataLength = 107;

        #endregion

        #region 宣告檔案路徑

        //寫出檔案名稱
        String strOutFileName = "";

        //寫出檔案路徑
        String FILE_PATH = "";
        FileParseByXml xml = new FileParseByXml();

        String strRecNAME = "";
        String strPay_Card_Trans = "";

        String strPAY_TYPE = "0001";  //ttt
        #endregion

        static readonly Cybersoft.Coca.Log.Logger logger = Cybersoft.Log.LogManager<Cybersoft.Coca.Log.Logger>.GetLogger(System.Reflection.MethodInfo.GetCurrentMethod().ReflectedType.Name);
        //public string RUN(string strPAY_TYPE)
        public string RUN()
        {
            #region 準備MSGLG.SYSOUT CLASS
            strJobName = System.Reflection.MethodInfo.GetCurrentMethod().ReflectedType.Name;
            logger.strJobID = JobID;
            logger.strJOBNAME = strJobName;
            logger.dtRunDate = DateTime.Now;
            #endregion

            try
            {
                #region 取得系統資訊(下次批次日)
                CMCSYS001 SYSINF = new CMCSYS001();
                SYSINF.strJobID = JobID;
                SYSINF.strJobName = strJobName;
                String SYSINF_RC = SYSINF.getSYSINF();
                TODAY_PROCESS_DTE = SYSINF.datetimeTODAY_PROCESS_DTE;
                TODAY_YYYYMMDD = TODAY_PROCESS_DTE.ToString("yyyyMMdd");
                TODAY_YYYMMDD = TODAY_PROCESS_DTE.AddYears(-1911).ToString("yyyyMMdd").Substring(1, 7);
                #endregion

                if (strPAY_TYPE != "")
                {
                    #region 讀取公共事業參數
                    SETUP_PUBLIC.init();

                    SETUP_PUBLIC.strWherePAY_TYPE = strPAY_TYPE;
                    SETUP_PUBLIC.strWherePOST_RESULT = "00";
                    string SETUP_PUBLIC_RC = SETUP_PUBLIC.query();

                    switch (SETUP_PUBLIC_RC)
                    {
                        case "S0000": //查詢成功
                            int SETUP_PUBLIC_Query_Count = SETUP_PUBLIC.resultTable.Rows.Count;
                            //取得參數內容(檔案名稱)
                            if (SETUP_PUBLIC_Query_Count == 1)
                            {
                                strRecNAME = SETUP_PUBLIC.resultTable.Rows[0]["FILE_FORMAT"].ToString().Trim();
                                strPay_Card_Trans = SETUP_PUBLIC.resultTable.Rows[0]["PAY_CARD_TRANS"].ToString().Trim();
                            }
                            else
                            {
                                logger.strJobQueue = "**機構代碼設定筆數有誤(相同類別代碼設定超過1筆)**";
                                logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                                return "B0016:" + logger.strJobQueue;
                            }
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
                }
                else
                {
                    logger.strJobQueue = "參數檔未設定檔案格式 : 機構代碼(" + strPAY_TYPE + ") ";
                    logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                    return "B0099:" + logger.strJobQueue;
                }

                #region 宣告檔案路徑
                Cybersoft.Base.CMCURL001 CMCURL = new CMCURL001();
                //檔案格式
                string strPmtFileLayout = CMCURL.getURL("PHONE_CARD2BANK");
                if (strPmtFileLayout == "")
                {
                    logger.strJobQueue = "格式取得錯誤!!! PHONE_CARD2BANK <URL> 未設定";
                    logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                    return "B0099:" + logger.strJobQueue;
                }
                //檔案路徑
                string strFilePath = CMCURL.getPATH("PHONE_CARD2BANK");
                if (strFilePath == "")
                {
                    logger.strJobQueue = "路徑取得錯誤!!! PHONE_CARD2BANK <PATH> 未設定";
                    logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                    return "B0099:" + logger.strJobQueue;
                }
                //檔案名稱
                String strFileName = CMCURL.getFILE_NAME("PHONE_CARD2BANK");
                if (strFileName == "")
                {
                    logger.strJobQueue = "檔名取得錯誤!!! PHONE_CARD2BANK <FILE_NAME> 未設定" ;
                    logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                    return "B0099:" + logger.strJobQueue;
                }
                else
                {                    
                    strFileName =strFileName + "_CHGOUT";
                    strFileName = CMCURL.ReplaceVarDateFormat(strFileName, TODAY_PROCESS_DTE);                    
                }
                //附檔名
                String strEXT = CMCURL.getEXT("PHONE_CARD2BANK");
                if (strEXT != "")
                {
                    strFileName = strFileName + strEXT;
                }
                //
                FILE_PATH = strFilePath + strFileName;
                logger.strJobQueue = " PHONE_CHGOUT路徑為 " + FILE_PATH;
                logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                #endregion

                #region 載入檔案格式資訊
                FileParseByXml xml = new FileParseByXml();
                
                DataTable FD_XmlTable = xml.Xml2DataTable(strPmtFileLayout, "PHONE_CHGOUT");
                if (xml.strMSG.Length > 0)
                {
                    logger.strJobQueue = "[Xml2DataTable(FD)] - " + xml.strMSG.ToString().Trim();
                    logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                    return "B0099:" + logger.strJobQueue;
                }
                
                FD_DataTable = xml.dtXML2DataTable(FD_XmlTable);
                #endregion
                                
                #region (for RE-RUN)復原PUBLIC_APPLY_中華電信
                PUBLIC_APPLY.init();
                int PUBLIC_APPLY_RECOV_Count = 0;
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
                        logger.strJobQueue = "(For Rerun)回覆PUBLIC_APPLY原狀態(PAY_TYPE(0001),筆數為：" + PUBLIC_APPLY_RECOV_Count;
                        logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                        break;

                    case "F0023": //查無資料
                        logger.strJobQueue = "更新PUBLIC_APPLY 無資料(For Rerun),RC=" + PUBLIC_APPLY_RC;
                        logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                        break;

                    default: //資料庫錯誤
                        logger.strJobQueue = "更新PUBLIC_APPLY 資料錯誤:" + PUBLIC_APPLY_RC;
                        logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                        return "B0016:" + logger.strJobQueue;
                }
                #endregion

                #region 到期日<=今日&生效註記="Y"，維護傳送註記&傳送日期為空白
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
                        PUBLIC_APPLY_Upd_ReplyCNT = PUBLIC_APPLY.intUptCnt;
                        break;

                    case "F0023": //無需處理資料
                        logger.strJobQueue = "今日無中華電信約定檔需產生;";
                        logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                        break;

                    default: //資料庫錯誤
                        logger.strJobQueue = "查詢PUBLIC_APPLY 資料錯誤:" + PUBLIC_APPLY_RC;
                        logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                        return "B0016:" + logger.strJobQueue;
                }
                #endregion

                #region 擷取今日需送約定檔之中華電信資料
                PUBLIC_APPLY.init();
                PUBLIC_APPLY.strWherePAY_TYPE = strPAY_TYPE;
                PUBLIC_APPLY.strWhereREPLY_FLAG = " ";
                PUBLIC_APPLY_RC = PUBLIC_APPLY.query();
                switch (PUBLIC_APPLY_RC)
                {
                    case "S0000": //查詢成功
                        PUBLIC_APPLY_Query_Count = PUBLIC_APPLY.resultTable.Rows.Count;
                        break;

                    case "F0023": //無需處理資料
                        logger.strJobQueue = "今日無中華電信約定檔需產生;";
                        logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                        break;

                    default: //資料庫錯誤
                        logger.strJobQueue = "查詢PUBLIC_APPLY 資料錯誤:" + PUBLIC_APPLY_RC;
                        logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                        return "B0016:" + logger.strJobQueue;
                }
                #endregion

                for (int i = 0; i < PUBLIC_APPLY.resultTable.Rows.Count; i++)
                {
                    #region 取得欄位內容
                    string SEQ = Convert.ToString(PUBLIC_APPLY.resultTable.Rows[i]["SEQ"]);
                    string PAY_TYPE = Convert.ToString(PUBLIC_APPLY.resultTable.Rows[i]["PAY_TYPE"]);
                    string PAY_CARD_NBR = Convert.ToString(PUBLIC_APPLY.resultTable.Rows[i]["PAY_CARD_NBR"]);
                    string PAY_NBR = Convert.ToString(PUBLIC_APPLY.resultTable.Rows[i]["PAY_NBR"]);
                    string APPLY_DTE = Convert.ToString(PUBLIC_APPLY.resultTable.Rows[i]["APPLY_DTE"]);
                    string EXPIR_DTE = Convert.ToString(PUBLIC_APPLY.resultTable.Rows[i]["EXPIR_DTE"]);
                    #endregion

                    #region 判斷異動種類和組明細
                    //判斷異動種類：
                    //APPLY_DTE >= TODAY → 新增，代碼"1" 
                    //EXPIR_DTE == TODAY → 終止，代碼"3"
                    //其他 → 異動，代碼"2"
                    if (APPLY_DTE == EXPIR_DTE)
                    {
                        #region 更新PUBLIC_APPLY
                        PUBLIC_APPLY.init();
                        PUBLIC_APPLY.strREPLY_FLAG = "N";
                        PUBLIC_APPLY.strREPLY_DTE = TODAY_YYYYMMDD;
                        PUBLIC_APPLY.strVAILD_FLAG = "N";
                        PUBLIC_APPLY.strMNT_USER = strJobName;
                        PUBLIC_APPLY.datetimeMNT_DT = TODAY_PROCESS_DTE;
                        //PUBLIC_APPLY.strMNT_USER = "PBBPHC001";
                        //PUBLIC_APPLY.datetimeMNT_DT = TODAY_PROCESS_DTE;
                        PUBLIC_APPLY.strWherePAY_TYPE = PAY_TYPE;
                        PUBLIC_APPLY.strWhereSEQ = SEQ;
                        PUBLIC_APPLY.strWherePAY_NBR = PAY_NBR;
                        PUBLIC_APPLY.strWherePAY_CARD_NBR = PAY_CARD_NBR;

                        PUBLIC_APPLY_RC = PUBLIC_APPLY.update();
                        switch (PUBLIC_APPLY_RC)
                        {
                            case "S0000": //更新成功   
                                PUBLIC_APPLY_Update_Count2 = PUBLIC_APPLY_Update_Count2 + PUBLIC_APPLY.intUptCnt;
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
                        continue;
                    }
                    #endregion
                    
                    //寫TABLE
                    CHANGE_TYPE = "";
                    MoveCHGOUT(PUBLIC_APPLY.resultTable.Rows[i]);
                    // TABLE轉FD的字串
                    strDATA_FD = xml.DataTable2FileStrArray(BIG5, FD_DataTable, FD_XmlTable, "");

                    #region 更新PUBLIC_APPLY
                    PUBLIC_APPLY.init();
                    PUBLIC_APPLY.strREPLY_FLAG = "Y";
                    PUBLIC_APPLY.strREPLY_DTE = TODAY_YYYYMMDD;
                    PUBLIC_APPLY.strMNT_USER = strJobName;
                    PUBLIC_APPLY.datetimeMNT_DT = TODAY_PROCESS_DTE;
                    if ((CHANGE_TYPE == "1") || (CHANGE_TYPE == "2"))
                    {
                        PUBLIC_APPLY.strVAILD_FLAG = "Y";
                    }
                    else if (CHANGE_TYPE == "3")
                    {
                        PUBLIC_APPLY.strVAILD_FLAG = "N";
                    }
                    //PUBLIC_APPLY.strMNT_USER = "PBBPHC001";
                    //PUBLIC_APPLY.datetimeMNT_DT = TODAY_PROCESS_DTE;
                    PUBLIC_APPLY.strWherePAY_TYPE = PAY_TYPE;
                    PUBLIC_APPLY.strWhereSEQ = SEQ;
                    PUBLIC_APPLY.strWherePAY_NBR = PAY_NBR;
                    PUBLIC_APPLY.strWherePAY_CARD_NBR = PAY_CARD_NBR;
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

                #region 產生約定檔 陣列

                //設定產出檔案名稱                
                strOutFileName = FILE_PATH;
                FileStream fsOutFile = new FileStream(strOutFileName, FileMode.Create, FileAccess.Write, FileShare.None);
                using (StreamWriter srOutFile = new StreamWriter(fsOutFile, BIG5))
                {
                    //逐筆寫出資料
                    for (int k = 0; k < strDATA_FD.Count(); k++)
                    {
                        srOutFile.Write(strDATA_FD[k]);
                        srOutFile.Write("\r\n");
                        srOutFile.Flush();
                        PHONE_TXT_Count = PHONE_TXT_Count + 1;
                    }
                    srOutFile.Close();
                }
                fsOutFile.Close();


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

        #region Display
        void writeDisplay()
        {
            logger.strJobID = "00002";
            //擷取中華電信需產出約定檔的資料
            logger.strTBL_NAME = "PUBLIC_APPLY";
            logger.strTBL_ACCESS = "Q";
            logger.intTBL_COUNT = PUBLIC_APPLY_Query_Count;
            logger.writeCounter();

            //中華電信約定檔筆數(含頭尾)
            logger.strTBL_NAME = "PHONE_CHGOUT";
            logger.strTBL_ACCESS = "I";
            logger.intTBL_COUNT = PHONE_TXT_Count;
            logger.writeCounter();

            //更新PUBLIC_APPLY
            logger.strTBL_NAME = "PUBLIC_APPLY";
            logger.strTBL_ACCESS = "U";
            logger.intTBL_COUNT = PUBLIC_APPLY_Update_Count;
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
            logger.intTBL_COUNT = PUBLIC_APPLY_Upd_ReplyCNT;
            logger.strMEMO = "當天到期，生效註記更新為-已失效";
            logger.writeCounter();

            logger.DisplayCounter(Cybersoft.Coca.Log.LevelEnum.Info);
        }
        #endregion

        #region moveFD
        void MoveCHGOUT(DataRow drPUBLIC_APPLY)
        {            
            FD_DataTable.Rows.Add();
            int i = FD_DataTable.Rows.Count -1;

            string strPAY_NBR = Convert.ToString(drPUBLIC_APPLY["PAY_NBR"]);
            string strAPPLY_DTE = Convert.ToString(drPUBLIC_APPLY["APPLY_DTE"]);
            string strEXPIR_DTE = Convert.ToString(drPUBLIC_APPLY["EXPIR_DTE"]);

            #region 判斷異動種類和組明細  (新增:1 更改:2 刪除:3)
            //判斷異動種類：
            //APPLY_DTE >= TODAY → 新增，代碼"1" 
            //EXPIR_DTE == TODAY → 終止，代碼"3"
            //其他 → 異動，代碼"2"
            //判斷異動類別:新增:1 更改:2 刪除:3
            if (Convert.ToInt32(strEXPIR_DTE) <= Convert.ToInt32(TODAY_YYYYMMDD))
            {
                CHANGE_TYPE = "3";
            }
            else
            {
                if (Convert.ToInt32(strAPPLY_DTE) <= Convert.ToInt32(TODAY_YYYYMMDD))
                {
                    CHANGE_TYPE = "1";
                }
                else
                {
                    CHANGE_TYPE = "2";
                }
            }
            #endregion
            //
            FD_DataTable.Rows[i]["FILLER1"] = " ";
            FD_DataTable.Rows[i]["BATCH_NO"] = Convert.ToString(drPUBLIC_APPLY["PAY_TYPE"]);
            FD_DataTable.Rows[i]["OFFICE_CODE"] = strPAY_NBR.Substring(0, 4).Trim().PadRight(4,' ');
            FD_DataTable.Rows[i]["TELNO"] = strPAY_NBR.Substring(5, 10).Trim().PadLeft(12, ' ');
            FD_DataTable.Rows[i]["TX_CODE"] = "91"; 
            FD_DataTable.Rows[i]["FILLER2"] = new string(' ', 30);
            FD_DataTable.Rows[i]["CHANGE_TYPE"] = CHANGE_TYPE;
            if (strPay_Card_Trans == "N")
            {
                FD_DataTable.Rows[i]["CARDIT_NO"] = Convert.ToString(drPUBLIC_APPLY["PAY_CARD_NBR"]);
            }
            else
            { }
            FD_DataTable.Rows[i]["FILLER3"] =" " ;
            FD_DataTable.Rows[i]["PAY_TYPE"] = "6";
            FD_DataTable.Rows[i]["FILLER4"] = new string(' ', 35);

            return;
        }
        #endregion
        
    }
}

