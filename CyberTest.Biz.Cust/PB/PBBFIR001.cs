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
    /// 讀財金格式約定回覆檔更新PUBLIC_APPLY
    /// 
    /// </summary>
    public class PBBFIR001
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
        PUBLIC_APPLYDao PUBLIC_APPLY_NEW = new PUBLIC_APPLYDao();

         //宣告SETUP_PUBLIC
        SETUP_PUBLICDao SETUP_PUBLIC = new SETUP_PUBLICDao();
        DataTable SETUP_PUBLIC_DATATABLE = new DataTable();

        //暫存報表TABLE
        DataTable REPORT_TABLE = new DataTable();
        DataTable REPORT_TABLE_SORT = new DataTable();
        DataTable inSubtotTable = new DataTable();

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
        String TODAY_MMDD = "";
        String ERROR_CODE_DESCR = "";

        //筆數&金額
        int PUBLIC_APPLY_Update_Count = 0;
        int PUBLIC_APPLY_Insert_Count = 0;

        int k = 0; //REPORT_TABLE
        int intFailCount = 0;
        int HeadFileDataCount = 0;
        int DetailFileDataCount = 0;
        int TrailFileDataCount = 0;
        int intReadInfDataCount = 0;

        //約定回覆檔欄位
        //Trail檔欄位
        Decimal FILE_TOT_COUNT = 0;
        Decimal FILE_SUCC_COUNT = 0;
        Decimal FILE_UNSUCC_COUNT = 0;

        //Detail檔欄位
        String REPLY_DTE = "";
        String TRANS_DTE = "";
        String PAY_ACCT_NBR = "";
        String PAY_NBR = "";
        String CHANGE_FLAG = "";
        String CHANGE_DTE = "";
        String ERROR_CODE = "";
        String OLD_PAY_NBR = "";
        String NEW_PAY_NBR = "";
        String ORIG_REPLY_DTE = "";
        String APPLY_DTE = "";
        String strFileName = "";
        String CARD_NBR = "";

        //檔案長度
        const int intDataLength = 180;
        string strJobName = "PBBFIR001";

        String strRecNAME = "PUBLIC_FISC_IN";
        String strFILE_NAME = "";
        String strFILE_PATH_FULL = "";
        String TODAY_CH_DTE = "";
        String strPay_Type = "";
        String strDescr = "";
        String strBANK_NAME = "";
        String strxml_NODE = "";
        String strSheet = "";
        //String strSheet_name = "";

        DataTable FH_xml_DataTable = new DataTable();
        DataTable FD_xml_DataTable = new DataTable();
        DataTable FT_xml_DataTable = new DataTable();
        DataTable FH_DataTable = new DataTable();
        DataTable FD_DataTable = new DataTable();
        DataTable FT_DataTable = new DataTable();
        DataTable CHANGE_AREA_DataTable = new DataTable();

        DataTable InhData_DataTable = new DataTable();
        DataTable IntData_DataTable = new DataTable();
        DataTable InData_DataTable_all = new DataTable();
        #endregion

        #region 宣告檔案路徑

        //XML放置路徑 
        String strXML_Layout = "";
        //寫出檔案路徑
        String FILE_PATH = "";

        #endregion

        static readonly Cybersoft.Coca.Log.Logger logger = Cybersoft.Log.LogManager<Cybersoft.Coca.Log.Logger>.GetLogger(System.Reflection.MethodInfo.GetCurrentMethod().ReflectedType.Name);
        //========================================================================================================
        #region 程式主邏輯【MAIN Routine】
        public string RUN(string strTRANS_TYPE)
        {
            #region 準備MSGLG.SYSOUT CLASS
            logger.strJobID = JobID;
            logger.strJOBNAME = "PBBFIR001";
            logger.dtRunDate = DateTime.Now;
            #endregion

            try
            {
                #region 取得系統資訊(前次批次日、本日批次日)
                CMCSYS001 SYSINF = new CMCSYS001();
                SYSINF.strJobID = JobID;
                SYSINF.strJobName = "PBBFIR001";
                String SYSINF_RC = SYSINF.getSYSINF();
                PREV_PROCESS_DTE = SYSINF.datetimePREV_PROCESS_DTE;
                TODAY_PROCESS_DTE = SYSINF.datetimeTODAY_PROCESS_DTE;
                NEXT_PROCESS_DTE = SYSINF.datetimeNEXT_PROCESS_DTE;
                TODAY_CH_DTE = (Convert.ToString(Convert.ToInt64(TODAY_PROCESS_DTE.ToString("yyyy")) - 1911) + TODAY_PROCESS_DTE.ToString("MMdd")).PadLeft(7, '0');
                TODAY_YYYYMMDD = TODAY_PROCESS_DTE.ToString("yyyyMMdd");
                TODAY_MMDD = TODAY_PROCESS_DTE.ToString("MMdd");
                strBANK_NAME = SYSINF.strREPORT_TITLE;
                #endregion
                
                #region 讀取公共事業參數
                SETUP_PUBLIC.init();

                SETUP_PUBLIC.strWhereFILE_TRANSFER_UNIT = "FISC";
                SETUP_PUBLIC.strPOST_RESULT = "00";
                SETUP_PUBLIC.strWhereFILE_TRANSFER_TYPE = strTRANS_TYPE.Substring(0, 5);
                string SETUP_PUBLIC_RC = SETUP_PUBLIC.query();

                switch (SETUP_PUBLIC_RC)
                {
                    case "S0000": //查詢成功
                        strPay_Type = SETUP_PUBLIC.resultTable.Rows[0]["PAY_TYPE"].ToString().Trim();
                        strDescr = SETUP_PUBLIC.resultTable.Rows[0]["DESCR"].ToString().Trim();
                        //排序
                        break;

                    case "F0023": //無需處理資料
                        logger.strJobQueue = "公用事業參數檔中無查無此設定的公共事業單位" + strTRANS_TYPE.Substring(0, 5) + "，不執行，請系統負責人確認。";
                        logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                        return "B0016:" + logger.strJobQueue;

                    default: //資料庫錯誤
                        logger.strJobQueue = "查詢SETUP_PUBLIC 資料錯誤:" + PUBLIC_APPLY_RC;
                        logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                        return "B0016:" + logger.strJobQueue;
                }

                #endregion

                switch (strTRANS_TYPE)
                {
                    case "011202":
                        strxml_NODE = "CHANGE_AREA_011202"; //市水帳號異動失敗
                        DEFINE_REPORT_Sheet1();
                        strSheet = "Sheet1";
                        //strSheet_name = "異動失敗";
                        strFileName = "PBRFIR001-約定代繳_"+ strDescr +"_異動失敗" ;
                        break;
                    case "011203":
                        strxml_NODE = "CHANGE_AREA_011203"; //市水水號異動(新舊號變更)
                        DEFINE_REPORT_Sheet2();
                        strSheet = "Sheet2";
                        //strSheet_name = "異動";
                        strFileName = "PBRFIR001-約定代繳_" + strDescr +"_異動";
                        break;
                    case "011204":
                        strxml_NODE = "CHANGE_AREA_011204";//市水取消帳號異動
                        DEFINE_REPORT_Sheet1();
                        strSheet = "Sheet1";
                        //strSheet_name = "取消";
                        strFileName = "PBRFIR001-約定代繳_"+ strDescr +"_取消";
                        break;
                    case "010102":
                        strxml_NODE = "CHANGE_AREA_010102";  //省水水號異動(新舊號變更)
                        DEFINE_REPORT_Sheet2();
                        strSheet = "Sheet2";
                        //strSheet_name = "異動";
                        strFileName = "PBRFIR001-約定代繳_" + strDescr +"_異動";
                        break;
                }
                #region 宣告檔案路徑
                //XML名稱
                strXML_Layout = CMCURL.getURL(strRecNAME);
                //寫出檔案路徑
                FILE_PATH = CMCURL.getPATH(strRecNAME);
                if (strXML_Layout == "" || FILE_PATH == "")
                {
                    logger.strJobQueue = "路徑取得錯誤!!! PUBLIC_FISC.xml路徑為 " + strXML_Layout + ",PUBLIC_FISC的FILE_PATH路徑為 " + FILE_PATH;
                    logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                    return "B0099:" + logger.strJobQueue;
                }
                else
                {
                    strFILE_NAME = strTRANS_TYPE + TODAY_CH_DTE;
                    strFILE_PATH_FULL = FILE_PATH + "\\" + strFILE_NAME;
                    logger.strJobQueue = "PUBLIC_FISC.xml路徑為 " + strXML_Layout + ",PUBLIC_FISC的FILE_PATH路徑為 " + strFILE_PATH_FULL;
                    logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                }
                if (!File.Exists(strFILE_PATH_FULL))
                {
                    logger.strJobQueue = "今日無約定回覆檔需要進行更新!!檔名：" + strFILE_NAME;
                    logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                    writeReport(REPORT_TABLE);
                    return "B0000：" + logger.strJobQueue;
                }
                #endregion

                #region (Rerun) 1.換號-刪除PUBLIC_APPLY 今日新增資料.復原換號註記(VAILD_FLAG:C→Y)
                if ((strTRANS_TYPE == "011203") || (strTRANS_TYPE == "010102"))
                {
                    #region 刪除 PUBLIC_APPLY 今日新增資料	
                    PUBLIC_APPLY.table_define();
                    PUBLIC_APPLY.init();
                    PUBLIC_APPLY.resultTable.Clear();
                    //條件
                    PUBLIC_APPLY.strWherePAY_TYPE = strPay_Type;
                    PUBLIC_APPLY.strWhereAPPLY_DTE = TODAY_YYYYMMDD;
                    PUBLIC_APPLY.strWhereREPLY_DTE = TODAY_YYYYMMDD;
                    PUBLIC_APPLY.strWhereERROR_REASON = TODAY_YYYYMMDD;
                    PUBLIC_APPLY.strWhereVAILD_FLAG = "Y";
                    PUBLIC_APPLY.strWhereMNT_USER = strJobName;
                    PUBLIC_APPLY.DateTimeWhereMNT_DT = TODAY_PROCESS_DTE;
                    PUBLIC_APPLY_RC = PUBLIC_APPLY.delete();
                    switch (PUBLIC_APPLY_RC.Substring(0, 5))
                    {
                        case "S0000":  // 刪除成功
                            break;
                        default:       // 資料庫錯誤
                            logger.strJobQueue = "[ReRun1-PUBLIC_APPLY.delete()]：" + PUBLIC_APPLY_RC;
                            logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                            return "B0016:" + logger.strJobQueue;
                    }
                    logger.strJobQueue = "刪除 PUBLIC_APPLY 當日換號新增資料共 " + PUBLIC_APPLY.intDelCnt + " 筆。";
                    logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                    #endregion
                    #region 復原換號註記
                    PUBLIC_APPLY_RC = string.Empty;
                    //條件
                    PUBLIC_APPLY.strWherePAY_TYPE = strPay_Type;
                    PUBLIC_APPLY.strWhereVAILD_FLAG = "C";
                    PUBLIC_APPLY.strWhereEXPIR_DTE = TODAY_PROCESS_DTE.ToString("yyyyMMdd");
                    PUBLIC_APPLY.strWhereMNT_USER = strJobName;
                    PUBLIC_APPLY.DateTimeWhereMNT_DT = TODAY_PROCESS_DTE;
                    //更新內容
                    PUBLIC_APPLY.strERROR_REASON = "";
                    PUBLIC_APPLY.strERROR_REASON_DT = "";
                    PUBLIC_APPLY.strVAILD_FLAG = "Y";
                    PUBLIC_APPLY.strEXPIR_DTE = "29991231";
                    PUBLIC_APPLY_RC = PUBLIC_APPLY.update();
                    switch (PUBLIC_APPLY_RC.Substring(0, 5))
                    {
                        case "S0000":  // 成功
                            break;
                        default:       // 資料庫錯誤
                            logger.strJobQueue = "[ReRun2-PUBLIC_APPLY.update()]：" + PUBLIC_APPLY_RC;
                            logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                            return "B0016:" + logger.strJobQueue;
                    }
                    logger.strJobQueue = "復原 PUBLIC_APPLY 當日換號資料共 " + PUBLIC_APPLY.intUptCnt + " 筆。";
                    logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                    #endregion
                }
                #endregion
                #region (Rerun) 2.異動取消-復原PUBLIC_APPLY換號註記(VAILD_FLAG:N→Y)
                if ((strTRANS_TYPE == "011202") || (strTRANS_TYPE == "011204"))
                {
                    PUBLIC_APPLY.init();
                    //條件
                    PUBLIC_APPLY.strWherePAY_TYPE = strPay_Type;
                    PUBLIC_APPLY.strWhereREPLY_FLAG = "Y";
                    PUBLIC_APPLY.strWhereVAILD_FLAG = "N";
                    PUBLIC_APPLY.strWhereEXPIR_DTE = TODAY_YYYYMMDD;
                    PUBLIC_APPLY.strWhereMNT_USER = strJobName;
                    //PUBLIC_APPLY.datetimeWhereMNT_DT = TODAY_PROCESS_DTE;
                    //更新內容
                    PUBLIC_APPLY.strVAILD_FLAG = "Y";
                    PUBLIC_APPLY.strEXPIR_DTE = "29991231";
                    PUBLIC_APPLY.strERROR_REASON = "";
                    PUBLIC_APPLY.strERROR_REASON_DT = "";
                    //執行
                    PUBLIC_APPLY_RC = PUBLIC_APPLY.update();
                    switch (PUBLIC_APPLY_RC)
                    {
                        case "S0000":  // 成功
                            break;
                        default:       // 資料庫錯誤
                            logger.strJobQueue = "[ReRun3-PUBLIC_APPLY.update()]：" + PUBLIC_APPLY_RC;
                            logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                            return "B0016:" + logger.strJobQueue;
                    }
                    logger.strJobQueue = "復原 PUBLIC_APPLY 當日換號資料共 " + PUBLIC_APPLY.intUptCnt + " 筆。";
                    logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                }
                #endregion
                
                #region 載入檔案格式資訊
                FileParseByXml xml = new FileParseByXml();
                //HADER
                #region BATCHTX06(FH) Layout (轉入BATCHTX06.xml的FH格式)
                FH_xml_DataTable = xml.Xml2DataTable(strXML_Layout, "BATCHTX06_H");
                if (xml.strMSG.Length > 0)
                {
                    logger.strJobQueue = "[Xml2DataTable(" + strRecNAME + "_FH)] - " + xml.strMSG.ToString().Trim();
                    logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                    return "B0099:" + logger.strJobQueue;
                }

                #endregion
                //BODY
                #region BATCHTX06(FD) Layout (轉入BATCHTX06.xml的FD格式)
                FD_xml_DataTable = xml.Xml2DataTable(strXML_Layout, "BATCHTX06");
                if (xml.strMSG.Length > 0)
                {
                    logger.strJobQueue = "[Xml2DataTable(" + strRecNAME + "_FD)] - " + xml.strMSG.ToString().Trim();
                    logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                    return "B0099:" + logger.strJobQueue;
                }
                #endregion
                //TRAILER
                #region BATCHTX06(FT) Layout (轉入BATCHTX06.xml的FT格式)
                FT_xml_DataTable = xml.Xml2DataTable(strXML_Layout, "BATCHTX06_T");
                if (xml.strMSG.Length > 0)
                {
                    logger.strJobQueue = "[Xml2DataTable(" + strRecNAME + "_FT)] - " + xml.strMSG.ToString().Trim();
                    logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                    return "B0099:" + logger.strJobQueue;
                }
                #endregion
                //CHANGE_AREA異動資料區
                #region CHANGE_AREA異動資料區

                CHANGE_AREA_DataTable = xml.Xml2DataTable(strXML_Layout, strxml_NODE);

                if (xml.strMSG.Length > 0)
                {
                    logger.strJobQueue = "[Xml2DataTable(" + strxml_NODE + ")] - " + xml.strMSG.ToString().Trim();
                    logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                    return "B0099:" + logger.strJobQueue;
                }
                #endregion
                #endregion

                #region 設定檔案編碼
                Encoding BIG5 = Encoding.GetEncoding("big5");
                #endregion
                
                #region 讀取資料
                using (StreamReader srInFile = new StreamReader(strFILE_PATH_FULL, BIG5))
                {
                    string strInData;

                    while ((strInData = srInFile.ReadLine()) != null)
                    {
                        DataTable InData_DataTable = new DataTable();

                        #region 檔案格式檢核(筆資料長度)
                        if (strInData.Length != intDataLength)
                        {
                            logger.strJobQueue = "約定回覆檔中第" + HeadFileDataCount + "筆的長度不為" + intDataLength + "，請通知系統人員! 該筆實際長度為 " + strInData.Length;
                            logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                            return "B0099:" + logger.strJobQueue;
                        }
                        #endregion

                        #region Header
                        if (strInData.Substring(0, 1) == "1")
                        {
                            HeadFileDataCount++;
                            #region 依 Layout 拆解資料
                            InhData_DataTable = xml.FileLine2DataTable(BIG5, strInData, FH_xml_DataTable);

                            if (xml.strMSG.Length > 0)
                            {
                                logger.strJobQueue = "[FileLine2DataTable(REC)] (L" + intReadInfDataCount + ") - " + xml.strMSG.ToString().Trim();
                                logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                                return "B0099:" + logger.strJobQueue;
                            }
                            #endregion
                            if (InhData_DataTable.Rows[0]["TRANSFER_TYPE_H"].ToString() != strTRANS_TYPE)
                            {
                                logger.strJobQueue = "約定回覆檔Header異動類別：" + InhData_DataTable.Rows[0]["TRANSFER_TYPE_H"] + "與檔名不一致" + strTRANS_TYPE + "，請通知系統人員! ";
                                logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                                return "B0099:" + logger.strJobQueue;
                            }
                            if (InhData_DataTable.Rows[0]["TRANSFER_DTE_H"].ToString() != TODAY_CH_DTE)
                            {
                                logger.strJobQueue = "約定回覆檔Header，傳輸日期：" + InhData_DataTable.Rows[0]["TRANSFER_DTE_H"] + "非今日" + TODAY_CH_DTE + "，請通知系統人員! ";
                                logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                                return "B0099:" + logger.strJobQueue;
                            }
                           
                        }
                        #endregion

                        #region Body
                        else if (strInData.Substring(0, 1) == "2")
                        {
                            DetailFileDataCount++;
                            #region 依 Layout 拆解資料
                            InData_DataTable = xml.FileLine2DataTable(BIG5, strInData, FD_xml_DataTable);

                            if (xml.strMSG.Length > 0)
                            {
                                logger.strJobQueue = "[FileLine2DataTable(REC)] (L" + intReadInfDataCount + ") - " + xml.strMSG.ToString().Trim();
                                logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                                return "B0099:" + logger.strJobQueue;
                            }
                            #endregion
                            if (InData_DataTable.Rows[0]["TRANSFER_TYPE"].ToString() != strTRANS_TYPE)
                            {
                                logger.strJobQueue = "約定回覆檔Body異動類別：" + InData_DataTable.Rows[0]["TRANSFER_TYPE"] + "與檔名不一致" + strTRANS_TYPE + "，請通知系統人員! ";
                                logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                                return "B0099:" + logger.strJobQueue;
                            }
                            if (InData_DataTable.Rows[0]["TRANSFER_DTE"].ToString() != TODAY_CH_DTE)
                            {
                                logger.strJobQueue = "約定回覆檔Body，傳輸日期：" + InData_DataTable.Rows[0]["TRANSFER_DTE"] + "非今日" + TODAY_CH_DTE + "，請通知系統人員! ";
                                logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                                return "B0099:" + logger.strJobQueue;
                            }
                            #region 搬明細資料
                            if (DetailFileDataCount == 1)
                            {
                                InData_DataTable_all = InData_DataTable.Clone();
                            }
                            InData_DataTable_all.ImportRow(InData_DataTable.Rows[0]);

                            #endregion
                        }
                        #endregion

                        #region Trail
                        else if (strInData.Substring(0, 1) == "3")
                        {
                            TrailFileDataCount++;
                            #region 依 Layout 拆解資料
                            IntData_DataTable = xml.FileLine2DataTable(BIG5, strInData, FT_xml_DataTable);

                            if (xml.strMSG.Length > 0)
                            {
                                logger.strJobQueue = "[FileLine2DataTable(REC)] (L" + intReadInfDataCount + ") - " + xml.strMSG.ToString().Trim();
                                logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                                return "B0099:" + logger.strJobQueue;
                            }
                            #endregion
                            if (IntData_DataTable.Rows[0]["TRANSFER_TYPE_T"].ToString() != strTRANS_TYPE)
                            {
                                logger.strJobQueue = "約定回覆檔Trail異動類別：" + IntData_DataTable.Rows[0]["TRANSFER_TYPE_T"] + "與檔名不一致" + strTRANS_TYPE + "，請通知系統人員! ";
                                logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                                return "B0099:" + logger.strJobQueue;
                            }
                            if (IntData_DataTable.Rows[0]["TRANSFER_DTE_T"].ToString() != TODAY_CH_DTE)
                            {
                                logger.strJobQueue = "約定回覆檔Trail，傳輸日期：" + IntData_DataTable.Rows[0]["TRANSFER_DTE_T"] + "非今日" + TODAY_CH_DTE + "，請通知系統人員! ";
                                logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                                return "B0099:" + logger.strJobQueue;
                            }
                            FILE_TOT_COUNT = Convert.ToDecimal(IntData_DataTable.Rows[0]["TOT_CNT"]);
                            FILE_SUCC_COUNT = Convert.ToDecimal(IntData_DataTable.Rows[0]["SUCC_CNT"]);
                            FILE_UNSUCC_COUNT = Convert.ToDecimal(IntData_DataTable.Rows[0]["UNSUCC_CNT"]);
                        }
                        #endregion

                    }
                }
                #endregion

                #region 檢核筆數
                if (HeadFileDataCount > 1)
                {
                    logger.strJobQueue = "約定回覆檔Head檔筆數 > 1筆，請通知系統人員!";
                    logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                    return "B0099:" + logger.strJobQueue;
                }
                if (TrailFileDataCount > 1)
                {
                    logger.strJobQueue = "約定回覆檔Trail檔筆數 > 1筆，請通知系統人員!";
                    logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                    return "B0099:" + logger.strJobQueue;
                }
                // 比對Head檔和明細檔資料筆數
                if (FILE_TOT_COUNT != DetailFileDataCount)
                {
                    logger.strJobQueue = "約定回覆Trail檔資料筆數：" + FILE_TOT_COUNT + "與實際明細資料筆數：" + DetailFileDataCount + "不相等，請通知系統人員! ";
                    logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                    return "B0099:" + logger.strJobQueue;
                }
                #endregion

                #region 將明細資料轉進DATA_TABLE

                     for (int k = 0; k < InData_DataTable_all.Rows.Count; k++)
                     {
                         //台北市水以外的來源資料PAY_ACCT_NBR前2碼帶"00"
                         if (strTRANS_TYPE.Substring(0, 5) != "01120")
                         {
                             string temp = Convert.ToString(InData_DataTable_all.Rows[k]["PAY_ACCT_NBR"]);
                             PAY_ACCT_NBR = temp.Substring(2, temp.Length - 2);
                         }
                         else
                         {
                             PAY_ACCT_NBR = Convert.ToString(InData_DataTable_all.Rows[k]["PAY_ACCT_NBR"]);
                         }                         
                         TRANS_DTE = Convert.ToString(InData_DataTable_all.Rows[k]["TRANSFER_DTE"]);
                         //CHANGE_DTE = Convert.ToString(InData_DataTable_all.Rows[k]["CHANGE_DTE"]);
                         int intYYYY = Convert.ToInt32(Convert.ToString(InData_DataTable_all.Rows[k]["CHANGE_DTE"]).PadLeft(8, '0').Substring(0, 4)) + 1911; //民國年
                         CHANGE_DTE = Convert.ToString(intYYYY) + "/"+
                                      Convert.ToString(InData_DataTable_all.Rows[k]["CHANGE_DTE"]).PadLeft(8, '0').Substring(4, 2) + "/" +
                                      Convert.ToString(InData_DataTable_all.Rows[k]["CHANGE_DTE"]).PadLeft(8, '0').Substring(6, 2);
                         CHANGE_FLAG = Convert.ToString(InData_DataTable_all.Rows[k]["CHANGE_FLAG"]);
                         #region 依 Layout 拆解資料
                         string strChange_aera = InData_DataTable_all.Rows[k]["CHANGE_AREA"].ToString().PadRight(71,' ');
                         DataTable CHANGE_AREAData_DataTable = xml.FileLine2DataTable(BIG5, strChange_aera, CHANGE_AREA_DataTable);
                         if (xml.strMSG.Length > 0)
                         {
                             logger.strJobQueue = "[FileLine2DataTable(CHANGE_AREA)] (L" + k + ") - " + xml.strMSG.ToString().Trim();
                             logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                             throw new System.Exception("B0099:" + logger.strJobQueue);
                         }
                         #endregion
                         switch (strTRANS_TYPE)
                         {
                             case "010102":      //省水異動
                                 OLD_PAY_NBR = Convert.ToString(CHANGE_AREAData_DataTable.Rows[0]["OLD_PAY_NBR"]);
                                 NEW_PAY_NBR = Convert.ToString(CHANGE_AREAData_DataTable.Rows[0]["NEW_PAY_NBR"]);
                                 ERROR_CODE = "";
                                 Change_PAY_NBR();
                                 break;

                             case "011202":      //市水異動失敗
                                 PAY_NBR = Convert.ToString(CHANGE_AREAData_DataTable.Rows[0]["PAY_NBR"]);
                                 ORIG_REPLY_DTE = Convert.ToString(CHANGE_AREAData_DataTable.Rows[0]["ORIG_REPLY_DTE"]);
                                 ERROR_CODE = Convert.ToString(CHANGE_AREAData_DataTable.Rows[0]["ERROR_CODE"]);
                                 Error_code_descr();
                                 update_PUBLIC_APPLY(strTRANS_TYPE);
                                 break;

                             case "011203":      //市水換號
                                 OLD_PAY_NBR = Convert.ToString(CHANGE_AREAData_DataTable.Rows[0]["OLD_PAY_NBR"]);
                                 NEW_PAY_NBR = Convert.ToString(CHANGE_AREAData_DataTable.Rows[0]["NEW_PAY_NBR"]);
                                 ERROR_CODE = "";
                                 Change_PAY_NBR();
                                 break;

                             case "011204":      //市水取消
                                 PAY_NBR = Convert.ToString(CHANGE_AREAData_DataTable.Rows[0]["PAY_NBR"]);
                                 ERROR_CODE = Convert.ToString(CHANGE_AREAData_DataTable.Rows[0]["ERROR_CODE"]);
                                 Error_code_descr();
                                 update_PUBLIC_APPLY(strTRANS_TYPE);
                                 break;

                        }

                        intFailCount++;
                    }
                #endregion
                #region 寫至報表
                writeReport(REPORT_TABLE);
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
       
        #region 定義報表Table Sheet1
        void DEFINE_REPORT_Sheet1()
        {
            
            REPORT_TABLE.Columns.Add("SEQ", typeof(string));
            REPORT_TABLE.Columns.Add("APPLY_DTE", typeof(string));
            REPORT_TABLE.Columns.Add("REPLY_DTE", typeof(string));
            REPORT_TABLE.Columns.Add("PAY_NBR", typeof(string));
            REPORT_TABLE.Columns.Add("PAY_ACCT_NBR", typeof(string));
            REPORT_TABLE.Columns.Add("CARD_NBR", typeof(string));
            REPORT_TABLE.Columns.Add("CHANGE_REASON", typeof(string));
            REPORT_TABLE.Columns.Add("CHANGE_DTE", typeof(string));
            REPORT_TABLE.Columns.Add("ERROR_CODE", typeof(string));
            
        }
        #endregion

        #region insertReport_TABLE_Sheet1
        void insertReport_TABLE_Sheet1()
        {
            REPORT_TABLE.Rows.Add();
            k = REPORT_TABLE.Rows.Count - 1;
            REPORT_TABLE.Rows[k]["SEQ"] = REPORT_TABLE.Rows.Count.ToString().PadLeft(7, '0');
            REPORT_TABLE.Rows[k]["APPLY_DTE"] = APPLY_DTE;
            REPORT_TABLE.Rows[k]["REPLY_DTE"] = REPLY_DTE;
            REPORT_TABLE.Rows[k]["PAY_NBR"] = PAY_NBR;
            REPORT_TABLE.Rows[k]["PAY_ACCT_NBR"] = PAY_ACCT_NBR;
            REPORT_TABLE.Rows[k]["CARD_NBR"] = CARD_NBR;
            REPORT_TABLE.Rows[k]["CHANGE_DTE"] = CHANGE_DTE;
            REPORT_TABLE.Rows[k]["ERROR_CODE"] = ERROR_CODE_DESCR;

            #region CHANGE_FLAG
            switch (CHANGE_FLAG)
            {
                case "1":
                    REPORT_TABLE.Rows[k]["CHANGE_REASON"] = CHANGE_FLAG + "-新增";
                    break;

                case "2":
                    REPORT_TABLE.Rows[k]["CHANGE_REASON"] = CHANGE_FLAG + "-終止";
                    break;

                case "3":
                    REPORT_TABLE.Rows[k]["CHANGE_REASON"] = CHANGE_FLAG + "-修改";
                    break;

                default:
                    REPORT_TABLE.Rows[k]["CHANGE_REASON"] = CHANGE_FLAG + "-異動種類不明";
                    break;
            }
            #endregion
        }
        #endregion

        #region 定義報表Table Sheet2
        void DEFINE_REPORT_Sheet2()
        {
            REPORT_TABLE.Columns.Add("SEQ", typeof(string));
            REPORT_TABLE.Columns.Add("APPLY_DTE", typeof(string));
            REPORT_TABLE.Columns.Add("REPLY_DTE", typeof(string));
            REPORT_TABLE.Columns.Add("OLD_PAY_NBR", typeof(string));
            REPORT_TABLE.Columns.Add("NEW_PAY_NBR", typeof(string));
            REPORT_TABLE.Columns.Add("PAY_ACCT_NBR", typeof(string));
            REPORT_TABLE.Columns.Add("CARD_NBR", typeof(string));
            REPORT_TABLE.Columns.Add("CHANGE_DTE", typeof(string));
        }
        #endregion

        #region insertReport_TABLE_Sheet2
        void insertReport_TABLE_Sheet2()
        {
            REPORT_TABLE.Rows.Add();
            k = REPORT_TABLE.Rows.Count - 1;
            REPORT_TABLE.Rows[k]["SEQ"] = REPORT_TABLE.Rows.Count.ToString().PadLeft(7, '0');
            REPORT_TABLE.Rows[k]["APPLY_DTE"] = APPLY_DTE;
            REPORT_TABLE.Rows[k]["REPLY_DTE"] = REPLY_DTE;
            REPORT_TABLE.Rows[k]["OLD_PAY_NBR"] = OLD_PAY_NBR;
            REPORT_TABLE.Rows[k]["NEW_PAY_NBR"] = NEW_PAY_NBR;
            REPORT_TABLE.Rows[k]["PAY_ACCT_NBR"] = PAY_ACCT_NBR;
            REPORT_TABLE.Rows[k]["CARD_NBR"] = CARD_NBR;
            REPORT_TABLE.Rows[k]["CHANGE_DTE"] = CHANGE_DTE;
        }
        #endregion

        #region 錯誤代碼說明
        void Error_code_descr()
        {
            #region ERROR_CODE
            ERROR_CODE_DESCR = "";
            switch (ERROR_CODE)
            {
                case "01":
                    ERROR_CODE_DESCR = ERROR_CODE + "-水號錯誤";
                    break;
                case "1":
                    ERROR_CODE_DESCR = ERROR_CODE + "-用戶申辦變更為其他代繳單位帳號(北市水處)";
                    break;
                case "2":
                    ERROR_CODE_DESCR = ERROR_CODE + "-二次扣款失敗(北市水處)";
                    break;
                case "3":
                    ERROR_CODE_DESCR = ERROR_CODE + "-用戶至水處申辦取消帳號(北市水處)";
                    break;
                default:
                    ERROR_CODE_DESCR = ERROR_CODE + "-ERROR_CODE不明";
                    break;
            }
            #endregion
        }
        #endregion

        #region 更新 PUBLIC_APPLY資訊
        void update_PUBLIC_APPLY(string strTRANS_TYPE)
        {
            #region 更新PUBLIC_APPLY
            PUBLIC_APPLY.init();
            PUBLIC_APPLY.strWherePAY_TYPE = strPay_Type;
            PUBLIC_APPLY.strWherePAY_NBR = PAY_NBR;                 
            PUBLIC_APPLY.strWherePAY_ACCT_NBR = PAY_ACCT_NBR;
            PUBLIC_APPLY.strWhereREPLY_FLAG = "Y";
            if (strTRANS_TYPE == "011202")
            {
                PUBLIC_APPLY.strWhereREPLY_DTE = (Convert.ToDecimal(ORIG_REPLY_DTE) + 19110000).ToString();
            }
            #region 取得 REPLY_DTE、APPLY_DTE
            PUBLIC_APPLY_RC = PUBLIC_APPLY.query_BRANCH_INF(PAY_ACCT_NBR, "PAY_ACCT_NBR");
            switch (PUBLIC_APPLY_RC)
            {
                case "S0000": //查詢成功 
                    //int intYYY = Convert.ToInt32(Convert.ToString(PUBLIC_APPLY.resultTable.Rows[0]["REPLY_DTE"]).PadRight(8, ' ').Substring(0, 4)) - 1911; //民國年
                    //REPLY_DTE = Convert.ToString(intYYY) + Convert.ToString(PUBLIC_APPLY.resultTable.Rows[0]["REPLY_DTE"]).PadRight(8, ' ').Substring(4, 4);

                    //intYYY = Convert.ToInt32(Convert.ToString(PUBLIC_APPLY.resultTable.Rows[0]["APPLY_DTE"]).PadRight(8, ' ').Substring(0, 4)) - 1911; //民國年
                    //APPLY_DTE = Convert.ToString(intYYY) + Convert.ToString(PUBLIC_APPLY.resultTable.Rows[0]["APPLY_DTE"]).PadRight(8, ' ').Substring(4, 4);
                    REPLY_DTE = Convert.ToString(PUBLIC_APPLY.resultTable.Rows[0]["REPLY_DTE"]).Substring(0,4) +"/" +
                                Convert.ToString(PUBLIC_APPLY.resultTable.Rows[0]["REPLY_DTE"]).Substring(4,2) +"/" +
                                Convert.ToString(PUBLIC_APPLY.resultTable.Rows[0]["REPLY_DTE"]).Substring(6,2);
                    APPLY_DTE = Convert.ToString(PUBLIC_APPLY.resultTable.Rows[0]["APPLY_DTE"]).Substring(0, 4) + "/" +
                                Convert.ToString(PUBLIC_APPLY.resultTable.Rows[0]["APPLY_DTE"]).Substring(4, 2) + "/" +
                                Convert.ToString(PUBLIC_APPLY.resultTable.Rows[0]["APPLY_DTE"]).Substring(6, 2);
                    CARD_NBR = Convert.ToString(PUBLIC_APPLY.resultTable.Rows[0]["PAY_CARD_NBR"]);
                    break;
                case "F0023":
                    REPLY_DTE = "";
                    APPLY_DTE = "";
                    CARD_NBR  = "";
                    break;
                default: //資料庫錯誤
                    logger.strJobQueue = "查詢PUBLIC_APPLY.query_BRANCH_INF() 資料錯誤:" + PUBLIC_APPLY_RC;
                    logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                    throw new System.Exception("B0016：" + logger.strJobQueue);
            }
            #endregion
            PUBLIC_APPLY.strVAILD_FLAG = "N";
            PUBLIC_APPLY.strEXPIR_DTE = TODAY_YYYYMMDD;
            PUBLIC_APPLY.strERROR_REASON = ERROR_CODE_DESCR;
            PUBLIC_APPLY.strERROR_REASON_DT = TODAY_YYYYMMDD;
            PUBLIC_APPLY.strMNT_USER = strJobName;
            PUBLIC_APPLY.datetimeMNT_DT = TODAY_PROCESS_DTE;
            PUBLIC_APPLY_RC = PUBLIC_APPLY.update();
            switch (PUBLIC_APPLY_RC)
            {
                case "S0000": //更新成功   
                    PUBLIC_APPLY_Update_Count = PUBLIC_APPLY_Update_Count + PUBLIC_APPLY.intUptCnt;
                    if (PUBLIC_APPLY.intUptCnt == 0)
                    {
                        logger.strJobQueue = "更新PUBLIC_APPLY 資料錯誤: F0023, PAY_ACCT_NBR = " + PAY_ACCT_NBR + " PAY_NBR = " + PAY_NBR;
                        logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                        throw new System.Exception("F0023：" + logger.strJobQueue);
                    }
                    break;

                default: //資料庫錯誤
                    logger.strJobQueue = "更新PUBLIC_APPLY 資料錯誤:" + PUBLIC_APPLY_RC;
                    logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                    throw new System.Exception("B0016：" + logger.strJobQueue);
            }
            #endregion

            //寫出報表
            insertReport_TABLE_Sheet1();
        }
        #endregion

        #region 變更 PUBLIC_APPLY PAY_NBR資訊
        void Change_PAY_NBR()
        {
            #region 終止舊的PAY_NBR 
            PUBLIC_APPLY.init();
            PUBLIC_APPLY.strWherePAY_TYPE = strPay_Type;
            PUBLIC_APPLY.strWherePAY_NBR = OLD_PAY_NBR;
            PUBLIC_APPLY.strWherePAY_ACCT_NBR = PAY_ACCT_NBR;
            PUBLIC_APPLY.strWhereREPLY_FLAG = "Y";
            #region 取得 REPLY_DTE、APPLY_DTE
            PUBLIC_APPLY_RC = PUBLIC_APPLY.query_BRANCH_INF(PAY_ACCT_NBR, "PAY_ACCT_NBR");
            switch (PUBLIC_APPLY_RC)
            {
                case "S0000": //查詢成功 
                    //int intYYY = Convert.ToInt32(Convert.ToString(PUBLIC_APPLY.resultTable.Rows[0]["REPLY_DTE"]).PadRight(8, ' ').Substring(0, 4)) - 1911; //民國年
                    //REPLY_DTE = Convert.ToString(intYYY) + Convert.ToString(PUBLIC_APPLY.resultTable.Rows[0]["REPLY_DTE"]).PadRight(8, ' ').Substring(4, 4);

                    //intYYY = Convert.ToInt32(Convert.ToString(PUBLIC_APPLY.resultTable.Rows[0]["APPLY_DTE"]).PadRight(8, ' ').Substring(0, 4)) - 1911; //民國年
                    //APPLY_DTE = Convert.ToString(intYYY) + Convert.ToString(PUBLIC_APPLY.resultTable.Rows[0]["APPLY_DTE"]).PadRight(8, ' ').Substring(4, 4);

                    REPLY_DTE = Convert.ToString(PUBLIC_APPLY.resultTable.Rows[0]["REPLY_DTE"]).Substring(0, 4) + "/" +
                                Convert.ToString(PUBLIC_APPLY.resultTable.Rows[0]["REPLY_DTE"]).Substring(4, 2) + "/" +
                                Convert.ToString(PUBLIC_APPLY.resultTable.Rows[0]["REPLY_DTE"]).Substring(6, 2);
                    APPLY_DTE = Convert.ToString(PUBLIC_APPLY.resultTable.Rows[0]["APPLY_DTE"]).Substring(0, 4) + "/" +
                                Convert.ToString(PUBLIC_APPLY.resultTable.Rows[0]["APPLY_DTE"]).Substring(4, 2) + "/" +
                                Convert.ToString(PUBLIC_APPLY.resultTable.Rows[0]["APPLY_DTE"]).Substring(6, 2);
                    CARD_NBR  = Convert.ToString(PUBLIC_APPLY.resultTable.Rows[0]["PAY_CARD_NBR"]);
                    break;
                case "F0023":
                    REPLY_DTE = "";
                    APPLY_DTE = "";
                    break;
                default: //資料庫錯誤
                    logger.strJobQueue = "查詢PUBLIC_APPLY.query_BRANCH_INF() 資料錯誤:" + PUBLIC_APPLY_RC;
                    logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                    throw new System.Exception("B0016：" + logger.strJobQueue);
            }
            #endregion
            //終止舊的PAY_NBR
            PUBLIC_APPLY.strVAILD_FLAG = "C";
            PUBLIC_APPLY.strEXPIR_DTE = TODAY_PROCESS_DTE.ToString("yyyyMMdd");
            PUBLIC_APPLY.strERROR_REASON = "變更扣繳帳號為" + NEW_PAY_NBR;
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
                        logger.strJobQueue = "更新PUBLIC_APPLY 資料錯誤: F0023, PAY_ACCT_NBR = " + PAY_ACCT_NBR + " PAY_NBR = " + PAY_NBR;
                        logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                        throw new System.Exception("F0023：" + logger.strJobQueue);
                    }
                    break;

                default: //資料庫錯誤
                    logger.strJobQueue = "更新PUBLIC_APPLY 資料錯誤:" + PUBLIC_APPLY_RC;
                    logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                    throw new System.Exception("B0016：" + logger.strJobQueue);
            }
            #endregion

            #region 新增新PAY_NBR
            PUBLIC_APPLY_NEW.table_define();
            PUBLIC_APPLY_NEW.resultTable.Clear();
            PUBLIC_APPLY_NEW.resultTable.ImportRow(PUBLIC_APPLY.resultTable.Rows[0]);
            PUBLIC_APPLY_NEW.resultTable.Rows[0]["PAY_NBR"] = NEW_PAY_NBR;
            PUBLIC_APPLY_NEW.resultTable.Rows[0]["APPLY_DTE"] = TODAY_YYYYMMDD;
            PUBLIC_APPLY_NEW.resultTable.Rows[0]["EXPIR_DTE"] = "29991231";
            PUBLIC_APPLY_NEW.resultTable.Rows[0]["FIRST_DTE"] = "";
            PUBLIC_APPLY_NEW.resultTable.Rows[0]["PAY_DTE"] = "";
            PUBLIC_APPLY_NEW.resultTable.Rows[0]["REPLY_FLAG"] = "Y";
            PUBLIC_APPLY_NEW.resultTable.Rows[0]["VAILD_FLAG"] = "Y";
            PUBLIC_APPLY_NEW.resultTable.Rows[0]["REPLY_DTE"] = TODAY_YYYYMMDD;
            PUBLIC_APPLY_NEW.resultTable.Rows[0]["CREATE_USER"] = strJobName;
            PUBLIC_APPLY_NEW.resultTable.Rows[0]["CREATE_DT"] = TODAY_PROCESS_DTE;
            PUBLIC_APPLY_NEW.resultTable.Rows[0]["MNT_DT"] = TODAY_PROCESS_DTE;
            PUBLIC_APPLY_NEW.resultTable.Rows[0]["MNT_USER"] = strJobName;
            string PUBLIC_APPLY_NEW_RC = PUBLIC_APPLY_NEW.insert_by_DT();
            switch (PUBLIC_APPLY_NEW_RC)
            {
                case "S0000": //更新成功 
                    PUBLIC_APPLY_Insert_Count++;
                    break;
                case "F0022": //重複
                    logger.strJobQueue = "新增PUBLIC_APPLY 資料重複: 扣繳帳號新增：" + NEW_PAY_NBR;
                    logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                    throw new System.Exception("F0022：" + logger.strJobQueue);
                   
                default: //資料庫錯誤
                    logger.strJobQueue = "更新PUBLIC_APPLY 資料錯誤:" + PUBLIC_APPLY_RC;
                    logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                    throw new System.Exception("B0016：" + logger.strJobQueue);
            }
            #endregion
            //寫出報表
            insertReport_TABLE_Sheet2();
        }
        #endregion

        #region 寫出至報表
        void writeReport(DataTable inTable)
        {
            CMCRPT001 CMCRPT001 = new CMCRPT001();

            //設定特殊欄位資料
            ArrayList alSumData = new ArrayList();
            alSumData.Add(new ExcelFactory.ListItem("#RPT_BANK_NAME-", strBANK_NAME+"-"));
            alSumData.Add(new ExcelFactory.ListItem("#RPT_TYPE_NAME", strDescr));
            alSumData.Add(new ExcelFactory.ListItem("#RPT_DATE", TODAY_PROCESS_DTE.ToString("yyyy/MM/dd")));
            alSumData.Add(new ExcelFactory.ListItem("#TOTAL_CNT", intFailCount + " 筆"));
            //設定合併欄位資料
            ArrayList alMegData = new ArrayList();

            //產出報表
            CMCRPT001.Output(inTable, alSumData, alMegData, strFileName, "PBRFIR001", strSheet, strSheet, TODAY_PROCESS_DTE,true);
        }
        #endregion

        #region Display
        void writeDisplay()
        {
            logger.strJobID = "00002";

            //讀取筆數
            logger.strTBL_NAME = "IF_PUBLIC_FISC";
            logger.strTBL_ACCESS = "Q";
            logger.intTBL_COUNT = DetailFileDataCount;
            logger.writeCounter();

            //更新PUBLIC_APPLY
            logger.strTBL_NAME = "PUBLIC_APPLY";
            logger.strTBL_ACCESS = "U";
            logger.intTBL_COUNT = PUBLIC_APPLY_Update_Count;
            logger.writeCounter();

            //新增PUBLIC_APPLY
            logger.strTBL_NAME = "PUBLIC_APPLY_NEW";
            logger.strTBL_ACCESS = "I";
            logger.intTBL_COUNT = PUBLIC_APPLY_Insert_Count;
            logger.writeCounter();

            logger.DisplayCounter(Cybersoft.Coca.Log.LevelEnum.Info);
        }
        #endregion
    }
}

