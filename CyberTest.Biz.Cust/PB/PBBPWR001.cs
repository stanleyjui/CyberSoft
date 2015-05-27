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
    /// 讀市水約定回覆檔更新PUBLIC_APPLY，並寫出信用卡代繳自來水費用異動明細表(CAD0002C.txt)
    /// </summary>
    public class PBBPWR001
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
        DateTime SYS_PROCESS_DTE = new DateTime();
        String TODAY_YYYYMMDD = "";
        String TODAY_MMDD = "";

        //筆數&金額
        int PUBLIC_APPLY_Update_Count = 0;
        int k = 0; //REPORT_TABLE
        int intFailCount = 0;
        int HeadFileDataCount = 0;
        int DetailFileDataCount = 0;
        int intReadInfDataCount = 0;

        //市水約定回覆檔欄位
        //Head檔欄位
        Decimal FILE_COUNT = 0;

        //Detail檔欄位
        String REPLY_DTE = "";
        String BR_NO = "";
        String BR_NAME = "";
        String PAY_TYPE = "0002";
        String TRANS_DTE = "";
        String PAY_ACCT_NBR = "";
        String PAY_NBR = "";
        String CHANGE_TYPE = "";
        String CHANGE_DTE = "";
        String WATER_CHANGE_DTE = "";
        String ERROR_CODE = "";

        //檔案長度
        const int intDataLength = 69;
        const int intDataHeadLength = 18;

        #endregion

        #region 宣告檔案路徑

        //XML放置路徑 
        String strXML_Layout = "";
        //寫出檔案名稱
        String strOutFileName = "";
        //寫出檔案路徑
        String FILE_PATH_H = "";
        String FILE_PATH = "";

        #endregion

        static readonly Cybersoft.Coca.Log.Logger logger = Cybersoft.Log.LogManager<Cybersoft.Coca.Log.Logger>.GetLogger(System.Reflection.MethodInfo.GetCurrentMethod().ReflectedType.Name);
        public string RUN()
        {
            #region 準備MSGLG.SYSOUT CLASS
            logger.strJobID = JobID;
            logger.strJOBNAME = "PBBPWR001";
            logger.dtRunDate = DateTime.Now;
            #endregion

            try
            {
                #region 取得系統資訊(前次批次日、本日批次日)
                CMCSYS001 SYSINF = new CMCSYS001();
                SYSINF.strJobID = JobID;
                SYSINF.strJobName = "PBBPWR001";
                String SYSINF_RC = SYSINF.getSYSINF();
                PREV_PROCESS_DTE = SYSINF.datetimePREV_PROCESS_DTE;
                TODAY_PROCESS_DTE = SYSINF.datetimeTODAY_PROCESS_DTE;
                NEXT_PROCESS_DTE = SYSINF.datetimeNEXT_PROCESS_DTE;
                SYS_PROCESS_DTE = System.DateTime.Now;
                //判斷下一營業日是否為星期四
                //if (NEXT_PROCESS_DTE.DayOfWeek.ToString() != "Thursday")
                //{
                //    logger.strJobQueue = "下一營業日 (" + NEXT_PROCESS_DTE.ToString("yyyy/MM/dd") + ") 非星期四, 故不執行";
                //    logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                //    return "B0000:" + logger.strJobQueue;
                //}
                TODAY_YYYYMMDD = TODAY_PROCESS_DTE.ToString("yyyyMMdd");
                TODAY_MMDD = TODAY_PROCESS_DTE.ToString("MMdd");
                #endregion

                #region 宣告檔案路徑
                //XML名稱
                strXML_Layout = CMCURL.getURL("TPEWATER_CHGIN.xml");
                //寫出檔案路徑
                FILE_PATH_H = CMCURL.getPATH("TPEWATER_CHGIN.xml");
                FILE_PATH = CMCURL.getPATH("TPEWATER_CHGIN");

                if (strXML_Layout == "" || FILE_PATH == "" || FILE_PATH_H == "")
                {
                    logger.strJobQueue = "路徑取得錯誤!!! TPEWATER_CHGIN.xml路徑為 " + strXML_Layout + " TPEWATER_CHGIN_H路徑為 " + FILE_PATH_H + " TPEWATER_CHGIN路徑為 " + FILE_PATH;
                    logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                    return "B0099:" + logger.strJobQueue;
                }
                else
                {
                    FILE_PATH_H = FILE_PATH_H.Replace("yyyymmdd", TODAY_YYYYMMDD);
                    FILE_PATH_H = FILE_PATH_H.Replace("mmdd", TODAY_MMDD);  //替換日期
                    FILE_PATH = FILE_PATH.Replace("yyyymmdd", TODAY_YYYYMMDD);
                    FILE_PATH = FILE_PATH.Replace("mmdd", TODAY_MMDD);  //替換日期
                    logger.strJobQueue = "TPEWATER_CHGIN.xml路徑為 " + strXML_Layout + " TPEWATER_CHGIN_H路徑為 " + FILE_PATH_H + " TPEWATER_CHGIN路徑為 " + FILE_PATH;
                    logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                }
                string Check_RC = CMCURL.isFileExists(FILE_PATH).ToString();
                if (Check_RC.Substring(0, 5) != "S0000")
                {
                    logger.strJobQueue = Check_RC.Trim();
                    logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                    return "B0000";
                }
                Check_RC = CMCURL.isFileExists(FILE_PATH_H).ToString();
                if (Check_RC.Substring(0, 5) != "S0000")
                {
                    logger.strJobQueue = Check_RC.Trim();
                    logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                    return "B0000";
                }
                #endregion

                #region 定義報表Table
                REPORT_TABLE.Columns.Add("KEY", typeof(string));
                REPORT_TABLE.Columns.Add("BR_NAME", typeof(string));
                REPORT_TABLE.Columns.Add("REPLY_DTE", typeof(string));
                REPORT_TABLE.Columns.Add("RPT_SEQ", typeof(string));
                REPORT_TABLE.Columns.Add("TRANS_DTE", typeof(string));
                REPORT_TABLE.Columns.Add("PAY_NBR", typeof(string));
                REPORT_TABLE.Columns.Add("ACCT_NBR", typeof(string));
                REPORT_TABLE.Columns.Add("CHANGE_DTE", typeof(string));
                REPORT_TABLE.Columns.Add("CHANGE_REASON", typeof(string));
                REPORT_TABLE.Columns.Add("WATER_CHANGE_DTE", typeof(string));
                REPORT_TABLE.Columns.Add("ERROR_CODE", typeof(string));
                #endregion

                #region 定義小計subtotal

                inSubtotTable.Columns.Add("POS_DTE_YYYYMMDD", typeof(string));
                inSubtotTable.Columns["POS_DTE_YYYYMMDD"].DefaultValue = TODAY_PROCESS_DTE.ToString("yyyy/MM/dd");

                inSubtotTable.Columns.Add("KEY", typeof(string));
                inSubtotTable.Columns["KEY"].DefaultValue = "";

                inSubtotTable.Columns.Add("BR_NAME", typeof(string));
                inSubtotTable.Columns["BR_NAME"].DefaultValue = "";

                inSubtotTable.Columns.Add("SUB_FAIL_CNT", typeof(string));
                inSubtotTable.Columns["SUB_FAIL_CNT"].DefaultValue = "";

                //inSubtotTable.Columns.Add("SUB_CNT", typeof(string));
                //inSubtotTable.Columns["SUB_CNT"].DefaultValue = "";


                #endregion

                #region 載入檔案格式資訊
                FileParseByXml xml = new FileParseByXml();

                // REC1 Layout
                DataTable REC_H_DataTable = xml.Xml2DataTable(strXML_Layout, "REC1");

                if (xml.strMSG.Length > 0)
                {
                    logger.strJobQueue = "[Xml2DataTable(REC1)] - " + xml.strMSG.ToString().Trim();
                    logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                    return "B0099:" + logger.strJobQueue;
                }

                // REC2 Layout
                DataTable REC_DataTable = xml.Xml2DataTable(strXML_Layout, "REC2");

                if (xml.strMSG.Length > 0)
                {
                    logger.strJobQueue = "[Xml2DataTable(REC2)] - " + xml.strMSG.ToString().Trim();
                    logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                    return "B0099:" + logger.strJobQueue;
                }
                #endregion

                #region 設定檔案編碼
                Encoding BIG5 = Encoding.GetEncoding("big5");
                #endregion

                #region 讀取Head檔資料
                strOutFileName = FILE_PATH_H;
                using (StreamReader srInFile = new StreamReader(strOutFileName, BIG5))
                {
                    string strInhData;

                    while ((strInhData = srInFile.ReadLine()) != null)
                    {
                        HeadFileDataCount++;
                        DataTable InhData_DataTable = new DataTable();

                        #region 檔案格式檢核(筆資料長度)

                        if (strInhData.Length != intDataHeadLength)
                        {
                            logger.strJobQueue = "市水約定回覆Head檔中第" + HeadFileDataCount + "筆的長度有誤，請通知系統人員! 該筆實際長度為 " + strInhData.Length;
                            logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                            return "B0099:" + logger.strJobQueue;
                        }

                        if (HeadFileDataCount > 1)
                        {
                            logger.strJobQueue = "中華電信異動回覆Head檔筆數 > 1筆，請通知系統人員!";
                            logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                            return "B0098:" + logger.strJobQueue;
                        }
                        #endregion

                        #region 依 Layout 拆解資料
                        InhData_DataTable = xml.FileLine2DataTable(BIG5, strInhData, REC_H_DataTable);
                        if (xml.strMSG.Length > 0)
                        {
                            logger.strJobQueue = "[FileLine2DataTable(REC1)] (L" + HeadFileDataCount + ") - " + xml.strMSG.ToString().Trim();
                            logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                            return "B0099:" + logger.strJobQueue;
                        }
                        #endregion

                        #region 檢核明細資料

                        FILE_COUNT = Convert.ToDecimal(InhData_DataTable.Rows[0]["TRANS_CNT"]);

                        #endregion
                    }
                }
                #endregion

                #region 計算明細檔總筆數
                strOutFileName = FILE_PATH;
                using (StreamReader srInFile = new StreamReader(strOutFileName, BIG5))
                {
                    string strInfData;

                    while ((strInfData = srInFile.ReadLine()) != null)
                    {
                        DetailFileDataCount++;
                    }
                }
                #endregion

                #region 比對Head檔和明細檔資料筆數
                if (FILE_COUNT != DetailFileDataCount)
                {
                    logger.strJobQueue = "市水約定回覆Head檔資料筆數：" + FILE_COUNT + "與實際明細資料筆數：" + DetailFileDataCount + "不相等，請通知系統人員! ";
                    logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                    return "B0097:" + logger.strJobQueue;
                }
                #endregion

                #region 將明細資料轉進DATA_TABLE
                strOutFileName = FILE_PATH;
                using (StreamReader srInFile = new StreamReader(strOutFileName, BIG5))
                {
                    string strInfData;

                    while ((strInfData = srInFile.ReadLine()) != null)
                    {
                        intReadInfDataCount++;

                        #region 檔案格式檢核(筆資料長度)

                        if (strInfData.Length != intDataLength)
                        {
                            logger.strJobQueue = "市水約定回覆檔中第" + intReadInfDataCount + "筆的長度有誤，請通知系統人員! 該筆實際長度為 " + strInfData.Length;
                            logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                            return "B0099:" + logger.strJobQueue;
                        }

                        #endregion

                        DataTable InfData_DataTable = new DataTable();

                        #region 依 Layout 拆解資料
                        InfData_DataTable = xml.FileLine2DataTable(BIG5, strInfData, REC_DataTable);

                        if (xml.strMSG.Length > 0)
                        {
                            logger.strJobQueue = "[FileLine2DataTable(REC)] (L" + intReadInfDataCount + ") - " + xml.strMSG.ToString().Trim();
                            logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                            return "B0099:" + logger.strJobQueue;
                        }
                        #endregion

                        #region 檢核明細資料
                        TRANS_DTE = Convert.ToString(InfData_DataTable.Rows[0]["TRANS_DTE_D"]);
                        PAY_ACCT_NBR = Convert.ToString(InfData_DataTable.Rows[0]["ACCT_NBR"]).Replace("a09", "");
                        PAY_NBR = Convert.ToString(InfData_DataTable.Rows[0]["WATER_NBR"]);
                        CHANGE_TYPE = Convert.ToString(InfData_DataTable.Rows[0]["CHANGE_REASON"]);
                        CHANGE_DTE = Convert.ToString(InfData_DataTable.Rows[0]["CHANGE_DTE"]);
                        WATER_CHANGE_DTE = Convert.ToString(InfData_DataTable.Rows[0]["WATER_CHANGE_DTE"]);
                        ERROR_CODE = Convert.ToString(InfData_DataTable.Rows[0]["ERROR_CODE"]);
                        #endregion

                        #region 更新PUBLIC_APPLY，並取得 REPLY_DTE、管理行代號、管理行名稱
                        PUBLIC_APPLY.init();
                        PUBLIC_APPLY.strWherePAY_TYPE = PAY_TYPE;
                        PUBLIC_APPLY.strWherePAY_NBR = PAY_NBR;
                        PUBLIC_APPLY.strWherePAY_ACCT_NBR = PAY_ACCT_NBR;
                        PUBLIC_APPLY.strWhereREPLY_FLAG = "Y";

                        #region 取得 REPLY_DTE、管理行代號、管理行名稱
                        PUBLIC_APPLY_RC = PUBLIC_APPLY.query_BRANCH_INF(PAY_ACCT_NBR, "PAY_ACCT_NBR");
                        switch (PUBLIC_APPLY_RC)
                        {
                            case "S0000": //查詢成功   
                                int intYYY = Convert.ToInt32(Convert.ToString(PUBLIC_APPLY.resultTable.Rows[0]["REPLY_DTE"]).PadRight(8, ' ').Substring(0, 4)) - 1911; //民國年
                                REPLY_DTE = Convert.ToString(intYYY) + Convert.ToString(PUBLIC_APPLY.resultTable.Rows[0]["REPLY_DTE"]).PadRight(8, ' ').Substring(4, 4);
                                BR_NO = Convert.ToString(PUBLIC_APPLY.resultTable.Rows[0]["BR_NO"]);
                                BR_NAME = Convert.ToString(PUBLIC_APPLY.resultTable.Rows[0]["BR_NAME"]);
                                break;
                            case "F0023":
                                REPLY_DTE = "";
                                BR_NO = "0000";
                                BR_NAME = "";
                                break;
                            default: //資料庫錯誤
                                logger.strJobQueue = "查詢PUBLIC_APPLY.query_BRANCH_INF() 資料錯誤:" + PUBLIC_APPLY_RC;
                                logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                                return "B0016:" + logger.strJobQueue;
                        }
                        #endregion

                        PUBLIC_APPLY.strERROR_REASON = ERROR_CODE;
                        PUBLIC_APPLY.strERROR_REASON_DT = TODAY_PROCESS_DTE.ToString("yyyyMMdd");
                        PUBLIC_APPLY_RC = PUBLIC_APPLY.update();
                        switch (PUBLIC_APPLY_RC)
                        {
                            case "S0000": //更新成功   
                                PUBLIC_APPLY_Update_Count = PUBLIC_APPLY_Update_Count + PUBLIC_APPLY.intUptCnt;
                                if (PUBLIC_APPLY.intUptCnt == 0)
                                {
                                    logger.strJobQueue = "更新PUBLIC_APPLY 資料錯誤: F0023, PAY_ACCT_NBR = " + PAY_ACCT_NBR + " PAY_NBR = " + PAY_NBR;
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

                        #region 將約定失敗的資料寫入報表Table
                        intFailCount++;
                        insertReport_TABLE();
                        #endregion
                    }
                }
                #endregion

                #region REPORT_TABLE 排序
                if (intReadInfDataCount > 0)
                {
                    REPORT_TABLE.DefaultView.Sort = "KEY, REPLY_DTE asc"; //KEY = BR_NO
                    REPORT_TABLE_SORT = REPORT_TABLE.DefaultView.ToTable();

                    string thisKEY = "";
                    string nextKEY = "";
                    int FAIL_CNT = 0;
                    int PAGE_CNT = 1;
                    int index = 0;
                    for (int s = 0; s < REPORT_TABLE_SORT.Rows.Count; s++)
                    {
                        thisKEY = REPORT_TABLE_SORT.Rows[s]["KEY"].ToString().Trim();
                        FAIL_CNT = FAIL_CNT + 1;
                        if (s < REPORT_TABLE_SORT.Rows.Count - 1)
                        {
                            nextKEY = REPORT_TABLE_SORT.Rows[s + 1]["KEY"].ToString().Trim();
                            if (thisKEY != nextKEY)
                            {
                                inSubtotTable.Rows.Add(inSubtotTable.NewRow());
                                inSubtotTable.Rows[index]["KEY"] = thisKEY;
                                inSubtotTable.Rows[index]["BR_NAME"] = REPORT_TABLE_SORT.Rows[s]["BR_NAME"].ToString().Trim();
                                inSubtotTable.Rows[index]["SUB_FAIL_CNT"] = FAIL_CNT.ToString("###,##0").PadLeft(7, ' ');
                                //inSubtotTable.Rows[index]["SUB_CNT"] = PAGE_CNT.ToString("##0").PadLeft(3, ' '); //頁數
                                FAIL_CNT = 0;
                                index = index + 1;
                                PAGE_CNT = PAGE_CNT + 1;
                            }
                        }
                        else
                        {
                            inSubtotTable.Rows.Add(inSubtotTable.NewRow());
                            inSubtotTable.Rows[index]["KEY"] = thisKEY;
                            inSubtotTable.Rows[index]["BR_NAME"] = REPORT_TABLE_SORT.Rows[s]["BR_NAME"].ToString().Trim();
                            inSubtotTable.Rows[index]["SUB_FAIL_CNT"] = FAIL_CNT.ToString("###,##0").PadLeft(7, ' ');
                            //inSubtotTable.Rows[index]["SUB_CNT"] = PAGE_CNT.ToString("##0").PadLeft(3, ' '); //頁數
                        }
                    }
                }
                #endregion

                writeReport_CAD0002C(REPORT_TABLE_SORT);

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

        #region insertReport_TABLE
        void insertReport_TABLE()
        {
            REPORT_TABLE.Rows.Add();
            k = REPORT_TABLE.Rows.Count - 1;
            REPORT_TABLE.Rows[k]["KEY"] = BR_NO;
            REPORT_TABLE.Rows[k]["BR_NAME"] = BR_NAME;
            REPORT_TABLE.Rows[k]["REPLY_DTE"] = REPLY_DTE;
            REPORT_TABLE.Rows[k]["RPT_SEQ"] = REPORT_TABLE.Rows.Count.ToString().PadLeft(7, '0');
            REPORT_TABLE.Rows[k]["TRANS_DTE"] = TRANS_DTE;
            REPORT_TABLE.Rows[k]["PAY_NBR"] = PAY_NBR;
            REPORT_TABLE.Rows[k]["ACCT_NBR"] = PAY_ACCT_NBR;
            REPORT_TABLE.Rows[k]["CHANGE_DTE"] = CHANGE_DTE;
            REPORT_TABLE.Rows[k]["WATER_CHANGE_DTE"] = WATER_CHANGE_DTE;

            #region CHANGE_TYPE
            switch (CHANGE_TYPE)
            {
                case "1":
                    REPORT_TABLE.Rows[k]["CHANGE_REASON"] = CHANGE_TYPE + "-新增";
                    break;

                case "2":
                    REPORT_TABLE.Rows[k]["CHANGE_REASON"] = CHANGE_TYPE + "-更改";
                    break;

                case "3":
                    REPORT_TABLE.Rows[k]["CHANGE_REASON"] = CHANGE_TYPE + "-刪除";
                    break;

                default:
                    REPORT_TABLE.Rows[k]["CHANGE_REASON"] = CHANGE_TYPE + "-異動種類不明";
                    break;
            }
            #endregion

            #region ERROR_CODE
            switch (ERROR_CODE)
            {
                case "01":
                    REPORT_TABLE.Rows[k]["ERROR_CODE"] = ERROR_CODE + "-水號錯誤";
                    break;

                default:
                    REPORT_TABLE.Rows[k]["ERROR_CODE"] = ERROR_CODE + "-ERROR_CODE不明";
                    break;
            }
            #endregion
        }
        #endregion

        #region writeReport_CAD0002C
        void writeReport_CAD0002C(DataTable inTable)
        {
            CMCRPT002 CMCRPT002 = new CMCRPT002();
            CMCRPT002.TemplateFileName = "CAD0002C";  //範本檔名
            CMCRPT002.isOverWrite = "Y";  //產出是否覆蓋既有檔案
            CMCRPT002.MaxPageCount = 50;  //產出報表每幾筆換一頁 
            //設定表頭欄位資料
            ArrayList alHeaderData = new ArrayList();
            alHeaderData.Add(new ListItem("RPT_DTE_YYYYMMDD", DateTime.Now.ToString("yyyy/MM/dd")));
            alHeaderData.Add(new ListItem("RPT_DTE_HHmm", DateTime.Now.ToString("HH:mm")));
            //設定表尾欄位資料
            ArrayList alTrailerData = new ArrayList();
            //設定小計資料表

            //產出報表(TXT)
            string rc = CMCRPT002.Output(inTable, alHeaderData, null, inSubtotTable, "CAD0002C", TODAY_PROCESS_DTE);
            if (rc != "")
            {
                logger.strJobQueue = rc;
                logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
            }
        }
        #endregion

        #region Display
        void writeDisplay()
        {
            logger.strJobID = "00002";

            //中華電信約定回應檔筆數(含頭尾)
            logger.strTBL_NAME = "TPEWATER_CHGIN";
            logger.strTBL_ACCESS = "I";
            logger.intTBL_COUNT = intReadInfDataCount;
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

