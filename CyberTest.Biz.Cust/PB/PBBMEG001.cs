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
    /// PBBMEG001, 合併卡系統及優利檔案。 
    /// </summary>
    /// 

    public class PBBMEG001
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
        
        #region 宣告常數
        DateTime dtNow = DateTime.Now;
        const string strJOBNAME = "PBBMEG001";
        #endregion

        #region 宣告變數
        String strTmp = "";
        DateTime dtTmp = new DateTime(1900, 1, 1);

        String strSourPath = "";
        String strSourFileName = "";
        String strDestPath = "";
        String strDestFileName = "";
        string CMCURL_RC = "";
        #endregion

        #region 宣告共用類別
        static readonly Cybersoft.Coca.Log.Logger logger = Cybersoft.Log.LogManager<Cybersoft.Coca.Log.Logger>.GetLogger(System.Reflection.MethodInfo.GetCurrentMethod().ReflectedType.Name);
        #endregion

        //========================================================================================================
        #region 程式主邏輯【MAIN Routine】
        public string RUN(String getFILE_NAME)
        {
            #region 準備MSGLG.SYSOUT CLASS
            logger.strJobID = JobID;
            logger.strJOBNAME = strJOBNAME;
            logger.dtRunDate = dtNow;
            #endregion

            try
            {
                #region  取得系統資訊(前次批次日、本日批次日、下一批次日)
                CMCSYS001 SYSINF = new CMCSYS001();
                SYSINF.strJobID = JobID;
                SYSINF.strJobName = strJOBNAME;
                String SYSINF_RC = SYSINF.getSYSINF();
                switch (SYSINF_RC.Substring(0, 5))
                {
                    case "S0000":  // 成功
                        break;
                    default:       // 資料庫錯誤
                        logger.strJobQueue = "[SYSINF.getSYSINF()]：" + SYSINF_RC;
                        logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                        return "B0016:" + logger.strJobQueue;
                }
                //DateTime dtPREV_PROCESS_DTE = SYSINF.datetimeBANK_PREV_PROCESS_DTE;
                DateTime dtPREV_PROCESS_DTE = SYSINF.datetimeBANK_PREV_PROCESS_DTE;
                DateTime dtTODAY_PROCESS_DTE = SYSINF.datetimeTODAY_PROCESS_DTE;
                DateTime dtNEXT_PROCESS_DTE = SYSINF.datetimeNEXT_PROCESS_DTE;
                #endregion

                #region  設定收檔路徑
                Cybersoft.Base.CMCURL001 CMCURL = new CMCURL001();
                // 取得來源路徑與檔名
                //strSourPath = CMCURL.getPATH(getFILE_NAME);
                //strSourFileName = CMCURL.getFILE_NAME(getFILE_NAME);
                // 取得目的路徑與檔名
                strSourPath = CMCURL.getURL(getFILE_NAME);
                strSourFileName = CMCURL.getFILE_NAME(getFILE_NAME);
                strSourFileName = strSourFileName.Replace("yyyymmdd", dtPREV_PROCESS_DTE.ToString("yyyyMMdd"));  //替換日期
                strDestPath = CMCURL.getURL(getFILE_NAME);
                strDestFileName = CMCURL.getFILE_NAME(getFILE_NAME);
                strDestFileName = strSourFileName.Replace("yyyymmdd", dtPREV_PROCESS_DTE.ToString("yyyyMMdd"));  //替換日期
                #endregion

                #region 執行收檔
                #region 新增本日資料夾
                String strBackupPath = strDestPath + "SYS_" + dtPREV_PROCESS_DTE.ToString("yyyyMMdd").ToString().Trim() + @"\";
                if (!Directory.Exists(strBackupPath))
                {
                    Directory.CreateDirectory(strBackupPath);
                }
                #endregion

                #region 判斷優利檔案是否存在, 不存在即不併檔
                string Check_RC = CMCURL.isFileExists(strSourPath+strSourFileName+"US").ToString();
                if (Check_RC.Substring(0, 5) != "S0000")
                {
                    File.Copy(strSourPath + strSourFileName + "CA", strSourPath + strSourFileName);
                    logger.strJobQueue = Check_RC.Trim();
                    logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                    return "B0000";
                }
                #endregion

                #region  撈取中心符合的檔案並複製至 File Server 本日資料夾中
                //string strTmpSour = "";
                //string strTmpDest = "";
                //去最後一位逗點
                strTmp = strSourFileName + "US," + strSourFileName + "CA";
                //strTmp = strTmp.Replace("yyyymmdd", dtPREV_PROCESS_DTE.ToString("yyyyMMdd"));
                
                CMCURL_RC = CMCURL.MergerFile(strSourPath, strTmp, strDestPath, strDestFileName, "N", "Y", Encoding.GetEncoding("big5"));
                switch (CMCURL_RC.Substring(0, 5))
                {
                    case "S0000":  //修改成功
                        break;
                    default:       //資料庫錯誤
                        logger.strJobQueue = "[CMCURL.MergerFile()]：" + CMCURL_RC;
                        logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                        return "B0099:" + logger.strJobQueue;
                }
                logger.strJobQueue = CMCURL_RC;
                logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);

                // 將檔案移至資料 strBackupPath 中
                CMCURL_RC = CMCURL.MoveFile(strSourPath, strTmp, strBackupPath, "N", "Y", "Y");
                switch (CMCURL_RC.Substring(0, 5))
                {
                    case "S0000":  //修改成功
                        break;
                    default:       //資料庫錯誤
                        logger.strJobQueue = "[CMCURL.MoveFile()]：" + CMCURL_RC;
                        logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                        return "B0099:" + logger.strJobQueue;
                }
                logger.strJobQueue = CMCURL_RC;
                logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                #endregion
                #endregion

                //========================================================================================================
                //9000-CLOSE-RTN批次結束
                writeDisplay();
                return "B0000";

            }
            catch (Exception e)
            {
                logger.strJobQueue = strJOBNAME + e.ToString();
                logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                return "B0099:" + e.ToString();
            }
            finally
            {

            }
        }
        #endregion
        //========================================================================================================
        #region SYSOUT
        void writeDisplay()
        {
            //DISPLAY 
            logger.DisplayCounter(Cybersoft.Coca.Log.LevelEnum.Info);
        }
        #endregion
    }
}
