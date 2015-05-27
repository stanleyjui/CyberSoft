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
    /// 處理當日台電換號
    /// 執行週期: 每營業日執行
    /// </summary>
    public class PBBFIC002
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
        int PUBLIC_HIST_Query_Count = 0;

        //宣告PUBLIC_APPLY
        String PUBLIC_APPLY_RC = "";
        PUBLIC_APPLYDao PUBLIC_APPLY = new PUBLIC_APPLYDao();
        DataTable PUBLIC_APPLY_DataTable = null;
        int PUBLIC_APPLY_Update_Count = 0;

        //宣告PUBLIC_APPLY_T
        PUBLIC_APPLYDao PUBLIC_APPLY_T = new PUBLIC_APPLYDao();
        int PUBLIC_APPLY_T_Insert_Count = 0;

        //宣告PUBLIC_LIST
        PUBLIC_LISTDao PUBLIC_LIST = new PUBLIC_LISTDao();
        int PUBLIC_LIST_Query_Count = 0;

        #endregion

        #region 設定檔案編碼
        Encoding BIG5 = Encoding.GetEncoding("big5");
        #endregion

        #region 宣告常數
        const string strRecNAME = "PUBLIC_FISC";
        const string strJobName = "PBBFIC002";
        #endregion 

        #region 宣告共用變數
        //批次日期
        DateTime PREV_PROCESS_DTE = new DateTime();
        DateTime TODAY_PROCESS_DTE = new DateTime();
        DateTime NEXT_PROCESS_DTE = new DateTime();

        //筆數&金額
        int i = 0;
        int SEQ = 0;
        String PAY_NBR_OLD = "";
        String PAY_NBR_NEW = "";
        bool APPLY_FLAG = false;    //新帳號是否需新增

        #endregion

        #region 宣告檔案路徑

        #endregion

        static readonly Cybersoft.Coca.Log.Logger logger = Cybersoft.Log.LogManager<Cybersoft.Coca.Log.Logger>.GetLogger(System.Reflection.MethodInfo.GetCurrentMethod().ReflectedType.Name);
        //========================================================================================================
        #region 程式主邏輯【MAIN Routine】
        public string RUN()
        {
            #region 準備MSGLG.SYSOUT CLASS
            logger.strJobID = JobID;
            logger.strJOBNAME =strJobName;
            logger.dtRunDate = DateTime.Now;
            #endregion

            try
            {
                #region 取得系統資訊(前次批次日、本日批次日)
                CMCSYS001 SYSINF = new CMCSYS001();
                SYSINF.strJobID = JobID;
                SYSINF.strJobName =strJobName;
                String SYSINF_RC = SYSINF.getSYSINF();
                PREV_PROCESS_DTE = SYSINF.datetimePREV_PROCESS_DTE;
                TODAY_PROCESS_DTE = SYSINF.datetimeTODAY_PROCESS_DTE;
                NEXT_PROCESS_DTE = SYSINF.datetimeBANK_NEXT_PROCESS_DTE;
                #endregion

                #region 複製Table定義
                PUBLIC_APPLY_T.table_define();
                PUBLIC_APPLY.table_define();
                PUBLIC_HIST.table_define();
                #endregion

                #region 擷取今日需送回應檔之資料
                PUBLIC_HIST.init();
                PUBLIC_HIST.strWherePAY_TYPE = "0004";  //台電
                PUBLIC_HIST.strWhereTRANS_DTE = TODAY_PROCESS_DTE.ToString("yyyyMMdd");
                PUBLIC_HIST_RC = PUBLIC_HIST.query_for_PAY_NBR_CHANGE();
                switch (PUBLIC_HIST_RC)
                {
                    case "S0000": //查詢成功
                        PUBLIC_HIST_Query_Count = PUBLIC_HIST.resultTable.Rows.Count;
                        break;

                    case "F0023": //無需處理資料
                        logger.strJobQueue = "今日無台電換號資料";
                        logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                        return "B0000";

                    default: //資料庫錯誤
                        logger.strJobQueue = "查詢 PUBLIC_HIST.query_for_PAY_NBR_CHANGE() 資料錯誤:" + PUBLIC_HIST_RC;
                        logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                        return "B0016:" + logger.strJobQueue;
                }
                #endregion

                if (PUBLIC_HIST_Query_Count > 0)
                {

                    for (i = 0; i < PUBLIC_HIST.resultTable.Rows.Count; i++)
                    {
                        SEQ = 0;
                        APPLY_FLAG = false;
                        PAY_NBR_NEW = PUBLIC_HIST.resultTable.Rows[i]["CHANGE_NBR_NEW"].ToString().Trim();
                        PAY_NBR_OLD = PUBLIC_HIST.resultTable.Rows[i]["PAY_NBR"].ToString().Trim();

                        #region 以新號碼讀取PUBLIC_APPLY
                        PUBLIC_APPLY.init();
                        PUBLIC_APPLY.strWherePAY_TYPE = "0004";
                        PUBLIC_APPLY.strWherePAY_NBR = PAY_NBR_NEW;
                        PUBLIC_APPLY.strWherePAY_CARD_NBR_PREV = PUBLIC_HIST.resultTable.Rows[i]["PAY_CARD_NBR"].ToString().Trim();
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
                        PUBLIC_APPLY.strWherePAY_TYPE = "0004";
                        PUBLIC_APPLY.strWherePAY_NBR = PAY_NBR_OLD;
                        PUBLIC_APPLY.strWherePAY_CARD_NBR_PREV = PUBLIC_HIST.resultTable.Rows[i]["PAY_CARD_NBR"].ToString().Trim();
                        PUBLIC_APPLY_RC = PUBLIC_APPLY.query_for_PAY_NBR_CHANGE();
                        switch (PUBLIC_APPLY_RC)
                        {
                            case "S0000": //查詢成功
                                PUBLIC_APPLY.resultTable.DefaultView.Sort = "SEQ DESC";
                                PUBLIC_APPLY_DataTable = PUBLIC_APPLY.resultTable.DefaultView.ToTable();
                                if (APPLY_FLAG)
                                {
                                    //約定新帳號
                                    PUBLIC_APPLY_T.resultTable.ImportRow(PUBLIC_APPLY.resultTable.Rows[0]);
                                    PUBLIC_APPLY_T.resultTable.Rows[PUBLIC_APPLY_T_Insert_Count]["SEQ"] = SEQ.ToString().PadLeft(3, '0');
                                    PUBLIC_APPLY_T.resultTable.Rows[PUBLIC_APPLY_T_Insert_Count]["PAY_NBR"] = PAY_NBR_NEW;
                                    PUBLIC_APPLY_T.resultTable.Rows[PUBLIC_APPLY_T_Insert_Count]["EXPIR_DTE"] = "29991231";
                                    PUBLIC_APPLY_T.resultTable.Rows[PUBLIC_APPLY_T_Insert_Count]["APPLY_DTE"] = TODAY_PROCESS_DTE.ToString("yyyyMMdd");
                                    PUBLIC_APPLY_T.resultTable.Rows[PUBLIC_APPLY_T_Insert_Count]["FIRST_DTE"] = TODAY_PROCESS_DTE.ToString("yyyyMMdd");
                                    PUBLIC_APPLY_T.resultTable.Rows[PUBLIC_APPLY_T_Insert_Count]["PAY_DTE"] = TODAY_PROCESS_DTE.ToString("yyyyMMdd");
                                    PUBLIC_APPLY_T.resultTable.Rows[PUBLIC_APPLY_T_Insert_Count]["VAILD_FLAG"] = "Y";
                                    PUBLIC_APPLY_T.resultTable.Rows[PUBLIC_APPLY_T_Insert_Count]["REPLY_FLAG"] = "Y";
                                    PUBLIC_APPLY_T.resultTable.Rows[PUBLIC_APPLY_T_Insert_Count]["REPLY_DTE"] = TODAY_PROCESS_DTE.ToString("yyyyMMdd");
                                    PUBLIC_APPLY_T.resultTable.Rows[PUBLIC_APPLY_T_Insert_Count]["MNT_DT"] = TODAY_PROCESS_DTE;
                                    PUBLIC_APPLY_T.resultTable.Rows[PUBLIC_APPLY_T_Insert_Count]["MNT_USER"] =strJobName;
                                    PUBLIC_APPLY_T_Insert_Count++;
                                }

                                #region 將舊號碼終止約定
                                PUBLIC_APPLY.init();
                                PUBLIC_APPLY.strWherePAY_TYPE = "0004";
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
                                PUBLIC_APPLY.strMNT_USER =strJobName;

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


