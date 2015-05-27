using System;
using System.Data;
using System.Collections;
using System.Configuration;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Text;

/// <summary>
/// PUBLIC_APPLYDao，Provide PUBLIC_APPLYCreate/Read/Update/Delete Function
/// 2014/12/31 DB Log_Cybersoft.Dao.Core.DAOBase
/// </summary>

namespace Cybersoft.Data.DAL
{
    //public partial class PUBLIC_APPLYDao : Cybersoft.Data.DAOBase
    public partial class testPUBLIC_APPLYDao : Cybersoft.Dao.Core.DAOBase
    {
        #region Modify History
        /// <history>
        /// <design>
        /// <name>Cybersoft.COCA DaoGenerator</name>
        /// <date>2014/12/19 下午 04:50:54</date>
        #endregion
        #region DataBase message convert
        Cybersoft.Dao.Core.MSG_DB MSG = new Cybersoft.Dao.Core.MSG_DB();
        #endregion
        #region Property(Field)
        private string PAY_CARD_NBR = null;
        public string strPAY_CARD_NBR
        {
            get { return PAY_CARD_NBR; }
            set { PAY_CARD_NBR = value; }
        }
        private string PAY_TYPE = null;
        public string strPAY_TYPE
        {
            get { return PAY_TYPE; }
            set { PAY_TYPE = value; }
        }
        private string PAY_NBR = null;
        public string strPAY_NBR
        {
            get { return PAY_NBR; }
            set { PAY_NBR = value; }
        }
        private string SEQ = null;
        public string strSEQ
        {
            get { return SEQ; }
            set { SEQ = value; }
        }
        private string BU = null;
        public string strBU
        {
            get { return BU; }
            set { BU = value; }
        }
        private string PRODUCT = null;
        public string strPRODUCT
        {
            get { return PRODUCT; }
            set { PRODUCT = value; }
        }
        private string CARD_PRODUCT = null;
        public string strCARD_PRODUCT
        {
            get { return CARD_PRODUCT; }
            set { CARD_PRODUCT = value; }
        }
        private string ACCT_NBR = null;
        public string strACCT_NBR
        {
            get { return ACCT_NBR; }
            set { ACCT_NBR = value; }
        }
        private string PAY_ACCT_NBR = null;
        public string strPAY_ACCT_NBR
        {
            get { return PAY_ACCT_NBR; }
            set { PAY_ACCT_NBR = value; }
        }
        private string PAY_CARD_NBR_PREV = null;
        public string strPAY_CARD_NBR_PREV
        {
            get { return PAY_CARD_NBR_PREV; }
            set { PAY_CARD_NBR_PREV = value; }
        }
        private string PAY_ACCT_NBR_PREV = null;
        public string strPAY_ACCT_NBR_PREV
        {
            get { return PAY_ACCT_NBR_PREV; }
            set { PAY_ACCT_NBR_PREV = value; }
        }
        private string CUST_SEQ = null;
        public string strCUST_SEQ
        {
            get { return CUST_SEQ; }
            set { CUST_SEQ = value; }
        }
        private string EXPIR_DTE = null;
        public string strEXPIR_DTE
        {
            get { return EXPIR_DTE; }
            set { EXPIR_DTE = value; }
        }
        private string APPLY_DTE = null;
        public string strAPPLY_DTE
        {
            get { return APPLY_DTE; }
            set { APPLY_DTE = value; }
        }
        private string FIRST_DTE = null;
        public string strFIRST_DTE
        {
            get { return FIRST_DTE; }
            set { FIRST_DTE = value; }
        }
        private string PAY_DTE = null;
        public string strPAY_DTE
        {
            get { return PAY_DTE; }
            set { PAY_DTE = value; }
        }
        private string VAILD_FLAG = null;
        public string strVAILD_FLAG
        {
            get { return VAILD_FLAG; }
            set { VAILD_FLAG = value; }
        }
        private string SEND_MSG_FLAG = null;
        public string strSEND_MSG_FLAG
        {
            get { return SEND_MSG_FLAG; }
            set { SEND_MSG_FLAG = value; }
        }
        private string REPLY_FLAG = null;
        public string strREPLY_FLAG
        {
            get { return REPLY_FLAG; }
            set { REPLY_FLAG = value; }
        }
        private string REPLY_DTE = null;
        public string strREPLY_DTE
        {
            get { return REPLY_DTE; }
            set { REPLY_DTE = value; }
        }
        private DateTime STOP_DTE = new DateTime(1900, 1, 1);
        public DateTime datetimeSTOP_DTE
        {
            get { return STOP_DTE; }
            set { STOP_DTE = value; }
        }
        private string STOP_USER = null;
        public string strSTOP_USER
        {
            get { return STOP_USER; }
            set { STOP_USER = value; }
        }
        private DateTime CREATE_DT = new DateTime(1900, 1, 1);
        public DateTime datetimeCREATE_DT
        {
            get { return CREATE_DT; }
            set { CREATE_DT = value; }
        }
        private string CREATE_USER = null;
        public string strCREATE_USER
        {
            get { return CREATE_USER; }
            set { CREATE_USER = value; }
        }
        private string ERROR_REASON = null;
        public string strERROR_REASON
        {
            get { return ERROR_REASON; }
            set { ERROR_REASON = value; }
        }
        private string ERROR_REASON_DT = null;
        public string strERROR_REASON_DT
        {
            get { return ERROR_REASON_DT; }
            set { ERROR_REASON_DT = value; }
        }
        private string PUBLIC_APPLY_CHAR_1 = null;
        public string strPUBLIC_APPLY_CHAR_1
        {
            get { return PUBLIC_APPLY_CHAR_1; }
            set { PUBLIC_APPLY_CHAR_1 = value; }
        }
        private string PUBLIC_APPLY_CHAR_2 = null;
        public string strPUBLIC_APPLY_CHAR_2
        {
            get { return PUBLIC_APPLY_CHAR_2; }
            set { PUBLIC_APPLY_CHAR_2 = value; }
        }
        private string PUBLIC_APPLY_CHAR_3 = null;
        public string strPUBLIC_APPLY_CHAR_3
        {
            get { return PUBLIC_APPLY_CHAR_3; }
            set { PUBLIC_APPLY_CHAR_3 = value; }
        }
        private string PUBLIC_APPLY_CHAR_4 = null;
        public string strPUBLIC_APPLY_CHAR_4
        {
            get { return PUBLIC_APPLY_CHAR_4; }
            set { PUBLIC_APPLY_CHAR_4 = value; }
        }
        private string PUBLIC_APPLY_CHAR_5 = null;
        public string strPUBLIC_APPLY_CHAR_5
        {
            get { return PUBLIC_APPLY_CHAR_5; }
            set { PUBLIC_APPLY_CHAR_5 = value; }
        }
        private decimal PUBLIC_APPLY_AMT_1 = -1000000000000;
        public decimal decPUBLIC_APPLY_AMT_1
        {
            get { return PUBLIC_APPLY_AMT_1; }
            set { PUBLIC_APPLY_AMT_1 = value; }
        }
        private decimal PUBLIC_APPLY_AMT_2 = -1000000000000;
        public decimal decPUBLIC_APPLY_AMT_2
        {
            get { return PUBLIC_APPLY_AMT_2; }
            set { PUBLIC_APPLY_AMT_2 = value; }
        }
        private decimal PUBLIC_APPLY_AMT_3 = -1000000000000;
        public decimal decPUBLIC_APPLY_AMT_3
        {
            get { return PUBLIC_APPLY_AMT_3; }
            set { PUBLIC_APPLY_AMT_3 = value; }
        }
        private decimal PUBLIC_APPLY_AMT_4 = -1000000000000;
        public decimal decPUBLIC_APPLY_AMT_4
        {
            get { return PUBLIC_APPLY_AMT_4; }
            set { PUBLIC_APPLY_AMT_4 = value; }
        }
        private decimal PUBLIC_APPLY_AMT_5 = -1000000000000;
        public decimal decPUBLIC_APPLY_AMT_5
        {
            get { return PUBLIC_APPLY_AMT_5; }
            set { PUBLIC_APPLY_AMT_5 = value; }
        }
        private DateTime PUBLIC_APPLY_DT_1 = new DateTime(1900, 1, 1);
        public DateTime datetimePUBLIC_APPLY_DT_1
        {
            get { return PUBLIC_APPLY_DT_1; }
            set { PUBLIC_APPLY_DT_1 = value; }
        }
        private DateTime PUBLIC_APPLY_DT_2 = new DateTime(1900, 1, 1);
        public DateTime datetimePUBLIC_APPLY_DT_2
        {
            get { return PUBLIC_APPLY_DT_2; }
            set { PUBLIC_APPLY_DT_2 = value; }
        }
        private DateTime PUBLIC_APPLY_DT_3 = new DateTime(1900, 1, 1);
        public DateTime datetimePUBLIC_APPLY_DT_3
        {
            get { return PUBLIC_APPLY_DT_3; }
            set { PUBLIC_APPLY_DT_3 = value; }
        }
        private DateTime PUBLIC_APPLY_DT_4 = new DateTime(1900, 1, 1);
        public DateTime datetimePUBLIC_APPLY_DT_4
        {
            get { return PUBLIC_APPLY_DT_4; }
            set { PUBLIC_APPLY_DT_4 = value; }
        }
        private DateTime PUBLIC_APPLY_DT_5 = new DateTime(1900, 1, 1);
        public DateTime datetimePUBLIC_APPLY_DT_5
        {
            get { return PUBLIC_APPLY_DT_5; }
            set { PUBLIC_APPLY_DT_5 = value; }
        }
        private DateTime MNT_DT = new DateTime(1900, 1, 1);
        public DateTime datetimeMNT_DT
        {
            get { return MNT_DT; }
            set { MNT_DT = value; }
        }
        private string MNT_USER = null;
        public string strMNT_USER
        {
            get { return MNT_USER; }
            set { MNT_USER = value; }
        }
        #endregion
        #region Property(Where condtion)
        private string wherePAY_CARD_NBR = null;
        public string strWherePAY_CARD_NBR
        {
            get { return wherePAY_CARD_NBR; }
            set { wherePAY_CARD_NBR = value; }
        }
        private string wherePAY_TYPE = null;
        public string strWherePAY_TYPE
        {
            get { return wherePAY_TYPE; }
            set { wherePAY_TYPE = value; }
        }
        private string wherePAY_NBR = null;
        public string strWherePAY_NBR
        {
            get { return wherePAY_NBR; }
            set { wherePAY_NBR = value; }
        }
        private string whereSEQ = null;
        public string strWhereSEQ
        {
            get { return whereSEQ; }
            set { whereSEQ = value; }
        }
        private string whereBU = null;
        public string strWhereBU
        {
            get { return whereBU; }
            set { whereBU = value; }
        }
        private string wherePRODUCT = null;
        public string strWherePRODUCT
        {
            get { return wherePRODUCT; }
            set { wherePRODUCT = value; }
        }
        private string whereCARD_PRODUCT = null;
        public string strWhereCARD_PRODUCT
        {
            get { return whereCARD_PRODUCT; }
            set { whereCARD_PRODUCT = value; }
        }
        private string whereACCT_NBR = null;
        public string strWhereACCT_NBR
        {
            get { return whereACCT_NBR; }
            set { whereACCT_NBR = value; }
        }
        private string wherePAY_ACCT_NBR = null;
        public string strWherePAY_ACCT_NBR
        {
            get { return wherePAY_ACCT_NBR; }
            set { wherePAY_ACCT_NBR = value; }
        }
        private string wherePAY_CARD_NBR_PREV = null;
        public string strWherePAY_CARD_NBR_PREV
        {
            get { return wherePAY_CARD_NBR_PREV; }
            set { wherePAY_CARD_NBR_PREV = value; }
        }
        private string wherePAY_ACCT_NBR_PREV = null;
        public string strWherePAY_ACCT_NBR_PREV
        {
            get { return wherePAY_ACCT_NBR_PREV; }
            set { wherePAY_ACCT_NBR_PREV = value; }
        }
        private string whereCUST_SEQ = null;
        public string strWhereCUST_SEQ
        {
            get { return whereCUST_SEQ; }
            set { whereCUST_SEQ = value; }
        }
        private string whereEXPIR_DTE = null;
        public string strWhereEXPIR_DTE
        {
            get { return whereEXPIR_DTE; }
            set { whereEXPIR_DTE = value; }
        }
        private string whereAPPLY_DTE = null;
        public string strWhereAPPLY_DTE
        {
            get { return whereAPPLY_DTE; }
            set { whereAPPLY_DTE = value; }
        }
        private string whereFIRST_DTE = null;
        public string strWhereFIRST_DTE
        {
            get { return whereFIRST_DTE; }
            set { whereFIRST_DTE = value; }
        }
        private string wherePAY_DTE = null;
        public string strWherePAY_DTE
        {
            get { return wherePAY_DTE; }
            set { wherePAY_DTE = value; }
        }
        private string whereVAILD_FLAG = null;
        public string strWhereVAILD_FLAG
        {
            get { return whereVAILD_FLAG; }
            set { whereVAILD_FLAG = value; }
        }
        private string whereSEND_MSG_FLAG = null;
        public string strWhereSEND_MSG_FLAG
        {
            get { return whereSEND_MSG_FLAG; }
            set { whereSEND_MSG_FLAG = value; }
        }
        private string whereREPLY_FLAG = null;
        public string strWhereREPLY_FLAG
        {
            get { return whereREPLY_FLAG; }
            set { whereREPLY_FLAG = value; }
        }
        private string whereREPLY_DTE = null;
        public string strWhereREPLY_DTE
        {
            get { return whereREPLY_DTE; }
            set { whereREPLY_DTE = value; }
        }
        private DateTime whereSTOP_DTE = new DateTime(1900, 1, 1);
        public DateTime DateTimeWhereSTOP_DTE
        {
            get { return whereSTOP_DTE; }
            set { whereSTOP_DTE = value; }
        }
        private string whereSTOP_USER = null;
        public string strWhereSTOP_USER
        {
            get { return whereSTOP_USER; }
            set { whereSTOP_USER = value; }
        }
        private DateTime whereCREATE_DT = new DateTime(1900, 1, 1);
        public DateTime DateTimeWhereCREATE_DT
        {
            get { return whereCREATE_DT; }
            set { whereCREATE_DT = value; }
        }
        private string whereCREATE_USER = null;
        public string strWhereCREATE_USER
        {
            get { return whereCREATE_USER; }
            set { whereCREATE_USER = value; }
        }
        private string whereERROR_REASON = null;
        public string strWhereERROR_REASON
        {
            get { return whereERROR_REASON; }
            set { whereERROR_REASON = value; }
        }
        private string whereERROR_REASON_DT = null;
        public string strWhereERROR_REASON_DT
        {
            get { return whereERROR_REASON_DT; }
            set { whereERROR_REASON_DT = value; }
        }
        private string wherePUBLIC_APPLY_CHAR_1 = null;
        public string strWherePUBLIC_APPLY_CHAR_1
        {
            get { return wherePUBLIC_APPLY_CHAR_1; }
            set { wherePUBLIC_APPLY_CHAR_1 = value; }
        }
        private string wherePUBLIC_APPLY_CHAR_2 = null;
        public string strWherePUBLIC_APPLY_CHAR_2
        {
            get { return wherePUBLIC_APPLY_CHAR_2; }
            set { wherePUBLIC_APPLY_CHAR_2 = value; }
        }
        private string wherePUBLIC_APPLY_CHAR_3 = null;
        public string strWherePUBLIC_APPLY_CHAR_3
        {
            get { return wherePUBLIC_APPLY_CHAR_3; }
            set { wherePUBLIC_APPLY_CHAR_3 = value; }
        }
        private string wherePUBLIC_APPLY_CHAR_4 = null;
        public string strWherePUBLIC_APPLY_CHAR_4
        {
            get { return wherePUBLIC_APPLY_CHAR_4; }
            set { wherePUBLIC_APPLY_CHAR_4 = value; }
        }
        private string wherePUBLIC_APPLY_CHAR_5 = null;
        public string strWherePUBLIC_APPLY_CHAR_5
        {
            get { return wherePUBLIC_APPLY_CHAR_5; }
            set { wherePUBLIC_APPLY_CHAR_5 = value; }
        }
        private decimal wherePUBLIC_APPLY_AMT_1 = -1000000000000;
        public decimal decWherePUBLIC_APPLY_AMT_1
        {
            get { return wherePUBLIC_APPLY_AMT_1; }
            set { wherePUBLIC_APPLY_AMT_1 = value; }
        }
        private decimal wherePUBLIC_APPLY_AMT_2 = -1000000000000;
        public decimal decWherePUBLIC_APPLY_AMT_2
        {
            get { return wherePUBLIC_APPLY_AMT_2; }
            set { wherePUBLIC_APPLY_AMT_2 = value; }
        }
        private decimal wherePUBLIC_APPLY_AMT_3 = -1000000000000;
        public decimal decWherePUBLIC_APPLY_AMT_3
        {
            get { return wherePUBLIC_APPLY_AMT_3; }
            set { wherePUBLIC_APPLY_AMT_3 = value; }
        }
        private decimal wherePUBLIC_APPLY_AMT_4 = -1000000000000;
        public decimal decWherePUBLIC_APPLY_AMT_4
        {
            get { return wherePUBLIC_APPLY_AMT_4; }
            set { wherePUBLIC_APPLY_AMT_4 = value; }
        }
        private decimal wherePUBLIC_APPLY_AMT_5 = -1000000000000;
        public decimal decWherePUBLIC_APPLY_AMT_5
        {
            get { return wherePUBLIC_APPLY_AMT_5; }
            set { wherePUBLIC_APPLY_AMT_5 = value; }
        }
        private DateTime wherePUBLIC_APPLY_DT_1 = new DateTime(1900, 1, 1);
        public DateTime DateTimeWherePUBLIC_APPLY_DT_1
        {
            get { return wherePUBLIC_APPLY_DT_1; }
            set { wherePUBLIC_APPLY_DT_1 = value; }
        }
        private DateTime wherePUBLIC_APPLY_DT_2 = new DateTime(1900, 1, 1);
        public DateTime DateTimeWherePUBLIC_APPLY_DT_2
        {
            get { return wherePUBLIC_APPLY_DT_2; }
            set { wherePUBLIC_APPLY_DT_2 = value; }
        }
        private DateTime wherePUBLIC_APPLY_DT_3 = new DateTime(1900, 1, 1);
        public DateTime DateTimeWherePUBLIC_APPLY_DT_3
        {
            get { return wherePUBLIC_APPLY_DT_3; }
            set { wherePUBLIC_APPLY_DT_3 = value; }
        }
        private DateTime wherePUBLIC_APPLY_DT_4 = new DateTime(1900, 1, 1);
        public DateTime DateTimeWherePUBLIC_APPLY_DT_4
        {
            get { return wherePUBLIC_APPLY_DT_4; }
            set { wherePUBLIC_APPLY_DT_4 = value; }
        }
        private DateTime wherePUBLIC_APPLY_DT_5 = new DateTime(1900, 1, 1);
        public DateTime DateTimeWherePUBLIC_APPLY_DT_5
        {
            get { return wherePUBLIC_APPLY_DT_5; }
            set { wherePUBLIC_APPLY_DT_5 = value; }
        }
        private DateTime whereMNT_DT = new DateTime(1900, 1, 1);
        public DateTime DateTimeWhereMNT_DT
        {
            get { return whereMNT_DT; }
            set { whereMNT_DT = value; }
        }
        private string whereMNT_USER = null;
        public string strWhereMNT_USER
        {
            get { return whereMNT_USER; }
            set { whereMNT_USER = value; }
        }
        #endregion
        #region Property(field initial value)
        private string initPAY_CARD_NBR = "";
        public string strinitPAY_CARD_NBR
        {
            get { return initPAY_CARD_NBR; }
            set { initPAY_CARD_NBR = value; }
        }
        private string initPAY_TYPE = "";
        public string strinitPAY_TYPE
        {
            get { return initPAY_TYPE; }
            set { initPAY_TYPE = value; }
        }
        private string initPAY_NBR = "";
        public string strinitPAY_NBR
        {
            get { return initPAY_NBR; }
            set { initPAY_NBR = value; }
        }
        private string initSEQ = "";
        public string strinitSEQ
        {
            get { return initSEQ; }
            set { initSEQ = value; }
        }
        private string initBU = "";
        public string strinitBU
        {
            get { return initBU; }
            set { initBU = value; }
        }
        private string initPRODUCT = "";
        public string strinitPRODUCT
        {
            get { return initPRODUCT; }
            set { initPRODUCT = value; }
        }
        private string initCARD_PRODUCT = "";
        public string strinitCARD_PRODUCT
        {
            get { return initCARD_PRODUCT; }
            set { initCARD_PRODUCT = value; }
        }
        private string initACCT_NBR = "";
        public string strinitACCT_NBR
        {
            get { return initACCT_NBR; }
            set { initACCT_NBR = value; }
        }
        private string initPAY_ACCT_NBR = "";
        public string strinitPAY_ACCT_NBR
        {
            get { return initPAY_ACCT_NBR; }
            set { initPAY_ACCT_NBR = value; }
        }
        private string initPAY_CARD_NBR_PREV = "";
        public string strinitPAY_CARD_NBR_PREV
        {
            get { return initPAY_CARD_NBR_PREV; }
            set { initPAY_CARD_NBR_PREV = value; }
        }
        private string initPAY_ACCT_NBR_PREV = "";
        public string strinitPAY_ACCT_NBR_PREV
        {
            get { return initPAY_ACCT_NBR_PREV; }
            set { initPAY_ACCT_NBR_PREV = value; }
        }
        private string initCUST_SEQ = "";
        public string strinitCUST_SEQ
        {
            get { return initCUST_SEQ; }
            set { initCUST_SEQ = value; }
        }
        private string initEXPIR_DTE = "";
        public string strinitEXPIR_DTE
        {
            get { return initEXPIR_DTE; }
            set { initEXPIR_DTE = value; }
        }
        private string initAPPLY_DTE = "";
        public string strinitAPPLY_DTE
        {
            get { return initAPPLY_DTE; }
            set { initAPPLY_DTE = value; }
        }
        private string initFIRST_DTE = "";
        public string strinitFIRST_DTE
        {
            get { return initFIRST_DTE; }
            set { initFIRST_DTE = value; }
        }
        private string initPAY_DTE = "";
        public string strinitPAY_DTE
        {
            get { return initPAY_DTE; }
            set { initPAY_DTE = value; }
        }
        private string initVAILD_FLAG = "N";
        public string strinitVAILD_FLAG
        {
            get { return initVAILD_FLAG; }
            set { initVAILD_FLAG = value; }
        }
        private string initSEND_MSG_FLAG = "N";
        public string strinitSEND_MSG_FLAG
        {
            get { return initSEND_MSG_FLAG; }
            set { initSEND_MSG_FLAG = value; }
        }
        private string initREPLY_FLAG = "N";
        public string strinitREPLY_FLAG
        {
            get { return initREPLY_FLAG; }
            set { initREPLY_FLAG = value; }
        }
        private string initREPLY_DTE = "";
        public string strinitREPLY_DTE
        {
            get { return initREPLY_DTE; }
            set { initREPLY_DTE = value; }
        }
        private DateTime initSTOP_DTE = new DateTime(1900, 1, 1);
        public DateTime DateTimeinitSTOP_DTE
        {
            get { return initSTOP_DTE; }
            set { initSTOP_DTE = value; }
        }
        private string initSTOP_USER = "";
        public string strinitSTOP_USER
        {
            get { return initSTOP_USER; }
            set { initSTOP_USER = value; }
        }
        private DateTime initCREATE_DT = new DateTime(1900, 1, 1);
        public DateTime DateTimeinitCREATE_DT
        {
            get { return initCREATE_DT; }
            set { initCREATE_DT = value; }
        }
        private string initCREATE_USER = "";
        public string strinitCREATE_USER
        {
            get { return initCREATE_USER; }
            set { initCREATE_USER = value; }
        }
        private string initERROR_REASON = "";
        public string strinitERROR_REASON
        {
            get { return initERROR_REASON; }
            set { initERROR_REASON = value; }
        }
        private string initERROR_REASON_DT = "";
        public string strinitERROR_REASON_DT
        {
            get { return initERROR_REASON_DT; }
            set { initERROR_REASON_DT = value; }
        }
        private string initPUBLIC_APPLY_CHAR_1 = "";
        public string strinitPUBLIC_APPLY_CHAR_1
        {
            get { return initPUBLIC_APPLY_CHAR_1; }
            set { initPUBLIC_APPLY_CHAR_1 = value; }
        }
        private string initPUBLIC_APPLY_CHAR_2 = "";
        public string strinitPUBLIC_APPLY_CHAR_2
        {
            get { return initPUBLIC_APPLY_CHAR_2; }
            set { initPUBLIC_APPLY_CHAR_2 = value; }
        }
        private string initPUBLIC_APPLY_CHAR_3 = "";
        public string strinitPUBLIC_APPLY_CHAR_3
        {
            get { return initPUBLIC_APPLY_CHAR_3; }
            set { initPUBLIC_APPLY_CHAR_3 = value; }
        }
        private string initPUBLIC_APPLY_CHAR_4 = "";
        public string strinitPUBLIC_APPLY_CHAR_4
        {
            get { return initPUBLIC_APPLY_CHAR_4; }
            set { initPUBLIC_APPLY_CHAR_4 = value; }
        }
        private string initPUBLIC_APPLY_CHAR_5 = "";
        public string strinitPUBLIC_APPLY_CHAR_5
        {
            get { return initPUBLIC_APPLY_CHAR_5; }
            set { initPUBLIC_APPLY_CHAR_5 = value; }
        }
        private decimal initPUBLIC_APPLY_AMT_1 = 0;
        public decimal decinitPUBLIC_APPLY_AMT_1
        {
            get { return initPUBLIC_APPLY_AMT_1; }
            set { initPUBLIC_APPLY_AMT_1 = value; }
        }
        private decimal initPUBLIC_APPLY_AMT_2 = 0;
        public decimal decinitPUBLIC_APPLY_AMT_2
        {
            get { return initPUBLIC_APPLY_AMT_2; }
            set { initPUBLIC_APPLY_AMT_2 = value; }
        }
        private decimal initPUBLIC_APPLY_AMT_3 = 0;
        public decimal decinitPUBLIC_APPLY_AMT_3
        {
            get { return initPUBLIC_APPLY_AMT_3; }
            set { initPUBLIC_APPLY_AMT_3 = value; }
        }
        private decimal initPUBLIC_APPLY_AMT_4 = 0;
        public decimal decinitPUBLIC_APPLY_AMT_4
        {
            get { return initPUBLIC_APPLY_AMT_4; }
            set { initPUBLIC_APPLY_AMT_4 = value; }
        }
        private decimal initPUBLIC_APPLY_AMT_5 = 0;
        public decimal decinitPUBLIC_APPLY_AMT_5
        {
            get { return initPUBLIC_APPLY_AMT_5; }
            set { initPUBLIC_APPLY_AMT_5 = value; }
        }
        private DateTime initPUBLIC_APPLY_DT_1 = new DateTime(1900, 1, 1);
        public DateTime DateTimeinitPUBLIC_APPLY_DT_1
        {
            get { return initPUBLIC_APPLY_DT_1; }
            set { initPUBLIC_APPLY_DT_1 = value; }
        }
        private DateTime initPUBLIC_APPLY_DT_2 = new DateTime(1900, 1, 1);
        public DateTime DateTimeinitPUBLIC_APPLY_DT_2
        {
            get { return initPUBLIC_APPLY_DT_2; }
            set { initPUBLIC_APPLY_DT_2 = value; }
        }
        private DateTime initPUBLIC_APPLY_DT_3 = new DateTime(1900, 1, 1);
        public DateTime DateTimeinitPUBLIC_APPLY_DT_3
        {
            get { return initPUBLIC_APPLY_DT_3; }
            set { initPUBLIC_APPLY_DT_3 = value; }
        }
        private DateTime initPUBLIC_APPLY_DT_4 = new DateTime(1900, 1, 1);
        public DateTime DateTimeinitPUBLIC_APPLY_DT_4
        {
            get { return initPUBLIC_APPLY_DT_4; }
            set { initPUBLIC_APPLY_DT_4 = value; }
        }
        private DateTime initPUBLIC_APPLY_DT_5 = new DateTime(1900, 1, 1);
        public DateTime DateTimeinitPUBLIC_APPLY_DT_5
        {
            get { return initPUBLIC_APPLY_DT_5; }
            set { initPUBLIC_APPLY_DT_5 = value; }
        }
        private DateTime initMNT_DT = new DateTime(1900, 1, 1);
        public DateTime DateTimeinitMNT_DT
        {
            get { return initMNT_DT; }
            set { initMNT_DT = value; }
        }
        private string initMNT_USER = "";
        public string strinitMNT_USER
        {
            get { return initMNT_USER; }
            set { initMNT_USER = value; }
        }
        #endregion
        
