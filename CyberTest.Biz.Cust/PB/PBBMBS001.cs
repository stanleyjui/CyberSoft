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
using Cybersoft.ExportDocument;
using Cybersoft.Data.DAL.MBS;

namespace Cybersoft.Biz.Cust
{
    /// <summary>
    /// PBBMBS001 產生今日公共事業_商家入扣檔   
    /// 執行時間：每日執行 
    /// </summary>

    public class PBBMBS001
    {
        private string JobID;
        public string strJobID
        {
            get { return JobID; }
            set { JobID = value; }
        }
        private string JOBNAME;
        public string strJOBNAME
        {
            get { return JOBNAME; }
            set { JOBNAME = value; }
        }


        #region 宣告TABLE
        //宣告PUBLIC_HIST
        string PUBLIC_HIST_RC = string.Empty;
        PUBLIC_HISTDao PUBLIC_HIST = new PUBLIC_HISTDao();
        int PUBLIC_HIST_query_cnt = 0;


        //宣告MBS_PAYMENT
        string MBS_PAYMENT_RC = string.Empty;
        MBS_PAYMENTDao MBS_PAYMENT = new MBS_PAYMENTDao();
        DataTable MBS_PAYMENT_DataTable = new DataTable();
        int MBS_PAYMENT_Count_Insert = 0;
        int MBS_PAYMENT_Count_Delete = 0;

        #endregion

        #region 宣告常數
        const string MNT_USER = "PBBMBS001";
        DateTime dtNow = System.DateTime.Now;
        #endregion

        #region 宣告變數
        DateTime TODAY_PROCESS_DTE = new DateTime();
        DateTime NEXT_PROCESS_DTE = new DateTime();
        //double fee = 0;
        #endregion

        #region 宣告共用類別
        static readonly Cybersoft.Coca.Log.Logger logger = Cybersoft.Log.LogManager<Cybersoft.Coca.Log.Logger>.GetLogger(System.Reflection.MethodInfo.GetCurrentMethod().ReflectedType.Name);
        #endregion

