using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cybersoft.Data.DAL;
using System.Data;
using Cybersoft.Base;
using System.IO;
using Cybersoft.Data;
using System.Collections;
using Cybersoft.ExportDocument;
using System.Globalization;

namespace Cybersoft.Biz.Cust
{
    /// <summary>
    /// PBBELR002，
    /// 信用卡代繳台電費用聯往借貸BXDTL.TXT
    /// /// </summary>
    public class PBBELR002
    {
        private string JobID;
        public string strJobID
        {
            get { return JobID; }
            set { JobID = value; }
        }

        
        #region 宣告table
        //宣告 TX_WAREHOUSE
        string TX_WAREHOUSE_RC = "";
        TX_WAREHOUSEDao TX_WAREHOUSE = new TX_WAREHOUSEDao();
        DataTable TX_WAREHOUSE_DataTable = new DataTable();
        int TX_WAREHOUSE_Count_Query = 0;

        string ZZ_REPORT_HIST_RC = "";
        ZZ_REPORT_HISTDao ZZ_REPORT_HIST = new ZZ_REPORT_HISTDao();
        int ZZ_REPORT_HIST_Count_Delete = 0;
        #endregion

        #region 宣告共用常數
        //檔案長度
        const int intDataLength = 98;
        
        #endregion

        #region 宣告共用變數
        DataTable BXDTL_DataTable = new DataTable();
        DataTable BXDTL_xmlTable = null;
        int BXDTL_Count_Insert = 0;   //寫出檔筆數

        DateTime PREV_PROCESS_DTE = new DateTime();
        DateTime TODAY_PROCESS_DTE = new DateTime();
        DateTime NEXT_PROCESS_DTE = new DateTime();
        DateTime BANK_TODAY_PROCESS_DTE = new DateTime();
        DateTime BANK_NEXT_PROCESS_DTE = new DateTime();
        string strRSP_NO = string.Empty;

        int i = 0;
        //ZZ_REPORT_HIST新增筆數
        int intZZ_REPORT_HIST_I_CNT = 0;
        #endregion

        #region 宣告檔案路徑
        //檔案目的路徑(資料夾)
        string strDestPath = "";
        //目的檔名
        string get_outfile_name = "";
        //目的檔案完整路徑
        string strFileFullPath_out = "";

        //檔案格式
        string FileLayout = "";
        #endregion

        #region 宣告共用類別
        //取得XML路徑
        CMCURL001 CMCURL001 = new CMCURL001();
        FileParseByXml xml = new FileParseByXml();
        #endregion

        #region 設定檔案編碼
        Encoding BIG5 = Encoding.GetEncoding("big5");
        #endregion

