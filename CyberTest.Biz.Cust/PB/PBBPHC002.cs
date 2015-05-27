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
    /// 處理當日中華電信換號
    /// 執行週期: 每營業日執行
    /// IF ABEND, 需人工確認處理
    /// </summary>
    public class PBBPHC002
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
        DataTable PUBLIC_APPLY_DataTable = null;
        int PUBLIC_APPLY_Update_Count = 0;

        //宣告PUBLIC_APPLY_T
        PUBLIC_APPLYDao PUBLIC_APPLY_T = new PUBLIC_APPLYDao();
        int PUBLIC_APPLY_T_Insert_Count = 0;

        //宣告PUBLIC_LIST
        String PUBLIC_LIST_RC = "";
        PUBLIC_LISTDao PUBLIC_LIST = new PUBLIC_LISTDao();
        int PUBLIC_LIST_Query_Count = 0;

        #endregion

        #region 設定檔案編碼
        Encoding BIG5 = Encoding.GetEncoding("big5");
        #endregion

        #region 宣告共用變數

        //批次日期
        DateTime PREV_PROCESS_DTE = new DateTime();
        DateTime TODAY_PROCESS_DTE = new DateTime();
        DateTime NEXT_PROCESS_DTE = new DateTime();

        //筆數&金額
        int i = 0;
        int SEQ = 0;
        String PAY_DATA_AREA = "";
        String OFFICE_CODE_OLD = "";
        String TELNO_OLD = "";
        String PAY_NBR_OLD = "";
        String OFFICE_CODE_NEW = "";
        String TELNO_NEW = "";
        String PAY_NBR_NEW = "";
        bool APPLY_FLAG = false;    //新帳號是否需新增

        #endregion

        #region 宣告檔案路徑

        //XML放置路徑 
        String strXML_Layout = "";

        #endregion

        static readonly Cybersoft.Coca.Log.Logger logger = Cybersoft.Log.LogManager<Cybersoft.Coca.Log.Logger>.GetLogger(System.Reflection.MethodInfo.GetCurrentMethod().ReflectedType.Name);
        public string RUN()
        {
            #region 準備MSGLG.SYSOUT CLASS
            logger.strJobID = JobID;
            logger.strJOBNAME = "PBBPHC002";
            logger.dtRunDate = DateTime.Now;
            #endregion

            try
            {
                #region 取得系統資訊(前次批次日、本日批次日)
                CMCSYS001 SYSINF = new CMCSYS001();
                SYSINF.strJobID = JobID;
                SYSINF.strJobName = "PBBPHC002";
                String SYSINF_RC = SYSINF.getSYSINF();
                PREV_PROCESS_DTE = SYSINF.datetimePREV_PROCESS_DTE;
                TODAY_PROCESS_DTE = SYSINF.datetimeTODAY_PROCESS_DTE;
                NEXT_PROCESS_DTE = SYSINF.datetimeBANK_NEXT_PROCESS_DTE;
                #endregion

                #region 複製Table定義
                PUBLIC_APPLY_T.table_define();
                #endregion

                #region 宣告檔案路徑
                Cybersoft.Base.CMCURL001 CMCURL = new CMCURL001();
                //XML名稱
                strXML_Layout = CMCURL.getURL("PUBLIC_PHONE.xml");

                if (strXML_Layout == "")
                {
                    logger.strJobQueue = "取得PUBLIC_PHONE.xml錯誤!!!";
                    logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                    return "B0099:" + logger.strJobQueue;
                }
                else
                {
                    logger.strJobQueue = "PUBLIC_PHONE.xml路徑為 " + strXML_Layout;
                    logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                }
                #endregion

                #region 擷取今日需送回應檔之中華電信資料
                PUBLIC_LIST.init();
                PUBLIC_LIST.strWherePAY_TYPE = "0001";
                PUBLIC_LIST.strWherePROCESS_DTE = TODAY_PROCESS_DTE.ToString("yyyyMMdd");
                PUBLIC_LIST_RC = PUBLIC_LIST.query_for_PHONE_CHANGE();
                switch (PUBLIC_LIST_RC)
                {
                    case "S0000": //查詢成功
                        PUBLIC_LIST_Query_Count = PUBLIC_LIST.resultTable.Rows.Count;
                        break;

                    case "F0023": //無需處理資料
                        logger.strJobQueue = "今日無中華電信換號資料";
                        logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                        return "B0000";

                    default: //資料庫錯誤
                        logger.strJobQueue = "查詢 PUBLIC_LIST.query_for_PHONE_CHANGE() 資料錯誤:" + PUBLIC_LIST_RC;
                        logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                        return "B0016:" + logger.strJobQueue;
                }
                #endregion

                #region 載入XML
                FileParseByXml xml = new FileParseByXml();
                //REC Layout
                DataTable REC_DataTable = xml.Xml2DataTable(strXML_Layout, "STMTIN");

                if (xml.strMSG.Length > 0)
                {
                    logger.strJobQueue = "[Xml2DataTable(REC)] - " + xml.strMSG.ToString().Trim();
                    logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                    return "B0099:" + logger.strJobQueue;
                }
                #endregion

                for (i = 0; i < PUBLIC_LIST.resultTable.Rows.Count; i++)
                {
                    SEQ = 0;
                    APPLY_FLAG = false;

                    PAY_DATA_AREA = Convert.ToString(PUBLIC_LIST.resultTable.Rows[i]["PAY_DATA_AREA"]);
                    DataTable InfData_DataTable = new DataTable();

                    #region 依 Layout 拆解資料
                    InfData_DataTable = xml.FileLine2DataTable(BIG5, PAY_DATA_AREA, REC_DataTable);
                    if (xml.strMSG.Length > 0)
                    {
                        logger.strJobQueue = "[FileLine2DataTable(REC)] (L" + i + 1 + ") - " + xml.strMSG.ToString().Trim();
                        logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                        return "B0099:" + logger.strJobQueue;
                    }
                    OFFICE_CODE_OLD = Convert.ToString(InfData_DataTable.Rows[0]["OFFICE_CODE_OLD"]);
                    TELNO_OLD = Convert.ToString(InfData_DataTable.Rows[0]["TELNO_OLD"]);
                    PAY_NBR_OLD = OFFICE_CODE_OLD.ToString().PadRight(4, ' ') + TELNO_OLD.PadLeft(12, ' ');

                    OFFICE_CODE_NEW = Convert.ToString(InfData_DataTable.Rows[0]["OFFICE_CODE"]);
                    TELNO_NEW = Convert.ToString(InfData_DataTable.Rows[0]["TELNO"]);
                    PAY_NBR_NEW = OFFICE_CODE_NEW.ToString().PadRight(4, ' ') + TELNO_NEW.PadLeft(12, ' ');
                    #endregion

                    #region 以新號碼讀取PUBLIC_APPLY
                    PUBLIC_APPLY.init();
                    PUBLIC_APPLY.strWherePAY_TYPE = "0001";
                    PUBLIC_APPLY.strWherePAY_NBR = PAY_NBR_NEW;
                    PUBLIC_APPLY.strWherePAY_CARD_NBR_PREV = Convert.ToString(InfData_DataTable.Rows[0]["ACCT_NBR"]);
                    PUBLIC_APPLY_RC = PUBLIC_APPLY.query_for_PHONE_CHANGE();
                    switch (PUBLIC_APPLY_RC)
                    {
                        case "S0000": //查詢成功
                            if (PUBLIC_APPLY.resultTable.Rows[0]["VAILD_FLAG"].ToString() != "Y")
                            {
                                //表示該新帳號曾經解除約定，需重新約定
                                SEQ = Convert.ToInt16(PUBLIC_APPLY.resultTable.Rows[0]["SEQ"]) + 1;
                                APPLY_FLAG = true;
                            }
                            else
                            {
                                //表示該新帳號已約定成功
                                APPLY_FLAG = false;
                            }
                            break;

                        case "F0023": //查無該筆資料，需約定該新帳號至PUBLIC_APPLY
                            SEQ = 0;
                            APPLY_FLAG = true;
                            break;

                        default: //資料庫錯誤
                            logger.strJobQueue = "查詢 PUBLIC_APPLY(NEW) 資料錯誤:" + PUBLIC_APPLY_RC;
                            logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                            return "B0016:" + logger.strJobQueue;
                    }
                    #endregion

                    #region 以舊號碼讀取PUBLIC_APPLY
                    PUBLIC_APPLY.init();
                    PUBLIC_APPLY.strWherePAY_TYPE = "0001";
                    PUBLIC_APPLY.strWherePAY_NBR = PAY_NBR_OLD;
                    PUBLIC_APPLY.strWherePAY_CARD_NBR_PREV = Convert.ToString(InfData_DataTable.Rows[0]["ACCT_NBR"]);
                    PUBLIC_APPLY_RC = PUBLIC_APPLY.query_for_PHONE_CHANGE();
                    switch (PUBLIC_APPLY_RC)
                    {
                        case "S0000": //查詢成功
                            PUBLIC_APPLY.resultTable.DefaultView.Sort = "SEQ DESC"; 
                            PUBLIC_APPLY_DataTable = PUBLIC_APPLY.resultTable.DefaultView.ToTable();
                            if (APPLY_FLAG)
                            {
                                //約定新帳號
                                PUBLIC_APPLY_T.resultTable.ImportRow(PUBLIC_APPLY.resultTable.Rows[0]);
                                PUBLIC_APPLY_T.resultTable.Rows[PUBLIC_APPLY_T_Insert_Count]["SEQ"] = SEQ.ToString().PadLeft(3,'0');
                                PUBLIC_APPLY_T.resultTable.Rows[PUBLIC_APPLY_T_Insert_Count]["PAY_NBR"] = PAY_NBR_NEW;
                                PUBLIC_APPLY_T.resultTable.Rows[PUBLIC_APPLY_T_Insert_Count]["EXPIR_DTE"] = "29991231";
                                PUBLIC_APPLY_T.resultTable.Rows[PUBLIC_APPLY_T_Insert_Count]["APPLY_DTE"] = TODAY_PROCESS_DTE.ToString("yyyyMMdd");
                                PUBLIC_APPLY_T.resultTable.Rows[PUBLIC_APPLY_T_Insert_Count]["FIRST_DTE"] = TODAY_PROCESS_DTE.ToString("yyyyMMdd");
                                PUBLIC_APPLY_T.resultTable.Rows[PUBLIC_APPLY_T_Insert_Count]["PAY_DTE"] = TODAY_PROCESS_DTE.ToString("yyyyMMdd");
                                PUBLIC_APPLY_T.resultTable.Rows[PUBLIC_APPLY_T_Insert_Count]["VAILD_FLAG"] = "Y";
                                PUBLIC_APPLY_T.resultTable.Rows[PUBLIC_APPLY_T_Insert_Count]["REPLY_FLAG"] = "Y";
                                PUBLIC_APPLY_T.resultTable.Rows[PUBLIC_APPLY_T_Insert_Count]["REPLY_DTE"] = TODAY_PROCESS_DTE.ToString("yyyyMMdd");
                                PUBLIC_APPLY_T.resultTable.Rows[PUBLIC_APPLY_T_Insert_Count]["USER_FIELD_1"] = "";
                                PUBLIC_APPLY_T.resultTable.Rows[PUBLIC_APPLY_T_Insert_Count]["USER_FIELD_2"] = "";
                                PUBLIC_APPLY_T.resultTable.Rows[PUBLIC_APPLY_T_Insert_Count]["MNT_DT"] = TODAY_PROCESS_DTE;
                                PUBLIC_APPLY_T.resultTable.Rows[PUBLIC_APPLY_T_Insert_Count]["MNT_USER"] = "PBBPHC002";
                                PUBLIC_APPLY_T_Insert_Count++;
                            }

                            #region 將舊號碼終止約定
                            PUBLIC_APPLY.init();
                            PUBLIC_APPLY.strWherePAY_TYPE = "0001";
                            PUBLIC_APPLY.strWherePAY_NBR = PAY_NBR_OLD;
                            PUBLIC_APPLY.strWhereVAILD_FLAG = "Y";
                            PUBLIC_APPLY.strWherePAY_CARD_NBR = PUBLIC_APPLY.resultTable.Rows[0]["PAY_CARD_NBR"].ToString();
                            PUBLIC_APPLY.strWhereSEQ = PUBLIC_APPLY.resultTable.Rows[0]["SEQ"].ToString();
                            PUBLIC_APPLY.DateTimeWhereMNT_DT = Convert.ToDateTime(PUBLIC_APPLY.resultTable.Rows[0]["MNT_DT"]);
                            PUBLIC_APPLY.strWhereMNT_USER = PUBLIC_APPLY.resultTable.Rows[0]["MNT_USER"].ToString();

                            PUBLIC_APPLY.strEXPIR_DTE = TODAY_PROCESS_DTE.ToString("yyyyMMdd");
                            PUBLIC_APPLY.strVAILD_FLAG = "C";
                            PUBLIC_APPLY.strERROR_REASON = "變更扣繳帳號為" + PAY_NBR_NEW;
                            PUBLIC_APPLY.strERROR_REASON_DT = TODAY_PROCESS_DTE.ToString("yyyyMMdd");
                            PUBLIC_APPLY.datetimeMNT_DT = TODAY_PROCESS_DTE;
                            PUBLIC_APPLY.strMNT_USER = "PBBPHC002";

                            PUBLIC_APPLY_RC = PUBLIC_APPLY.update();
                            switch (PUBLIC_APPLY_RC)
                            {
                                case "S0000":
                                    if (PUBLIC_APPLY.intUptCnt == 0)
                                    {
                                        logger.strJobQueue = "異動PUBLIC_APPLY失敗，PAY_NBR = " + PAY_NBR_OLD;
                                        logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                                    }
                                    PUBLIC_APPLY_Update_Count = PUBLIC_APPLY_Update_Count + PUBLIC_APPLY.intUptCnt;
                                    break;

                                default: //資料庫錯誤
                                    logger.strJobQueue = "PUBLIC_APPLY.update() 錯誤:" + PUBLIC_APPLY_RC;
                                    logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                                    return "B0016:" + logger.strJobQueue;
                            }
                            #endregion
                            break;

                        case "F0023": //查無舊號碼約定資料, reject(寫Log)
                            logger.strJobQueue = "查無舊號碼資料, PAY_NBR: " + PAY_NBR_OLD;
                            logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                            break;

                        default: //資料庫錯誤
                            logger.strJobQueue = "查詢 PUBLIC_APPLY(OLD) 資料錯誤:" + PUBLIC_APPLY_RC;
                            logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                            return "B0016:" + logger.strJobQueue;
                    }
                    #endregion
                }

                #region 將資料整批寫入PUBLIC_APPLY檔
                if (PUBLIC_APPLY_T.resultTable.Rows.Count > 0)
                {
                    PUBLIC_APPLY_T.insert_by_DT();

                    //判別回傳筆數是否相同
                    if (PUBLIC_APPLY_T.resultTable.Rows.Count != PUBLIC_APPLY_T.intInsCnt)
                    {
                        logger.strJobQueue = "整批新增PUBLIC_APPLY_T時筆數異常, 原筆數 : " + PUBLIC_APPLY_T.resultTable.Rows.Count + " 實際筆數: " + PUBLIC_APPLY_T.intInsCnt;
                        logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                        return "B0012" + logger.strJobQueue;
                    }

                    logger.strJobQueue = "整批新增至 PUBLIC_APPLY 成功筆數 = " + PUBLIC_APPLY_T.intInsCnt;
                    logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
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

        #region Display
        void writeDisplay()
        {
            logger.strJobID = "00002";
            //擷取今日需送回應檔之中華電信資料
            logger.strTBL_NAME = "PUBLIC_LIST";
            logger.strTBL_ACCESS = "Q";
            logger.intTBL_COUNT = PUBLIC_LIST_Query_Count;
            logger.writeCounter();

            //新增新號碼約定
            logger.strTBL_NAME = "PUBLIC_APPLY";
            logger.strTBL_ACCESS = "I";
            logger.intTBL_COUNT = PUBLIC_APPLY_T_Insert_Count;
            logger.writeCounter();
            
            //終止舊號碼約定
            logger.strTBL_NAME = "PUBLIC_APPLY";
            logger.strTBL_ACCESS = "U";
            logger.intTBL_COUNT = PUBLIC_APPLY_Update_Count;
            logger.writeCounter();

            logger.DisplayCounter(Cybersoft.Coca.Log.LevelEnum.Info);
        }
        #endregion
    }
}