        //========================================================================================================
        #region MAIN  
        public string RUN()
        {
            #region //準備MSGLG.SYSOUT CLASS
            logger.strJobID = JobID;
            logger.strJOBNAME = MNT_USER;
            logger.dtRunDate = DateTime.Now;
            #endregion

            try
            {
                #region  取得系統資訊(前次批次日、本日批次日)
                CMCSYS001 SYSINF = new CMCSYS001();
                SYSINF.strJobID = JobID;
                SYSINF.strJobName = strJOBNAME;
                String SYSINF_RC = SYSINF.getSYSINF();
                TODAY_PROCESS_DTE = SYSINF.datetimeTODAY_PROCESS_DTE;
                NEXT_PROCESS_DTE = SYSINF.datetimeBANK_NEXT_PROCESS_DTE;
                #endregion

                #region 初始化 DataTable
                PUBLIC_HIST.table_define();
                MBS_PAYMENT.table_define();
                #endregion

                #region RERUN機制 刪除MBS_PAYMENT資料
                MBS_PAYMENT.init();
                MBS_PAYMENT.resultTable.Clear();
                // 條件
                MBS_PAYMENT.strWherePAY_DATE = TODAY_PROCESS_DTE.ToString("yyyy/MM/dd");
                MBS_PAYMENT.strWherePAY_SOURCE = "Y";
                MBS_PAYMENT_RC = MBS_PAYMENT.delete();
                switch (MBS_PAYMENT_RC.Substring(0, 5))
                {
                    case "S0000":  //修改成功
                        break;
                    default:       //資料庫錯誤
                        logger.strJobQueue = "[MBS_PAYMENT.delete()]：" + MBS_PAYMENT_RC;
                        logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                        return "B0016:" + logger.strJobQueue;
                }
                MBS_PAYMENT_Count_Delete = MBS_PAYMENT.intDelCnt;
                logger.strJobQueue = "刪除 MBS_PAYMENT 共" + MBS_PAYMENT_Count_Delete + "筆。";
                logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                #endregion

                #region 取得今日公用事業代扣資料
                PUBLIC_HIST.init();
                PUBLIC_HIST.strWhereTRANS_DTE = TODAY_PROCESS_DTE.ToString("yyyyMMdd");
                PUBLIC_HIST.strWherePAY_RESULT = "0000";
                PUBLIC_HIST_RC = PUBLIC_HIST.query_for_MBS_PAYMENT();
                switch (PUBLIC_HIST_RC)
                {
                    case "S0000": //查詢成功
                        PUBLIC_HIST_query_cnt = PUBLIC_HIST.resultTable.Rows.Count;
                        break;
                    case "F0023": //查無資料                       
                        logger.strJobQueue = "本日無公用事業代扣資料!!";
                        logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                        return "B0000" + logger.strJobQueue;
                    default: //資料庫錯誤
                        logger.strJobQueue = "PUBLIC_HIST.query_for_MBS_PAYMENT 錯誤 *** " + PUBLIC_HIST_RC;
                        logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                        return "B0016" + logger.strJobQueue;
                }
                #endregion

                #region 組 MBS_PAYMENT
                for (int i = 0; i < PUBLIC_HIST.resultTable.Rows.Count; i++)
                {
                   
                    MBS_PAYMENT.init();
                    MBS_PAYMENT.resultTable.Clear();
                    MBS_PAYMENT.initInsert_row();
                    //特店代號
                    MBS_PAYMENT.strPAY_MER_NO = PUBLIC_HIST.resultTable.Rows[i]["UT_MER_NO"].ToString();
                    //分行代碼
                    MBS_PAYMENT.strPAY_BRANCH = "";
                    //存款別
                    MBS_PAYMENT.strPAY_PBCK = "01";
                    //特店付款日期
                    MBS_PAYMENT.strPAY_DATE = NEXT_PROCESS_DTE.ToString("yyyy/MM/dd");
                    //筆數
                    decimal decCNT = Convert.ToDecimal(PUBLIC_HIST.resultTable.Rows[i]["CNT"]);
                    MBS_PAYMENT.decPAY_CNT = decCNT;
                    //手續資費
                    switch(PUBLIC_HIST.resultTable.Rows[i]["PAY_TYPE"].ToString().Trim())
                    {
                        case "0002":
                        case "0003":
                        case "0004":
                            //fee = 3;
                            break;

                        case "0001":
                        case "0008":
                        case "0016":
                        case "0035":
                            //fee = 2.5;
                            break;

                        default:
                            //fee = 0;
                            break;
                    }
                    //金額
                    decimal decPAY_AMT_TOT = Convert.ToDecimal(PUBLIC_HIST.resultTable.Rows[i]["PAY_AMT_TOT"]);
                    MBS_PAYMENT.decPAY_AMT = decPAY_AMT_TOT;
                    //if (decPAY_AMT_TOT > 0)
                    if (PUBLIC_HIST.resultTable.Rows[i]["USER_FIELD_1"].ToString().Trim() !="R")
                    {
                        //交易別(付款正數填31、付款負數填41)
                        MBS_PAYMENT.strPAY_TRTYPE = "31";
                        //交易代號(付款正數填6205、付款負數填6206)
                        MBS_PAYMENT.strPAY_TRCODE = "6205";
                        //存提區分(付款正數填02、付款負數填05)
                        MBS_PAYMENT.strPAY_DW = "02";
                        //資料檔名(付款正數填XD03、付款負數填XD01)
                        MBS_PAYMENT.strPAY_FILEID = "XD03";
                    }
                    else
                    {
                        MBS_PAYMENT.strPAY_TRTYPE = "41";
                        MBS_PAYMENT.strPAY_TRCODE = "6206";
                        MBS_PAYMENT.strPAY_DW = "05";
                        MBS_PAYMENT.strPAY_FILEID = "XD01";
                    }
                    
                    //金資手續費
                    //MBS_PAYMENT.decPAY_COMM = (decCNT * Convert.ToDecimal(fee));
                    MBS_PAYMENT.decPAY_COMM = 0;
                    //發卡行回佣
                    MBS_PAYMENT.decPAY_INT_FEE = 0;
                    //付款淨額(交易金額 – 金資手續費)
                    //MBS_PAYMENT.decPAY_NET = decPAY_AMT_TOT - (decCNT * Convert.ToDecimal(fee));
                    MBS_PAYMENT.decPAY_NET = decPAY_AMT_TOT;
                    //調整金額
                    MBS_PAYMENT.decPAY_ADJ_AMT = 0;
                    //調整手續費
                    MBS_PAYMENT.decPAY_ADJ_COMM = 0;
                    //調整發卡行回佣
                    MBS_PAYMENT.decPAY_ADJ_FEE = 0;
                    //調整淨額
                    MBS_PAYMENT.decPAY_ADJ_NET = 0;
                    //付款方式(0-內部轉帳，1-外部匯款)
                    MBS_PAYMENT.strPAY_MODE = "1";
                    //付款銀行
                    MBS_PAYMENT.strPAY_BANK = "";
                    //付款帳號
                    MBS_PAYMENT.strPAY_ACCNO = "";
                    //應付金額(由發卡檔轉入交易金額 – 金資手續費)
                    //MBS_PAYMENT.decPAY_PAYABLE = decPAY_AMT_TOT - (decCNT * Convert.ToDecimal(fee));
                    MBS_PAYMENT.decPAY_PAYABLE = decPAY_AMT_TOT;
                    //匯款費用(外部匯款才會有金額)
                    MBS_PAYMENT.decPAY_REMIT = 0;
                    //實付金額(由發卡檔轉入交易金額 – 金資手續費)
                    //MBS_PAYMENT.decPAY_ACTUAL_PAY_AMT = decPAY_AMT_TOT - (decCNT * Convert.ToDecimal(fee));
                    MBS_PAYMENT.decPAY_ACTUAL_PAY_AMT = decPAY_AMT_TOT;
                    //備註(公共事業代號)
                    MBS_PAYMENT.strPAY_REMARK = PUBLIC_HIST.resultTable.Rows[i]["PAY_TYPE"].ToString();
                    //資料來源(Y-CyberCArd，N-收單)
                    MBS_PAYMENT.strPAY_SOURCE = "Y";
                    MBS_PAYMENT_RC = MBS_PAYMENT.insert_CyberCArd();
                    switch (MBS_PAYMENT_RC) 
                    {
                        case "S0000":
                            MBS_PAYMENT_Count_Insert++;
                            break;

                        case "F0022":
                            logger.strJobQueue = "新增特店付款檔資料重複 *** _KEY值為:" + PUBLIC_HIST.resultTable.Rows[i]["SEQ"].ToString() + " RC = " + MBS_PAYMENT_RC;
                            logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                            return "B0016" + logger.strJobQueue;

                        default: //資料庫錯誤
                            logger.strJobQueue = "新增特店付款檔資料 錯誤 *** " + MBS_PAYMENT_RC;
                            logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                            return "B0016" + logger.strJobQueue;
                    }
                    
                }
                #endregion


                //========================================================================================================
                // CLOSE-RTN
                writeDisplay();
                return "B0000";

            }
            catch (Exception e)
            {
                logger.strJobQueue = strJOBNAME + " *** " + e.ToString();
                logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                return "B0099:" + e.ToString();
            }
        }
        #endregion
        
        //========================================================================================================

        #region  SYSOUT
        void writeDisplay()
        {
            //刪除特店付款檔(RERUN)
            logger.strTBL_NAME = "MBS_PAYMENT";
            logger.strTBL_ACCESS = "D";
            logger.strMEMO = "刪除特店付款檔(RERUN)";
            logger.intTBL_COUNT = MBS_PAYMENT_Count_Delete;
            logger.writeCounter();

            //取得公用事業代扣名單
            logger.strTBL_NAME = "PUBLIC_HIST";
            logger.strTBL_ACCESS = "Q";
            logger.strMEMO = "公用事業代扣名單";
            logger.intTBL_COUNT = PUBLIC_HIST_query_cnt;
            logger.writeCounter();

            //寫入特店付款檔
            logger.strTBL_NAME = "MBS_PAYMENT";
            logger.strTBL_ACCESS = "I";
            logger.strMEMO = "寫入特店付款檔";
            logger.intTBL_COUNT = MBS_PAYMENT_Count_Insert;
            logger.writeCounter();

            
            logger.DisplayCounter(Cybersoft.Coca.Log.LevelEnum.Info);
        }
        #endregion

    }
}