        static readonly Cybersoft.Coca.Log.Logger logger = Cybersoft.Log.LogManager<Cybersoft.Coca.Log.Logger>.GetLogger(System.Reflection.MethodInfo.GetCurrentMethod().ReflectedType.Name);
        //========================================================================================================
        public string RUN()
        {
            #region 準備MSGLG.SYSOUT CLASS
            logger.strJobID = JobID;
            logger.strJOBNAME = "PBBELR002";
            logger.dtRunDate = DateTime.Now;
            #endregion
            try
            {
                #region 取得系統資訊(前次批次日、本日批次日)
                CMCSYS001 SYSINF = new CMCSYS001();
                SYSINF.strJobID = JobID;
                SYSINF.strJobName = "PBBELR002";
                String SYSINF_RC = SYSINF.getSYSINF();
                PREV_PROCESS_DTE = SYSINF.datetimePREV_PROCESS_DTE;
                TODAY_PROCESS_DTE = SYSINF.datetimeTODAY_PROCESS_DTE;
                NEXT_PROCESS_DTE = SYSINF.datetimeNEXT_PROCESS_DTE;
                BANK_NEXT_PROCESS_DTE = SYSINF.datetimeBANK_NEXT_PROCESS_DTE;
                BANK_TODAY_PROCESS_DTE = SYSINF.datetimeBANK_TODAY_PROCESS_DTE;
                #endregion

                #region 取得檔案路徑
                getFilePath();
                #endregion

                #region 載入檔案格式資訊
                //載入檔檔案格式資訊
                BXDTL_xmlTable = xml.Xml2DataTable(FileLayout, "REC");
                // 使用XML定義的LAYOUT定對暫存TABLE
                BXDTL_DataTable = xml.dtXML2DataTable(BXDTL_xmlTable);
                #endregion

                //定義TABLE
                TX_WAREHOUSE.table_define();
                ZZ_REPORT_HIST.table_define();

                #region 建立本次的清單
                ZZ_REPORT_HIST.init();
                ZZ_REPORT_HIST.DateTimeWhereBATCH_PROCESS_DTE = TODAY_PROCESS_DTE;
                ZZ_REPORT_HIST.strWhereFILE_NAME = "BXDTL(台電聯往借貸).txt";
                ZZ_REPORT_HIST.strWhereMNT_USER = "PBBELR002";
                ZZ_REPORT_HIST_RC = ZZ_REPORT_HIST.delete();
                switch (ZZ_REPORT_HIST_RC)
                {
                    case "S0000": //刪除成功
                        ZZ_REPORT_HIST_Count_Delete = ZZ_REPORT_HIST.intDelCnt;
                        logger.strJobQueue = " ZZ_REPORT_HIST.delete() finish 筆數:" + ZZ_REPORT_HIST_Count_Delete.ToString("###,###,##0").PadLeft(11, ' ');
                        logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                        break;

                    default: //資料庫錯誤
                        logger.strJobQueue = "刪除今日已新增BXDTL(台電聯往借貸).txt的資料錯誤 " + ZZ_REPORT_HIST_RC;
                        logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                        return "B0016" + logger.strJobQueue;
                }
                #endregion


                #region 建立本次的清單
                TX_WAREHOUSE.init();
                TX_WAREHOUSE.resultTable.Clear();
                TX_WAREHOUSE.strWhereSOURCE_CODE = "PU00000004";
                //TX_WAREHOUSE.DateTimeWhereEFF_DTE = NEXT_PROCESS_DTE;
                TX_WAREHOUSE.DateTimeWhereEFF_DTE = BANK_NEXT_PROCESS_DTE;
                TX_WAREHOUSE_RC = TX_WAREHOUSE.query_for_BXDTL();
                switch (TX_WAREHOUSE_RC.Substring(0, 5))
                {
                    case "S0000":  //查詢成功
                        break;
                    case "F0023":  //查無資料
                        logger.strJobQueue = "本日無信用卡代繳台電費用聯往借貸資料：" + TX_WAREHOUSE_RC;
                        logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                        return "B0000:" + logger.strJobQueue;
                    default:       //資料庫錯誤
                        logger.strJobQueue = "[TX_WAREHOUSE.query_for_BXDTL() ERROR ]：" + TX_WAREHOUSE_RC;
                        logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                        return "B0016:" + logger.strJobQueue;
                }
                logger.strJobQueue = "讀取本日信用卡代繳台電費用聯往借貸筆共 " + TX_WAREHOUSE.resultTable.Rows.Count + " 筆。";
                logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                #endregion

                #region 確認檢核結果
                for (i = 0; i < TX_WAREHOUSE.resultTable.Rows.Count; i++)
                {
                    //寫出檔
                    outfile_data_move();
                }
                #endregion

                #region 產生檔案BXDTL.TXT
                write_outfile();
                #endregion

                //寫入ZZ_REPORT_HIST
                #region 寫入ZZ_REPORT_HIST
                writeZZ_REPORT_HIST();
                #endregion

                #region 整批新增ZZ_REPORT_HIST
                intZZ_REPORT_HIST_I_CNT = ZZ_REPORT_HIST.resultTable.Rows.Count;
                ZZ_REPORT_HIST.insert_by_DT();
                //判別回傳筆數是否相同
                if (intZZ_REPORT_HIST_I_CNT != ZZ_REPORT_HIST.intInsCnt)
                {
                    logger.strJobQueue = "整批新增TSCC_HT時筆數異常,原筆數 : " + intZZ_REPORT_HIST_I_CNT + " 實際筆數: " + ZZ_REPORT_HIST.intInsCnt;
                    logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                    return "B0012：" + logger.strJobQueue;
                }
                #endregion

                #region writeDisplay
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
        //========================================================================================================
        #region 1.載入xml路徑 2.載入檔案路徑3.檔案名稱
        private void getFilePath()
        {
            //載入檔案格式xml路徑
            FileLayout = CMCURL001.getURL("BXDTL.xml");

            //載入目的檔案路徑(僅資料夾)
            strDestPath = CMCURL001.getPATH("BXDTL");
            //產出檔案名稱
            get_outfile_name = "BXDTL(台電聯往借貸).txt";
            //產出檔案完整路徑
            strFileFullPath_out = strDestPath + get_outfile_name;
            strFileFullPath_out = strFileFullPath_out.Replace("yyyymmdd",TODAY_PROCESS_DTE.ToString("yyyyMMdd"));

            logger.strJobQueue = "檔案格式xml路徑: " + FileLayout;
            logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
            logger.strJobQueue = "檔案(BXDTL.txt)路徑:" + strFileFullPath_out;
            logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
        }
        #endregion

        #region 寫檔BXDTL.txt
        void outfile_data_move()
        {
            DataRow DT_G = BXDTL_DataTable.NewRow();
            DT_G["TXSEQ"] = (i+1).ToString().PadLeft(5,'0');
            DT_G["TXDAY"] = NEXT_PROCESS_DTE.AddYears(-1911).ToString("yyyyMMdd").PadLeft(8, '0').Substring(1, 7);
            DT_G["KINBR"] = "3138";
            DT_G["DEPNO"] = "00";
            DT_G["OBRNO"] = TX_WAREHOUSE.resultTable.Rows[i]["BR_NO"].ToString();
            DT_G["ODEPNO"] = "00";
            DT_G["DSCID"] = "999";
            DT_G["CRDB"] = "2";
            int decPAY_AMT = Convert.ToInt32(TX_WAREHOUSE.resultTable.Rows[i]["PAY_AMT"]);
            int decPAY_FEE = Convert.ToInt32(TX_WAREHOUSE.resultTable.Rows[i]["CNT"]) * 3;
            int decPAY_AMT_C = (decPAY_AMT - decPAY_FEE) * 100;
            DT_G["TXAMT"] = decPAY_AMT_C.ToString().PadLeft(13, '0');
            DT_G["ACNO"] = "BBBB001040";
            DT_G["OACNO"] = "BBBB001040";
            DT_G["COMT1"] = "信用卡代繳台電費用" + TX_WAREHOUSE.resultTable.Rows[i]["CNT"].ToString().PadLeft(4,'0') + "筆";
            DT_G["COMT2"] = "";
            DT_G["COMT3"] = "";
            //新增 DataRow
            BXDTL_DataTable.Rows.Add(DT_G);
        }
        #endregion

        #region 產出檔
        void write_outfile()
        {
            //產出(DATA_TABLE --> 陣列 --> 檔案)
            if (BXDTL_DataTable.Rows.Count > 0)
            {
                #region 產生BXDTL 

                #region 將DATA_TABLE的資料轉成字串至陣列中_BXDTL檔
                //明細
                String[] BXDTL_STRING = xml.DataTable2FileStrArray(BIG5, BXDTL_DataTable, BXDTL_xmlTable, "", true);
                if (xml.strMSG.Length > 0)
                {
                    logger.strJobQueue = "[DataTable2FileStrArray(BXDTL)] - " + xml.strMSG.ToString().Trim();
                    logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                    throw new System.Exception("B0099：" + logger.strJobQueue);
                }
                
                #endregion

                #region 陣列寫出至檔案中

                //TOTAL筆數
                BXDTL_Count_Insert = 0;
                #region 寫出 DATA
                FileStream fsOutFile = new FileStream(strFileFullPath_out, FileMode.Create, FileAccess.Write, FileShare.None);
                using (StreamWriter srOutFile = new StreamWriter(fsOutFile, BIG5))
                { 
                    //寫明細
                    for (int j = 0; j < BXDTL_DataTable.Rows.Count; j++)
                    {

                        srOutFile.Write(BXDTL_STRING[j]);
                        srOutFile.Write(System.Environment.NewLine);
                        BXDTL_Count_Insert++;
                    }
                    srOutFile.Close();
                }
                fsOutFile.Close();
                #region 確認交換資料通知檔有無產出檔案
                if (!File.Exists(strFileFullPath_out))
                {
                    logger.strJobQueue = "檔BXDTL.TXT檔案產出失敗：" + strFileFullPath_out;
                    logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                    throw new System.Exception("B0012：" + logger.strJobQueue);
                }
                #endregion
                #endregion
                #endregion
                #endregion

            }
        }
        #endregion

        private void writeZZ_REPORT_HIST()
        {
            ZZ_REPORT_HIST.initInsert_row();
            int r = ZZ_REPORT_HIST.resultTable.Rows.Count - 1;
            //批次日期
            ZZ_REPORT_HIST.resultTable.Rows[r]["BATCH_PROCESS_DTE"] = TODAY_PROCESS_DTE;
            //報表英文名稱
            ZZ_REPORT_HIST.resultTable.Rows[r]["ENG_FILE_NAME"] = "PBRELO001";
            //報表全名
            ZZ_REPORT_HIST.resultTable.Rows[r]["FILE_NAME"] = "BXDTL(台電聯往借貸).txt";
            //報表筆數
            ZZ_REPORT_HIST.resultTable.Rows[r]["DATA_CNT"] = BXDTL_Count_Insert;
            //超連結位址
            ZZ_REPORT_HIST.resultTable.Rows[r]["URL"] = strFileFullPath_out;
            //維護時間
            ZZ_REPORT_HIST.resultTable.Rows[r]["MNT_DT"] = System.DateTime.Now;
            //維護人員
            ZZ_REPORT_HIST.resultTable.Rows[r]["MNT_USER"] = "PBBELR002";
        }

        #region Display
        void writeDisplay()
        {
            logger.strTBL_NAME = "TX_WAREHOUSE";
            logger.strTBL_ACCESS = "Q";
            logger.intTBL_COUNT = TX_WAREHOUSE_Count_Query;
            logger.writeCounter();

            logger.strTBL_NAME = "BXDTL";
            logger.strTBL_ACCESS = "I";
            logger.intTBL_COUNT = BXDTL_Count_Insert;
            logger.writeCounter();

            logger.DisplayCounter(Cybersoft.Coca.Log.LevelEnum.Info);
        }
        #endregion
    }
}