        #region Property(Group condtion)
        private string groupPAY_CARD_NBR = null;
        public string strGroupPAY_CARD_NBR
        {
            get { return groupPAY_CARD_NBR; }
            set { groupPAY_CARD_NBR = value; }
        }
        private string groupPAY_TYPE = null;
        public string strGroupPAY_TYPE
        {
            get { return groupPAY_TYPE; }
            set { groupPAY_TYPE = value; }
        }
        private string groupPAY_NBR = null;
        public string strGroupPAY_NBR
        {
            get { return groupPAY_NBR; }
            set { groupPAY_NBR = value; }
        }
        private string groupSEQ = null;
        public string strGroupSEQ
        {
            get { return groupSEQ; }
            set { groupSEQ = value; }
        }
        private string groupBU = null;
        public string strGroupBU
        {
            get { return groupBU; }
            set { groupBU = value; }
        }
        private string groupPRODUCT = null;
        public string strGroupPRODUCT
        {
            get { return groupPRODUCT; }
            set { groupPRODUCT = value; }
        }
        private string groupCARD_PRODUCT = null;
        public string strGroupCARD_PRODUCT
        {
            get { return groupCARD_PRODUCT; }
            set { groupCARD_PRODUCT = value; }
        }
        private string groupACCT_NBR = null;
        public string strGroupACCT_NBR
        {
            get { return groupACCT_NBR; }
            set { groupACCT_NBR = value; }
        }
        private string groupPAY_ACCT_NBR = null;
        public string strGroupPAY_ACCT_NBR
        {
            get { return groupPAY_ACCT_NBR; }
            set { groupPAY_ACCT_NBR = value; }
        }
        private string groupPAY_CARD_NBR_PREV = null;
        public string strGroupPAY_CARD_NBR_PREV
        {
            get { return groupPAY_CARD_NBR_PREV; }
            set { groupPAY_CARD_NBR_PREV = value; }
        }
        private string groupPAY_ACCT_NBR_PREV = null;
        public string strGroupPAY_ACCT_NBR_PREV
        {
            get { return groupPAY_ACCT_NBR_PREV; }
            set { groupPAY_ACCT_NBR_PREV = value; }
        }
        private string groupCUST_SEQ = null;
        public string strGroupCUST_SEQ
        {
            get { return groupCUST_SEQ; }
            set { groupCUST_SEQ = value; }
        }
        private string groupEXPIR_DTE = null;
        public string strGroupEXPIR_DTE
        {
            get { return groupEXPIR_DTE; }
            set { groupEXPIR_DTE = value; }
        }
        private string groupAPPLY_DTE = null;
        public string strGroupAPPLY_DTE
        {
            get { return groupAPPLY_DTE; }
            set { groupAPPLY_DTE = value; }
        }
        private string groupFIRST_DTE = null;
        public string strGroupFIRST_DTE
        {
            get { return groupFIRST_DTE; }
            set { groupFIRST_DTE = value; }
        }
        private string groupPAY_DTE = null;
        public string strGroupPAY_DTE
        {
            get { return groupPAY_DTE; }
            set { groupPAY_DTE = value; }
        }
        private string groupVAILD_FLAG = null;
        public string strGroupVAILD_FLAG
        {
            get { return groupVAILD_FLAG; }
            set { groupVAILD_FLAG = value; }
        }
        private string groupSEND_MSG_FLAG = null;
        public string strGroupSEND_MSG_FLAG
        {
            get { return groupSEND_MSG_FLAG; }
            set { groupSEND_MSG_FLAG = value; }
        }
        private string groupREPLY_FLAG = null;
        public string strGroupREPLY_FLAG
        {
            get { return groupREPLY_FLAG; }
            set { groupREPLY_FLAG = value; }
        }
        private string groupREPLY_DTE = null;
        public string strGroupREPLY_DTE
        {
            get { return groupREPLY_DTE; }
            set { groupREPLY_DTE = value; }
        }
        private string groupSTOP_DTE = null;
        public string strGroupSTOP_DTE
        {
            get { return groupSTOP_DTE; }
            set { groupSTOP_DTE = value; }
        }
        private string groupSTOP_USER = null;
        public string strGroupSTOP_USER
        {
            get { return groupSTOP_USER; }
            set { groupSTOP_USER = value; }
        }
        private string groupCREATE_DT = null;
        public string strGroupCREATE_DT
        {
            get { return groupCREATE_DT; }
            set { groupCREATE_DT = value; }
        }
        private string groupCREATE_USER = null;
        public string strGroupCREATE_USER
        {
            get { return groupCREATE_USER; }
            set { groupCREATE_USER = value; }
        }
        private string groupERROR_REASON = null;
        public string strGroupERROR_REASON
        {
            get { return groupERROR_REASON; }
            set { groupERROR_REASON = value; }
        }
        private string groupERROR_REASON_DT = null;
        public string strGroupERROR_REASON_DT
        {
            get { return groupERROR_REASON_DT; }
            set { groupERROR_REASON_DT = value; }
        }
        private string groupPUBLIC_APPLY_CHAR_1 = null;
        public string strGroupPUBLIC_APPLY_CHAR_1
        {
            get { return groupPUBLIC_APPLY_CHAR_1; }
            set { groupPUBLIC_APPLY_CHAR_1 = value; }
        }
        private string groupPUBLIC_APPLY_CHAR_2 = null;
        public string strGroupPUBLIC_APPLY_CHAR_2
        {
            get { return groupPUBLIC_APPLY_CHAR_2; }
            set { groupPUBLIC_APPLY_CHAR_2 = value; }
        }
        private string groupPUBLIC_APPLY_CHAR_3 = null;
        public string strGroupPUBLIC_APPLY_CHAR_3
        {
            get { return groupPUBLIC_APPLY_CHAR_3; }
            set { groupPUBLIC_APPLY_CHAR_3 = value; }
        }
        private string groupPUBLIC_APPLY_CHAR_4 = null;
        public string strGroupPUBLIC_APPLY_CHAR_4
        {
            get { return groupPUBLIC_APPLY_CHAR_4; }
            set { groupPUBLIC_APPLY_CHAR_4 = value; }
        }
        private string groupPUBLIC_APPLY_CHAR_5 = null;
        public string strGroupPUBLIC_APPLY_CHAR_5
        {
            get { return groupPUBLIC_APPLY_CHAR_5; }
            set { groupPUBLIC_APPLY_CHAR_5 = value; }
        }
        private string groupPUBLIC_APPLY_AMT_1 = null;
        public string strGroupPUBLIC_APPLY_AMT_1
        {
            get { return groupPUBLIC_APPLY_AMT_1; }
            set { groupPUBLIC_APPLY_AMT_1 = value; }
        }
        private string groupPUBLIC_APPLY_AMT_2 = null;
        public string strGroupPUBLIC_APPLY_AMT_2
        {
            get { return groupPUBLIC_APPLY_AMT_2; }
            set { groupPUBLIC_APPLY_AMT_2 = value; }
        }
        private string groupPUBLIC_APPLY_AMT_3 = null;
        public string strGroupPUBLIC_APPLY_AMT_3
        {
            get { return groupPUBLIC_APPLY_AMT_3; }
            set { groupPUBLIC_APPLY_AMT_3 = value; }
        }
        private string groupPUBLIC_APPLY_AMT_4 = null;
        public string strGroupPUBLIC_APPLY_AMT_4
        {
            get { return groupPUBLIC_APPLY_AMT_4; }
            set { groupPUBLIC_APPLY_AMT_4 = value; }
        }
        private string groupPUBLIC_APPLY_AMT_5 = null;
        public string strGroupPUBLIC_APPLY_AMT_5
        {
            get { return groupPUBLIC_APPLY_AMT_5; }
            set { groupPUBLIC_APPLY_AMT_5 = value; }
        }
        private string groupPUBLIC_APPLY_DT_1 = null;
        public string strGroupPUBLIC_APPLY_DT_1
        {
            get { return groupPUBLIC_APPLY_DT_1; }
            set { groupPUBLIC_APPLY_DT_1 = value; }
        }
        private string groupPUBLIC_APPLY_DT_2 = null;
        public string strGroupPUBLIC_APPLY_DT_2
        {
            get { return groupPUBLIC_APPLY_DT_2; }
            set { groupPUBLIC_APPLY_DT_2 = value; }
        }
        private string groupPUBLIC_APPLY_DT_3 = null;
        public string strGroupPUBLIC_APPLY_DT_3
        {
            get { return groupPUBLIC_APPLY_DT_3; }
            set { groupPUBLIC_APPLY_DT_3 = value; }
        }
        private string groupPUBLIC_APPLY_DT_4 = null;
        public string strGroupPUBLIC_APPLY_DT_4
        {
            get { return groupPUBLIC_APPLY_DT_4; }
            set { groupPUBLIC_APPLY_DT_4 = value; }
        }
        private string groupPUBLIC_APPLY_DT_5 = null;
        public string strGroupPUBLIC_APPLY_DT_5
        {
            get { return groupPUBLIC_APPLY_DT_5; }
            set { groupPUBLIC_APPLY_DT_5 = value; }
        }
        private string groupMNT_DT = null;
        public string strGroupMNT_DT
        {
            get { return groupMNT_DT; }
            set { groupMNT_DT = value; }
        }
        private string groupMNT_USER = null;
        public string strGroupMNT_USER
        {
            get { return groupMNT_USER; }
            set { groupMNT_USER = value; }
        }
        #endregion
        #region counter
        private int InsCnt; //insert counter
        public int intInsCnt
        {
            get { return InsCnt; }
            set { InsCnt = value; }
        }
        private int UptCnt; //update counter
        public int intUptCnt
        {
            get { return UptCnt; }
            set { UptCnt = value; }
        }
        private int DelCnt; //delete counter
        public int intDelCnt
        {
            get { return DelCnt; }
            set { DelCnt = value; }
        }
        #endregion
        #region init value/ Property(DataTable)
        DateTime dateStart = new DateTime(1900, 1, 1); //datetime 
        private string msg_code;   //message code return
        private DataTable myTable; //DataTable
        public DataTable resultTable
        {
            get { return myTable; }
            set { myTable = value; }
        }
        #endregion
        #region init()
        public void init()
        {
            #region Modify History
            /// <history>
            /// <design>
            /// <name>Cybersoft.COCA DaoGenerator</name>
            /// <date>2014/12/19 下午 04:50:54</date>
            #endregion
            initField();
            initWhere();
        }
        #endregion
        #region initField()
        public void initField()
        {
            #region Modify History
            /// <history>
            /// <design>
            /// <name>Cybersoft.COCA DaoGenerator</name>
            /// <date>2014/12/19 下午 04:50:54</date>
            #endregion
            PAY_CARD_NBR = null;
            PAY_TYPE = null;
            PAY_NBR = null;
            SEQ = null;
            BU = null;
            PRODUCT = null;
            CARD_PRODUCT = null;
            ACCT_NBR = null;
            PAY_ACCT_NBR = null;
            PAY_CARD_NBR_PREV = null;
            PAY_ACCT_NBR_PREV = null;
            CUST_SEQ = null;
            EXPIR_DTE = null;
            APPLY_DTE = null;
            FIRST_DTE = null;
            PAY_DTE = null;
            VAILD_FLAG = null;
            SEND_MSG_FLAG = null;
            REPLY_FLAG = null;
            REPLY_DTE = null;
            STOP_DTE = dateStart;
            STOP_USER = null;
            CREATE_DT = dateStart;
            CREATE_USER = null;
            ERROR_REASON = null;
            ERROR_REASON_DT = null;
            PUBLIC_APPLY_CHAR_1 = null;
            PUBLIC_APPLY_CHAR_2 = null;
            PUBLIC_APPLY_CHAR_3 = null;
            PUBLIC_APPLY_CHAR_4 = null;
            PUBLIC_APPLY_CHAR_5 = null;
            PUBLIC_APPLY_AMT_1 = -1000000000000;
            PUBLIC_APPLY_AMT_2 = -1000000000000;
            PUBLIC_APPLY_AMT_3 = -1000000000000;
            PUBLIC_APPLY_AMT_4 = -1000000000000;
            PUBLIC_APPLY_AMT_5 = -1000000000000;
            PUBLIC_APPLY_DT_1 = dateStart;
            PUBLIC_APPLY_DT_2 = dateStart;
            PUBLIC_APPLY_DT_3 = dateStart;
            PUBLIC_APPLY_DT_4 = dateStart;
            PUBLIC_APPLY_DT_5 = dateStart;
            MNT_DT = dateStart;
            MNT_USER = null;
        }
        #endregion
        #region initWhere()
        public void initWhere()
        {
            #region Modify History
            /// <history>
            /// <design>
            /// <name>Cybersoft.COCA DaoGenerator</name>
            /// <date>2014/12/19 下午 04:50:54</date>
            #endregion
            wherePAY_CARD_NBR = null;
            wherePAY_TYPE = null;
            wherePAY_NBR = null;
            whereSEQ = null;
            whereBU = null;
            wherePRODUCT = null;
            whereCARD_PRODUCT = null;
            whereACCT_NBR = null;
            wherePAY_ACCT_NBR = null;
            wherePAY_CARD_NBR_PREV = null;
            wherePAY_ACCT_NBR_PREV = null;
            whereCUST_SEQ = null;
            whereEXPIR_DTE = null;
            whereAPPLY_DTE = null;
            whereFIRST_DTE = null;
            wherePAY_DTE = null;
            whereVAILD_FLAG = null;
            whereSEND_MSG_FLAG = null;
            whereREPLY_FLAG = null;
            whereREPLY_DTE = null;
            whereSTOP_DTE = dateStart;
            whereSTOP_USER = null;
            whereCREATE_DT = dateStart;
            whereCREATE_USER = null;
            whereERROR_REASON = null;
            whereERROR_REASON_DT = null;
            wherePUBLIC_APPLY_CHAR_1 = null;
            wherePUBLIC_APPLY_CHAR_2 = null;
            wherePUBLIC_APPLY_CHAR_3 = null;
            wherePUBLIC_APPLY_CHAR_4 = null;
            wherePUBLIC_APPLY_CHAR_5 = null;
            wherePUBLIC_APPLY_AMT_1 = -1000000000000;
            wherePUBLIC_APPLY_AMT_2 = -1000000000000;
            wherePUBLIC_APPLY_AMT_3 = -1000000000000;
            wherePUBLIC_APPLY_AMT_4 = -1000000000000;
            wherePUBLIC_APPLY_AMT_5 = -1000000000000;
            wherePUBLIC_APPLY_DT_1 = dateStart;
            wherePUBLIC_APPLY_DT_2 = dateStart;
            wherePUBLIC_APPLY_DT_3 = dateStart;
            wherePUBLIC_APPLY_DT_4 = dateStart;
            wherePUBLIC_APPLY_DT_5 = dateStart;
            whereMNT_DT = dateStart;
            whereMNT_USER = null;
        }
        #endregion
        #region initInsert()
        public void initInsert()
        {
            #region Modify History
            /// <history>
            /// <design>
            /// <name>Cybersoft.COCA DaoGenerator</name>
            /// <date>2014/12/19 下午 04:50:54</date>
            #endregion
            PAY_CARD_NBR = initPAY_CARD_NBR;
            PAY_TYPE = initPAY_TYPE;
            PAY_NBR = initPAY_NBR;
            SEQ = initSEQ;
            BU = initBU;
            PRODUCT = initPRODUCT;
            CARD_PRODUCT = initCARD_PRODUCT;
            ACCT_NBR = initACCT_NBR;
            PAY_ACCT_NBR = initPAY_ACCT_NBR;
            PAY_CARD_NBR_PREV = initPAY_CARD_NBR_PREV;
            PAY_ACCT_NBR_PREV = initPAY_ACCT_NBR_PREV;
            CUST_SEQ = initCUST_SEQ;
            EXPIR_DTE = initEXPIR_DTE;
            APPLY_DTE = initAPPLY_DTE;
            FIRST_DTE = initFIRST_DTE;
            PAY_DTE = initPAY_DTE;
            VAILD_FLAG = initVAILD_FLAG;
            SEND_MSG_FLAG = initSEND_MSG_FLAG;
            REPLY_FLAG = initREPLY_FLAG;
            REPLY_DTE = initREPLY_DTE;
            STOP_DTE = initSTOP_DTE;
            STOP_USER = initSTOP_USER;
            CREATE_DT = initCREATE_DT;
            CREATE_USER = initCREATE_USER;
            ERROR_REASON = initERROR_REASON;
            ERROR_REASON_DT = initERROR_REASON_DT;
            PUBLIC_APPLY_CHAR_1 = initPUBLIC_APPLY_CHAR_1;
            PUBLIC_APPLY_CHAR_2 = initPUBLIC_APPLY_CHAR_2;
            PUBLIC_APPLY_CHAR_3 = initPUBLIC_APPLY_CHAR_3;
            PUBLIC_APPLY_CHAR_4 = initPUBLIC_APPLY_CHAR_4;
            PUBLIC_APPLY_CHAR_5 = initPUBLIC_APPLY_CHAR_5;
            PUBLIC_APPLY_AMT_1 = initPUBLIC_APPLY_AMT_1;
            PUBLIC_APPLY_AMT_2 = initPUBLIC_APPLY_AMT_2;
            PUBLIC_APPLY_AMT_3 = initPUBLIC_APPLY_AMT_3;
            PUBLIC_APPLY_AMT_4 = initPUBLIC_APPLY_AMT_4;
            PUBLIC_APPLY_AMT_5 = initPUBLIC_APPLY_AMT_5;
            PUBLIC_APPLY_DT_1 = initPUBLIC_APPLY_DT_1;
            PUBLIC_APPLY_DT_2 = initPUBLIC_APPLY_DT_2;
            PUBLIC_APPLY_DT_3 = initPUBLIC_APPLY_DT_3;
            PUBLIC_APPLY_DT_4 = initPUBLIC_APPLY_DT_4;
            PUBLIC_APPLY_DT_5 = initPUBLIC_APPLY_DT_5;
            MNT_DT = initMNT_DT;
            MNT_USER = initMNT_USER;
        }
        #endregion
        #region initInsert_row()
        public string initInsert_row()
        {
            #region Modify History
            /// <history>
            /// <design>
            /// <name>Cybersoft.COCA DaoGenerator</name>
            /// <date>2014/12/19 下午 04:50:54</date>
            #endregion
            try
            {
                //建立 DataRow物件
                DataRow DR = myTable.NewRow();
                //欄位搬初始值迴圈
                DR["PAY_CARD_NBR"] = initPAY_CARD_NBR;
                DR["PAY_TYPE"] = initPAY_TYPE;
                DR["PAY_NBR"] = initPAY_NBR;
                DR["SEQ"] = initSEQ;
                DR["BU"] = initBU;
                DR["PRODUCT"] = initPRODUCT;
                DR["CARD_PRODUCT"] = initCARD_PRODUCT;
                DR["ACCT_NBR"] = initACCT_NBR;
                DR["PAY_ACCT_NBR"] = initPAY_ACCT_NBR;
                DR["PAY_CARD_NBR_PREV"] = initPAY_CARD_NBR_PREV;
                DR["PAY_ACCT_NBR_PREV"] = initPAY_ACCT_NBR_PREV;
                DR["CUST_SEQ"] = initCUST_SEQ;
                DR["EXPIR_DTE"] = initEXPIR_DTE;
                DR["APPLY_DTE"] = initAPPLY_DTE;
                DR["FIRST_DTE"] = initFIRST_DTE;
                DR["PAY_DTE"] = initPAY_DTE;
                DR["VAILD_FLAG"] = initVAILD_FLAG;
                DR["SEND_MSG_FLAG"] = initSEND_MSG_FLAG;
                DR["REPLY_FLAG"] = initREPLY_FLAG;
                DR["REPLY_DTE"] = initREPLY_DTE;
                DR["STOP_DTE"] = initSTOP_DTE;
                DR["STOP_USER"] = initSTOP_USER;
                DR["CREATE_DT"] = initCREATE_DT;
                DR["CREATE_USER"] = initCREATE_USER;
                DR["ERROR_REASON"] = initERROR_REASON;
                DR["ERROR_REASON_DT"] = initERROR_REASON_DT;
                DR["PUBLIC_APPLY_CHAR_1"] = initPUBLIC_APPLY_CHAR_1;
                DR["PUBLIC_APPLY_CHAR_2"] = initPUBLIC_APPLY_CHAR_2;
                DR["PUBLIC_APPLY_CHAR_3"] = initPUBLIC_APPLY_CHAR_3;
                DR["PUBLIC_APPLY_CHAR_4"] = initPUBLIC_APPLY_CHAR_4;
                DR["PUBLIC_APPLY_CHAR_5"] = initPUBLIC_APPLY_CHAR_5;
                DR["PUBLIC_APPLY_AMT_1"] = initPUBLIC_APPLY_AMT_1;
                DR["PUBLIC_APPLY_AMT_2"] = initPUBLIC_APPLY_AMT_2;
                DR["PUBLIC_APPLY_AMT_3"] = initPUBLIC_APPLY_AMT_3;
                DR["PUBLIC_APPLY_AMT_4"] = initPUBLIC_APPLY_AMT_4;
                DR["PUBLIC_APPLY_AMT_5"] = initPUBLIC_APPLY_AMT_5;
                DR["PUBLIC_APPLY_DT_1"] = initPUBLIC_APPLY_DT_1;
                DR["PUBLIC_APPLY_DT_2"] = initPUBLIC_APPLY_DT_2;
                DR["PUBLIC_APPLY_DT_3"] = initPUBLIC_APPLY_DT_3;
                DR["PUBLIC_APPLY_DT_4"] = initPUBLIC_APPLY_DT_4;
                DR["PUBLIC_APPLY_DT_5"] = initPUBLIC_APPLY_DT_5;
                DR["MNT_DT"] = initMNT_DT;
                DR["MNT_USER"] = initMNT_USER;
                //新增 DataRow
                myTable.Rows.Add(DR);
                msg_code = "S0000"; //新增成功代碼
            }
            catch (Exception e)
            {
                msg_code = e.ToString();
            }
            return msg_code;
        }
        #endregion
        #region table_define()
        public void table_define()
        {
            #region Modify History
            /// <history>
            /// <design>
            /// <name>Cybersoft.COCA DaoGenerator</name>
            /// <date>2014/12/19 下午 04:50:54</date>
            #endregion
            try
            {
                //建立 Table物件
                myTable = new DataTable();
                //建立 Table欄位
                myTable.Columns.Add("PAY_CARD_NBR", typeof(string));
                myTable.Columns.Add("PAY_TYPE", typeof(string));
                myTable.Columns.Add("PAY_NBR", typeof(string));
                myTable.Columns.Add("SEQ", typeof(string));
                myTable.Columns.Add("BU", typeof(string));
                myTable.Columns.Add("PRODUCT", typeof(string));
                myTable.Columns.Add("CARD_PRODUCT", typeof(string));
                myTable.Columns.Add("ACCT_NBR", typeof(string));
                myTable.Columns.Add("PAY_ACCT_NBR", typeof(string));
                myTable.Columns.Add("PAY_CARD_NBR_PREV", typeof(string));
                myTable.Columns.Add("PAY_ACCT_NBR_PREV", typeof(string));
                myTable.Columns.Add("CUST_SEQ", typeof(string));
                myTable.Columns.Add("EXPIR_DTE", typeof(string));
                myTable.Columns.Add("APPLY_DTE", typeof(string));
                myTable.Columns.Add("FIRST_DTE", typeof(string));
                myTable.Columns.Add("PAY_DTE", typeof(string));
                myTable.Columns.Add("VAILD_FLAG", typeof(string));
                myTable.Columns.Add("SEND_MSG_FLAG", typeof(string));
                myTable.Columns.Add("REPLY_FLAG", typeof(string));
                myTable.Columns.Add("REPLY_DTE", typeof(string));
                myTable.Columns.Add("STOP_DTE", typeof(DateTime));
                myTable.Columns.Add("STOP_USER", typeof(string));
                myTable.Columns.Add("CREATE_DT", typeof(DateTime));
                myTable.Columns.Add("CREATE_USER", typeof(string));
                myTable.Columns.Add("ERROR_REASON", typeof(string));
                myTable.Columns.Add("ERROR_REASON_DT", typeof(string));
                myTable.Columns.Add("PUBLIC_APPLY_CHAR_1", typeof(string));
                myTable.Columns.Add("PUBLIC_APPLY_CHAR_2", typeof(string));
                myTable.Columns.Add("PUBLIC_APPLY_CHAR_3", typeof(string));
                myTable.Columns.Add("PUBLIC_APPLY_CHAR_4", typeof(string));
                myTable.Columns.Add("PUBLIC_APPLY_CHAR_5", typeof(string));
                myTable.Columns.Add("PUBLIC_APPLY_AMT_1", typeof(decimal));
                myTable.Columns.Add("PUBLIC_APPLY_AMT_2", typeof(decimal));
                myTable.Columns.Add("PUBLIC_APPLY_AMT_3", typeof(decimal));
                myTable.Columns.Add("PUBLIC_APPLY_AMT_4", typeof(decimal));
                myTable.Columns.Add("PUBLIC_APPLY_AMT_5", typeof(decimal));
                myTable.Columns.Add("PUBLIC_APPLY_DT_1", typeof(DateTime));
                myTable.Columns.Add("PUBLIC_APPLY_DT_2", typeof(DateTime));
                myTable.Columns.Add("PUBLIC_APPLY_DT_3", typeof(DateTime));
                myTable.Columns.Add("PUBLIC_APPLY_DT_4", typeof(DateTime));
                myTable.Columns.Add("PUBLIC_APPLY_DT_5", typeof(DateTime));
                myTable.Columns.Add("MNT_DT", typeof(DateTime));
                myTable.Columns.Add("MNT_USER", typeof(string));
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        #endregion
        #region insert_by_DT()
        public string insert_by_DT()
        {
            #region Modify History
            /// <history>
            /// <design>
            /// <name>Cybersoft.COCA DaoGenerator</name>
            /// <date>2014/12/19 下午 04:50:54</date>
            #endregion
            try
            {
                int rowCount = Cybersoft.Data.DAL.Common.BatchInsert(myTable, "PUBLIC_APPLY");
                if (myTable.Rows.Count != rowCount)
                {
                    msg_code = "F0047"; //Error 
                }
                else
                {
                    msg_code = "S0000"; //Success
                    InsCnt = rowCount;
                }
            }
            catch (SqlException e)
            {
                MSG.strMsg = Convert.ToString(e.Number) + '-' + e.Message;
                msg_code = MSG.getMsg();
            }
            finally
            {

            }
            return msg_code;
        }
        #endregion
        #region query()
        public string query()
        {
            #region Modify History
            /// <history>
            /// <design>
            /// <name>Cybersoft.COCA DaoGenerator</name>
            /// <date>2014/12/19 下午 04:50:54</date>
            #endregion
            try
            {
                #region set SQL statement
                StringBuilder sbstrSQL = new StringBuilder();
                sbstrSQL.Append("SELECT a.PAY_CARD_NBR,a.PAY_TYPE,a.PAY_NBR,a.SEQ,a.BU,a.PRODUCT,a.CARD_PRODUCT,a.ACCT_NBR,a.PAY_ACCT_NBR,a.PAY_CARD_NBR_PREV,a.PAY_ACCT_NBR_PREV,a.CUST_SEQ,a.EXPIR_DTE,a.APPLY_DTE,a.FIRST_DTE,a.PAY_DTE,a.VAILD_FLAG,a.SEND_MSG_FLAG,a.REPLY_FLAG,a.REPLY_DTE,a.STOP_DTE,a.STOP_USER,a.CREATE_DT,a.CREATE_USER,a.ERROR_REASON,a.ERROR_REASON_DT,a.PUBLIC_APPLY_CHAR_1,a.PUBLIC_APPLY_CHAR_2,a.PUBLIC_APPLY_CHAR_3,a.PUBLIC_APPLY_CHAR_4,a.PUBLIC_APPLY_CHAR_5,a.PUBLIC_APPLY_AMT_1,a.PUBLIC_APPLY_AMT_2,a.PUBLIC_APPLY_AMT_3,a.PUBLIC_APPLY_AMT_4,a.PUBLIC_APPLY_AMT_5,a.PUBLIC_APPLY_DT_1,a.PUBLIC_APPLY_DT_2,a.PUBLIC_APPLY_DT_3,a.PUBLIC_APPLY_DT_4,a.PUBLIC_APPLY_DT_5,a.MNT_DT,a.MNT_USER FROM PUBLIC_APPLY a where 1=1 ");
                if (this.wherePAY_CARD_NBR != null)
                {
                    sbstrSQL.Append(" and a.PAY_CARD_NBR=@wherePAY_CARD_NBR ");
                }
                if (this.wherePAY_TYPE != null)
                {
                    sbstrSQL.Append(" and a.PAY_TYPE=@wherePAY_TYPE ");
                }
                if (this.wherePAY_NBR != null)
                {
                    sbstrSQL.Append(" and a.PAY_NBR=@wherePAY_NBR ");
                }
                if (this.whereSEQ != null)
                {
                    sbstrSQL.Append(" and a.SEQ=@whereSEQ ");
                }
                if (this.whereBU != null)
                {
                    sbstrSQL.Append(" and a.BU=@whereBU ");
                }
                if (this.wherePRODUCT != null)
                {
                    sbstrSQL.Append(" and a.PRODUCT=@wherePRODUCT ");
                }
                if (this.whereCARD_PRODUCT != null)
                {
                    sbstrSQL.Append(" and a.CARD_PRODUCT=@whereCARD_PRODUCT ");
                }
                if (this.whereACCT_NBR != null)
                {
                    sbstrSQL.Append(" and a.ACCT_NBR=@whereACCT_NBR ");
                }
                if (this.wherePAY_ACCT_NBR != null)
                {
                    sbstrSQL.Append(" and a.PAY_ACCT_NBR=@wherePAY_ACCT_NBR ");
                }
                if (this.wherePAY_CARD_NBR_PREV != null)
                {
                    sbstrSQL.Append(" and a.PAY_CARD_NBR_PREV=@wherePAY_CARD_NBR_PREV ");
                }
                if (this.wherePAY_ACCT_NBR_PREV != null)
                {
                    sbstrSQL.Append(" and a.PAY_ACCT_NBR_PREV=@wherePAY_ACCT_NBR_PREV ");
                }
                if (this.whereCUST_SEQ != null)
                {
                    sbstrSQL.Append(" and a.CUST_SEQ=@whereCUST_SEQ ");
                }
                if (this.whereEXPIR_DTE != null)
                {
                    sbstrSQL.Append(" and a.EXPIR_DTE=@whereEXPIR_DTE ");
                }
                if (this.whereAPPLY_DTE != null)
                {
                    sbstrSQL.Append(" and a.APPLY_DTE=@whereAPPLY_DTE ");
                }
                if (this.whereFIRST_DTE != null)
                {
                    sbstrSQL.Append(" and a.FIRST_DTE=@whereFIRST_DTE ");
                }
                if (this.wherePAY_DTE != null)
                {
                    sbstrSQL.Append(" and a.PAY_DTE=@wherePAY_DTE ");
                }
                if (this.whereVAILD_FLAG != null)
                {
                    sbstrSQL.Append(" and a.VAILD_FLAG=@whereVAILD_FLAG ");
                }
                if (this.whereSEND_MSG_FLAG != null)
                {
                    sbstrSQL.Append(" and a.SEND_MSG_FLAG=@whereSEND_MSG_FLAG ");
                }
                if (this.whereREPLY_FLAG != null)
                {
                    sbstrSQL.Append(" and a.REPLY_FLAG=@whereREPLY_FLAG ");
                }
                if (this.whereREPLY_DTE != null)
                {
                    sbstrSQL.Append(" and a.REPLY_DTE=@whereREPLY_DTE ");
                }
                if (this.whereSTOP_DTE > dateStart)
                {
                    sbstrSQL.Append("  and a.STOP_DTE=@whereSTOP_DTE ");
                }
                if (this.whereSTOP_USER != null)
                {
                    sbstrSQL.Append(" and a.STOP_USER=@whereSTOP_USER ");
                }
                if (this.whereCREATE_DT > dateStart)
                {
                    sbstrSQL.Append("  and a.CREATE_DT=@whereCREATE_DT ");
                }
                if (this.whereCREATE_USER != null)
                {
                    sbstrSQL.Append(" and a.CREATE_USER=@whereCREATE_USER ");
                }
                if (this.whereERROR_REASON != null)
                {
                    sbstrSQL.Append(" and a.ERROR_REASON=@whereERROR_REASON ");
                }
                if (this.whereERROR_REASON_DT != null)
                {
                    sbstrSQL.Append(" and a.ERROR_REASON_DT=@whereERROR_REASON_DT ");
                }
                if (this.wherePUBLIC_APPLY_CHAR_1 != null)
                {
                    sbstrSQL.Append(" and a.PUBLIC_APPLY_CHAR_1=@wherePUBLIC_APPLY_CHAR_1 ");
                }
                if (this.wherePUBLIC_APPLY_CHAR_2 != null)
                {
                    sbstrSQL.Append(" and a.PUBLIC_APPLY_CHAR_2=@wherePUBLIC_APPLY_CHAR_2 ");
                }
                if (this.wherePUBLIC_APPLY_CHAR_3 != null)
                {
                    sbstrSQL.Append(" and a.PUBLIC_APPLY_CHAR_3=@wherePUBLIC_APPLY_CHAR_3 ");
                }
                if (this.wherePUBLIC_APPLY_CHAR_4 != null)
                {
                    sbstrSQL.Append(" and a.PUBLIC_APPLY_CHAR_4=@wherePUBLIC_APPLY_CHAR_4 ");
                }
                if (this.wherePUBLIC_APPLY_CHAR_5 != null)
                {
                    sbstrSQL.Append(" and a.PUBLIC_APPLY_CHAR_5=@wherePUBLIC_APPLY_CHAR_5 ");
                }
                if (this.wherePUBLIC_APPLY_AMT_1 > -1000000000000)
                {
                    sbstrSQL.Append(" and a.PUBLIC_APPLY_AMT_1=@wherePUBLIC_APPLY_AMT_1 ");
                }
                if (this.wherePUBLIC_APPLY_AMT_2 > -1000000000000)
                {
                    sbstrSQL.Append(" and a.PUBLIC_APPLY_AMT_2=@wherePUBLIC_APPLY_AMT_2 ");
                }
                if (this.wherePUBLIC_APPLY_AMT_3 > -1000000000000)
                {
                    sbstrSQL.Append(" and a.PUBLIC_APPLY_AMT_3=@wherePUBLIC_APPLY_AMT_3 ");
                }
                if (this.wherePUBLIC_APPLY_AMT_4 > -1000000000000)
                {
                    sbstrSQL.Append(" and a.PUBLIC_APPLY_AMT_4=@wherePUBLIC_APPLY_AMT_4 ");
                }
                if (this.wherePUBLIC_APPLY_AMT_5 > -1000000000000)
                {
                    sbstrSQL.Append(" and a.PUBLIC_APPLY_AMT_5=@wherePUBLIC_APPLY_AMT_5 ");
                }
                if (this.wherePUBLIC_APPLY_DT_1 > dateStart)
                {
                    sbstrSQL.Append("  and a.PUBLIC_APPLY_DT_1=@wherePUBLIC_APPLY_DT_1 ");
                }
                if (this.wherePUBLIC_APPLY_DT_2 > dateStart)
                {
                    sbstrSQL.Append("  and a.PUBLIC_APPLY_DT_2=@wherePUBLIC_APPLY_DT_2 ");
                }
                if (this.wherePUBLIC_APPLY_DT_3 > dateStart)
                {
                    sbstrSQL.Append("  and a.PUBLIC_APPLY_DT_3=@wherePUBLIC_APPLY_DT_3 ");
                }
                if (this.wherePUBLIC_APPLY_DT_4 > dateStart)
                {
                    sbstrSQL.Append("  and a.PUBLIC_APPLY_DT_4=@wherePUBLIC_APPLY_DT_4 ");
                }
                if (this.wherePUBLIC_APPLY_DT_5 > dateStart)
                {
                    sbstrSQL.Append("  and a.PUBLIC_APPLY_DT_5=@wherePUBLIC_APPLY_DT_5 ");
                }
                if (this.whereMNT_DT > dateStart)
                {
                    sbstrSQL.Append("  and a.MNT_DT=@whereMNT_DT ");
                }
                if (this.whereMNT_USER != null)
                {
                    sbstrSQL.Append(" and a.MNT_USER=@whereMNT_USER ");
                }
                #endregion
                this.SelectOperator.SqlText = sbstrSQL.ToString();
                #region replace SQL parameter
                if (sbstrSQL.ToString().Contains("@wherePAY_CARD_NBR "))
                {
                    this.SelectOperator.SetValue("@wherePAY_CARD_NBR", this.wherePAY_CARD_NBR, SqlDbType.VarChar);
                }
                if (sbstrSQL.ToString().Contains("@wherePAY_TYPE "))
                {
                    this.SelectOperator.SetValue("@wherePAY_TYPE", this.wherePAY_TYPE, SqlDbType.VarChar);
                }
                if (sbstrSQL.ToString().Contains("@wherePAY_NBR "))
                {
                    this.SelectOperator.SetValue("@wherePAY_NBR", this.wherePAY_NBR, SqlDbType.VarChar);
                }
                if (sbstrSQL.ToString().Contains("@whereSEQ "))
                {
                    this.SelectOperator.SetValue("@whereSEQ", this.whereSEQ, SqlDbType.VarChar);
                }
                if (sbstrSQL.ToString().Contains("@whereBU "))
                {
                    this.SelectOperator.SetValue("@whereBU", this.whereBU, SqlDbType.VarChar);
                }
                if (sbstrSQL.ToString().Contains("@wherePRODUCT "))
                {
                    this.SelectOperator.SetValue("@wherePRODUCT", this.wherePRODUCT, SqlDbType.VarChar);
                }
                if (sbstrSQL.ToString().Contains("@whereCARD_PRODUCT "))
                {
                    this.SelectOperator.SetValue("@whereCARD_PRODUCT", this.whereCARD_PRODUCT, SqlDbType.VarChar);
                }
                if (sbstrSQL.ToString().Contains("@whereACCT_NBR "))
                {
                    this.SelectOperator.SetValue("@whereACCT_NBR", this.whereACCT_NBR, SqlDbType.VarChar);
                }
                if (sbstrSQL.ToString().Contains("@wherePAY_ACCT_NBR "))
                {
                    this.SelectOperator.SetValue("@wherePAY_ACCT_NBR", this.wherePAY_ACCT_NBR, SqlDbType.VarChar);
                }
                if (sbstrSQL.ToString().Contains("@wherePAY_CARD_NBR_PREV "))
                {
                    this.SelectOperator.SetValue("@wherePAY_CARD_NBR_PREV", this.wherePAY_CARD_NBR_PREV, SqlDbType.VarChar);
                }
                if (sbstrSQL.ToString().Contains("@wherePAY_ACCT_NBR_PREV "))
                {
                    this.SelectOperator.SetValue("@wherePAY_ACCT_NBR_PREV", this.wherePAY_ACCT_NBR_PREV, SqlDbType.VarChar);
                }
                if (sbstrSQL.ToString().Contains("@whereCUST_SEQ "))
                {
                    this.SelectOperator.SetValue("@whereCUST_SEQ", this.whereCUST_SEQ, SqlDbType.VarChar);
                }
                if (sbstrSQL.ToString().Contains("@whereEXPIR_DTE "))
                {
                    this.SelectOperator.SetValue("@whereEXPIR_DTE", this.whereEXPIR_DTE, SqlDbType.VarChar);
                }
                if (sbstrSQL.ToString().Contains("@whereAPPLY_DTE "))
                {
                    this.SelectOperator.SetValue("@whereAPPLY_DTE", this.whereAPPLY_DTE, SqlDbType.VarChar);
                }
                if (sbstrSQL.ToString().Contains("@whereFIRST_DTE "))
                {
                    this.SelectOperator.SetValue("@whereFIRST_DTE", this.whereFIRST_DTE, SqlDbType.VarChar);
                }
                if (sbstrSQL.ToString().Contains("@wherePAY_DTE "))
                {
                    this.SelectOperator.SetValue("@wherePAY_DTE", this.wherePAY_DTE, SqlDbType.VarChar);
                }
                if (sbstrSQL.ToString().Contains("@whereVAILD_FLAG "))
                {
                    this.SelectOperator.SetValue("@whereVAILD_FLAG", this.whereVAILD_FLAG, SqlDbType.VarChar);
                }
                if (sbstrSQL.ToString().Contains("@whereSEND_MSG_FLAG "))
                {
                    this.SelectOperator.SetValue("@whereSEND_MSG_FLAG", this.whereSEND_MSG_FLAG, SqlDbType.VarChar);
                }
                if (sbstrSQL.ToString().Contains("@whereREPLY_FLAG "))
                {
                    this.SelectOperator.SetValue("@whereREPLY_FLAG", this.whereREPLY_FLAG, SqlDbType.VarChar);
                }
                if (sbstrSQL.ToString().Contains("@whereREPLY_DTE "))
                {
                    this.SelectOperator.SetValue("@whereREPLY_DTE", this.whereREPLY_DTE, SqlDbType.VarChar);
                }
                if (sbstrSQL.ToString().Contains("@whereSTOP_DTE "))
                {
                    this.SelectOperator.SetValue("@whereSTOP_DTE", this.whereSTOP_DTE, SqlDbType.DateTime);
                }
                if (sbstrSQL.ToString().Contains("@whereSTOP_USER "))
                {
                    this.SelectOperator.SetValue("@whereSTOP_USER", this.whereSTOP_USER, SqlDbType.VarChar);
                }
                if (sbstrSQL.ToString().Contains("@whereCREATE_DT "))
                {
                    this.SelectOperator.SetValue("@whereCREATE_DT", this.whereCREATE_DT, SqlDbType.DateTime);
                }
                if (sbstrSQL.ToString().Contains("@whereCREATE_USER "))
                {
                    this.SelectOperator.SetValue("@whereCREATE_USER", this.whereCREATE_USER, SqlDbType.VarChar);
                }
                if (sbstrSQL.ToString().Contains("@whereERROR_REASON "))
                {
                    this.SelectOperator.SetValue("@whereERROR_REASON", this.whereERROR_REASON, SqlDbType.NVarChar);
                }
                if (sbstrSQL.ToString().Contains("@whereERROR_REASON_DT "))
                {
                    this.SelectOperator.SetValue("@whereERROR_REASON_DT", this.whereERROR_REASON_DT, SqlDbType.VarChar);
                }
                if (sbstrSQL.ToString().Contains("@wherePUBLIC_APPLY_CHAR_1 "))
                {
                    this.SelectOperator.SetValue("@wherePUBLIC_APPLY_CHAR_1", this.wherePUBLIC_APPLY_CHAR_1, SqlDbType.VarChar);
                }
                if (sbstrSQL.ToString().Contains("@wherePUBLIC_APPLY_CHAR_2 "))
                {
                    this.SelectOperator.SetValue("@wherePUBLIC_APPLY_CHAR_2", this.wherePUBLIC_APPLY_CHAR_2, SqlDbType.VarChar);
                }
                if (sbstrSQL.ToString().Contains("@wherePUBLIC_APPLY_CHAR_3 "))
                {
                    this.SelectOperator.SetValue("@wherePUBLIC_APPLY_CHAR_3", this.wherePUBLIC_APPLY_CHAR_3, SqlDbType.VarChar);
                }
                if (sbstrSQL.ToString().Contains("@wherePUBLIC_APPLY_CHAR_4 "))
                {
                    this.SelectOperator.SetValue("@wherePUBLIC_APPLY_CHAR_4", this.wherePUBLIC_APPLY_CHAR_4, SqlDbType.VarChar);
                }
                if (sbstrSQL.ToString().Contains("@wherePUBLIC_APPLY_CHAR_5 "))
                {
                    this.SelectOperator.SetValue("@wherePUBLIC_APPLY_CHAR_5", this.wherePUBLIC_APPLY_CHAR_5, SqlDbType.VarChar);
                }
                if (sbstrSQL.ToString().Contains("@wherePUBLIC_APPLY_AMT_1 "))
                {
                    this.SelectOperator.SetValue("@wherePUBLIC_APPLY_AMT_1", this.wherePUBLIC_APPLY_AMT_1, SqlDbType.Decimal);
                }
                if (sbstrSQL.ToString().Contains("@wherePUBLIC_APPLY_AMT_2 "))
                {
                    this.SelectOperator.SetValue("@wherePUBLIC_APPLY_AMT_2", this.wherePUBLIC_APPLY_AMT_2, SqlDbType.Decimal);
                }
                if (sbstrSQL.ToString().Contains("@wherePUBLIC_APPLY_AMT_3 "))
                {
                    this.SelectOperator.SetValue("@wherePUBLIC_APPLY_AMT_3", this.wherePUBLIC_APPLY_AMT_3, SqlDbType.Decimal);
                }
                if (sbstrSQL.ToString().Contains("@wherePUBLIC_APPLY_AMT_4 "))
                {
                    this.SelectOperator.SetValue("@wherePUBLIC_APPLY_AMT_4", this.wherePUBLIC_APPLY_AMT_4, SqlDbType.Decimal);
                }
                if (sbstrSQL.ToString().Contains("@wherePUBLIC_APPLY_AMT_5 "))
                {
                    this.SelectOperator.SetValue("@wherePUBLIC_APPLY_AMT_5", this.wherePUBLIC_APPLY_AMT_5, SqlDbType.Decimal);
                }
                if (sbstrSQL.ToString().Contains("@wherePUBLIC_APPLY_DT_1 "))
                {
                    this.SelectOperator.SetValue("@wherePUBLIC_APPLY_DT_1", this.wherePUBLIC_APPLY_DT_1, SqlDbType.DateTime);
                }
                if (sbstrSQL.ToString().Contains("@wherePUBLIC_APPLY_DT_2 "))
                {
                    this.SelectOperator.SetValue("@wherePUBLIC_APPLY_DT_2", this.wherePUBLIC_APPLY_DT_2, SqlDbType.DateTime);
                }
                if (sbstrSQL.ToString().Contains("@wherePUBLIC_APPLY_DT_3 "))
                {
                    this.SelectOperator.SetValue("@wherePUBLIC_APPLY_DT_3", this.wherePUBLIC_APPLY_DT_3, SqlDbType.DateTime);
                }
                if (sbstrSQL.ToString().Contains("@wherePUBLIC_APPLY_DT_4 "))
                {
                    this.SelectOperator.SetValue("@wherePUBLIC_APPLY_DT_4", this.wherePUBLIC_APPLY_DT_4, SqlDbType.DateTime);
                }
                if (sbstrSQL.ToString().Contains("@wherePUBLIC_APPLY_DT_5 "))
                {
                    this.SelectOperator.SetValue("@wherePUBLIC_APPLY_DT_5", this.wherePUBLIC_APPLY_DT_5, SqlDbType.DateTime);
                }
                if (sbstrSQL.ToString().Contains("@whereMNT_DT "))
                {
                    this.SelectOperator.SetValue("@whereMNT_DT", this.whereMNT_DT, SqlDbType.DateTime);
                }
                if (sbstrSQL.ToString().Contains("@whereMNT_USER "))
                {
                    this.SelectOperator.SetValue("@whereMNT_USER", this.whereMNT_USER, SqlDbType.VarChar);
                }
                #endregion
                //myTable set to DataTable object
                myTable = this.SelectOperator.GetDataTable("PUBLIC_APPLY");
                if (myTable.Rows.Count == 0)
                {
                    msg_code = "F0023"; //not found
                }
                else
                {
                    msg_code = "S0000"; //query success
                }
            }
            catch (SqlException e)
            {
                MSG.strMsg = Convert.ToString(e.Number) + '-' + e.Message;
                msg_code = MSG.getMsg();
            }
            return msg_code;
        }
        #endregion
        #region insert()
        public string insert()
        {
            #region Modify History
            /// <history>
            /// <design>
            /// <name>Cybersoft.COCA DaoGenerator</name>
            /// <date>2014/12/19 下午 04:50:54</date>
            #endregion
            try
            {
                #region set SQL statement
                StringBuilder sbstrSQL = new StringBuilder();
                sbstrSQL.Append("INSERT INTO PUBLIC_APPLY (PAY_CARD_NBR,PAY_TYPE,PAY_NBR,SEQ,BU,PRODUCT,CARD_PRODUCT,ACCT_NBR,PAY_ACCT_NBR,PAY_CARD_NBR_PREV,PAY_ACCT_NBR_PREV,CUST_SEQ,EXPIR_DTE,APPLY_DTE,FIRST_DTE,PAY_DTE,VAILD_FLAG,SEND_MSG_FLAG,REPLY_FLAG,REPLY_DTE,STOP_DTE,STOP_USER,CREATE_DT,CREATE_USER,ERROR_REASON,ERROR_REASON_DT,PUBLIC_APPLY_CHAR_1,PUBLIC_APPLY_CHAR_2,PUBLIC_APPLY_CHAR_3,PUBLIC_APPLY_CHAR_4,PUBLIC_APPLY_CHAR_5,PUBLIC_APPLY_AMT_1,PUBLIC_APPLY_AMT_2,PUBLIC_APPLY_AMT_3,PUBLIC_APPLY_AMT_4,PUBLIC_APPLY_AMT_5,PUBLIC_APPLY_DT_1,PUBLIC_APPLY_DT_2,PUBLIC_APPLY_DT_3,PUBLIC_APPLY_DT_4,PUBLIC_APPLY_DT_5,MNT_DT,MNT_USER) VALUES (@PAY_CARD_NBR ,@PAY_TYPE ,@PAY_NBR ,@SEQ ,@BU ,@PRODUCT ,@CARD_PRODUCT ,@ACCT_NBR ,@PAY_ACCT_NBR ,@PAY_CARD_NBR_PREV ,@PAY_ACCT_NBR_PREV ,@CUST_SEQ ,@EXPIR_DTE ,@APPLY_DTE ,@FIRST_DTE ,@PAY_DTE ,@VAILD_FLAG ,@SEND_MSG_FLAG ,@REPLY_FLAG ,@REPLY_DTE ,@STOP_DTE ,@STOP_USER ,@CREATE_DT ,@CREATE_USER ,@ERROR_REASON ,@ERROR_REASON_DT ,@PUBLIC_APPLY_CHAR_1 ,@PUBLIC_APPLY_CHAR_2 ,@PUBLIC_APPLY_CHAR_3 ,@PUBLIC_APPLY_CHAR_4 ,@PUBLIC_APPLY_CHAR_5 ,@PUBLIC_APPLY_AMT_1 ,@PUBLIC_APPLY_AMT_2 ,@PUBLIC_APPLY_AMT_3 ,@PUBLIC_APPLY_AMT_4 ,@PUBLIC_APPLY_AMT_5 ,@PUBLIC_APPLY_DT_1 ,@PUBLIC_APPLY_DT_2 ,@PUBLIC_APPLY_DT_3 ,@PUBLIC_APPLY_DT_4 ,@PUBLIC_APPLY_DT_5 ,@MNT_DT ,@MNT_USER )");
                #endregion
                this.InsertOperator.SqlText = sbstrSQL.ToString();
                #region replace SQL parameter
                this.InsertOperator.SetValue("@PAY_CARD_NBR", this.PAY_CARD_NBR, SqlDbType.VarChar);
                this.InsertOperator.SetValue("@PAY_TYPE", this.PAY_TYPE, SqlDbType.VarChar);
                this.InsertOperator.SetValue("@PAY_NBR", this.PAY_NBR, SqlDbType.VarChar);
                this.InsertOperator.SetValue("@SEQ", this.SEQ, SqlDbType.VarChar);
                this.InsertOperator.SetValue("@BU", this.BU, SqlDbType.VarChar);
                this.InsertOperator.SetValue("@PRODUCT", this.PRODUCT, SqlDbType.VarChar);
                this.InsertOperator.SetValue("@CARD_PRODUCT", this.CARD_PRODUCT, SqlDbType.VarChar);
                this.InsertOperator.SetValue("@ACCT_NBR", this.ACCT_NBR, SqlDbType.VarChar);
                this.InsertOperator.SetValue("@PAY_ACCT_NBR", this.PAY_ACCT_NBR, SqlDbType.VarChar);
                this.InsertOperator.SetValue("@PAY_CARD_NBR_PREV", this.PAY_CARD_NBR_PREV, SqlDbType.VarChar);
                this.InsertOperator.SetValue("@PAY_ACCT_NBR_PREV", this.PAY_ACCT_NBR_PREV, SqlDbType.VarChar);
                this.InsertOperator.SetValue("@CUST_SEQ", this.CUST_SEQ, SqlDbType.VarChar);
                this.InsertOperator.SetValue("@EXPIR_DTE", this.EXPIR_DTE, SqlDbType.VarChar);
                this.InsertOperator.SetValue("@APPLY_DTE", this.APPLY_DTE, SqlDbType.VarChar);
                this.InsertOperator.SetValue("@FIRST_DTE", this.FIRST_DTE, SqlDbType.VarChar);
                this.InsertOperator.SetValue("@PAY_DTE", this.PAY_DTE, SqlDbType.VarChar);
                this.InsertOperator.SetValue("@VAILD_FLAG", this.VAILD_FLAG, SqlDbType.VarChar);
                this.InsertOperator.SetValue("@SEND_MSG_FLAG", this.SEND_MSG_FLAG, SqlDbType.VarChar);
                this.InsertOperator.SetValue("@REPLY_FLAG", this.REPLY_FLAG, SqlDbType.VarChar);
                this.InsertOperator.SetValue("@REPLY_DTE", this.REPLY_DTE, SqlDbType.VarChar);
                this.InsertOperator.SetValue("@STOP_DTE", this.STOP_DTE, SqlDbType.DateTime);
                this.InsertOperator.SetValue("@STOP_USER", this.STOP_USER, SqlDbType.VarChar);
                this.InsertOperator.SetValue("@CREATE_DT", this.CREATE_DT, SqlDbType.DateTime);
                this.InsertOperator.SetValue("@CREATE_USER", this.CREATE_USER, SqlDbType.VarChar);
                this.InsertOperator.SetValue("@ERROR_REASON", this.ERROR_REASON, SqlDbType.NVarChar);
                this.InsertOperator.SetValue("@ERROR_REASON_DT", this.ERROR_REASON_DT, SqlDbType.VarChar);
                this.InsertOperator.SetValue("@PUBLIC_APPLY_CHAR_1", this.PUBLIC_APPLY_CHAR_1, SqlDbType.VarChar);
                this.InsertOperator.SetValue("@PUBLIC_APPLY_CHAR_2", this.PUBLIC_APPLY_CHAR_2, SqlDbType.VarChar);
                this.InsertOperator.SetValue("@PUBLIC_APPLY_CHAR_3", this.PUBLIC_APPLY_CHAR_3, SqlDbType.VarChar);
                this.InsertOperator.SetValue("@PUBLIC_APPLY_CHAR_4", this.PUBLIC_APPLY_CHAR_4, SqlDbType.VarChar);
                this.InsertOperator.SetValue("@PUBLIC_APPLY_CHAR_5", this.PUBLIC_APPLY_CHAR_5, SqlDbType.VarChar);
                this.InsertOperator.SetValue("@PUBLIC_APPLY_AMT_1", this.PUBLIC_APPLY_AMT_1, SqlDbType.Decimal);
                this.InsertOperator.SetValue("@PUBLIC_APPLY_AMT_2", this.PUBLIC_APPLY_AMT_2, SqlDbType.Decimal);
                this.InsertOperator.SetValue("@PUBLIC_APPLY_AMT_3", this.PUBLIC_APPLY_AMT_3, SqlDbType.Decimal);
                this.InsertOperator.SetValue("@PUBLIC_APPLY_AMT_4", this.PUBLIC_APPLY_AMT_4, SqlDbType.Decimal);
                this.InsertOperator.SetValue("@PUBLIC_APPLY_AMT_5", this.PUBLIC_APPLY_AMT_5, SqlDbType.Decimal);
                this.InsertOperator.SetValue("@PUBLIC_APPLY_DT_1", this.PUBLIC_APPLY_DT_1, SqlDbType.DateTime);
                this.InsertOperator.SetValue("@PUBLIC_APPLY_DT_2", this.PUBLIC_APPLY_DT_2, SqlDbType.DateTime);
                this.InsertOperator.SetValue("@PUBLIC_APPLY_DT_3", this.PUBLIC_APPLY_DT_3, SqlDbType.DateTime);
                this.InsertOperator.SetValue("@PUBLIC_APPLY_DT_4", this.PUBLIC_APPLY_DT_4, SqlDbType.DateTime);
                this.InsertOperator.SetValue("@PUBLIC_APPLY_DT_5", this.PUBLIC_APPLY_DT_5, SqlDbType.DateTime);
                this.InsertOperator.SetValue("@MNT_DT", this.MNT_DT, SqlDbType.DateTime);
                this.InsertOperator.SetValue("@MNT_USER", this.MNT_USER, SqlDbType.VarChar);
                #endregion
                InsCnt = this.InsertOperator.Execute();
                msg_code = "S0000"; //success
            }
            catch (SqlException e)
            {
                MSG.strMsg = Convert.ToString(e.Number) + '-' + e.Message;
                msg_code = MSG.getMsg();
            }
            return msg_code;
        }
        #endregion
        #region update()
        public string update()
        {
            #region Modify History
            /// <history>
            /// <design>
            /// <name>Cybersoft.COCA DaoGenerator</name>
            /// <date>2014/12/19 下午 04:50:54</date>
            #endregion
            try
            {
                #region set SQL statement
                StringBuilder sbstrSQL = new StringBuilder();
                sbstrSQL.Append("UPDATE PUBLIC_APPLY set ");                //update field
                if (this.PAY_CARD_NBR != null)
                {
                    sbstrSQL.Append(" PAY_CARD_NBR=@PAY_CARD_NBR ,");
                }
                if (this.PAY_TYPE != null)
                {
                    sbstrSQL.Append(" PAY_TYPE=@PAY_TYPE ,");
                }
                if (this.PAY_NBR != null)
                {
                    sbstrSQL.Append(" PAY_NBR=@PAY_NBR ,");
                }
                if (this.SEQ != null)
                {
                    sbstrSQL.Append(" SEQ=@SEQ ,");
                }
                if (this.BU != null)
                {
                    sbstrSQL.Append(" BU=@BU ,");
                }
                if (this.PRODUCT != null)
                {
                    sbstrSQL.Append(" PRODUCT=@PRODUCT ,");
                }
                if (this.CARD_PRODUCT != null)
                {
                    sbstrSQL.Append(" CARD_PRODUCT=@CARD_PRODUCT ,");
                }
                if (this.ACCT_NBR != null)
                {
                    sbstrSQL.Append(" ACCT_NBR=@ACCT_NBR ,");
                }
                if (this.PAY_ACCT_NBR != null)
                {
                    sbstrSQL.Append(" PAY_ACCT_NBR=@PAY_ACCT_NBR ,");
                }
                if (this.PAY_CARD_NBR_PREV != null)
                {
                    sbstrSQL.Append(" PAY_CARD_NBR_PREV=@PAY_CARD_NBR_PREV ,");
                }
                if (this.PAY_ACCT_NBR_PREV != null)
                {
                    sbstrSQL.Append(" PAY_ACCT_NBR_PREV=@PAY_ACCT_NBR_PREV ,");
                }
                if (this.CUST_SEQ != null)
                {
                    sbstrSQL.Append(" CUST_SEQ=@CUST_SEQ ,");
                }
                if (this.EXPIR_DTE != null)
                {
                    sbstrSQL.Append(" EXPIR_DTE=@EXPIR_DTE ,");
                }
                if (this.APPLY_DTE != null)
                {
                    sbstrSQL.Append(" APPLY_DTE=@APPLY_DTE ,");
                }
                if (this.FIRST_DTE != null)
                {
                    sbstrSQL.Append(" FIRST_DTE=@FIRST_DTE ,");
                }
                if (this.PAY_DTE != null)
                {
                    sbstrSQL.Append(" PAY_DTE=@PAY_DTE ,");
                }
                if (this.VAILD_FLAG != null)
                {
                    sbstrSQL.Append(" VAILD_FLAG=@VAILD_FLAG ,");
                }
                if (this.SEND_MSG_FLAG != null)
                {
                    sbstrSQL.Append(" SEND_MSG_FLAG=@SEND_MSG_FLAG ,");
                }
                if (this.REPLY_FLAG != null)
                {
                    sbstrSQL.Append(" REPLY_FLAG=@REPLY_FLAG ,");
                }
                if (this.REPLY_DTE != null)
                {
                    sbstrSQL.Append(" REPLY_DTE=@REPLY_DTE ,");
                }
                if (this.STOP_DTE > dateStart)
                {
                    if (this.STOP_DTE.ToString("yyyyMMdd") == "19000101")
                    {
                        sbstrSQL.Append(" STOP_DTE= '19000101' ,");
                    }
                    else
                    {
                        sbstrSQL.Append(" STOP_DTE=@STOP_DTE ,");
                    }
                }
                if (this.STOP_USER != null)
                {
                    sbstrSQL.Append(" STOP_USER=@STOP_USER ,");
                }
                if (this.CREATE_DT > dateStart)
                {
                    if (this.CREATE_DT.ToString("yyyyMMdd") == "19000101")
                    {
                        sbstrSQL.Append(" CREATE_DT= '19000101' ,");
                    }
                    else
                    {
                        sbstrSQL.Append(" CREATE_DT=@CREATE_DT ,");
                    }
                }
                if (this.CREATE_USER != null)
                {
                    sbstrSQL.Append(" CREATE_USER=@CREATE_USER ,");
                }
                if (this.ERROR_REASON != null)
                {
                    sbstrSQL.Append(" ERROR_REASON=@ERROR_REASON ,");
                }
                if (this.ERROR_REASON_DT != null)
                {
                    sbstrSQL.Append(" ERROR_REASON_DT=@ERROR_REASON_DT ,");
                }
                if (this.PUBLIC_APPLY_CHAR_1 != null)
                {
                    sbstrSQL.Append(" PUBLIC_APPLY_CHAR_1=@PUBLIC_APPLY_CHAR_1 ,");
                }
                if (this.PUBLIC_APPLY_CHAR_2 != null)
                {
                    sbstrSQL.Append(" PUBLIC_APPLY_CHAR_2=@PUBLIC_APPLY_CHAR_2 ,");
                }
                if (this.PUBLIC_APPLY_CHAR_3 != null)
                {
                    sbstrSQL.Append(" PUBLIC_APPLY_CHAR_3=@PUBLIC_APPLY_CHAR_3 ,");
                }
                if (this.PUBLIC_APPLY_CHAR_4 != null)
                {
                    sbstrSQL.Append(" PUBLIC_APPLY_CHAR_4=@PUBLIC_APPLY_CHAR_4 ,");
                }
                if (this.PUBLIC_APPLY_CHAR_5 != null)
                {
                    sbstrSQL.Append(" PUBLIC_APPLY_CHAR_5=@PUBLIC_APPLY_CHAR_5 ,");
                }
                if (this.PUBLIC_APPLY_AMT_1 > -1000000000000)
                {
                    sbstrSQL.Append(" PUBLIC_APPLY_AMT_1=@PUBLIC_APPLY_AMT_1 ,");
                }
                if (this.PUBLIC_APPLY_AMT_2 > -1000000000000)
                {
                    sbstrSQL.Append(" PUBLIC_APPLY_AMT_2=@PUBLIC_APPLY_AMT_2 ,");
                }
                if (this.PUBLIC_APPLY_AMT_3 > -1000000000000)
                {
                    sbstrSQL.Append(" PUBLIC_APPLY_AMT_3=@PUBLIC_APPLY_AMT_3 ,");
                }
                if (this.PUBLIC_APPLY_AMT_4 > -1000000000000)
                {
                    sbstrSQL.Append(" PUBLIC_APPLY_AMT_4=@PUBLIC_APPLY_AMT_4 ,");
                }
                if (this.PUBLIC_APPLY_AMT_5 > -1000000000000)
                {
                    sbstrSQL.Append(" PUBLIC_APPLY_AMT_5=@PUBLIC_APPLY_AMT_5 ,");
                }
                if (this.PUBLIC_APPLY_DT_1 > dateStart)
                {
                    if (this.PUBLIC_APPLY_DT_1.ToString("yyyyMMdd") == "19000101")
                    {
                        sbstrSQL.Append(" PUBLIC_APPLY_DT_1= '19000101' ,");
                    }
                    else
                    {
                        sbstrSQL.Append(" PUBLIC_APPLY_DT_1=@PUBLIC_APPLY_DT_1 ,");
                    }
                }
                if (this.PUBLIC_APPLY_DT_2 > dateStart)
                {
                    if (this.PUBLIC_APPLY_DT_2.ToString("yyyyMMdd") == "19000101")
                    {
                        sbstrSQL.Append(" PUBLIC_APPLY_DT_2= '19000101' ,");
                    }
                    else
                    {
                        sbstrSQL.Append(" PUBLIC_APPLY_DT_2=@PUBLIC_APPLY_DT_2 ,");
                    }
                }
                if (this.PUBLIC_APPLY_DT_3 > dateStart)
                {
                    if (this.PUBLIC_APPLY_DT_3.ToString("yyyyMMdd") == "19000101")
                    {
                        sbstrSQL.Append(" PUBLIC_APPLY_DT_3= '19000101' ,");
                    }
                    else
                    {
                        sbstrSQL.Append(" PUBLIC_APPLY_DT_3=@PUBLIC_APPLY_DT_3 ,");
                    }
                }
                if (this.PUBLIC_APPLY_DT_4 > dateStart)
                {
                    if (this.PUBLIC_APPLY_DT_4.ToString("yyyyMMdd") == "19000101")
                    {
                        sbstrSQL.Append(" PUBLIC_APPLY_DT_4= '19000101' ,");
                    }
                    else
                    {
                        sbstrSQL.Append(" PUBLIC_APPLY_DT_4=@PUBLIC_APPLY_DT_4 ,");
                    }
                }
                if (this.PUBLIC_APPLY_DT_5 > dateStart)
                {
                    if (this.PUBLIC_APPLY_DT_5.ToString("yyyyMMdd") == "19000101")
                    {
                        sbstrSQL.Append(" PUBLIC_APPLY_DT_5= '19000101' ,");
                    }
                    else
                    {
                        sbstrSQL.Append(" PUBLIC_APPLY_DT_5=@PUBLIC_APPLY_DT_5 ,");
                    }
                }
                if (this.MNT_DT > dateStart)
                {
                    if (this.MNT_DT.ToString("yyyyMMdd") == "19000101")
                    {
                        sbstrSQL.Append(" MNT_DT= '19000101' ,");
                    }
                    else
                    {
                        sbstrSQL.Append(" MNT_DT=@MNT_DT ,");
                    }
                }
                if (this.MNT_USER != null)
                {
                    sbstrSQL.Append(" MNT_USER=@MNT_USER ,");
                }
                sbstrSQL.Remove(sbstrSQL.ToString().Length - 1, 1); //移除最後一個逗號
                sbstrSQL.Append(" where 1=1 ");
                if (this.wherePAY_CARD_NBR != null)
                {
                    sbstrSQL.Append(" and PAY_CARD_NBR=@wherePAY_CARD_NBR ");
                }
                if (this.wherePAY_TYPE != null)
                {
                    sbstrSQL.Append(" and PAY_TYPE=@wherePAY_TYPE ");
                }
                if (this.wherePAY_NBR != null)
                {
                    sbstrSQL.Append(" and PAY_NBR=@wherePAY_NBR ");
                }
                if (this.whereSEQ != null)
                {
                    sbstrSQL.Append(" and SEQ=@whereSEQ ");
                }
                if (this.whereBU != null)
                {
                    sbstrSQL.Append(" and BU=@whereBU ");
                }
                if (this.wherePRODUCT != null)
                {
                    sbstrSQL.Append(" and PRODUCT=@wherePRODUCT ");
                }
                if (this.whereCARD_PRODUCT != null)
                {
                    sbstrSQL.Append(" and CARD_PRODUCT=@whereCARD_PRODUCT ");
                }
                if (this.whereACCT_NBR != null)
                {
                    sbstrSQL.Append(" and ACCT_NBR=@whereACCT_NBR ");
                }
                if (this.wherePAY_ACCT_NBR != null)
                {
                    sbstrSQL.Append(" and PAY_ACCT_NBR=@wherePAY_ACCT_NBR ");
                }
                if (this.wherePAY_CARD_NBR_PREV != null)
                {
                    sbstrSQL.Append(" and PAY_CARD_NBR_PREV=@wherePAY_CARD_NBR_PREV ");
                }
                if (this.wherePAY_ACCT_NBR_PREV != null)
                {
                    sbstrSQL.Append(" and PAY_ACCT_NBR_PREV=@wherePAY_ACCT_NBR_PREV ");
                }
                if (this.whereCUST_SEQ != null)
                {
                    sbstrSQL.Append(" and CUST_SEQ=@whereCUST_SEQ ");
                }
                if (this.whereEXPIR_DTE != null)
                {
                    sbstrSQL.Append(" and EXPIR_DTE=@whereEXPIR_DTE ");
                }
                if (this.whereAPPLY_DTE != null)
                {
                    sbstrSQL.Append(" and APPLY_DTE=@whereAPPLY_DTE ");
                }
                if (this.whereFIRST_DTE != null)
                {
                    sbstrSQL.Append(" and FIRST_DTE=@whereFIRST_DTE ");
                }
                if (this.wherePAY_DTE != null)
                {
                    sbstrSQL.Append(" and PAY_DTE=@wherePAY_DTE ");
                }
                if (this.whereVAILD_FLAG != null)
                {
                    sbstrSQL.Append(" and VAILD_FLAG=@whereVAILD_FLAG ");
                }
                if (this.whereSEND_MSG_FLAG != null)
                {
                    sbstrSQL.Append(" and SEND_MSG_FLAG=@whereSEND_MSG_FLAG ");
                }
                if (this.whereREPLY_FLAG != null)
                {
                    sbstrSQL.Append(" and REPLY_FLAG=@whereREPLY_FLAG ");
                }
                if (this.whereREPLY_DTE != null)
                {
                    sbstrSQL.Append(" and REPLY_DTE=@whereREPLY_DTE ");
                }
                if (this.whereSTOP_DTE > dateStart)
                {
                    sbstrSQL.Append(" and STOP_DTE=@whereSTOP_DTE ");
                }
                if (this.whereSTOP_USER != null)
                {
                    sbstrSQL.Append(" and STOP_USER=@whereSTOP_USER ");
                }
                if (this.whereCREATE_DT > dateStart)
                {
                    sbstrSQL.Append(" and CREATE_DT=@whereCREATE_DT ");
                }
                if (this.whereCREATE_USER != null)
                {
                    sbstrSQL.Append(" and CREATE_USER=@whereCREATE_USER ");
                }
                if (this.whereERROR_REASON != null)
                {
                    sbstrSQL.Append(" and ERROR_REASON=@whereERROR_REASON ");
                }
                if (this.whereERROR_REASON_DT != null)
                {
                    sbstrSQL.Append(" and ERROR_REASON_DT=@whereERROR_REASON_DT ");
                }
                if (this.wherePUBLIC_APPLY_CHAR_1 != null)
                {
                    sbstrSQL.Append(" and PUBLIC_APPLY_CHAR_1=@wherePUBLIC_APPLY_CHAR_1 ");
                }
                if (this.wherePUBLIC_APPLY_CHAR_2 != null)
                {
                    sbstrSQL.Append(" and PUBLIC_APPLY_CHAR_2=@wherePUBLIC_APPLY_CHAR_2 ");
                }
                if (this.wherePUBLIC_APPLY_CHAR_3 != null)
                {
                    sbstrSQL.Append(" and PUBLIC_APPLY_CHAR_3=@wherePUBLIC_APPLY_CHAR_3 ");
                }
                if (this.wherePUBLIC_APPLY_CHAR_4 != null)
                {
                    sbstrSQL.Append(" and PUBLIC_APPLY_CHAR_4=@wherePUBLIC_APPLY_CHAR_4 ");
                }
                if (this.wherePUBLIC_APPLY_CHAR_5 != null)
                {
                    sbstrSQL.Append(" and PUBLIC_APPLY_CHAR_5=@wherePUBLIC_APPLY_CHAR_5 ");
                }
                if (this.wherePUBLIC_APPLY_AMT_1 > -1000000000000)
                {
                    sbstrSQL.Append(" and PUBLIC_APPLY_AMT_1=@wherePUBLIC_APPLY_AMT_1 ");
                }
                if (this.wherePUBLIC_APPLY_AMT_2 > -1000000000000)
                {
                    sbstrSQL.Append(" and PUBLIC_APPLY_AMT_2=@wherePUBLIC_APPLY_AMT_2 ");
                }
                if (this.wherePUBLIC_APPLY_AMT_3 > -1000000000000)
                {
                    sbstrSQL.Append(" and PUBLIC_APPLY_AMT_3=@wherePUBLIC_APPLY_AMT_3 ");
                }
                if (this.wherePUBLIC_APPLY_AMT_4 > -1000000000000)
                {
                    sbstrSQL.Append(" and PUBLIC_APPLY_AMT_4=@wherePUBLIC_APPLY_AMT_4 ");
                }
                if (this.wherePUBLIC_APPLY_AMT_5 > -1000000000000)
                {
                    sbstrSQL.Append(" and PUBLIC_APPLY_AMT_5=@wherePUBLIC_APPLY_AMT_5 ");
                }
                if (this.wherePUBLIC_APPLY_DT_1 > dateStart)
                {
                    sbstrSQL.Append(" and PUBLIC_APPLY_DT_1=@wherePUBLIC_APPLY_DT_1 ");
                }
                if (this.wherePUBLIC_APPLY_DT_2 > dateStart)
                {
                    sbstrSQL.Append(" and PUBLIC_APPLY_DT_2=@wherePUBLIC_APPLY_DT_2 ");
                }
                if (this.wherePUBLIC_APPLY_DT_3 > dateStart)
                {
                    sbstrSQL.Append(" and PUBLIC_APPLY_DT_3=@wherePUBLIC_APPLY_DT_3 ");
                }
                if (this.wherePUBLIC_APPLY_DT_4 > dateStart)
                {
                    sbstrSQL.Append(" and PUBLIC_APPLY_DT_4=@wherePUBLIC_APPLY_DT_4 ");
                }
                if (this.wherePUBLIC_APPLY_DT_5 > dateStart)
                {
                    sbstrSQL.Append(" and PUBLIC_APPLY_DT_5=@wherePUBLIC_APPLY_DT_5 ");
                }
                if (this.whereMNT_DT > dateStart)
                {
                    sbstrSQL.Append(" and MNT_DT=@whereMNT_DT ");
                }
                if (this.whereMNT_USER != null)
                {
                    sbstrSQL.Append(" and MNT_USER=@whereMNT_USER ");
                }
                #endregion
                this.UpdateOperator.SqlText = sbstrSQL.ToString();
                #region replace SQL parameter
                if (sbstrSQL.ToString().Contains("@PAY_CARD_NBR "))
                {
                    this.UpdateOperator.SetValue("@PAY_CARD_NBR", this.PAY_CARD_NBR, SqlDbType.VarChar);
                }
                if (sbstrSQL.ToString().Contains("@PAY_TYPE "))
                {
                    this.UpdateOperator.SetValue("@PAY_TYPE", this.PAY_TYPE, SqlDbType.VarChar);
                }
                if (sbstrSQL.ToString().Contains("@PAY_NBR "))
                {
                    this.UpdateOperator.SetValue("@PAY_NBR", this.PAY_NBR, SqlDbType.VarChar);
                }
                if (sbstrSQL.ToString().Contains("@SEQ "))
                {
                    this.UpdateOperator.SetValue("@SEQ", this.SEQ, SqlDbType.VarChar);
                }
                if (sbstrSQL.ToString().Contains("@BU "))
                {
                    this.UpdateOperator.SetValue("@BU", this.BU, SqlDbType.VarChar);
                }
                if (sbstrSQL.ToString().Contains("@PRODUCT "))
                {
                    this.UpdateOperator.SetValue("@PRODUCT", this.PRODUCT, SqlDbType.VarChar);
                }
                if (sbstrSQL.ToString().Contains("@CARD_PRODUCT "))
                {
                    this.UpdateOperator.SetValue("@CARD_PRODUCT", this.CARD_PRODUCT, SqlDbType.VarChar);
                }
                if (sbstrSQL.ToString().Contains("@ACCT_NBR "))
                {
                    this.UpdateOperator.SetValue("@ACCT_NBR", this.ACCT_NBR, SqlDbType.VarChar);
                }
                if (sbstrSQL.ToString().Contains("@PAY_ACCT_NBR "))
                {
                    this.UpdateOperator.SetValue("@PAY_ACCT_NBR", this.PAY_ACCT_NBR, SqlDbType.VarChar);
                }
                if (sbstrSQL.ToString().Contains("@PAY_CARD_NBR_PREV "))
                {
                    this.UpdateOperator.SetValue("@PAY_CARD_NBR_PREV", this.PAY_CARD_NBR_PREV, SqlDbType.VarChar);
                }
                if (sbstrSQL.ToString().Contains("@PAY_ACCT_NBR_PREV "))
                {
                    this.UpdateOperator.SetValue("@PAY_ACCT_NBR_PREV", this.PAY_ACCT_NBR_PREV, SqlDbType.VarChar);
                }
                if (sbstrSQL.ToString().Contains("@CUST_SEQ "))
                {
                    this.UpdateOperator.SetValue("@CUST_SEQ", this.CUST_SEQ, SqlDbType.VarChar);
                }
                if (sbstrSQL.ToString().Contains("@EXPIR_DTE "))
                {
                    this.UpdateOperator.SetValue("@EXPIR_DTE", this.EXPIR_DTE, SqlDbType.VarChar);
                }
                if (sbstrSQL.ToString().Contains("@APPLY_DTE "))
                {
                    this.UpdateOperator.SetValue("@APPLY_DTE", this.APPLY_DTE, SqlDbType.VarChar);
                }
                if (sbstrSQL.ToString().Contains("@FIRST_DTE "))
                {
                    this.UpdateOperator.SetValue("@FIRST_DTE", this.FIRST_DTE, SqlDbType.VarChar);
                }
                if (sbstrSQL.ToString().Contains("@PAY_DTE "))
                {
                    this.UpdateOperator.SetValue("@PAY_DTE", this.PAY_DTE, SqlDbType.VarChar);
                }
                if (sbstrSQL.ToString().Contains("@VAILD_FLAG "))
                {
                    this.UpdateOperator.SetValue("@VAILD_FLAG", this.VAILD_FLAG, SqlDbType.VarChar);
                }
                if (sbstrSQL.ToString().Contains("@SEND_MSG_FLAG "))
                {
                    this.UpdateOperator.SetValue("@SEND_MSG_FLAG", this.SEND_MSG_FLAG, SqlDbType.VarChar);
                }
                if (sbstrSQL.ToString().Contains("@REPLY_FLAG "))
                {
                    this.UpdateOperator.SetValue("@REPLY_FLAG", this.REPLY_FLAG, SqlDbType.VarChar);
                }
                if (sbstrSQL.ToString().Contains("@REPLY_DTE "))
                {
                    this.UpdateOperator.SetValue("@REPLY_DTE", this.REPLY_DTE, SqlDbType.VarChar);
                }
                if (sbstrSQL.ToString().Contains("@STOP_DTE "))
                {
                    this.UpdateOperator.SetValue("@STOP_DTE", this.STOP_DTE, SqlDbType.DateTime);
                }
                if (sbstrSQL.ToString().Contains("@STOP_USER "))
                {
                    this.UpdateOperator.SetValue("@STOP_USER", this.STOP_USER, SqlDbType.VarChar);
                }
                if (sbstrSQL.ToString().Contains("@CREATE_DT "))
                {
                    this.UpdateOperator.SetValue("@CREATE_DT", this.CREATE_DT, SqlDbType.DateTime);
                }
                if (sbstrSQL.ToString().Contains("@CREATE_USER "))
                {
                    this.UpdateOperator.SetValue("@CREATE_USER", this.CREATE_USER, SqlDbType.VarChar);
                }
                if (sbstrSQL.ToString().Contains("@ERROR_REASON "))
                {
                    this.UpdateOperator.SetValue("@ERROR_REASON", this.ERROR_REASON, SqlDbType.NVarChar);
                }
                if (sbstrSQL.ToString().Contains("@ERROR_REASON_DT "))
                {
                    this.UpdateOperator.SetValue("@ERROR_REASON_DT", this.ERROR_REASON_DT, SqlDbType.VarChar);
                }
                if (sbstrSQL.ToString().Contains("@PUBLIC_APPLY_CHAR_1 "))
                {
                    this.UpdateOperator.SetValue("@PUBLIC_APPLY_CHAR_1", this.PUBLIC_APPLY_CHAR_1, SqlDbType.VarChar);
                }
                if (sbstrSQL.ToString().Contains("@PUBLIC_APPLY_CHAR_2 "))
                {
                    this.UpdateOperator.SetValue("@PUBLIC_APPLY_CHAR_2", this.PUBLIC_APPLY_CHAR_2, SqlDbType.VarChar);
                }
                if (sbstrSQL.ToString().Contains("@PUBLIC_APPLY_CHAR_3 "))
                {
                    this.UpdateOperator.SetValue("@PUBLIC_APPLY_CHAR_3", this.PUBLIC_APPLY_CHAR_3, SqlDbType.VarChar);
                }
                if (sbstrSQL.ToString().Contains("@PUBLIC_APPLY_CHAR_4 "))
                {
                    this.UpdateOperator.SetValue("@PUBLIC_APPLY_CHAR_4", this.PUBLIC_APPLY_CHAR_4, SqlDbType.VarChar);
                }
                if (sbstrSQL.ToString().Contains("@PUBLIC_APPLY_CHAR_5 "))
                {
                    this.UpdateOperator.SetValue("@PUBLIC_APPLY_CHAR_5", this.PUBLIC_APPLY_CHAR_5, SqlDbType.VarChar);
                }
                if (sbstrSQL.ToString().Contains("@PUBLIC_APPLY_AMT_1 "))
                {
                    this.UpdateOperator.SetValue("@PUBLIC_APPLY_AMT_1", this.PUBLIC_APPLY_AMT_1, SqlDbType.Decimal);
                }
                if (sbstrSQL.ToString().Contains("@PUBLIC_APPLY_AMT_2 "))
                {
                    this.UpdateOperator.SetValue("@PUBLIC_APPLY_AMT_2", this.PUBLIC_APPLY_AMT_2, SqlDbType.Decimal);
                }
                if (sbstrSQL.ToString().Contains("@PUBLIC_APPLY_AMT_3 "))
                {
                    this.UpdateOperator.SetValue("@PUBLIC_APPLY_AMT_3", this.PUBLIC_APPLY_AMT_3, SqlDbType.Decimal);
                }
                if (sbstrSQL.ToString().Contains("@PUBLIC_APPLY_AMT_4 "))
                {
                    this.UpdateOperator.SetValue("@PUBLIC_APPLY_AMT_4", this.PUBLIC_APPLY_AMT_4, SqlDbType.Decimal);
                }
                if (sbstrSQL.ToString().Contains("@PUBLIC_APPLY_AMT_5 "))
                {
                    this.UpdateOperator.SetValue("@PUBLIC_APPLY_AMT_5", this.PUBLIC_APPLY_AMT_5, SqlDbType.Decimal);
                }
                if (sbstrSQL.ToString().Contains("@PUBLIC_APPLY_DT_1 "))
                {
                    this.UpdateOperator.SetValue("@PUBLIC_APPLY_DT_1", this.PUBLIC_APPLY_DT_1, SqlDbType.DateTime);
                }
                if (sbstrSQL.ToString().Contains("@PUBLIC_APPLY_DT_2 "))
                {
                    this.UpdateOperator.SetValue("@PUBLIC_APPLY_DT_2", this.PUBLIC_APPLY_DT_2, SqlDbType.DateTime);
                }
                if (sbstrSQL.ToString().Contains("@PUBLIC_APPLY_DT_3 "))
                {
                    this.UpdateOperator.SetValue("@PUBLIC_APPLY_DT_3", this.PUBLIC_APPLY_DT_3, SqlDbType.DateTime);
                }
                if (sbstrSQL.ToString().Contains("@PUBLIC_APPLY_DT_4 "))
                {
                    this.UpdateOperator.SetValue("@PUBLIC_APPLY_DT_4", this.PUBLIC_APPLY_DT_4, SqlDbType.DateTime);
                }
                if (sbstrSQL.ToString().Contains("@PUBLIC_APPLY_DT_5 "))
                {
                    this.UpdateOperator.SetValue("@PUBLIC_APPLY_DT_5", this.PUBLIC_APPLY_DT_5, SqlDbType.DateTime);
                }
                if (sbstrSQL.ToString().Contains("@MNT_DT "))
                {
                    this.UpdateOperator.SetValue("@MNT_DT", this.MNT_DT, SqlDbType.DateTime);
                }
                if (sbstrSQL.ToString().Contains("@MNT_USER "))
                {
                    this.UpdateOperator.SetValue("@MNT_USER", this.MNT_USER, SqlDbType.VarChar);
                }
                if (sbstrSQL.ToString().Contains("@wherePAY_CARD_NBR "))
                {
                    this.UpdateOperator.SetValue("@wherePAY_CARD_NBR", this.wherePAY_CARD_NBR, SqlDbType.VarChar);
                }
                if (sbstrSQL.ToString().Contains("@wherePAY_TYPE "))
                {
                    this.UpdateOperator.SetValue("@wherePAY_TYPE", this.wherePAY_TYPE, SqlDbType.VarChar);
                }
                if (sbstrSQL.ToString().Contains("@wherePAY_NBR "))
                {
                    this.UpdateOperator.SetValue("@wherePAY_NBR", this.wherePAY_NBR, SqlDbType.VarChar);
                }
                if (sbstrSQL.ToString().Contains("@whereSEQ "))
                {
                    this.UpdateOperator.SetValue("@whereSEQ", this.whereSEQ, SqlDbType.VarChar);
                }
                if (sbstrSQL.ToString().Contains("@whereBU "))
                {
                    this.UpdateOperator.SetValue("@whereBU", this.whereBU, SqlDbType.VarChar);
                }
                if (sbstrSQL.ToString().Contains("@wherePRODUCT "))
                {
                    this.UpdateOperator.SetValue("@wherePRODUCT", this.wherePRODUCT, SqlDbType.VarChar);
                }
                if (sbstrSQL.ToString().Contains("@whereCARD_PRODUCT "))
                {
                    this.UpdateOperator.SetValue("@whereCARD_PRODUCT", this.whereCARD_PRODUCT, SqlDbType.VarChar);
                }
                if (sbstrSQL.ToString().Contains("@whereACCT_NBR "))
                {
                    this.UpdateOperator.SetValue("@whereACCT_NBR", this.whereACCT_NBR, SqlDbType.VarChar);
                }
                if (sbstrSQL.ToString().Contains("@wherePAY_ACCT_NBR "))
                {
                    this.UpdateOperator.SetValue("@wherePAY_ACCT_NBR", this.wherePAY_ACCT_NBR, SqlDbType.VarChar);
                }
                if (sbstrSQL.ToString().Contains("@wherePAY_CARD_NBR_PREV "))
                {
                    this.UpdateOperator.SetValue("@wherePAY_CARD_NBR_PREV", this.wherePAY_CARD_NBR_PREV, SqlDbType.VarChar);
                }
                if (sbstrSQL.ToString().Contains("@wherePAY_ACCT_NBR_PREV "))
                {
                    this.UpdateOperator.SetValue("@wherePAY_ACCT_NBR_PREV", this.wherePAY_ACCT_NBR_PREV, SqlDbType.VarChar);
                }
                if (sbstrSQL.ToString().Contains("@whereCUST_SEQ "))
                {
                    this.UpdateOperator.SetValue("@whereCUST_SEQ", this.whereCUST_SEQ, SqlDbType.VarChar);
                }
                if (sbstrSQL.ToString().Contains("@whereEXPIR_DTE "))
                {
                    this.UpdateOperator.SetValue("@whereEXPIR_DTE", this.whereEXPIR_DTE, SqlDbType.VarChar);
                }
                if (sbstrSQL.ToString().Contains("@whereAPPLY_DTE "))
                {
                    this.UpdateOperator.SetValue("@whereAPPLY_DTE", this.whereAPPLY_DTE, SqlDbType.VarChar);
                }
                if (sbstrSQL.ToString().Contains("@whereFIRST_DTE "))
                {
                    this.UpdateOperator.SetValue("@whereFIRST_DTE", this.whereFIRST_DTE, SqlDbType.VarChar);
                }
                if (sbstrSQL.ToString().Contains("@wherePAY_DTE "))
                {
                    this.UpdateOperator.SetValue("@wherePAY_DTE", this.wherePAY_DTE, SqlDbType.VarChar);
                }
                if (sbstrSQL.ToString().Contains("@whereVAILD_FLAG "))
                {
                    this.UpdateOperator.SetValue("@whereVAILD_FLAG", this.whereVAILD_FLAG, SqlDbType.VarChar);
                }
                if (sbstrSQL.ToString().Contains("@whereSEND_MSG_FLAG "))
                {
                    this.UpdateOperator.SetValue("@whereSEND_MSG_FLAG", this.whereSEND_MSG_FLAG, SqlDbType.VarChar);
                }
                if (sbstrSQL.ToString().Contains("@whereREPLY_FLAG "))
                {
                    this.UpdateOperator.SetValue("@whereREPLY_FLAG", this.whereREPLY_FLAG, SqlDbType.VarChar);
                }
                if (sbstrSQL.ToString().Contains("@whereREPLY_DTE "))
                {
                    this.UpdateOperator.SetValue("@whereREPLY_DTE", this.whereREPLY_DTE, SqlDbType.VarChar);
                }
                if (sbstrSQL.ToString().Contains("@whereSTOP_DTE "))
                {
                    this.UpdateOperator.SetValue("@whereSTOP_DTE", this.whereSTOP_DTE, SqlDbType.DateTime);
                }
                if (sbstrSQL.ToString().Contains("@whereSTOP_USER "))
                {
                    this.UpdateOperator.SetValue("@whereSTOP_USER", this.whereSTOP_USER, SqlDbType.VarChar);
                }
                if (sbstrSQL.ToString().Contains("@whereCREATE_DT "))
                {
                    this.UpdateOperator.SetValue("@whereCREATE_DT", this.whereCREATE_DT, SqlDbType.DateTime);
                }
                if (sbstrSQL.ToString().Contains("@whereCREATE_USER "))
                {
                    this.UpdateOperator.SetValue("@whereCREATE_USER", this.whereCREATE_USER, SqlDbType.VarChar);
                }
                if (sbstrSQL.ToString().Contains("@whereERROR_REASON "))
                {
                    this.UpdateOperator.SetValue("@whereERROR_REASON", this.whereERROR_REASON, SqlDbType.NVarChar);
                }
                if (sbstrSQL.ToString().Contains("@whereERROR_REASON_DT "))
                {
                    this.UpdateOperator.SetValue("@whereERROR_REASON_DT", this.whereERROR_REASON_DT, SqlDbType.VarChar);
                }
                if (sbstrSQL.ToString().Contains("@wherePUBLIC_APPLY_CHAR_1 "))
                {
                    this.UpdateOperator.SetValue("@wherePUBLIC_APPLY_CHAR_1", this.wherePUBLIC_APPLY_CHAR_1, SqlDbType.VarChar);
                }
                if (sbstrSQL.ToString().Contains("@wherePUBLIC_APPLY_CHAR_2 "))
                {
                    this.UpdateOperator.SetValue("@wherePUBLIC_APPLY_CHAR_2", this.wherePUBLIC_APPLY_CHAR_2, SqlDbType.VarChar);
                }
                if (sbstrSQL.ToString().Contains("@wherePUBLIC_APPLY_CHAR_3 "))
                {
                    this.UpdateOperator.SetValue("@wherePUBLIC_APPLY_CHAR_3", this.wherePUBLIC_APPLY_CHAR_3, SqlDbType.VarChar);
                }
                if (sbstrSQL.ToString().Contains("@wherePUBLIC_APPLY_CHAR_4 "))
                {
                    this.UpdateOperator.SetValue("@wherePUBLIC_APPLY_CHAR_4", this.wherePUBLIC_APPLY_CHAR_4, SqlDbType.VarChar);
                }
                if (sbstrSQL.ToString().Contains("@wherePUBLIC_APPLY_CHAR_5 "))
                {
                    this.UpdateOperator.SetValue("@wherePUBLIC_APPLY_CHAR_5", this.wherePUBLIC_APPLY_CHAR_5, SqlDbType.VarChar);
                }
                if (sbstrSQL.ToString().Contains("@wherePUBLIC_APPLY_AMT_1 "))
                {
                    this.UpdateOperator.SetValue("@wherePUBLIC_APPLY_AMT_1", this.wherePUBLIC_APPLY_AMT_1, SqlDbType.Decimal);
                }
                if (sbstrSQL.ToString().Contains("@wherePUBLIC_APPLY_AMT_2 "))
                {
                    this.UpdateOperator.SetValue("@wherePUBLIC_APPLY_AMT_2", this.wherePUBLIC_APPLY_AMT_2, SqlDbType.Decimal);
                }
                if (sbstrSQL.ToString().Contains("@wherePUBLIC_APPLY_AMT_3 "))
                {
                    this.UpdateOperator.SetValue("@wherePUBLIC_APPLY_AMT_3", this.wherePUBLIC_APPLY_AMT_3, SqlDbType.Decimal);
                }
                if (sbstrSQL.ToString().Contains("@wherePUBLIC_APPLY_AMT_4 "))
                {
                    this.UpdateOperator.SetValue("@wherePUBLIC_APPLY_AMT_4", this.wherePUBLIC_APPLY_AMT_4, SqlDbType.Decimal);
                }
                if (sbstrSQL.ToString().Contains("@wherePUBLIC_APPLY_AMT_5 "))
                {
                    this.UpdateOperator.SetValue("@wherePUBLIC_APPLY_AMT_5", this.wherePUBLIC_APPLY_AMT_5, SqlDbType.Decimal);
                }
                if (sbstrSQL.ToString().Contains("@wherePUBLIC_APPLY_DT_1 "))
                {
                    this.UpdateOperator.SetValue("@wherePUBLIC_APPLY_DT_1", this.wherePUBLIC_APPLY_DT_1, SqlDbType.DateTime);
                }
                if (sbstrSQL.ToString().Contains("@wherePUBLIC_APPLY_DT_2 "))
                {
                    this.UpdateOperator.SetValue("@wherePUBLIC_APPLY_DT_2", this.wherePUBLIC_APPLY_DT_2, SqlDbType.DateTime);
                }
                if (sbstrSQL.ToString().Contains("@wherePUBLIC_APPLY_DT_3 "))
                {
                    this.UpdateOperator.SetValue("@wherePUBLIC_APPLY_DT_3", this.wherePUBLIC_APPLY_DT_3, SqlDbType.DateTime);
                }
                if (sbstrSQL.ToString().Contains("@wherePUBLIC_APPLY_DT_4 "))
                {
                    this.UpdateOperator.SetValue("@wherePUBLIC_APPLY_DT_4", this.wherePUBLIC_APPLY_DT_4, SqlDbType.DateTime);
                }
                if (sbstrSQL.ToString().Contains("@wherePUBLIC_APPLY_DT_5 "))
                {
                    this.UpdateOperator.SetValue("@wherePUBLIC_APPLY_DT_5", this.wherePUBLIC_APPLY_DT_5, SqlDbType.DateTime);
                }
                if (sbstrSQL.ToString().Contains("@whereMNT_DT "))
                {
                    this.UpdateOperator.SetValue("@whereMNT_DT", this.whereMNT_DT, SqlDbType.DateTime);
                }
                if (sbstrSQL.ToString().Contains("@whereMNT_USER "))
                {
                    this.UpdateOperator.SetValue("@whereMNT_USER", this.whereMNT_USER, SqlDbType.VarChar);
                }
                #endregion
                UptCnt = this.UpdateOperator.Execute();
                msg_code = "S0000"; //update success
            }
            catch (SqlException e)
            {
                MSG.strMsg = Convert.ToString(e.Number) + '-' + e.Message;
                msg_code = MSG.getMsg();
            }
            return msg_code;
        }
        #endregion
        #region delete()
        public string delete()
        {
            #region Modify History
            /// <history>
            /// <design>
            /// <name>Cybersoft.COCA DaoGenerator</name>
            /// <date>2014/12/19 下午 04:50:54</date>
            #endregion
            try
            {
                #region set SQL statement
                StringBuilder sbstrSQL = new StringBuilder();
                sbstrSQL.Append(" DELETE PUBLIC_APPLY where 1=1 ");
                if (this.wherePAY_CARD_NBR != null)
                {
                    sbstrSQL.Append(" and PAY_CARD_NBR=@wherePAY_CARD_NBR ");
                }
                if (this.wherePAY_TYPE != null)
                {
                    sbstrSQL.Append(" and PAY_TYPE=@wherePAY_TYPE ");
                }
                if (this.wherePAY_NBR != null)
                {
                    sbstrSQL.Append(" and PAY_NBR=@wherePAY_NBR ");
                }
                if (this.whereSEQ != null)
                {
                    sbstrSQL.Append(" and SEQ=@whereSEQ ");
                }
                if (this.whereBU != null)
                {
                    sbstrSQL.Append(" and BU=@whereBU ");
                }
                if (this.wherePRODUCT != null)
                {
                    sbstrSQL.Append(" and PRODUCT=@wherePRODUCT ");
                }
                if (this.whereCARD_PRODUCT != null)
                {
                    sbstrSQL.Append(" and CARD_PRODUCT=@whereCARD_PRODUCT ");
                }
                if (this.whereACCT_NBR != null)
                {
                    sbstrSQL.Append(" and ACCT_NBR=@whereACCT_NBR ");
                }
                if (this.wherePAY_ACCT_NBR != null)
                {
                    sbstrSQL.Append(" and PAY_ACCT_NBR=@wherePAY_ACCT_NBR ");
                }
                if (this.wherePAY_CARD_NBR_PREV != null)
                {
                    sbstrSQL.Append(" and PAY_CARD_NBR_PREV=@wherePAY_CARD_NBR_PREV ");
                }
                if (this.wherePAY_ACCT_NBR_PREV != null)
                {
                    sbstrSQL.Append(" and PAY_ACCT_NBR_PREV=@wherePAY_ACCT_NBR_PREV ");
                }
                if (this.whereCUST_SEQ != null)
                {
                    sbstrSQL.Append(" and CUST_SEQ=@whereCUST_SEQ ");
                }
                if (this.whereEXPIR_DTE != null)
                {
                    sbstrSQL.Append(" and EXPIR_DTE=@whereEXPIR_DTE ");
                }
                if (this.whereAPPLY_DTE != null)
                {
                    sbstrSQL.Append(" and APPLY_DTE=@whereAPPLY_DTE ");
                }
                if (this.whereFIRST_DTE != null)
                {
                    sbstrSQL.Append(" and FIRST_DTE=@whereFIRST_DTE ");
                }
                if (this.wherePAY_DTE != null)
                {
                    sbstrSQL.Append(" and PAY_DTE=@wherePAY_DTE ");
                }
                if (this.whereVAILD_FLAG != null)
                {
                    sbstrSQL.Append(" and VAILD_FLAG=@whereVAILD_FLAG ");
                }
                if (this.whereSEND_MSG_FLAG != null)
                {
                    sbstrSQL.Append(" and SEND_MSG_FLAG=@whereSEND_MSG_FLAG ");
                }
                if (this.whereREPLY_FLAG != null)
                {
                    sbstrSQL.Append(" and REPLY_FLAG=@whereREPLY_FLAG ");
                }
                if (this.whereREPLY_DTE != null)
                {
                    sbstrSQL.Append(" and REPLY_DTE=@whereREPLY_DTE ");
                }
                if (this.whereSTOP_DTE > dateStart)
                {
                    sbstrSQL.Append(" and STOP_DTE=@whereSTOP_DTE ");
                }
                if (this.whereSTOP_USER != null)
                {
                    sbstrSQL.Append(" and STOP_USER=@whereSTOP_USER ");
                }
                if (this.whereCREATE_DT > dateStart)
                {
                    sbstrSQL.Append(" and CREATE_DT=@whereCREATE_DT ");
                }
                if (this.whereCREATE_USER != null)
                {
                    sbstrSQL.Append(" and CREATE_USER=@whereCREATE_USER ");
                }
                if (this.whereERROR_REASON != null)
                {
                    sbstrSQL.Append(" and ERROR_REASON=@whereERROR_REASON ");
                }
                if (this.whereERROR_REASON_DT != null)
                {
                    sbstrSQL.Append(" and ERROR_REASON_DT=@whereERROR_REASON_DT ");
                }
                if (this.wherePUBLIC_APPLY_CHAR_1 != null)
                {
                    sbstrSQL.Append(" and PUBLIC_APPLY_CHAR_1=@wherePUBLIC_APPLY_CHAR_1 ");
                }
                if (this.wherePUBLIC_APPLY_CHAR_2 != null)
                {
                    sbstrSQL.Append(" and PUBLIC_APPLY_CHAR_2=@wherePUBLIC_APPLY_CHAR_2 ");
                }
                if (this.wherePUBLIC_APPLY_CHAR_3 != null)
                {
                    sbstrSQL.Append(" and PUBLIC_APPLY_CHAR_3=@wherePUBLIC_APPLY_CHAR_3 ");
                }
                if (this.wherePUBLIC_APPLY_CHAR_4 != null)
                {
                    sbstrSQL.Append(" and PUBLIC_APPLY_CHAR_4=@wherePUBLIC_APPLY_CHAR_4 ");
                }
                if (this.wherePUBLIC_APPLY_CHAR_5 != null)
                {
                    sbstrSQL.Append(" and PUBLIC_APPLY_CHAR_5=@wherePUBLIC_APPLY_CHAR_5 ");
                }
                if (this.wherePUBLIC_APPLY_AMT_1 > -1000000000000)
                {
                    sbstrSQL.Append(" and PUBLIC_APPLY_AMT_1=@wherePUBLIC_APPLY_AMT_1 ");
                }
                if (this.wherePUBLIC_APPLY_AMT_2 > -1000000000000)
                {
                    sbstrSQL.Append(" and PUBLIC_APPLY_AMT_2=@wherePUBLIC_APPLY_AMT_2 ");
                }
                if (this.wherePUBLIC_APPLY_AMT_3 > -1000000000000)
                {
                    sbstrSQL.Append(" and PUBLIC_APPLY_AMT_3=@wherePUBLIC_APPLY_AMT_3 ");
                }
                if (this.wherePUBLIC_APPLY_AMT_4 > -1000000000000)
                {
                    sbstrSQL.Append(" and PUBLIC_APPLY_AMT_4=@wherePUBLIC_APPLY_AMT_4 ");
                }
                if (this.wherePUBLIC_APPLY_AMT_5 > -1000000000000)
                {
                    sbstrSQL.Append(" and PUBLIC_APPLY_AMT_5=@wherePUBLIC_APPLY_AMT_5 ");
                }
                if (this.wherePUBLIC_APPLY_DT_1 > dateStart)
                {
                    sbstrSQL.Append(" and PUBLIC_APPLY_DT_1=@wherePUBLIC_APPLY_DT_1 ");
                }
                if (this.wherePUBLIC_APPLY_DT_2 > dateStart)
                {
                    sbstrSQL.Append(" and PUBLIC_APPLY_DT_2=@wherePUBLIC_APPLY_DT_2 ");
                }
                if (this.wherePUBLIC_APPLY_DT_3 > dateStart)
                {
                    sbstrSQL.Append(" and PUBLIC_APPLY_DT_3=@wherePUBLIC_APPLY_DT_3 ");
                }
                if (this.wherePUBLIC_APPLY_DT_4 > dateStart)
                {
                    sbstrSQL.Append(" and PUBLIC_APPLY_DT_4=@wherePUBLIC_APPLY_DT_4 ");
                }
                if (this.wherePUBLIC_APPLY_DT_5 > dateStart)
                {
                    sbstrSQL.Append(" and PUBLIC_APPLY_DT_5=@wherePUBLIC_APPLY_DT_5 ");
                }
                if (this.whereMNT_DT > dateStart)
                {
                    sbstrSQL.Append(" and MNT_DT=@whereMNT_DT ");
                }
                if (this.whereMNT_USER != null)
                {
                    sbstrSQL.Append(" and MNT_USER=@whereMNT_USER ");
                }
                #endregion
                this.DeleteOperator.SqlText = sbstrSQL.ToString();
                #region repalce SQL parameter
                if (sbstrSQL.ToString().Contains("@wherePAY_CARD_NBR "))
                {
                    this.DeleteOperator.SetValue("@wherePAY_CARD_NBR", this.wherePAY_CARD_NBR, SqlDbType.VarChar);
                }
                if (sbstrSQL.ToString().Contains("@wherePAY_TYPE "))
                {
                    this.DeleteOperator.SetValue("@wherePAY_TYPE", this.wherePAY_TYPE, SqlDbType.VarChar);
                }
                if (sbstrSQL.ToString().Contains("@wherePAY_NBR "))
                {
                    this.DeleteOperator.SetValue("@wherePAY_NBR", this.wherePAY_NBR, SqlDbType.VarChar);
                }
                if (sbstrSQL.ToString().Contains("@whereSEQ "))
                {
                    this.DeleteOperator.SetValue("@whereSEQ", this.whereSEQ, SqlDbType.VarChar);
                }
                if (sbstrSQL.ToString().Contains("@whereBU "))
                {
                    this.DeleteOperator.SetValue("@whereBU", this.whereBU, SqlDbType.VarChar);
                }
                if (sbstrSQL.ToString().Contains("@wherePRODUCT "))
                {
                    this.DeleteOperator.SetValue("@wherePRODUCT", this.wherePRODUCT, SqlDbType.VarChar);
                }
                if (sbstrSQL.ToString().Contains("@whereCARD_PRODUCT "))
                {
                    this.DeleteOperator.SetValue("@whereCARD_PRODUCT", this.whereCARD_PRODUCT, SqlDbType.VarChar);
                }
                if (sbstrSQL.ToString().Contains("@whereACCT_NBR "))
                {
                    this.DeleteOperator.SetValue("@whereACCT_NBR", this.whereACCT_NBR, SqlDbType.VarChar);
                }
                if (sbstrSQL.ToString().Contains("@wherePAY_ACCT_NBR "))
                {
                    this.DeleteOperator.SetValue("@wherePAY_ACCT_NBR", this.wherePAY_ACCT_NBR, SqlDbType.VarChar);
                }
                if (sbstrSQL.ToString().Contains("@wherePAY_CARD_NBR_PREV "))
                {
                    this.DeleteOperator.SetValue("@wherePAY_CARD_NBR_PREV", this.wherePAY_CARD_NBR_PREV, SqlDbType.VarChar);
                }
                if (sbstrSQL.ToString().Contains("@wherePAY_ACCT_NBR_PREV "))
                {
                    this.DeleteOperator.SetValue("@wherePAY_ACCT_NBR_PREV", this.wherePAY_ACCT_NBR_PREV, SqlDbType.VarChar);
                }
                if (sbstrSQL.ToString().Contains("@whereCUST_SEQ "))
                {
                    this.DeleteOperator.SetValue("@whereCUST_SEQ", this.whereCUST_SEQ, SqlDbType.VarChar);
                }
                if (sbstrSQL.ToString().Contains("@whereEXPIR_DTE "))
                {
                    this.DeleteOperator.SetValue("@whereEXPIR_DTE", this.whereEXPIR_DTE, SqlDbType.VarChar);
                }
                if (sbstrSQL.ToString().Contains("@whereAPPLY_DTE "))
                {
                    this.DeleteOperator.SetValue("@whereAPPLY_DTE", this.whereAPPLY_DTE, SqlDbType.VarChar);
                }
                if (sbstrSQL.ToString().Contains("@whereFIRST_DTE "))
                {
                    this.DeleteOperator.SetValue("@whereFIRST_DTE", this.whereFIRST_DTE, SqlDbType.VarChar);
                }
                if (sbstrSQL.ToString().Contains("@wherePAY_DTE "))
                {
                    this.DeleteOperator.SetValue("@wherePAY_DTE", this.wherePAY_DTE, SqlDbType.VarChar);
                }
                if (sbstrSQL.ToString().Contains("@whereVAILD_FLAG "))
                {
                    this.DeleteOperator.SetValue("@whereVAILD_FLAG", this.whereVAILD_FLAG, SqlDbType.VarChar);
                }
                if (sbstrSQL.ToString().Contains("@whereSEND_MSG_FLAG "))
                {
                    this.DeleteOperator.SetValue("@whereSEND_MSG_FLAG", this.whereSEND_MSG_FLAG, SqlDbType.VarChar);
                }
                if (sbstrSQL.ToString().Contains("@whereREPLY_FLAG "))
                {
                    this.DeleteOperator.SetValue("@whereREPLY_FLAG", this.whereREPLY_FLAG, SqlDbType.VarChar);
                }
                if (sbstrSQL.ToString().Contains("@whereREPLY_DTE "))
                {
                    this.DeleteOperator.SetValue("@whereREPLY_DTE", this.whereREPLY_DTE, SqlDbType.VarChar);
                }
                if (sbstrSQL.ToString().Contains("@whereSTOP_DTE "))
                {
                    this.DeleteOperator.SetValue("@whereSTOP_DTE", this.whereSTOP_DTE, SqlDbType.DateTime);
                }
                if (sbstrSQL.ToString().Contains("@whereSTOP_USER "))
                {
                    this.DeleteOperator.SetValue("@whereSTOP_USER", this.whereSTOP_USER, SqlDbType.VarChar);
                }
                if (sbstrSQL.ToString().Contains("@whereCREATE_DT "))
                {
                    this.DeleteOperator.SetValue("@whereCREATE_DT", this.whereCREATE_DT, SqlDbType.DateTime);
                }
                if (sbstrSQL.ToString().Contains("@whereCREATE_USER "))
                {
                    this.DeleteOperator.SetValue("@whereCREATE_USER", this.whereCREATE_USER, SqlDbType.VarChar);
                }
                if (sbstrSQL.ToString().Contains("@whereERROR_REASON "))
                {
                    this.DeleteOperator.SetValue("@whereERROR_REASON", this.whereERROR_REASON, SqlDbType.NVarChar);
                }
                if (sbstrSQL.ToString().Contains("@whereERROR_REASON_DT "))
                {
                    this.DeleteOperator.SetValue("@whereERROR_REASON_DT", this.whereERROR_REASON_DT, SqlDbType.VarChar);
                }
                if (sbstrSQL.ToString().Contains("@wherePUBLIC_APPLY_CHAR_1 "))
                {
                    this.DeleteOperator.SetValue("@wherePUBLIC_APPLY_CHAR_1", this.wherePUBLIC_APPLY_CHAR_1, SqlDbType.VarChar);
                }
                if (sbstrSQL.ToString().Contains("@wherePUBLIC_APPLY_CHAR_2 "))
                {
                    this.DeleteOperator.SetValue("@wherePUBLIC_APPLY_CHAR_2", this.wherePUBLIC_APPLY_CHAR_2, SqlDbType.VarChar);
                }
                if (sbstrSQL.ToString().Contains("@wherePUBLIC_APPLY_CHAR_3 "))
                {
                    this.DeleteOperator.SetValue("@wherePUBLIC_APPLY_CHAR_3", this.wherePUBLIC_APPLY_CHAR_3, SqlDbType.VarChar);
                }
                if (sbstrSQL.ToString().Contains("@wherePUBLIC_APPLY_CHAR_4 "))
                {
                    this.DeleteOperator.SetValue("@wherePUBLIC_APPLY_CHAR_4", this.wherePUBLIC_APPLY_CHAR_4, SqlDbType.VarChar);
                }
                if (sbstrSQL.ToString().Contains("@wherePUBLIC_APPLY_CHAR_5 "))
                {
                    this.DeleteOperator.SetValue("@wherePUBLIC_APPLY_CHAR_5", this.wherePUBLIC_APPLY_CHAR_5, SqlDbType.VarChar);
                }
                if (sbstrSQL.ToString().Contains("@wherePUBLIC_APPLY_AMT_1 "))
                {
                    this.DeleteOperator.SetValue("@wherePUBLIC_APPLY_AMT_1", this.wherePUBLIC_APPLY_AMT_1, SqlDbType.Decimal);
                }
                if (sbstrSQL.ToString().Contains("@wherePUBLIC_APPLY_AMT_2 "))
                {
                    this.DeleteOperator.SetValue("@wherePUBLIC_APPLY_AMT_2", this.wherePUBLIC_APPLY_AMT_2, SqlDbType.Decimal);
                }
                if (sbstrSQL.ToString().Contains("@wherePUBLIC_APPLY_AMT_3 "))
                {
                    this.DeleteOperator.SetValue("@wherePUBLIC_APPLY_AMT_3", this.wherePUBLIC_APPLY_AMT_3, SqlDbType.Decimal);
                }
                if (sbstrSQL.ToString().Contains("@wherePUBLIC_APPLY_AMT_4 "))
                {
                    this.DeleteOperator.SetValue("@wherePUBLIC_APPLY_AMT_4", this.wherePUBLIC_APPLY_AMT_4, SqlDbType.Decimal);
                }
                if (sbstrSQL.ToString().Contains("@wherePUBLIC_APPLY_AMT_5 "))
                {
                    this.DeleteOperator.SetValue("@wherePUBLIC_APPLY_AMT_5", this.wherePUBLIC_APPLY_AMT_5, SqlDbType.Decimal);
                }
                if (sbstrSQL.ToString().Contains("@wherePUBLIC_APPLY_DT_1 "))
                {
                    this.DeleteOperator.SetValue("@wherePUBLIC_APPLY_DT_1", this.wherePUBLIC_APPLY_DT_1, SqlDbType.DateTime);
                }
                if (sbstrSQL.ToString().Contains("@wherePUBLIC_APPLY_DT_2 "))
                {
                    this.DeleteOperator.SetValue("@wherePUBLIC_APPLY_DT_2", this.wherePUBLIC_APPLY_DT_2, SqlDbType.DateTime);
                }
                if (sbstrSQL.ToString().Contains("@wherePUBLIC_APPLY_DT_3 "))
                {
                    this.DeleteOperator.SetValue("@wherePUBLIC_APPLY_DT_3", this.wherePUBLIC_APPLY_DT_3, SqlDbType.DateTime);
                }
                if (sbstrSQL.ToString().Contains("@wherePUBLIC_APPLY_DT_4 "))
                {
                    this.DeleteOperator.SetValue("@wherePUBLIC_APPLY_DT_4", this.wherePUBLIC_APPLY_DT_4, SqlDbType.DateTime);
                }
                if (sbstrSQL.ToString().Contains("@wherePUBLIC_APPLY_DT_5 "))
                {
                    this.DeleteOperator.SetValue("@wherePUBLIC_APPLY_DT_5", this.wherePUBLIC_APPLY_DT_5, SqlDbType.DateTime);
                }
                if (sbstrSQL.ToString().Contains("@whereMNT_DT "))
                {
                    this.DeleteOperator.SetValue("@whereMNT_DT", this.whereMNT_DT, SqlDbType.DateTime);
                }
                if (sbstrSQL.ToString().Contains("@whereMNT_USER "))
                {
                    this.DeleteOperator.SetValue("@whereMNT_USER", this.whereMNT_USER, SqlDbType.VarChar);
                }
                #endregion
                DelCnt = this.DeleteOperator.Execute();
                msg_code = "S0000"; //delete success
            }
            catch (SqlException e)
            {
                MSG.strMsg = Convert.ToString(e.Number) + '-' + e.Message;
                msg_code = MSG.getMsg();
            }
            return msg_code;
        }
        #endregion
        #region update(Hashtable HashTB)
        public string update(Hashtable HashTB)
        {
            #region Modify History
            /// <history>
            /// <design>
            /// <name>Cybersoft.COCA DaoGenerator</name>
            /// <date>2014/12/19 下午 04:50:54</date>
            #endregion
            try
            {
                #region 宣告MNT_TODAYDAO並放入初始值
                Cybersoft.Dao.Core.MNT_TODAYDao MNT_TODAY = new Cybersoft.Dao.Core.MNT_TODAYDao();
                MNT_TODAY.table_define();
                MNT_TODAY.strinitTBL_NAME = "PUBLIC_APPLY";
                MNT_TODAY.strinitPOST_RESULT = "00";
                MNT_TODAY.strinitMNT_PGM = Convert.ToString(HashTB["SessionFN_CODE"]);
                MNT_TODAY.strinitMNT_USER = Convert.ToString(HashTB["SessionAccount"]);
                MNT_TODAY.DateTimeinitMNT_DT = DateTime.Now;
                MNT_TODAY.DateTimeinitEFF_DTE = DateTime.Now;
                MNT_TODAY.DateTimeinitPOSTING_DTE = DateTime.Now;
                #endregion
                #region 取得鍵值
                Cybersoft.Dao.Core.KEY_ITEM KEY = new Cybersoft.Dao.Core.KEY_ITEM();
                KEY.getKeyItem(HashTB);
                MNT_TODAY.strinitBU = KEY.strBU;
                MNT_TODAY.strinitACCT_NBR = KEY.strACCT_NBR;
                MNT_TODAY.strinitPRODUCT = KEY.strPRODUCT;
                MNT_TODAY.strinitCURRENCY = KEY.strCURRENCY;
                MNT_TODAY.strinitCARD_NBR = KEY.strCARD_NBR;
                MNT_TODAY.strinitOTHER_KEY_1 = KEY.strOTHER_KEY_1;
                MNT_TODAY.strinitOTHER_KEY_2 = KEY.strOTHER_KEY_2;
                MNT_TODAY.strinitACQ_BU = KEY.strACQ_BU;
                MNT_TODAY.strinitMER_NBR = KEY.strMER_NBR;
                #endregion
                #region 寫入log
                int MNT_Count = 0;
                //修改欄位部分
                if (this.PAY_CARD_NBR != null && this.PAY_CARD_NBR != Convert.ToString(myTable.Rows[0]["PAY_CARD_NBR"]))
                {
                    MNT_TODAY.initInsert_row();
                    MNT_TODAY.resultTable.Rows[MNT_Count]["FIELD_NAME"] = "PAY_CARD_NBR";
                    MNT_TODAY.resultTable.Rows[MNT_Count]["VARCHAR_BEFORE"] = Convert.ToString(myTable.Rows[0]["PAY_CARD_NBR"]);
                    MNT_TODAY.resultTable.Rows[MNT_Count]["VARCHAR_AFTER"] = this.PAY_CARD_NBR;
                    MNT_Count++;
                }
                if (this.PAY_TYPE != null && this.PAY_TYPE != Convert.ToString(myTable.Rows[0]["PAY_TYPE"]))
                {
                    MNT_TODAY.initInsert_row();
                    MNT_TODAY.resultTable.Rows[MNT_Count]["FIELD_NAME"] = "PAY_TYPE";
                    MNT_TODAY.resultTable.Rows[MNT_Count]["VARCHAR_BEFORE"] = Convert.ToString(myTable.Rows[0]["PAY_TYPE"]);
                    MNT_TODAY.resultTable.Rows[MNT_Count]["VARCHAR_AFTER"] = this.PAY_TYPE;
                    MNT_Count++;
                }
                if (this.PAY_NBR != null && this.PAY_NBR != Convert.ToString(myTable.Rows[0]["PAY_NBR"]))
                {
                    MNT_TODAY.initInsert_row();
                    MNT_TODAY.resultTable.Rows[MNT_Count]["FIELD_NAME"] = "PAY_NBR";
                    MNT_TODAY.resultTable.Rows[MNT_Count]["VARCHAR_BEFORE"] = Convert.ToString(myTable.Rows[0]["PAY_NBR"]);
                    MNT_TODAY.resultTable.Rows[MNT_Count]["VARCHAR_AFTER"] = this.PAY_NBR;
                    MNT_Count++;
                }
                if (this.SEQ != null && this.SEQ != Convert.ToString(myTable.Rows[0]["SEQ"]))
                {
                    MNT_TODAY.initInsert_row();
                    MNT_TODAY.resultTable.Rows[MNT_Count]["FIELD_NAME"] = "SEQ";
                    MNT_TODAY.resultTable.Rows[MNT_Count]["VARCHAR_BEFORE"] = Convert.ToString(myTable.Rows[0]["SEQ"]);
                    MNT_TODAY.resultTable.Rows[MNT_Count]["VARCHAR_AFTER"] = this.SEQ;
                    MNT_Count++;
                }
                if (this.BU != null && this.BU != Convert.ToString(myTable.Rows[0]["BU"]))
                {
                    MNT_TODAY.initInsert_row();
                    MNT_TODAY.resultTable.Rows[MNT_Count]["FIELD_NAME"] = "BU";
                    MNT_TODAY.resultTable.Rows[MNT_Count]["VARCHAR_BEFORE"] = Convert.ToString(myTable.Rows[0]["BU"]);
                    MNT_TODAY.resultTable.Rows[MNT_Count]["VARCHAR_AFTER"] = this.BU;
                    MNT_Count++;
                }
                if (this.PRODUCT != null && this.PRODUCT != Convert.ToString(myTable.Rows[0]["PRODUCT"]))
                {
                    MNT_TODAY.initInsert_row();
                    MNT_TODAY.resultTable.Rows[MNT_Count]["FIELD_NAME"] = "PRODUCT";
                    MNT_TODAY.resultTable.Rows[MNT_Count]["VARCHAR_BEFORE"] = Convert.ToString(myTable.Rows[0]["PRODUCT"]);
                    MNT_TODAY.resultTable.Rows[MNT_Count]["VARCHAR_AFTER"] = this.PRODUCT;
                    MNT_Count++;
                }
                if (this.CARD_PRODUCT != null && this.CARD_PRODUCT != Convert.ToString(myTable.Rows[0]["CARD_PRODUCT"]))
                {
                    MNT_TODAY.initInsert_row();
                    MNT_TODAY.resultTable.Rows[MNT_Count]["FIELD_NAME"] = "CARD_PRODUCT";
                    MNT_TODAY.resultTable.Rows[MNT_Count]["VARCHAR_BEFORE"] = Convert.ToString(myTable.Rows[0]["CARD_PRODUCT"]);
                    MNT_TODAY.resultTable.Rows[MNT_Count]["VARCHAR_AFTER"] = this.CARD_PRODUCT;
                    MNT_Count++;
                }
                if (this.ACCT_NBR != null && this.ACCT_NBR != Convert.ToString(myTable.Rows[0]["ACCT_NBR"]))
                {
                    MNT_TODAY.initInsert_row();
                    MNT_TODAY.resultTable.Rows[MNT_Count]["FIELD_NAME"] = "ACCT_NBR";
                    MNT_TODAY.resultTable.Rows[MNT_Count]["VARCHAR_BEFORE"] = Convert.ToString(myTable.Rows[0]["ACCT_NBR"]);
                    MNT_TODAY.resultTable.Rows[MNT_Count]["VARCHAR_AFTER"] = this.ACCT_NBR;
                    MNT_Count++;
                }
                if (this.PAY_ACCT_NBR != null && this.PAY_ACCT_NBR != Convert.ToString(myTable.Rows[0]["PAY_ACCT_NBR"]))
                {
                    MNT_TODAY.initInsert_row();
                    MNT_TODAY.resultTable.Rows[MNT_Count]["FIELD_NAME"] = "PAY_ACCT_NBR";
                    MNT_TODAY.resultTable.Rows[MNT_Count]["VARCHAR_BEFORE"] = Convert.ToString(myTable.Rows[0]["PAY_ACCT_NBR"]);
                    MNT_TODAY.resultTable.Rows[MNT_Count]["VARCHAR_AFTER"] = this.PAY_ACCT_NBR;
                    MNT_Count++;
                }
                if (this.PAY_CARD_NBR_PREV != null && this.PAY_CARD_NBR_PREV != Convert.ToString(myTable.Rows[0]["PAY_CARD_NBR_PREV"]))
                {
                    MNT_TODAY.initInsert_row();
                    MNT_TODAY.resultTable.Rows[MNT_Count]["FIELD_NAME"] = "PAY_CARD_NBR_PREV";
                    MNT_TODAY.resultTable.Rows[MNT_Count]["VARCHAR_BEFORE"] = Convert.ToString(myTable.Rows[0]["PAY_CARD_NBR_PREV"]);
                    MNT_TODAY.resultTable.Rows[MNT_Count]["VARCHAR_AFTER"] = this.PAY_CARD_NBR_PREV;
                    MNT_Count++;
                }
                if (this.PAY_ACCT_NBR_PREV != null && this.PAY_ACCT_NBR_PREV != Convert.ToString(myTable.Rows[0]["PAY_ACCT_NBR_PREV"]))
                {
                    MNT_TODAY.initInsert_row();
                    MNT_TODAY.resultTable.Rows[MNT_Count]["FIELD_NAME"] = "PAY_ACCT_NBR_PREV";
                    MNT_TODAY.resultTable.Rows[MNT_Count]["VARCHAR_BEFORE"] = Convert.ToString(myTable.Rows[0]["PAY_ACCT_NBR_PREV"]);
                    MNT_TODAY.resultTable.Rows[MNT_Count]["VARCHAR_AFTER"] = this.PAY_ACCT_NBR_PREV;
                    MNT_Count++;
                }
                if (this.CUST_SEQ != null && this.CUST_SEQ != Convert.ToString(myTable.Rows[0]["CUST_SEQ"]))
                {
                    MNT_TODAY.initInsert_row();
                    MNT_TODAY.resultTable.Rows[MNT_Count]["FIELD_NAME"] = "CUST_SEQ";
                    MNT_TODAY.resultTable.Rows[MNT_Count]["VARCHAR_BEFORE"] = Convert.ToString(myTable.Rows[0]["CUST_SEQ"]);
                    MNT_TODAY.resultTable.Rows[MNT_Count]["VARCHAR_AFTER"] = this.CUST_SEQ;
                    MNT_Count++;
                }
                if (this.EXPIR_DTE != null && this.EXPIR_DTE != Convert.ToString(myTable.Rows[0]["EXPIR_DTE"]))
                {
                    MNT_TODAY.initInsert_row();
                    MNT_TODAY.resultTable.Rows[MNT_Count]["FIELD_NAME"] = "EXPIR_DTE";
                    MNT_TODAY.resultTable.Rows[MNT_Count]["VARCHAR_BEFORE"] = Convert.ToString(myTable.Rows[0]["EXPIR_DTE"]);
                    MNT_TODAY.resultTable.Rows[MNT_Count]["VARCHAR_AFTER"] = this.EXPIR_DTE;
                    MNT_Count++;
                }
                if (this.APPLY_DTE != null && this.APPLY_DTE != Convert.ToString(myTable.Rows[0]["APPLY_DTE"]))
                {
                    MNT_TODAY.initInsert_row();
                    MNT_TODAY.resultTable.Rows[MNT_Count]["FIELD_NAME"] = "APPLY_DTE";
                    MNT_TODAY.resultTable.Rows[MNT_Count]["VARCHAR_BEFORE"] = Convert.ToString(myTable.Rows[0]["APPLY_DTE"]);
                    MNT_TODAY.resultTable.Rows[MNT_Count]["VARCHAR_AFTER"] = this.APPLY_DTE;
                    MNT_Count++;
                }
                if (this.FIRST_DTE != null && this.FIRST_DTE != Convert.ToString(myTable.Rows[0]["FIRST_DTE"]))
                {
                    MNT_TODAY.initInsert_row();
                    MNT_TODAY.resultTable.Rows[MNT_Count]["FIELD_NAME"] = "FIRST_DTE";
                    MNT_TODAY.resultTable.Rows[MNT_Count]["VARCHAR_BEFORE"] = Convert.ToString(myTable.Rows[0]["FIRST_DTE"]);
                    MNT_TODAY.resultTable.Rows[MNT_Count]["VARCHAR_AFTER"] = this.FIRST_DTE;
                    MNT_Count++;
                }
                if (this.PAY_DTE != null && this.PAY_DTE != Convert.ToString(myTable.Rows[0]["PAY_DTE"]))
                {
                    MNT_TODAY.initInsert_row();
                    MNT_TODAY.resultTable.Rows[MNT_Count]["FIELD_NAME"] = "PAY_DTE";
                    MNT_TODAY.resultTable.Rows[MNT_Count]["VARCHAR_BEFORE"] = Convert.ToString(myTable.Rows[0]["PAY_DTE"]);
                    MNT_TODAY.resultTable.Rows[MNT_Count]["VARCHAR_AFTER"] = this.PAY_DTE;
                    MNT_Count++;
                }
                if (this.VAILD_FLAG != null && this.VAILD_FLAG != Convert.ToString(myTable.Rows[0]["VAILD_FLAG"]))
                {
                    MNT_TODAY.initInsert_row();
                    MNT_TODAY.resultTable.Rows[MNT_Count]["FIELD_NAME"] = "VAILD_FLAG";
                    MNT_TODAY.resultTable.Rows[MNT_Count]["VARCHAR_BEFORE"] = Convert.ToString(myTable.Rows[0]["VAILD_FLAG"]);
                    MNT_TODAY.resultTable.Rows[MNT_Count]["VARCHAR_AFTER"] = this.VAILD_FLAG;
                    MNT_Count++;
                }
                if (this.SEND_MSG_FLAG != null && this.SEND_MSG_FLAG != Convert.ToString(myTable.Rows[0]["SEND_MSG_FLAG"]))
                {
                    MNT_TODAY.initInsert_row();
                    MNT_TODAY.resultTable.Rows[MNT_Count]["FIELD_NAME"] = "SEND_MSG_FLAG";
                    MNT_TODAY.resultTable.Rows[MNT_Count]["VARCHAR_BEFORE"] = Convert.ToString(myTable.Rows[0]["SEND_MSG_FLAG"]);
                    MNT_TODAY.resultTable.Rows[MNT_Count]["VARCHAR_AFTER"] = this.SEND_MSG_FLAG;
                    MNT_Count++;
                }
                if (this.REPLY_FLAG != null && this.REPLY_FLAG != Convert.ToString(myTable.Rows[0]["REPLY_FLAG"]))
                {
                    MNT_TODAY.initInsert_row();
                    MNT_TODAY.resultTable.Rows[MNT_Count]["FIELD_NAME"] = "REPLY_FLAG";
                    MNT_TODAY.resultTable.Rows[MNT_Count]["VARCHAR_BEFORE"] = Convert.ToString(myTable.Rows[0]["REPLY_FLAG"]);
                    MNT_TODAY.resultTable.Rows[MNT_Count]["VARCHAR_AFTER"] = this.REPLY_FLAG;
                    MNT_Count++;
                }
                if (this.REPLY_DTE != null && this.REPLY_DTE != Convert.ToString(myTable.Rows[0]["REPLY_DTE"]))
                {
                    MNT_TODAY.initInsert_row();
                    MNT_TODAY.resultTable.Rows[MNT_Count]["FIELD_NAME"] = "REPLY_DTE";
                    MNT_TODAY.resultTable.Rows[MNT_Count]["VARCHAR_BEFORE"] = Convert.ToString(myTable.Rows[0]["REPLY_DTE"]);
                    MNT_TODAY.resultTable.Rows[MNT_Count]["VARCHAR_AFTER"] = this.REPLY_DTE;
                    MNT_Count++;
                }
                if (this.STOP_DTE > dateStart && this.STOP_DTE != Convert.ToDateTime(myTable.Rows[0]["STOP_DTE"]))
                {
                    if (this.STOP_DTE.ToString("yyyyMMdd") == "19000101" &&
                        Convert.ToDateTime(myTable.Rows[0]["STOP_DTE"]) == dateStart)
                    {
                    }
                    else
                    {
                        MNT_TODAY.initInsert_row();
                        MNT_TODAY.resultTable.Rows[MNT_Count]["FIELD_NAME"] = "STOP_DTE";
                        MNT_TODAY.resultTable.Rows[MNT_Count]["DT_BEFORE"] = Convert.ToDateTime(myTable.Rows[0]["STOP_DTE"]);
                        if (this.STOP_DTE.ToString("yyyyMMdd") == "19000101")
                        {
                            MNT_TODAY.resultTable.Rows[MNT_Count]["DT_AFTER"] = "1900-01-01";
                        }
                        else
                        {
                            MNT_TODAY.resultTable.Rows[MNT_Count]["DT_AFTER"] = this.STOP_DTE;
                        }
                        MNT_Count++;
                    }
                }
                if (this.STOP_USER != null && this.STOP_USER != Convert.ToString(myTable.Rows[0]["STOP_USER"]))
                {
                    MNT_TODAY.initInsert_row();
                    MNT_TODAY.resultTable.Rows[MNT_Count]["FIELD_NAME"] = "STOP_USER";
                    MNT_TODAY.resultTable.Rows[MNT_Count]["VARCHAR_BEFORE"] = Convert.ToString(myTable.Rows[0]["STOP_USER"]);
                    MNT_TODAY.resultTable.Rows[MNT_Count]["VARCHAR_AFTER"] = this.STOP_USER;
                    MNT_Count++;
                }
                if (this.CREATE_DT > dateStart && this.CREATE_DT != Convert.ToDateTime(myTable.Rows[0]["CREATE_DT"]))
                {
                    if (this.CREATE_DT.ToString("yyyyMMdd") == "19000101" &&
                        Convert.ToDateTime(myTable.Rows[0]["CREATE_DT"]) == dateStart)
                    {
                    }
                    else
                    {
                        MNT_TODAY.initInsert_row();
                        MNT_TODAY.resultTable.Rows[MNT_Count]["FIELD_NAME"] = "CREATE_DT";
                        MNT_TODAY.resultTable.Rows[MNT_Count]["DT_BEFORE"] = Convert.ToDateTime(myTable.Rows[0]["CREATE_DT"]);
                        if (this.CREATE_DT.ToString("yyyyMMdd") == "19000101")
                        {
                            MNT_TODAY.resultTable.Rows[MNT_Count]["DT_AFTER"] = "1900-01-01";
                        }
                        else
                        {
                            MNT_TODAY.resultTable.Rows[MNT_Count]["DT_AFTER"] = this.CREATE_DT;
                        }
                        MNT_Count++;
                    }
                }
                if (this.CREATE_USER != null && this.CREATE_USER != Convert.ToString(myTable.Rows[0]["CREATE_USER"]))
                {
                    MNT_TODAY.initInsert_row();
                    MNT_TODAY.resultTable.Rows[MNT_Count]["FIELD_NAME"] = "CREATE_USER";
                    MNT_TODAY.resultTable.Rows[MNT_Count]["VARCHAR_BEFORE"] = Convert.ToString(myTable.Rows[0]["CREATE_USER"]);
                    MNT_TODAY.resultTable.Rows[MNT_Count]["VARCHAR_AFTER"] = this.CREATE_USER;
                    MNT_Count++;
                }
                if (this.ERROR_REASON != null && this.ERROR_REASON != Convert.ToString(myTable.Rows[0]["ERROR_REASON"]))
                {
                    MNT_TODAY.initInsert_row();
                    MNT_TODAY.resultTable.Rows[MNT_Count]["FIELD_NAME"] = "ERROR_REASON";
                    MNT_TODAY.resultTable.Rows[MNT_Count]["VARCHAR_BEFORE"] = Convert.ToString(myTable.Rows[0]["ERROR_REASON"]);
                    MNT_TODAY.resultTable.Rows[MNT_Count]["VARCHAR_AFTER"] = this.ERROR_REASON;
                    MNT_Count++;
                }
                if (this.ERROR_REASON_DT != null && this.ERROR_REASON_DT != Convert.ToString(myTable.Rows[0]["ERROR_REASON_DT"]))
                {
                    MNT_TODAY.initInsert_row();
                    MNT_TODAY.resultTable.Rows[MNT_Count]["FIELD_NAME"] = "ERROR_REASON_DT";
                    MNT_TODAY.resultTable.Rows[MNT_Count]["VARCHAR_BEFORE"] = Convert.ToString(myTable.Rows[0]["ERROR_REASON_DT"]);
                    MNT_TODAY.resultTable.Rows[MNT_Count]["VARCHAR_AFTER"] = this.ERROR_REASON_DT;
                    MNT_Count++;
                }
                if (this.PUBLIC_APPLY_CHAR_1 != null && this.PUBLIC_APPLY_CHAR_1 != Convert.ToString(myTable.Rows[0]["PUBLIC_APPLY_CHAR_1"]))
                {
                    MNT_TODAY.initInsert_row();
                    MNT_TODAY.resultTable.Rows[MNT_Count]["FIELD_NAME"] = "PUBLIC_APPLY_CHAR_1";
                    MNT_TODAY.resultTable.Rows[MNT_Count]["VARCHAR_BEFORE"] = Convert.ToString(myTable.Rows[0]["PUBLIC_APPLY_CHAR_1"]);
                    MNT_TODAY.resultTable.Rows[MNT_Count]["VARCHAR_AFTER"] = this.PUBLIC_APPLY_CHAR_1;
                    MNT_Count++;
                }
                if (this.PUBLIC_APPLY_CHAR_2 != null && this.PUBLIC_APPLY_CHAR_2 != Convert.ToString(myTable.Rows[0]["PUBLIC_APPLY_CHAR_2"]))
                {
                    MNT_TODAY.initInsert_row();
                    MNT_TODAY.resultTable.Rows[MNT_Count]["FIELD_NAME"] = "PUBLIC_APPLY_CHAR_2";
                    MNT_TODAY.resultTable.Rows[MNT_Count]["VARCHAR_BEFORE"] = Convert.ToString(myTable.Rows[0]["PUBLIC_APPLY_CHAR_2"]);
                    MNT_TODAY.resultTable.Rows[MNT_Count]["VARCHAR_AFTER"] = this.PUBLIC_APPLY_CHAR_2;
                    MNT_Count++;
                }
                if (this.PUBLIC_APPLY_CHAR_3 != null && this.PUBLIC_APPLY_CHAR_3 != Convert.ToString(myTable.Rows[0]["PUBLIC_APPLY_CHAR_3"]))
                {
                    MNT_TODAY.initInsert_row();
                    MNT_TODAY.resultTable.Rows[MNT_Count]["FIELD_NAME"] = "PUBLIC_APPLY_CHAR_3";
                    MNT_TODAY.resultTable.Rows[MNT_Count]["VARCHAR_BEFORE"] = Convert.ToString(myTable.Rows[0]["PUBLIC_APPLY_CHAR_3"]);
                    MNT_TODAY.resultTable.Rows[MNT_Count]["VARCHAR_AFTER"] = this.PUBLIC_APPLY_CHAR_3;
                    MNT_Count++;
                }
                if (this.PUBLIC_APPLY_CHAR_4 != null && this.PUBLIC_APPLY_CHAR_4 != Convert.ToString(myTable.Rows[0]["PUBLIC_APPLY_CHAR_4"]))
                {
                    MNT_TODAY.initInsert_row();
                    MNT_TODAY.resultTable.Rows[MNT_Count]["FIELD_NAME"] = "PUBLIC_APPLY_CHAR_4";
                    MNT_TODAY.resultTable.Rows[MNT_Count]["VARCHAR_BEFORE"] = Convert.ToString(myTable.Rows[0]["PUBLIC_APPLY_CHAR_4"]);
                    MNT_TODAY.resultTable.Rows[MNT_Count]["VARCHAR_AFTER"] = this.PUBLIC_APPLY_CHAR_4;
                    MNT_Count++;
                }
                if (this.PUBLIC_APPLY_CHAR_5 != null && this.PUBLIC_APPLY_CHAR_5 != Convert.ToString(myTable.Rows[0]["PUBLIC_APPLY_CHAR_5"]))
                {
                    MNT_TODAY.initInsert_row();
                    MNT_TODAY.resultTable.Rows[MNT_Count]["FIELD_NAME"] = "PUBLIC_APPLY_CHAR_5";
                    MNT_TODAY.resultTable.Rows[MNT_Count]["VARCHAR_BEFORE"] = Convert.ToString(myTable.Rows[0]["PUBLIC_APPLY_CHAR_5"]);
                    MNT_TODAY.resultTable.Rows[MNT_Count]["VARCHAR_AFTER"] = this.PUBLIC_APPLY_CHAR_5;
                    MNT_Count++;
                }
                if (this.PUBLIC_APPLY_AMT_1 > -1000000000000 && this.PUBLIC_APPLY_AMT_1 != Convert.ToDecimal(myTable.Rows[0]["PUBLIC_APPLY_AMT_1"]))
                {
                    MNT_TODAY.initInsert_row();
                    MNT_TODAY.resultTable.Rows[MNT_Count]["FIELD_NAME"] = "PUBLIC_APPLY_AMT_1";
                    MNT_TODAY.resultTable.Rows[MNT_Count]["DECIMAL_BEFORE"] = Convert.ToDecimal(myTable.Rows[0]["PUBLIC_APPLY_AMT_1"]);
                    MNT_TODAY.resultTable.Rows[MNT_Count]["DECIMAL_AFTER"] = this.PUBLIC_APPLY_AMT_1;
                    MNT_Count++;
                }
                if (this.PUBLIC_APPLY_AMT_2 > -1000000000000 && this.PUBLIC_APPLY_AMT_2 != Convert.ToDecimal(myTable.Rows[0]["PUBLIC_APPLY_AMT_2"]))
                {
                    MNT_TODAY.initInsert_row();
                    MNT_TODAY.resultTable.Rows[MNT_Count]["FIELD_NAME"] = "PUBLIC_APPLY_AMT_2";
                    MNT_TODAY.resultTable.Rows[MNT_Count]["DECIMAL_BEFORE"] = Convert.ToDecimal(myTable.Rows[0]["PUBLIC_APPLY_AMT_2"]);
                    MNT_TODAY.resultTable.Rows[MNT_Count]["DECIMAL_AFTER"] = this.PUBLIC_APPLY_AMT_2;
                    MNT_Count++;
                }
                if (this.PUBLIC_APPLY_AMT_3 > -1000000000000 && this.PUBLIC_APPLY_AMT_3 != Convert.ToDecimal(myTable.Rows[0]["PUBLIC_APPLY_AMT_3"]))
                {
                    MNT_TODAY.initInsert_row();
                    MNT_TODAY.resultTable.Rows[MNT_Count]["FIELD_NAME"] = "PUBLIC_APPLY_AMT_3";
                    MNT_TODAY.resultTable.Rows[MNT_Count]["DECIMAL_BEFORE"] = Convert.ToDecimal(myTable.Rows[0]["PUBLIC_APPLY_AMT_3"]);
                    MNT_TODAY.resultTable.Rows[MNT_Count]["DECIMAL_AFTER"] = this.PUBLIC_APPLY_AMT_3;
                    MNT_Count++;
                }
                if (this.PUBLIC_APPLY_AMT_4 > -1000000000000 && this.PUBLIC_APPLY_AMT_4 != Convert.ToDecimal(myTable.Rows[0]["PUBLIC_APPLY_AMT_4"]))
                {
                    MNT_TODAY.initInsert_row();
                    MNT_TODAY.resultTable.Rows[MNT_Count]["FIELD_NAME"] = "PUBLIC_APPLY_AMT_4";
                    MNT_TODAY.resultTable.Rows[MNT_Count]["DECIMAL_BEFORE"] = Convert.ToDecimal(myTable.Rows[0]["PUBLIC_APPLY_AMT_4"]);
                    MNT_TODAY.resultTable.Rows[MNT_Count]["DECIMAL_AFTER"] = this.PUBLIC_APPLY_AMT_4;
                    MNT_Count++;
                }
                if (this.PUBLIC_APPLY_AMT_5 > -1000000000000 && this.PUBLIC_APPLY_AMT_5 != Convert.ToDecimal(myTable.Rows[0]["PUBLIC_APPLY_AMT_5"]))
                {
                    MNT_TODAY.initInsert_row();
                    MNT_TODAY.resultTable.Rows[MNT_Count]["FIELD_NAME"] = "PUBLIC_APPLY_AMT_5";
                    MNT_TODAY.resultTable.Rows[MNT_Count]["DECIMAL_BEFORE"] = Convert.ToDecimal(myTable.Rows[0]["PUBLIC_APPLY_AMT_5"]);
                    MNT_TODAY.resultTable.Rows[MNT_Count]["DECIMAL_AFTER"] = this.PUBLIC_APPLY_AMT_5;
                    MNT_Count++;
                }
                if (this.PUBLIC_APPLY_DT_1 > dateStart && this.PUBLIC_APPLY_DT_1 != Convert.ToDateTime(myTable.Rows[0]["PUBLIC_APPLY_DT_1"]))
                {
                    if (this.PUBLIC_APPLY_DT_1.ToString("yyyyMMdd") == "19000101" &&
                        Convert.ToDateTime(myTable.Rows[0]["PUBLIC_APPLY_DT_1"]) == dateStart)
                    {
                    }
                    else
                    {
                        MNT_TODAY.initInsert_row();
                        MNT_TODAY.resultTable.Rows[MNT_Count]["FIELD_NAME"] = "PUBLIC_APPLY_DT_1";
                        MNT_TODAY.resultTable.Rows[MNT_Count]["DT_BEFORE"] = Convert.ToDateTime(myTable.Rows[0]["PUBLIC_APPLY_DT_1"]);
                        if (this.PUBLIC_APPLY_DT_1.ToString("yyyyMMdd") == "19000101")
                        {
                            MNT_TODAY.resultTable.Rows[MNT_Count]["DT_AFTER"] = "1900-01-01";
                        }
                        else
                        {
                            MNT_TODAY.resultTable.Rows[MNT_Count]["DT_AFTER"] = this.PUBLIC_APPLY_DT_1;
                        }
                        MNT_Count++;
                    }
                }
                if (this.PUBLIC_APPLY_DT_2 > dateStart && this.PUBLIC_APPLY_DT_2 != Convert.ToDateTime(myTable.Rows[0]["PUBLIC_APPLY_DT_2"]))
                {
                    if (this.PUBLIC_APPLY_DT_2.ToString("yyyyMMdd") == "19000101" &&
                        Convert.ToDateTime(myTable.Rows[0]["PUBLIC_APPLY_DT_2"]) == dateStart)
                    {
                    }
                    else
                    {
                        MNT_TODAY.initInsert_row();
                        MNT_TODAY.resultTable.Rows[MNT_Count]["FIELD_NAME"] = "PUBLIC_APPLY_DT_2";
                        MNT_TODAY.resultTable.Rows[MNT_Count]["DT_BEFORE"] = Convert.ToDateTime(myTable.Rows[0]["PUBLIC_APPLY_DT_2"]);
                        if (this.PUBLIC_APPLY_DT_2.ToString("yyyyMMdd") == "19000101")
                        {
                            MNT_TODAY.resultTable.Rows[MNT_Count]["DT_AFTER"] = "1900-01-01";
                        }
                        else
                        {
                            MNT_TODAY.resultTable.Rows[MNT_Count]["DT_AFTER"] = this.PUBLIC_APPLY_DT_2;
                        }
                        MNT_Count++;
                    }
                }
                if (this.PUBLIC_APPLY_DT_3 > dateStart && this.PUBLIC_APPLY_DT_3 != Convert.ToDateTime(myTable.Rows[0]["PUBLIC_APPLY_DT_3"]))
                {
                    if (this.PUBLIC_APPLY_DT_3.ToString("yyyyMMdd") == "19000101" &&
                        Convert.ToDateTime(myTable.Rows[0]["PUBLIC_APPLY_DT_3"]) == dateStart)
                    {
                    }
                    else
                    {
                        MNT_TODAY.initInsert_row();
                        MNT_TODAY.resultTable.Rows[MNT_Count]["FIELD_NAME"] = "PUBLIC_APPLY_DT_3";
                        MNT_TODAY.resultTable.Rows[MNT_Count]["DT_BEFORE"] = Convert.ToDateTime(myTable.Rows[0]["PUBLIC_APPLY_DT_3"]);
                        if (this.PUBLIC_APPLY_DT_3.ToString("yyyyMMdd") == "19000101")
                        {
                            MNT_TODAY.resultTable.Rows[MNT_Count]["DT_AFTER"] = "1900-01-01";
                        }
                        else
                        {
                            MNT_TODAY.resultTable.Rows[MNT_Count]["DT_AFTER"] = this.PUBLIC_APPLY_DT_3;
                        }
                        MNT_Count++;
                    }
                }
                if (this.PUBLIC_APPLY_DT_4 > dateStart && this.PUBLIC_APPLY_DT_4 != Convert.ToDateTime(myTable.Rows[0]["PUBLIC_APPLY_DT_4"]))
                {
                    if (this.PUBLIC_APPLY_DT_4.ToString("yyyyMMdd") == "19000101" &&
                        Convert.ToDateTime(myTable.Rows[0]["PUBLIC_APPLY_DT_4"]) == dateStart)
                    {
                    }
                    else
                    {
                        MNT_TODAY.initInsert_row();
                        MNT_TODAY.resultTable.Rows[MNT_Count]["FIELD_NAME"] = "PUBLIC_APPLY_DT_4";
                        MNT_TODAY.resultTable.Rows[MNT_Count]["DT_BEFORE"] = Convert.ToDateTime(myTable.Rows[0]["PUBLIC_APPLY_DT_4"]);
                        if (this.PUBLIC_APPLY_DT_4.ToString("yyyyMMdd") == "19000101")
                        {
                            MNT_TODAY.resultTable.Rows[MNT_Count]["DT_AFTER"] = "1900-01-01";
                        }
                        else
                        {
                            MNT_TODAY.resultTable.Rows[MNT_Count]["DT_AFTER"] = this.PUBLIC_APPLY_DT_4;
                        }
                        MNT_Count++;
                    }
                }
                if (this.PUBLIC_APPLY_DT_5 > dateStart && this.PUBLIC_APPLY_DT_5 != Convert.ToDateTime(myTable.Rows[0]["PUBLIC_APPLY_DT_5"]))
                {
                    if (this.PUBLIC_APPLY_DT_5.ToString("yyyyMMdd") == "19000101" &&
                        Convert.ToDateTime(myTable.Rows[0]["PUBLIC_APPLY_DT_5"]) == dateStart)
                    {
                    }
                    else
                    {
                        MNT_TODAY.initInsert_row();
                        MNT_TODAY.resultTable.Rows[MNT_Count]["FIELD_NAME"] = "PUBLIC_APPLY_DT_5";
                        MNT_TODAY.resultTable.Rows[MNT_Count]["DT_BEFORE"] = Convert.ToDateTime(myTable.Rows[0]["PUBLIC_APPLY_DT_5"]);
                        if (this.PUBLIC_APPLY_DT_5.ToString("yyyyMMdd") == "19000101")
                        {
                            MNT_TODAY.resultTable.Rows[MNT_Count]["DT_AFTER"] = "1900-01-01";
                        }
                        else
                        {
                            MNT_TODAY.resultTable.Rows[MNT_Count]["DT_AFTER"] = this.PUBLIC_APPLY_DT_5;
                        }
                        MNT_Count++;
                    }
                }
                #endregion
                if (MNT_TODAY.resultTable.Rows.Count == 0)  //沒有欄位有異動
                {
                    msg_code = "S0001";
                }
                else
                {
                    msg_code = update();
                    if ("S0000".Equals(msg_code))
                    {
                        MNT_TODAY.insert_by_DT();
                    }
                }
            }
            catch (SqlException e)
            {
                MSG.strMsg = Convert.ToString(e.Number) + '-' + e.Message;
                msg_code = MSG.getMsg();
            }
            finally
            {

            }
            return msg_code;
        }
        #endregion
        #region insert(Hashtable HashTB)
        public string insert(Hashtable HashTB)
        {
            #region Modify History
            /// <history>
            /// <design>
            /// <name>Cybersoft.COCA DaoGenerator</name>
            /// <date>2014/12/19 下午 04:50:54</date>
            #endregion
            try
            {
                #region 宣告MNT_TODAYDAO並放入初始值
                Cybersoft.Dao.Core.MNT_TODAYDao MNT_TODAY = new Cybersoft.Dao.Core.MNT_TODAYDao();
                MNT_TODAY.table_define();
                MNT_TODAY.strinitTBL_NAME = "PUBLIC_APPLY";
                MNT_TODAY.strinitPOST_RESULT = "00";
                MNT_TODAY.strinitMNT_PGM = Convert.ToString(HashTB["SessionFN_CODE"]);
                MNT_TODAY.strinitMNT_USER = Convert.ToString(HashTB["SessionAccount"]);
                MNT_TODAY.DateTimeinitMNT_DT = DateTime.Now;
                MNT_TODAY.DateTimeinitEFF_DTE = DateTime.Now;
                MNT_TODAY.DateTimeinitPOSTING_DTE = DateTime.Now;
                #endregion
                #region 取得鍵值
                Cybersoft.Dao.Core.KEY_ITEM KEY = new Cybersoft.Dao.Core.KEY_ITEM();
                KEY.getKeyItem(HashTB);
                MNT_TODAY.strinitBU = KEY.strBU;
                MNT_TODAY.strinitACCT_NBR = KEY.strACCT_NBR;
                MNT_TODAY.strinitPRODUCT = KEY.strPRODUCT;
                MNT_TODAY.strinitCURRENCY = KEY.strCURRENCY;
                MNT_TODAY.strinitCARD_NBR = KEY.strCARD_NBR;
                MNT_TODAY.strinitOTHER_KEY_1 = KEY.strOTHER_KEY_1;
                MNT_TODAY.strinitOTHER_KEY_2 = KEY.strOTHER_KEY_2;
                MNT_TODAY.strinitACQ_BU = KEY.strACQ_BU;
                MNT_TODAY.strinitMER_NBR = KEY.strMER_NBR;
                #endregion
                #region 寫入log
                //修改欄位部分
                MNT_TODAY.initInsert_row();
                MNT_TODAY.resultTable.Rows[0]["FIELD_NAME"] = "INSERT";
                #endregion
                msg_code = insert();
                if (msg_code == "S0000")
                {
                    MNT_TODAY.insert_by_DT();
                }
            }
            catch (SqlException e)
            {
                MSG.strMsg = Convert.ToString(e.Number) + '-' + e.Message;
                msg_code = MSG.getMsg();
            }
            finally
            {

            }
            return msg_code;
        }
        #endregion
        #region query_group_by()
        public string query_group_by()
        {
            #region Modify History
            /// <history>
            /// <design>
            /// <name>Cybersoft.COCA DaoGenerator</name>
            /// <date>2014/12/19 下午 04:50:54</date>
            #endregion
            try
            {
                #region set SQL statement
                StringBuilder sbstrSQL = new StringBuilder();
                sbstrSQL.Append("SELECT ");
                #endregion
                #region GROUP BY
                if (this.groupPAY_CARD_NBR != null)
                {
                    sbstrSQL.Append("a.PAY_CARD_NBR");
                }
                else
                {
                    sbstrSQL.Append("''");
                }
                sbstrSQL.Append(" as PAY_CARD_NBR,");
                if (this.groupPAY_TYPE != null)
                {
                    sbstrSQL.Append("a.PAY_TYPE");
                }
                else
                {
                    sbstrSQL.Append("''");
                }
                sbstrSQL.Append(" as PAY_TYPE,");
                if (this.groupPAY_NBR != null)
                {
                    sbstrSQL.Append("a.PAY_NBR");
                }
                else
                {
                    sbstrSQL.Append("''");
                }
                sbstrSQL.Append(" as PAY_NBR,");
                if (this.groupSEQ != null)
                {
                    sbstrSQL.Append("a.SEQ");
                }
                else
                {
                    sbstrSQL.Append("''");
                }
                sbstrSQL.Append(" as SEQ,");
                if (this.groupBU != null)
                {
                    sbstrSQL.Append("a.BU");
                }
                else
                {
                    sbstrSQL.Append("''");
                }
                sbstrSQL.Append(" as BU,");
                if (this.groupPRODUCT != null)
                {
                    sbstrSQL.Append("a.PRODUCT");
                }
                else
                {
                    sbstrSQL.Append("''");
                }
                sbstrSQL.Append(" as PRODUCT,");
                if (this.groupCARD_PRODUCT != null)
                {
                    sbstrSQL.Append("a.CARD_PRODUCT");
                }
                else
                {
                    sbstrSQL.Append("''");
                }
                sbstrSQL.Append(" as CARD_PRODUCT,");
                if (this.groupACCT_NBR != null)
                {
                    sbstrSQL.Append("a.ACCT_NBR");
                }
                else
                {
                    sbstrSQL.Append("''");
                }
                sbstrSQL.Append(" as ACCT_NBR,");
                if (this.groupPAY_ACCT_NBR != null)
                {
                    sbstrSQL.Append("a.PAY_ACCT_NBR");
                }
                else
                {
                    sbstrSQL.Append("''");
                }
                sbstrSQL.Append(" as PAY_ACCT_NBR,");
                if (this.groupPAY_CARD_NBR_PREV != null)
                {
                    sbstrSQL.Append("a.PAY_CARD_NBR_PREV");
                }
                else
                {
                    sbstrSQL.Append("''");
                }
                sbstrSQL.Append(" as PAY_CARD_NBR_PREV,");
                if (this.groupPAY_ACCT_NBR_PREV != null)
                {
                    sbstrSQL.Append("a.PAY_ACCT_NBR_PREV");
                }
                else
                {
                    sbstrSQL.Append("''");
                }
                sbstrSQL.Append(" as PAY_ACCT_NBR_PREV,");
                if (this.groupCUST_SEQ != null)
                {
                    sbstrSQL.Append("a.CUST_SEQ");
                }
                else
                {
                    sbstrSQL.Append("''");
                }
                sbstrSQL.Append(" as CUST_SEQ,");
                if (this.groupEXPIR_DTE != null)
                {
                    sbstrSQL.Append("a.EXPIR_DTE");
                }
                else
                {
                    sbstrSQL.Append("''");
                }
                sbstrSQL.Append(" as EXPIR_DTE,");
                if (this.groupAPPLY_DTE != null)
                {
                    sbstrSQL.Append("a.APPLY_DTE");
                }
                else
                {
                    sbstrSQL.Append("''");
                }
                sbstrSQL.Append(" as APPLY_DTE,");
                if (this.groupFIRST_DTE != null)
                {
                    sbstrSQL.Append("a.FIRST_DTE");
                }
                else
                {
                    sbstrSQL.Append("''");
                }
                sbstrSQL.Append(" as FIRST_DTE,");
                if (this.groupPAY_DTE != null)
                {
                    sbstrSQL.Append("a.PAY_DTE");
                }
                else
                {
                    sbstrSQL.Append("''");
                }
                sbstrSQL.Append(" as PAY_DTE,");
                if (this.groupVAILD_FLAG != null)
                {
                    sbstrSQL.Append("a.VAILD_FLAG");
                }
                else
                {
                    sbstrSQL.Append("''");
                }
                sbstrSQL.Append(" as VAILD_FLAG,");
                if (this.groupSEND_MSG_FLAG != null)
                {
                    sbstrSQL.Append("a.SEND_MSG_FLAG");
                }
                else
                {
                    sbstrSQL.Append("''");
                }
                sbstrSQL.Append(" as SEND_MSG_FLAG,");
                if (this.groupREPLY_FLAG != null)
                {
                    sbstrSQL.Append("a.REPLY_FLAG");
                }
                else
                {
                    sbstrSQL.Append("''");
                }
                sbstrSQL.Append(" as REPLY_FLAG,");
                if (this.groupREPLY_DTE != null)
                {
                    sbstrSQL.Append("a.REPLY_DTE");
                }
                else
                {
                    sbstrSQL.Append("''");
                }
                sbstrSQL.Append(" as REPLY_DTE,");
                if (this.groupSTOP_DTE != null)
                {
                    sbstrSQL.Append("a.STOP_DTE");
                }
                else
                {
                    sbstrSQL.Append("''");
                }
                sbstrSQL.Append(" as STOP_DTE,");
                if (this.groupSTOP_USER != null)
                {
                    sbstrSQL.Append("a.STOP_USER");
                }
                else
                {
                    sbstrSQL.Append("''");
                }
                sbstrSQL.Append(" as STOP_USER,");
                if (this.groupCREATE_DT != null)
                {
                    sbstrSQL.Append("a.CREATE_DT");
                }
                else
                {
                    sbstrSQL.Append("''");
                }
                sbstrSQL.Append(" as CREATE_DT,");
                if (this.groupCREATE_USER != null)
                {
                    sbstrSQL.Append("a.CREATE_USER");
                }
                else
                {
                    sbstrSQL.Append("''");
                }
                sbstrSQL.Append(" as CREATE_USER,");
                if (this.groupERROR_REASON != null)
                {
                    sbstrSQL.Append("a.ERROR_REASON");
                }
                else
                {
                    sbstrSQL.Append("''");
                }
                sbstrSQL.Append(" as ERROR_REASON,");
                if (this.groupERROR_REASON_DT != null)
                {
                    sbstrSQL.Append("a.ERROR_REASON_DT");
                }
                else
                {
                    sbstrSQL.Append("''");
                }
                sbstrSQL.Append(" as ERROR_REASON_DT,");
                if (this.groupPUBLIC_APPLY_CHAR_1 != null)
                {
                    sbstrSQL.Append("a.PUBLIC_APPLY_CHAR_1");
                }
                else
                {
                    sbstrSQL.Append("''");
                }
                sbstrSQL.Append(" as PUBLIC_APPLY_CHAR_1,");
                if (this.groupPUBLIC_APPLY_CHAR_2 != null)
                {
                    sbstrSQL.Append("a.PUBLIC_APPLY_CHAR_2");
                }
                else
                {
                    sbstrSQL.Append("''");
                }
                sbstrSQL.Append(" as PUBLIC_APPLY_CHAR_2,");
                if (this.groupPUBLIC_APPLY_CHAR_3 != null)
                {
                    sbstrSQL.Append("a.PUBLIC_APPLY_CHAR_3");
                }
                else
                {
                    sbstrSQL.Append("''");
                }
                sbstrSQL.Append(" as PUBLIC_APPLY_CHAR_3,");
                if (this.groupPUBLIC_APPLY_CHAR_4 != null)
                {
                    sbstrSQL.Append("a.PUBLIC_APPLY_CHAR_4");
                }
                else
                {
                    sbstrSQL.Append("''");
                }
                sbstrSQL.Append(" as PUBLIC_APPLY_CHAR_4,");
                if (this.groupPUBLIC_APPLY_CHAR_5 != null)
                {
                    sbstrSQL.Append("a.PUBLIC_APPLY_CHAR_5");
                }
                else
                {
                    sbstrSQL.Append("''");
                }
                sbstrSQL.Append(" as PUBLIC_APPLY_CHAR_5,");
                if (this.groupPUBLIC_APPLY_AMT_1 != null)
                {
                    sbstrSQL.Append("a.PUBLIC_APPLY_AMT_1");
                }
                else
                {
                    sbstrSQL.Append("''");
                }
                sbstrSQL.Append(" as PUBLIC_APPLY_AMT_1,");
                if (this.groupPUBLIC_APPLY_AMT_2 != null)
                {
                    sbstrSQL.Append("a.PUBLIC_APPLY_AMT_2");
                }
                else
                {
                    sbstrSQL.Append("''");
                }
                sbstrSQL.Append(" as PUBLIC_APPLY_AMT_2,");
                if (this.groupPUBLIC_APPLY_AMT_3 != null)
                {
                    sbstrSQL.Append("a.PUBLIC_APPLY_AMT_3");
                }
                else
                {
                    sbstrSQL.Append("''");
                }
                sbstrSQL.Append(" as PUBLIC_APPLY_AMT_3,");
                if (this.groupPUBLIC_APPLY_AMT_4 != null)
                {
                    sbstrSQL.Append("a.PUBLIC_APPLY_AMT_4");
                }
                else
                {
                    sbstrSQL.Append("''");
                }
                sbstrSQL.Append(" as PUBLIC_APPLY_AMT_4,");
                if (this.groupPUBLIC_APPLY_AMT_5 != null)
                {
                    sbstrSQL.Append("a.PUBLIC_APPLY_AMT_5");
                }
                else
                {
                    sbstrSQL.Append("''");
                }
                sbstrSQL.Append(" as PUBLIC_APPLY_AMT_5,");
                if (this.groupPUBLIC_APPLY_DT_1 != null)
                {
                    sbstrSQL.Append("a.PUBLIC_APPLY_DT_1");
                }
                else
                {
                    sbstrSQL.Append("''");
                }
                sbstrSQL.Append(" as PUBLIC_APPLY_DT_1,");
                if (this.groupPUBLIC_APPLY_DT_2 != null)
                {
                    sbstrSQL.Append("a.PUBLIC_APPLY_DT_2");
                }
                else
                {
                    sbstrSQL.Append("''");
                }
                sbstrSQL.Append(" as PUBLIC_APPLY_DT_2,");
                if (this.groupPUBLIC_APPLY_DT_3 != null)
                {
                    sbstrSQL.Append("a.PUBLIC_APPLY_DT_3");
                }
                else
                {
                    sbstrSQL.Append("''");
                }
                sbstrSQL.Append(" as PUBLIC_APPLY_DT_3,");
                if (this.groupPUBLIC_APPLY_DT_4 != null)
                {
                    sbstrSQL.Append("a.PUBLIC_APPLY_DT_4");
                }
                else
                {
                    sbstrSQL.Append("''");
                }
                sbstrSQL.Append(" as PUBLIC_APPLY_DT_4,");
                if (this.groupPUBLIC_APPLY_DT_5 != null)
                {
                    sbstrSQL.Append("a.PUBLIC_APPLY_DT_5");
                }
                else
                {
                    sbstrSQL.Append("''");
                }
                sbstrSQL.Append(" as PUBLIC_APPLY_DT_5,");
                if (this.groupMNT_DT != null)
                {
                    sbstrSQL.Append("a.MNT_DT");
                }
                else
                {
                    sbstrSQL.Append("''");
                }
                sbstrSQL.Append(" as MNT_DT,");
                if (this.groupMNT_USER != null)
                {
                    sbstrSQL.Append("a.MNT_USER");
                }
                else
                {
                    sbstrSQL.Append("''");
                }
                sbstrSQL.Append(" as MNT_USER,");
                sbstrSQL.Remove(sbstrSQL.ToString().Length - 1, 1); //移除最後一個逗號
                #endregion
                sbstrSQL.Append(" FROM PUBLIC_APPLY a Where 1=1  ");
                #region WHERE CONIDTION
                if (this.wherePAY_CARD_NBR != null)
                {
                    sbstrSQL.Append(" and a.PAY_CARD_NBR=@wherePAY_CARD_NBR ");
                }
                if (this.wherePAY_TYPE != null)
                {
                    sbstrSQL.Append(" and a.PAY_TYPE=@wherePAY_TYPE ");
                }
                if (this.wherePAY_NBR != null)
                {
                    sbstrSQL.Append(" and a.PAY_NBR=@wherePAY_NBR ");
                }
                if (this.whereSEQ != null)
                {
                    sbstrSQL.Append(" and a.SEQ=@whereSEQ ");
                }
                if (this.whereBU != null)
                {
                    sbstrSQL.Append(" and a.BU=@whereBU ");
                }
                if (this.wherePRODUCT != null)
                {
                    sbstrSQL.Append(" and a.PRODUCT=@wherePRODUCT ");
                }
                if (this.whereCARD_PRODUCT != null)
                {
                    sbstrSQL.Append(" and a.CARD_PRODUCT=@whereCARD_PRODUCT ");
                }
                if (this.whereACCT_NBR != null)
                {
                    sbstrSQL.Append(" and a.ACCT_NBR=@whereACCT_NBR ");
                }
                if (this.wherePAY_ACCT_NBR != null)
                {
                    sbstrSQL.Append(" and a.PAY_ACCT_NBR=@wherePAY_ACCT_NBR ");
                }
                if (this.wherePAY_CARD_NBR_PREV != null)
                {
                    sbstrSQL.Append(" and a.PAY_CARD_NBR_PREV=@wherePAY_CARD_NBR_PREV ");
                }
                if (this.wherePAY_ACCT_NBR_PREV != null)
                {
                    sbstrSQL.Append(" and a.PAY_ACCT_NBR_PREV=@wherePAY_ACCT_NBR_PREV ");
                }
                if (this.whereCUST_SEQ != null)
                {
                    sbstrSQL.Append(" and a.CUST_SEQ=@whereCUST_SEQ ");
                }
                if (this.whereEXPIR_DTE != null)
                {
                    sbstrSQL.Append(" and a.EXPIR_DTE=@whereEXPIR_DTE ");
                }
                if (this.whereAPPLY_DTE != null)
                {
                    sbstrSQL.Append(" and a.APPLY_DTE=@whereAPPLY_DTE ");
                }
                if (this.whereFIRST_DTE != null)
                {
                    sbstrSQL.Append(" and a.FIRST_DTE=@whereFIRST_DTE ");
                }
                if (this.wherePAY_DTE != null)
                {
                    sbstrSQL.Append(" and a.PAY_DTE=@wherePAY_DTE ");
                }
                if (this.whereVAILD_FLAG != null)
                {
                    sbstrSQL.Append(" and a.VAILD_FLAG=@whereVAILD_FLAG ");
                }
                if (this.whereSEND_MSG_FLAG != null)
                {
                    sbstrSQL.Append(" and a.SEND_MSG_FLAG=@whereSEND_MSG_FLAG ");
                }
                if (this.whereREPLY_FLAG != null)
                {
                    sbstrSQL.Append(" and a.REPLY_FLAG=@whereREPLY_FLAG ");
                }
                if (this.whereREPLY_DTE != null)
                {
                    sbstrSQL.Append(" and a.REPLY_DTE=@whereREPLY_DTE ");
                }
                if (this.whereSTOP_DTE > dateStart)
                {
                    sbstrSQL.Append("  and a.STOP_DTE=@whereSTOP_DTE ");
                }
                if (this.whereSTOP_USER != null)
                {
                    sbstrSQL.Append(" and a.STOP_USER=@whereSTOP_USER ");
                }
                if (this.whereCREATE_DT > dateStart)
                {
                    sbstrSQL.Append("  and a.CREATE_DT=@whereCREATE_DT ");
                }
                if (this.whereCREATE_USER != null)
                {
                    sbstrSQL.Append(" and a.CREATE_USER=@whereCREATE_USER ");
                }
                if (this.whereERROR_REASON != null)
                {
                    sbstrSQL.Append(" and a.ERROR_REASON=@whereERROR_REASON ");
                }
                if (this.whereERROR_REASON_DT != null)
                {
                    sbstrSQL.Append(" and a.ERROR_REASON_DT=@whereERROR_REASON_DT ");
                }
                if (this.wherePUBLIC_APPLY_CHAR_1 != null)
                {
                    sbstrSQL.Append(" and a.PUBLIC_APPLY_CHAR_1=@wherePUBLIC_APPLY_CHAR_1 ");
                }
                if (this.wherePUBLIC_APPLY_CHAR_2 != null)
                {
                    sbstrSQL.Append(" and a.PUBLIC_APPLY_CHAR_2=@wherePUBLIC_APPLY_CHAR_2 ");
                }
                if (this.wherePUBLIC_APPLY_CHAR_3 != null)
                {
                    sbstrSQL.Append(" and a.PUBLIC_APPLY_CHAR_3=@wherePUBLIC_APPLY_CHAR_3 ");
                }
                if (this.wherePUBLIC_APPLY_CHAR_4 != null)
                {
                    sbstrSQL.Append(" and a.PUBLIC_APPLY_CHAR_4=@wherePUBLIC_APPLY_CHAR_4 ");
                }
                if (this.wherePUBLIC_APPLY_CHAR_5 != null)
                {
                    sbstrSQL.Append(" and a.PUBLIC_APPLY_CHAR_5=@wherePUBLIC_APPLY_CHAR_5 ");
                }
                if (this.wherePUBLIC_APPLY_AMT_1 > -1000000000000)
                {
                    sbstrSQL.Append(" and a.PUBLIC_APPLY_AMT_1=@wherePUBLIC_APPLY_AMT_1 ");
                }
                if (this.wherePUBLIC_APPLY_AMT_2 > -1000000000000)
                {
                    sbstrSQL.Append(" and a.PUBLIC_APPLY_AMT_2=@wherePUBLIC_APPLY_AMT_2 ");
                }
                if (this.wherePUBLIC_APPLY_AMT_3 > -1000000000000)
                {
                    sbstrSQL.Append(" and a.PUBLIC_APPLY_AMT_3=@wherePUBLIC_APPLY_AMT_3 ");
                }
                if (this.wherePUBLIC_APPLY_AMT_4 > -1000000000000)
                {
                    sbstrSQL.Append(" and a.PUBLIC_APPLY_AMT_4=@wherePUBLIC_APPLY_AMT_4 ");
                }
                if (this.wherePUBLIC_APPLY_AMT_5 > -1000000000000)
                {
                    sbstrSQL.Append(" and a.PUBLIC_APPLY_AMT_5=@wherePUBLIC_APPLY_AMT_5 ");
                }
                if (this.wherePUBLIC_APPLY_DT_1 > dateStart)
                {
                    sbstrSQL.Append("  and a.PUBLIC_APPLY_DT_1=@wherePUBLIC_APPLY_DT_1 ");
                }
                if (this.wherePUBLIC_APPLY_DT_2 > dateStart)
                {
                    sbstrSQL.Append("  and a.PUBLIC_APPLY_DT_2=@wherePUBLIC_APPLY_DT_2 ");
                }
                if (this.wherePUBLIC_APPLY_DT_3 > dateStart)
                {
                    sbstrSQL.Append("  and a.PUBLIC_APPLY_DT_3=@wherePUBLIC_APPLY_DT_3 ");
                }
                if (this.wherePUBLIC_APPLY_DT_4 > dateStart)
                {
                    sbstrSQL.Append("  and a.PUBLIC_APPLY_DT_4=@wherePUBLIC_APPLY_DT_4 ");
                }
                if (this.wherePUBLIC_APPLY_DT_5 > dateStart)
                {
                    sbstrSQL.Append("  and a.PUBLIC_APPLY_DT_5=@wherePUBLIC_APPLY_DT_5 ");
                }
                if (this.whereMNT_DT > dateStart)
                {
                    sbstrSQL.Append("  and a.MNT_DT=@whereMNT_DT ");
                }
                if (this.whereMNT_USER != null)
                {
                    sbstrSQL.Append(" and a.MNT_USER=@whereMNT_USER ");
                }
                #endregion
                sbstrSQL.Append("GROUP BY ");
                #region GROUP BY
                if (this.groupPAY_CARD_NBR != null)
                {
                    sbstrSQL.Append("a.PAY_CARD_NBR,");
                }
                if (this.groupPAY_TYPE != null)
                {
                    sbstrSQL.Append("a.PAY_TYPE,");
                }
                if (this.groupPAY_NBR != null)
                {
                    sbstrSQL.Append("a.PAY_NBR,");
                }
                if (this.groupSEQ != null)
                {
                    sbstrSQL.Append("a.SEQ,");
                }
                if (this.groupBU != null)
                {
                    sbstrSQL.Append("a.BU,");
                }
                if (this.groupPRODUCT != null)
                {
                    sbstrSQL.Append("a.PRODUCT,");
                }
                if (this.groupCARD_PRODUCT != null)
                {
                    sbstrSQL.Append("a.CARD_PRODUCT,");
                }
                if (this.groupACCT_NBR != null)
                {
                    sbstrSQL.Append("a.ACCT_NBR,");
                }
                if (this.groupPAY_ACCT_NBR != null)
                {
                    sbstrSQL.Append("a.PAY_ACCT_NBR,");
                }
                if (this.groupPAY_CARD_NBR_PREV != null)
                {
                    sbstrSQL.Append("a.PAY_CARD_NBR_PREV,");
                }
                if (this.groupPAY_ACCT_NBR_PREV != null)
                {
                    sbstrSQL.Append("a.PAY_ACCT_NBR_PREV,");
                }
                if (this.groupCUST_SEQ != null)
                {
                    sbstrSQL.Append("a.CUST_SEQ,");
                }
                if (this.groupEXPIR_DTE != null)
                {
                    sbstrSQL.Append("a.EXPIR_DTE,");
                }
                if (this.groupAPPLY_DTE != null)
                {
                    sbstrSQL.Append("a.APPLY_DTE,");
                }
                if (this.groupFIRST_DTE != null)
                {
                    sbstrSQL.Append("a.FIRST_DTE,");
                }
                if (this.groupPAY_DTE != null)
                {
                    sbstrSQL.Append("a.PAY_DTE,");
                }
                if (this.groupVAILD_FLAG != null)
                {
                    sbstrSQL.Append("a.VAILD_FLAG,");
                }
                if (this.groupSEND_MSG_FLAG != null)
                {
                    sbstrSQL.Append("a.SEND_MSG_FLAG,");
                }
                if (this.groupREPLY_FLAG != null)
                {
                    sbstrSQL.Append("a.REPLY_FLAG,");
                }
                if (this.groupREPLY_DTE != null)
                {
                    sbstrSQL.Append("a.REPLY_DTE,");
                }
                if (this.groupSTOP_DTE != null)
                {
                    sbstrSQL.Append("a.STOP_DTE,");
                }
                if (this.groupSTOP_USER != null)
                {
                    sbstrSQL.Append("a.STOP_USER,");
                }
                if (this.groupCREATE_DT != null)
                {
                    sbstrSQL.Append("a.CREATE_DT,");
                }
                if (this.groupCREATE_USER != null)
                {
                    sbstrSQL.Append("a.CREATE_USER,");
                }
                if (this.groupERROR_REASON != null)
                {
                    sbstrSQL.Append("a.ERROR_REASON,");
                }
                if (this.groupERROR_REASON_DT != null)
                {
                    sbstrSQL.Append("a.ERROR_REASON_DT,");
                }
                if (this.groupPUBLIC_APPLY_CHAR_1 != null)
                {
                    sbstrSQL.Append("a.PUBLIC_APPLY_CHAR_1,");
                }
                if (this.groupPUBLIC_APPLY_CHAR_2 != null)
                {
                    sbstrSQL.Append("a.PUBLIC_APPLY_CHAR_2,");
                }
                if (this.groupPUBLIC_APPLY_CHAR_3 != null)
                {
                    sbstrSQL.Append("a.PUBLIC_APPLY_CHAR_3,");
                }
                if (this.groupPUBLIC_APPLY_CHAR_4 != null)
                {
                    sbstrSQL.Append("a.PUBLIC_APPLY_CHAR_4,");
                }
                if (this.groupPUBLIC_APPLY_CHAR_5 != null)
                {
                    sbstrSQL.Append("a.PUBLIC_APPLY_CHAR_5,");
                }
                if (this.groupPUBLIC_APPLY_AMT_1 != null)
                {
                    sbstrSQL.Append("a.PUBLIC_APPLY_AMT_1,");
                }
                if (this.groupPUBLIC_APPLY_AMT_2 != null)
                {
                    sbstrSQL.Append("a.PUBLIC_APPLY_AMT_2,");
                }
                if (this.groupPUBLIC_APPLY_AMT_3 != null)
                {
                    sbstrSQL.Append("a.PUBLIC_APPLY_AMT_3,");
                }
                if (this.groupPUBLIC_APPLY_AMT_4 != null)
                {
                    sbstrSQL.Append("a.PUBLIC_APPLY_AMT_4,");
                }
                if (this.groupPUBLIC_APPLY_AMT_5 != null)
                {
                    sbstrSQL.Append("a.PUBLIC_APPLY_AMT_5,");
                }
                if (this.groupPUBLIC_APPLY_DT_1 != null)
                {
                    sbstrSQL.Append("a.PUBLIC_APPLY_DT_1,");
                }
                if (this.groupPUBLIC_APPLY_DT_2 != null)
                {
                    sbstrSQL.Append("a.PUBLIC_APPLY_DT_2,");
                }
                if (this.groupPUBLIC_APPLY_DT_3 != null)
                {
                    sbstrSQL.Append("a.PUBLIC_APPLY_DT_3,");
                }
                if (this.groupPUBLIC_APPLY_DT_4 != null)
                {
                    sbstrSQL.Append("a.PUBLIC_APPLY_DT_4,");
                }
                if (this.groupPUBLIC_APPLY_DT_5 != null)
                {
                    sbstrSQL.Append("a.PUBLIC_APPLY_DT_5,");
                }
                if (this.groupMNT_DT != null)
                {
                    sbstrSQL.Append("a.MNT_DT,");
                }
                if (this.groupMNT_USER != null)
                {
                    sbstrSQL.Append("a.MNT_USER,");
                }
                #endregion
                sbstrSQL.Remove(sbstrSQL.ToString().Length - 1, 1); //移除最後一個逗號
                this.SelectOperator.SqlText = sbstrSQL.ToString();
                #region replace SQL parameter
                if (sbstrSQL.ToString().Contains("@wherePAY_CARD_NBR "))
                {
                    this.SelectOperator.SetValue("@wherePAY_CARD_NBR", this.wherePAY_CARD_NBR, SqlDbType.VarChar);
                }
                if (sbstrSQL.ToString().Contains("@wherePAY_TYPE "))
                {
                    this.SelectOperator.SetValue("@wherePAY_TYPE", this.wherePAY_TYPE, SqlDbType.VarChar);
                }
                if (sbstrSQL.ToString().Contains("@wherePAY_NBR "))
                {
                    this.SelectOperator.SetValue("@wherePAY_NBR", this.wherePAY_NBR, SqlDbType.VarChar);
                }
                if (sbstrSQL.ToString().Contains("@whereSEQ "))
                {
                    this.SelectOperator.SetValue("@whereSEQ", this.whereSEQ, SqlDbType.VarChar);
                }
                if (sbstrSQL.ToString().Contains("@whereBU "))
                {
                    this.SelectOperator.SetValue("@whereBU", this.whereBU, SqlDbType.VarChar);
                }
                if (sbstrSQL.ToString().Contains("@wherePRODUCT "))
                {
                    this.SelectOperator.SetValue("@wherePRODUCT", this.wherePRODUCT, SqlDbType.VarChar);
                }
                if (sbstrSQL.ToString().Contains("@whereCARD_PRODUCT "))
                {
                    this.SelectOperator.SetValue("@whereCARD_PRODUCT", this.whereCARD_PRODUCT, SqlDbType.VarChar);
                }
                if (sbstrSQL.ToString().Contains("@whereACCT_NBR "))
                {
                    this.SelectOperator.SetValue("@whereACCT_NBR", this.whereACCT_NBR, SqlDbType.VarChar);
                }
                if (sbstrSQL.ToString().Contains("@wherePAY_ACCT_NBR "))
                {
                    this.SelectOperator.SetValue("@wherePAY_ACCT_NBR", this.wherePAY_ACCT_NBR, SqlDbType.VarChar);
                }
                if (sbstrSQL.ToString().Contains("@wherePAY_CARD_NBR_PREV "))
                {
                    this.SelectOperator.SetValue("@wherePAY_CARD_NBR_PREV", this.wherePAY_CARD_NBR_PREV, SqlDbType.VarChar);
                }
                if (sbstrSQL.ToString().Contains("@wherePAY_ACCT_NBR_PREV "))
                {
                    this.SelectOperator.SetValue("@wherePAY_ACCT_NBR_PREV", this.wherePAY_ACCT_NBR_PREV, SqlDbType.VarChar);
                }
                if (sbstrSQL.ToString().Contains("@whereCUST_SEQ "))
                {
                    this.SelectOperator.SetValue("@whereCUST_SEQ", this.whereCUST_SEQ, SqlDbType.VarChar);
                }
                if (sbstrSQL.ToString().Contains("@whereEXPIR_DTE "))
                {
                    this.SelectOperator.SetValue("@whereEXPIR_DTE", this.whereEXPIR_DTE, SqlDbType.VarChar);
                }
                if (sbstrSQL.ToString().Contains("@whereAPPLY_DTE "))
                {
                    this.SelectOperator.SetValue("@whereAPPLY_DTE", this.whereAPPLY_DTE, SqlDbType.VarChar);
                }
                if (sbstrSQL.ToString().Contains("@whereFIRST_DTE "))
                {
                    this.SelectOperator.SetValue("@whereFIRST_DTE", this.whereFIRST_DTE, SqlDbType.VarChar);
                }
                if (sbstrSQL.ToString().Contains("@wherePAY_DTE "))
                {
                    this.SelectOperator.SetValue("@wherePAY_DTE", this.wherePAY_DTE, SqlDbType.VarChar);
                }
                if (sbstrSQL.ToString().Contains("@whereVAILD_FLAG "))
                {
                    this.SelectOperator.SetValue("@whereVAILD_FLAG", this.whereVAILD_FLAG, SqlDbType.VarChar);
                }
                if (sbstrSQL.ToString().Contains("@whereSEND_MSG_FLAG "))
                {
                    this.SelectOperator.SetValue("@whereSEND_MSG_FLAG", this.whereSEND_MSG_FLAG, SqlDbType.VarChar);
                }
                if (sbstrSQL.ToString().Contains("@whereREPLY_FLAG "))
                {
                    this.SelectOperator.SetValue("@whereREPLY_FLAG", this.whereREPLY_FLAG, SqlDbType.VarChar);
                }
                if (sbstrSQL.ToString().Contains("@whereREPLY_DTE "))
                {
                    this.SelectOperator.SetValue("@whereREPLY_DTE", this.whereREPLY_DTE, SqlDbType.VarChar);
                }
                if (sbstrSQL.ToString().Contains("@whereSTOP_DTE "))
                {
                    this.SelectOperator.SetValue("@whereSTOP_DTE", this.whereSTOP_DTE, SqlDbType.DateTime);
                }
                if (sbstrSQL.ToString().Contains("@whereSTOP_USER "))
                {
                    this.SelectOperator.SetValue("@whereSTOP_USER", this.whereSTOP_USER, SqlDbType.VarChar);
                }
                if (sbstrSQL.ToString().Contains("@whereCREATE_DT "))
                {
                    this.SelectOperator.SetValue("@whereCREATE_DT", this.whereCREATE_DT, SqlDbType.DateTime);
                }
                if (sbstrSQL.ToString().Contains("@whereCREATE_USER "))
                {
                    this.SelectOperator.SetValue("@whereCREATE_USER", this.whereCREATE_USER, SqlDbType.VarChar);
                }
                if (sbstrSQL.ToString().Contains("@whereERROR_REASON "))
                {
                    this.SelectOperator.SetValue("@whereERROR_REASON", this.whereERROR_REASON, SqlDbType.NVarChar);
                }
                if (sbstrSQL.ToString().Contains("@whereERROR_REASON_DT "))
                {
                    this.SelectOperator.SetValue("@whereERROR_REASON_DT", this.whereERROR_REASON_DT, SqlDbType.VarChar);
                }
                if (sbstrSQL.ToString().Contains("@wherePUBLIC_APPLY_CHAR_1 "))
                {
                    this.SelectOperator.SetValue("@wherePUBLIC_APPLY_CHAR_1", this.wherePUBLIC_APPLY_CHAR_1, SqlDbType.VarChar);
                }
                if (sbstrSQL.ToString().Contains("@wherePUBLIC_APPLY_CHAR_2 "))
                {
                    this.SelectOperator.SetValue("@wherePUBLIC_APPLY_CHAR_2", this.wherePUBLIC_APPLY_CHAR_2, SqlDbType.VarChar);
                }
                if (sbstrSQL.ToString().Contains("@wherePUBLIC_APPLY_CHAR_3 "))
                {
                    this.SelectOperator.SetValue("@wherePUBLIC_APPLY_CHAR_3", this.wherePUBLIC_APPLY_CHAR_3, SqlDbType.VarChar);
                }
                if (sbstrSQL.ToString().Contains("@wherePUBLIC_APPLY_CHAR_4 "))
                {
                    this.SelectOperator.SetValue("@wherePUBLIC_APPLY_CHAR_4", this.wherePUBLIC_APPLY_CHAR_4, SqlDbType.VarChar);
                }
                if (sbstrSQL.ToString().Contains("@wherePUBLIC_APPLY_CHAR_5 "))
                {
                    this.SelectOperator.SetValue("@wherePUBLIC_APPLY_CHAR_5", this.wherePUBLIC_APPLY_CHAR_5, SqlDbType.VarChar);
                }
                if (sbstrSQL.ToString().Contains("@wherePUBLIC_APPLY_AMT_1 "))
                {
                    this.SelectOperator.SetValue("@wherePUBLIC_APPLY_AMT_1", this.wherePUBLIC_APPLY_AMT_1, SqlDbType.Decimal);
                }
                if (sbstrSQL.ToString().Contains("@wherePUBLIC_APPLY_AMT_2 "))
                {
                    this.SelectOperator.SetValue("@wherePUBLIC_APPLY_AMT_2", this.wherePUBLIC_APPLY_AMT_2, SqlDbType.Decimal);
                }
                if (sbstrSQL.ToString().Contains("@wherePUBLIC_APPLY_AMT_3 "))
                {
                    this.SelectOperator.SetValue("@wherePUBLIC_APPLY_AMT_3", this.wherePUBLIC_APPLY_AMT_3, SqlDbType.Decimal);
                }
                if (sbstrSQL.ToString().Contains("@wherePUBLIC_APPLY_AMT_4 "))
                {
                    this.SelectOperator.SetValue("@wherePUBLIC_APPLY_AMT_4", this.wherePUBLIC_APPLY_AMT_4, SqlDbType.Decimal);
                }
                if (sbstrSQL.ToString().Contains("@wherePUBLIC_APPLY_AMT_5 "))
                {
                    this.SelectOperator.SetValue("@wherePUBLIC_APPLY_AMT_5", this.wherePUBLIC_APPLY_AMT_5, SqlDbType.Decimal);
                }
                if (sbstrSQL.ToString().Contains("@wherePUBLIC_APPLY_DT_1 "))
                {
                    this.SelectOperator.SetValue("@wherePUBLIC_APPLY_DT_1", this.wherePUBLIC_APPLY_DT_1, SqlDbType.DateTime);
                }
                if (sbstrSQL.ToString().Contains("@wherePUBLIC_APPLY_DT_2 "))
                {
                    this.SelectOperator.SetValue("@wherePUBLIC_APPLY_DT_2", this.wherePUBLIC_APPLY_DT_2, SqlDbType.DateTime);
                }
                if (sbstrSQL.ToString().Contains("@wherePUBLIC_APPLY_DT_3 "))
                {
                    this.SelectOperator.SetValue("@wherePUBLIC_APPLY_DT_3", this.wherePUBLIC_APPLY_DT_3, SqlDbType.DateTime);
                }
                if (sbstrSQL.ToString().Contains("@wherePUBLIC_APPLY_DT_4 "))
                {
                    this.SelectOperator.SetValue("@wherePUBLIC_APPLY_DT_4", this.wherePUBLIC_APPLY_DT_4, SqlDbType.DateTime);
                }
                if (sbstrSQL.ToString().Contains("@wherePUBLIC_APPLY_DT_5 "))
                {
                    this.SelectOperator.SetValue("@wherePUBLIC_APPLY_DT_5", this.wherePUBLIC_APPLY_DT_5, SqlDbType.DateTime);
                }
                if (sbstrSQL.ToString().Contains("@whereMNT_DT "))
                {
                    this.SelectOperator.SetValue("@whereMNT_DT", this.whereMNT_DT, SqlDbType.DateTime);
                }
                if (sbstrSQL.ToString().Contains("@whereMNT_USER "))
                {
                    this.SelectOperator.SetValue("@whereMNT_USER", this.whereMNT_USER, SqlDbType.VarChar);
                }
                #endregion
                //myTable set to DataTable object
                myTable = this.SelectOperator.GetDataTable("PUBLIC_APPLY");
                if (myTable.Rows.Count == 0)
                {
                    msg_code = "F0023"; //not found
                }
                else
                {
                    msg_code = "S0000"; //query success
                }
            }
            catch (SqlException e)
            {
                MSG.strMsg = Convert.ToString(e.Number) + '-' + e.Message;
                msg_code = MSG.getMsg();
            }
            finally
            {

            }
            return msg_code;
        }
        #endregion


        #region Property(user define)
        private String whereAPPLY_DTE_ST = null;
        public String strWhereAPPLY_DTE_ST
        {
            get { return whereAPPLY_DTE_ST; }
            set { whereAPPLY_DTE_ST = value; }
        }
        private String whereAPPLY_DTE_ED = null;
        public String strWhereAPPLY_DTE_ED
        {
            get { return whereAPPLY_DTE_ED; }
            set { whereAPPLY_DTE_ED = value; }
        }
        private String whereEXPIR_DTE_ST = null;
        public String strWhereEXPIR_DTE_ST
        {
            get { return whereEXPIR_DTE_ST; }
            set { whereEXPIR_DTE_ST = value; }
        }
        private String whereEXPIR_DTE_ED = null;
        public String strWhereEXPIR_DTE_ED
        {
            get { return whereEXPIR_DTE_ED; }
            set { whereEXPIR_DTE_ED = value; }
        }
        #endregion
        #region update_ACH_APPLY_RESULT()
        public string update_ACH_APPLY_RESULT()
        {
            #region Modify History
            /// <history>
            /// <design>
            /// <name>Cybersoft StevenLee</name>
            /// <date>2011/1/11 上午 15:50:00</date>
            #endregion
            try
            {
                #region set SQL statement
                StringBuilder sbstrSQL = new StringBuilder();
                sbstrSQL.Append(" UPDATE PUBLIC_APPLY ");
                sbstrSQL.Append("     SET ERROR_REASON = @ERROR_REASON, ERROR_REASON_DT = @ERROR_REASON_DT ");
                sbstrSQL.Append("  WHERE PAY_TYPE = @wherePAY_TYPE AND PAY_NBR like @wherePAY_NBR ");
                sbstrSQL.Append("    AND PAY_ACCT_NBR = @wherePAY_ACCT_NBR AND REPLY_FLAG = @whereREPLY_FLAG ");
                #endregion
                this.UpdateOperator.SqlText = sbstrSQL.ToString();
                this.UpdateOperator.SetValue("@ERROR_REASON", this.ERROR_REASON);
                this.UpdateOperator.SetValue("@ERROR_REASON_DT", this.ERROR_REASON_DT);
                this.UpdateOperator.SetValue("@wherePAY_TYPE", this.wherePAY_TYPE);
                this.UpdateOperator.SetValue("@wherePAY_NBR", this.wherePAY_NBR);
                this.UpdateOperator.SetValue("@wherePAY_ACCT_NBR", this.wherePAY_ACCT_NBR);
                this.UpdateOperator.SetValue("@whereREPLY_FLAG", this.whereREPLY_FLAG);

                UptCnt = this.UpdateOperator.Execute();
                msg_code = "S0000"; //修改成功代碼
            }
            catch (SqlException e)
            {
                MSG.strMsg = Convert.ToString(e.Number) + '-' + e.Message;
                msg_code = MSG.getMsg();
            }
            return msg_code;
        }
        #endregion
        #region query_for_app()
        public string query_for_app()
        {
            #region Modify History
            /// <history>
            /// <design>
            /// <name>Cybersoft.COCA DaoGenerator</name>
            /// <date>2012/4/9 上午 12:26:10</date>
            #endregion
            try
            {
                #region set SQL statement
                StringBuilder sbstrSQL = new StringBuilder();
                sbstrSQL.Append("SELECT B.NAME,C.CUST_ID,A.*  ");
                sbstrSQL.Append("FROM PUBLIC_APPLY A ");
                sbstrSQL.Append("LEFT JOIN CUST_INF B ON A.BU=B.BU AND A.CUST_SEQ=B.CUST_NBR ");
                sbstrSQL.Append("LEFT JOIN ACCT_LINK C ON A.BU=C.BU AND A.ACCT_NBR=C.ACCT_NBR AND A.CUST_SEQ=C.CUST_SEQ ");
                sbstrSQL.Append("WHERE 1=1 ");
                if (this.whereBU != null)
                {
                    sbstrSQL.Append(" and A.BU = @whereBU ");
                }
                if (this.whereACCT_NBR != null)
                {
                    sbstrSQL.Append(" and A.ACCT_NBR = @whereACCT_NBR ");
                }
                if (this.wherePAY_CARD_NBR != null)
                {
                    sbstrSQL.Append(" and A.PAY_CARD_NBR = @wherePAY_CARD_NBR ");
                }
                if (this.wherePAY_TYPE != null)
                {
                    sbstrSQL.Append(" and A.PAY_TYPE = @wherePAY_TYPE ");
                }
                if (this.whereVAILD_FLAG != null)
                {
                    if (this.whereVAILD_FLAG == "0")  //web-扣繳申請，指定查詢待生效
                    {
                        sbstrSQL.Append(" and A.VAILD_FLAG = '' ");
                    }
                    else
                    {
                        sbstrSQL.Append(" and A.VAILD_FLAG = @whereVAILD_FLAG ");
                    }
                }
                //if (this.whereAPPLY_DTE != null)
                //{
                //    sbstrSQL.Append(" and A.APPLY_DTE >= @whereAPPLY_DTE ");
                //}
                //if (this.whereEXPIR_DTE != null)
                //{
                //    sbstrSQL.Append(" and A.EXPIR_DTE <= @whereEXPIR_DTE ");
                //}
                if (this.whereAPPLY_DTE_ST != null)
                {
                    sbstrSQL.Append(" and A.APPLY_DTE >= @whereAPPLY_DTE_ST ");
                }
                if (this.whereAPPLY_DTE_ED != null)
                {
                    sbstrSQL.Append(" and A.APPLY_DTE <= @whereAPPLY_DTE_ED ");
                }
                if (this.whereEXPIR_DTE_ST != null)
                {
                    sbstrSQL.Append(" and A.EXPIR_DTE >= @whereEXPIR_DTE_ST ");
                }
                if (this.whereEXPIR_DTE_ED != null)
                {
                    sbstrSQL.Append(" and A.EXPIR_DTE <= @whereEXPIR_DTE_ED ");
                }
                if (this.strWherePAY_NBR != null)
                {
                    sbstrSQL.Append(" and A.PAY_NBR LIKE '%" + this.strWherePAY_NBR + "%' ");
                }
                sbstrSQL.Append(" ORDER BY A.PAY_TYPE, A.PAY_NBR ");
                #endregion
                this.SelectOperator.SqlText = sbstrSQL.ToString();
                #region replace SQL parameter
                if (sbstrSQL.ToString().Contains("@whereBU "))
                {
                    this.SelectOperator.SetValue("@whereBU", this.whereBU);
                }
                if (sbstrSQL.ToString().Contains("@whereACCT_NBR "))
                {
                    this.SelectOperator.SetValue("@whereACCT_NBR", this.whereACCT_NBR);
                }
                if (sbstrSQL.ToString().Contains("@wherePAY_CARD_NBR "))
                {
                    this.SelectOperator.SetValue("@wherePAY_CARD_NBR", this.wherePAY_CARD_NBR);
                }
                if (sbstrSQL.ToString().Contains("@wherePAY_TYPE "))
                {
                    this.SelectOperator.SetValue("@wherePAY_TYPE", this.wherePAY_TYPE);
                }
                if (sbstrSQL.ToString().Contains("@whereAPPLY_DTE "))
                {
                    this.SelectOperator.SetValue("@whereAPPLY_DTE", this.whereAPPLY_DTE);
                }
                if (sbstrSQL.ToString().Contains("@whereEXPIR_DTE "))
                {
                    this.SelectOperator.SetValue("@whereEXPIR_DTE", this.whereEXPIR_DTE);
                }
                if (sbstrSQL.ToString().Contains("@whereAPPLY_DTE_ST "))
                {
                    this.SelectOperator.SetValue("@whereAPPLY_DTE_ST", this.whereAPPLY_DTE_ST);
                }
                if (sbstrSQL.ToString().Contains("@whereAPPLY_DTE_ED "))
                {
                    this.SelectOperator.SetValue("@whereAPPLY_DTE_ED", this.whereAPPLY_DTE_ED);
                }
                if (sbstrSQL.ToString().Contains("@whereEXPIR_DTE_ST "))
                {
                    this.SelectOperator.SetValue("@whereEXPIR_DTE_ST", this.whereEXPIR_DTE_ST);
                }
                if (sbstrSQL.ToString().Contains("@whereEXPIR_DTE_ED "))
                {
                    this.SelectOperator.SetValue("@whereEXPIR_DTE_ED", this.whereEXPIR_DTE_ED);
                }
                if (sbstrSQL.ToString().Contains("@wherePAY_NBR "))
                {
                    this.SelectOperator.SetValue("@wherePAY_NBR", this.wherePAY_NBR);
                }
                if (sbstrSQL.ToString().Contains("@whereVAILD_FLAG "))
                {
                    this.SelectOperator.SetValue("@whereVAILD_FLAG", this.whereVAILD_FLAG);
                }
                #endregion
                //myTable set to DataTable object
                myTable = this.SelectOperator.GetDataTable("PUBLIC_APPLY");
                if (myTable.Rows.Count == 0)
                {
                    msg_code = "F0023"; //not found
                }
                else
                {
                    msg_code = "S0000"; //query success
                }
            }
            catch (SqlException e)
            {
                MSG.strMsg = Convert.ToString(e.Number) + '-' + e.Message;
                msg_code = MSG.getMsg();
            }
            return msg_code;
        }
        #endregion
        #region update_by_Telegram(電文異動)
        public string update_by_Telegram(DataTable DataTable_BEFORE, string strMNT_PGM)
        {
            #region Modify History
            /// <history>
            /// <design>
            /// <name>anna</name>
            /// <date>2012/4/12 下午 02:15:00</date>
            #endregion
            try
            {
                #region 宣告MNT_TODAYDAO並放入初始值
                Cybersoft.Dao.Core.MNT_TODAYDao MNT_TODAY = new Cybersoft.Dao.Core.MNT_TODAYDao();
                MNT_TODAY.table_define();
                MNT_TODAY.strinitTBL_NAME = DataTable_BEFORE.TableName;
                MNT_TODAY.strinitPOST_RESULT = "00";
                MNT_TODAY.strinitMNT_PGM = strMNT_PGM;
                MNT_TODAY.strinitMNT_USER = this.MNT_USER;
                MNT_TODAY.DateTimeinitMNT_DT = DateTime.Now;
                MNT_TODAY.DateTimeinitEFF_DTE = DateTime.Now;
                MNT_TODAY.DateTimeinitPOSTING_DTE = DateTime.Now;

                #endregion

                #region 取得鍵值
                MNT_TODAY.strinitBU = DataTable_BEFORE.Rows[0]["BU"].ToString().Trim();
                MNT_TODAY.strinitACCT_NBR = DataTable_BEFORE.Rows[0]["ACCT_NBR"].ToString().Trim();
                MNT_TODAY.strinitPRODUCT = "";
                MNT_TODAY.strinitCURRENCY = "";
                MNT_TODAY.strinitCARD_NBR = DataTable_BEFORE.Rows[0]["PAY_CARD_NBR"].ToString().Trim();
                MNT_TODAY.strinitOTHER_KEY_1 = "";
                MNT_TODAY.strinitOTHER_KEY_2 = "";

                #endregion
                #region 寫入log
                int MNT_Count = 0;
                //修改欄位部分
                if (this.PAY_CARD_NBR != null && this.PAY_CARD_NBR != Convert.ToString(DataTable_BEFORE.Rows[0]["PAY_CARD_NBR"]))
                {
                    MNT_TODAY.initInsert_row();
                    MNT_TODAY.resultTable.Rows[MNT_Count]["FIELD_NAME"] = "PAY_CARD_NBR";
                    MNT_TODAY.resultTable.Rows[MNT_Count]["VARCHAR_BEFORE"] = Convert.ToString(DataTable_BEFORE.Rows[0]["PAY_CARD_NBR"]);
                    MNT_TODAY.resultTable.Rows[MNT_Count]["VARCHAR_AFTER"] = this.PAY_CARD_NBR;
                    MNT_Count++;
                }
                if (this.PAY_TYPE != null && this.PAY_TYPE != Convert.ToString(DataTable_BEFORE.Rows[0]["PAY_TYPE"]))
                {
                    MNT_TODAY.initInsert_row();
                    MNT_TODAY.resultTable.Rows[MNT_Count]["FIELD_NAME"] = "PAY_TYPE";
                    MNT_TODAY.resultTable.Rows[MNT_Count]["VARCHAR_BEFORE"] = Convert.ToString(DataTable_BEFORE.Rows[0]["PAY_TYPE"]);
                    MNT_TODAY.resultTable.Rows[MNT_Count]["VARCHAR_AFTER"] = this.PAY_TYPE;
                    MNT_Count++;
                }
                if (this.PAY_NBR != null && this.PAY_NBR != Convert.ToString(DataTable_BEFORE.Rows[0]["PAY_NBR"]))
                {
                    MNT_TODAY.initInsert_row();
                    MNT_TODAY.resultTable.Rows[MNT_Count]["FIELD_NAME"] = "PAY_NBR";
                    MNT_TODAY.resultTable.Rows[MNT_Count]["VARCHAR_BEFORE"] = Convert.ToString(DataTable_BEFORE.Rows[0]["PAY_NBR"]);
                    MNT_TODAY.resultTable.Rows[MNT_Count]["VARCHAR_AFTER"] = this.PAY_NBR;
                    MNT_Count++;
                }
                if (this.SEQ != null && this.SEQ != Convert.ToString(DataTable_BEFORE.Rows[0]["SEQ"]))
                {
                    MNT_TODAY.initInsert_row();
                    MNT_TODAY.resultTable.Rows[MNT_Count]["FIELD_NAME"] = "SEQ";
                    MNT_TODAY.resultTable.Rows[MNT_Count]["VARCHAR_BEFORE"] = Convert.ToString(DataTable_BEFORE.Rows[0]["SEQ"]);
                    MNT_TODAY.resultTable.Rows[MNT_Count]["VARCHAR_AFTER"] = this.SEQ;
                    MNT_Count++;
                }
                if (this.BU != null && this.BU != Convert.ToString(DataTable_BEFORE.Rows[0]["BU"]))
                {
                    MNT_TODAY.initInsert_row();
                    MNT_TODAY.resultTable.Rows[MNT_Count]["FIELD_NAME"] = "BU";
                    MNT_TODAY.resultTable.Rows[MNT_Count]["VARCHAR_BEFORE"] = Convert.ToString(DataTable_BEFORE.Rows[0]["BU"]);
                    MNT_TODAY.resultTable.Rows[MNT_Count]["VARCHAR_AFTER"] = this.BU;
                    MNT_Count++;
                }
                if (this.PRODUCT != null && this.PRODUCT != Convert.ToString(DataTable_BEFORE.Rows[0]["PRODUCT"]))
                {
                    MNT_TODAY.initInsert_row();
                    MNT_TODAY.resultTable.Rows[MNT_Count]["FIELD_NAME"] = "PRODUCT";
                    MNT_TODAY.resultTable.Rows[MNT_Count]["VARCHAR_BEFORE"] = Convert.ToString(DataTable_BEFORE.Rows[0]["PRODUCT"]);
                    MNT_TODAY.resultTable.Rows[MNT_Count]["VARCHAR_AFTER"] = this.PRODUCT;
                    MNT_Count++;
                }
                if (this.CARD_PRODUCT != null && this.CARD_PRODUCT != Convert.ToString(DataTable_BEFORE.Rows[0]["CARD_PRODUCT"]))
                {
                    MNT_TODAY.initInsert_row();
                    MNT_TODAY.resultTable.Rows[MNT_Count]["FIELD_NAME"] = "CARD_PRODUCT";
                    MNT_TODAY.resultTable.Rows[MNT_Count]["VARCHAR_BEFORE"] = Convert.ToString(DataTable_BEFORE.Rows[0]["CARD_PRODUCT"]);
                    MNT_TODAY.resultTable.Rows[MNT_Count]["VARCHAR_AFTER"] = this.CARD_PRODUCT;
                    MNT_Count++;
                }
                if (this.ACCT_NBR != null && this.ACCT_NBR != Convert.ToString(DataTable_BEFORE.Rows[0]["ACCT_NBR"]))
                {
                    MNT_TODAY.initInsert_row();
                    MNT_TODAY.resultTable.Rows[MNT_Count]["FIELD_NAME"] = "ACCT_NBR";
                    MNT_TODAY.resultTable.Rows[MNT_Count]["VARCHAR_BEFORE"] = Convert.ToString(DataTable_BEFORE.Rows[0]["ACCT_NBR"]);
                    MNT_TODAY.resultTable.Rows[MNT_Count]["VARCHAR_AFTER"] = this.ACCT_NBR;
                    MNT_Count++;
                }
                if (this.PAY_ACCT_NBR != null && this.PAY_ACCT_NBR != Convert.ToString(DataTable_BEFORE.Rows[0]["PAY_ACCT_NBR"]))
                {
                    MNT_TODAY.initInsert_row();
                    MNT_TODAY.resultTable.Rows[MNT_Count]["FIELD_NAME"] = "PAY_ACCT_NBR";
                    MNT_TODAY.resultTable.Rows[MNT_Count]["VARCHAR_BEFORE"] = Convert.ToString(DataTable_BEFORE.Rows[0]["PAY_ACCT_NBR"]);
                    MNT_TODAY.resultTable.Rows[MNT_Count]["VARCHAR_AFTER"] = this.PAY_ACCT_NBR;
                    MNT_Count++;
                }
                if (this.PAY_CARD_NBR_PREV != null && this.PAY_CARD_NBR_PREV != Convert.ToString(DataTable_BEFORE.Rows[0]["PAY_CARD_NBR_PREV"]))
                {
                    MNT_TODAY.initInsert_row();
                    MNT_TODAY.resultTable.Rows[MNT_Count]["FIELD_NAME"] = "PAY_CARD_NBR_PREV";
                    MNT_TODAY.resultTable.Rows[MNT_Count]["VARCHAR_BEFORE"] = Convert.ToString(DataTable_BEFORE.Rows[0]["PAY_CARD_NBR_PREV"]);
                    MNT_TODAY.resultTable.Rows[MNT_Count]["VARCHAR_AFTER"] = this.PAY_CARD_NBR_PREV;
                    MNT_Count++;
                }
                if (this.PAY_ACCT_NBR_PREV != null && this.PAY_ACCT_NBR_PREV != Convert.ToString(DataTable_BEFORE.Rows[0]["PAY_ACCT_NBR_PREV"]))
                {
                    MNT_TODAY.initInsert_row();
                    MNT_TODAY.resultTable.Rows[MNT_Count]["FIELD_NAME"] = "PAY_ACCT_NBR_PREV";
                    MNT_TODAY.resultTable.Rows[MNT_Count]["VARCHAR_BEFORE"] = Convert.ToString(DataTable_BEFORE.Rows[0]["PAY_ACCT_NBR_PREV"]);
                    MNT_TODAY.resultTable.Rows[MNT_Count]["VARCHAR_AFTER"] = this.PAY_ACCT_NBR_PREV;
                    MNT_Count++;
                }
                if (this.CUST_SEQ != null && this.CUST_SEQ != Convert.ToString(DataTable_BEFORE.Rows[0]["CUST_SEQ"]))
                {
                    MNT_TODAY.initInsert_row();
                    MNT_TODAY.resultTable.Rows[MNT_Count]["FIELD_NAME"] = "CUST_SEQ";
                    MNT_TODAY.resultTable.Rows[MNT_Count]["VARCHAR_BEFORE"] = Convert.ToString(DataTable_BEFORE.Rows[0]["CUST_SEQ"]);
                    MNT_TODAY.resultTable.Rows[MNT_Count]["VARCHAR_AFTER"] = this.CUST_SEQ;
                    MNT_Count++;
                }
                if (this.EXPIR_DTE != null && this.EXPIR_DTE != Convert.ToString(DataTable_BEFORE.Rows[0]["EXPIR_DTE"]))
                {
                    MNT_TODAY.initInsert_row();
                    MNT_TODAY.resultTable.Rows[MNT_Count]["FIELD_NAME"] = "EXPIR_DTE";
                    MNT_TODAY.resultTable.Rows[MNT_Count]["VARCHAR_BEFORE"] = Convert.ToString(DataTable_BEFORE.Rows[0]["EXPIR_DTE"]);
                    MNT_TODAY.resultTable.Rows[MNT_Count]["VARCHAR_AFTER"] = this.EXPIR_DTE;
                    MNT_Count++;
                }
                if (this.APPLY_DTE != null && this.APPLY_DTE != Convert.ToString(DataTable_BEFORE.Rows[0]["APPLY_DTE"]))
                {
                    MNT_TODAY.initInsert_row();
                    MNT_TODAY.resultTable.Rows[MNT_Count]["FIELD_NAME"] = "APPLY_DTE";
                    MNT_TODAY.resultTable.Rows[MNT_Count]["VARCHAR_BEFORE"] = Convert.ToString(DataTable_BEFORE.Rows[0]["APPLY_DTE"]);
                    MNT_TODAY.resultTable.Rows[MNT_Count]["VARCHAR_AFTER"] = this.APPLY_DTE;
                    MNT_Count++;
                }
                if (this.FIRST_DTE != null && this.FIRST_DTE != Convert.ToString(DataTable_BEFORE.Rows[0]["FIRST_DTE"]))
                {
                    MNT_TODAY.initInsert_row();
                    MNT_TODAY.resultTable.Rows[MNT_Count]["FIELD_NAME"] = "FIRST_DTE";
                    MNT_TODAY.resultTable.Rows[MNT_Count]["VARCHAR_BEFORE"] = Convert.ToString(DataTable_BEFORE.Rows[0]["FIRST_DTE"]);
                    MNT_TODAY.resultTable.Rows[MNT_Count]["VARCHAR_AFTER"] = this.FIRST_DTE;
                    MNT_Count++;
                }
                if (this.PAY_DTE != null && this.PAY_DTE != Convert.ToString(DataTable_BEFORE.Rows[0]["PAY_DTE"]))
                {
                    MNT_TODAY.initInsert_row();
                    MNT_TODAY.resultTable.Rows[MNT_Count]["FIELD_NAME"] = "PAY_DTE";
                    MNT_TODAY.resultTable.Rows[MNT_Count]["VARCHAR_BEFORE"] = Convert.ToString(DataTable_BEFORE.Rows[0]["PAY_DTE"]);
                    MNT_TODAY.resultTable.Rows[MNT_Count]["VARCHAR_AFTER"] = this.PAY_DTE;
                    MNT_Count++;
                }
                if (this.VAILD_FLAG != null && this.VAILD_FLAG != Convert.ToString(DataTable_BEFORE.Rows[0]["VAILD_FLAG"]))
                {
                    MNT_TODAY.initInsert_row();
                    MNT_TODAY.resultTable.Rows[MNT_Count]["FIELD_NAME"] = "VAILD_FLAG";
                    MNT_TODAY.resultTable.Rows[MNT_Count]["VARCHAR_BEFORE"] = Convert.ToString(DataTable_BEFORE.Rows[0]["VAILD_FLAG"]);
                    MNT_TODAY.resultTable.Rows[MNT_Count]["VARCHAR_AFTER"] = this.VAILD_FLAG;
                    MNT_Count++;
                }
                if (this.SEND_MSG_FLAG != null && this.SEND_MSG_FLAG != Convert.ToString(DataTable_BEFORE.Rows[0]["SEND_MSG_FLAG"]))
                {
                    MNT_TODAY.initInsert_row();
                    MNT_TODAY.resultTable.Rows[MNT_Count]["FIELD_NAME"] = "SEND_MSG_FLAG";
                    MNT_TODAY.resultTable.Rows[MNT_Count]["VARCHAR_BEFORE"] = Convert.ToString(DataTable_BEFORE.Rows[0]["SEND_MSG_FLAG"]);
                    MNT_TODAY.resultTable.Rows[MNT_Count]["VARCHAR_AFTER"] = this.SEND_MSG_FLAG;
                    MNT_Count++;
                }
                if (this.REPLY_FLAG != null && this.REPLY_FLAG != Convert.ToString(DataTable_BEFORE.Rows[0]["REPLY_FLAG"]))
                {
                    MNT_TODAY.initInsert_row();
                    MNT_TODAY.resultTable.Rows[MNT_Count]["FIELD_NAME"] = "REPLY_FLAG";
                    MNT_TODAY.resultTable.Rows[MNT_Count]["VARCHAR_BEFORE"] = Convert.ToString(DataTable_BEFORE.Rows[0]["REPLY_FLAG"]);
                    MNT_TODAY.resultTable.Rows[MNT_Count]["VARCHAR_AFTER"] = this.REPLY_FLAG;
                    MNT_Count++;
                }
                if (this.REPLY_DTE != null && this.REPLY_DTE != Convert.ToString(DataTable_BEFORE.Rows[0]["REPLY_DTE"]))
                {
                    MNT_TODAY.initInsert_row();
                    MNT_TODAY.resultTable.Rows[MNT_Count]["FIELD_NAME"] = "REPLY_DTE";
                    MNT_TODAY.resultTable.Rows[MNT_Count]["VARCHAR_BEFORE"] = Convert.ToString(DataTable_BEFORE.Rows[0]["REPLY_DTE"]);
                    MNT_TODAY.resultTable.Rows[MNT_Count]["VARCHAR_AFTER"] = this.REPLY_DTE;
                    MNT_Count++;
                }
                if (this.STOP_DTE > dateStart && this.STOP_DTE != Convert.ToDateTime(DataTable_BEFORE.Rows[0]["STOP_DTE"]))
                {
                    if (this.STOP_DTE.ToString("yyyyMMdd") == "19000101" &&
                        Convert.ToDateTime(DataTable_BEFORE.Rows[0]["STOP_DTE"]) == dateStart)
                    {
                    }
                    else
                    {
                        MNT_TODAY.initInsert_row();
                        MNT_TODAY.resultTable.Rows[MNT_Count]["FIELD_NAME"] = "STOP_DTE";
                        MNT_TODAY.resultTable.Rows[MNT_Count]["DT_BEFORE"] = Convert.ToDateTime(DataTable_BEFORE.Rows[0]["STOP_DTE"]);
                        if (this.STOP_DTE.ToString("yyyyMMdd") == "19000101")
                        {
                            MNT_TODAY.resultTable.Rows[MNT_Count]["DT_AFTER"] = "1900-01-01";
                        }
                        else
                        {
                            MNT_TODAY.resultTable.Rows[MNT_Count]["DT_AFTER"] = this.STOP_DTE;
                        }
                        MNT_Count++;
                    }
                }
                if (this.STOP_USER != null && this.STOP_USER != Convert.ToString(DataTable_BEFORE.Rows[0]["STOP_USER"]))
                {
                    MNT_TODAY.initInsert_row();
                    MNT_TODAY.resultTable.Rows[MNT_Count]["FIELD_NAME"] = "STOP_USER";
                    MNT_TODAY.resultTable.Rows[MNT_Count]["VARCHAR_BEFORE"] = Convert.ToString(DataTable_BEFORE.Rows[0]["STOP_USER"]);
                    MNT_TODAY.resultTable.Rows[MNT_Count]["VARCHAR_AFTER"] = this.STOP_USER;
                    MNT_Count++;
                }
                if (this.CREATE_DT > dateStart && this.CREATE_DT != Convert.ToDateTime(DataTable_BEFORE.Rows[0]["CREATE_DT"]))
                {
                    if (this.CREATE_DT.ToString("yyyyMMdd") == "19000101" &&
                        Convert.ToDateTime(DataTable_BEFORE.Rows[0]["CREATE_DT"]) == dateStart)
                    {
                    }
                    else
                    {
                        MNT_TODAY.initInsert_row();
                        MNT_TODAY.resultTable.Rows[MNT_Count]["FIELD_NAME"] = "CREATE_DT";
                        MNT_TODAY.resultTable.Rows[MNT_Count]["DT_BEFORE"] = Convert.ToDateTime(DataTable_BEFORE.Rows[0]["CREATE_DT"]);
                        if (this.CREATE_DT.ToString("yyyyMMdd") == "19000101")
                        {
                            MNT_TODAY.resultTable.Rows[MNT_Count]["DT_AFTER"] = "1900-01-01";
                        }
                        else
                        {
                            MNT_TODAY.resultTable.Rows[MNT_Count]["DT_AFTER"] = this.CREATE_DT;
                        }
                        MNT_Count++;
                    }
                }
                if (this.CREATE_USER != null && this.CREATE_USER != Convert.ToString(DataTable_BEFORE.Rows[0]["CREATE_USER"]))
                {
                    MNT_TODAY.initInsert_row();
                    MNT_TODAY.resultTable.Rows[MNT_Count]["FIELD_NAME"] = "CREATE_USER";
                    MNT_TODAY.resultTable.Rows[MNT_Count]["VARCHAR_BEFORE"] = Convert.ToString(DataTable_BEFORE.Rows[0]["CREATE_USER"]);
                    MNT_TODAY.resultTable.Rows[MNT_Count]["VARCHAR_AFTER"] = this.CREATE_USER;
                    MNT_Count++;
                }
                if (this.ERROR_REASON != null && this.ERROR_REASON != Convert.ToString(DataTable_BEFORE.Rows[0]["ERROR_REASON"]))
                {
                    MNT_TODAY.initInsert_row();
                    MNT_TODAY.resultTable.Rows[MNT_Count]["FIELD_NAME"] = "ERROR_REASON";
                    MNT_TODAY.resultTable.Rows[MNT_Count]["VARCHAR_BEFORE"] = Convert.ToString(DataTable_BEFORE.Rows[0]["ERROR_REASON"]);
                    MNT_TODAY.resultTable.Rows[MNT_Count]["VARCHAR_AFTER"] = this.ERROR_REASON;
                    MNT_Count++;
                }
                if (this.ERROR_REASON_DT != null && this.ERROR_REASON_DT != Convert.ToString(DataTable_BEFORE.Rows[0]["ERROR_REASON_DT"]))
                {
                    MNT_TODAY.initInsert_row();
                    MNT_TODAY.resultTable.Rows[MNT_Count]["FIELD_NAME"] = "ERROR_REASON_DT";
                    MNT_TODAY.resultTable.Rows[MNT_Count]["VARCHAR_BEFORE"] = Convert.ToString(DataTable_BEFORE.Rows[0]["ERROR_REASON_DT"]);
                    MNT_TODAY.resultTable.Rows[MNT_Count]["VARCHAR_AFTER"] = this.ERROR_REASON_DT;
                    MNT_Count++;
                }
                if (this.PUBLIC_APPLY_CHAR_1 != null && this.PUBLIC_APPLY_CHAR_1 != Convert.ToString(DataTable_BEFORE.Rows[0]["PUBLIC_APPLY_CHAR_1"]))
                {
                    MNT_TODAY.initInsert_row();
                    MNT_TODAY.resultTable.Rows[MNT_Count]["FIELD_NAME"] = "PUBLIC_APPLY_CHAR_1";
                    MNT_TODAY.resultTable.Rows[MNT_Count]["VARCHAR_BEFORE"] = Convert.ToString(DataTable_BEFORE.Rows[0]["PUBLIC_APPLY_CHAR_1"]);
                    MNT_TODAY.resultTable.Rows[MNT_Count]["VARCHAR_AFTER"] = this.PUBLIC_APPLY_CHAR_1;
                    MNT_Count++;
                }
                if (this.PUBLIC_APPLY_CHAR_2 != null && this.PUBLIC_APPLY_CHAR_2 != Convert.ToString(DataTable_BEFORE.Rows[0]["PUBLIC_APPLY_CHAR_2"]))
                {
                    MNT_TODAY.initInsert_row();
                    MNT_TODAY.resultTable.Rows[MNT_Count]["FIELD_NAME"] = "PUBLIC_APPLY_CHAR_2";
                    MNT_TODAY.resultTable.Rows[MNT_Count]["VARCHAR_BEFORE"] = Convert.ToString(DataTable_BEFORE.Rows[0]["PUBLIC_APPLY_CHAR_2"]);
                    MNT_TODAY.resultTable.Rows[MNT_Count]["VARCHAR_AFTER"] = this.PUBLIC_APPLY_CHAR_2;
                    MNT_Count++;
                }
                if (this.PUBLIC_APPLY_CHAR_3 != null && this.PUBLIC_APPLY_CHAR_3 != Convert.ToString(DataTable_BEFORE.Rows[0]["PUBLIC_APPLY_CHAR_3"]))
                {
                    MNT_TODAY.initInsert_row();
                    MNT_TODAY.resultTable.Rows[MNT_Count]["FIELD_NAME"] = "PUBLIC_APPLY_CHAR_3";
                    MNT_TODAY.resultTable.Rows[MNT_Count]["VARCHAR_BEFORE"] = Convert.ToString(DataTable_BEFORE.Rows[0]["PUBLIC_APPLY_CHAR_3"]);
                    MNT_TODAY.resultTable.Rows[MNT_Count]["VARCHAR_AFTER"] = this.PUBLIC_APPLY_CHAR_3;
                    MNT_Count++;
                }
                if (this.PUBLIC_APPLY_CHAR_4 != null && this.PUBLIC_APPLY_CHAR_4 != Convert.ToString(DataTable_BEFORE.Rows[0]["PUBLIC_APPLY_CHAR_4"]))
                {
                    MNT_TODAY.initInsert_row();
                    MNT_TODAY.resultTable.Rows[MNT_Count]["FIELD_NAME"] = "PUBLIC_APPLY_CHAR_4";
                    MNT_TODAY.resultTable.Rows[MNT_Count]["VARCHAR_BEFORE"] = Convert.ToString(DataTable_BEFORE.Rows[0]["PUBLIC_APPLY_CHAR_4"]);
                    MNT_TODAY.resultTable.Rows[MNT_Count]["VARCHAR_AFTER"] = this.PUBLIC_APPLY_CHAR_4;
                    MNT_Count++;
                }
                if (this.PUBLIC_APPLY_CHAR_5 != null && this.PUBLIC_APPLY_CHAR_5 != Convert.ToString(DataTable_BEFORE.Rows[0]["PUBLIC_APPLY_CHAR_5"]))
                {
                    MNT_TODAY.initInsert_row();
                    MNT_TODAY.resultTable.Rows[MNT_Count]["FIELD_NAME"] = "PUBLIC_APPLY_CHAR_5";
                    MNT_TODAY.resultTable.Rows[MNT_Count]["VARCHAR_BEFORE"] = Convert.ToString(DataTable_BEFORE.Rows[0]["PUBLIC_APPLY_CHAR_5"]);
                    MNT_TODAY.resultTable.Rows[MNT_Count]["VARCHAR_AFTER"] = this.PUBLIC_APPLY_CHAR_5;
                    MNT_Count++;
                }
                if (this.PUBLIC_APPLY_AMT_1 > -1000000000000 && this.PUBLIC_APPLY_AMT_1 != Convert.ToDecimal(DataTable_BEFORE.Rows[0]["PUBLIC_APPLY_AMT_1"]))
                {
                    MNT_TODAY.initInsert_row();
                    MNT_TODAY.resultTable.Rows[MNT_Count]["FIELD_NAME"] = "PUBLIC_APPLY_AMT_1";
                    MNT_TODAY.resultTable.Rows[MNT_Count]["DECIMAL_BEFORE"] = Convert.ToDecimal(DataTable_BEFORE.Rows[0]["PUBLIC_APPLY_AMT_1"]);
                    MNT_TODAY.resultTable.Rows[MNT_Count]["DECIMAL_AFTER"] = this.PUBLIC_APPLY_AMT_1;
                    MNT_Count++;
                }
                if (this.PUBLIC_APPLY_AMT_2 > -1000000000000 && this.PUBLIC_APPLY_AMT_2 != Convert.ToDecimal(DataTable_BEFORE.Rows[0]["PUBLIC_APPLY_AMT_2"]))
                {
                    MNT_TODAY.initInsert_row();
                    MNT_TODAY.resultTable.Rows[MNT_Count]["FIELD_NAME"] = "PUBLIC_APPLY_AMT_2";
                    MNT_TODAY.resultTable.Rows[MNT_Count]["DECIMAL_BEFORE"] = Convert.ToDecimal(DataTable_BEFORE.Rows[0]["PUBLIC_APPLY_AMT_2"]);
                    MNT_TODAY.resultTable.Rows[MNT_Count]["DECIMAL_AFTER"] = this.PUBLIC_APPLY_AMT_2;
                    MNT_Count++;
                }
                if (this.PUBLIC_APPLY_AMT_3 > -1000000000000 && this.PUBLIC_APPLY_AMT_3 != Convert.ToDecimal(DataTable_BEFORE.Rows[0]["PUBLIC_APPLY_AMT_3"]))
                {
                    MNT_TODAY.initInsert_row();
                    MNT_TODAY.resultTable.Rows[MNT_Count]["FIELD_NAME"] = "PUBLIC_APPLY_AMT_3";
                    MNT_TODAY.resultTable.Rows[MNT_Count]["DECIMAL_BEFORE"] = Convert.ToDecimal(DataTable_BEFORE.Rows[0]["PUBLIC_APPLY_AMT_3"]);
                    MNT_TODAY.resultTable.Rows[MNT_Count]["DECIMAL_AFTER"] = this.PUBLIC_APPLY_AMT_3;
                    MNT_Count++;
                }
                if (this.PUBLIC_APPLY_AMT_4 > -1000000000000 && this.PUBLIC_APPLY_AMT_4 != Convert.ToDecimal(DataTable_BEFORE.Rows[0]["PUBLIC_APPLY_AMT_4"]))
                {
                    MNT_TODAY.initInsert_row();
                    MNT_TODAY.resultTable.Rows[MNT_Count]["FIELD_NAME"] = "PUBLIC_APPLY_AMT_4";
                    MNT_TODAY.resultTable.Rows[MNT_Count]["DECIMAL_BEFORE"] = Convert.ToDecimal(DataTable_BEFORE.Rows[0]["PUBLIC_APPLY_AMT_4"]);
                    MNT_TODAY.resultTable.Rows[MNT_Count]["DECIMAL_AFTER"] = this.PUBLIC_APPLY_AMT_4;
                    MNT_Count++;
                }
                if (this.PUBLIC_APPLY_AMT_5 > -1000000000000 && this.PUBLIC_APPLY_AMT_5 != Convert.ToDecimal(DataTable_BEFORE.Rows[0]["PUBLIC_APPLY_AMT_5"]))
                {
                    MNT_TODAY.initInsert_row();
                    MNT_TODAY.resultTable.Rows[MNT_Count]["FIELD_NAME"] = "PUBLIC_APPLY_AMT_5";
                    MNT_TODAY.resultTable.Rows[MNT_Count]["DECIMAL_BEFORE"] = Convert.ToDecimal(DataTable_BEFORE.Rows[0]["PUBLIC_APPLY_AMT_5"]);
                    MNT_TODAY.resultTable.Rows[MNT_Count]["DECIMAL_AFTER"] = this.PUBLIC_APPLY_AMT_5;
                    MNT_Count++;
                }
                if (this.PUBLIC_APPLY_DT_1 > dateStart && this.PUBLIC_APPLY_DT_1 != Convert.ToDateTime(DataTable_BEFORE.Rows[0]["PUBLIC_APPLY_DT_1"]))
                {
                    if (this.PUBLIC_APPLY_DT_1.ToString("yyyyMMdd") == "19000101" &&
                        Convert.ToDateTime(DataTable_BEFORE.Rows[0]["PUBLIC_APPLY_DT_1"]) == dateStart)
                    {
                    }
                    else
                    {
                        MNT_TODAY.initInsert_row();
                        MNT_TODAY.resultTable.Rows[MNT_Count]["FIELD_NAME"] = "PUBLIC_APPLY_DT_1";
                        MNT_TODAY.resultTable.Rows[MNT_Count]["DT_BEFORE"] = Convert.ToDateTime(DataTable_BEFORE.Rows[0]["PUBLIC_APPLY_DT_1"]);
                        if (this.PUBLIC_APPLY_DT_1.ToString("yyyyMMdd") == "19000101")
                        {
                            MNT_TODAY.resultTable.Rows[MNT_Count]["DT_AFTER"] = "1900-01-01";
                        }
                        else
                        {
                            MNT_TODAY.resultTable.Rows[MNT_Count]["DT_AFTER"] = this.PUBLIC_APPLY_DT_1;
                        }
                        MNT_Count++;
                    }
                }
                if (this.PUBLIC_APPLY_DT_2 > dateStart && this.PUBLIC_APPLY_DT_2 != Convert.ToDateTime(DataTable_BEFORE.Rows[0]["PUBLIC_APPLY_DT_2"]))
                {
                    if (this.PUBLIC_APPLY_DT_2.ToString("yyyyMMdd") == "19000101" &&
                        Convert.ToDateTime(DataTable_BEFORE.Rows[0]["PUBLIC_APPLY_DT_2"]) == dateStart)
                    {
                    }
                    else
                    {
                        MNT_TODAY.initInsert_row();
                        MNT_TODAY.resultTable.Rows[MNT_Count]["FIELD_NAME"] = "PUBLIC_APPLY_DT_2";
                        MNT_TODAY.resultTable.Rows[MNT_Count]["DT_BEFORE"] = Convert.ToDateTime(DataTable_BEFORE.Rows[0]["PUBLIC_APPLY_DT_2"]);
                        if (this.PUBLIC_APPLY_DT_2.ToString("yyyyMMdd") == "19000101")
                        {
                            MNT_TODAY.resultTable.Rows[MNT_Count]["DT_AFTER"] = "1900-01-01";
                        }
                        else
                        {
                            MNT_TODAY.resultTable.Rows[MNT_Count]["DT_AFTER"] = this.PUBLIC_APPLY_DT_2;
                        }
                        MNT_Count++;
                    }
                }
                if (this.PUBLIC_APPLY_DT_3 > dateStart && this.PUBLIC_APPLY_DT_3 != Convert.ToDateTime(DataTable_BEFORE.Rows[0]["PUBLIC_APPLY_DT_3"]))
                {
                    if (this.PUBLIC_APPLY_DT_3.ToString("yyyyMMdd") == "19000101" &&
                        Convert.ToDateTime(DataTable_BEFORE.Rows[0]["PUBLIC_APPLY_DT_3"]) == dateStart)
                    {
                    }
                    else
                    {
                        MNT_TODAY.initInsert_row();
                        MNT_TODAY.resultTable.Rows[MNT_Count]["FIELD_NAME"] = "PUBLIC_APPLY_DT_3";
                        MNT_TODAY.resultTable.Rows[MNT_Count]["DT_BEFORE"] = Convert.ToDateTime(DataTable_BEFORE.Rows[0]["PUBLIC_APPLY_DT_3"]);
                        if (this.PUBLIC_APPLY_DT_3.ToString("yyyyMMdd") == "19000101")
                        {
                            MNT_TODAY.resultTable.Rows[MNT_Count]["DT_AFTER"] = "1900-01-01";
                        }
                        else
                        {
                            MNT_TODAY.resultTable.Rows[MNT_Count]["DT_AFTER"] = this.PUBLIC_APPLY_DT_3;
                        }
                        MNT_Count++;
                    }
                }
                if (this.PUBLIC_APPLY_DT_4 > dateStart && this.PUBLIC_APPLY_DT_4 != Convert.ToDateTime(DataTable_BEFORE.Rows[0]["PUBLIC_APPLY_DT_4"]))
                {
                    if (this.PUBLIC_APPLY_DT_4.ToString("yyyyMMdd") == "19000101" &&
                        Convert.ToDateTime(DataTable_BEFORE.Rows[0]["PUBLIC_APPLY_DT_4"]) == dateStart)
                    {
                    }
                    else
                    {
                        MNT_TODAY.initInsert_row();
                        MNT_TODAY.resultTable.Rows[MNT_Count]["FIELD_NAME"] = "PUBLIC_APPLY_DT_4";
                        MNT_TODAY.resultTable.Rows[MNT_Count]["DT_BEFORE"] = Convert.ToDateTime(DataTable_BEFORE.Rows[0]["PUBLIC_APPLY_DT_4"]);
                        if (this.PUBLIC_APPLY_DT_4.ToString("yyyyMMdd") == "19000101")
                        {
                            MNT_TODAY.resultTable.Rows[MNT_Count]["DT_AFTER"] = "1900-01-01";
                        }
                        else
                        {
                            MNT_TODAY.resultTable.Rows[MNT_Count]["DT_AFTER"] = this.PUBLIC_APPLY_DT_4;
                        }
                        MNT_Count++;
                    }
                }
                if (this.PUBLIC_APPLY_DT_5 > dateStart && this.PUBLIC_APPLY_DT_5 != Convert.ToDateTime(DataTable_BEFORE.Rows[0]["PUBLIC_APPLY_DT_5"]))
                {
                    if (this.PUBLIC_APPLY_DT_5.ToString("yyyyMMdd") == "19000101" &&
                        Convert.ToDateTime(DataTable_BEFORE.Rows[0]["PUBLIC_APPLY_DT_5"]) == dateStart)
                    {
                    }
                    else
                    {
                        MNT_TODAY.initInsert_row();
                        MNT_TODAY.resultTable.Rows[MNT_Count]["FIELD_NAME"] = "PUBLIC_APPLY_DT_5";
                        MNT_TODAY.resultTable.Rows[MNT_Count]["DT_BEFORE"] = Convert.ToDateTime(DataTable_BEFORE.Rows[0]["PUBLIC_APPLY_DT_5"]);
                        if (this.PUBLIC_APPLY_DT_5.ToString("yyyyMMdd") == "19000101")
                        {
                            MNT_TODAY.resultTable.Rows[MNT_Count]["DT_AFTER"] = "1900-01-01";
                        }
                        else
                        {
                            MNT_TODAY.resultTable.Rows[MNT_Count]["DT_AFTER"] = this.PUBLIC_APPLY_DT_5;
                        }
                        MNT_Count++;
                    }
                }
                #endregion
                
                if (MNT_TODAY.resultTable.Rows.Count == 0)  //沒有欄位有異動
                {
                    msg_code = "F0023";
                }
                else
                {
                    msg_code = update();  //寫入資料
                if (msg_code == "S0000" || msg_code == "S0002")
                    MNT_TODAY.insert_by_DT(); //寫入異動紀錄
                }
            }
            catch (SqlException e)
            {
                MSG.strMsg = Convert.ToString(e.Number) + '-' + e.Message;
                msg_code = MSG.getMsg();
            }

            return msg_code;
        }
        #endregion
        #region insert_by_Telegram(電文新增)
        public string insert_by_Telegram(string strMNT_PGM)
        {
            #region Modify History
            /// <history>
            /// <design>
            /// <name>Anna</name>
            /// <date>2012/4/12 上午 10:32:20</date>
            #endregion
            try
            {
                #region 宣告MNT_TODAYDAO並放入初始值
                Cybersoft.Dao.Core.MNT_TODAYDao MNT_TODAY = new Cybersoft.Dao.Core.MNT_TODAYDao();
                MNT_TODAY.table_define();
                MNT_TODAY.strinitTBL_NAME = "PUBLIC_APPLYD";
                MNT_TODAY.strinitPOST_RESULT = "00";
                MNT_TODAY.strinitMNT_PGM = strMNT_PGM;
                MNT_TODAY.strinitMNT_USER = this.strMNT_USER;
                MNT_TODAY.DateTimeinitMNT_DT = DateTime.Now;
                MNT_TODAY.DateTimeinitEFF_DTE = DateTime.Now;
                MNT_TODAY.DateTimeinitPOSTING_DTE = DateTime.Now;

                #endregion

                #region 取得鍵值
                MNT_TODAY.strinitBU = this.strBU;
                MNT_TODAY.strinitACCT_NBR = this.ACCT_NBR;
                MNT_TODAY.strinitPRODUCT = "";
                MNT_TODAY.strinitCURRENCY = "";
                MNT_TODAY.strinitCARD_NBR = this.strPAY_CARD_NBR;
                MNT_TODAY.strinitOTHER_KEY_1 = "";
                MNT_TODAY.strinitOTHER_KEY_2 = "";

                #endregion

                #region 寫入log
                //修改欄位部分
                MNT_TODAY.initInsert_row();
                MNT_TODAY.resultTable.Rows[0]["FIELD_NAME"] = "INSERT";
                #endregion

                msg_code = insert();
                if (msg_code == "S0000" || msg_code == "S0002")
                MNT_TODAY.insert_by_DT();
            }
            catch (SqlException e)
            {
                MSG.strMsg = Convert.ToString(e.Number) + '-' + e.Message;
                msg_code = MSG.getMsg();
            }

            return msg_code;
        }
        #endregion
        #region query_for_PBAPPLY
        public string query_for_PBAPPLY(String FILE_FORMAT)
        {
            #region Modify History
            /// <history>
            /// <design>取得當日ACH約定代繳資料
            /// <name>Sylvia</name>
            /// <date>2012/4/16 上午 11:40:00</date>
            #endregion

            try
            {
                #region  set SQL statement
                StringBuilder sbstrSQL = new StringBuilder();
                //sbstrSQL.Append("SELECT * FROM PUBLIC_APPLY WHERE PAY_TYPE IN ( " + PAY_TYPE + " ) AND REPLY_FLAG = ' ' ");
                //sbstrSQL.Append("ORDER BY PAY_TYPE ");
                sbstrSQL.Append("SELECT  a.PAY_CARD_NBR,a.PAY_TYPE,a.PAY_NBR,a.SEQ,a.BU,a.PRODUCT,a.CARD_PRODUCT,a.ACCT_NBR,a.PAY_ACCT_NBR,a.PAY_CARD_NBR_PREV,a.PAY_ACCT_NBR_PREV,a.CUST_SEQ,a.EXPIR_DTE,a.APPLY_DTE,a.VAILD_FLAG,a.REPLY_FLAG,a.REPLY_DTE , a.MNT_USER, a.MNT_DT ");
                sbstrSQL.Append("                ,c.CUST_ID, b.FILE_TRANSFER_TYPE, b.FILE_TRANSFER_UNIT, b.DESCR  ");
                sbstrSQL.Append("  FROM PUBLIC_APPLY a ");
                sbstrSQL.Append("     JOIN SETUP_PUBLIC b ON b.PAY_TYPE = a.PAY_TYPE   AND  b.FILE_FORMAT = '" + FILE_FORMAT + "'  AND  b.POST_RESULT = '00'");
                sbstrSQL.Append("     JOIN ID_VIEW c ON A.CUST_SEQ=C.CUST_SEQ ");
                sbstrSQL.Append("WHERE 1=1 ");
                sbstrSQL.Append("      AND a.REPLY_FLAG = '' ");
                sbstrSQL.Append(" ORDER BY a.PAY_TYPE, a.PAY_NBR ");
                #endregion
                this.SelectOperator.SqlText = sbstrSQL.ToString();
                #region replace SQL parameter
                
                #endregion
                
                //myTable set to DataTable object
                myTable = this.SelectOperator.GetDataTable("PUBLIC_APPLY");
                if (myTable.Rows.Count == 0)
                {
                    msg_code = "F0023"; //not found
                }
                else
                {
                    msg_code = "S0000"; //query success
                }
            }
            catch (SqlException e)
            {
                MSG.strMsg = Convert.ToString(e.Number) + '-' + e.Message;
                msg_code = MSG.getMsg();
            }
            finally
            {

            }
            return msg_code;
        }
        #endregion
        #region query_BRANCH_INF(string PAY_ACCT_NBR, string Action)
        public string query_BRANCH_INF(string PAY_ACCT_NBR, string Action)
        {
            #region Modify History
            /// <history>
            /// <design>取得約定代繳客戶的管理分行資訊
            /// <name>Sylvia</name>
            /// <date>2012/9/21 13:20</date>
            #endregion

            try
            {
                #region  set SQL statement
                StringBuilder sbstrSQL = new StringBuilder();
                sbstrSQL.Append(" SELECT A.REPLY_DTE,D.BR_NO,D.BR_NAME,A.PAY_CARD_NBR   ");
                sbstrSQL.Append(" FROM PUBLIC_APPLY A   ");
                sbstrSQL.Append(" JOIN CARD_INF B ON A.PAY_CARD_NBR  = B.CARD_NBR  ");
                sbstrSQL.Append(" JOIN CUST_INF C ON B.CUST_NBR = C.CUST_NBR  ");
                sbstrSQL.Append(" JOIN BRANCH D   ON C.ADDR_BRANCH = D.BR_NO ");
                sbstrSQL.Append(" WHERE A.REPLY_FLAG = @whereREPLY_FLAG AND A.PAY_TYPE = @wherePAY_TYPE ");
                if (Action == "PAY_ACCT_NBR")
                {
                    sbstrSQL.Append(" AND A.PAY_CARD_NBR IN ( ");
                    sbstrSQL.Append(" SELECT SUBSTRING(CONVERT(VARCHAR(16),BEG_NBR),1,6)+substring('" + PAY_ACCT_NBR + "',5,10) AS CARD_NBR ");
                    sbstrSQL.Append(" FROM SETUP_PRODUCT GROUP BY PRODUCT_SERVICE_3,BEG_NBR  ");
                    sbstrSQL.Append(" HAVING PRODUCT_SERVICE_3 = substring('" + PAY_ACCT_NBR + "',3,2)  ");
                    sbstrSQL.Append(" )  ORDER BY REPLY_DTE DESC");
                }
                else
                {
                    sbstrSQL.Append(" AND A.PAY_CARD_NBR = @wherePAY_CARD_NBR ORDER BY REPLY_DTE DESC");
                }

                #endregion

                this.SelectOperator.SqlText = sbstrSQL.ToString();

                #region replace SQL parameter
                if (sbstrSQL.ToString().Contains("@whereREPLY_FLAG "))
                {
                    this.SelectOperator.SetValue("@whereREPLY_FLAG", this.whereREPLY_FLAG);
                }
                if (sbstrSQL.ToString().Contains("@wherePAY_TYPE "))
                {
                    this.SelectOperator.SetValue("@wherePAY_TYPE", this.wherePAY_TYPE);
                }
                if (sbstrSQL.ToString().Contains("@wherePAY_CARD_NBR "))
                {
                    this.SelectOperator.SetValue("@wherePAY_CARD_NBR", this.wherePAY_CARD_NBR);
                }
                #endregion

                //myTable set to DataTable object
                myTable = this.SelectOperator.GetDataTable("PUBLIC_APPLY");
                if (myTable.Rows.Count == 0)
                {
                    msg_code = "F0023"; //not found
                }
                else
                {
                    msg_code = "S0000"; //query success
                }
            }
            catch (SqlException e)
            {
                MSG.strMsg = Convert.ToString(e.Number) + '-' + e.Message;
                msg_code = MSG.getMsg();
            }
            finally
            {

            }
            return msg_code;
        }
        #endregion
        #region update_PREV_INF(string strTODAY)
        public string update_PREV_INF(string strTODAY)
        {
            #region Modify History
            /// <history>
            /// <design>
            /// <name>Cybersoft ANNA</name>
            /// <date>2012-09-26</date>
            #endregion
            try
            {
                #region set SQL statement
                StringBuilder sbstrSQL = new StringBuilder();

                sbstrSQL.Append(" UPDATE B SET B.PAY_CARD_NBR_PREV = A.PAY_CARD_NBR, ");
                sbstrSQL.Append("              B.PAY_ACCT_NBR_PREV = case when D.PAY_CARD_TRANS = '' then A.PAY_CARD_NBR                     ");
                sbstrSQL.Append("                                         else D.PAY_CARD_TRANS + C.PRODUCT_SERVICE_3 + substring(A.PAY_CARD_NBR,7,10)  end , ");
                sbstrSQL.Append("               B.FIRST_DTE = CASE WHEN B.FIRST_DTE = ''  THEN @strTODAY                       ");
                sbstrSQL.Append("                                  ELSE B.FIRST_DTE END,                                       ");
                sbstrSQL.Append("               B.PAY_DTE = @strTODAY                                                          ");
                sbstrSQL.Append(" FROM (SELECT * FROM PUBLIC_HIST  WHERE TRANS_DTE = @strTODAY AND PAY_RESULT = '0000' ) A  ");
                sbstrSQL.Append(" JOIN (SELECT * FROM PUBLIC_APPLY WHERE VAILD_FLAG = 'Y' ) B ");
                sbstrSQL.Append("   ON  A.PAY_TYPE = B.PAY_TYPE AND A.PAY_NBR = B.PAY_NBR  AND A.BU = B.BU  ");
                //指定同卡別
                //sbstrSQL.Append("                                   AND A.CARD_PRODUCT = B.CARD_PRODUCT  AND substring(A.PAY_CARD_NBR,1,13) = substring(B.PAY_CARD_NBR,1,13)  ");
                sbstrSQL.Append(" JOIN (SELECT DISTINCT RTRIM(PRODUCT_SERVICE_3) PRODUCT_SERVICE_3,PRODUCT  FROM SETUP_PRODUCT ) C  ");
                sbstrSQL.Append("   ON A.CARD_PRODUCT = C.PRODUCT ");
                sbstrSQL.Append(" JOIN  SETUP_PUBLIC D  on A.PAY_TYPE = D.PAY_TYPE  ");

                #endregion
                this.UpdateOperator.SqlText = sbstrSQL.ToString();
                this.UpdateOperator.SetValue("@strTODAY", strTODAY);

                UptCnt = this.UpdateOperator.Execute();
                msg_code = "S0000"; //修改成功代碼
            }
            catch (SqlException e)
            {
                MSG.strMsg = Convert.ToString(e.Number) + '-' + e.Message;
                msg_code = MSG.getMsg();
            }
            return msg_code;
        }
        #endregion
        #region query_for_app_detail()
        public string query_for_app_detail()
        {
            #region Modify History
            /// <history>
            /// <design>
            /// <name>JOSEPH</name>
            /// <date>2013/08/13</date>
            #endregion
            try
            {
                #region set SQL statement
                StringBuilder sbstrSQL = new StringBuilder();
                sbstrSQL.Append("SELECT B.NAME,C.CUST_ID,A.*  ");
                sbstrSQL.Append("FROM PUBLIC_APPLY A ");
                sbstrSQL.Append("LEFT JOIN CUST_INF B ON A.BU=B.BU AND A.CUST_SEQ=B.CUST_NBR ");
                sbstrSQL.Append("LEFT JOIN ACCT_LINK C ON A.BU=C.BU AND A.ACCT_NBR=C.ACCT_NBR AND A.CUST_SEQ=C.CUST_SEQ ");
                sbstrSQL.Append("WHERE 1=1 ");
                if (this.wherePAY_CARD_NBR != null)
                {
                    sbstrSQL.Append(" and A.PAY_CARD_NBR = @wherePAY_CARD_NBR ");
                }
                if (this.wherePAY_TYPE != null)
                {
                    sbstrSQL.Append(" and A.PAY_TYPE = @wherePAY_TYPE ");
                }
                if (this.strWherePAY_NBR != null)
                {
                    sbstrSQL.Append(" and A.PAY_NBR = @wherePAY_NBR ");
                }
                if (this.strSEQ != null)
                {
                    sbstrSQL.Append(" and A.SEQ = @whereSEQ ");
                }
                #endregion
                this.SelectOperator.SqlText = sbstrSQL.ToString();
                #region replace SQL parameter
                if (sbstrSQL.ToString().Contains("@wherePAY_CARD_NBR "))
                {
                    this.SelectOperator.SetValue("@wherePAY_CARD_NBR", this.wherePAY_CARD_NBR);
                }
                if (sbstrSQL.ToString().Contains("@wherePAY_TYPE "))
                {
                    this.SelectOperator.SetValue("@wherePAY_TYPE", this.wherePAY_TYPE);
                }
                if (sbstrSQL.ToString().Contains("@wherePAY_NBR "))
                {
                    this.SelectOperator.SetValue("@wherePAY_NBR", this.wherePAY_NBR);
                }
                if (sbstrSQL.ToString().Contains("@whereSEQ "))
                {
                    this.SelectOperator.SetValue("@whereSEQ", this.whereSEQ);
                }
                #endregion
                //myTable set to DataTable object
                myTable = this.SelectOperator.GetDataTable("PUBLIC_APPLY");
                if (myTable.Rows.Count == 0)
                {
                    msg_code = "F0023"; //not found
                }
                else
                {
                    msg_code = "S0000"; //query success
                }
            }
            catch (SqlException e)
            {
                MSG.strMsg = Convert.ToString(e.Number) + '-' + e.Message;
                msg_code = MSG.getMsg();
            }
            return msg_code;
        }
        #endregion

        #region query_for_fisc()
        public string query_for_fisc()
        {
            #region Modify History
            /// <history>
            /// <design> 撈取報送財金約定檔
            /// <name>Cybersoft.COCA DaoGenerator</name>
            /// <date>2013/4/9 上午 12:26:10</date>
            #endregion
            try
            {
                #region set SQL statement
                StringBuilder sbstrSQL = new StringBuilder();
                sbstrSQL.Append("SELECT C.CUST_ID,B.FILE_TRANSFER_TYPE, B.TRANSFER_UNIT as TRANSFER_UNIT, B.DESCR, A.*  ");
                sbstrSQL.Append("  FROM PUBLIC_APPLY A ");
                sbstrSQL.Append("  JOIN SETUP_PUBLIC B ON B.PAY_TYPE = A.PAY_TYPE  AND B.FILE_TRANSFER_UNIT = 'FISC' AND POST_RESULT = '00'");
                //sbstrSQL.Append("  JOIN ACCT_LINK C ON A.BU=C.BU AND A.ACCT_NBR=C.ACCT_NBR AND  C.CARD_FLAG = 'P' ");
                sbstrSQL.Append("  JOIN ID_VIEW C ON A.CUST_SEQ=C.CUST_SEQ ");
                sbstrSQL.Append(" WHERE 1=1 ");
                sbstrSQL.Append("   AND A.REPLY_FLAG = '' ");
                sbstrSQL.Append(" ORDER BY A.PAY_TYPE, A.PAY_NBR ");
                #endregion
                this.SelectOperator.SqlText = sbstrSQL.ToString();
                #region replace SQL parameter
                
                #endregion
                //myTable set to DataTable object
                myTable = this.SelectOperator.GetDataTable("PUBLIC_APPLY");
                if (myTable.Rows.Count == 0)
                {
                    msg_code = "F0023"; //not found
                }
                else
                {
                    msg_code = "S0000"; //query success
                }
            }
            catch (SqlException e)
            {
                MSG.strMsg = Convert.ToString(e.Number) + '-' + e.Message;
                msg_code = MSG.getMsg();
            }
            return msg_code;
        }
        #endregion
        #region update_for_fisc()
        public string update_for_fisc()
        {
            #region Modify History
            /// <history>
            /// <design> 財金約定檔報送註記更新
            /// <name></name>
            /// <date>2013/4/9 上午 12:26:10</date>
            #endregion
            try
            {
                #region set SQL statement
                StringBuilder sbstrSQL = new StringBuilder();
                sbstrSQL.Append("  UPDATE PUBLIC_APPLY  ");
                sbstrSQL.Append("     SET REPLY_FLAG = @REPLY_FLAG,");
                sbstrSQL.Append("         REPLY_DATE = @REPLY_DATE,");
                sbstrSQL.Append("         MNT_USER   = @MNT_USER,");
                sbstrSQL.Append("         MNT_DATE   = @MNT_DATE,");
                sbstrSQL.Remove(sbstrSQL.ToString().Length - 1, 1); //移除最後一個逗號
                sbstrSQL.Append("  LEFT JOIN SETUP_PUBLIC B ON B.FILE_TRANSFER_UNIT = 'FISC' AND POST_RESULT = '00'");
                sbstrSQL.Append("  LEFT JOIN ACCT_LINK C ON A.BU=C.BU AND A.ACCT_NBR=C.ACCT_NBR AND A.CUST_SEQ=C.CUST_SEQ AND CARD_FLAG = 'P' ");
                sbstrSQL.Append(" WHERE 1=1 ");
                sbstrSQL.Append("   AND A.REPLY_FLAG = '' ");
                sbstrSQL.Append("   AND A.PAY_TYPE = B.PAY_TYPE ");

                #endregion
                this.SelectOperator.SqlText = sbstrSQL.ToString();
                #region replace SQL parameter

                #endregion
                //myTable set to DataTable object
                myTable = this.SelectOperator.GetDataTable("PUBLIC_APPLY");
                if (myTable.Rows.Count == 0)
                {
                    msg_code = "F0023"; //not found
                }
                else
                {
                    msg_code = "S0000"; //query success
                }
            }
            catch (SqlException e)
            {
                MSG.strMsg = Convert.ToString(e.Number) + '-' + e.Message;
                msg_code = MSG.getMsg();
            }
            return msg_code;
        }
        #endregion

        #region query_for_PHONE_CHANGE
        public string query_for_PHONE_CHANGE()
        {
            #region Modify History
            /// <history>
            /// <design>取得新帳號約定資料
            /// <name>Sylvia</name>
            /// <date>2012/4/16 上午 11:40:00</date>
            #endregion

            try
            {
                #region  set SQL statement
                StringBuilder sbstrSQL = new StringBuilder();
                sbstrSQL.Append("SELECT TOP 1 * FROM PUBLIC_APPLY ");
                //                sbstrSQL.Append(" WHERE PAY_TYPE = @wherePAY_TYPE AND PAY_NBR = @wherePAY_NBR AND SUBSTRING(PAY_CARD_NBR_PREV,1,14) = @wherePAY_CARD_NBR_PREV ");
                sbstrSQL.Append(" WHERE PAY_TYPE = @wherePAY_TYPE  ");
                sbstrSQL.Append("   AND PAY_NBR = @wherePAY_NBR            ");
                sbstrSQL.Append("   AND PAY_CARD_NBR_PREV = @wherePAY_CARD_NBR_PREV ");
                sbstrSQL.Append(" ORDER BY VAILD_FLAG DESC, SEQ DESC ");
                #endregion
                this.SelectOperator.SqlText = sbstrSQL.ToString();
                #region replace SQL parameter
                this.SelectOperator.SetValue("@wherePAY_TYPE", this.wherePAY_TYPE);
                this.SelectOperator.SetValue("@wherePAY_NBR", this.wherePAY_NBR);
                this.SelectOperator.SetValue("@wherePAY_CARD_NBR_PREV", this.wherePAY_CARD_NBR_PREV.ToString());
                #endregion

                //myTable set to DataTable object
                myTable = this.SelectOperator.GetDataTable("PUBLIC_APPLY");
                if (myTable.Rows.Count == 0)
                {
                    msg_code = "F0023"; //not found
                }
                else
                {
                    msg_code = "S0000"; //query success
                }
            }
            catch (SqlException e)
            {
                MSG.strMsg = Convert.ToString(e.Number) + '-' + e.Message;
                msg_code = MSG.getMsg();
            }
            finally
            {

            }
            return msg_code;
        }
        #endregion
        #region query_for_PAY_NBR_CHANGE
        public string query_for_PAY_NBR_CHANGE()
        {
            #region Modify History
            /// <history>
            /// <design>取得新帳號約定資料
            /// <name>Sylvia</name>
            /// <date>2013/7/10 上午 11:40:00</date>
            #endregion

            try
            {
                #region  set SQL statement
                StringBuilder sbstrSQL = new StringBuilder();
                sbstrSQL.Append("SELECT TOP 1 * FROM PUBLIC_APPLY ");
                sbstrSQL.Append(" WHERE PAY_TYPE = @wherePAY_TYPE AND PAY_NBR = @wherePAY_NBR AND PAY_CARD_NBR_PREV = @wherePAY_CARD_NBR_PREV ");
                sbstrSQL.Append(" ORDER BY VAILD_FLAG DESC, SEQ DESC ");
                #endregion
                this.SelectOperator.SqlText = sbstrSQL.ToString();
                #region replace SQL parameter
                this.SelectOperator.SetValue("@wherePAY_TYPE", this.wherePAY_TYPE);
                this.SelectOperator.SetValue("@wherePAY_NBR", this.wherePAY_NBR);
                this.SelectOperator.SetValue("@wherePAY_CARD_NBR_PREV", this.wherePAY_CARD_NBR_PREV);
                #endregion

                //myTable set to DataTable object
                myTable = this.SelectOperator.GetDataTable("PUBLIC_APPLY");
                if (myTable.Rows.Count == 0)
                {
                    msg_code = "F0023"; //not found
                }
                else
                {
                    msg_code = "S0000"; //query success
                }
            }
            catch (SqlException e)
            {
                MSG.strMsg = Convert.ToString(e.Number) + '-' + e.Message;
                msg_code = MSG.getMsg();
            }
            finally
            {

            }
            return msg_code;
        }
        #endregion
        #region update_reply_flag(string strFlag)
        public string update_reply_flag(string strFlag)
        {
            #region Modify History
            /// <history>
            /// <design> 中華電信約定檔-傳送註記更新
            /// <name></name>
            /// <date>2013/11/22 </date>
            #endregion
            try
            {
                #region set SQL statement
                StringBuilder sbstrSQL = new StringBuilder();
                sbstrSQL.Append("  UPDATE PUBLIC_APPLY  SET ");
                if (strFlag == " ")
                {
                    sbstrSQL.Append("     REPLY_FLAG = '',");
                    sbstrSQL.Append("     REPLY_DTE  = '',");
                }
                else
                {
                    sbstrSQL.Append("     REPLY_FLAG = '" + strFlag + "',");
                    sbstrSQL.Append("     REPLY_DTE  = @REPLY_DTE,");
                }
                sbstrSQL.Append("         MNT_USER   = @MNT_USER,");
                sbstrSQL.Append("         MNT_DT     = @MNT_DT,");
                sbstrSQL.Remove(sbstrSQL.ToString().Length - 1, 1); //移除最後一個逗號
                sbstrSQL.Append(" WHERE 1=1 ");
                sbstrSQL.Append("   AND  PAY_TYPE = @wherePAY_TYPE ");
                sbstrSQL.Append("   AND  EXPIR_DTE   <= @whereEXPIR_DTE    ");
                sbstrSQL.Append("   AND  VAILD_FLAG   = @whereVAILD_FLAG   ");

                #endregion
                this.UpdateOperator.SqlText = sbstrSQL.ToString();
                #region replace SQL parameter
                if (sbstrSQL.ToString().Contains("@REPLY_DTE"))
                {
                    this.UpdateOperator.SetValue("@REPLY_DTE", this.REPLY_DTE);
                }
                if (sbstrSQL.ToString().Contains("@MNT_DT"))
                {
                    this.UpdateOperator.SetValue("@MNT_DT", this.MNT_DT);
                }
                if (sbstrSQL.ToString().Contains("@MNT_USER"))
                {
                    this.UpdateOperator.SetValue("@MNT_USER", this.MNT_USER);
                }
                if (sbstrSQL.ToString().Contains("@wherePAY_TYPE"))
                {
                    this.UpdateOperator.SetValue("@wherePAY_TYPE", this.wherePAY_TYPE);
                }
                if (sbstrSQL.ToString().Contains("@whereEXPIR_DTE"))
                {
                    this.UpdateOperator.SetValue("@whereEXPIR_DTE", this.whereEXPIR_DTE);
                }
                if (sbstrSQL.ToString().Contains("@whereVAILD_FLAG"))
                {
                    this.UpdateOperator.SetValue("@whereVAILD_FLAG", this.whereVAILD_FLAG);
                }
                #endregion
                //myTable set to DataTable object
                UptCnt = this.UpdateOperator.Execute();
                msg_code = "S0000"; //update success
            }
            catch (SqlException e)
            {
                MSG.strMsg = Convert.ToString(e.Number) + '-' + e.Message;
                msg_code = MSG.getMsg();
            }
            return msg_code;
        }
        #endregion

        #region query_for_PBRRPT001(卡片停卡報表)
        public string query_for_PBRRPT001()
        {
            #region Modify History
            /// <history>
            /// <design>卡片停卡報表
            /// <name>Cybersoft</name>
            /// <date>2013/7/10 上午 11:40:00</date>
            #endregion

            try
            {
                #region  set SQL statement
                StringBuilder sbstrSQL = new StringBuilder();
                sbstrSQL.Append("select  f.DESCR,d.CUST_ID,a.PAY_CARD_NBR,a.PAY_NBR,(b.CTL_CODE+c.DESCR)as CTL_CODE ,b.CTL_CODE_DTE ,c.CARD_VALID,d.ALL_STOP,a.PAY_TYPE ");
                sbstrSQL.Append("  from ( SELECT * FROM PUBLIC_APPLY WHERE VAILD_FLAG = 'Y' )a                   ");
                sbstrSQL.Append("  left join CARD_INF      b on a.PAY_CARD_NBR = b.CARD_NBR and a.BU = b.BU      ");
                sbstrSQL.Append("       join SETUP_CTLCODE c on b.CTL_CODE = c.CTL_CODE and c.CARD_VALID = 'N'   ");
                sbstrSQL.Append("  left join ACCT_ANALYSIS d on a.BU = d.BU and a.ACCT_NBR = d.ACCT_NBR          ");
                sbstrSQL.Append("       join SETUP_PUBLIC  f on a.PAY_TYPE = f.PAY_TYPE                      ");
                sbstrSQL.Append("  order by b.CTL_CODE_DTE,a.PAY_TYPE                                        ");
                #endregion
                this.SelectOperator.SqlText = sbstrSQL.ToString();
                #region replace SQL parameter
                #endregion

                //myTable set to DataTable object
                myTable = this.SelectOperator.GetDataTable("PUBLIC_APPLY");
                if (myTable.Rows.Count == 0)
                {
                    msg_code = "F0023"; //not found
                }
                else
                {
                    msg_code = "S0000"; //query success
                }
            }
            catch (SqlException e)
            {
                MSG.strMsg = Convert.ToString(e.Number) + '-' + e.Message;
                msg_code = MSG.getMsg();
            }
            finally
            {

            }
            return msg_code;
        }
        #endregion
    }
}

