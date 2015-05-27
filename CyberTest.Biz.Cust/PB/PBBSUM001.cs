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
    /// 產生公用事業彙整報表
    /// </summary>
    public class PBBSUM001
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
        DataTable PUBLIC_HIST_DataTable = new DataTable();
        DataTable PUBLIC_HIST_DataTable_SORT = new DataTable();
        DataTable PUBLIC_HIST_DataTable_ACH = new DataTable();
        int CNT = 0;
        int CNT_ACH = 0;

        #endregion

        #region 宣告共用變數

        //批次日期
        DateTime TODAY_PROCESS_DTE = new DateTime();
        String TODAY_YYYYMMDD = "";

        decimal SUCC_CNT_1 = 0;
        decimal SUCC_AMT_1 = 0;
        decimal SUCC_FEE_1 = 0;
        decimal SUCC_AMT_T_1 = 0;
        decimal SUCC_CNT_2 = 0;
        decimal SUCC_AMT_2 = 0;
        decimal SUCC_FEE_2 = 0;
        decimal SUCC_AMT_T_2 = 0;
        decimal FAIL_CNT_3 = 0;
        decimal FAIL_AMT_3 = 0;
        decimal FAIL_AMT_T_3 = 0;
        decimal FAIL_CNT_4 = 0;
        decimal FAIL_AMT_4 = 0;
        decimal FAIL_AMT_T_4 = 0;

        #endregion

        #region 宣告檔案路徑

        #endregion

        static readonly Cybersoft.Coca.Log.Logger logger = Cybersoft.Log.LogManager<Cybersoft.Coca.Log.Logger>.GetLogger(System.Reflection.MethodInfo.GetCurrentMethod().ReflectedType.Name);
        public string RUN()
        {
            #region 準備MSGLG.SYSOUT CLASS
            logger.strJobID = JobID;
            logger.strJOBNAME = "PBBSUM001";
            logger.dtRunDate = DateTime.Now;
            #endregion

            try
            {
                #region 取得系統資訊(下次批次日)
                CMCSYS001 SYSINF = new CMCSYS001();
                SYSINF.strJobID = JobID;
                SYSINF.strJobName = "PBBSUM001";
                String SYSINF_RC = SYSINF.getSYSINF();
                TODAY_PROCESS_DTE = SYSINF.datetimeTODAY_PROCESS_DTE;
                TODAY_YYYYMMDD = TODAY_PROCESS_DTE.ToString("yyyyMMdd");
                #endregion

                #region 報表欄位
                //REPORT_TABLE.Columns.Add("MNT_CODE", typeof(string));
                #endregion

                #region 取得公用事業代繳(非ACH)
                PUBLIC_HIST_DataTable = null;
                PUBLIC_HIST_RC = PUBLIC_HIST.query_for_PBBSUM001(TODAY_YYYYMMDD);
                switch (PUBLIC_HIST_RC)
                {
                    case "S0000": //查詢成功
                        PUBLIC_HIST_DataTable = PUBLIC_HIST.resultTable;
                        CNT = PUBLIC_HIST.resultTable.Rows.Count;
                        break;

                    case "F0023": //查無該筆資料
                        logger.strJobQueue = "PUBLIC_HIST.query_for_PBBSUM001() 無資料";
                        logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                        return "B0015:" + logger.strJobQueue;

                    default: //資料庫錯誤
                        logger.strJobQueue = "PUBLIC_HIST.query_for_PBBSUM001() 錯誤 " + PUBLIC_HIST_RC;
                        logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                        return "B0016:" + logger.strJobQueue;
                }

                #region 欄位總計
                for (int i = 0; i < CNT; i++)
                {
                    SUCC_CNT_1 = SUCC_CNT_1 + Convert.ToDecimal(PUBLIC_HIST_DataTable.Rows[i]["SUCC_CNT_1"]);
                    SUCC_AMT_1 = SUCC_AMT_1 + Convert.ToDecimal(PUBLIC_HIST_DataTable.Rows[i]["SUCC_AMT_1"]);
                    SUCC_FEE_1 = SUCC_FEE_1 + Convert.ToDecimal(PUBLIC_HIST_DataTable.Rows[i]["SUCC_FEE_1"]);
                    SUCC_AMT_T_1 = SUCC_AMT_T_1 + Convert.ToDecimal(PUBLIC_HIST_DataTable.Rows[i]["SUCC_AMT_T_1"]);
                    SUCC_CNT_2 = SUCC_CNT_2 + Convert.ToDecimal(PUBLIC_HIST_DataTable.Rows[i]["SUCC_CNT_2"]);
                    SUCC_AMT_2 = SUCC_AMT_2 + Convert.ToDecimal(PUBLIC_HIST_DataTable.Rows[i]["SUCC_AMT_2"]);
                    SUCC_FEE_2 = SUCC_FEE_2 + Convert.ToDecimal(PUBLIC_HIST_DataTable.Rows[i]["SUCC_FEE_2"]);
                    SUCC_AMT_T_2 = SUCC_AMT_T_2 + Convert.ToDecimal(PUBLIC_HIST_DataTable.Rows[i]["SUCC_AMT_T_2"]);
                    FAIL_CNT_3 = FAIL_CNT_3 + Convert.ToDecimal(PUBLIC_HIST_DataTable.Rows[i]["FAIL_CNT_3"]);
                    FAIL_AMT_3 = FAIL_AMT_3 + Convert.ToDecimal(PUBLIC_HIST_DataTable.Rows[i]["FAIL_AMT_3"]);
                    FAIL_AMT_T_3 = FAIL_AMT_T_3 + Convert.ToDecimal(PUBLIC_HIST_DataTable.Rows[i]["FAIL_AMT_T_3"]);
                    FAIL_CNT_4 = FAIL_CNT_4 + Convert.ToDecimal(PUBLIC_HIST_DataTable.Rows[i]["FAIL_CNT_4"]);
                    FAIL_AMT_4 = FAIL_AMT_4 + Convert.ToDecimal(PUBLIC_HIST_DataTable.Rows[i]["FAIL_AMT_4"]);
                    FAIL_AMT_T_4 = FAIL_AMT_T_4 + Convert.ToDecimal(PUBLIC_HIST_DataTable.Rows[i]["FAIL_AMT_T_4"]);
                }
                #endregion

                PUBLIC_HIST_DataTable_SORT = PUBLIC_HIST_DataTable.Clone(); //複製欄位結構

                //ACH總項目
                DataRow[] DR_1 = PUBLIC_HIST_DataTable.Select("CODE = '-'");
                //主類別
                DataRow[] DR_2 = PUBLIC_HIST_DataTable.Select("CODE = '0001' OR CODE = '0002' OR CODE = '0003' OR CODE = '0004'");
                //停車類
                DataRow[] DR_3 = PUBLIC_HIST_DataTable.Select("DESCR like '%車%'");
                //瓦斯類
                DataRow[] DR_4 = PUBLIC_HIST_DataTable.Select("DESCR like '%瓦%'");

                for (int r = 0; r < DR_1.Length; r++)
                {
                    PUBLIC_HIST_DataTable_SORT.ImportRow(DR_1[r]);
                }
                for (int r = 0; r < DR_2.Length; r++)
                {
                    PUBLIC_HIST_DataTable_SORT.ImportRow(DR_2[r]);
                }
                for (int r = 0; r < DR_3.Length; r++)
                {
                    PUBLIC_HIST_DataTable_SORT.ImportRow(DR_3[r]);
                }
                for (int r = 0; r < DR_4.Length; r++)
                {
                    PUBLIC_HIST_DataTable_SORT.ImportRow(DR_4[r]);
                }
                
                #endregion

                #region 取得公用事業代繳(ACH)
                PUBLIC_HIST_RC = PUBLIC_HIST.query_for_PBBSUM001_ACH(TODAY_YYYYMMDD);
                switch (PUBLIC_HIST_RC)
                {
                    case "S0000": //查詢成功
                        PUBLIC_HIST_DataTable_ACH = PUBLIC_HIST.resultTable;
                        CNT_ACH = PUBLIC_HIST.resultTable.Rows.Count;
                        break;

                    case "F0023": //查無該筆資料
                        logger.strJobQueue = "PUBLIC_HIST.query_for_PBBSUM001_ACH() 無資料";
                        logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                        return "B0015:" + logger.strJobQueue;

                    default: //資料庫錯誤
                        logger.strJobQueue = "PUBLIC_HIST.query_for_PBBSUM001_ACH() 錯誤 " + PUBLIC_HIST_RC;
                        logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                        return "B0016:" + logger.strJobQueue;
                }
                #endregion

                writeReport(PUBLIC_HIST_DataTable_SORT, PUBLIC_HIST_DataTable_ACH);
                
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

        #region 批次報表
        string writeReport(DataTable inTable, DataTable inTable_ACH)
        {
            try
            {
                CMCRPT001 CMCRPT001 = new CMCRPT001();
                //設定特殊欄位資料
                ArrayList alSumData = new ArrayList();
                alSumData.Add(new ExcelFactory.ListItem("#RPT_DATE", TODAY_PROCESS_DTE.ToShortDateString()));
                alSumData.Add(new ExcelFactory.ListItem("#SUCC_CNT_1", SUCC_CNT_1));
                alSumData.Add(new ExcelFactory.ListItem("#SUCC_AMT_1", SUCC_AMT_1));
                alSumData.Add(new ExcelFactory.ListItem("#SUCC_FEE_1", SUCC_FEE_1));
                alSumData.Add(new ExcelFactory.ListItem("#SUCC_AMT_T_1", SUCC_AMT_T_1));
                alSumData.Add(new ExcelFactory.ListItem("#SUCC_CNT_2", SUCC_CNT_2));
                alSumData.Add(new ExcelFactory.ListItem("#SUCC_AMT_2", SUCC_AMT_2));
                alSumData.Add(new ExcelFactory.ListItem("#SUCC_FEE_2", SUCC_FEE_2));
                alSumData.Add(new ExcelFactory.ListItem("#SUCC_AMT_T_2", SUCC_AMT_T_2));
                alSumData.Add(new ExcelFactory.ListItem("#FAIL_CNT_3", FAIL_CNT_3));
                alSumData.Add(new ExcelFactory.ListItem("#FAIL_AMT_3", FAIL_AMT_3));
                alSumData.Add(new ExcelFactory.ListItem("#FAIL_AMT_T_3", FAIL_AMT_T_3));
                alSumData.Add(new ExcelFactory.ListItem("#FAIL_CNT_4", FAIL_CNT_4));
                alSumData.Add(new ExcelFactory.ListItem("#FAIL_AMT_4", FAIL_AMT_4));
                alSumData.Add(new ExcelFactory.ListItem("#FAIL_AMT_T_4", FAIL_AMT_T_4));


                ArrayList[] alSumDatas = new ArrayList[2];
                alSumDatas[0] = alSumData;
                alSumDatas[1] = alSumData;


                #region 產出Detail 與 報表分頁名稱

                DataTable[] outTable = new DataTable[2];

                outTable[0] = inTable.Copy();
                outTable[0].TableName = "公用事業彙整報表";

                outTable[1] = inTable_ACH.Copy();
                outTable[1].TableName = "ACH代扣繳彙整表";

                string[] strTempSheets = { "公用事業彙整報表", "ACH代扣繳彙整表" };
                #endregion

                //產出報表
                CMCRPT001.Output(outTable, alSumDatas, "PBRSUM001_公用事業彙整報表", "PBRSUM001", strTempSheets, TODAY_PROCESS_DTE);

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

        #region Display
        void writeDisplay()
        {
            //logger.strJobID = "00002";
            ////擷取ACH需產出約定檔的資料
            //logger.strTBL_NAME = "PUBLIC_HIST";
            //logger.strTBL_ACCESS = "Q";
            //logger.intTBL_COUNT = PUBLIC_HIST_Query_Count;
            //logger.writeCounter();

            ////ACH約定檔筆數(含頭尾)
            //logger.strTBL_NAME = "ACH_CHGOUT";
            //logger.strTBL_ACCESS = "I";
            //logger.intTBL_COUNT = ACH_TXT_Count;
            //logger.writeCounter();

            ////更新PUBLIC_HIST
            //logger.strTBL_NAME = "PUBLIC_HIST";
            //logger.strTBL_ACCESS = "U";
            //logger.intTBL_COUNT = PUBLIC_HIST_Update_Count;
            //logger.writeCounter();

            logger.DisplayCounter(Cybersoft.Coca.Log.LevelEnum.Info);
        }
        #endregion
    }
}

