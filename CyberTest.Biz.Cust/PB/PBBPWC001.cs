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
    /// 產生北水約定檔
    /// </summary>
    public class PBBPWC001
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
        DateTime SYS_PROCESS_DTE = new DateTime();
        String SYS_YYYMMDD = "";
        String SYS_YYYYMMDD = "";
        String PROCESS_START_DTE = "";

        //筆數&金額
        int PUBLIC_APPLY_Update_Count = 0;
        int PUBLIC_APPLY_Query_Count = 0;
        int i = 0;
        int WATER_TXT_Count = 0;

        //北市水約定檔欄位
        String[] WATER_CHGOUT = null;
        String TPEWATER_CHGOUT_H = "";
        String CHANGE_TYPE = "";
        String UNIT_CODE = "a09";
        int TRANS_CNT = 0;
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
        String FILE_PATH_H = "";
        String FILE_PATH_D = "";
        
        #endregion

        static readonly Cybersoft.Coca.Log.Logger logger = Cybersoft.Log.LogManager<Cybersoft.Coca.Log.Logger>.GetLogger(System.Reflection.MethodInfo.GetCurrentMethod().ReflectedType.Name);
        public string RUN()
        {
            #region 準備MSGLG.SYSOUT CLASS
            logger.strJobID = JobID;
            logger.strJOBNAME = "PBBPWC001";
            logger.dtRunDate = DateTime.Now;
            #endregion

            try
            {
                #region 取得系統資訊(下次批次日)
                CMCSYS001 SYSINF = new CMCSYS001();
                SYSINF.strJobID = JobID;
                SYSINF.strJobName = "PBBPWC001";
                String SYSINF_RC = SYSINF.getSYSINF();
                SYS_PROCESS_DTE = System.DateTime.Now;
                //判斷下一營業日是否為星期三
                //if (SYS_PROCESS_DTE.DayOfWeek.ToString("d") == "2")
                //{
                //    logger.strJobQueue = "下一營業日 (" + SYS_PROCESS_DTE.ToString("yyyy/MM/dd") + ") 非星期三, 故不執行";
                //    logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                //    return "B0000:" + logger.strJobQueue;
                //}
                SYS_YYYYMMDD = SYS_PROCESS_DTE.ToString("yyyyMMdd");
                SYS_YYYMMDD = SYS_PROCESS_DTE.AddYears(-1911).ToString("yyyyMMdd").Substring(1, 7);
                PROCESS_START_DTE = System.DateTime.Now.AddDays(-7).ToString("yyyyMMdd");
                #endregion

                #region 宣告檔案路徑
                Cybersoft.Base.CMCURL001 CMCURL = new CMCURL001();
                //寫出檔案路徑(Header)
                FILE_PATH_H = CMCURL.getPATH("TPEWATER_CHGOUT_H");

                if (FILE_PATH_H == "")
                {
                    logger.strJobQueue = "路徑取得錯誤!!! TPEWATER_CHGOUT_H路徑為 " + FILE_PATH_H;
                    logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                    return "B0099:" + logger.strJobQueue;
                }
                else
                {
                    FILE_PATH_H = FILE_PATH_H.Replace("yyyymmdd", SYS_PROCESS_DTE.ToString("yyyyMMdd"));
                    logger.strJobQueue = " TPEWATER_CHGOUT_H路徑為 " + FILE_PATH_H;
                    logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                }
                //寫出檔案路徑(Body)
                FILE_PATH_D = CMCURL.getPATH("TPEWATER_CHGOUT_D");

                if (FILE_PATH_D == "")
                {
                    logger.strJobQueue = "路徑取得錯誤!!! TPEWATER_CHGOUT_D路徑為 " + FILE_PATH_D;
                    logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                    return "B0099:" + logger.strJobQueue;
                }
                else
                {
                    FILE_PATH_D = FILE_PATH_D.Replace("yyyymmdd", SYS_PROCESS_DTE.ToString("yyyyMMdd"));
                    logger.strJobQueue = " TPEWATER_CHGOUT_D路徑為 " + FILE_PATH_D;
                    logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                }
                #endregion

                #region 擷取今日需送約定檔之北水資料
                PUBLIC_APPLY.init();
                PUBLIC_APPLY.strWherePAY_TYPE = "0002";
                PUBLIC_APPLY.strWhereREPLY_FLAG = " ";
                PUBLIC_APPLY_RC = PUBLIC_APPLY.query();
                switch (PUBLIC_APPLY_RC)
                {
                    case "S0000": //查詢成功
                        PUBLIC_APPLY_Query_Count = PUBLIC_APPLY.resultTable.Rows.Count;
                        WATER_CHGOUT = new string[PUBLIC_APPLY_Query_Count];
                        break;

                    case "F0023": //無需處理資料
                        logger.strJobQueue = "今日無北水約定檔需產生;";
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
                    //EXPIR_DTE == TODAY → 終止，代碼"3"
                    //其他 → 異動，代碼"2"
                    //if (string.Compare(APPLY_DTE, SYS_YYYYMMDD) >= 0)
                    //{
                    //    CHANGE_TYPE = "1";
                    //}
                    //else if (string.Compare(EXPIR_DTE, SYS_YYYYMMDD) == 0)
                    //{
                    //    CHANGE_TYPE = "3";
                    //}
                    //else
                    //{
                    //    CHANGE_TYPE = "2";
                    //}

                    if ((Convert.ToInt32(APPLY_DTE) >= Convert.ToInt32(PROCESS_START_DTE)) &&
                        (Convert.ToInt32(APPLY_DTE) <= Convert.ToInt32(SYS_YYYYMMDD)))
                    {
                        CHANGE_TYPE = "1";
                    }
                    else if ((Convert.ToInt32(EXPIR_DTE) >= Convert.ToInt32(PROCESS_START_DTE)) &&
                    (Convert.ToInt32(EXPIR_DTE) <= Convert.ToInt32(SYS_YYYYMMDD)))
                    {
                        CHANGE_TYPE = "3";
                    }
                    else
                    {
                        CHANGE_TYPE = "2";
                    }

                    //將申請日轉成yyyMMdd格式
                    CHANGE_DTE = DateTime.ParseExact(APPLY_DTE, "yyyyMMdd", null).AddYears(-1911).ToString("yyyyMMdd").Substring(1, 7);

                    //組明細
                    WATER_CHGOUT[i] = UNIT_CODE.PadRight(5, ' ') + SYS_YYYMMDD + PAY_NBR.PadRight(10, ' ') +
                                      (UNIT_CODE + PAY_ACCT_NBR).PadRight(30, ' ') + CHANGE_DTE + CHANGE_TYPE;

                    #endregion

                    #region 更新PUBLIC_APPLY
                    PUBLIC_APPLY.init();
                    PUBLIC_APPLY.strREPLY_FLAG = "Y";
                    PUBLIC_APPLY.strREPLY_DTE = SYS_YYYYMMDD;
                    PUBLIC_APPLY.strMNT_USER = "PBBPWC001";
                    PUBLIC_APPLY.datetimeMNT_DT = SYS_PROCESS_DTE;
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

                //組Header檔
                TRANS_CNT = PUBLIC_APPLY.resultTable.Rows.Count;
                TPEWATER_CHGOUT_H = UNIT_CODE.PadRight(5, ' ') + SYS_YYYMMDD + TRANS_CNT.ToString("000000");

                #region 產生約定檔 陣列 --> tra09mmdd_3.h.yyyymmdd & tra09mmdd_3.d.yyyymmdd

                //設定產出檔案名稱(Header)  
                strOutFileName = FILE_PATH_H;
                FileStream fsOutFile_h = new FileStream(strOutFileName, FileMode.Create, FileAccess.Write, FileShare.None);
                using (StreamWriter srOutFile_h = new StreamWriter(fsOutFile_h, BIG5))
                {
                    //寫出檔頭
                    srOutFile_h.Write(TPEWATER_CHGOUT_H);
                    srOutFile_h.Write("\r\n");
                    srOutFile_h.Flush();
                    srOutFile_h.Close();
                }
                fsOutFile_h.Close();

                //設定產出檔案名稱(Body)
                strOutFileName = FILE_PATH_D;
                FileStream fsOutFile_d = new FileStream(strOutFileName, FileMode.Create, FileAccess.Write, FileShare.None);
                using (StreamWriter srOutFile_d = new StreamWriter(fsOutFile_d, BIG5))
                {
                    //逐筆寫出資料
                    for (int k = 0; k < PUBLIC_APPLY.resultTable.Rows.Count; k++)
                    {
                        srOutFile_d.Write(WATER_CHGOUT[k]);
                        srOutFile_d.Write("\r\n");
                        srOutFile_d.Flush();
                        WATER_TXT_Count = WATER_TXT_Count + 1;
                    }
                    srOutFile_d.Close();
                }
                fsOutFile_d.Close();


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
            //擷取北水需產出約定檔的資料
            logger.strTBL_NAME = "PUBLIC_APPLY";
            logger.strTBL_ACCESS = "Q";
            logger.intTBL_COUNT = PUBLIC_APPLY_Query_Count;
            logger.writeCounter();

            //北水約定檔筆數(僅BODY)
            logger.strTBL_NAME = "WATER_CHGOUT";
            logger.strTBL_ACCESS = "I";
            logger.intTBL_COUNT = WATER_TXT_Count;
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

