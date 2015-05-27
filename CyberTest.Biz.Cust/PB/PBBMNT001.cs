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
    /// 維護最近扣款卡號及最近扣款日
    /// [可RERUN],但批次日不能小於PUBLIC_APPLY的FIRST_DTE(第一次約定日期)
    /// 產出報表：PBRRPT001-約定代繳_公共事業_卡片停卡報表
    /// Sheet1:約定卡片停卡
    /// Sheet2:申辦約定但已無流通卡
    /// </summary>
    public class PBBMNT001
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
        PUBLIC_APPLYDao PUBLIC_APPLY = new PUBLIC_APPLYDao();
        String PUBLIC_APPLY_RC = "";
        DataTable PUBLIC_APPLY_TABLE = new DataTable();
        DataTable REPORT_TABLE_Sheet1 = new DataTable();
        DataTable REPORT_TABLE_Sheet2 = new DataTable();
        #endregion

        #region 宣告共用變數
        string strBank_Name = "";
        //批次日期
        DateTime TODAY_PROCESS_DTE = new DateTime();

        //筆數
        int PUBLIC_APPLY_Count = 0;

        #endregion

        static readonly Cybersoft.Coca.Log.Logger logger = Cybersoft.Log.LogManager<Cybersoft.Coca.Log.Logger>.GetLogger(System.Reflection.MethodInfo.GetCurrentMethod().ReflectedType.Name);
        //========================================================================================================
        #region 程式主邏輯【MAIN Routine】
        public string RUN()
        {
            #region 準備MSGLG.SYSOUT CLASS
            logger.strJobID = JobID;
            logger.strJOBNAME = "PBBMNT001";
            logger.dtRunDate = DateTime.Now;
            #endregion

            try
            {
                #region 取得系統資訊(前次批次日、本日批次日)
                CMCSYS001 SYSINF = new CMCSYS001();
                SYSINF.strJobID = JobID;
                SYSINF.strJobName = "PBBMNT001";
                String SYSINF_RC = SYSINF.getSYSINF();
                TODAY_PROCESS_DTE = SYSINF.datetimeTODAY_PROCESS_DTE;
                strBank_Name = SYSINF.strREPORT_TITLE;
                #endregion

                #region 撈取今日扣繳成功資料，維護PUBLIC_APPLY的前次扣繳卡號(PAY_CARD_NBR_PREV)、前次扣繳帳號(PAY_ACCT_NBR_PREV)、前次扣款日(PAY_DATE)

                PUBLIC_APPLY_RC = PUBLIC_APPLY.update_PREV_INF(TODAY_PROCESS_DTE.ToString("yyyyMMdd"));
                switch (PUBLIC_APPLY_RC)
                {
                    case "S0000": //更新成功   
                        PUBLIC_APPLY_Count = PUBLIC_APPLY.intUptCnt;
                        logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                        break;

                    default: //資料庫錯誤
                        logger.strJobQueue = "更新PUBLIC_APPLY 資料錯誤:" + PUBLIC_APPLY_RC;
                        logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                        return "B0016:" + logger.strJobQueue;
                }
                #endregion

                #region 產出PBRRPT001-約定代繳_公共事業_卡片停卡報表
                #region 撈取公共事業_卡片停卡明細(Sheet1)
                PUBLIC_APPLY.init();
                PUBLIC_APPLY_RC = PUBLIC_APPLY.query_for_PBRRPT001();
                switch (PUBLIC_APPLY_RC)
                {
                    case "S0000": //更新成功   
                        PUBLIC_APPLY_TABLE = PUBLIC_APPLY.resultTable;
                        PUBLIC_APPLY_Count = PUBLIC_APPLY.resultTable.Rows.Count;
                        break;

                    default: //資料庫錯誤
                        logger.strJobQueue = "更新PUBLIC_APPLY 資料錯誤:" + PUBLIC_APPLY_RC;
                        logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                        return "B0016:" + logger.strJobQueue;
                }
                #endregion

                #region 撈取申辦約定但已全停卡明細(Sheet2)
                PUBLIC_APPLY_TABLE.DefaultView.RowFilter = "ALL_STOP = 'Y' ";
                REPORT_TABLE_Sheet2 = PUBLIC_APPLY_TABLE.DefaultView.ToTable();
                #endregion

                //寫出報表
                writeReport();
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

        #region Display
        void writeDisplay()
        {
            logger.strJobID = "00002";

            //PUBLIC_HIST檢核成功資料
            logger.strTBL_NAME = "PUBLIC_APPLY";
            logger.strTBL_ACCESS = "U";
            logger.strMEMO = "UpdateDiff";
            logger.intTBL_COUNT = PUBLIC_APPLY_Count;
            logger.writeCounter();


            logger.DisplayCounter(Cybersoft.Coca.Log.LevelEnum.Info);
        }
        #endregion

        #region 寫出至報表
        void writeReport()
        {
            //sheet1
            #region 刪除 CARD_VALID 和 ALL_STOP 和 UTILITY_CODE 不顯示
            PUBLIC_APPLY_TABLE.Columns.Remove("CARD_VALID");
            PUBLIC_APPLY_TABLE.Columns.Remove("ALL_STOP");
            PUBLIC_APPLY_TABLE.Columns.Remove("UTILITY_CODE");
            #endregion

            //sheet2
            #region 刪除 CARD_VALID 和 ALL_STOP 和 UTILITY_CODE 不顯示
            REPORT_TABLE_Sheet2.Columns.Remove("CARD_VALID");
            REPORT_TABLE_Sheet2.Columns.Remove("ALL_STOP");
            REPORT_TABLE_Sheet2.Columns.Remove("UTILITY_CODE");
            #endregion

            CMCRPT001 CMCRPT001 = new CMCRPT001();
            DataTable[] inTables = new DataTable[2];
            inTables[0] = PUBLIC_APPLY_TABLE.Copy();
            inTables[0].TableName = "約定卡片停卡";
            inTables[1] = REPORT_TABLE_Sheet2.Copy();
            inTables[1].TableName = "申辦約定但已無流通卡";

            ArrayList alSumData = new ArrayList();
            alSumData.Add(new ExcelFactory.ListItem("#RPT_BANK_NAME", strBank_Name));
            alSumData.Add(new ExcelFactory.ListItem("#RPT_DATE", TODAY_PROCESS_DTE.ToString("yyyy/MM/dd")));
            alSumData.Add(new ExcelFactory.ListItem("#TOTAL_CNT", PUBLIC_APPLY_TABLE.Rows.Count));

            ArrayList alSumData2 = new ArrayList();
            alSumData2.Add(new ExcelFactory.ListItem("#RPT_BANK_NAME", strBank_Name));
            alSumData2.Add(new ExcelFactory.ListItem("#RPT_DATE", TODAY_PROCESS_DTE.ToString("yyyy/MM/dd")));
            alSumData2.Add(new ExcelFactory.ListItem("#TOTAL_CNT", REPORT_TABLE_Sheet2.Rows.Count));

            ArrayList[] alSumDatas = new ArrayList[2];
            alSumDatas[0] = alSumData;
            alSumDatas[1] = alSumData2;

            string[] strTempSheets = { "Sheet1", "Sheet2" };
            //設定合併欄位資料
            ArrayList alMegData = new ArrayList();
            //產出報表
            string strFileName = "PBRRPT001-約定代繳_公用事業_卡片停卡報表";
            CMCRPT001.Output(inTables, alSumDatas, strFileName, "PBRRPT001", strTempSheets, TODAY_PROCESS_DTE);

        }
        #endregion
    }
}
