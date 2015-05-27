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
    /// 產生陽明山瓦斯約定檔
    /// 執行週期：每月1日撈取上月份申請/終止之資料，並報送至公用事業機構
    /// </summary>
    public class PBBYMC001
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
        
        #endregion

        #region 設定檔案編碼
        Encoding BIG5 = Encoding.GetEncoding("big5");
        #endregion

        #region 副程式
        CMCNBR001 CMCNBR001 = new CMCNBR001();
        #endregion

        #region 宣告共用變數

        //批次日期
        DateTime BANK_PREV_PROCESS_DTE = new DateTime();
        DateTime TODAY_PROCESS_DTE = new DateTime();
        //String LAST_MONTH_START = "";

        //筆數&金額
        int PUBLIC_APPLY_Update_Count = 0;
        int PUBLIC_APPLY_Query_Count = 0;
        int i = 0;
        int YMGAS_TXT_Count = 0;

        //陽明山瓦斯約定檔欄位
        String[] YMGAS_CHGOUT = null; 
        String CHANGE_TYPE = "0";
        String CHANGE_DTE = "";

        //PUBLIC_APPLY欄位
        String PAY_CARD_NBR = "";
        String PAY_ACCT_NBR = "";
        String PAY_TYPE = "";
        String PAY_NBR = "";
        String SEQ = "";
        String EXPIR_DTE = "";
        String APPLY_DTE = "";
        String REPLY_FLAG = "";
                           
        //檔案長度
        const int intDataLength = 85;  

        #endregion

        #region 宣告檔案路徑

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
            logger.strJOBNAME = "PBBYMC001";
            logger.dtRunDate = DateTime.Now;
            #endregion

            try
            {
                #region 取得系統資訊(前次批次日、本日批次日)
                CMCSYS001 SYSINF = new CMCSYS001();
                SYSINF.strJobID = JobID;
                SYSINF.strJobName = "PBBYMC001";
                String SYSINF_RC = SYSINF.getSYSINF();
                BANK_PREV_PROCESS_DTE = SYSINF.datetimeBANK_PREV_PROCESS_DTE;
                TODAY_PROCESS_DTE = SYSINF.datetimeTODAY_PROCESS_DTE;
                //LAST_MONTH_START = System.DateTime.Now.AddMonths(-1).ToString("yyyyMMdd");
                //判斷下一個營業日是否為月底日
                //if (NEXT_PROCESS_DTE.ToString("dd") != DateTime.DaysInMonth(NEXT_PROCESS_DTE.Year, NEXT_PROCESS_DTE.Month).ToString())
                //{
                //    logger.strJobQueue = "下一個營業日 (" + NEXT_PROCESS_DTE.ToString("yyyy/MM/dd") + ") 非月底日(" + DateTime.DaysInMonth(NEXT_PROCESS_DTE.Year, NEXT_PROCESS_DTE.Month).ToString() + "), 故不執行";
                //    logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                //    return "B0000:" + logger.strJobQueue;
                //}
                #endregion

                #region 宣告檔案路徑
                Cybersoft.Base.CMCURL001 CMCURL = new CMCURL001();
                //寫出檔案路徑
                FILE_PATH = CMCURL.getPATH("YMGAS_CHGOUT");

                if (FILE_PATH == "")
                {
                    logger.strJobQueue = "路徑取得錯誤!!! YMGAS_CHGOUT路徑為 " + FILE_PATH;
                    logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                    return "B0099:" + logger.strJobQueue;
                }
                else
                {
                    FILE_PATH = FILE_PATH.Replace("yyyymmdd", BANK_PREV_PROCESS_DTE.ToString("yyyyMMdd"));
                    logger.strJobQueue = " YMGAS_CHGOUT路徑為 " + FILE_PATH;
                    logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                }
                #endregion

                #region 擷取今日需送約定檔之陽明山瓦斯資料
                PUBLIC_APPLY.init();
                PUBLIC_APPLY.strWherePAY_TYPE = "0008";
                PUBLIC_APPLY.strWhereREPLY_FLAG = " ";
                PUBLIC_APPLY_RC = PUBLIC_APPLY.query();
                switch (PUBLIC_APPLY_RC)
                {
                    case "S0000": //查詢成功
                        PUBLIC_APPLY_Query_Count = PUBLIC_APPLY.resultTable.Rows.Count;
                        YMGAS_CHGOUT = new string[PUBLIC_APPLY_Query_Count];
                        break;

                    case "F0023": //無需處理資料
                        logger.strJobQueue = "今日無陽明山瓦斯約定檔需產生;";
                        logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                        break;

                    default: //資料庫錯誤
                        logger.strJobQueue = "查詢PUBLIC_APPLY 資料錯誤:" + PUBLIC_APPLY_RC;
                        logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                        return "B0016:" + logger.strJobQueue;
                }
                #endregion

                for (i = 0; i < PUBLIC_APPLY.resultTable.Rows.Count; i++)
                {

                    DataTable InfData_DataTable = new DataTable();

                    #region 讀取PUBLIC_APPLY

                    SEQ = Convert.ToString(PUBLIC_APPLY.resultTable.Rows[i]["SEQ"]);
                    PAY_TYPE = Convert.ToString(PUBLIC_APPLY.resultTable.Rows[i]["PAY_TYPE"]);
                    PAY_CARD_NBR = Convert.ToString(PUBLIC_APPLY.resultTable.Rows[i]["PAY_CARD_NBR"]);
                    PAY_ACCT_NBR = Convert.ToString(PUBLIC_APPLY.resultTable.Rows[i]["PAY_ACCT_NBR"]);
                    PAY_NBR = Convert.ToString(PUBLIC_APPLY.resultTable.Rows[i]["PAY_NBR"]);
                    REPLY_FLAG = Convert.ToString(PUBLIC_APPLY.resultTable.Rows[i]["REPLY_FLAG"]);
                    APPLY_DTE = Convert.ToString(PUBLIC_APPLY.resultTable.Rows[i]["APPLY_DTE"]);
                    EXPIR_DTE = Convert.ToString(PUBLIC_APPLY.resultTable.Rows[i]["EXPIR_DTE"]);

                    #endregion

                    #region 判斷異動種類和組明細
                    //判斷異動種類：
                    //APPLY_DTE >= TODAY → 新增，代碼"1" 
                    //EXPIR_DTE == TODAY → 終止，代碼"2"
                    //if ((Convert.ToInt32(APPLY_DTE) >= Convert.ToInt32(LAST_MONTH_START)) &&
                    //    (Convert.ToInt32(APPLY_DTE) <= Convert.ToInt32(TODAY_PROCESS_DTE.ToString("yyyyMMdd"))))
                    //{
                    //    CHANGE_TYPE = "1";
                    //    CHANGE_DTE = (DateTime.ParseExact(APPLY_DTE, "yyyyMMdd", null)).AddYears(-1911).ToString("yyyyMMdd").Substring(2, 6);
                    //}
                    //else if ((Convert.ToInt32(EXPIR_DTE) >= Convert.ToInt32(LAST_MONTH_START)) &&
                    //(Convert.ToInt32(EXPIR_DTE) <= Convert.ToInt32(TODAY_PROCESS_DTE.ToString("yyyyMMdd"))))
                    //{
                    //    CHANGE_TYPE = "2";
                    //    CHANGE_DTE = (DateTime.ParseExact(EXPIR_DTE, "yyyyMMdd", null)).AddYears(-1911).ToString("yyyyMMdd").Substring(2, 6);
                    //}
                    //else
                    //{
                    //    logger.strJobQueue = "第" + (i + 1) + "筆資料有誤, PAY_NBR = " + PAY_NBR + ", APPLY_DTE = " + APPLY_DTE + ", EXPIR_DTE = " + EXPIR_DTE + ", 請確認!!";
                    //    logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                    //    return "B0016:" + logger.strJobQueue;
                    //}

                    if (Convert.ToInt32(EXPIR_DTE) <= Convert.ToInt32(TODAY_PROCESS_DTE.ToString("yyyyMMdd")))
                    {
                        CHANGE_TYPE = "2";
                        CHANGE_DTE = (DateTime.ParseExact(EXPIR_DTE, "yyyyMMdd", null)).AddYears(-1911).ToString("yyyyMMdd").Substring(2, 6);
                    }
                    else
                    {
                        if (Convert.ToInt32(APPLY_DTE) <= Convert.ToInt32(TODAY_PROCESS_DTE.ToString("yyyyMMdd")))
                        {
                            CHANGE_TYPE = "1";
                            CHANGE_DTE = (DateTime.ParseExact(APPLY_DTE, "yyyyMMdd", null)).AddYears(-1911).ToString("yyyyMMdd").Substring(2, 6);
                        }
                        else
                        {
                            logger.strJobQueue = "第" + (i + 1) + "筆資料有誤, PAY_NBR = " + PAY_NBR + ", APPLY_DTE = " + APPLY_DTE + ", EXPIR_DTE = " + EXPIR_DTE + ", 請確認!!";
                            logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                            return "B0016:" + logger.strJobQueue;
                        }
                    }

                    //組明細
                    YMGAS_CHGOUT[i] = CHANGE_TYPE + CHANGE_DTE + PAY_ACCT_NBR + PAY_NBR.Substring(2, 8) + new string(' ', 51);
                    //將變數值還原
                    CHANGE_TYPE = "0";

                    #endregion

                    #region 更新PUBLIC_APPLY
                    PUBLIC_APPLY.init();
                    PUBLIC_APPLY.strREPLY_FLAG = "Y";
                    PUBLIC_APPLY.strREPLY_DTE = TODAY_PROCESS_DTE.ToString("yyyyMMdd");
                    //PUBLIC_APPLY.strMNT_USER = "PBBYMC001";
                    //PUBLIC_APPLY.datetimeMNT_DT = NEXT_PROCESS_DTE;
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
                if (PUBLIC_APPLY_Update_Count != PUBLIC_APPLY.resultTable.Rows.Count)
                {
                    logger.strJobQueue = "更新 PUBLIC_APPLY 時筆數異常,原筆數 : " + PUBLIC_APPLY.resultTable.Rows.Count + " 實際筆數: " + PUBLIC_APPLY_Update_Count;
                    logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                    return "B0012" + logger.strJobQueue;
                }

                logger.strJobQueue = "更新 PUBLIC_APPLY 成功筆數 =" + PUBLIC_APPLY_Update_Count;
                logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);

                #endregion

                #region 產生約定檔 陣列 --> 108Z_yyyymmdd_5178_001

                //設定產出檔案名稱                
                strOutFileName = FILE_PATH;
                FileStream fsOutFile = new FileStream(strOutFileName, FileMode.Create, FileAccess.Write, FileShare.None);
                using (StreamWriter srOutFile = new StreamWriter(fsOutFile, BIG5))
                {
                    //逐筆寫出資料
                    for (int k = 0; k < PUBLIC_APPLY.resultTable.Rows.Count; k++)
                    {
                        srOutFile.Write(YMGAS_CHGOUT[k]);
                        srOutFile.Write("\r\n");
                        srOutFile.Flush();
                        YMGAS_TXT_Count = YMGAS_TXT_Count + 1;
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
            //擷取陽明山瓦斯需產出約定檔的資料
            logger.strTBL_NAME = "PUBLIC_APPLY";
            logger.strTBL_ACCESS = "Q";
            logger.intTBL_COUNT = PUBLIC_APPLY_Query_Count;
            logger.writeCounter();

            //陽明山瓦斯約定檔筆數(含頭尾)
            logger.strTBL_NAME = "YMGAS_CHGOUT";
            logger.strTBL_ACCESS = "I";
            logger.intTBL_COUNT = YMGAS_TXT_Count;
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

