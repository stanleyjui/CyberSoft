using System;
using System.Data;
using Cybersoft.Data;
using Cybersoft.Data.DAL;
using Cybersoft.Log;
using Cybersoft.Base;

namespace Cybersoft.Biz.Cust
{
    /// <summary>
    /// PBBGDT001，取得系統資訊
    /// </summary>
    public class PBBGDT001
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
        #region 宣告共用變數
        //DATE
        #region DATE
        DateTime PREV_PROCESS_DTE = new DateTime();
        DateTime NEXT_PROCESS_DTE = new DateTime();
        DateTime TODAY_PROCESS_DTE = new DateTime();
        DateTime BANK_PREV_PROCESS_DTE = new DateTime();
        DateTime BANK_NEXT_PROCESS_DTE = new DateTime();
        DateTime BANK_TODAY_PROCESS_DTE = new DateTime();
        DateTime PREV_CYCLE_DTE = new DateTime();
        DateTime PREV_MON_END_DTE = new DateTime();
        #endregion
        //FLAG
        #region FLAG
        string PROCESS_CYCLE = "";
        string PROCESS_MON_END = "";
        string PROCESS_WEEK_END = "";
        string BANK_PROCESS_CYCLE = "";
        string BANK_PROCESS_MON_END = "";
        string BANK_PROCESS_WEEK_END = "";
        #endregion
        #endregion
        static readonly Cybersoft.Coca.Log.Logger logger = Cybersoft.Log.LogManager<Cybersoft.Coca.Log.Logger>.GetLogger(System.Reflection.MethodInfo.GetCurrentMethod().ReflectedType.Name);
        public string RUN()
        {
            #region 準備MSGLG.SYSOUT CLASS
            logger.strJobID = JobID;
            logger.strJOBNAME = "PBBGDT001";
            logger.dtRunDate = DateTime.Now;
            #endregion
            try
            {
                #region 取得系統資訊
                CMCSYS001 SYSINF = new CMCSYS001();
                SYSINF.strJobID = JobID;
                SYSINF.strJobName = "PBBGDT001";
                String SYSINF_RC = SYSINF.getSYSINF();
                #endregion
                string result = "";
                switch (RunCode)
                {
                    //DATE
                    #region DATE
                    case "PREV_PROCESS_DTE":
                        PREV_PROCESS_DTE = SYSINF.datetimePREV_PROCESS_DTE;
                        result = PREV_PROCESS_DTE.ToString("yyyyMMdd");
                        break;
                    case "NEXT_PROCESS_DTE":
                        NEXT_PROCESS_DTE = SYSINF.datetimeNEXT_PROCESS_DTE;
                        result = NEXT_PROCESS_DTE.ToString("yyyyMMdd");
                        break;
                    case "TODAY_PROCESS_DTE":
                        TODAY_PROCESS_DTE = SYSINF.datetimeTODAY_PROCESS_DTE;
                        result = TODAY_PROCESS_DTE.ToString("yyyyMMdd");
                        break;
                    case "BANK_PREV_PROCESS_DTE":
                        BANK_PREV_PROCESS_DTE = SYSINF.datetimeBANK_PREV_PROCESS_DTE;
                        result = BANK_PREV_PROCESS_DTE.ToString("yyyyMMdd");
                        break;
                    case "BANK_NEXT_PROCESS_DTE":
                        BANK_NEXT_PROCESS_DTE = SYSINF.datetimeBANK_NEXT_PROCESS_DTE;
                        result = BANK_NEXT_PROCESS_DTE.ToString("yyyyMMdd");
                        break;
                    case "BANK_TODAY_PROCESS_DTE":
                        BANK_TODAY_PROCESS_DTE = SYSINF.datetimeBANK_TODAY_PROCESS_DTE;
                        result = BANK_TODAY_PROCESS_DTE.ToString("yyyyMMdd");
                        break;
                    case "PREV_CYCLE_DTE":
                        PREV_CYCLE_DTE = SYSINF.datetimePREV_CYCLE_DTE;
                        result = PREV_CYCLE_DTE.ToString("yyyyMMdd");
                        break;
                    case "PREV_MON_END_DTE":
                        PREV_MON_END_DTE = SYSINF.datetimePREV_MON_END_DTE;
                        result = PREV_MON_END_DTE.ToString("yyyyMMdd");
                        break;
                    #endregion
                    //FLAG
                    #region FLAG
                    //Y = 1 , N = 0
                    case "PROCESS_CYCLE":
                        PROCESS_CYCLE = SYSINF.strPROCESS_CYCLE;
                        result = PROCESS_CYCLE;
                        break;
                    case "PROCESS_MON_END":
                        PROCESS_MON_END = SYSINF.strPROCESS_MON_END;
                        result = PROCESS_MON_END;
                        break;
                    case "PROCESS_WEEK_END":
                        PROCESS_WEEK_END = SYSINF.strPROCESS_WEEK_END;
                        result = PROCESS_WEEK_END;
                        break;
                    case "BANK_PROCESS_CYCLE":
                        BANK_PROCESS_CYCLE = SYSINF.strBANK_PROCESS_CYCLE;
                        result = BANK_PROCESS_CYCLE;
                        break;
                    case "BANK_PROCESS_MON_END":
                        BANK_PROCESS_MON_END = SYSINF.strBANK_PROCESS_MON_END;
                        result = BANK_PROCESS_MON_END;
                        break;
                    case "BANK_PROCESS_WEEK_END":
                        BANK_PROCESS_WEEK_END = SYSINF.strBANK_PROCESS_WEEK_END;
                        result = BANK_PROCESS_WEEK_END;
                        break;
                    #endregion
                    default:
                        result = "99";
                        logger.strJobQueue = "請輸入正確參數!目前參數為：" + RunCode;
                        logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                        break;
                }
                if ("Y".Equals(result))
                {
                    result = "1";
                }
                if ("N".Equals(result))
                {
                    result = "0";
                }
                logger.strJobQueue = "目前參數為：" + RunCode + "，取得回傳值為：" + result;
                logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                return result;
            }
            catch (Exception e)
            {
                logger.strJobQueue = e.ToString();
                logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                return "99";
            }
        }
    }
}

