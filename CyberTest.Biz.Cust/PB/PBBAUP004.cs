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
    /// 程式說明:公共事業代繳批次授權及產出授權一覽表(中華電信、台電、省水、市水)
    /// ABEND處理:不可RERUN;主要是以須進行批次COLA授權
    /// 特別功能:利用FLAG可產生今日需授權或所有需授權之資料
    /// 授權錯誤代碼：select * from COLA_CODE where TYPE = 'REASON'
    /// </summary>
    public class PBBAUP004
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
        DataTable PUBLIC_HIST_TABLE = new DataTable();

        //宣告PUBLIC_HIST_upd
        String PUBLIC_HIST_upd_RC = "";
        PUBLIC_HISTDao PUBLIC_HIST_upd = new PUBLIC_HISTDao();
        int PUBLIC_HIST_UPDATE_Count = 0;

        //宣告COLA_CODE
        String COLA_CODE_RC = "";
        COLA_CODEDao COLA_CODE = new COLA_CODEDao();
        DataTable COLA_CODE_DataTable = new DataTable();

        //宣告SETUP_REJECT
        String SETUP_REJECT_RC = "";
        SETUP_REJECTDao SETUP_REJECT = new SETUP_REJECTDao();
        DataTable SETUP_REJECT_DataTable = new DataTable();

        //批次授權明細檔
        string COLA_UPD_AUTH_RC = "";
        COLA_UPD_AUTHDao COLA_UPD_AUTH = new COLA_UPD_AUTHDao();
        DataTable COLA_UPD_AUTH_DataTable = new DataTable();
        int COLA_UPD_AUTH_Query_Count = 0;

        //批次授權記錄檔
        string COLA_UPD_AUTH_H_RC = "";
        COLA_UPD_AUTH_HDao COLA_UPD_AUTH_H = new COLA_UPD_AUTH_HDao();
        DataTable COLA_UPD_AUTH_H_DataTable = new DataTable();
        int COLA_UPD_AUTH_H_Query_Count = 0;

        //暫存報表TABLE
        DataTable REPORT_TABLE = new DataTable();
        #endregion

        #region 宣告檔案路徑
        //XML放置路徑 
        String strXML_Layout = "";
        //寫出檔案名稱
        String strOutFileName = "";
        //寫出檔案路徑
        String FILE_PATH = "";
        #endregion

        #region 設定檔案編碼
        Encoding BIG5 = Encoding.GetEncoding("big5");
        #endregion

        #region 宣告共用變數
        string strJobName = "PBBAUP004";
        //string strfile_name = "PB1"; //改為參數
        //批次日期        
        DateTime TODAY_PROCESS_DTE = new DateTime();
        DateTime NEXT_PROCESS_DTE = new DateTime();
        DateTime Bank_NEXT_PROCESS_DTE = new DateTime();

        //PUBLIC_HIST 欄位
        String PAY_TYPE_DESCR = "";
        String TRANS_DTE = "";
        String PAY_TYPE = "";
        String PAY_CARD_NBR = "";
        String EXPIR_DTE = "";
        Decimal PAY_AMT = 0;
        String PAY_NBR = "";
        String PAY_SEQ = "";
        DateTime PAY_DTE = new DateTime();
        String PAY_RESULT = "";
        String CUST_ID = "";
        String FILLER = "";
        String strBank_name = "";
        //授權回應碼相關欄位
        string AUTH_CODE = "";
        string RETURN_CODE = "";

        //報表欄位
        String ERROR_REASON = "";
        String strSEQ = "";
        DateTime PAY_DTE_PHONE = new DateTime();
        DateTime PAY_DTE_ELECT = new DateTime();
        DateTime PAY_DTE_TWWATER = new DateTime();
        DateTime PAY_DTE_ELECT_H = new DateTime();
        DateTime PAY_DTE_TPWATER = new DateTime();

        //筆數
        int PUBLIC_HIST_Query_Count = 0;
        int i = 0; //控制PUBLIC_HIST
        int x = 0; //控制VPOS_T
        int y = 0; //控制報表Table
        int NOMATCH_CNT = 0;
        int CHK_ERR_Count = 0;
        int CHK_OK_Count = 0;
        int CHK_ERR_PHONE_Count = 0;
        int CHK_ERR_ELECT_Count = 0;
        int CHK_ERR_ELECT_H_Count = 0;
        int CHK_ERR_TWWATER_Count = 0;
        int CHK_ERR_TPWATER_Count = 0;
        int CHK_OK_PHONE_Count = 0;
        int CHK_OK_ELECT_Count = 0;
        int CHK_OK_ELECT_H_Count = 0;
        int CHK_OK_TWWATER_Count = 0;
        int CHK_OK_TPWATER_Count = 0;
        int PHONE_Count = 0;
        int ELECT_Count = 0;
        int ELECT_H_Count = 0;
        int TWWATER_Count = 0;
        int TPWATER_Count = 0;
        int VPOS_TXT_Count = 0;
        int PUBLIC_HIST_NoMatch_Count = 0;
        int AUTH_APPROVE_Count = 0;
        int AUTH_REJECT_Count = 0;

        //DAO 控制參數
        String FLAG = "T";  //T:當日需授權  A:所有需授權

        #endregion

        static readonly Cybersoft.Coca.Log.Logger logger = Cybersoft.Log.LogManager<Cybersoft.Coca.Log.Logger>.GetLogger(System.Reflection.MethodInfo.GetCurrentMethod().ReflectedType.Name);
        //========================================================================================================
        #region 程式主邏輯【MAIN Routine】
        public string RUN(string strfile_name)
        {
            #region 準備MSGLG.SYSOUT CLASS
            logger.strJobID = JobID;
            logger.strJOBNAME = strJobName;
            logger.dtRunDate = DateTime.Now;
            #endregion

            try
            {
                #region 取得系統資訊(前次批次日、本日批次日)
                CMCSYS001 SYSINF = new CMCSYS001();
                SYSINF.strJobID = JobID;
                SYSINF.strJobName = strJobName;
                String SYSINF_RC = SYSINF.getSYSINF();
                strBank_name = SYSINF.strREPORT_TITLE;

                TODAY_PROCESS_DTE = SYSINF.datetimeTODAY_PROCESS_DTE;
                NEXT_PROCESS_DTE = SYSINF.datetimeNEXT_PROCESS_DTE;
                Bank_NEXT_PROCESS_DTE = SYSINF.datetimeBANK_NEXT_PROCESS_DTE;

                DateTime dtNow = DateTime.Now;
                #endregion

                #region 定義報表Table--公用事業授權一覽表
                REPORT_TABLE.Columns.Add("PAY_TYPE", typeof(string));
                REPORT_TABLE.Columns.Add("ID", typeof(string));
                REPORT_TABLE.Columns.Add("PAY_CARD_NBR", typeof(string));
                REPORT_TABLE.Columns.Add("PAY_NBR", typeof(string));
                REPORT_TABLE.Columns.Add("AMT", typeof(string));
                REPORT_TABLE.Columns.Add("PAY_RESULT", typeof(string));
                REPORT_TABLE.Columns.Add("AUTH_CODE", typeof(string));
                REPORT_TABLE.Columns.Add("ERROR_REASON", typeof(String));
                #endregion

                #region 撈出COLA授權後的錯誤代碼
                COLA_CODE.init();
                COLA_CODE.strWhereTYPE = "REASON";
                COLA_CODE_RC = COLA_CODE.query();
                switch (COLA_CODE_RC)
                {
                    case "S0000": //查詢成功
                        COLA_CODE_DataTable = COLA_CODE.resultTable;
                        break;

                    case "F0023": //無需處理資料
                        logger.strJobQueue = "查無COLA 授權錯誤代碼資料。";
                        logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                        break;

                    default: //資料庫錯誤
                        logger.strJobQueue = "查詢COLA_CODE 資料錯誤:" + COLA_CODE_RC;
                        logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                        return "B0016:" + logger.strJobQueue;
                }
                #endregion

                #region 撈出PUBLIC的錯誤代碼
                SETUP_REJECT.init();
                SETUP_REJECT.strWhereREJECT_GROUP = "PUBLIC";
                SETUP_REJECT_RC = SETUP_REJECT.query();
                switch (SETUP_REJECT_RC)
                {
                    case "S0000": //查詢成功
                        SETUP_REJECT_DataTable = SETUP_REJECT.resultTable;
                        break;

                    case "F0023": //無需處理資料
                        logger.strJobQueue = "查無COLA 授權錯誤代碼資料。";
                        logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                        break;

                    default: //資料庫錯誤
                        logger.strJobQueue = "查詢SETUP_REJECT 資料錯誤:" + SETUP_REJECT_RC;
                        logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                        return "B0016:" + logger.strJobQueue;
                }
                #endregion

                #region (for rerun) 將PUBLIC_HIST 有授權的資料狀態還原為N002
                PUBLIC_HIST.init();
                PUBLIC_HIST.strWhereTRANS_DTE = TODAY_PROCESS_DTE.ToString("yyyyMMdd");
                PUBLIC_HIST.DateTimeWhereMNT_DT = TODAY_PROCESS_DTE;
                PUBLIC_HIST.strWhereMNT_USER = strJobName;
                PUBLIC_HIST.strPAY_RESULT = "N002";
                PUBLIC_HIST_RC = PUBLIC_HIST.update_for_rerun();
                switch (PUBLIC_HIST_RC)
                {
                    case "S0000": //查詢成功
                        logger.strJobQueue = "(fro rerun)將PUBLIC_HIST 有授權的資料狀態還原為N002：" + PUBLIC_HIST.intUptCnt + "。";
                        logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                        break;

                    case "F0023": //無需處理資料
                        logger.strJobQueue = "(fro rerun)將PUBLIC_HIST 有授權的資料狀態還原為N002，筆數0筆。";
                        logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                        return "B0000";

                    default: //資料庫錯誤
                        logger.strJobQueue = "(fro rerun)將PUBLIC_HIST 有授權的資料狀態還原為N002錯誤:" + PUBLIC_HIST_RC;
                        logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                        return "B0016:" + logger.strJobQueue;
                }
                #endregion

                #region 查詢今日是否有傳送批次授權紀錄(PUBLIC_HIST)
                PUBLIC_HIST.init();
                PUBLIC_HIST.strWhereTRANS_DTE = TODAY_PROCESS_DTE.ToString("yyyyMMdd");
                PUBLIC_HIST.strWherePAY_RESULT = "N002";  //授權中
                PUBLIC_HIST_RC = PUBLIC_HIST.query();
                switch (PUBLIC_HIST_RC)
                {
                    case "S0000": //查詢成功
                        PUBLIC_HIST_Query_Count = PUBLIC_HIST.resultTable.Rows.Count;
                        PUBLIC_HIST_TABLE = PUBLIC_HIST.resultTable;
                        //排序
                        PUBLIC_HIST_TABLE.DefaultView.Sort = "PAY_CARD_NBR asc,PAY_AMT asc";
                        PUBLIC_HIST_TABLE = PUBLIC_HIST_TABLE.DefaultView.ToTable();

                        break;

                    case "F0023": //無需處理資料
                        logger.strJobQueue = "今日無產生傳送批次授權紀錄，不需更新。";
                        logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                        return "B0000";

                    default: //資料庫錯誤
                        logger.strJobQueue = "查詢PUBLIC_HIST 批次授權紀錄錯誤:" + PUBLIC_HIST_RC;
                        logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                        return "B0016:" + logger.strJobQueue;
                }
                #endregion 

                #region 查詢本日公共事業授權批次作業是否正常完成
                COLA_UPD_AUTH_H.init();
                //COLA_UPD_AUTH_H.strWhereSEQ = strfile_name + TODAY_PROCESS_DTE.ToString("yyyyMMdd");
                COLA_UPD_AUTH_H.strWhereSEQ = strfile_name + dtNow.ToString("yyyyMMdd");
                //COLA_UPD_AUTH_H.strWhereALLOW_DIRECTOR = strfile_name;
                //COLA_UPD_AUTH_H.strWhereUPLOAD_USER = "PBBAUP003";
                //COLA_UPD_AUTH_H.DateTimeWhereUPLOAD_DATE = TODAY_PROCESS_DTE;
                //COLA_UPD_AUTH_H.DateTimeWhereUPLOAD_DATE = DateTime.Now;
                COLA_UPD_AUTH_H_RC = COLA_UPD_AUTH_H.query_BATCH_AUTH_H();
                switch (COLA_UPD_AUTH_H_RC)
                {
                    case "S0000": //查詢成功
                        COLA_UPD_AUTH_H_Query_Count = COLA_UPD_AUTH_H.resultTable.Rows.Count;
                        COLA_UPD_AUTH_H_DataTable = COLA_UPD_AUTH_H.resultTable;
                        break;

                    case "F0023": //無需處理資料
                        logger.strJobQueue = "今日須執行批次授權，但查無批次授權紀錄，請與系統負責人聯繫。";
                        logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                        return "B0016";

                    default: //資料庫錯誤
                        logger.strJobQueue = "查詢COLA_UPD_AUTH_H 批次授權紀錄錯誤:" + COLA_UPD_AUTH_H_RC;
                        logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                        return "B0016:" + logger.strJobQueue;
                }
                #endregion

                #region 檢核是否有異常授權筆數(取最新一批授權紀錄)
                if (COLA_UPD_AUTH_H_Query_Count > 0)
                {
                    strSEQ = COLA_UPD_AUTH_H_DataTable.Rows[0]["SEQ"].ToString();
                    if ((Convert.ToDecimal(COLA_UPD_AUTH_H_DataTable.Rows[0]["TOTAL_ITEMS"]) +
                         Convert.ToDecimal(COLA_UPD_AUTH_H_DataTable.Rows[0]["TOTAL_FIX_ITEMS"]))
                         == PUBLIC_HIST_Query_Count)
                    {
                        if ((COLA_UPD_AUTH_H_DataTable.Rows[i]["FLAG"].ToString() != "00") &&
                            (COLA_UPD_AUTH_H_DataTable.Rows[i]["FLAG"].ToString() != "XX"))
                        {
                            logger.strJobQueue = "此批授權檔有失敗紀錄，請聯絡系統負責人，請查閱系統紀錄COLA_UPD_AUTH_H：SEQ = " + strSEQ +
                                                 " 若是當在這裡，請確認COLA_UPD_AUTH的錯誤資料是哪筆，修正其問題後，重新rerun此程式。";

                            logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                            return "B0099";
                        }
                    }
                    else
                    {
                        logger.strJobQueue = "查此批授權檔筆數與實際需授權筆數不一致，請聯絡系統負責人!!請查閱系統紀錄COLA_UPD_AUTH_H：SEQ = " + strSEQ;
                        logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                        return "B0099";
                    }
                    //查詢此批授權結果
                    COLA_UPD_AUTH.init();
                    COLA_UPD_AUTH.strWhereSEQ = strSEQ;
                    COLA_UPD_AUTH_RC = COLA_UPD_AUTH.query_Batch_auth();
                    switch (COLA_UPD_AUTH_RC)
                    {
                        case "S0000": //查詢成功
                            COLA_UPD_AUTH_Query_Count = COLA_UPD_AUTH.resultTable.Rows.Count;
                            COLA_UPD_AUTH_DataTable = COLA_UPD_AUTH.resultTable;
                            break;

                        case "F0023": //無需處理資料
                            logger.strJobQueue = "查此批授權明細筆數與實際需授權筆數不一致，請聯絡系統負責人!!請查閱系統紀錄COLA_UPD_AUTH：SEQ = " + strSEQ;
                            logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                            return "B0016";

                        default: //資料庫錯誤
                            logger.strJobQueue = "查詢COLA_UPD_AUTH 批次授權明細錯誤:" + COLA_UPD_AUTH_RC;
                            logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                            return "B0016:" + logger.strJobQueue;
                    }
                }
                #endregion

                #region 更新授權結果(old)
                //PUBLIC_HIST.init();
                //PUBLIC_HIST_RC = PUBLIC_HIST.query_for_COLA_UPD_AUTH(strSEQ,TODAY_PROCESS_DTE.ToString("yyyyMMdd"));
                //switch (PUBLIC_HIST_RC)
                //{
                //    case "S0000": //查詢成功
                //        PUBLIC_HIST_TABLE = PUBLIC_HIST.resultTable;
                //        PUBLIC_HIST_Query_Count = PUBLIC_HIST.resultTable.Rows.Count;
                //        break;

                //    case "F0023": //無需處理資料
                //        logger.strJobQueue = "今日無產生授權的代繳資料,請確認";
                //        logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                //        break;

                //    default: //資料庫錯誤
                //        logger.strJobQueue = "查詢PUBLIC_HIST 資料錯誤:" + PUBLIC_HIST_RC;
                //        logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                //        return "B0016:" + logger.strJobQueue;
                //}
                //if (PUBLIC_HIST_Query_Count > 0)
                //{
                //    for (int i = 0; i < PUBLIC_HIST_Query_Count; i++)
                //    {
                //        string strAUTH_CODE = "";
                //        string strERROR_REASON = "";
                //        if (PUBLIC_HIST_TABLE.Rows[i]["CHECK_CODE"].ToString() == "00" && PUBLIC_HIST_TABLE.Rows[i]["RSN_CODE"].ToString() == "00")
                //        {
                //            strAUTH_CODE = PUBLIC_HIST_TABLE.Rows[i]["AUTH_CODE"].ToString();
                //            PAY_RESULT = "S000";
                //            strERROR_REASON = "授權成功";
                //        }
                //        else
                //        {
                //            strAUTH_CODE = "";
                //            RETURN_CODE = PUBLIC_HIST_TABLE.Rows[i]["CHECK_CODE"].ToString().Substring(0,2) + PUBLIC_HIST_TABLE.Rows[i]["RSN_CODE"].ToString().Substring(0,2);
                //            switch (PUBLIC_HIST_TABLE.Rows[i]["CHECK_CODE"].ToString().Substring(0, 2))
                //            {
                //                case "06":
                //                    PAY_RESULT = "I006";  //可用餘額不足 
                //                    break;
                //                case "04":
                //                    PAY_RESULT = "I004";  //卡片未開卡  
                //                    break;
                //                default:
                //                    PAY_RESULT = "I009";  //授權失敗
                //                    break;
                //            }
                //            //錯誤原因說明
                //            DataRow[] DR = COLA_CODE_DataTable.Select(" TYPE_SUB ='" + RETURN_CODE.Substring(0, 2) + "' AND ADD_VAL1 = '" + RETURN_CODE.Substring(2, 2) + "' ");
                //            if (DR.Length > 0)
                //            {
                //                strERROR_REASON = RETURN_CODE +"_"+Convert.ToString(DR[0]["DESCR"]);
                //            }
                //            else
                //            {
                //                strERROR_REASON = RETURN_CODE;
                //            }
                //        }

                //        PUBLIC_HIST_upd.init();
                //        //查詢條件
                //        PUBLIC_HIST_upd.strWherePAY_SEQ = PUBLIC_HIST_TABLE.Rows[i]["PAY_SEQ"].ToString();
                //        PUBLIC_HIST_upd.strWherePAY_TYPE = PUBLIC_HIST_TABLE.Rows[i]["PAY_TYPE"].ToString();
                //        PUBLIC_HIST_upd.strWhereBU = PUBLIC_HIST_TABLE.Rows[i]["BU"].ToString();
                //        PUBLIC_HIST_upd.strWhereACCT_NBR = PUBLIC_HIST_TABLE.Rows[i]["ACCT_NBR"].ToString();
                //        //更新資訊
                //        PUBLIC_HIST_upd.strAUTH_CODE = strAUTH_CODE;
                //        PUBLIC_HIST_upd.strPAY_RESULT = PAY_RESULT;
                //        PUBLIC_HIST_upd.strERROR_REASON = strERROR_REASON;
                //        PUBLIC_HIST_upd.datetimeMNT_DT = TODAY_PROCESS_DTE;
                //        PUBLIC_HIST_upd.strMNT_USER = strJobName;
                //        PUBLIC_HIST_upd_RC = PUBLIC_HIST_upd.update();
                //        switch (PUBLIC_HIST_upd_RC)
                //        {
                //            case "S0000": //更新成功   
                //                PUBLIC_HIST_UPDATE_Count = PUBLIC_HIST_UPDATE_Count + PUBLIC_HIST_upd.intUptCnt;
                //                if (PUBLIC_HIST_upd.intUptCnt == 0)
                //                {
                //                    logger.strJobQueue = "更新PUBLIC_HIST 資料錯誤: F0023, CARD_NBR = " + PUBLIC_HIST_TABLE.Rows[i]["PAY_CARD_NBR"].ToString() + " CARD_NBR_APPLY = " + PAY_SEQ;
                //                    logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                //                    return "F0023" + logger.strJobQueue;
                //                }
                //                break;

                //            default: //資料庫錯誤
                //                logger.strJobQueue = "更新PUBLIC_HIST 資料錯誤:" + PUBLIC_HIST_upd_RC;
                //                logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                //                return "B0016:" + logger.strJobQueue;
                //        }
                //    }
                //}
                #endregion

                #region 更新授權
                for (int i = 0; i < PUBLIC_HIST_Query_Count; i++)
                {
                    if ((PUBLIC_HIST_TABLE.Rows[i]["PAY_CARD_NBR"].ToString() == COLA_UPD_AUTH_DataTable.Rows[i]["CARD_NBR"].ToString()) &
                        (Convert.ToDecimal(PUBLIC_HIST_TABLE.Rows[i]["PAY_AMT"]) == Convert.ToDecimal(COLA_UPD_AUTH_DataTable.Rows[i]["AMT"])))
                    {
                        string strAUTH_CODE = "";
                        string strERROR_REASON = "";
                        if ((Convert.ToString(COLA_UPD_AUTH_DataTable.Rows[i]["CHECK_CODE"]).Trim() != "") &&
                            (Convert.ToString(COLA_UPD_AUTH_DataTable.Rows[i]["RSN_CODE"]).Trim() != ""))
                        {
                            if (COLA_UPD_AUTH_DataTable.Rows[i]["CHECK_CODE"].ToString() == "00" && COLA_UPD_AUTH_DataTable.Rows[i]["RSN_CODE"].ToString() == "00")
                            {
                                strAUTH_CODE = COLA_UPD_AUTH_DataTable.Rows[i]["AUTH_CODE"].ToString();
                                PAY_RESULT = "S000";
                                strERROR_REASON = "授權成功";
                            }
                            else
                            {
                                strAUTH_CODE = "";
                                RETURN_CODE = COLA_UPD_AUTH_DataTable.Rows[i]["CHECK_CODE"].ToString().Substring(0, 2) + COLA_UPD_AUTH_DataTable.Rows[i]["RSN_CODE"].ToString().Substring(0, 2);
                                switch (COLA_UPD_AUTH_DataTable.Rows[i]["CHECK_CODE"].ToString().Substring(0, 2))
                                {
                                    case "06":
                                        PAY_RESULT = "I006";  //可用餘額不足 
                                        break;
                                    case "04":
                                        PAY_RESULT = "I004";  //卡片未開卡  
                                        break;
                                    default:
                                        PAY_RESULT = "I009";  //授權失敗
                                        break;
                                }
                                //錯誤原因說明
                                DataRow[] DR = COLA_CODE_DataTable.Select(" TYPE_SUB ='" + RETURN_CODE.Substring(0, 2) + "' AND ADD_VAL1 = '" + RETURN_CODE.Substring(2, 2) + "' ");
                                if (DR.Length > 0)
                                {
                                    strERROR_REASON = RETURN_CODE + "_" + Convert.ToString(DR[0]["DESCR"]);
                                }
                                else
                                {
                                    strERROR_REASON = RETURN_CODE;
                                }
                            }
                        }
                        else
                        {
                            PAY_RESULT = "I009";  //授權失敗
                            strERROR_REASON = "授權失敗";
                            strAUTH_CODE = "";
                        }
                        #region 更新PUBLIC_HIST
                        PUBLIC_HIST_upd.init();
                        //查詢條件
                        PUBLIC_HIST_upd.strWherePAY_SEQ = PUBLIC_HIST_TABLE.Rows[i]["PAY_SEQ"].ToString();
                        PUBLIC_HIST_upd.strWherePAY_TYPE = PUBLIC_HIST_TABLE.Rows[i]["PAY_TYPE"].ToString();
                        PUBLIC_HIST_upd.strWhereBU = PUBLIC_HIST_TABLE.Rows[i]["BU"].ToString();
                        PUBLIC_HIST_upd.strWhereACCT_NBR = PUBLIC_HIST_TABLE.Rows[i]["ACCT_NBR"].ToString();
                        //更新資訊
                        PUBLIC_HIST_upd.strAUTH_CODE = strAUTH_CODE;
                        PUBLIC_HIST_upd.strPAY_RESULT = PAY_RESULT;
                        PUBLIC_HIST_upd.strERROR_REASON = strERROR_REASON;
                        PUBLIC_HIST_upd.datetimeMNT_DT = TODAY_PROCESS_DTE;
                        PUBLIC_HIST_upd.strMNT_USER = strJobName;
                        PUBLIC_HIST_upd_RC = PUBLIC_HIST_upd.update();
                        switch (PUBLIC_HIST_upd_RC)
                        {
                            case "S0000": //更新成功   
                                PUBLIC_HIST_UPDATE_Count = PUBLIC_HIST_UPDATE_Count + PUBLIC_HIST_upd.intUptCnt;
                                if (PUBLIC_HIST_upd.intUptCnt == 0)
                                {
                                    logger.strJobQueue = "更新PUBLIC_HIST授權碼 資料錯誤: F0023, CARD_NBR = " + PUBLIC_HIST_TABLE.Rows[i]["PAY_CARD_NBR"].ToString() + " CARD_NBR_APPLY = " + PAY_SEQ;
                                    logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                                    return "F0023" + logger.strJobQueue;
                                }
                                break;

                            default: //資料庫錯誤
                                logger.strJobQueue = "更新PUBLIC_HIST 資料錯誤:" + PUBLIC_HIST_upd_RC;
                                logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                                return "B0016:" + logger.strJobQueue;
                        }
                        #endregion
                    }
                    else
                    {
                        NOMATCH_CNT++;
                        logger.strJobQueue = "PUBLIC_HIST與批次授權結果比對不一致:" +
                                             "授權紀錄卡號：" + COLA_UPD_AUTH_DataTable.Rows[i]["CARD_NBR"].ToString() + "授權金額：" + Convert.ToDecimal(COLA_UPD_AUTH_DataTable.Rows[i]["AMT"]) +
                                             "公共事業代繳卡號" + PUBLIC_HIST_TABLE.Rows[i]["PAY_CARD_NBR"].ToString() + "扣款金額：" + Convert.ToDecimal(PUBLIC_HIST_TABLE.Rows[i]["PAY_AMT"]);
                        logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                    }
                }
                if (NOMATCH_CNT > 0)
                {
                    logger.strJobQueue = "PUBLIC_HIST與COLA_UPD_AUTH比對有失敗筆數：" + NOMATCH_CNT + "請確認LOG。";
                    logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                    return "B0016:" + logger.strJobQueue;
                }
                #endregion

                #region 擷取今日公用事業授權結果
                TRANS_DTE = TODAY_PROCESS_DTE.ToString("yyyyMMdd");
                PUBLIC_HIST_RC = PUBLIC_HIST.query_AUTH_RPT(FLAG, TRANS_DTE);
                switch (PUBLIC_HIST_RC)
                {
                    case "S0000": //查詢成功
                        PUBLIC_HIST_Query_Count = PUBLIC_HIST.resultTable.Rows.Count;
                        break;

                    case "F0023": //無需處理資料
                        logger.strJobQueue = "今日無產生授權的代繳資料,請確認";
                        logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                        break;

                    default: //資料庫錯誤
                        logger.strJobQueue = "查詢PUBLIC_HIST 資料錯誤:" + PUBLIC_HIST_RC;
                        logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                        return "B0016:" + logger.strJobQueue;
                }
                #endregion

                #region 循序產出報表
                for (i = 0; i < PUBLIC_HIST.resultTable.Rows.Count; i++)
                {
                    PAY_TYPE = Convert.ToString(PUBLIC_HIST.resultTable.Rows[i]["PAY_TYPE"]);
                    PAY_TYPE_DESCR = Convert.ToString(PUBLIC_HIST.resultTable.Rows[i]["DESCR"]);
                    CUST_ID = Convert.ToString(PUBLIC_HIST.resultTable.Rows[i]["CUST_ID"]);
                    PAY_CARD_NBR = Convert.ToString(PUBLIC_HIST.resultTable.Rows[i]["PAY_CARD_NBR"]);
                    PAY_NBR = Convert.ToString(PUBLIC_HIST.resultTable.Rows[i]["PAY_NBR"]);
                    PAY_AMT = Convert.ToDecimal(PUBLIC_HIST.resultTable.Rows[i]["PAY_AMT"]);
                    AUTH_CODE = Convert.ToString(PUBLIC_HIST.resultTable.Rows[i]["AUTH_CODE"]);
                    ERROR_REASON = Convert.ToString(PUBLIC_HIST.resultTable.Rows[i]["ERROR_REASON"]);
                    PAY_DTE = Convert.ToDateTime(PUBLIC_HIST.resultTable.Rows[i]["PAY_DTE"]);
                    PAY_RESULT = Convert.ToString(PUBLIC_HIST.resultTable.Rows[i]["PAY_RESULT"]);

                    //寫出報表明細
                    insert_Report(strfile_name);
                }
                #endregion

                #region 各公共事業筆數
                PHONE_Count = CHK_ERR_PHONE_Count + CHK_OK_PHONE_Count;
                ELECT_Count = CHK_ERR_ELECT_Count + CHK_OK_ELECT_Count;
                ELECT_H_Count = CHK_ERR_ELECT_H_Count + CHK_OK_ELECT_H_Count;
                TWWATER_Count = CHK_ERR_TWWATER_Count + CHK_OK_TWWATER_Count;
                TPWATER_Count = CHK_ERR_TPWATER_Count + CHK_OK_TPWATER_Count;
                #endregion

                #region 將資料寫入報表中
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
        #region 新增至報表
        void insert_Report(string strfile_name)
        {
            REPORT_TABLE.Rows.Add();
            y = REPORT_TABLE.Rows.Count - 1;
            REPORT_TABLE.Rows[y]["PAY_TYPE"] = PAY_TYPE_DESCR;
            REPORT_TABLE.Rows[y]["ID"] = CUST_ID;
            REPORT_TABLE.Rows[y]["PAY_CARD_NBR"] = PAY_CARD_NBR;
            REPORT_TABLE.Rows[y]["PAY_NBR"] = PAY_NBR;
            REPORT_TABLE.Rows[y]["AMT"] = PAY_AMT;
            REPORT_TABLE.Rows[y]["AUTH_CODE"] = AUTH_CODE;
            //REPORT_TABLE.Rows[y]["DOWN_DTE"] = TODAY_PROCESS_DTE.ToShortDateString();
            //REPORT_TABLE.Rows[y]["PAY_DTE"] = PAY_DTE;
            switch (PAY_RESULT)
            {
                case "S000":
                    REPORT_TABLE.Rows[y]["PAY_RESULT"] = "授權成功";
                    REPORT_TABLE.Rows[y]["ERROR_REASON"] = "";
                    break;
                case "0000":
                    REPORT_TABLE.Rows[y]["PAY_RESULT"] = "授權成功";
                    REPORT_TABLE.Rows[y]["ERROR_REASON"] = PAY_RESULT + "-" + ERROR_REASON;
                    break;
                default:
                    REPORT_TABLE.Rows[y]["PAY_RESULT"] = "授權失敗";
                    REPORT_TABLE.Rows[y]["ERROR_REASON"] = PAY_RESULT+"-"+ ERROR_REASON;
                    break;
            }
           
            #region 計數
            if (PAY_RESULT == "S000" || PAY_RESULT == "0000")
            {
                switch (PAY_TYPE)
                {
                    case "0001"://中華電信
                        CHK_OK_PHONE_Count = CHK_OK_PHONE_Count + 1;
                        break;
                    case "0002"://市水
                        CHK_OK_TPWATER_Count = CHK_OK_TPWATER_Count + 1;
                        break;
                    case "0003"://省水
                        CHK_OK_TWWATER_Count = CHK_OK_TWWATER_Count + 1;
                        break;
                    case "0004"://台電
                        CHK_OK_ELECT_Count = CHK_OK_ELECT_Count + 1;
                        break;
                    case "H004"://台電
                        CHK_OK_ELECT_H_Count = CHK_OK_ELECT_H_Count + 1;
                        break;
                }
            }
            else 
            {
                switch (PAY_TYPE)
                {
                    case "0001"://中華電信
                        CHK_ERR_PHONE_Count = CHK_ERR_PHONE_Count + 1;
                        break;
                    case "0002"://市水
                        CHK_ERR_TPWATER_Count = CHK_ERR_TPWATER_Count + 1;
                        break;
                    case "0003"://省水
                        CHK_ERR_TWWATER_Count = CHK_ERR_TWWATER_Count + 1;
                        break;
                    case "0004"://台電
                        CHK_ERR_ELECT_Count = CHK_ERR_ELECT_Count + 1;
                        break;
                    case "H004"://台電
                        CHK_ERR_ELECT_H_Count = CHK_ERR_ELECT_H_Count + 1;
                        break;
                }
            }
            #endregion

            #region 為了出報表,將第一筆解繳日期存起來
            //PB1:例行性作業，解繳日為隔日;其他特殊作業，解繳日為當日
            if (strfile_name == "PB1")   
            {
                if (CHK_OK_PHONE_Count == 1 || CHK_ERR_PHONE_Count == 1)
                {
                    //PAY_DTE_PHONE = PAY_DTE;
                    PAY_DTE_PHONE = Bank_NEXT_PROCESS_DTE;
                }
                if (CHK_OK_ELECT_Count == 1 || CHK_ERR_ELECT_Count == 1)
                {
                    //PAY_DTE_ELECT = PAY_DTE;
                    PAY_DTE_ELECT = Bank_NEXT_PROCESS_DTE;
                }
                if (CHK_OK_ELECT_H_Count == 1 || CHK_ERR_ELECT_H_Count == 1)
                {
                    //PAY_DTE_ELECT_H = PAY_DTE;
                    PAY_DTE_ELECT_H = Bank_NEXT_PROCESS_DTE;
                }
                if (CHK_OK_TWWATER_Count == 1 || CHK_ERR_TWWATER_Count == 1)
                {
                    //PAY_DTE_TWWATER = PAY_DTE;
                    PAY_DTE_TWWATER = Bank_NEXT_PROCESS_DTE;
                }
                if (CHK_OK_TPWATER_Count == 1 || CHK_ERR_TPWATER_Count == 1)
                {
                    //PAY_DTE_TPWATER = PAY_DTE;
                    PAY_DTE_TPWATER = Bank_NEXT_PROCESS_DTE;
                }
            }
            else
            {
                if (CHK_OK_PHONE_Count == 1 || CHK_ERR_PHONE_Count == 1)
                {
                    PAY_DTE_PHONE = PAY_DTE;
                }
                if (CHK_OK_ELECT_Count == 1 || CHK_ERR_ELECT_Count == 1)
                {
                    PAY_DTE_ELECT = PAY_DTE;
                }
                if (CHK_OK_ELECT_H_Count == 1 || CHK_ERR_ELECT_H_Count == 1)
                {
                    PAY_DTE_ELECT_H = PAY_DTE;
                }
                if (CHK_OK_TWWATER_Count == 1 || CHK_ERR_TWWATER_Count == 1)
                {
                    PAY_DTE_TWWATER = PAY_DTE;
                }
                if (CHK_OK_TPWATER_Count == 1 || CHK_ERR_TPWATER_Count == 1)
                {
                    PAY_DTE_TPWATER = PAY_DTE;
                }
            }

            
            #endregion
        }
        #endregion

        #region 寫出至報表
        void writeReport(DataTable inTable)
        {
            //排序
            inTable.DefaultView.Sort = "AUTH_CODE asc,PAY_TYPE asc,ERROR_REASON asc";
            inTable = inTable.DefaultView.ToTable();

            CMCRPT001 CMCRPT001 = new CMCRPT001();
            //設定特殊欄位資料
            ArrayList alSumData = new ArrayList();

            alSumData.Add(new ExcelFactory.ListItem("#RPT_BANK_NAME", strBank_name));
            alSumData.Add(new ExcelFactory.ListItem("#RPT_DATE", TODAY_PROCESS_DTE.ToShortDateString()));
            if (PHONE_Count > 0)   //表示今天有中華電信
            {
                alSumData.Add(new ExcelFactory.ListItem("#DOWN_DTE_PHONE", TODAY_PROCESS_DTE.ToShortDateString()));
                alSumData.Add(new ExcelFactory.ListItem("#PAY_DTE_PHONE", PAY_DTE_PHONE.ToShortDateString()));
                alSumData.Add(new ExcelFactory.ListItem("#TOTAL_CNT_PHONE", PHONE_Count.ToString() + "筆"));
                alSumData.Add(new ExcelFactory.ListItem("#SUCC_CNT_PHONE", CHK_OK_PHONE_Count.ToString() + "筆"));
                alSumData.Add(new ExcelFactory.ListItem("#FAIL_CNT_PHONE", CHK_ERR_PHONE_Count.ToString() + "筆"));
            }
            else
            {
                alSumData.Add(new ExcelFactory.ListItem("#DOWN_DTE_PHONE", ""));
                alSumData.Add(new ExcelFactory.ListItem("#PAY_DTE_PHONE", ""));
                alSumData.Add(new ExcelFactory.ListItem("#TOTAL_CNT_PHONE", "0 筆"));
                alSumData.Add(new ExcelFactory.ListItem("#SUCC_CNT_PHONE", "0 筆"));
                alSumData.Add(new ExcelFactory.ListItem("#FAIL_CNT_PHONE", "0 筆"));
            }
            if (ELECT_Count > 0)   //表示今天有台電(一般)
            {
                alSumData.Add(new ExcelFactory.ListItem("#DOWN_DTE_ELECT", TODAY_PROCESS_DTE.ToShortDateString()));
                alSumData.Add(new ExcelFactory.ListItem("#PAY_DTE_ELECT", PAY_DTE_ELECT.ToShortDateString()));
                alSumData.Add(new ExcelFactory.ListItem("#TOTAL_CNT_ELECT", ELECT_Count.ToString() + "筆"));
                alSumData.Add(new ExcelFactory.ListItem("#SUCC_CNT_ELECT", CHK_OK_ELECT_Count.ToString() + "筆"));
                alSumData.Add(new ExcelFactory.ListItem("#FAIL_CNT_ELECT", CHK_ERR_ELECT_Count.ToString() + "筆"));
            }
            else
            {
                alSumData.Add(new ExcelFactory.ListItem("#DOWN_DTE_ELECT", ""));
                alSumData.Add(new ExcelFactory.ListItem("#PAY_DTE_ELECT", ""));
                alSumData.Add(new ExcelFactory.ListItem("#TOTAL_CNT_ELECT", "0 筆"));
                alSumData.Add(new ExcelFactory.ListItem("#SUCC_CNT_ELECT", "0 筆"));
                alSumData.Add(new ExcelFactory.ListItem("#FAIL_CNT_ELECT", "0 筆"));
            }
            if (ELECT_H_Count > 0)   //表示今天有台電(高壓)
            {
                alSumData.Add(new ExcelFactory.ListItem("#DOWN_DTE_ELECT_H", TODAY_PROCESS_DTE.ToShortDateString()));
                alSumData.Add(new ExcelFactory.ListItem("#PAY_DTE_ELECT_H", PAY_DTE_ELECT_H.ToShortDateString()));
                alSumData.Add(new ExcelFactory.ListItem("#TOTAL_CNT_ELECT_H", ELECT_H_Count.ToString() + "筆"));
                alSumData.Add(new ExcelFactory.ListItem("#SUCC_CNT_ELECT_H", CHK_OK_ELECT_H_Count.ToString() + "筆"));
                alSumData.Add(new ExcelFactory.ListItem("#FAIL_CNT_ELECT_H", CHK_ERR_ELECT_H_Count.ToString() + "筆"));
            }
            else
            {
                alSumData.Add(new ExcelFactory.ListItem("#DOWN_DTE_ELECT_H", ""));
                alSumData.Add(new ExcelFactory.ListItem("#PAY_DTE_ELECT_H", ""));
                alSumData.Add(new ExcelFactory.ListItem("#TOTAL_CNT_ELECT_H", "0 筆"));
                alSumData.Add(new ExcelFactory.ListItem("#SUCC_CNT_ELECT_H", "0 筆"));
                alSumData.Add(new ExcelFactory.ListItem("#FAIL_CNT_ELECT_H", "0 筆"));
            }
            if (TWWATER_Count > 0)   //表示今天有省水
            {
                alSumData.Add(new ExcelFactory.ListItem("#DOWN_DTE_TWWATER", TODAY_PROCESS_DTE.ToShortDateString()));
                alSumData.Add(new ExcelFactory.ListItem("#PAY_DTE_TWWATER", PAY_DTE_TWWATER.ToShortDateString()));
                alSumData.Add(new ExcelFactory.ListItem("#TOTAL_CNT_TWWATER", TWWATER_Count.ToString() + "筆"));
                alSumData.Add(new ExcelFactory.ListItem("#SUCC_CNT_TWWATER", CHK_OK_TWWATER_Count.ToString() + "筆"));
                alSumData.Add(new ExcelFactory.ListItem("#FAIL_CNT_TWWATER", CHK_ERR_TWWATER_Count.ToString() + "筆"));
            }
            else
            {
                alSumData.Add(new ExcelFactory.ListItem("#DOWN_DTE_TWWATER", ""));
                alSumData.Add(new ExcelFactory.ListItem("#PAY_DTE_TWWATER", ""));
                alSumData.Add(new ExcelFactory.ListItem("#TOTAL_CNT_TWWATER", "0 筆"));
                alSumData.Add(new ExcelFactory.ListItem("#SUCC_CNT_TWWATER", "0 筆"));
                alSumData.Add(new ExcelFactory.ListItem("#FAIL_CNT_TWWATER", "0 筆"));
            }
            if (TPWATER_Count > 0)   //表示今天有市水
            {
                alSumData.Add(new ExcelFactory.ListItem("#DOWN_DTE_TPWATER", TODAY_PROCESS_DTE.ToShortDateString()));
                alSumData.Add(new ExcelFactory.ListItem("#PAY_DTE_TPWATER", PAY_DTE_TPWATER.ToShortDateString()));
                alSumData.Add(new ExcelFactory.ListItem("#TOTAL_CNT_TPWATER", TPWATER_Count.ToString() + "筆"));
                alSumData.Add(new ExcelFactory.ListItem("#SUCC_CNT_TPWATER", CHK_OK_TPWATER_Count.ToString() + "筆"));
                alSumData.Add(new ExcelFactory.ListItem("#FAIL_CNT_TPWATER", CHK_ERR_TPWATER_Count.ToString() + "筆"));
            }
            else
            {
                alSumData.Add(new ExcelFactory.ListItem("#DOWN_DTE_TPWATER", ""));
                alSumData.Add(new ExcelFactory.ListItem("#PAY_DTE_TPWATER", ""));
                alSumData.Add(new ExcelFactory.ListItem("#TOTAL_CNT_TPWATER", "0 筆"));
                alSumData.Add(new ExcelFactory.ListItem("#SUCC_CNT_TPWATER", "0 筆"));
                alSumData.Add(new ExcelFactory.ListItem("#FAIL_CNT_TPWATER", "0 筆"));
            }

            //設定合併欄位資料
            ArrayList alMegData = new ArrayList();

            //產出報表
            CMCRPT001.Output(inTable, alSumData, alMegData, "PBRAUP001(公用事業授權一覽表)", "PBRAUP001", "Sheet1", "Sheet1", TODAY_PROCESS_DTE);

        }
        #endregion

        #region Display
        void writeDisplay()
        {
            logger.strJobID = "00002";
            //擷取今日PUBLIC_HIST資料
            logger.strTBL_NAME = "PUBLIC_HIST";
            logger.strTBL_ACCESS = "Q";
            logger.intTBL_COUNT = PUBLIC_HIST_Query_Count;
            logger.writeCounter();

            //送授權筆數(內部檢核成功)
            logger.strTBL_NAME = "PUBLIC_HIST";
            logger.strTBL_ACCESS = "Q";
            logger.strMEMO = "CHECK OK";
            logger.intTBL_COUNT = CHK_OK_Count;
            logger.writeCounter();

            //內部檢核失敗
            logger.strTBL_NAME = "PUBLIC_HIST";
            logger.strTBL_ACCESS = "Q";
            logger.strMEMO = "CHECK ERROR";
            logger.intTBL_COUNT = CHK_ERR_Count;
            logger.writeCounter();

            //比對失敗
            logger.strTBL_NAME = "COLA_UPD_AUTH";
            logger.strTBL_ACCESS = "Q";
            logger.strMEMO = "match error";
            logger.intTBL_COUNT = NOMATCH_CNT;
            logger.writeCounter();


            //中華電信內部檢核失敗
            logger.strTBL_NAME = "PUBLIC_HIST";
            logger.strTBL_ACCESS = "Q";
            logger.strMEMO = "PHONE ERROR";
            logger.intTBL_COUNT = CHK_ERR_PHONE_Count;
            logger.writeCounter();

            //台電內部檢核失敗(一般)
            logger.strTBL_NAME = "PUBLIC_HIST";
            logger.strTBL_ACCESS = "Q";
            logger.strMEMO = "ELECT ERROR";
            logger.intTBL_COUNT = CHK_ERR_ELECT_Count;
            logger.writeCounter();

            //台電內部檢核失敗(高壓)
            logger.strTBL_NAME = "PUBLIC_HIST";
            logger.strTBL_ACCESS = "Q";
            logger.strMEMO = "ELECT_H ERROR";
            logger.intTBL_COUNT = CHK_ERR_ELECT_H_Count;
            logger.writeCounter();

            //省水內部檢核失敗
            logger.strTBL_NAME = "PUBLIC_HIST";
            logger.strTBL_ACCESS = "Q";
            logger.strMEMO = "TWATER ERROR";
            logger.intTBL_COUNT = CHK_ERR_TWWATER_Count;
            logger.writeCounter();

            //授權成功
            logger.strTBL_NAME = "COLA_TX_AUTH_TODAY";
            logger.strTBL_ACCESS = "I";
            logger.strMEMO = "APPROVE";
            logger.intTBL_COUNT = AUTH_APPROVE_Count;
            logger.writeCounter();

            //授權失敗
            logger.strTBL_NAME = "COLA_TX_AUTH_TODAY";
            logger.strTBL_ACCESS = "I";
            logger.strMEMO = "REJECT";
            logger.intTBL_COUNT = AUTH_REJECT_Count;
            logger.writeCounter();

            logger.DisplayCounter(Cybersoft.Coca.Log.LevelEnum.Info);
        }
        #endregion

    }
}

