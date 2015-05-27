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
    /// 讀中華電信約定回覆檔更新PUBLIC_APPLY
    /// </summary>
    public class PBBPHR001
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
        DataTable SETUP_PUBLIC_DATATABLE = new DataTable();

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
        String TODAY_YYYMMDD = "";
        String ERROR_CODE_DESCR = "";

        //筆數&金額
        int PUBLIC_APPLY_Update_Count = 0;
        int k = 0; //REPORT_TABLE
        int intFailCount = 0;
        int intReadInfDataCount = 0;

        //中華電信約定回覆檔欄位
        String strJobName = "PBBPHR001";
        String REPLY_DTE = "";
        String APPLY_DTE = "";
        String PAY_TYPE = "";
        String PAY_CARD_NBR = "";
        String OFFICE_CODE = "";
        String TELNO = "";
        String PAY_NBR = "";
        String CHANGE_TYPE = "";
        String ERROR_CODE = "";
        String TX_CODE = "";
        string strBANK_NAME = "";
        String strPAY_TYPE = "0001";  //ttt
        //檔案長度
        const int intDataLength = 107;
        string strRecNAME = "";
        string strPay_Card_Trans = "";
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
        public string RUN()
        {
            #region 準備MSGLG.SYSOUT CLASS
            logger.strJobID = JobID;
            logger.strJOBNAME = "PBBPHR001";
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
                NEXT_PROCESS_DTE = SYSINF.datetimeNEXT_PROCESS_DTE;
                TODAY_YYYMMDD = TODAY_PROCESS_DTE.AddYears(-1911).ToString("yyyyMMdd").Substring(1, 7);
                strBANK_NAME = SYSINF.strREPORT_TITLE;
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
                //XML名稱
                strXML_Layout = CMCURL.getURL(strRecNAME + ".xml");
                //寫出檔案路徑
                FILE_PATH = CMCURL.getPATH(strRecNAME + "_CHGIN");

                if (strXML_Layout == "" || FILE_PATH == "")
                {
                    logger.strJobQueue = "路徑取得錯誤!!! " + strRecNAME + ".xml路徑為 " + strXML_Layout + strRecNAME +" _CHGIN路徑為 " + FILE_PATH;
                    logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                    return "B0099:" + logger.strJobQueue;
                }
                else
                {
                    FILE_PATH = FILE_PATH.Replace("yyymmdd", TODAY_YYYMMDD);  //替換日期
                    logger.strJobQueue = strRecNAME + ".xml路徑為 " + strXML_Layout + strRecNAME + " _CHGIN路徑為 " + FILE_PATH;
                    logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                }

                string Check_RC = CMCURL.isFileExists(FILE_PATH).ToString();
                if (Check_RC.Substring(0, 5) != "S0000")
                {
                    logger.strJobQueue = Check_RC.Trim();
                    logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                    writeReport(REPORT_TABLE_SORT);
                    return "B0000";
                }
                #endregion

                #region 定義報表Table
                REPORT_TABLE.Columns.Add("SEQ", typeof(string));
                REPORT_TABLE.Columns.Add("APPLY_DTE", typeof(string));
                REPORT_TABLE.Columns.Add("REPLY_DTE", typeof(string));
                REPORT_TABLE.Columns.Add("OFFICE_CODE", typeof(string));
                REPORT_TABLE.Columns.Add("TELNO", typeof(string));
                REPORT_TABLE.Columns.Add("PAY_CARD_NBR", typeof(string));
                REPORT_TABLE.Columns.Add("CHANGE_TYPE", typeof(string));
                REPORT_TABLE.Columns.Add("TX_CODE", typeof(string));
                REPORT_TABLE.Columns.Add("ERROR_CODE", typeof(string));
                #endregion

                #region 載入檔案格式資訊
                FileParseByXml xml = new FileParseByXml();

                // REC Layout
                DataTable REC_DataTable = xml.Xml2DataTable(strXML_Layout, "PHONE_CHGIN");

                if (xml.strMSG.Length > 0)
                {
                    logger.strJobQueue = "[Xml2DataTable(PHONE_CHGIN)] - " + xml.strMSG.ToString().Trim();
                    logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                    return "B0099:" + logger.strJobQueue;
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

                    while ((strInfData = srInFile.ReadLine()) != null)
                    {
                        intReadInfDataCount++;

                        #region 檔案格式檢核(筆資料長度)

                        if (strInfData.Length != intDataLength)
                        {
                            logger.strJobQueue = "中華電信異動回覆檔中第" + intReadInfDataCount + "筆的長度有誤，請通知系統人員! 該筆實際長度為 " + strInfData.Length;
                            logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                            //return "B0099:" + logger.strJobQueue;
                            strInfData = strInfData.PadRight(107, ' ');
                        }

                        #endregion

                        DataTable InfData_DataTable = new DataTable();

                        #region 依 Layout 拆解資料
                        InfData_DataTable = xml.FileLine2DataTable(BIG5, strInfData, REC_DataTable);
                        if (xml.strMSG.Length > 0)
                        {
                            logger.strJobQueue = "[FileLine2DataTable(REC)] (L" + intReadInfDataCount + ") - " + xml.strMSG.ToString().Trim();
                            logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                            return "B0099:" + logger.strJobQueue;
                        }
                        #endregion

                        #region 檢核明細資料

                        PAY_TYPE = Convert.ToString(InfData_DataTable.Rows[0]["BATCH_NO"]);
                        OFFICE_CODE = Convert.ToString(InfData_DataTable.Rows[0]["OFFICE_CODE"]);
                        TELNO = Convert.ToString(InfData_DataTable.Rows[0]["TELNO"]);
                        PAY_CARD_NBR = Convert.ToString(InfData_DataTable.Rows[0]["CARD_NBR"]);
                        CHANGE_TYPE = Convert.ToString(InfData_DataTable.Rows[0]["CHANGE_TYPE"]);
                        ERROR_CODE = Convert.ToString(InfData_DataTable.Rows[0]["ERROR_CODE"]);
                        TX_CODE = Convert.ToString(InfData_DataTable.Rows[0]["TX_CODE"]);

                        PAY_NBR = OFFICE_CODE.PadRight(4, ' ') + TELNO.PadLeft(12, ' ');
                        Error_code_descr();//錯誤代碼說明
                        #endregion

                        #region 將約定失敗的資料寫入報表Table & 更新PUBLIC_APPLY

                        #region 更新PUBLIC_APPLY
                        PUBLIC_APPLY.init();
                        PUBLIC_APPLY.strWherePAY_TYPE = PAY_TYPE;
                        PUBLIC_APPLY.strWherePAY_NBR = PAY_NBR;
                        PUBLIC_APPLY.strWherePAY_CARD_NBR = PAY_CARD_NBR;
                        PUBLIC_APPLY.strWhereREPLY_FLAG = "Y";
                        #region 取得 REPLY_DTE
                        //PUBLIC_APPLY_RC = PUBLIC_APPLY.query();
                        PUBLIC_APPLY_RC = PUBLIC_APPLY.query_BRANCH_INF("", "");
                        switch (PUBLIC_APPLY_RC)
                        {
                            case "S0000": //查詢成功   
                                //int intYYY = Convert.ToInt32(Convert.ToString(PUBLIC_APPLY.resultTable.Rows[0]["REPLY_DTE"]).PadRight(8, ' ').Substring(0, 4)) - 1911; //民國年
                                //REPLY_DTE = Convert.ToString(intYYY) + Convert.ToString(PUBLIC_APPLY.resultTable.Rows[0]["REPLY_DTE"]).PadRight(8, ' ').Substring(4, 4);
                                REPLY_DTE = Convert.ToString(PUBLIC_APPLY.resultTable.Rows[0]["REPLY_DTE"]).Substring(0, 4) + "/" +
                                            Convert.ToString(PUBLIC_APPLY.resultTable.Rows[0]["REPLY_DTE"]).Substring(4, 2) + "/" +
                                            Convert.ToString(PUBLIC_APPLY.resultTable.Rows[0]["REPLY_DTE"]).Substring(6, 2);
                                APPLY_DTE = Convert.ToString(PUBLIC_APPLY.resultTable.Rows[0]["APPLY_DTE"]).Substring(0, 4) + "/" +
                                            Convert.ToString(PUBLIC_APPLY.resultTable.Rows[0]["APPLY_DTE"]).Substring(4, 2) + "/" +
                                            Convert.ToString(PUBLIC_APPLY.resultTable.Rows[0]["APPLY_DTE"]).Substring(6, 2);

                                break;
                            case "F0023":
                                REPLY_DTE = "";
                                APPLY_DTE = "";
                                break;
                            default: //資料庫錯誤
                                logger.strJobQueue = "查詢PUBLIC_APPLY.query() 資料錯誤:" + PUBLIC_APPLY_RC;
                                logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                                return "B0016:" + logger.strJobQueue;
                        }
                        #endregion
                        if (ERROR_CODE != "")
                        {
                            PUBLIC_APPLY.strVAILD_FLAG = "N";
                            PUBLIC_APPLY.strEXPIR_DTE = TODAY_PROCESS_DTE.ToString("yyyyMMdd");
                        }
                        PUBLIC_APPLY.strERROR_REASON = ERROR_CODE_DESCR;
                        PUBLIC_APPLY.strERROR_REASON_DT = TODAY_PROCESS_DTE.ToString("yyyyMMdd");
                        PUBLIC_APPLY.strMNT_USER = strJobName;
                        PUBLIC_APPLY.datetimeMNT_DT = TODAY_PROCESS_DTE;
                        PUBLIC_APPLY_RC = PUBLIC_APPLY.update();
                        switch (PUBLIC_APPLY_RC)
                        {
                            case "S0000": //更新成功   
                                PUBLIC_APPLY_Update_Count = PUBLIC_APPLY_Update_Count + PUBLIC_APPLY.intUptCnt;
                                if (PUBLIC_APPLY.intUptCnt == 0)
                                {
                                    logger.strJobQueue = "更新PUBLIC_APPLY 資料錯誤: F0023, PAY_CARD_NBR = " + PAY_CARD_NBR + " PAY_NBR = " + PAY_NBR;
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

                        //寫入報表
                        intFailCount++;
                        insertReport_TABLE();

                        #endregion
                    }
                }
                #endregion
                #region REPORT_TABLE 排序
                if (intReadInfDataCount > 0)
                {
                    REPORT_TABLE.DefaultView.Sort = "APPLY_DTE asc, REPLY_DTE asc";
                    REPORT_TABLE_SORT = REPORT_TABLE.DefaultView.ToTable();
                }
                #endregion
                writeReport(REPORT_TABLE_SORT);

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

        #region 錯誤代碼說明
        void Error_code_descr()
        {
            #region ERROR_CODE
            ERROR_CODE_DESCR = "";
            switch (ERROR_CODE)
            {
                case "01":
                    ERROR_CODE_DESCR = ERROR_CODE + "-機構代號或電話號碼錯誤";
                    break;

                case "02":
                    ERROR_CODE_DESCR = ERROR_CODE + "-異動代號錯誤";
                    break;

                case "03":
                    ERROR_CODE_DESCR = ERROR_CODE + "-繳費方式錯誤";
                    break;

                case "04":
                    ERROR_CODE_DESCR = ERROR_CODE + "-帳單寄送識別錯誤";
                    break;

                case "05":
                    ERROR_CODE_DESCR = ERROR_CODE + "-銀行代號或用戶帳號錯誤（常發生於刪除異動時）";
                    break;

                default:
                    ERROR_CODE_DESCR = ERROR_CODE + "-ERROR_CODE不明";
                    break;
            }
            #endregion
        }
        #endregion

        #region insertReport_TABLE
        void insertReport_TABLE()
        {
            REPORT_TABLE.Rows.Add();
            k = REPORT_TABLE.Rows.Count - 1;
            REPORT_TABLE.Rows[k]["SEQ"] = REPORT_TABLE.Rows.Count.ToString().PadLeft(7, '0');
            REPORT_TABLE.Rows[k]["APPLY_DTE"] = APPLY_DTE;
            REPORT_TABLE.Rows[k]["REPLY_DTE"] = REPLY_DTE;
            REPORT_TABLE.Rows[k]["OFFICE_CODE"] = OFFICE_CODE;
            REPORT_TABLE.Rows[k]["TELNO"] = TELNO;
            REPORT_TABLE.Rows[k]["PAY_CARD_NBR"] = PAY_CARD_NBR;
            REPORT_TABLE.Rows[k]["TX_CODE"] = TX_CODE;
            REPORT_TABLE.Rows[k]["ERROR_CODE"] = ERROR_CODE_DESCR;
            #region CHANGE_TYPE
            switch (CHANGE_TYPE)
            {
                case "1":
                    REPORT_TABLE.Rows[k]["CHANGE_TYPE"] = CHANGE_TYPE + "-新增";
                    break;

                case "2":
                    REPORT_TABLE.Rows[k]["CHANGE_TYPE"] = CHANGE_TYPE + "-更改";
                    break;

                case "3":
                    REPORT_TABLE.Rows[k]["CHANGE_TYPE"] = CHANGE_TYPE + "-刪除";
                    break;

                default:
                    REPORT_TABLE.Rows[k]["CHANGE_TYPE"] = CHANGE_TYPE + "-異動種類不明";
                    break;
            }
            #endregion
        }
        #endregion

        #region 寫出至報表
        void writeReport(DataTable inTable)
        {
            CMCRPT001 CMCRPT001 = new CMCRPT001();

            //設定特殊欄位資料
            ArrayList alSumData = new ArrayList();
            alSumData.Add(new ExcelFactory.ListItem("#RPT_BANK_NAME-", strBANK_NAME + "-"));
            alSumData.Add(new ExcelFactory.ListItem("#RPT_TYPE_NAME", "中華電信"));
            alSumData.Add(new ExcelFactory.ListItem("#RPT_DATE", TODAY_PROCESS_DTE.ToString("yyyy/MM/dd")));
            alSumData.Add(new ExcelFactory.ListItem("#TOTAL_CNT", intFailCount + " 筆"));
            //設定合併欄位資料
            ArrayList alMegData = new ArrayList();

            string strFileName = "PBRPHR001-約定代繳_中華電信_異動失敗報表";

            //產出報表
            CMCRPT001.Output(inTable, alSumData, alMegData, strFileName, "PBRPHR001", "Sheet1", "Sheet1", TODAY_PROCESS_DTE);
        }
        #endregion

        #region Display
        void writeDisplay()
        {
            logger.strJobID = "00002";

            //中華電信約定回應檔筆數(含頭尾)
            logger.strTBL_NAME = "PHONE_CHGIN";
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

