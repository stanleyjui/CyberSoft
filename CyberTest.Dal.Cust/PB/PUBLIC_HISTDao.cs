using System;
using System.Data;
using System.Collections;
using System.Configuration;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Text;

/// <summary>
/// PUBLIC_HISTDao，Provide PUBLIC_HISTCreate/Read/Update/Delete Function
/// 2014/12/31 DB Log_Cybersoft.Dao.Core.DAOBase
/// </summary>

namespace Cybersoft.Data.DAL
{
    //public partial class PUBLIC_HISTDao : Cybersoft.Data.DAOBase
    public partial class testPUBLIC_HISTDao : Cybersoft.Dao.Core.DAOBase
    {
        #region Modify History
        /// <history>
        /// <design>
        /// <name>Cybersoft.COCA DaoGenerator</name>
        /// <date>2014/12/24 上午 10:27:09</date>
        #endregion
        #region DataBase message convert
        Cybersoft.Dao.Core.MSG_DB MSG = new Cybersoft.Dao.Core.MSG_DB();
        #endregion
        #region Property(Field)
        private string TRANS_DTE = null;
        public string strTRANS_DTE
        {
            get { return TRANS_DTE; }
            set { TRANS_DTE = value; }
        }
        private string PAY_TYPE = null;
        public string strPAY_TYPE
        {
            get { return PAY_TYPE; }
            set { PAY_TYPE = value; }
        }
        private string BU = null;
        public string strBU
        {
            get { return BU; }
            set { BU = value; }
        }
        private string ACCT_NBR = null;
        public string strACCT_NBR
        {
            get { return ACCT_NBR; }
            set { ACCT_NBR = value; }
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
        private string CURRENCY = null;
        public string strCURRENCY
        {
            get { return CURRENCY; }
            set { CURRENCY = value; }
        }
        private string PAY_CARD_NBR = null;
        public string strPAY_CARD_NBR
        {
            get { return PAY_CARD_NBR; }
            set { PAY_CARD_NBR = value; }
        }
        private string EXPIR_DTE = null;
        public string strEXPIR_DTE
        {
            get { return EXPIR_DTE; }
            set { EXPIR_DTE = value; }
        }
        private string CUST_SEQ = null;
        public string strCUST_SEQ
        {
            get { return CUST_SEQ; }
            set { CUST_SEQ = value; }
        }
        private string PAY_NBR = null;
        public string strPAY_NBR
        {
            get { return PAY_NBR; }
            set { PAY_NBR = value; }
        }
        private decimal PAY_AMT = -1000000000000;
        public decimal decPAY_AMT
        {
            get { return PAY_AMT; }
            set { PAY_AMT = value; }
        }
        private DateTime PAY_DTE = new DateTime(1900, 1, 1);
        public DateTime datetimePAY_DTE
        {
            get { return PAY_DTE; }
            set { PAY_DTE = value; }
        }
        private string PAY_SEQ = null;
        public string strPAY_SEQ
        {
            get { return PAY_SEQ; }
            set { PAY_SEQ = value; }
        }
        private string PAY_RESULT = null;
        public string strPAY_RESULT
        {
            get { return PAY_RESULT; }
            set { PAY_RESULT = value; }
        }
        private string AUTH_CODE = null;
        public string strAUTH_CODE
        {
            get { return AUTH_CODE; }
            set { AUTH_CODE = value; }
        }
        private string ERROR_REASON = null;
        public string strERROR_REASON
        {
            get { return ERROR_REASON; }
            set { ERROR_REASON = value; }
        }
        private string REVERSAL_FLAG = null;
        public string strREVERSAL_FLAG
        {
            get { return REVERSAL_FLAG; }
            set { REVERSAL_FLAG = value; }
        }
        private string CTL_CODE = null;
        public string strCTL_CODE
        {
            get { return CTL_CODE; }
            set { CTL_CODE = value; }
        }
        private string AUTH_RESP = null;
        public string strAUTH_RESP
        {
            get { return AUTH_RESP; }
            set { AUTH_RESP = value; }
        }
        private string CHANGE_NBR_NEW = null;
        public string strCHANGE_NBR_NEW
        {
            get { return CHANGE_NBR_NEW; }
            set { CHANGE_NBR_NEW = value; }
        }
        private string FILE_TRANSFER_TYPE = null;
        public string strFILE_TRANSFER_TYPE
        {
            get { return FILE_TRANSFER_TYPE; }
            set { FILE_TRANSFER_TYPE = value; }
        }
        private string PAY_CARD_NBR_ORI = null;
        public string strPAY_CARD_NBR_ORI
        {
            get { return PAY_CARD_NBR_ORI; }
            set { PAY_CARD_NBR_ORI = value; }
        }
        private string PAY_ACCT_NBR_ORI = null;
        public string strPAY_ACCT_NBR_ORI
        {
            get { return PAY_ACCT_NBR_ORI; }
            set { PAY_ACCT_NBR_ORI = value; }
        }
        private string PUBLIC_HIST_FIELD_1 = null;
        public string strPUBLIC_HIST_FIELD_1
        {
            get { return PUBLIC_HIST_FIELD_1; }
            set { PUBLIC_HIST_FIELD_1 = value; }
        }
        private string PUBLIC_HIST_FIELD_2 = null;
        public string strPUBLIC_HIST_FIELD_2
        {
            get { return PUBLIC_HIST_FIELD_2; }
            set { PUBLIC_HIST_FIELD_2 = value; }
        }
        private string PUBLIC_HIST_FIELD_3 = null;
        public string strPUBLIC_HIST_FIELD_3
        {
            get { return PUBLIC_HIST_FIELD_3; }
            set { PUBLIC_HIST_FIELD_3 = value; }
        }
        private string PUBLIC_HIST_FIELD_4 = null;
        public string strPUBLIC_HIST_FIELD_4
        {
            get { return PUBLIC_HIST_FIELD_4; }
            set { PUBLIC_HIST_FIELD_4 = value; }
        }
        private string PUBLIC_HIST_FIELD_5 = null;
        public string strPUBLIC_HIST_FIELD_5
        {
            get { return PUBLIC_HIST_FIELD_5; }
            set { PUBLIC_HIST_FIELD_5 = value; }
        }
        private decimal PUBLIC_HIST_AMT_1 = -1000000000000;
        public decimal decPUBLIC_HIST_AMT_1
        {
            get { return PUBLIC_HIST_AMT_1; }
            set { PUBLIC_HIST_AMT_1 = value; }
        }
        private decimal PUBLIC_HIST_AMT_2 = -1000000000000;
        public decimal decPUBLIC_HIST_AMT_2
        {
            get { return PUBLIC_HIST_AMT_2; }
            set { PUBLIC_HIST_AMT_2 = value; }
        }
        private decimal PUBLIC_HIST_AMT_3 = -1000000000000;
        public decimal decPUBLIC_HIST_AMT_3
        {
            get { return PUBLIC_HIST_AMT_3; }
            set { PUBLIC_HIST_AMT_3 = value; }
        }
        private decimal PUBLIC_HIST_AMT_4 = -1000000000000;
        public decimal decPUBLIC_HIST_AMT_4
        {
            get { return PUBLIC_HIST_AMT_4; }
            set { PUBLIC_HIST_AMT_4 = value; }
        }
        private decimal PUBLIC_HIST_AMT_5 = -1000000000000;
        public decimal decPUBLIC_HIST_AMT_5
        {
            get { return PUBLIC_HIST_AMT_5; }
            set { PUBLIC_HIST_AMT_5 = value; }
        }
        private DateTime PUBLIC_HIST_DT_1 = new DateTime(1900, 1, 1);
        public DateTime datetimePUBLIC_HIST_DT_1
        {
            get { return PUBLIC_HIST_DT_1; }
            set { PUBLIC_HIST_DT_1 = value; }
        }
        private DateTime PUBLIC_HIST_DT_2 = new DateTime(1900, 1, 1);
        public DateTime datetimePUBLIC_HIST_DT_2
        {
            get { return PUBLIC_HIST_DT_2; }
            set { PUBLIC_HIST_DT_2 = value; }
        }
        private DateTime PUBLIC_HIST_DT_3 = new DateTime(1900, 1, 1);
        public DateTime datetimePUBLIC_HIST_DT_3
        {
            get { return PUBLIC_HIST_DT_3; }
            set { PUBLIC_HIST_DT_3 = value; }
        }
        private DateTime PUBLIC_HIST_DT_4 = new DateTime(1900, 1, 1);
        public DateTime datetimePUBLIC_HIST_DT_4
        {
            get { return PUBLIC_HIST_DT_4; }
            set { PUBLIC_HIST_DT_4 = value; }
        }
        private DateTime PUBLIC_HIST_DT_5 = new DateTime(1900, 1, 1);
        public DateTime datetimePUBLIC_HIST_DT_5
        {
            get { return PUBLIC_HIST_DT_5; }
            set { PUBLIC_HIST_DT_5 = value; }
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
        private string whereTRANS_DTE = null;
        public string strWhereTRANS_DTE
        {
            get { return whereTRANS_DTE; }
            set { whereTRANS_DTE = value; }
        }
        private string wherePAY_TYPE = null;
        public string strWherePAY_TYPE
        {
            get { return wherePAY_TYPE; }
            set { wherePAY_TYPE = value; }
        }
        private string whereBU = null;
        public string strWhereBU
        {
            get { return whereBU; }
            set { whereBU = value; }
        }
        private string whereACCT_NBR = null;
        public string strWhereACCT_NBR
        {
            get { return whereACCT_NBR; }
            set { whereACCT_NBR = value; }
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
        private string whereCURRENCY = null;
        public string strWhereCURRENCY
        {
            get { return whereCURRENCY; }
            set { whereCURRENCY = value; }
        }
        private string wherePAY_CARD_NBR = null;
        public string strWherePAY_CARD_NBR
        {
            get { return wherePAY_CARD_NBR; }
            set { wherePAY_CARD_NBR = value; }
        }
        private string whereEXPIR_DTE = null;
        public string strWhereEXPIR_DTE
        {
            get { return whereEXPIR_DTE; }
            set { whereEXPIR_DTE = value; }
        }
        private string whereCUST_SEQ = null;
        public string strWhereCUST_SEQ
        {
            get { return whereCUST_SEQ; }
            set { whereCUST_SEQ = value; }
        }
        private string wherePAY_NBR = null;
        public string strWherePAY_NBR
        {
            get { return wherePAY_NBR; }
            set { wherePAY_NBR = value; }
        }
        private decimal wherePAY_AMT = -1000000000000;
        public decimal decWherePAY_AMT
        {
            get { return wherePAY_AMT; }
            set { wherePAY_AMT = value; }
        }
        private DateTime wherePAY_DTE = new DateTime(1900, 1, 1);
        public DateTime DateTimeWherePAY_DTE
        {
            get { return wherePAY_DTE; }
            set { wherePAY_DTE = value; }
        }
        private string wherePAY_SEQ = null;
        public string strWherePAY_SEQ
        {
            get { return wherePAY_SEQ; }
            set { wherePAY_SEQ = value; }
        }
        private string wherePAY_RESULT = null;
        public string strWherePAY_RESULT
        {
            get { return wherePAY_RESULT; }
            set { wherePAY_RESULT = value; }
        }
        private string whereAUTH_CODE = null;
        public string strWhereAUTH_CODE
        {
            get { return whereAUTH_CODE; }
            set { whereAUTH_CODE = value; }
        }
        private string whereERROR_REASON = null;
        public string strWhereERROR_REASON
        {
            get { return whereERROR_REASON; }
            set { whereERROR_REASON = value; }
        }
        private string whereREVERSAL_FLAG = null;
        public string strWhereREVERSAL_FLAG
        {
            get { return whereREVERSAL_FLAG; }
            set { whereREVERSAL_FLAG = value; }
        }
        private string whereCTL_CODE = null;
        public string strWhereCTL_CODE
        {
            get { return whereCTL_CODE; }
            set { whereCTL_CODE = value; }
        }
        private string whereAUTH_RESP = null;
        public string strWhereAUTH_RESP
        {
            get { return whereAUTH_RESP; }
            set { whereAUTH_RESP = value; }
        }
        private string whereCHANGE_NBR_NEW = null;
        public string strWhereCHANGE_NBR_NEW
        {
            get { return whereCHANGE_NBR_NEW; }
            set { whereCHANGE_NBR_NEW = value; }
        }
        private string whereFILE_TRANSFER_TYPE = null;
        public string strWhereFILE_TRANSFER_TYPE
        {
            get { return whereFILE_TRANSFER_TYPE; }
            set { whereFILE_TRANSFER_TYPE = value; }
        }
        private string wherePAY_CARD_NBR_ORI = null;
        public string strWherePAY_CARD_NBR_ORI
        {
            get { return wherePAY_CARD_NBR_ORI; }
            set { wherePAY_CARD_NBR_ORI = value; }
        }
        private string wherePAY_ACCT_NBR_ORI = null;
        public string strWherePAY_ACCT_NBR_ORI
        {
            get { return wherePAY_ACCT_NBR_ORI; }
            set { wherePAY_ACCT_NBR_ORI = value; }
        }
        private string wherePUBLIC_HIST_FIELD_1 = null;
        public string strWherePUBLIC_HIST_FIELD_1
        {
            get { return wherePUBLIC_HIST_FIELD_1; }
            set { wherePUBLIC_HIST_FIELD_1 = value; }
        }
        private string wherePUBLIC_HIST_FIELD_2 = null;
        public string strWherePUBLIC_HIST_FIELD_2
        {
            get { return wherePUBLIC_HIST_FIELD_2; }
            set { wherePUBLIC_HIST_FIELD_2 = value; }
        }
        private string wherePUBLIC_HIST_FIELD_3 = null;
        public string strWherePUBLIC_HIST_FIELD_3
        {
            get { return wherePUBLIC_HIST_FIELD_3; }
            set { wherePUBLIC_HIST_FIELD_3 = value; }
        }
        private string wherePUBLIC_HIST_FIELD_4 = null;
        public string strWherePUBLIC_HIST_FIELD_4
        {
            get { return wherePUBLIC_HIST_FIELD_4; }
            set { wherePUBLIC_HIST_FIELD_4 = value; }
        }
        private string wherePUBLIC_HIST_FIELD_5 = null;
        public string strWherePUBLIC_HIST_FIELD_5
        {
            get { return wherePUBLIC_HIST_FIELD_5; }
            set { wherePUBLIC_HIST_FIELD_5 = value; }
        }
        private decimal wherePUBLIC_HIST_AMT_1 = -1000000000000;
        public decimal decWherePUBLIC_HIST_AMT_1
        {
            get { return wherePUBLIC_HIST_AMT_1; }
            set { wherePUBLIC_HIST_AMT_1 = value; }
        }
        private decimal wherePUBLIC_HIST_AMT_2 = -1000000000000;
        public decimal decWherePUBLIC_HIST_AMT_2
        {
            get { return wherePUBLIC_HIST_AMT_2; }
            set { wherePUBLIC_HIST_AMT_2 = value; }
        }
        private decimal wherePUBLIC_HIST_AMT_3 = -1000000000000;
        public decimal decWherePUBLIC_HIST_AMT_3
        {
            get { return wherePUBLIC_HIST_AMT_3; }
            set { wherePUBLIC_HIST_AMT_3 = value; }
        }
        private decimal wherePUBLIC_HIST_AMT_4 = -1000000000000;
        public decimal decWherePUBLIC_HIST_AMT_4
        {
            get { return wherePUBLIC_HIST_AMT_4; }
            set { wherePUBLIC_HIST_AMT_4 = value; }
        }
        private decimal wherePUBLIC_HIST_AMT_5 = -1000000000000;
        public decimal decWherePUBLIC_HIST_AMT_5
        {
            get { return wherePUBLIC_HIST_AMT_5; }
            set { wherePUBLIC_HIST_AMT_5 = value; }
        }
        private DateTime wherePUBLIC_HIST_DT_1 = new DateTime(1900, 1, 1);
        public DateTime DateTimeWherePUBLIC_HIST_DT_1
        {
            get { return wherePUBLIC_HIST_DT_1; }
            set { wherePUBLIC_HIST_DT_1 = value; }
        }
        private DateTime wherePUBLIC_HIST_DT_2 = new DateTime(1900, 1, 1);
        public DateTime DateTimeWherePUBLIC_HIST_DT_2
        {
            get { return wherePUBLIC_HIST_DT_2; }
            set { wherePUBLIC_HIST_DT_2 = value; }
        }
        private DateTime wherePUBLIC_HIST_DT_3 = new DateTime(1900, 1, 1);
        public DateTime DateTimeWherePUBLIC_HIST_DT_3
        {
            get { return wherePUBLIC_HIST_DT_3; }
            set { wherePUBLIC_HIST_DT_3 = value; }
        }
        private DateTime wherePUBLIC_HIST_DT_4 = new DateTime(1900, 1, 1);
        public DateTime DateTimeWherePUBLIC_HIST_DT_4
        {
            get { return wherePUBLIC_HIST_DT_4; }
            set { wherePUBLIC_HIST_DT_4 = value; }
        }
        private DateTime wherePUBLIC_HIST_DT_5 = new DateTime(1900, 1, 1);
        public DateTime DateTimeWherePUBLIC_HIST_DT_5
        {
            get { return wherePUBLIC_HIST_DT_5; }
            set { wherePUBLIC_HIST_DT_5 = value; }
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
        private string initTRANS_DTE = "";
        public string strinitTRANS_DTE
        {
            get { return initTRANS_DTE; }
            set { initTRANS_DTE = value; }
        }
        private string initPAY_TYPE = "";
        public string strinitPAY_TYPE
        {
            get { return initPAY_TYPE; }
            set { initPAY_TYPE = value; }
        }
        private string initBU = "";
        public string strinitBU
        {
            get { return initBU; }
            set { initBU = value; }
        }
        private string initACCT_NBR = "";
        public string strinitACCT_NBR
        {
            get { return initACCT_NBR; }
            set { initACCT_NBR = value; }
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
        private string initCURRENCY = "";
        public string strinitCURRENCY
        {
            get { return initCURRENCY; }
            set { initCURRENCY = value; }
        }
        private string initPAY_CARD_NBR = "";
        public string strinitPAY_CARD_NBR
        {
            get { return initPAY_CARD_NBR; }
            set { initPAY_CARD_NBR = value; }
        }
        private string initEXPIR_DTE = "";
        public string strinitEXPIR_DTE
        {
            get { return initEXPIR_DTE; }
            set { initEXPIR_DTE = value; }
        }
        private string initCUST_SEQ = "";
        public string strinitCUST_SEQ
        {
            get { return initCUST_SEQ; }
            set { initCUST_SEQ = value; }
        }
        private string initPAY_NBR = "";
        public string strinitPAY_NBR
        {
            get { return initPAY_NBR; }
            set { initPAY_NBR = value; }
        }
        private decimal initPAY_AMT = 0;
        public decimal decinitPAY_AMT
        {
            get { return initPAY_AMT; }
            set { initPAY_AMT = value; }
        }
        private DateTime initPAY_DTE = new DateTime(1900, 1, 1);
        public DateTime DateTimeinitPAY_DTE
        {
            get { return initPAY_DTE; }
            set { initPAY_DTE = value; }
        }
        private string initPAY_SEQ = "";
        public string strinitPAY_SEQ
        {
            get { return initPAY_SEQ; }
            set { initPAY_SEQ = value; }
        }
        private string initPAY_RESULT = "";
        public string strinitPAY_RESULT
        {
            get { return initPAY_RESULT; }
            set { initPAY_RESULT = value; }
        }
        private string initAUTH_CODE = "";
        public string strinitAUTH_CODE
        {
            get { return initAUTH_CODE; }
            set { initAUTH_CODE = value; }
        }
        private string initERROR_REASON = "";
        public string strinitERROR_REASON
        {
            get { return initERROR_REASON; }
            set { initERROR_REASON = value; }
        }
        private string initREVERSAL_FLAG = "";
        public string strinitREVERSAL_FLAG
        {
            get { return initREVERSAL_FLAG; }
            set { initREVERSAL_FLAG = value; }
        }
        private string initCTL_CODE = "";
        public string strinitCTL_CODE
        {
            get { return initCTL_CODE; }
            set { initCTL_CODE = value; }
        }
        private string initAUTH_RESP = "";
        public string strinitAUTH_RESP
        {
            get { return initAUTH_RESP; }
            set { initAUTH_RESP = value; }
        }
        private string initCHANGE_NBR_NEW = "";
        public string strinitCHANGE_NBR_NEW
        {
            get { return initCHANGE_NBR_NEW; }
            set { initCHANGE_NBR_NEW = value; }
        }
        private string initFILE_TRANSFER_TYPE = "";
        public string strinitFILE_TRANSFER_TYPE
        {
            get { return initFILE_TRANSFER_TYPE; }
            set { initFILE_TRANSFER_TYPE = value; }
        }
        private string initPAY_CARD_NBR_ORI = "";
        public string strinitPAY_CARD_NBR_ORI
        {
            get { return initPAY_CARD_NBR_ORI; }
            set { initPAY_CARD_NBR_ORI = value; }
        }
        private string initPAY_ACCT_NBR_ORI = "";
        public string strinitPAY_ACCT_NBR_ORI
        {
            get { return initPAY_ACCT_NBR_ORI; }
            set { initPAY_ACCT_NBR_ORI = value; }
        }
        private string initPUBLIC_HIST_FIELD_1 = "";
        public string strinitPUBLIC_HIST_FIELD_1
        {
            get { return initPUBLIC_HIST_FIELD_1; }
            set { initPUBLIC_HIST_FIELD_1 = value; }
        }
        private string initPUBLIC_HIST_FIELD_2 = "";
        public string strinitPUBLIC_HIST_FIELD_2
        {
            get { return initPUBLIC_HIST_FIELD_2; }
            set { initPUBLIC_HIST_FIELD_2 = value; }
        }
        private string initPUBLIC_HIST_FIELD_3 = "";
        public string strinitPUBLIC_HIST_FIELD_3
        {
            get { return initPUBLIC_HIST_FIELD_3; }
            set { initPUBLIC_HIST_FIELD_3 = value; }
        }
        private string initPUBLIC_HIST_FIELD_4 = "";
        public string strinitPUBLIC_HIST_FIELD_4
        {
            get { return initPUBLIC_HIST_FIELD_4; }
            set { initPUBLIC_HIST_FIELD_4 = value; }
        }
        private string initPUBLIC_HIST_FIELD_5 = "";
        public string strinitPUBLIC_HIST_FIELD_5
        {
            get { return initPUBLIC_HIST_FIELD_5; }
            set { initPUBLIC_HIST_FIELD_5 = value; }
        }
        private decimal initPUBLIC_HIST_AMT_1 = 0;
        public decimal decinitPUBLIC_HIST_AMT_1
        {
            get { return initPUBLIC_HIST_AMT_1; }
            set { initPUBLIC_HIST_AMT_1 = value; }
        }
        private decimal initPUBLIC_HIST_AMT_2 = 0;
        public decimal decinitPUBLIC_HIST_AMT_2
        {
            get { return initPUBLIC_HIST_AMT_2; }
            set { initPUBLIC_HIST_AMT_2 = value; }
        }
        private decimal initPUBLIC_HIST_AMT_3 = 0;
        public decimal decinitPUBLIC_HIST_AMT_3
        {
            get { return initPUBLIC_HIST_AMT_3; }
            set { initPUBLIC_HIST_AMT_3 = value; }
        }
        private decimal initPUBLIC_HIST_AMT_4 = 0;
        public decimal decinitPUBLIC_HIST_AMT_4
        {
            get { return initPUBLIC_HIST_AMT_4; }
            set { initPUBLIC_HIST_AMT_4 = value; }
        }
        private decimal initPUBLIC_HIST_AMT_5 = 0;
        public decimal decinitPUBLIC_HIST_AMT_5
        {
            get { return initPUBLIC_HIST_AMT_5; }
            set { initPUBLIC_HIST_AMT_5 = value; }
        }
        private DateTime initPUBLIC_HIST_DT_1 = new DateTime(1900, 1, 1);
        public DateTime DateTimeinitPUBLIC_HIST_DT_1
        {
            get { return initPUBLIC_HIST_DT_1; }
            set { initPUBLIC_HIST_DT_1 = value; }
        }
        private DateTime initPUBLIC_HIST_DT_2 = new DateTime(1900, 1, 1);
        public DateTime DateTimeinitPUBLIC_HIST_DT_2
        {
            get { return initPUBLIC_HIST_DT_2; }
            set { initPUBLIC_HIST_DT_2 = value; }
        }
        private DateTime initPUBLIC_HIST_DT_3 = new DateTime(1900, 1, 1);
        public DateTime DateTimeinitPUBLIC_HIST_DT_3
        {
            get { return initPUBLIC_HIST_DT_3; }
            set { initPUBLIC_HIST_DT_3 = value; }
        }
        private DateTime initPUBLIC_HIST_DT_4 = new DateTime(1900, 1, 1);
        public DateTime DateTimeinitPUBLIC_HIST_DT_4
        {
            get { return initPUBLIC_HIST_DT_4; }
            set { initPUBLIC_HIST_DT_4 = value; }
        }
        private DateTime initPUBLIC_HIST_DT_5 = new DateTime(1900, 1, 1);
        public DateTime DateTimeinitPUBLIC_HIST_DT_5
        {
            get { return initPUBLIC_HIST_DT_5; }
            set { initPUBLIC_HIST_DT_5 = value; }
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
        #region Property(user define)
        private DateTime wherePAY_DTE_ST = new DateTime(1900, 1, 1);
        public DateTime DateTimeWherePAY_DTE_ST
        {
            get { return wherePAY_DTE_ST; }
            set { wherePAY_DTE_ST = value; }
        }
        private DateTime wherePAY_DTE_ED = new DateTime(1900, 1, 1);
        public DateTime DateTimeWherePAY_DTE_ED
        {
            get { return wherePAY_DTE_ED; }
            set { wherePAY_DTE_ED = value; }
        }
        private string whereTRANS_DTE_ST = null;
        public string strWhereTRANS_DTE_ST
        {
            get { return whereTRANS_DTE_ST; }
            set { whereTRANS_DTE_ST = value; }
        }
        private string whereTRANS_DTE_ED = null;
        public string strWhereTRANS_DTE_ED
        {
            get { return whereTRANS_DTE_ED; }
            set { whereTRANS_DTE_ED = value; }
        }
        private string wherePAY_NBR_CHT = null;
        public string strWherePAY_NBR_CHT
        {
            get { return wherePAY_NBR_CHT; }
            set { wherePAY_NBR_CHT = value; }
        }
        #endregion
        #region Property(Group condtion)
        private string groupTRANS_DTE = null;
        public string strGroupTRANS_DTE
        {
            get { return groupTRANS_DTE; }
            set { groupTRANS_DTE = value; }
        }
        private string groupPAY_TYPE = null;
        public string strGroupPAY_TYPE
        {
            get { return groupPAY_TYPE; }
            set { groupPAY_TYPE = value; }
        }
        private string groupBU = null;
        public string strGroupBU
        {
            get { return groupBU; }
            set { groupBU = value; }
        }
        private string groupACCT_NBR = null;
        public string strGroupACCT_NBR
        {
            get { return groupACCT_NBR; }
            set { groupACCT_NBR = value; }
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
        private string groupCURRENCY = null;
        public string strGroupCURRENCY
        {
            get { return groupCURRENCY; }
            set { groupCURRENCY = value; }
        }
        private string groupPAY_CARD_NBR = null;
        public string strGroupPAY_CARD_NBR
        {
            get { return groupPAY_CARD_NBR; }
            set { groupPAY_CARD_NBR = value; }
        }
        private string groupEXPIR_DTE = null;
        public string strGroupEXPIR_DTE
        {
            get { return groupEXPIR_DTE; }
            set { groupEXPIR_DTE = value; }
        }
        private string groupCUST_SEQ = null;
        public string strGroupCUST_SEQ
        {
            get { return groupCUST_SEQ; }
            set { groupCUST_SEQ = value; }
        }
        private string groupPAY_NBR = null;
        public string strGroupPAY_NBR
        {
            get { return groupPAY_NBR; }
            set { groupPAY_NBR = value; }
        }
        private string groupPAY_AMT = null;
        public string strGroupPAY_AMT
        {
            get { return groupPAY_AMT; }
            set { groupPAY_AMT = value; }
        }
        private string groupPAY_DTE = null;
        public string strGroupPAY_DTE
        {
            get { return groupPAY_DTE; }
            set { groupPAY_DTE = value; }
        }
        private string groupPAY_SEQ = null;
        public string strGroupPAY_SEQ
        {
            get { return groupPAY_SEQ; }
            set { groupPAY_SEQ = value; }
        }
        private string groupPAY_RESULT = null;
        public string strGroupPAY_RESULT
        {
            get { return groupPAY_RESULT; }
            set { groupPAY_RESULT = value; }
        }
        private string groupAUTH_CODE = null;
        public string strGroupAUTH_CODE
        {
            get { return groupAUTH_CODE; }
            set { groupAUTH_CODE = value; }
        }
        private string groupERROR_REASON = null;
        public string strGroupERROR_REASON
        {
            get { return groupERROR_REASON; }
            set { groupERROR_REASON = value; }
        }
        private string groupREVERSAL_FLAG = null;
        public string strGroupREVERSAL_FLAG
        {
            get { return groupREVERSAL_FLAG; }
            set { groupREVERSAL_FLAG = value; }
        }
        private string groupCTL_CODE = null;
        public string strGroupCTL_CODE
        {
            get { return groupCTL_CODE; }
            set { groupCTL_CODE = value; }
        }
        private string groupAUTH_RESP = null;
        public string strGroupAUTH_RESP
        {
            get { return groupAUTH_RESP; }
            set { groupAUTH_RESP = value; }
        }
        private string groupCHANGE_NBR_NEW = null;
        public string strGroupCHANGE_NBR_NEW
        {
            get { return groupCHANGE_NBR_NEW; }
            set { groupCHANGE_NBR_NEW = value; }
        }
        private string groupFILE_TRANSFER_TYPE = null;
        public string strGroupFILE_TRANSFER_TYPE
        {
            get { return groupFILE_TRANSFER_TYPE; }
            set { groupFILE_TRANSFER_TYPE = value; }
        }
        private string groupPAY_CARD_NBR_ORI = null;
        public string strGroupPAY_CARD_NBR_ORI
        {
            get { return groupPAY_CARD_NBR_ORI; }
            set { groupPAY_CARD_NBR_ORI = value; }
        }
        private string groupPAY_ACCT_NBR_ORI = null;
        public string strGroupPAY_ACCT_NBR_ORI
        {
            get { return groupPAY_ACCT_NBR_ORI; }
            set { groupPAY_ACCT_NBR_ORI = value; }
        }
        private string groupPUBLIC_HIST_FIELD_1 = null;
        public string strGroupPUBLIC_HIST_FIELD_1
        {
            get { return groupPUBLIC_HIST_FIELD_1; }
            set { groupPUBLIC_HIST_FIELD_1 = value; }
        }
        private string groupPUBLIC_HIST_FIELD_2 = null;
        public string strGroupPUBLIC_HIST_FIELD_2
        {
            get { return groupPUBLIC_HIST_FIELD_2; }
            set { groupPUBLIC_HIST_FIELD_2 = value; }
        }
        private string groupPUBLIC_HIST_FIELD_3 = null;
        public string strGroupPUBLIC_HIST_FIELD_3
        {
            get { return groupPUBLIC_HIST_FIELD_3; }
            set { groupPUBLIC_HIST_FIELD_3 = value; }
        }
        private string groupPUBLIC_HIST_FIELD_4 = null;
        public string strGroupPUBLIC_HIST_FIELD_4
        {
            get { return groupPUBLIC_HIST_FIELD_4; }
            set { groupPUBLIC_HIST_FIELD_4 = value; }
        }
        private string groupPUBLIC_HIST_FIELD_5 = null;
        public string strGroupPUBLIC_HIST_FIELD_5
        {
            get { return groupPUBLIC_HIST_FIELD_5; }
            set { groupPUBLIC_HIST_FIELD_5 = value; }
        }
        private string groupPUBLIC_HIST_AMT_1 = null;
        public string strGroupPUBLIC_HIST_AMT_1
        {
            get { return groupPUBLIC_HIST_AMT_1; }
            set { groupPUBLIC_HIST_AMT_1 = value; }
        }
        private string groupPUBLIC_HIST_AMT_2 = null;
        public string strGroupPUBLIC_HIST_AMT_2
        {
            get { return groupPUBLIC_HIST_AMT_2; }
            set { groupPUBLIC_HIST_AMT_2 = value; }
        }
        private string groupPUBLIC_HIST_AMT_3 = null;
        public string strGroupPUBLIC_HIST_AMT_3
        {
            get { return groupPUBLIC_HIST_AMT_3; }
            set { groupPUBLIC_HIST_AMT_3 = value; }
        }
        private string groupPUBLIC_HIST_AMT_4 = null;
        public string strGroupPUBLIC_HIST_AMT_4
        {
            get { return groupPUBLIC_HIST_AMT_4; }
            set { groupPUBLIC_HIST_AMT_4 = value; }
        }
        private string groupPUBLIC_HIST_AMT_5 = null;
        public string strGroupPUBLIC_HIST_AMT_5
        {
            get { return groupPUBLIC_HIST_AMT_5; }
            set { groupPUBLIC_HIST_AMT_5 = value; }
        }
        private string groupPUBLIC_HIST_DT_1 = null;
        public string strGroupPUBLIC_HIST_DT_1
        {
            get { return groupPUBLIC_HIST_DT_1; }
            set { groupPUBLIC_HIST_DT_1 = value; }
        }
        private string groupPUBLIC_HIST_DT_2 = null;
        public string strGroupPUBLIC_HIST_DT_2
        {
            get { return groupPUBLIC_HIST_DT_2; }
            set { groupPUBLIC_HIST_DT_2 = value; }
        }
        private string groupPUBLIC_HIST_DT_3 = null;
        public string strGroupPUBLIC_HIST_DT_3
        {
            get { return groupPUBLIC_HIST_DT_3; }
            set { groupPUBLIC_HIST_DT_3 = value; }
        }
        private string groupPUBLIC_HIST_DT_4 = null;
        public string strGroupPUBLIC_HIST_DT_4
        {
            get { return groupPUBLIC_HIST_DT_4; }
            set { groupPUBLIC_HIST_DT_4 = value; }
        }
        private string groupPUBLIC_HIST_DT_5 = null;
        public string strGroupPUBLIC_HIST_DT_5
        {
            get { return groupPUBLIC_HIST_DT_5; }
            set { groupPUBLIC_HIST_DT_5 = value; }
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
            /// <date>2014/12/24 上午 10:27:09</date>
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
            /// <date>2014/12/24 上午 10:27:09</date>
            #endregion
            TRANS_DTE = null;
            PAY_TYPE = null;
            BU = null;
            ACCT_NBR = null;
            PRODUCT = null;
            CARD_PRODUCT = null;
            CURRENCY = null;
            PAY_CARD_NBR = null;
            EXPIR_DTE = null;
            CUST_SEQ = null;
            PAY_NBR = null;
            PAY_AMT = -1000000000000;
            PAY_DTE = dateStart;
            PAY_SEQ = null;
            PAY_RESULT = null;
            AUTH_CODE = null;
            ERROR_REASON = null;
            REVERSAL_FLAG = null;
            CTL_CODE = null;
            AUTH_RESP = null;
            CHANGE_NBR_NEW = null;
            FILE_TRANSFER_TYPE = null;
            PAY_CARD_NBR_ORI = null;
            PAY_ACCT_NBR_ORI = null;
            PUBLIC_HIST_FIELD_1 = null;
            PUBLIC_HIST_FIELD_2 = null;
            PUBLIC_HIST_FIELD_3 = null;
            PUBLIC_HIST_FIELD_4 = null;
            PUBLIC_HIST_FIELD_5 = null;
            PUBLIC_HIST_AMT_1 = -1000000000000;
            PUBLIC_HIST_AMT_2 = -1000000000000;
            PUBLIC_HIST_AMT_3 = -1000000000000;
            PUBLIC_HIST_AMT_4 = -1000000000000;
            PUBLIC_HIST_AMT_5 = -1000000000000;
            PUBLIC_HIST_DT_1 = dateStart;
            PUBLIC_HIST_DT_2 = dateStart;
            PUBLIC_HIST_DT_3 = dateStart;
            PUBLIC_HIST_DT_4 = dateStart;
            PUBLIC_HIST_DT_5 = dateStart;
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
            /// <date>2014/12/24 上午 10:27:09</date>
            #endregion
            whereTRANS_DTE = null;
            wherePAY_TYPE = null;
            whereBU = null;
            whereACCT_NBR = null;
            wherePRODUCT = null;
            whereCARD_PRODUCT = null;
            whereCURRENCY = null;
            wherePAY_CARD_NBR = null;
            whereEXPIR_DTE = null;
            whereCUST_SEQ = null;
            wherePAY_NBR = null;
            wherePAY_AMT = -1000000000000;
            wherePAY_DTE = dateStart;
            wherePAY_SEQ = null;
            wherePAY_RESULT = null;
            whereAUTH_CODE = null;
            whereERROR_REASON = null;
            whereREVERSAL_FLAG = null;
            whereCTL_CODE = null;
            whereAUTH_RESP = null;
            whereCHANGE_NBR_NEW = null;
            whereFILE_TRANSFER_TYPE = null;
            wherePAY_CARD_NBR_ORI = null;
            wherePAY_ACCT_NBR_ORI = null;
            wherePUBLIC_HIST_FIELD_1 = null;
            wherePUBLIC_HIST_FIELD_2 = null;
            wherePUBLIC_HIST_FIELD_3 = null;
            wherePUBLIC_HIST_FIELD_4 = null;
            wherePUBLIC_HIST_FIELD_5 = null;
            wherePUBLIC_HIST_AMT_1 = -1000000000000;
            wherePUBLIC_HIST_AMT_2 = -1000000000000;
            wherePUBLIC_HIST_AMT_3 = -1000000000000;
            wherePUBLIC_HIST_AMT_4 = -1000000000000;
            wherePUBLIC_HIST_AMT_5 = -1000000000000;
            wherePUBLIC_HIST_DT_1 = dateStart;
            wherePUBLIC_HIST_DT_2 = dateStart;
            wherePUBLIC_HIST_DT_3 = dateStart;
            wherePUBLIC_HIST_DT_4 = dateStart;
            wherePUBLIC_HIST_DT_5 = dateStart;
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
            /// <date>2014/12/24 上午 10:27:09</date>
            #endregion
            TRANS_DTE = initTRANS_DTE;
            PAY_TYPE = initPAY_TYPE;
            BU = initBU;
            ACCT_NBR = initACCT_NBR;
            PRODUCT = initPRODUCT;
            CARD_PRODUCT = initCARD_PRODUCT;
            CURRENCY = initCURRENCY;
            PAY_CARD_NBR = initPAY_CARD_NBR;
            EXPIR_DTE = initEXPIR_DTE;
            CUST_SEQ = initCUST_SEQ;
            PAY_NBR = initPAY_NBR;
            PAY_AMT = initPAY_AMT;
            PAY_DTE = initPAY_DTE;
            PAY_SEQ = initPAY_SEQ;
            PAY_RESULT = initPAY_RESULT;
            AUTH_CODE = initAUTH_CODE;
            ERROR_REASON = initERROR_REASON;
            REVERSAL_FLAG = initREVERSAL_FLAG;
            CTL_CODE = initCTL_CODE;
            AUTH_RESP = initAUTH_RESP;
            CHANGE_NBR_NEW = initCHANGE_NBR_NEW;
            FILE_TRANSFER_TYPE = initFILE_TRANSFER_TYPE;
            PAY_CARD_NBR_ORI = initPAY_CARD_NBR_ORI;
            PAY_ACCT_NBR_ORI = initPAY_ACCT_NBR_ORI;
            PUBLIC_HIST_FIELD_1 = initPUBLIC_HIST_FIELD_1;
            PUBLIC_HIST_FIELD_2 = initPUBLIC_HIST_FIELD_2;
            PUBLIC_HIST_FIELD_3 = initPUBLIC_HIST_FIELD_3;
            PUBLIC_HIST_FIELD_4 = initPUBLIC_HIST_FIELD_4;
            PUBLIC_HIST_FIELD_5 = initPUBLIC_HIST_FIELD_5;
            PUBLIC_HIST_AMT_1 = initPUBLIC_HIST_AMT_1;
            PUBLIC_HIST_AMT_2 = initPUBLIC_HIST_AMT_2;
            PUBLIC_HIST_AMT_3 = initPUBLIC_HIST_AMT_3;
            PUBLIC_HIST_AMT_4 = initPUBLIC_HIST_AMT_4;
            PUBLIC_HIST_AMT_5 = initPUBLIC_HIST_AMT_5;
            PUBLIC_HIST_DT_1 = initPUBLIC_HIST_DT_1;
            PUBLIC_HIST_DT_2 = initPUBLIC_HIST_DT_2;
            PUBLIC_HIST_DT_3 = initPUBLIC_HIST_DT_3;
            PUBLIC_HIST_DT_4 = initPUBLIC_HIST_DT_4;
            PUBLIC_HIST_DT_5 = initPUBLIC_HIST_DT_5;
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
            /// <date>2014/12/24 上午 10:27:09</date>
            #endregion
            try
            {
                //建立 DataRow物件
                DataRow DR = myTable.NewRow();
                //欄位搬初始值迴圈
                DR["TRANS_DTE"] = initTRANS_DTE;
                DR["PAY_TYPE"] = initPAY_TYPE;
                DR["BU"] = initBU;
                DR["ACCT_NBR"] = initACCT_NBR;
                DR["PRODUCT"] = initPRODUCT;
                DR["CARD_PRODUCT"] = initCARD_PRODUCT;
                DR["CURRENCY"] = initCURRENCY;
                DR["PAY_CARD_NBR"] = initPAY_CARD_NBR;
                DR["EXPIR_DTE"] = initEXPIR_DTE;
                DR["CUST_SEQ"] = initCUST_SEQ;
                DR["PAY_NBR"] = initPAY_NBR;
                DR["PAY_AMT"] = initPAY_AMT;
                DR["PAY_DTE"] = initPAY_DTE;
                DR["PAY_SEQ"] = initPAY_SEQ;
                DR["PAY_RESULT"] = initPAY_RESULT;
                DR["AUTH_CODE"] = initAUTH_CODE;
                DR["ERROR_REASON"] = initERROR_REASON;
                DR["REVERSAL_FLAG"] = initREVERSAL_FLAG;
                DR["CTL_CODE"] = initCTL_CODE;
                DR["AUTH_RESP"] = initAUTH_RESP;
                DR["CHANGE_NBR_NEW"] = initCHANGE_NBR_NEW;
                DR["FILE_TRANSFER_TYPE"] = initFILE_TRANSFER_TYPE;
                DR["PAY_CARD_NBR_ORI"] = initPAY_CARD_NBR_ORI;
                DR["PAY_ACCT_NBR_ORI"] = initPAY_ACCT_NBR_ORI;
                DR["PUBLIC_HIST_FIELD_1"] = initPUBLIC_HIST_FIELD_1;
                DR["PUBLIC_HIST_FIELD_2"] = initPUBLIC_HIST_FIELD_2;
                DR["PUBLIC_HIST_FIELD_3"] = initPUBLIC_HIST_FIELD_3;
                DR["PUBLIC_HIST_FIELD_4"] = initPUBLIC_HIST_FIELD_4;
                DR["PUBLIC_HIST_FIELD_5"] = initPUBLIC_HIST_FIELD_5;
                DR["PUBLIC_HIST_AMT_1"] = initPUBLIC_HIST_AMT_1;
                DR["PUBLIC_HIST_AMT_2"] = initPUBLIC_HIST_AMT_2;
                DR["PUBLIC_HIST_AMT_3"] = initPUBLIC_HIST_AMT_3;
                DR["PUBLIC_HIST_AMT_4"] = initPUBLIC_HIST_AMT_4;
                DR["PUBLIC_HIST_AMT_5"] = initPUBLIC_HIST_AMT_5;
                DR["PUBLIC_HIST_DT_1"] = initPUBLIC_HIST_DT_1;
                DR["PUBLIC_HIST_DT_2"] = initPUBLIC_HIST_DT_2;
                DR["PUBLIC_HIST_DT_3"] = initPUBLIC_HIST_DT_3;
                DR["PUBLIC_HIST_DT_4"] = initPUBLIC_HIST_DT_4;
                DR["PUBLIC_HIST_DT_5"] = initPUBLIC_HIST_DT_5;
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
            /// <date>2014/12/24 上午 10:27:09</date>
            #endregion
            try
            {
                //建立 Table物件
                myTable = new DataTable();
                //建立 Table欄位
                myTable.Columns.Add("TRANS_DTE", typeof(string));
                myTable.Columns.Add("PAY_TYPE", typeof(string));
                myTable.Columns.Add("BU", typeof(string));
                myTable.Columns.Add("ACCT_NBR", typeof(string));
                myTable.Columns.Add("PRODUCT", typeof(string));
                myTable.Columns.Add("CARD_PRODUCT", typeof(string));
                myTable.Columns.Add("CURRENCY", typeof(string));
                myTable.Columns.Add("PAY_CARD_NBR", typeof(string));
                myTable.Columns.Add("EXPIR_DTE", typeof(string));
                myTable.Columns.Add("CUST_SEQ", typeof(string));
                myTable.Columns.Add("PAY_NBR", typeof(string));
                myTable.Columns.Add("PAY_AMT", typeof(decimal));
                myTable.Columns.Add("PAY_DTE", typeof(DateTime));
                myTable.Columns.Add("PAY_SEQ", typeof(string));
                myTable.Columns.Add("PAY_RESULT", typeof(string));
                myTable.Columns.Add("AUTH_CODE", typeof(string));
                myTable.Columns.Add("ERROR_REASON", typeof(string));
                myTable.Columns.Add("REVERSAL_FLAG", typeof(string));
                myTable.Columns.Add("CTL_CODE", typeof(string));
                myTable.Columns.Add("AUTH_RESP", typeof(string));
                myTable.Columns.Add("CHANGE_NBR_NEW", typeof(string));
                myTable.Columns.Add("FILE_TRANSFER_TYPE", typeof(string));
                myTable.Columns.Add("PAY_CARD_NBR_ORI", typeof(string));
                myTable.Columns.Add("PAY_ACCT_NBR_ORI", typeof(string));
                myTable.Columns.Add("PUBLIC_HIST_FIELD_1", typeof(string));
                myTable.Columns.Add("PUBLIC_HIST_FIELD_2", typeof(string));
                myTable.Columns.Add("PUBLIC_HIST_FIELD_3", typeof(string));
                myTable.Columns.Add("PUBLIC_HIST_FIELD_4", typeof(string));
                myTable.Columns.Add("PUBLIC_HIST_FIELD_5", typeof(string));
                myTable.Columns.Add("PUBLIC_HIST_AMT_1", typeof(decimal));
                myTable.Columns.Add("PUBLIC_HIST_AMT_2", typeof(decimal));
                myTable.Columns.Add("PUBLIC_HIST_AMT_3", typeof(decimal));
                myTable.Columns.Add("PUBLIC_HIST_AMT_4", typeof(decimal));
                myTable.Columns.Add("PUBLIC_HIST_AMT_5", typeof(decimal));
                myTable.Columns.Add("PUBLIC_HIST_DT_1", typeof(DateTime));
                myTable.Columns.Add("PUBLIC_HIST_DT_2", typeof(DateTime));
                myTable.Columns.Add("PUBLIC_HIST_DT_3", typeof(DateTime));
                myTable.Columns.Add("PUBLIC_HIST_DT_4", typeof(DateTime));
                myTable.Columns.Add("PUBLIC_HIST_DT_5", typeof(DateTime));
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
            /// <date>2014/12/24 上午 10:27:09</date>
            #endregion
            try
            {
                int rowCount = Cybersoft.Data.DAL.Common.BatchInsert(myTable, "PUBLIC_HIST");
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
            /// <date>2014/12/24 上午 10:27:09</date>
            #endregion
            try
            {
                #region set SQL statement
                StringBuilder sbstrSQL = new StringBuilder();
                sbstrSQL.Append("SELECT a.TRANS_DTE,a.PAY_TYPE,a.BU,a.ACCT_NBR,a.PRODUCT,a.CARD_PRODUCT,a.CURRENCY,a.PAY_CARD_NBR,a.EXPIR_DTE,a.CUST_SEQ,a.PAY_NBR,a.PAY_AMT,a.PAY_DTE,a.PAY_SEQ,a.PAY_RESULT,a.AUTH_CODE,a.ERROR_REASON,a.REVERSAL_FLAG,a.CTL_CODE,a.AUTH_RESP,a.CHANGE_NBR_NEW,a.FILE_TRANSFER_TYPE,a.PAY_CARD_NBR_ORI,a.PAY_ACCT_NBR_ORI,a.PUBLIC_HIST_FIELD_1,a.PUBLIC_HIST_FIELD_2,a.PUBLIC_HIST_FIELD_3,a.PUBLIC_HIST_FIELD_4,a.PUBLIC_HIST_FIELD_5,a.PUBLIC_HIST_AMT_1,a.PUBLIC_HIST_AMT_2,a.PUBLIC_HIST_AMT_3,a.PUBLIC_HIST_AMT_4,a.PUBLIC_HIST_AMT_5,a.PUBLIC_HIST_DT_1,a.PUBLIC_HIST_DT_2,a.PUBLIC_HIST_DT_3,a.PUBLIC_HIST_DT_4,a.PUBLIC_HIST_DT_5,a.MNT_DT,a.MNT_USER FROM PUBLIC_HIST a where 1=1 ");
                if (this.whereTRANS_DTE != null)
                {
                    sbstrSQL.Append(" and a.TRANS_DTE=@whereTRANS_DTE ");
                }
                if (this.wherePAY_TYPE != null)
                {
                    sbstrSQL.Append(" and a.PAY_TYPE=@wherePAY_TYPE ");
                }
                if (this.whereBU != null)
                {
                    sbstrSQL.Append(" and a.BU=@whereBU ");
                }
                if (this.whereACCT_NBR != null)
                {
                    sbstrSQL.Append(" and a.ACCT_NBR=@whereACCT_NBR ");
                }
                if (this.wherePRODUCT != null)
                {
                    sbstrSQL.Append(" and a.PRODUCT=@wherePRODUCT ");
                }
                if (this.whereCARD_PRODUCT != null)
                {
                    sbstrSQL.Append(" and a.CARD_PRODUCT=@whereCARD_PRODUCT ");
                }
                if (this.whereCURRENCY != null)
                {
                    sbstrSQL.Append(" and a.CURRENCY=@whereCURRENCY ");
                }
                if (this.wherePAY_CARD_NBR != null)
                {
                    sbstrSQL.Append(" and a.PAY_CARD_NBR=@wherePAY_CARD_NBR ");
                }
                if (this.whereEXPIR_DTE != null)
                {
                    sbstrSQL.Append(" and a.EXPIR_DTE=@whereEXPIR_DTE ");
                }
                if (this.whereCUST_SEQ != null)
                {
                    sbstrSQL.Append(" and a.CUST_SEQ=@whereCUST_SEQ ");
                }
                if (this.wherePAY_NBR != null)
                {
                    sbstrSQL.Append(" and a.PAY_NBR=@wherePAY_NBR ");
                }
                if (this.wherePAY_AMT > -1000000000000)
                {
                    sbstrSQL.Append(" and a.PAY_AMT=@wherePAY_AMT ");
                }
                if (this.wherePAY_DTE > dateStart)
                {
                    sbstrSQL.Append("  and a.PAY_DTE=@wherePAY_DTE ");
                }
                if (this.wherePAY_SEQ != null)
                {
                    sbstrSQL.Append(" and a.PAY_SEQ=@wherePAY_SEQ ");
                }
                if (this.wherePAY_RESULT != null)
                {
                    sbstrSQL.Append(" and a.PAY_RESULT=@wherePAY_RESULT ");
                }
                if (this.whereAUTH_CODE != null)
                {
                    sbstrSQL.Append(" and a.AUTH_CODE=@whereAUTH_CODE ");
                }
                if (this.whereERROR_REASON != null)
                {
                    sbstrSQL.Append(" and a.ERROR_REASON=@whereERROR_REASON ");
                }
                if (this.whereREVERSAL_FLAG != null)
                {
                    sbstrSQL.Append(" and a.REVERSAL_FLAG=@whereREVERSAL_FLAG ");
                }
                if (this.whereCTL_CODE != null)
                {
                    sbstrSQL.Append(" and a.CTL_CODE=@whereCTL_CODE ");
                }
                if (this.whereAUTH_RESP != null)
                {
                    sbstrSQL.Append(" and a.AUTH_RESP=@whereAUTH_RESP ");
                }
                if (this.whereCHANGE_NBR_NEW != null)
                {
                    sbstrSQL.Append(" and a.CHANGE_NBR_NEW=@whereCHANGE_NBR_NEW ");
                }
                if (this.whereFILE_TRANSFER_TYPE != null)
                {
                    sbstrSQL.Append(" and a.FILE_TRANSFER_TYPE=@whereFILE_TRANSFER_TYPE ");
                }
                if (this.wherePAY_CARD_NBR_ORI != null)
                {
                    sbstrSQL.Append(" and a.PAY_CARD_NBR_ORI=@wherePAY_CARD_NBR_ORI ");
                }
                if (this.wherePAY_ACCT_NBR_ORI != null)
                {
                    sbstrSQL.Append(" and a.PAY_ACCT_NBR_ORI=@wherePAY_ACCT_NBR_ORI ");
                }
                if (this.wherePUBLIC_HIST_FIELD_1 != null)
                {
                    sbstrSQL.Append(" and a.PUBLIC_HIST_FIELD_1=@wherePUBLIC_HIST_FIELD_1 ");
                }
                if (this.wherePUBLIC_HIST_FIELD_2 != null)
                {
                    sbstrSQL.Append(" and a.PUBLIC_HIST_FIELD_2=@wherePUBLIC_HIST_FIELD_2 ");
                }
                if (this.wherePUBLIC_HIST_FIELD_3 != null)
                {
                    sbstrSQL.Append(" and a.PUBLIC_HIST_FIELD_3=@wherePUBLIC_HIST_FIELD_3 ");
                }
                if (this.wherePUBLIC_HIST_FIELD_4 != null)
                {
                    sbstrSQL.Append(" and a.PUBLIC_HIST_FIELD_4=@wherePUBLIC_HIST_FIELD_4 ");
                }
                if (this.wherePUBLIC_HIST_FIELD_5 != null)
                {
                    sbstrSQL.Append(" and a.PUBLIC_HIST_FIELD_5=@wherePUBLIC_HIST_FIELD_5 ");
                }
                if (this.wherePUBLIC_HIST_AMT_1 > -1000000000000)
                {
                    sbstrSQL.Append(" and a.PUBLIC_HIST_AMT_1=@wherePUBLIC_HIST_AMT_1 ");
                }
                if (this.wherePUBLIC_HIST_AMT_2 > -1000000000000)
                {
                    sbstrSQL.Append(" and a.PUBLIC_HIST_AMT_2=@wherePUBLIC_HIST_AMT_2 ");
                }
                if (this.wherePUBLIC_HIST_AMT_3 > -1000000000000)
                {
                    sbstrSQL.Append(" and a.PUBLIC_HIST_AMT_3=@wherePUBLIC_HIST_AMT_3 ");
                }
                if (this.wherePUBLIC_HIST_AMT_4 > -1000000000000)
                {
                    sbstrSQL.Append(" and a.PUBLIC_HIST_AMT_4=@wherePUBLIC_HIST_AMT_4 ");
                }
                if (this.wherePUBLIC_HIST_AMT_5 > -1000000000000)
                {
                    sbstrSQL.Append(" and a.PUBLIC_HIST_AMT_5=@wherePUBLIC_HIST_AMT_5 ");
                }
                if (this.wherePUBLIC_HIST_DT_1 > dateStart)
                {
                    sbstrSQL.Append("  and a.PUBLIC_HIST_DT_1=@wherePUBLIC_HIST_DT_1 ");
                }
                if (this.wherePUBLIC_HIST_DT_2 > dateStart)
                {
                    sbstrSQL.Append("  and a.PUBLIC_HIST_DT_2=@wherePUBLIC_HIST_DT_2 ");
                }
                if (this.wherePUBLIC_HIST_DT_3 > dateStart)
                {
                    sbstrSQL.Append("  and a.PUBLIC_HIST_DT_3=@wherePUBLIC_HIST_DT_3 ");
                }
                if (this.wherePUBLIC_HIST_DT_4 > dateStart)
                {
                    sbstrSQL.Append("  and a.PUBLIC_HIST_DT_4=@wherePUBLIC_HIST_DT_4 ");
                }
                if (this.wherePUBLIC_HIST_DT_5 > dateStart)
                {
                    sbstrSQL.Append("  and a.PUBLIC_HIST_DT_5=@wherePUBLIC_HIST_DT_5 ");
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
                if (sbstrSQL.ToString().Contains("@whereTRANS_DTE "))
                {
                    this.SelectOperator.SetValue("@whereTRANS_DTE", this.whereTRANS_DTE, SqlDbType.VarChar);
                }
                if (sbstrSQL.ToString().Contains("@wherePAY_TYPE "))
                {
                    this.SelectOperator.SetValue("@wherePAY_TYPE", this.wherePAY_TYPE, SqlDbType.VarChar);
                }
                if (sbstrSQL.ToString().Contains("@whereBU "))
                {
                    this.SelectOperator.SetValue("@whereBU", this.whereBU, SqlDbType.VarChar);
                }
                if (sbstrSQL.ToString().Contains("@whereACCT_NBR "))
                {
                    this.SelectOperator.SetValue("@whereACCT_NBR", this.whereACCT_NBR, SqlDbType.VarChar);
                }
                if (sbstrSQL.ToString().Contains("@wherePRODUCT "))
                {
                    this.SelectOperator.SetValue("@wherePRODUCT", this.wherePRODUCT, SqlDbType.VarChar);
                }
                if (sbstrSQL.ToString().Contains("@whereCARD_PRODUCT "))
                {
                    this.SelectOperator.SetValue("@whereCARD_PRODUCT", this.whereCARD_PRODUCT, SqlDbType.VarChar);
                }
                if (sbstrSQL.ToString().Contains("@whereCURRENCY "))
                {
                    this.SelectOperator.SetValue("@whereCURRENCY", this.whereCURRENCY, SqlDbType.VarChar);
                }
                if (sbstrSQL.ToString().Contains("@wherePAY_CARD_NBR "))
                {
                    this.SelectOperator.SetValue("@wherePAY_CARD_NBR", this.wherePAY_CARD_NBR, SqlDbType.VarChar);
                }
                if (sbstrSQL.ToString().Contains("@whereEXPIR_DTE "))
                {
                    this.SelectOperator.SetValue("@whereEXPIR_DTE", this.whereEXPIR_DTE, SqlDbType.VarChar);
                }
                if (sbstrSQL.ToString().Contains("@whereCUST_SEQ "))
                {
                    this.SelectOperator.SetValue("@whereCUST_SEQ", this.whereCUST_SEQ, SqlDbType.VarChar);
                }
                if (sbstrSQL.ToString().Contains("@wherePAY_NBR "))
                {
                    this.SelectOperator.SetValue("@wherePAY_NBR", this.wherePAY_NBR, SqlDbType.VarChar);
                }
                if (sbstrSQL.ToString().Contains("@wherePAY_AMT "))
                {
                    this.SelectOperator.SetValue("@wherePAY_AMT", this.wherePAY_AMT, SqlDbType.Decimal);
                }
                if (sbstrSQL.ToString().Contains("@wherePAY_DTE "))
                {
                    this.SelectOperator.SetValue("@wherePAY_DTE", this.wherePAY_DTE, SqlDbType.DateTime);
                }
                if (sbstrSQL.ToString().Contains("@wherePAY_SEQ "))
                {
                    this.SelectOperator.SetValue("@wherePAY_SEQ", this.wherePAY_SEQ, SqlDbType.VarChar);
                }
                if (sbstrSQL.ToString().Contains("@wherePAY_RESULT "))
                {
                    this.SelectOperator.SetValue("@wherePAY_RESULT", this.wherePAY_RESULT, SqlDbType.VarChar);
                }
                if (sbstrSQL.ToString().Contains("@whereAUTH_CODE "))
                {
                    this.SelectOperator.SetValue("@whereAUTH_CODE", this.whereAUTH_CODE, SqlDbType.VarChar);
                }
                if (sbstrSQL.ToString().Contains("@whereERROR_REASON "))
                {
                    this.SelectOperator.SetValue("@whereERROR_REASON", this.whereERROR_REASON, SqlDbType.VarChar);
                }
                if (sbstrSQL.ToString().Contains("@whereREVERSAL_FLAG "))
                {
                    this.SelectOperator.SetValue("@whereREVERSAL_FLAG", this.whereREVERSAL_FLAG, SqlDbType.VarChar);
                }
                if (sbstrSQL.ToString().Contains("@whereCTL_CODE "))
                {
                    this.SelectOperator.SetValue("@whereCTL_CODE", this.whereCTL_CODE, SqlDbType.VarChar);
                }
                if (sbstrSQL.ToString().Contains("@whereAUTH_RESP "))
                {
                    this.SelectOperator.SetValue("@whereAUTH_RESP", this.whereAUTH_RESP, SqlDbType.VarChar);
                }
                if (sbstrSQL.ToString().Contains("@whereCHANGE_NBR_NEW "))
                {
                    this.SelectOperator.SetValue("@whereCHANGE_NBR_NEW", this.whereCHANGE_NBR_NEW, SqlDbType.VarChar);
                }
                if (sbstrSQL.ToString().Contains("@whereFILE_TRANSFER_TYPE "))
                {
                    this.SelectOperator.SetValue("@whereFILE_TRANSFER_TYPE", this.whereFILE_TRANSFER_TYPE, SqlDbType.VarChar);
                }
                if (sbstrSQL.ToString().Contains("@wherePAY_CARD_NBR_ORI "))
                {
                    this.SelectOperator.SetValue("@wherePAY_CARD_NBR_ORI", this.wherePAY_CARD_NBR_ORI, SqlDbType.VarChar);
                }
                if (sbstrSQL.ToString().Contains("@wherePAY_ACCT_NBR_ORI "))
                {
                    this.SelectOperator.SetValue("@wherePAY_ACCT_NBR_ORI", this.wherePAY_ACCT_NBR_ORI, SqlDbType.VarChar);
                }
                if (sbstrSQL.ToString().Contains("@wherePUBLIC_HIST_FIELD_1 "))
                {
                    this.SelectOperator.SetValue("@wherePUBLIC_HIST_FIELD_1", this.wherePUBLIC_HIST_FIELD_1, SqlDbType.VarChar);
                }
                if (sbstrSQL.ToString().Contains("@wherePUBLIC_HIST_FIELD_2 "))
                {
                    this.SelectOperator.SetValue("@wherePUBLIC_HIST_FIELD_2", this.wherePUBLIC_HIST_FIELD_2, SqlDbType.VarChar);
                }
                if (sbstrSQL.ToString().Contains("@wherePUBLIC_HIST_FIELD_3 "))
                {
                    this.SelectOperator.SetValue("@wherePUBLIC_HIST_FIELD_3", this.wherePUBLIC_HIST_FIELD_3, SqlDbType.VarChar);
                }
                if (sbstrSQL.ToString().Contains("@wherePUBLIC_HIST_FIELD_4 "))
                {
                    this.SelectOperator.SetValue("@wherePUBLIC_HIST_FIELD_4", this.wherePUBLIC_HIST_FIELD_4, SqlDbType.VarChar);
                }
                if (sbstrSQL.ToString().Contains("@wherePUBLIC_HIST_FIELD_5 "))
                {
                    this.SelectOperator.SetValue("@wherePUBLIC_HIST_FIELD_5", this.wherePUBLIC_HIST_FIELD_5, SqlDbType.VarChar);
                }
                if (sbstrSQL.ToString().Contains("@wherePUBLIC_HIST_AMT_1 "))
                {
                    this.SelectOperator.SetValue("@wherePUBLIC_HIST_AMT_1", this.wherePUBLIC_HIST_AMT_1, SqlDbType.Decimal);
                }
                if (sbstrSQL.ToString().Contains("@wherePUBLIC_HIST_AMT_2 "))
                {
                    this.SelectOperator.SetValue("@wherePUBLIC_HIST_AMT_2", this.wherePUBLIC_HIST_AMT_2, SqlDbType.Decimal);
                }
                if (sbstrSQL.ToString().Contains("@wherePUBLIC_HIST_AMT_3 "))
                {
                    this.SelectOperator.SetValue("@wherePUBLIC_HIST_AMT_3", this.wherePUBLIC_HIST_AMT_3, SqlDbType.Decimal);
                }
                if (sbstrSQL.ToString().Contains("@wherePUBLIC_HIST_AMT_4 "))
                {
                    this.SelectOperator.SetValue("@wherePUBLIC_HIST_AMT_4", this.wherePUBLIC_HIST_AMT_4, SqlDbType.Decimal);
                }
                if (sbstrSQL.ToString().Contains("@wherePUBLIC_HIST_AMT_5 "))
                {
                    this.SelectOperator.SetValue("@wherePUBLIC_HIST_AMT_5", this.wherePUBLIC_HIST_AMT_5, SqlDbType.Decimal);
                }
                if (sbstrSQL.ToString().Contains("@wherePUBLIC_HIST_DT_1 "))
                {
                    this.SelectOperator.SetValue("@wherePUBLIC_HIST_DT_1", this.wherePUBLIC_HIST_DT_1, SqlDbType.DateTime);
                }
                if (sbstrSQL.ToString().Contains("@wherePUBLIC_HIST_DT_2 "))
                {
                    this.SelectOperator.SetValue("@wherePUBLIC_HIST_DT_2", this.wherePUBLIC_HIST_DT_2, SqlDbType.DateTime);
                }
                if (sbstrSQL.ToString().Contains("@wherePUBLIC_HIST_DT_3 "))
                {
                    this.SelectOperator.SetValue("@wherePUBLIC_HIST_DT_3", this.wherePUBLIC_HIST_DT_3, SqlDbType.DateTime);
                }
                if (sbstrSQL.ToString().Contains("@wherePUBLIC_HIST_DT_4 "))
                {
                    this.SelectOperator.SetValue("@wherePUBLIC_HIST_DT_4", this.wherePUBLIC_HIST_DT_4, SqlDbType.DateTime);
                }
                if (sbstrSQL.ToString().Contains("@wherePUBLIC_HIST_DT_5 "))
                {
                    this.SelectOperator.SetValue("@wherePUBLIC_HIST_DT_5", this.wherePUBLIC_HIST_DT_5, SqlDbType.DateTime);
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
                myTable = this.SelectOperator.GetDataTable("PUBLIC_HIST");
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
            /// <date>2014/12/24 上午 10:27:09</date>
            #endregion
            try
            {
                #region set SQL statement
                StringBuilder sbstrSQL = new StringBuilder();
                sbstrSQL.Append("INSERT INTO PUBLIC_HIST (TRANS_DTE,PAY_TYPE,BU,ACCT_NBR,PRODUCT,CARD_PRODUCT,CURRENCY,PAY_CARD_NBR,EXPIR_DTE,CUST_SEQ,PAY_NBR,PAY_AMT,PAY_DTE,PAY_SEQ,PAY_RESULT,AUTH_CODE,ERROR_REASON,REVERSAL_FLAG,CTL_CODE,AUTH_RESP,CHANGE_NBR_NEW,FILE_TRANSFER_TYPE,PAY_CARD_NBR_ORI,PAY_ACCT_NBR_ORI,PUBLIC_HIST_FIELD_1,PUBLIC_HIST_FIELD_2,PUBLIC_HIST_FIELD_3,PUBLIC_HIST_FIELD_4,PUBLIC_HIST_FIELD_5,PUBLIC_HIST_AMT_1,PUBLIC_HIST_AMT_2,PUBLIC_HIST_AMT_3,PUBLIC_HIST_AMT_4,PUBLIC_HIST_AMT_5,PUBLIC_HIST_DT_1,PUBLIC_HIST_DT_2,PUBLIC_HIST_DT_3,PUBLIC_HIST_DT_4,PUBLIC_HIST_DT_5,MNT_DT,MNT_USER) VALUES (@TRANS_DTE ,@PAY_TYPE ,@BU ,@ACCT_NBR ,@PRODUCT ,@CARD_PRODUCT ,@CURRENCY ,@PAY_CARD_NBR ,@EXPIR_DTE ,@CUST_SEQ ,@PAY_NBR ,@PAY_AMT ,@PAY_DTE ,@PAY_SEQ ,@PAY_RESULT ,@AUTH_CODE ,@ERROR_REASON ,@REVERSAL_FLAG ,@CTL_CODE ,@AUTH_RESP ,@CHANGE_NBR_NEW ,@FILE_TRANSFER_TYPE ,@PAY_CARD_NBR_ORI ,@PAY_ACCT_NBR_ORI ,@PUBLIC_HIST_FIELD_1 ,@PUBLIC_HIST_FIELD_2 ,@PUBLIC_HIST_FIELD_3 ,@PUBLIC_HIST_FIELD_4 ,@PUBLIC_HIST_FIELD_5 ,@PUBLIC_HIST_AMT_1 ,@PUBLIC_HIST_AMT_2 ,@PUBLIC_HIST_AMT_3 ,@PUBLIC_HIST_AMT_4 ,@PUBLIC_HIST_AMT_5 ,@PUBLIC_HIST_DT_1 ,@PUBLIC_HIST_DT_2 ,@PUBLIC_HIST_DT_3 ,@PUBLIC_HIST_DT_4 ,@PUBLIC_HIST_DT_5 ,@MNT_DT ,@MNT_USER )");
                #endregion
                this.InsertOperator.SqlText = sbstrSQL.ToString();
                #region replace SQL parameter
                this.InsertOperator.SetValue("@TRANS_DTE", this.TRANS_DTE, SqlDbType.VarChar);
                this.InsertOperator.SetValue("@PAY_TYPE", this.PAY_TYPE, SqlDbType.VarChar);
                this.InsertOperator.SetValue("@BU", this.BU, SqlDbType.VarChar);
                this.InsertOperator.SetValue("@ACCT_NBR", this.ACCT_NBR, SqlDbType.VarChar);
                this.InsertOperator.SetValue("@PRODUCT", this.PRODUCT, SqlDbType.VarChar);
                this.InsertOperator.SetValue("@CARD_PRODUCT", this.CARD_PRODUCT, SqlDbType.VarChar);
                this.InsertOperator.SetValue("@CURRENCY", this.CURRENCY, SqlDbType.VarChar);
                this.InsertOperator.SetValue("@PAY_CARD_NBR", this.PAY_CARD_NBR, SqlDbType.VarChar);
                this.InsertOperator.SetValue("@EXPIR_DTE", this.EXPIR_DTE, SqlDbType.VarChar);
                this.InsertOperator.SetValue("@CUST_SEQ", this.CUST_SEQ, SqlDbType.VarChar);
                this.InsertOperator.SetValue("@PAY_NBR", this.PAY_NBR, SqlDbType.VarChar);
                this.InsertOperator.SetValue("@PAY_AMT", this.PAY_AMT, SqlDbType.Decimal);
                this.InsertOperator.SetValue("@PAY_DTE", this.PAY_DTE, SqlDbType.DateTime);
                this.InsertOperator.SetValue("@PAY_SEQ", this.PAY_SEQ, SqlDbType.VarChar);
                this.InsertOperator.SetValue("@PAY_RESULT", this.PAY_RESULT, SqlDbType.VarChar);
                this.InsertOperator.SetValue("@AUTH_CODE", this.AUTH_CODE, SqlDbType.VarChar);
                this.InsertOperator.SetValue("@ERROR_REASON", this.ERROR_REASON, SqlDbType.VarChar);
                this.InsertOperator.SetValue("@REVERSAL_FLAG", this.REVERSAL_FLAG, SqlDbType.VarChar);
                this.InsertOperator.SetValue("@CTL_CODE", this.CTL_CODE, SqlDbType.VarChar);
                this.InsertOperator.SetValue("@AUTH_RESP", this.AUTH_RESP, SqlDbType.VarChar);
                this.InsertOperator.SetValue("@CHANGE_NBR_NEW", this.CHANGE_NBR_NEW, SqlDbType.VarChar);
                this.InsertOperator.SetValue("@FILE_TRANSFER_TYPE", this.FILE_TRANSFER_TYPE, SqlDbType.VarChar);
                this.InsertOperator.SetValue("@PAY_CARD_NBR_ORI", this.PAY_CARD_NBR_ORI, SqlDbType.VarChar);
                this.InsertOperator.SetValue("@PAY_ACCT_NBR_ORI", this.PAY_ACCT_NBR_ORI, SqlDbType.VarChar);
                this.InsertOperator.SetValue("@PUBLIC_HIST_FIELD_1", this.PUBLIC_HIST_FIELD_1, SqlDbType.VarChar);
                this.InsertOperator.SetValue("@PUBLIC_HIST_FIELD_2", this.PUBLIC_HIST_FIELD_2, SqlDbType.VarChar);
                this.InsertOperator.SetValue("@PUBLIC_HIST_FIELD_3", this.PUBLIC_HIST_FIELD_3, SqlDbType.VarChar);
                this.InsertOperator.SetValue("@PUBLIC_HIST_FIELD_4", this.PUBLIC_HIST_FIELD_4, SqlDbType.VarChar);
                this.InsertOperator.SetValue("@PUBLIC_HIST_FIELD_5", this.PUBLIC_HIST_FIELD_5, SqlDbType.VarChar);
                this.InsertOperator.SetValue("@PUBLIC_HIST_AMT_1", this.PUBLIC_HIST_AMT_1, SqlDbType.Decimal);
                this.InsertOperator.SetValue("@PUBLIC_HIST_AMT_2", this.PUBLIC_HIST_AMT_2, SqlDbType.Decimal);
                this.InsertOperator.SetValue("@PUBLIC_HIST_AMT_3", this.PUBLIC_HIST_AMT_3, SqlDbType.Decimal);
                this.InsertOperator.SetValue("@PUBLIC_HIST_AMT_4", this.PUBLIC_HIST_AMT_4, SqlDbType.Decimal);
                this.InsertOperator.SetValue("@PUBLIC_HIST_AMT_5", this.PUBLIC_HIST_AMT_5, SqlDbType.Decimal);
                this.InsertOperator.SetValue("@PUBLIC_HIST_DT_1", this.PUBLIC_HIST_DT_1, SqlDbType.DateTime);
                this.InsertOperator.SetValue("@PUBLIC_HIST_DT_2", this.PUBLIC_HIST_DT_2, SqlDbType.DateTime);
                this.InsertOperator.SetValue("@PUBLIC_HIST_DT_3", this.PUBLIC_HIST_DT_3, SqlDbType.DateTime);
                this.InsertOperator.SetValue("@PUBLIC_HIST_DT_4", this.PUBLIC_HIST_DT_4, SqlDbType.DateTime);
                this.InsertOperator.SetValue("@PUBLIC_HIST_DT_5", this.PUBLIC_HIST_DT_5, SqlDbType.DateTime);
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
            /// <date>2014/12/24 上午 10:27:09</date>
            #endregion
            try
            {
                #region set SQL statement
                StringBuilder sbstrSQL = new StringBuilder();
                sbstrSQL.Append("UPDATE PUBLIC_HIST set ");                //update field
                if (this.TRANS_DTE != null)
                {
                    sbstrSQL.Append(" TRANS_DTE=@TRANS_DTE ,");
                }
                if (this.PAY_TYPE != null)
                {
                    sbstrSQL.Append(" PAY_TYPE=@PAY_TYPE ,");
                }
                if (this.BU != null)
                {
                    sbstrSQL.Append(" BU=@BU ,");
                }
                if (this.ACCT_NBR != null)
                {
                    sbstrSQL.Append(" ACCT_NBR=@ACCT_NBR ,");
                }
                if (this.PRODUCT != null)
                {
                    sbstrSQL.Append(" PRODUCT=@PRODUCT ,");
                }
                if (this.CARD_PRODUCT != null)
                {
                    sbstrSQL.Append(" CARD_PRODUCT=@CARD_PRODUCT ,");
                }
                if (this.CURRENCY != null)
                {
                    sbstrSQL.Append(" CURRENCY=@CURRENCY ,");
                }
                if (this.PAY_CARD_NBR != null)
                {
                    sbstrSQL.Append(" PAY_CARD_NBR=@PAY_CARD_NBR ,");
                }
                if (this.EXPIR_DTE != null)
                {
                    sbstrSQL.Append(" EXPIR_DTE=@EXPIR_DTE ,");
                }
                if (this.CUST_SEQ != null)
                {
                    sbstrSQL.Append(" CUST_SEQ=@CUST_SEQ ,");
                }
                if (this.PAY_NBR != null)
                {
                    sbstrSQL.Append(" PAY_NBR=@PAY_NBR ,");
                }
                if (this.PAY_AMT > -1000000000000)
                {
                    sbstrSQL.Append(" PAY_AMT=@PAY_AMT ,");
                }
                if (this.PAY_DTE > dateStart)
                {
                    if (this.PAY_DTE.ToString("yyyyMMdd") == "19000101")
                    {
                        sbstrSQL.Append(" PAY_DTE= '19000101' ,");
                    }
                    else
                    {
                        sbstrSQL.Append(" PAY_DTE=@PAY_DTE ,");
                    }
                }
                if (this.PAY_SEQ != null)
                {
                    sbstrSQL.Append(" PAY_SEQ=@PAY_SEQ ,");
                }
                if (this.PAY_RESULT != null)
                {
                    sbstrSQL.Append(" PAY_RESULT=@PAY_RESULT ,");
                }
                if (this.AUTH_CODE != null)
                {
                    sbstrSQL.Append(" AUTH_CODE=@AUTH_CODE ,");
                }
                if (this.ERROR_REASON != null)
                {
                    sbstrSQL.Append(" ERROR_REASON=@ERROR_REASON ,");
                }
                if (this.REVERSAL_FLAG != null)
                {
                    sbstrSQL.Append(" REVERSAL_FLAG=@REVERSAL_FLAG ,");
                }
                if (this.CTL_CODE != null)
                {
                    sbstrSQL.Append(" CTL_CODE=@CTL_CODE ,");
                }
                if (this.AUTH_RESP != null)
                {
                    sbstrSQL.Append(" AUTH_RESP=@AUTH_RESP ,");
                }
                if (this.CHANGE_NBR_NEW != null)
                {
                    sbstrSQL.Append(" CHANGE_NBR_NEW=@CHANGE_NBR_NEW ,");
                }
                if (this.FILE_TRANSFER_TYPE != null)
                {
                    sbstrSQL.Append(" FILE_TRANSFER_TYPE=@FILE_TRANSFER_TYPE ,");
                }
                if (this.PAY_CARD_NBR_ORI != null)
                {
                    sbstrSQL.Append(" PAY_CARD_NBR_ORI=@PAY_CARD_NBR_ORI ,");
                }
                if (this.PAY_ACCT_NBR_ORI != null)
                {
                    sbstrSQL.Append(" PAY_ACCT_NBR_ORI=@PAY_ACCT_NBR_ORI ,");
                }
                if (this.PUBLIC_HIST_FIELD_1 != null)
                {
                    sbstrSQL.Append(" PUBLIC_HIST_FIELD_1=@PUBLIC_HIST_FIELD_1 ,");
                }
                if (this.PUBLIC_HIST_FIELD_2 != null)
                {
                    sbstrSQL.Append(" PUBLIC_HIST_FIELD_2=@PUBLIC_HIST_FIELD_2 ,");
                }
                if (this.PUBLIC_HIST_FIELD_3 != null)
                {
                    sbstrSQL.Append(" PUBLIC_HIST_FIELD_3=@PUBLIC_HIST_FIELD_3 ,");
                }
                if (this.PUBLIC_HIST_FIELD_4 != null)
                {
                    sbstrSQL.Append(" PUBLIC_HIST_FIELD_4=@PUBLIC_HIST_FIELD_4 ,");
                }
                if (this.PUBLIC_HIST_FIELD_5 != null)
                {
                    sbstrSQL.Append(" PUBLIC_HIST_FIELD_5=@PUBLIC_HIST_FIELD_5 ,");
                }
                if (this.PUBLIC_HIST_AMT_1 > -1000000000000)
                {
                    sbstrSQL.Append(" PUBLIC_HIST_AMT_1=@PUBLIC_HIST_AMT_1 ,");
                }
                if (this.PUBLIC_HIST_AMT_2 > -1000000000000)
                {
                    sbstrSQL.Append(" PUBLIC_HIST_AMT_2=@PUBLIC_HIST_AMT_2 ,");
                }
                if (this.PUBLIC_HIST_AMT_3 > -1000000000000)
                {
                    sbstrSQL.Append(" PUBLIC_HIST_AMT_3=@PUBLIC_HIST_AMT_3 ,");
                }
                if (this.PUBLIC_HIST_AMT_4 > -1000000000000)
                {
                    sbstrSQL.Append(" PUBLIC_HIST_AMT_4=@PUBLIC_HIST_AMT_4 ,");
                }
                if (this.PUBLIC_HIST_AMT_5 > -1000000000000)
                {
                    sbstrSQL.Append(" PUBLIC_HIST_AMT_5=@PUBLIC_HIST_AMT_5 ,");
                }
                if (this.PUBLIC_HIST_DT_1 > dateStart)
                {
                    if (this.PUBLIC_HIST_DT_1.ToString("yyyyMMdd") == "19000101")
                    {
                        sbstrSQL.Append(" PUBLIC_HIST_DT_1= '19000101' ,");
                    }
                    else
                    {
                        sbstrSQL.Append(" PUBLIC_HIST_DT_1=@PUBLIC_HIST_DT_1 ,");
                    }
                }
                if (this.PUBLIC_HIST_DT_2 > dateStart)
                {
                    if (this.PUBLIC_HIST_DT_2.ToString("yyyyMMdd") == "19000101")
                    {
                        sbstrSQL.Append(" PUBLIC_HIST_DT_2= '19000101' ,");
                    }
                    else
                    {
                        sbstrSQL.Append(" PUBLIC_HIST_DT_2=@PUBLIC_HIST_DT_2 ,");
                    }
                }
                if (this.PUBLIC_HIST_DT_3 > dateStart)
                {
                    if (this.PUBLIC_HIST_DT_3.ToString("yyyyMMdd") == "19000101")
                    {
                        sbstrSQL.Append(" PUBLIC_HIST_DT_3= '19000101' ,");
                    }
                    else
                    {
                        sbstrSQL.Append(" PUBLIC_HIST_DT_3=@PUBLIC_HIST_DT_3 ,");
                    }
                }
                if (this.PUBLIC_HIST_DT_4 > dateStart)
                {
                    if (this.PUBLIC_HIST_DT_4.ToString("yyyyMMdd") == "19000101")
                    {
                        sbstrSQL.Append(" PUBLIC_HIST_DT_4= '19000101' ,");
                    }
                    else
                    {
                        sbstrSQL.Append(" PUBLIC_HIST_DT_4=@PUBLIC_HIST_DT_4 ,");
                    }
                }
                if (this.PUBLIC_HIST_DT_5 > dateStart)
                {
                    if (this.PUBLIC_HIST_DT_5.ToString("yyyyMMdd") == "19000101")
                    {
                        sbstrSQL.Append(" PUBLIC_HIST_DT_5= '19000101' ,");
                    }
                    else
                    {
                        sbstrSQL.Append(" PUBLIC_HIST_DT_5=@PUBLIC_HIST_DT_5 ,");
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
                if (this.whereTRANS_DTE != null)
                {
                    sbstrSQL.Append(" and TRANS_DTE=@whereTRANS_DTE ");
                }
                if (this.wherePAY_TYPE != null)
                {
                    sbstrSQL.Append(" and PAY_TYPE=@wherePAY_TYPE ");
                }
                if (this.whereBU != null)
                {
                    sbstrSQL.Append(" and BU=@whereBU ");
                }
                if (this.whereACCT_NBR != null)
                {
                    sbstrSQL.Append(" and ACCT_NBR=@whereACCT_NBR ");
                }
                if (this.wherePRODUCT != null)
                {
                    sbstrSQL.Append(" and PRODUCT=@wherePRODUCT ");
                }
                if (this.whereCARD_PRODUCT != null)
                {
                    sbstrSQL.Append(" and CARD_PRODUCT=@whereCARD_PRODUCT ");
                }
                if (this.whereCURRENCY != null)
                {
                    sbstrSQL.Append(" and CURRENCY=@whereCURRENCY ");
                }
                if (this.wherePAY_CARD_NBR != null)
                {
                    sbstrSQL.Append(" and PAY_CARD_NBR=@wherePAY_CARD_NBR ");
                }
                if (this.whereEXPIR_DTE != null)
                {
                    sbstrSQL.Append(" and EXPIR_DTE=@whereEXPIR_DTE ");
                }
                if (this.whereCUST_SEQ != null)
                {
                    sbstrSQL.Append(" and CUST_SEQ=@whereCUST_SEQ ");
                }
                if (this.wherePAY_NBR != null)
                {
                    sbstrSQL.Append(" and PAY_NBR=@wherePAY_NBR ");
                }
                if (this.wherePAY_AMT > -1000000000000)
                {
                    sbstrSQL.Append(" and PAY_AMT=@wherePAY_AMT ");
                }
                if (this.wherePAY_DTE > dateStart)
                {
                    sbstrSQL.Append(" and PAY_DTE=@wherePAY_DTE ");
                }
                if (this.wherePAY_SEQ != null)
                {
                    sbstrSQL.Append(" and PAY_SEQ=@wherePAY_SEQ ");
                }
                if (this.wherePAY_RESULT != null)
                {
                    sbstrSQL.Append(" and PAY_RESULT=@wherePAY_RESULT ");
                }
                if (this.whereAUTH_CODE != null)
                {
                    sbstrSQL.Append(" and AUTH_CODE=@whereAUTH_CODE ");
                }
                if (this.whereERROR_REASON != null)
                {
                    sbstrSQL.Append(" and ERROR_REASON=@whereERROR_REASON ");
                }
                if (this.whereREVERSAL_FLAG != null)
                {
                    sbstrSQL.Append(" and REVERSAL_FLAG=@whereREVERSAL_FLAG ");
                }
                if (this.whereCTL_CODE != null)
                {
                    sbstrSQL.Append(" and CTL_CODE=@whereCTL_CODE ");
                }
                if (this.whereAUTH_RESP != null)
                {
                    sbstrSQL.Append(" and AUTH_RESP=@whereAUTH_RESP ");
                }
                if (this.whereCHANGE_NBR_NEW != null)
                {
                    sbstrSQL.Append(" and CHANGE_NBR_NEW=@whereCHANGE_NBR_NEW ");
                }
                if (this.whereFILE_TRANSFER_TYPE != null)
                {
                    sbstrSQL.Append(" and FILE_TRANSFER_TYPE=@whereFILE_TRANSFER_TYPE ");
                }
                if (this.wherePAY_CARD_NBR_ORI != null)
                {
                    sbstrSQL.Append(" and PAY_CARD_NBR_ORI=@wherePAY_CARD_NBR_ORI ");
                }
                if (this.wherePAY_ACCT_NBR_ORI != null)
                {
                    sbstrSQL.Append(" and PAY_ACCT_NBR_ORI=@wherePAY_ACCT_NBR_ORI ");
                }
                if (this.wherePUBLIC_HIST_FIELD_1 != null)
                {
                    sbstrSQL.Append(" and PUBLIC_HIST_FIELD_1=@wherePUBLIC_HIST_FIELD_1 ");
                }
                if (this.wherePUBLIC_HIST_FIELD_2 != null)
                {
                    sbstrSQL.Append(" and PUBLIC_HIST_FIELD_2=@wherePUBLIC_HIST_FIELD_2 ");
                }
                if (this.wherePUBLIC_HIST_FIELD_3 != null)
                {
                    sbstrSQL.Append(" and PUBLIC_HIST_FIELD_3=@wherePUBLIC_HIST_FIELD_3 ");
                }
                if (this.wherePUBLIC_HIST_FIELD_4 != null)
                {
                    sbstrSQL.Append(" and PUBLIC_HIST_FIELD_4=@wherePUBLIC_HIST_FIELD_4 ");
                }
                if (this.wherePUBLIC_HIST_FIELD_5 != null)
                {
                    sbstrSQL.Append(" and PUBLIC_HIST_FIELD_5=@wherePUBLIC_HIST_FIELD_5 ");
                }
                if (this.wherePUBLIC_HIST_AMT_1 > -1000000000000)
                {
                    sbstrSQL.Append(" and PUBLIC_HIST_AMT_1=@wherePUBLIC_HIST_AMT_1 ");
                }
                if (this.wherePUBLIC_HIST_AMT_2 > -1000000000000)
                {
                    sbstrSQL.Append(" and PUBLIC_HIST_AMT_2=@wherePUBLIC_HIST_AMT_2 ");
                }
                if (this.wherePUBLIC_HIST_AMT_3 > -1000000000000)
                {
                    sbstrSQL.Append(" and PUBLIC_HIST_AMT_3=@wherePUBLIC_HIST_AMT_3 ");
                }
                if (this.wherePUBLIC_HIST_AMT_4 > -1000000000000)
                {
                    sbstrSQL.Append(" and PUBLIC_HIST_AMT_4=@wherePUBLIC_HIST_AMT_4 ");
                }
                if (this.wherePUBLIC_HIST_AMT_5 > -1000000000000)
                {
                    sbstrSQL.Append(" and PUBLIC_HIST_AMT_5=@wherePUBLIC_HIST_AMT_5 ");
                }
                if (this.wherePUBLIC_HIST_DT_1 > dateStart)
                {
                    sbstrSQL.Append(" and PUBLIC_HIST_DT_1=@wherePUBLIC_HIST_DT_1 ");
                }
                if (this.wherePUBLIC_HIST_DT_2 > dateStart)
                {
                    sbstrSQL.Append(" and PUBLIC_HIST_DT_2=@wherePUBLIC_HIST_DT_2 ");
                }
                if (this.wherePUBLIC_HIST_DT_3 > dateStart)
                {
                    sbstrSQL.Append(" and PUBLIC_HIST_DT_3=@wherePUBLIC_HIST_DT_3 ");
                }
                if (this.wherePUBLIC_HIST_DT_4 > dateStart)
                {
                    sbstrSQL.Append(" and PUBLIC_HIST_DT_4=@wherePUBLIC_HIST_DT_4 ");
                }
                if (this.wherePUBLIC_HIST_DT_5 > dateStart)
                {
                    sbstrSQL.Append(" and PUBLIC_HIST_DT_5=@wherePUBLIC_HIST_DT_5 ");
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
                if (sbstrSQL.ToString().Contains("@TRANS_DTE "))
                {
                    this.UpdateOperator.SetValue("@TRANS_DTE", this.TRANS_DTE, SqlDbType.VarChar);
                }
                if (sbstrSQL.ToString().Contains("@PAY_TYPE "))
                {
                    this.UpdateOperator.SetValue("@PAY_TYPE", this.PAY_TYPE, SqlDbType.VarChar);
                }
                if (sbstrSQL.ToString().Contains("@BU "))
                {
                    this.UpdateOperator.SetValue("@BU", this.BU, SqlDbType.VarChar);
                }
                if (sbstrSQL.ToString().Contains("@ACCT_NBR "))
                {
                    this.UpdateOperator.SetValue("@ACCT_NBR", this.ACCT_NBR, SqlDbType.VarChar);
                }
                if (sbstrSQL.ToString().Contains("@PRODUCT "))
                {
                    this.UpdateOperator.SetValue("@PRODUCT", this.PRODUCT, SqlDbType.VarChar);
                }
                if (sbstrSQL.ToString().Contains("@CARD_PRODUCT "))
                {
                    this.UpdateOperator.SetValue("@CARD_PRODUCT", this.CARD_PRODUCT, SqlDbType.VarChar);
                }
                if (sbstrSQL.ToString().Contains("@CURRENCY "))
                {
                    this.UpdateOperator.SetValue("@CURRENCY", this.CURRENCY, SqlDbType.VarChar);
                }
                if (sbstrSQL.ToString().Contains("@PAY_CARD_NBR "))
                {
                    this.UpdateOperator.SetValue("@PAY_CARD_NBR", this.PAY_CARD_NBR, SqlDbType.VarChar);
                }
                if (sbstrSQL.ToString().Contains("@EXPIR_DTE "))
                {
                    this.UpdateOperator.SetValue("@EXPIR_DTE", this.EXPIR_DTE, SqlDbType.VarChar);
                }
                if (sbstrSQL.ToString().Contains("@CUST_SEQ "))
                {
                    this.UpdateOperator.SetValue("@CUST_SEQ", this.CUST_SEQ, SqlDbType.VarChar);
                }
                if (sbstrSQL.ToString().Contains("@PAY_NBR "))
                {
                    this.UpdateOperator.SetValue("@PAY_NBR", this.PAY_NBR, SqlDbType.VarChar);
                }
                if (sbstrSQL.ToString().Contains("@PAY_AMT "))
                {
                    this.UpdateOperator.SetValue("@PAY_AMT", this.PAY_AMT, SqlDbType.Decimal);
                }
                if (sbstrSQL.ToString().Contains("@PAY_DTE "))
                {
                    this.UpdateOperator.SetValue("@PAY_DTE", this.PAY_DTE, SqlDbType.DateTime);
                }
                if (sbstrSQL.ToString().Contains("@PAY_SEQ "))
                {
                    this.UpdateOperator.SetValue("@PAY_SEQ", this.PAY_SEQ, SqlDbType.VarChar);
                }
                if (sbstrSQL.ToString().Contains("@PAY_RESULT "))
                {
                    this.UpdateOperator.SetValue("@PAY_RESULT", this.PAY_RESULT, SqlDbType.VarChar);
                }
                if (sbstrSQL.ToString().Contains("@AUTH_CODE "))
                {
                    this.UpdateOperator.SetValue("@AUTH_CODE", this.AUTH_CODE, SqlDbType.VarChar);
                }
                if (sbstrSQL.ToString().Contains("@ERROR_REASON "))
                {
                    this.UpdateOperator.SetValue("@ERROR_REASON", this.ERROR_REASON, SqlDbType.VarChar);
                }
                if (sbstrSQL.ToString().Contains("@REVERSAL_FLAG "))
                {
                    this.UpdateOperator.SetValue("@REVERSAL_FLAG", this.REVERSAL_FLAG, SqlDbType.VarChar);
                }
                if (sbstrSQL.ToString().Contains("@CTL_CODE "))
                {
                    this.UpdateOperator.SetValue("@CTL_CODE", this.CTL_CODE, SqlDbType.VarChar);
                }
                if (sbstrSQL.ToString().Contains("@AUTH_RESP "))
                {
                    this.UpdateOperator.SetValue("@AUTH_RESP", this.AUTH_RESP, SqlDbType.VarChar);
                }
                if (sbstrSQL.ToString().Contains("@CHANGE_NBR_NEW "))
                {
                    this.UpdateOperator.SetValue("@CHANGE_NBR_NEW", this.CHANGE_NBR_NEW, SqlDbType.VarChar);
                }
                if (sbstrSQL.ToString().Contains("@FILE_TRANSFER_TYPE "))
                {
                    this.UpdateOperator.SetValue("@FILE_TRANSFER_TYPE", this.FILE_TRANSFER_TYPE, SqlDbType.VarChar);
                }
                if (sbstrSQL.ToString().Contains("@PAY_CARD_NBR_ORI "))
                {
                    this.UpdateOperator.SetValue("@PAY_CARD_NBR_ORI", this.PAY_CARD_NBR_ORI, SqlDbType.VarChar);
                }
                if (sbstrSQL.ToString().Contains("@PAY_ACCT_NBR_ORI "))
                {
                    this.UpdateOperator.SetValue("@PAY_ACCT_NBR_ORI", this.PAY_ACCT_NBR_ORI, SqlDbType.VarChar);
                }
                if (sbstrSQL.ToString().Contains("@PUBLIC_HIST_FIELD_1 "))
                {
                    this.UpdateOperator.SetValue("@PUBLIC_HIST_FIELD_1", this.PUBLIC_HIST_FIELD_1, SqlDbType.VarChar);
                }
                if (sbstrSQL.ToString().Contains("@PUBLIC_HIST_FIELD_2 "))
                {
                    this.UpdateOperator.SetValue("@PUBLIC_HIST_FIELD_2", this.PUBLIC_HIST_FIELD_2, SqlDbType.VarChar);
                }
                if (sbstrSQL.ToString().Contains("@PUBLIC_HIST_FIELD_3 "))
                {
                    this.UpdateOperator.SetValue("@PUBLIC_HIST_FIELD_3", this.PUBLIC_HIST_FIELD_3, SqlDbType.VarChar);
                }
                if (sbstrSQL.ToString().Contains("@PUBLIC_HIST_FIELD_4 "))
                {
                    this.UpdateOperator.SetValue("@PUBLIC_HIST_FIELD_4", this.PUBLIC_HIST_FIELD_4, SqlDbType.VarChar);
                }
                if (sbstrSQL.ToString().Contains("@PUBLIC_HIST_FIELD_5 "))
                {
                    this.UpdateOperator.SetValue("@PUBLIC_HIST_FIELD_5", this.PUBLIC_HIST_FIELD_5, SqlDbType.VarChar);
                }
                if (sbstrSQL.ToString().Contains("@PUBLIC_HIST_AMT_1 "))
                {
                    this.UpdateOperator.SetValue("@PUBLIC_HIST_AMT_1", this.PUBLIC_HIST_AMT_1, SqlDbType.Decimal);
                }
                if (sbstrSQL.ToString().Contains("@PUBLIC_HIST_AMT_2 "))
                {
                    this.UpdateOperator.SetValue("@PUBLIC_HIST_AMT_2", this.PUBLIC_HIST_AMT_2, SqlDbType.Decimal);
                }
                if (sbstrSQL.ToString().Contains("@PUBLIC_HIST_AMT_3 "))
                {
                    this.UpdateOperator.SetValue("@PUBLIC_HIST_AMT_3", this.PUBLIC_HIST_AMT_3, SqlDbType.Decimal);
                }
                if (sbstrSQL.ToString().Contains("@PUBLIC_HIST_AMT_4 "))
                {
                    this.UpdateOperator.SetValue("@PUBLIC_HIST_AMT_4", this.PUBLIC_HIST_AMT_4, SqlDbType.Decimal);
                }
                if (sbstrSQL.ToString().Contains("@PUBLIC_HIST_AMT_5 "))
                {
                    this.UpdateOperator.SetValue("@PUBLIC_HIST_AMT_5", this.PUBLIC_HIST_AMT_5, SqlDbType.Decimal);
                }
                if (sbstrSQL.ToString().Contains("@PUBLIC_HIST_DT_1 "))
                {
                    this.UpdateOperator.SetValue("@PUBLIC_HIST_DT_1", this.PUBLIC_HIST_DT_1, SqlDbType.DateTime);
                }
                if (sbstrSQL.ToString().Contains("@PUBLIC_HIST_DT_2 "))
                {
                    this.UpdateOperator.SetValue("@PUBLIC_HIST_DT_2", this.PUBLIC_HIST_DT_2, SqlDbType.DateTime);
                }
                if (sbstrSQL.ToString().Contains("@PUBLIC_HIST_DT_3 "))
                {
                    this.UpdateOperator.SetValue("@PUBLIC_HIST_DT_3", this.PUBLIC_HIST_DT_3, SqlDbType.DateTime);
                }
                if (sbstrSQL.ToString().Contains("@PUBLIC_HIST_DT_4 "))
                {
                    this.UpdateOperator.SetValue("@PUBLIC_HIST_DT_4", this.PUBLIC_HIST_DT_4, SqlDbType.DateTime);
                }
                if (sbstrSQL.ToString().Contains("@PUBLIC_HIST_DT_5 "))
                {
                    this.UpdateOperator.SetValue("@PUBLIC_HIST_DT_5", this.PUBLIC_HIST_DT_5, SqlDbType.DateTime);
                }
                if (sbstrSQL.ToString().Contains("@MNT_DT "))
                {
                    this.UpdateOperator.SetValue("@MNT_DT", this.MNT_DT, SqlDbType.DateTime);
                }
                if (sbstrSQL.ToString().Contains("@MNT_USER "))
                {
                    this.UpdateOperator.SetValue("@MNT_USER", this.MNT_USER, SqlDbType.VarChar);
                }
                if (sbstrSQL.ToString().Contains("@whereTRANS_DTE "))
                {
                    this.UpdateOperator.SetValue("@whereTRANS_DTE", this.whereTRANS_DTE, SqlDbType.VarChar);
                }
                if (sbstrSQL.ToString().Contains("@wherePAY_TYPE "))
                {
                    this.UpdateOperator.SetValue("@wherePAY_TYPE", this.wherePAY_TYPE, SqlDbType.VarChar);
                }
                if (sbstrSQL.ToString().Contains("@whereBU "))
                {
                    this.UpdateOperator.SetValue("@whereBU", this.whereBU, SqlDbType.VarChar);
                }
                if (sbstrSQL.ToString().Contains("@whereACCT_NBR "))
                {
                    this.UpdateOperator.SetValue("@whereACCT_NBR", this.whereACCT_NBR, SqlDbType.VarChar);
                }
                if (sbstrSQL.ToString().Contains("@wherePRODUCT "))
                {
                    this.UpdateOperator.SetValue("@wherePRODUCT", this.wherePRODUCT, SqlDbType.VarChar);
                }
                if (sbstrSQL.ToString().Contains("@whereCARD_PRODUCT "))
                {
                    this.UpdateOperator.SetValue("@whereCARD_PRODUCT", this.whereCARD_PRODUCT, SqlDbType.VarChar);
                }
                if (sbstrSQL.ToString().Contains("@whereCURRENCY "))
                {
                    this.UpdateOperator.SetValue("@whereCURRENCY", this.whereCURRENCY, SqlDbType.VarChar);
                }
                if (sbstrSQL.ToString().Contains("@wherePAY_CARD_NBR "))
                {
                    this.UpdateOperator.SetValue("@wherePAY_CARD_NBR", this.wherePAY_CARD_NBR, SqlDbType.VarChar);
                }
                if (sbstrSQL.ToString().Contains("@whereEXPIR_DTE "))
                {
                    this.UpdateOperator.SetValue("@whereEXPIR_DTE", this.whereEXPIR_DTE, SqlDbType.VarChar);
                }
                if (sbstrSQL.ToString().Contains("@whereCUST_SEQ "))
                {
                    this.UpdateOperator.SetValue("@whereCUST_SEQ", this.whereCUST_SEQ, SqlDbType.VarChar);
                }
                if (sbstrSQL.ToString().Contains("@wherePAY_NBR "))
                {
                    this.UpdateOperator.SetValue("@wherePAY_NBR", this.wherePAY_NBR, SqlDbType.VarChar);
                }
                if (sbstrSQL.ToString().Contains("@wherePAY_AMT "))
                {
                    this.UpdateOperator.SetValue("@wherePAY_AMT", this.wherePAY_AMT, SqlDbType.Decimal);
                }
                if (sbstrSQL.ToString().Contains("@wherePAY_DTE "))
                {
                    this.UpdateOperator.SetValue("@wherePAY_DTE", this.wherePAY_DTE, SqlDbType.DateTime);
                }
                if (sbstrSQL.ToString().Contains("@wherePAY_SEQ "))
                {
                    this.UpdateOperator.SetValue("@wherePAY_SEQ", this.wherePAY_SEQ, SqlDbType.VarChar);
                }
                if (sbstrSQL.ToString().Contains("@wherePAY_RESULT "))
                {
                    this.UpdateOperator.SetValue("@wherePAY_RESULT", this.wherePAY_RESULT, SqlDbType.VarChar);
                }
                if (sbstrSQL.ToString().Contains("@whereAUTH_CODE "))
                {
                    this.UpdateOperator.SetValue("@whereAUTH_CODE", this.whereAUTH_CODE, SqlDbType.VarChar);
                }
                if (sbstrSQL.ToString().Contains("@whereERROR_REASON "))
                {
                    this.UpdateOperator.SetValue("@whereERROR_REASON", this.whereERROR_REASON, SqlDbType.VarChar);
                }
                if (sbstrSQL.ToString().Contains("@whereREVERSAL_FLAG "))
                {
                    this.UpdateOperator.SetValue("@whereREVERSAL_FLAG", this.whereREVERSAL_FLAG, SqlDbType.VarChar);
                }
                if (sbstrSQL.ToString().Contains("@whereCTL_CODE "))
                {
                    this.UpdateOperator.SetValue("@whereCTL_CODE", this.whereCTL_CODE, SqlDbType.VarChar);
                }
                if (sbstrSQL.ToString().Contains("@whereAUTH_RESP "))
                {
                    this.UpdateOperator.SetValue("@whereAUTH_RESP", this.whereAUTH_RESP, SqlDbType.VarChar);
                }
                if (sbstrSQL.ToString().Contains("@whereCHANGE_NBR_NEW "))
                {
                    this.UpdateOperator.SetValue("@whereCHANGE_NBR_NEW", this.whereCHANGE_NBR_NEW, SqlDbType.VarChar);
                }
                if (sbstrSQL.ToString().Contains("@whereFILE_TRANSFER_TYPE "))
                {
                    this.UpdateOperator.SetValue("@whereFILE_TRANSFER_TYPE", this.whereFILE_TRANSFER_TYPE, SqlDbType.VarChar);
                }
                if (sbstrSQL.ToString().Contains("@wherePAY_CARD_NBR_ORI "))
                {
                    this.UpdateOperator.SetValue("@wherePAY_CARD_NBR_ORI", this.wherePAY_CARD_NBR_ORI, SqlDbType.VarChar);
                }
                if (sbstrSQL.ToString().Contains("@wherePAY_ACCT_NBR_ORI "))
                {
                    this.UpdateOperator.SetValue("@wherePAY_ACCT_NBR_ORI", this.wherePAY_ACCT_NBR_ORI, SqlDbType.VarChar);
                }
                if (sbstrSQL.ToString().Contains("@wherePUBLIC_HIST_FIELD_1 "))
                {
                    this.UpdateOperator.SetValue("@wherePUBLIC_HIST_FIELD_1", this.wherePUBLIC_HIST_FIELD_1, SqlDbType.VarChar);
                }
                if (sbstrSQL.ToString().Contains("@wherePUBLIC_HIST_FIELD_2 "))
                {
                    this.UpdateOperator.SetValue("@wherePUBLIC_HIST_FIELD_2", this.wherePUBLIC_HIST_FIELD_2, SqlDbType.VarChar);
                }
                if (sbstrSQL.ToString().Contains("@wherePUBLIC_HIST_FIELD_3 "))
                {
                    this.UpdateOperator.SetValue("@wherePUBLIC_HIST_FIELD_3", this.wherePUBLIC_HIST_FIELD_3, SqlDbType.VarChar);
                }
                if (sbstrSQL.ToString().Contains("@wherePUBLIC_HIST_FIELD_4 "))
                {
                    this.UpdateOperator.SetValue("@wherePUBLIC_HIST_FIELD_4", this.wherePUBLIC_HIST_FIELD_4, SqlDbType.VarChar);
                }
                if (sbstrSQL.ToString().Contains("@wherePUBLIC_HIST_FIELD_5 "))
                {
                    this.UpdateOperator.SetValue("@wherePUBLIC_HIST_FIELD_5", this.wherePUBLIC_HIST_FIELD_5, SqlDbType.VarChar);
                }
                if (sbstrSQL.ToString().Contains("@wherePUBLIC_HIST_AMT_1 "))
                {
                    this.UpdateOperator.SetValue("@wherePUBLIC_HIST_AMT_1", this.wherePUBLIC_HIST_AMT_1, SqlDbType.Decimal);
                }
                if (sbstrSQL.ToString().Contains("@wherePUBLIC_HIST_AMT_2 "))
                {
                    this.UpdateOperator.SetValue("@wherePUBLIC_HIST_AMT_2", this.wherePUBLIC_HIST_AMT_2, SqlDbType.Decimal);
                }
                if (sbstrSQL.ToString().Contains("@wherePUBLIC_HIST_AMT_3 "))
                {
                    this.UpdateOperator.SetValue("@wherePUBLIC_HIST_AMT_3", this.wherePUBLIC_HIST_AMT_3, SqlDbType.Decimal);
                }
                if (sbstrSQL.ToString().Contains("@wherePUBLIC_HIST_AMT_4 "))
                {
                    this.UpdateOperator.SetValue("@wherePUBLIC_HIST_AMT_4", this.wherePUBLIC_HIST_AMT_4, SqlDbType.Decimal);
                }
                if (sbstrSQL.ToString().Contains("@wherePUBLIC_HIST_AMT_5 "))
                {
                    this.UpdateOperator.SetValue("@wherePUBLIC_HIST_AMT_5", this.wherePUBLIC_HIST_AMT_5, SqlDbType.Decimal);
                }
                if (sbstrSQL.ToString().Contains("@wherePUBLIC_HIST_DT_1 "))
                {
                    this.UpdateOperator.SetValue("@wherePUBLIC_HIST_DT_1", this.wherePUBLIC_HIST_DT_1, SqlDbType.DateTime);
                }
                if (sbstrSQL.ToString().Contains("@wherePUBLIC_HIST_DT_2 "))
                {
                    this.UpdateOperator.SetValue("@wherePUBLIC_HIST_DT_2", this.wherePUBLIC_HIST_DT_2, SqlDbType.DateTime);
                }
                if (sbstrSQL.ToString().Contains("@wherePUBLIC_HIST_DT_3 "))
                {
                    this.UpdateOperator.SetValue("@wherePUBLIC_HIST_DT_3", this.wherePUBLIC_HIST_DT_3, SqlDbType.DateTime);
                }
                if (sbstrSQL.ToString().Contains("@wherePUBLIC_HIST_DT_4 "))
                {
                    this.UpdateOperator.SetValue("@wherePUBLIC_HIST_DT_4", this.wherePUBLIC_HIST_DT_4, SqlDbType.DateTime);
                }
                if (sbstrSQL.ToString().Contains("@wherePUBLIC_HIST_DT_5 "))
                {
                    this.UpdateOperator.SetValue("@wherePUBLIC_HIST_DT_5", this.wherePUBLIC_HIST_DT_5, SqlDbType.DateTime);
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
            /// <date>2014/12/24 上午 10:27:09</date>
            #endregion
            try
            {
                #region set SQL statement
                StringBuilder sbstrSQL = new StringBuilder();
                sbstrSQL.Append(" DELETE PUBLIC_HIST where 1=1 ");
                if (this.whereTRANS_DTE != null)
                {
                    sbstrSQL.Append(" and TRANS_DTE=@whereTRANS_DTE ");
                }
                if (this.wherePAY_TYPE != null)
                {
                    sbstrSQL.Append(" and PAY_TYPE=@wherePAY_TYPE ");
                }
                if (this.whereBU != null)
                {
                    sbstrSQL.Append(" and BU=@whereBU ");
                }
                if (this.whereACCT_NBR != null)
                {
                    sbstrSQL.Append(" and ACCT_NBR=@whereACCT_NBR ");
                }
                if (this.wherePRODUCT != null)
                {
                    sbstrSQL.Append(" and PRODUCT=@wherePRODUCT ");
                }
                if (this.whereCARD_PRODUCT != null)
                {
                    sbstrSQL.Append(" and CARD_PRODUCT=@whereCARD_PRODUCT ");
                }
                if (this.whereCURRENCY != null)
                {
                    sbstrSQL.Append(" and CURRENCY=@whereCURRENCY ");
                }
                if (this.wherePAY_CARD_NBR != null)
                {
                    sbstrSQL.Append(" and PAY_CARD_NBR=@wherePAY_CARD_NBR ");
                }
                if (this.whereEXPIR_DTE != null)
                {
                    sbstrSQL.Append(" and EXPIR_DTE=@whereEXPIR_DTE ");
                }
                if (this.whereCUST_SEQ != null)
                {
                    sbstrSQL.Append(" and CUST_SEQ=@whereCUST_SEQ ");
                }
                if (this.wherePAY_NBR != null)
                {
                    sbstrSQL.Append(" and PAY_NBR=@wherePAY_NBR ");
                }
                if (this.wherePAY_AMT > -1000000000000)
                {
                    sbstrSQL.Append(" and PAY_AMT=@wherePAY_AMT ");
                }
                if (this.wherePAY_DTE > dateStart)
                {
                    sbstrSQL.Append(" and PAY_DTE=@wherePAY_DTE ");
                }
                if (this.wherePAY_SEQ != null)
                {
                    sbstrSQL.Append(" and PAY_SEQ=@wherePAY_SEQ ");
                }
                if (this.wherePAY_RESULT != null)
                {
                    sbstrSQL.Append(" and PAY_RESULT=@wherePAY_RESULT ");
                }
                if (this.whereAUTH_CODE != null)
                {
                    sbstrSQL.Append(" and AUTH_CODE=@whereAUTH_CODE ");
                }
                if (this.whereERROR_REASON != null)
                {
                    sbstrSQL.Append(" and ERROR_REASON=@whereERROR_REASON ");
                }
                if (this.whereREVERSAL_FLAG != null)
                {
                    sbstrSQL.Append(" and REVERSAL_FLAG=@whereREVERSAL_FLAG ");
                }
                if (this.whereCTL_CODE != null)
                {
                    sbstrSQL.Append(" and CTL_CODE=@whereCTL_CODE ");
                }
                if (this.whereAUTH_RESP != null)
                {
                    sbstrSQL.Append(" and AUTH_RESP=@whereAUTH_RESP ");
                }
                if (this.whereCHANGE_NBR_NEW != null)
                {
                    sbstrSQL.Append(" and CHANGE_NBR_NEW=@whereCHANGE_NBR_NEW ");
                }
                if (this.whereFILE_TRANSFER_TYPE != null)
                {
                    sbstrSQL.Append(" and FILE_TRANSFER_TYPE=@whereFILE_TRANSFER_TYPE ");
                }
                if (this.wherePAY_CARD_NBR_ORI != null)
                {
                    sbstrSQL.Append(" and PAY_CARD_NBR_ORI=@wherePAY_CARD_NBR_ORI ");
                }
                if (this.wherePAY_ACCT_NBR_ORI != null)
                {
                    sbstrSQL.Append(" and PAY_ACCT_NBR_ORI=@wherePAY_ACCT_NBR_ORI ");
                }
                if (this.wherePUBLIC_HIST_FIELD_1 != null)
                {
                    sbstrSQL.Append(" and PUBLIC_HIST_FIELD_1=@wherePUBLIC_HIST_FIELD_1 ");
                }
                if (this.wherePUBLIC_HIST_FIELD_2 != null)
                {
                    sbstrSQL.Append(" and PUBLIC_HIST_FIELD_2=@wherePUBLIC_HIST_FIELD_2 ");
                }
                if (this.wherePUBLIC_HIST_FIELD_3 != null)
                {
                    sbstrSQL.Append(" and PUBLIC_HIST_FIELD_3=@wherePUBLIC_HIST_FIELD_3 ");
                }
                if (this.wherePUBLIC_HIST_FIELD_4 != null)
                {
                    sbstrSQL.Append(" and PUBLIC_HIST_FIELD_4=@wherePUBLIC_HIST_FIELD_4 ");
                }
                if (this.wherePUBLIC_HIST_FIELD_5 != null)
                {
                    sbstrSQL.Append(" and PUBLIC_HIST_FIELD_5=@wherePUBLIC_HIST_FIELD_5 ");
                }
                if (this.wherePUBLIC_HIST_AMT_1 > -1000000000000)
                {
                    sbstrSQL.Append(" and PUBLIC_HIST_AMT_1=@wherePUBLIC_HIST_AMT_1 ");
                }
                if (this.wherePUBLIC_HIST_AMT_2 > -1000000000000)
                {
                    sbstrSQL.Append(" and PUBLIC_HIST_AMT_2=@wherePUBLIC_HIST_AMT_2 ");
                }
                if (this.wherePUBLIC_HIST_AMT_3 > -1000000000000)
                {
                    sbstrSQL.Append(" and PUBLIC_HIST_AMT_3=@wherePUBLIC_HIST_AMT_3 ");
                }
                if (this.wherePUBLIC_HIST_AMT_4 > -1000000000000)
                {
                    sbstrSQL.Append(" and PUBLIC_HIST_AMT_4=@wherePUBLIC_HIST_AMT_4 ");
                }
                if (this.wherePUBLIC_HIST_AMT_5 > -1000000000000)
                {
                    sbstrSQL.Append(" and PUBLIC_HIST_AMT_5=@wherePUBLIC_HIST_AMT_5 ");
                }
                if (this.wherePUBLIC_HIST_DT_1 > dateStart)
                {
                    sbstrSQL.Append(" and PUBLIC_HIST_DT_1=@wherePUBLIC_HIST_DT_1 ");
                }
                if (this.wherePUBLIC_HIST_DT_2 > dateStart)
                {
                    sbstrSQL.Append(" and PUBLIC_HIST_DT_2=@wherePUBLIC_HIST_DT_2 ");
                }
                if (this.wherePUBLIC_HIST_DT_3 > dateStart)
                {
                    sbstrSQL.Append(" and PUBLIC_HIST_DT_3=@wherePUBLIC_HIST_DT_3 ");
                }
                if (this.wherePUBLIC_HIST_DT_4 > dateStart)
                {
                    sbstrSQL.Append(" and PUBLIC_HIST_DT_4=@wherePUBLIC_HIST_DT_4 ");
                }
                if (this.wherePUBLIC_HIST_DT_5 > dateStart)
                {
                    sbstrSQL.Append(" and PUBLIC_HIST_DT_5=@wherePUBLIC_HIST_DT_5 ");
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
                if (sbstrSQL.ToString().Contains("@whereTRANS_DTE "))
                {
                    this.DeleteOperator.SetValue("@whereTRANS_DTE", this.whereTRANS_DTE, SqlDbType.VarChar);
                }
                if (sbstrSQL.ToString().Contains("@wherePAY_TYPE "))
                {
                    this.DeleteOperator.SetValue("@wherePAY_TYPE", this.wherePAY_TYPE, SqlDbType.VarChar);
                }
                if (sbstrSQL.ToString().Contains("@whereBU "))
                {
                    this.DeleteOperator.SetValue("@whereBU", this.whereBU, SqlDbType.VarChar);
                }
                if (sbstrSQL.ToString().Contains("@whereACCT_NBR "))
                {
                    this.DeleteOperator.SetValue("@whereACCT_NBR", this.whereACCT_NBR, SqlDbType.VarChar);
                }
                if (sbstrSQL.ToString().Contains("@wherePRODUCT "))
                {
                    this.DeleteOperator.SetValue("@wherePRODUCT", this.wherePRODUCT, SqlDbType.VarChar);
                }
                if (sbstrSQL.ToString().Contains("@whereCARD_PRODUCT "))
                {
                    this.DeleteOperator.SetValue("@whereCARD_PRODUCT", this.whereCARD_PRODUCT, SqlDbType.VarChar);
                }
                if (sbstrSQL.ToString().Contains("@whereCURRENCY "))
                {
                    this.DeleteOperator.SetValue("@whereCURRENCY", this.whereCURRENCY, SqlDbType.VarChar);
                }
                if (sbstrSQL.ToString().Contains("@wherePAY_CARD_NBR "))
                {
                    this.DeleteOperator.SetValue("@wherePAY_CARD_NBR", this.wherePAY_CARD_NBR, SqlDbType.VarChar);
                }
                if (sbstrSQL.ToString().Contains("@whereEXPIR_DTE "))
                {
                    this.DeleteOperator.SetValue("@whereEXPIR_DTE", this.whereEXPIR_DTE, SqlDbType.VarChar);
                }
                if (sbstrSQL.ToString().Contains("@whereCUST_SEQ "))
                {
                    this.DeleteOperator.SetValue("@whereCUST_SEQ", this.whereCUST_SEQ, SqlDbType.VarChar);
                }
                if (sbstrSQL.ToString().Contains("@wherePAY_NBR "))
                {
                    this.DeleteOperator.SetValue("@wherePAY_NBR", this.wherePAY_NBR, SqlDbType.VarChar);
                }
                if (sbstrSQL.ToString().Contains("@wherePAY_AMT "))
                {
                    this.DeleteOperator.SetValue("@wherePAY_AMT", this.wherePAY_AMT, SqlDbType.Decimal);
                }
                if (sbstrSQL.ToString().Contains("@wherePAY_DTE "))
                {
                    this.DeleteOperator.SetValue("@wherePAY_DTE", this.wherePAY_DTE, SqlDbType.DateTime);
                }
                if (sbstrSQL.ToString().Contains("@wherePAY_SEQ "))
                {
                    this.DeleteOperator.SetValue("@wherePAY_SEQ", this.wherePAY_SEQ, SqlDbType.VarChar);
                }
                if (sbstrSQL.ToString().Contains("@wherePAY_RESULT "))
                {
                    this.DeleteOperator.SetValue("@wherePAY_RESULT", this.wherePAY_RESULT, SqlDbType.VarChar);
                }
                if (sbstrSQL.ToString().Contains("@whereAUTH_CODE "))
                {
                    this.DeleteOperator.SetValue("@whereAUTH_CODE", this.whereAUTH_CODE, SqlDbType.VarChar);
                }
                if (sbstrSQL.ToString().Contains("@whereERROR_REASON "))
                {
                    this.DeleteOperator.SetValue("@whereERROR_REASON", this.whereERROR_REASON, SqlDbType.VarChar);
                }
                if (sbstrSQL.ToString().Contains("@whereREVERSAL_FLAG "))
                {
                    this.DeleteOperator.SetValue("@whereREVERSAL_FLAG", this.whereREVERSAL_FLAG, SqlDbType.VarChar);
                }
                if (sbstrSQL.ToString().Contains("@whereCTL_CODE "))
                {
                    this.DeleteOperator.SetValue("@whereCTL_CODE", this.whereCTL_CODE, SqlDbType.VarChar);
                }
                if (sbstrSQL.ToString().Contains("@whereAUTH_RESP "))
                {
                    this.DeleteOperator.SetValue("@whereAUTH_RESP", this.whereAUTH_RESP, SqlDbType.VarChar);
                }
                if (sbstrSQL.ToString().Contains("@whereCHANGE_NBR_NEW "))
                {
                    this.DeleteOperator.SetValue("@whereCHANGE_NBR_NEW", this.whereCHANGE_NBR_NEW, SqlDbType.VarChar);
                }
                if (sbstrSQL.ToString().Contains("@whereFILE_TRANSFER_TYPE "))
                {
                    this.DeleteOperator.SetValue("@whereFILE_TRANSFER_TYPE", this.whereFILE_TRANSFER_TYPE, SqlDbType.VarChar);
                }
                if (sbstrSQL.ToString().Contains("@wherePAY_CARD_NBR_ORI "))
                {
                    this.DeleteOperator.SetValue("@wherePAY_CARD_NBR_ORI", this.wherePAY_CARD_NBR_ORI, SqlDbType.VarChar);
                }
                if (sbstrSQL.ToString().Contains("@wherePAY_ACCT_NBR_ORI "))
                {
                    this.DeleteOperator.SetValue("@wherePAY_ACCT_NBR_ORI", this.wherePAY_ACCT_NBR_ORI, SqlDbType.VarChar);
                }
                if (sbstrSQL.ToString().Contains("@wherePUBLIC_HIST_FIELD_1 "))
                {
                    this.DeleteOperator.SetValue("@wherePUBLIC_HIST_FIELD_1", this.wherePUBLIC_HIST_FIELD_1, SqlDbType.VarChar);
                }
                if (sbstrSQL.ToString().Contains("@wherePUBLIC_HIST_FIELD_2 "))
                {
                    this.DeleteOperator.SetValue("@wherePUBLIC_HIST_FIELD_2", this.wherePUBLIC_HIST_FIELD_2, SqlDbType.VarChar);
                }
                if (sbstrSQL.ToString().Contains("@wherePUBLIC_HIST_FIELD_3 "))
                {
                    this.DeleteOperator.SetValue("@wherePUBLIC_HIST_FIELD_3", this.wherePUBLIC_HIST_FIELD_3, SqlDbType.VarChar);
                }
                if (sbstrSQL.ToString().Contains("@wherePUBLIC_HIST_FIELD_4 "))
                {
                    this.DeleteOperator.SetValue("@wherePUBLIC_HIST_FIELD_4", this.wherePUBLIC_HIST_FIELD_4, SqlDbType.VarChar);
                }
                if (sbstrSQL.ToString().Contains("@wherePUBLIC_HIST_FIELD_5 "))
                {
                    this.DeleteOperator.SetValue("@wherePUBLIC_HIST_FIELD_5", this.wherePUBLIC_HIST_FIELD_5, SqlDbType.VarChar);
                }
                if (sbstrSQL.ToString().Contains("@wherePUBLIC_HIST_AMT_1 "))
                {
                    this.DeleteOperator.SetValue("@wherePUBLIC_HIST_AMT_1", this.wherePUBLIC_HIST_AMT_1, SqlDbType.Decimal);
                }
                if (sbstrSQL.ToString().Contains("@wherePUBLIC_HIST_AMT_2 "))
                {
                    this.DeleteOperator.SetValue("@wherePUBLIC_HIST_AMT_2", this.wherePUBLIC_HIST_AMT_2, SqlDbType.Decimal);
                }
                if (sbstrSQL.ToString().Contains("@wherePUBLIC_HIST_AMT_3 "))
                {
                    this.DeleteOperator.SetValue("@wherePUBLIC_HIST_AMT_3", this.wherePUBLIC_HIST_AMT_3, SqlDbType.Decimal);
                }
                if (sbstrSQL.ToString().Contains("@wherePUBLIC_HIST_AMT_4 "))
                {
                    this.DeleteOperator.SetValue("@wherePUBLIC_HIST_AMT_4", this.wherePUBLIC_HIST_AMT_4, SqlDbType.Decimal);
                }
                if (sbstrSQL.ToString().Contains("@wherePUBLIC_HIST_AMT_5 "))
                {
                    this.DeleteOperator.SetValue("@wherePUBLIC_HIST_AMT_5", this.wherePUBLIC_HIST_AMT_5, SqlDbType.Decimal);
                }
                if (sbstrSQL.ToString().Contains("@wherePUBLIC_HIST_DT_1 "))
                {
                    this.DeleteOperator.SetValue("@wherePUBLIC_HIST_DT_1", this.wherePUBLIC_HIST_DT_1, SqlDbType.DateTime);
                }
                if (sbstrSQL.ToString().Contains("@wherePUBLIC_HIST_DT_2 "))
                {
                    this.DeleteOperator.SetValue("@wherePUBLIC_HIST_DT_2", this.wherePUBLIC_HIST_DT_2, SqlDbType.DateTime);
                }
                if (sbstrSQL.ToString().Contains("@wherePUBLIC_HIST_DT_3 "))
                {
                    this.DeleteOperator.SetValue("@wherePUBLIC_HIST_DT_3", this.wherePUBLIC_HIST_DT_3, SqlDbType.DateTime);
                }
                if (sbstrSQL.ToString().Contains("@wherePUBLIC_HIST_DT_4 "))
                {
                    this.DeleteOperator.SetValue("@wherePUBLIC_HIST_DT_4", this.wherePUBLIC_HIST_DT_4, SqlDbType.DateTime);
                }
                if (sbstrSQL.ToString().Contains("@wherePUBLIC_HIST_DT_5 "))
                {
                    this.DeleteOperator.SetValue("@wherePUBLIC_HIST_DT_5", this.wherePUBLIC_HIST_DT_5, SqlDbType.DateTime);
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
            /// <date>2014/12/24 上午 10:27:09</date>
            #endregion
            try
            {
                #region 宣告MNT_TODAYDAO並放入初始值
                Cybersoft.Dao.Core.MNT_TODAYDao MNT_TODAY = new Cybersoft.Dao.Core.MNT_TODAYDao();
                MNT_TODAY.table_define();
                MNT_TODAY.strinitTBL_NAME = "PUBLIC_HIST";
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
                if (this.TRANS_DTE != null && this.TRANS_DTE != Convert.ToString(myTable.Rows[0]["TRANS_DTE"]))
                {
                    MNT_TODAY.initInsert_row();
                    MNT_TODAY.resultTable.Rows[MNT_Count]["FIELD_NAME"] = "TRANS_DTE";
                    MNT_TODAY.resultTable.Rows[MNT_Count]["VARCHAR_BEFORE"] = Convert.ToString(myTable.Rows[0]["TRANS_DTE"]);
                    MNT_TODAY.resultTable.Rows[MNT_Count]["VARCHAR_AFTER"] = this.TRANS_DTE;
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
                if (this.BU != null && this.BU != Convert.ToString(myTable.Rows[0]["BU"]))
                {
                    MNT_TODAY.initInsert_row();
                    MNT_TODAY.resultTable.Rows[MNT_Count]["FIELD_NAME"] = "BU";
                    MNT_TODAY.resultTable.Rows[MNT_Count]["VARCHAR_BEFORE"] = Convert.ToString(myTable.Rows[0]["BU"]);
                    MNT_TODAY.resultTable.Rows[MNT_Count]["VARCHAR_AFTER"] = this.BU;
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
                if (this.CURRENCY != null && this.CURRENCY != Convert.ToString(myTable.Rows[0]["CURRENCY"]))
                {
                    MNT_TODAY.initInsert_row();
                    MNT_TODAY.resultTable.Rows[MNT_Count]["FIELD_NAME"] = "CURRENCY";
                    MNT_TODAY.resultTable.Rows[MNT_Count]["VARCHAR_BEFORE"] = Convert.ToString(myTable.Rows[0]["CURRENCY"]);
                    MNT_TODAY.resultTable.Rows[MNT_Count]["VARCHAR_AFTER"] = this.CURRENCY;
                    MNT_Count++;
                }
                if (this.PAY_CARD_NBR != null && this.PAY_CARD_NBR != Convert.ToString(myTable.Rows[0]["PAY_CARD_NBR"]))
                {
                    MNT_TODAY.initInsert_row();
                    MNT_TODAY.resultTable.Rows[MNT_Count]["FIELD_NAME"] = "PAY_CARD_NBR";
                    MNT_TODAY.resultTable.Rows[MNT_Count]["VARCHAR_BEFORE"] = Convert.ToString(myTable.Rows[0]["PAY_CARD_NBR"]);
                    MNT_TODAY.resultTable.Rows[MNT_Count]["VARCHAR_AFTER"] = this.PAY_CARD_NBR;
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
                if (this.CUST_SEQ != null && this.CUST_SEQ != Convert.ToString(myTable.Rows[0]["CUST_SEQ"]))
                {
                    MNT_TODAY.initInsert_row();
                    MNT_TODAY.resultTable.Rows[MNT_Count]["FIELD_NAME"] = "CUST_SEQ";
                    MNT_TODAY.resultTable.Rows[MNT_Count]["VARCHAR_BEFORE"] = Convert.ToString(myTable.Rows[0]["CUST_SEQ"]);
                    MNT_TODAY.resultTable.Rows[MNT_Count]["VARCHAR_AFTER"] = this.CUST_SEQ;
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
                if (this.PAY_AMT > -1000000000000 && this.PAY_AMT != Convert.ToDecimal(myTable.Rows[0]["PAY_AMT"]))
                {
                    MNT_TODAY.initInsert_row();
                    MNT_TODAY.resultTable.Rows[MNT_Count]["FIELD_NAME"] = "PAY_AMT";
                    MNT_TODAY.resultTable.Rows[MNT_Count]["DECIMAL_BEFORE"] = Convert.ToDecimal(myTable.Rows[0]["PAY_AMT"]);
                    MNT_TODAY.resultTable.Rows[MNT_Count]["DECIMAL_AFTER"] = this.PAY_AMT;
                    MNT_Count++;
                }
                if (this.PAY_DTE > dateStart && this.PAY_DTE != Convert.ToDateTime(myTable.Rows[0]["PAY_DTE"]))
                {
                    if (this.PAY_DTE.ToString("yyyyMMdd") == "19000101" &&
                        Convert.ToDateTime(myTable.Rows[0]["PAY_DTE"]) == dateStart)
                    {
                    }
                    else
                    {
                        MNT_TODAY.initInsert_row();
                        MNT_TODAY.resultTable.Rows[MNT_Count]["FIELD_NAME"] = "PAY_DTE";
                        MNT_TODAY.resultTable.Rows[MNT_Count]["DT_BEFORE"] = Convert.ToDateTime(myTable.Rows[0]["PAY_DTE"]);
                        if (this.PAY_DTE.ToString("yyyyMMdd") == "19000101")
                        {
                            MNT_TODAY.resultTable.Rows[MNT_Count]["DT_AFTER"] = "1900-01-01";
                        }
                        else
                        {
                            MNT_TODAY.resultTable.Rows[MNT_Count]["DT_AFTER"] = this.PAY_DTE;
                        }
                        MNT_Count++;
                    }
                }
                if (this.PAY_SEQ != null && this.PAY_SEQ != Convert.ToString(myTable.Rows[0]["PAY_SEQ"]))
                {
                    MNT_TODAY.initInsert_row();
                    MNT_TODAY.resultTable.Rows[MNT_Count]["FIELD_NAME"] = "PAY_SEQ";
                    MNT_TODAY.resultTable.Rows[MNT_Count]["VARCHAR_BEFORE"] = Convert.ToString(myTable.Rows[0]["PAY_SEQ"]);
                    MNT_TODAY.resultTable.Rows[MNT_Count]["VARCHAR_AFTER"] = this.PAY_SEQ;
                    MNT_Count++;
                }
                if (this.PAY_RESULT != null && this.PAY_RESULT != Convert.ToString(myTable.Rows[0]["PAY_RESULT"]))
                {
                    MNT_TODAY.initInsert_row();
                    MNT_TODAY.resultTable.Rows[MNT_Count]["FIELD_NAME"] = "PAY_RESULT";
                    MNT_TODAY.resultTable.Rows[MNT_Count]["VARCHAR_BEFORE"] = Convert.ToString(myTable.Rows[0]["PAY_RESULT"]);
                    MNT_TODAY.resultTable.Rows[MNT_Count]["VARCHAR_AFTER"] = this.PAY_RESULT;
                    MNT_Count++;
                }
                if (this.AUTH_CODE != null && this.AUTH_CODE != Convert.ToString(myTable.Rows[0]["AUTH_CODE"]))
                {
                    MNT_TODAY.initInsert_row();
                    MNT_TODAY.resultTable.Rows[MNT_Count]["FIELD_NAME"] = "AUTH_CODE";
                    MNT_TODAY.resultTable.Rows[MNT_Count]["VARCHAR_BEFORE"] = Convert.ToString(myTable.Rows[0]["AUTH_CODE"]);
                    MNT_TODAY.resultTable.Rows[MNT_Count]["VARCHAR_AFTER"] = this.AUTH_CODE;
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
                if (this.REVERSAL_FLAG != null && this.REVERSAL_FLAG != Convert.ToString(myTable.Rows[0]["REVERSAL_FLAG"]))
                {
                    MNT_TODAY.initInsert_row();
                    MNT_TODAY.resultTable.Rows[MNT_Count]["FIELD_NAME"] = "REVERSAL_FLAG";
                    MNT_TODAY.resultTable.Rows[MNT_Count]["VARCHAR_BEFORE"] = Convert.ToString(myTable.Rows[0]["REVERSAL_FLAG"]);
                    MNT_TODAY.resultTable.Rows[MNT_Count]["VARCHAR_AFTER"] = this.REVERSAL_FLAG;
                    MNT_Count++;
                }
                if (this.CTL_CODE != null && this.CTL_CODE != Convert.ToString(myTable.Rows[0]["CTL_CODE"]))
                {
                    MNT_TODAY.initInsert_row();
                    MNT_TODAY.resultTable.Rows[MNT_Count]["FIELD_NAME"] = "CTL_CODE";
                    MNT_TODAY.resultTable.Rows[MNT_Count]["VARCHAR_BEFORE"] = Convert.ToString(myTable.Rows[0]["CTL_CODE"]);
                    MNT_TODAY.resultTable.Rows[MNT_Count]["VARCHAR_AFTER"] = this.CTL_CODE;
                    MNT_Count++;
                }
                if (this.AUTH_RESP != null && this.AUTH_RESP != Convert.ToString(myTable.Rows[0]["AUTH_RESP"]))
                {
                    MNT_TODAY.initInsert_row();
                    MNT_TODAY.resultTable.Rows[MNT_Count]["FIELD_NAME"] = "AUTH_RESP";
                    MNT_TODAY.resultTable.Rows[MNT_Count]["VARCHAR_BEFORE"] = Convert.ToString(myTable.Rows[0]["AUTH_RESP"]);
                    MNT_TODAY.resultTable.Rows[MNT_Count]["VARCHAR_AFTER"] = this.AUTH_RESP;
                    MNT_Count++;
                }
                if (this.CHANGE_NBR_NEW != null && this.CHANGE_NBR_NEW != Convert.ToString(myTable.Rows[0]["CHANGE_NBR_NEW"]))
                {
                    MNT_TODAY.initInsert_row();
                    MNT_TODAY.resultTable.Rows[MNT_Count]["FIELD_NAME"] = "CHANGE_NBR_NEW";
                    MNT_TODAY.resultTable.Rows[MNT_Count]["VARCHAR_BEFORE"] = Convert.ToString(myTable.Rows[0]["CHANGE_NBR_NEW"]);
                    MNT_TODAY.resultTable.Rows[MNT_Count]["VARCHAR_AFTER"] = this.CHANGE_NBR_NEW;
                    MNT_Count++;
                }
                if (this.FILE_TRANSFER_TYPE != null && this.FILE_TRANSFER_TYPE != Convert.ToString(myTable.Rows[0]["FILE_TRANSFER_TYPE"]))
                {
                    MNT_TODAY.initInsert_row();
                    MNT_TODAY.resultTable.Rows[MNT_Count]["FIELD_NAME"] = "FILE_TRANSFER_TYPE";
                    MNT_TODAY.resultTable.Rows[MNT_Count]["VARCHAR_BEFORE"] = Convert.ToString(myTable.Rows[0]["FILE_TRANSFER_TYPE"]);
                    MNT_TODAY.resultTable.Rows[MNT_Count]["VARCHAR_AFTER"] = this.FILE_TRANSFER_TYPE;
                    MNT_Count++;
                }
                if (this.PAY_CARD_NBR_ORI != null && this.PAY_CARD_NBR_ORI != Convert.ToString(myTable.Rows[0]["PAY_CARD_NBR_ORI"]))
                {
                    MNT_TODAY.initInsert_row();
                    MNT_TODAY.resultTable.Rows[MNT_Count]["FIELD_NAME"] = "PAY_CARD_NBR_ORI";
                    MNT_TODAY.resultTable.Rows[MNT_Count]["VARCHAR_BEFORE"] = Convert.ToString(myTable.Rows[0]["PAY_CARD_NBR_ORI"]);
                    MNT_TODAY.resultTable.Rows[MNT_Count]["VARCHAR_AFTER"] = this.PAY_CARD_NBR_ORI;
                    MNT_Count++;
                }
                if (this.PAY_ACCT_NBR_ORI != null && this.PAY_ACCT_NBR_ORI != Convert.ToString(myTable.Rows[0]["PAY_ACCT_NBR_ORI"]))
                {
                    MNT_TODAY.initInsert_row();
                    MNT_TODAY.resultTable.Rows[MNT_Count]["FIELD_NAME"] = "PAY_ACCT_NBR_ORI";
                    MNT_TODAY.resultTable.Rows[MNT_Count]["VARCHAR_BEFORE"] = Convert.ToString(myTable.Rows[0]["PAY_ACCT_NBR_ORI"]);
                    MNT_TODAY.resultTable.Rows[MNT_Count]["VARCHAR_AFTER"] = this.PAY_ACCT_NBR_ORI;
                    MNT_Count++;
                }
                if (this.PUBLIC_HIST_FIELD_1 != null && this.PUBLIC_HIST_FIELD_1 != Convert.ToString(myTable.Rows[0]["PUBLIC_HIST_FIELD_1"]))
                {
                    MNT_TODAY.initInsert_row();
                    MNT_TODAY.resultTable.Rows[MNT_Count]["FIELD_NAME"] = "PUBLIC_HIST_FIELD_1";
                    MNT_TODAY.resultTable.Rows[MNT_Count]["VARCHAR_BEFORE"] = Convert.ToString(myTable.Rows[0]["PUBLIC_HIST_FIELD_1"]);
                    MNT_TODAY.resultTable.Rows[MNT_Count]["VARCHAR_AFTER"] = this.PUBLIC_HIST_FIELD_1;
                    MNT_Count++;
                }
                if (this.PUBLIC_HIST_FIELD_2 != null && this.PUBLIC_HIST_FIELD_2 != Convert.ToString(myTable.Rows[0]["PUBLIC_HIST_FIELD_2"]))
                {
                    MNT_TODAY.initInsert_row();
                    MNT_TODAY.resultTable.Rows[MNT_Count]["FIELD_NAME"] = "PUBLIC_HIST_FIELD_2";
                    MNT_TODAY.resultTable.Rows[MNT_Count]["VARCHAR_BEFORE"] = Convert.ToString(myTable.Rows[0]["PUBLIC_HIST_FIELD_2"]);
                    MNT_TODAY.resultTable.Rows[MNT_Count]["VARCHAR_AFTER"] = this.PUBLIC_HIST_FIELD_2;
                    MNT_Count++;
                }
                if (this.PUBLIC_HIST_FIELD_3 != null && this.PUBLIC_HIST_FIELD_3 != Convert.ToString(myTable.Rows[0]["PUBLIC_HIST_FIELD_3"]))
                {
                    MNT_TODAY.initInsert_row();
                    MNT_TODAY.resultTable.Rows[MNT_Count]["FIELD_NAME"] = "PUBLIC_HIST_FIELD_3";
                    MNT_TODAY.resultTable.Rows[MNT_Count]["VARCHAR_BEFORE"] = Convert.ToString(myTable.Rows[0]["PUBLIC_HIST_FIELD_3"]);
                    MNT_TODAY.resultTable.Rows[MNT_Count]["VARCHAR_AFTER"] = this.PUBLIC_HIST_FIELD_3;
                    MNT_Count++;
                }
                if (this.PUBLIC_HIST_FIELD_4 != null && this.PUBLIC_HIST_FIELD_4 != Convert.ToString(myTable.Rows[0]["PUBLIC_HIST_FIELD_4"]))
                {
                    MNT_TODAY.initInsert_row();
                    MNT_TODAY.resultTable.Rows[MNT_Count]["FIELD_NAME"] = "PUBLIC_HIST_FIELD_4";
                    MNT_TODAY.resultTable.Rows[MNT_Count]["VARCHAR_BEFORE"] = Convert.ToString(myTable.Rows[0]["PUBLIC_HIST_FIELD_4"]);
                    MNT_TODAY.resultTable.Rows[MNT_Count]["VARCHAR_AFTER"] = this.PUBLIC_HIST_FIELD_4;
                    MNT_Count++;
                }
                if (this.PUBLIC_HIST_FIELD_5 != null && this.PUBLIC_HIST_FIELD_5 != Convert.ToString(myTable.Rows[0]["PUBLIC_HIST_FIELD_5"]))
                {
                    MNT_TODAY.initInsert_row();
                    MNT_TODAY.resultTable.Rows[MNT_Count]["FIELD_NAME"] = "PUBLIC_HIST_FIELD_5";
                    MNT_TODAY.resultTable.Rows[MNT_Count]["VARCHAR_BEFORE"] = Convert.ToString(myTable.Rows[0]["PUBLIC_HIST_FIELD_5"]);
                    MNT_TODAY.resultTable.Rows[MNT_Count]["VARCHAR_AFTER"] = this.PUBLIC_HIST_FIELD_5;
                    MNT_Count++;
                }
                if (this.PUBLIC_HIST_AMT_1 > -1000000000000 && this.PUBLIC_HIST_AMT_1 != Convert.ToDecimal(myTable.Rows[0]["PUBLIC_HIST_AMT_1"]))
                {
                    MNT_TODAY.initInsert_row();
                    MNT_TODAY.resultTable.Rows[MNT_Count]["FIELD_NAME"] = "PUBLIC_HIST_AMT_1";
                    MNT_TODAY.resultTable.Rows[MNT_Count]["DECIMAL_BEFORE"] = Convert.ToDecimal(myTable.Rows[0]["PUBLIC_HIST_AMT_1"]);
                    MNT_TODAY.resultTable.Rows[MNT_Count]["DECIMAL_AFTER"] = this.PUBLIC_HIST_AMT_1;
                    MNT_Count++;
                }
                if (this.PUBLIC_HIST_AMT_2 > -1000000000000 && this.PUBLIC_HIST_AMT_2 != Convert.ToDecimal(myTable.Rows[0]["PUBLIC_HIST_AMT_2"]))
                {
                    MNT_TODAY.initInsert_row();
                    MNT_TODAY.resultTable.Rows[MNT_Count]["FIELD_NAME"] = "PUBLIC_HIST_AMT_2";
                    MNT_TODAY.resultTable.Rows[MNT_Count]["DECIMAL_BEFORE"] = Convert.ToDecimal(myTable.Rows[0]["PUBLIC_HIST_AMT_2"]);
                    MNT_TODAY.resultTable.Rows[MNT_Count]["DECIMAL_AFTER"] = this.PUBLIC_HIST_AMT_2;
                    MNT_Count++;
                }
                if (this.PUBLIC_HIST_AMT_3 > -1000000000000 && this.PUBLIC_HIST_AMT_3 != Convert.ToDecimal(myTable.Rows[0]["PUBLIC_HIST_AMT_3"]))
                {
                    MNT_TODAY.initInsert_row();
                    MNT_TODAY.resultTable.Rows[MNT_Count]["FIELD_NAME"] = "PUBLIC_HIST_AMT_3";
                    MNT_TODAY.resultTable.Rows[MNT_Count]["DECIMAL_BEFORE"] = Convert.ToDecimal(myTable.Rows[0]["PUBLIC_HIST_AMT_3"]);
                    MNT_TODAY.resultTable.Rows[MNT_Count]["DECIMAL_AFTER"] = this.PUBLIC_HIST_AMT_3;
                    MNT_Count++;
                }
                if (this.PUBLIC_HIST_AMT_4 > -1000000000000 && this.PUBLIC_HIST_AMT_4 != Convert.ToDecimal(myTable.Rows[0]["PUBLIC_HIST_AMT_4"]))
                {
                    MNT_TODAY.initInsert_row();
                    MNT_TODAY.resultTable.Rows[MNT_Count]["FIELD_NAME"] = "PUBLIC_HIST_AMT_4";
                    MNT_TODAY.resultTable.Rows[MNT_Count]["DECIMAL_BEFORE"] = Convert.ToDecimal(myTable.Rows[0]["PUBLIC_HIST_AMT_4"]);
                    MNT_TODAY.resultTable.Rows[MNT_Count]["DECIMAL_AFTER"] = this.PUBLIC_HIST_AMT_4;
                    MNT_Count++;
                }
                if (this.PUBLIC_HIST_AMT_5 > -1000000000000 && this.PUBLIC_HIST_AMT_5 != Convert.ToDecimal(myTable.Rows[0]["PUBLIC_HIST_AMT_5"]))
                {
                    MNT_TODAY.initInsert_row();
                    MNT_TODAY.resultTable.Rows[MNT_Count]["FIELD_NAME"] = "PUBLIC_HIST_AMT_5";
                    MNT_TODAY.resultTable.Rows[MNT_Count]["DECIMAL_BEFORE"] = Convert.ToDecimal(myTable.Rows[0]["PUBLIC_HIST_AMT_5"]);
                    MNT_TODAY.resultTable.Rows[MNT_Count]["DECIMAL_AFTER"] = this.PUBLIC_HIST_AMT_5;
                    MNT_Count++;
                }
                if (this.PUBLIC_HIST_DT_1 > dateStart && this.PUBLIC_HIST_DT_1 != Convert.ToDateTime(myTable.Rows[0]["PUBLIC_HIST_DT_1"]))
                {
                    if (this.PUBLIC_HIST_DT_1.ToString("yyyyMMdd") == "19000101" &&
                        Convert.ToDateTime(myTable.Rows[0]["PUBLIC_HIST_DT_1"]) == dateStart)
                    {
                    }
                    else
                    {
                        MNT_TODAY.initInsert_row();
                        MNT_TODAY.resultTable.Rows[MNT_Count]["FIELD_NAME"] = "PUBLIC_HIST_DT_1";
                        MNT_TODAY.resultTable.Rows[MNT_Count]["DT_BEFORE"] = Convert.ToDateTime(myTable.Rows[0]["PUBLIC_HIST_DT_1"]);
                        if (this.PUBLIC_HIST_DT_1.ToString("yyyyMMdd") == "19000101")
                        {
                            MNT_TODAY.resultTable.Rows[MNT_Count]["DT_AFTER"] = "1900-01-01";
                        }
                        else
                        {
                            MNT_TODAY.resultTable.Rows[MNT_Count]["DT_AFTER"] = this.PUBLIC_HIST_DT_1;
                        }
                        MNT_Count++;
                    }
                }
                if (this.PUBLIC_HIST_DT_2 > dateStart && this.PUBLIC_HIST_DT_2 != Convert.ToDateTime(myTable.Rows[0]["PUBLIC_HIST_DT_2"]))
                {
                    if (this.PUBLIC_HIST_DT_2.ToString("yyyyMMdd") == "19000101" &&
                        Convert.ToDateTime(myTable.Rows[0]["PUBLIC_HIST_DT_2"]) == dateStart)
                    {
                    }
                    else
                    {
                        MNT_TODAY.initInsert_row();
                        MNT_TODAY.resultTable.Rows[MNT_Count]["FIELD_NAME"] = "PUBLIC_HIST_DT_2";
                        MNT_TODAY.resultTable.Rows[MNT_Count]["DT_BEFORE"] = Convert.ToDateTime(myTable.Rows[0]["PUBLIC_HIST_DT_2"]);
                        if (this.PUBLIC_HIST_DT_2.ToString("yyyyMMdd") == "19000101")
                        {
                            MNT_TODAY.resultTable.Rows[MNT_Count]["DT_AFTER"] = "1900-01-01";
                        }
                        else
                        {
                            MNT_TODAY.resultTable.Rows[MNT_Count]["DT_AFTER"] = this.PUBLIC_HIST_DT_2;
                        }
                        MNT_Count++;
                    }
                }
                if (this.PUBLIC_HIST_DT_3 > dateStart && this.PUBLIC_HIST_DT_3 != Convert.ToDateTime(myTable.Rows[0]["PUBLIC_HIST_DT_3"]))
                {
                    if (this.PUBLIC_HIST_DT_3.ToString("yyyyMMdd") == "19000101" &&
                        Convert.ToDateTime(myTable.Rows[0]["PUBLIC_HIST_DT_3"]) == dateStart)
                    {
                    }
                    else
                    {
                        MNT_TODAY.initInsert_row();
                        MNT_TODAY.resultTable.Rows[MNT_Count]["FIELD_NAME"] = "PUBLIC_HIST_DT_3";
                        MNT_TODAY.resultTable.Rows[MNT_Count]["DT_BEFORE"] = Convert.ToDateTime(myTable.Rows[0]["PUBLIC_HIST_DT_3"]);
                        if (this.PUBLIC_HIST_DT_3.ToString("yyyyMMdd") == "19000101")
                        {
                            MNT_TODAY.resultTable.Rows[MNT_Count]["DT_AFTER"] = "1900-01-01";
                        }
                        else
                        {
                            MNT_TODAY.resultTable.Rows[MNT_Count]["DT_AFTER"] = this.PUBLIC_HIST_DT_3;
                        }
                        MNT_Count++;
                    }
                }
                if (this.PUBLIC_HIST_DT_4 > dateStart && this.PUBLIC_HIST_DT_4 != Convert.ToDateTime(myTable.Rows[0]["PUBLIC_HIST_DT_4"]))
                {
                    if (this.PUBLIC_HIST_DT_4.ToString("yyyyMMdd") == "19000101" &&
                        Convert.ToDateTime(myTable.Rows[0]["PUBLIC_HIST_DT_4"]) == dateStart)
                    {
                    }
                    else
                    {
                        MNT_TODAY.initInsert_row();
                        MNT_TODAY.resultTable.Rows[MNT_Count]["FIELD_NAME"] = "PUBLIC_HIST_DT_4";
                        MNT_TODAY.resultTable.Rows[MNT_Count]["DT_BEFORE"] = Convert.ToDateTime(myTable.Rows[0]["PUBLIC_HIST_DT_4"]);
                        if (this.PUBLIC_HIST_DT_4.ToString("yyyyMMdd") == "19000101")
                        {
                            MNT_TODAY.resultTable.Rows[MNT_Count]["DT_AFTER"] = "1900-01-01";
                        }
                        else
                        {
                            MNT_TODAY.resultTable.Rows[MNT_Count]["DT_AFTER"] = this.PUBLIC_HIST_DT_4;
                        }
                        MNT_Count++;
                    }
                }
                if (this.PUBLIC_HIST_DT_5 > dateStart && this.PUBLIC_HIST_DT_5 != Convert.ToDateTime(myTable.Rows[0]["PUBLIC_HIST_DT_5"]))
                {
                    if (this.PUBLIC_HIST_DT_5.ToString("yyyyMMdd") == "19000101" &&
                        Convert.ToDateTime(myTable.Rows[0]["PUBLIC_HIST_DT_5"]) == dateStart)
                    {
                    }
                    else
                    {
                        MNT_TODAY.initInsert_row();
                        MNT_TODAY.resultTable.Rows[MNT_Count]["FIELD_NAME"] = "PUBLIC_HIST_DT_5";
                        MNT_TODAY.resultTable.Rows[MNT_Count]["DT_BEFORE"] = Convert.ToDateTime(myTable.Rows[0]["PUBLIC_HIST_DT_5"]);
                        if (this.PUBLIC_HIST_DT_5.ToString("yyyyMMdd") == "19000101")
                        {
                            MNT_TODAY.resultTable.Rows[MNT_Count]["DT_AFTER"] = "1900-01-01";
                        }
                        else
                        {
                            MNT_TODAY.resultTable.Rows[MNT_Count]["DT_AFTER"] = this.PUBLIC_HIST_DT_5;
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
        #region query_group_by()
        public string query_group_by()
        {
            #region Modify History
            /// <history>
            /// <design>
            /// <name>Cybersoft.COCA DaoGenerator</name>
            /// <date>2014/12/24 上午 10:27:09</date>
            #endregion
            try
            {
                #region set SQL statement
                StringBuilder sbstrSQL = new StringBuilder();
                sbstrSQL.Append("SELECT ");
                #endregion
                #region GROUP BY
                if (this.groupTRANS_DTE != null)
                {
                    sbstrSQL.Append("a.TRANS_DTE");
                }
                else
                {
                    sbstrSQL.Append("''");
                }
                sbstrSQL.Append(" as TRANS_DTE,");
                if (this.groupPAY_TYPE != null)
                {
                    sbstrSQL.Append("a.PAY_TYPE");
                }
                else
                {
                    sbstrSQL.Append("''");
                }
                sbstrSQL.Append(" as PAY_TYPE,");
                if (this.groupBU != null)
                {
                    sbstrSQL.Append("a.BU");
                }
                else
                {
                    sbstrSQL.Append("''");
                }
                sbstrSQL.Append(" as BU,");
                if (this.groupACCT_NBR != null)
                {
                    sbstrSQL.Append("a.ACCT_NBR");
                }
                else
                {
                    sbstrSQL.Append("''");
                }
                sbstrSQL.Append(" as ACCT_NBR,");
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
                if (this.groupCURRENCY != null)
                {
                    sbstrSQL.Append("a.CURRENCY");
                }
                else
                {
                    sbstrSQL.Append("''");
                }
                sbstrSQL.Append(" as CURRENCY,");
                if (this.groupPAY_CARD_NBR != null)
                {
                    sbstrSQL.Append("a.PAY_CARD_NBR");
                }
                else
                {
                    sbstrSQL.Append("''");
                }
                sbstrSQL.Append(" as PAY_CARD_NBR,");
                if (this.groupEXPIR_DTE != null)
                {
                    sbstrSQL.Append("a.EXPIR_DTE");
                }
                else
                {
                    sbstrSQL.Append("''");
                }
                sbstrSQL.Append(" as EXPIR_DTE,");
                if (this.groupCUST_SEQ != null)
                {
                    sbstrSQL.Append("a.CUST_SEQ");
                }
                else
                {
                    sbstrSQL.Append("''");
                }
                sbstrSQL.Append(" as CUST_SEQ,");
                if (this.groupPAY_NBR != null)
                {
                    sbstrSQL.Append("a.PAY_NBR");
                }
                else
                {
                    sbstrSQL.Append("''");
                }
                sbstrSQL.Append(" as PAY_NBR,");
                if (this.groupPAY_AMT != null)
                {
                    sbstrSQL.Append("a.PAY_AMT");
                }
                else
                {
                    sbstrSQL.Append("''");
                }
                sbstrSQL.Append(" as PAY_AMT,");
                if (this.groupPAY_DTE != null)
                {
                    sbstrSQL.Append("a.PAY_DTE");
                }
                else
                {
                    sbstrSQL.Append("''");
                }
                sbstrSQL.Append(" as PAY_DTE,");
                if (this.groupPAY_SEQ != null)
                {
                    sbstrSQL.Append("a.PAY_SEQ");
                }
                else
                {
                    sbstrSQL.Append("''");
                }
                sbstrSQL.Append(" as PAY_SEQ,");
                if (this.groupPAY_RESULT != null)
                {
                    sbstrSQL.Append("a.PAY_RESULT");
                }
                else
                {
                    sbstrSQL.Append("''");
                }
                sbstrSQL.Append(" as PAY_RESULT,");
                if (this.groupAUTH_CODE != null)
                {
                    sbstrSQL.Append("a.AUTH_CODE");
                }
                else
                {
                    sbstrSQL.Append("''");
                }
                sbstrSQL.Append(" as AUTH_CODE,");
                if (this.groupERROR_REASON != null)
                {
                    sbstrSQL.Append("a.ERROR_REASON");
                }
                else
                {
                    sbstrSQL.Append("''");
                }
                sbstrSQL.Append(" as ERROR_REASON,");
                if (this.groupREVERSAL_FLAG != null)
                {
                    sbstrSQL.Append("a.REVERSAL_FLAG");
                }
                else
                {
                    sbstrSQL.Append("''");
                }
                sbstrSQL.Append(" as REVERSAL_FLAG,");
                if (this.groupCTL_CODE != null)
                {
                    sbstrSQL.Append("a.CTL_CODE");
                }
                else
                {
                    sbstrSQL.Append("''");
                }
                sbstrSQL.Append(" as CTL_CODE,");
                if (this.groupAUTH_RESP != null)
                {
                    sbstrSQL.Append("a.AUTH_RESP");
                }
                else
                {
                    sbstrSQL.Append("''");
                }
                sbstrSQL.Append(" as AUTH_RESP,");
                if (this.groupCHANGE_NBR_NEW != null)
                {
                    sbstrSQL.Append("a.CHANGE_NBR_NEW");
                }
                else
                {
                    sbstrSQL.Append("''");
                }
                sbstrSQL.Append(" as CHANGE_NBR_NEW,");
                if (this.groupFILE_TRANSFER_TYPE != null)
                {
                    sbstrSQL.Append("a.FILE_TRANSFER_TYPE");
                }
                else
                {
                    sbstrSQL.Append("''");
                }
                sbstrSQL.Append(" as FILE_TRANSFER_TYPE,");
                if (this.groupPAY_CARD_NBR_ORI != null)
                {
                    sbstrSQL.Append("a.PAY_CARD_NBR_ORI");
                }
                else
                {
                    sbstrSQL.Append("''");
                }
                sbstrSQL.Append(" as PAY_CARD_NBR_ORI,");
                if (this.groupPAY_ACCT_NBR_ORI != null)
                {
                    sbstrSQL.Append("a.PAY_ACCT_NBR_ORI");
                }
                else
                {
                    sbstrSQL.Append("''");
                }
                sbstrSQL.Append(" as PAY_ACCT_NBR_ORI,");
                if (this.groupPUBLIC_HIST_FIELD_1 != null)
                {
                    sbstrSQL.Append("a.PUBLIC_HIST_FIELD_1");
                }
                else
                {
                    sbstrSQL.Append("''");
                }
                sbstrSQL.Append(" as PUBLIC_HIST_FIELD_1,");
                if (this.groupPUBLIC_HIST_FIELD_2 != null)
                {
                    sbstrSQL.Append("a.PUBLIC_HIST_FIELD_2");
                }
                else
                {
                    sbstrSQL.Append("''");
                }
                sbstrSQL.Append(" as PUBLIC_HIST_FIELD_2,");
                if (this.groupPUBLIC_HIST_FIELD_3 != null)
                {
                    sbstrSQL.Append("a.PUBLIC_HIST_FIELD_3");
                }
                else
                {
                    sbstrSQL.Append("''");
                }
                sbstrSQL.Append(" as PUBLIC_HIST_FIELD_3,");
                if (this.groupPUBLIC_HIST_FIELD_4 != null)
                {
                    sbstrSQL.Append("a.PUBLIC_HIST_FIELD_4");
                }
                else
                {
                    sbstrSQL.Append("''");
                }
                sbstrSQL.Append(" as PUBLIC_HIST_FIELD_4,");
                if (this.groupPUBLIC_HIST_FIELD_5 != null)
                {
                    sbstrSQL.Append("a.PUBLIC_HIST_FIELD_5");
                }
                else
                {
                    sbstrSQL.Append("''");
                }
                sbstrSQL.Append(" as PUBLIC_HIST_FIELD_5,");
                if (this.groupPUBLIC_HIST_AMT_1 != null)
                {
                    sbstrSQL.Append("a.PUBLIC_HIST_AMT_1");
                }
                else
                {
                    sbstrSQL.Append("''");
                }
                sbstrSQL.Append(" as PUBLIC_HIST_AMT_1,");
                if (this.groupPUBLIC_HIST_AMT_2 != null)
                {
                    sbstrSQL.Append("a.PUBLIC_HIST_AMT_2");
                }
                else
                {
                    sbstrSQL.Append("''");
                }
                sbstrSQL.Append(" as PUBLIC_HIST_AMT_2,");
                if (this.groupPUBLIC_HIST_AMT_3 != null)
                {
                    sbstrSQL.Append("a.PUBLIC_HIST_AMT_3");
                }
                else
                {
                    sbstrSQL.Append("''");
                }
                sbstrSQL.Append(" as PUBLIC_HIST_AMT_3,");
                if (this.groupPUBLIC_HIST_AMT_4 != null)
                {
                    sbstrSQL.Append("a.PUBLIC_HIST_AMT_4");
                }
                else
                {
                    sbstrSQL.Append("''");
                }
                sbstrSQL.Append(" as PUBLIC_HIST_AMT_4,");
                if (this.groupPUBLIC_HIST_AMT_5 != null)
                {
                    sbstrSQL.Append("a.PUBLIC_HIST_AMT_5");
                }
                else
                {
                    sbstrSQL.Append("''");
                }
                sbstrSQL.Append(" as PUBLIC_HIST_AMT_5,");
                if (this.groupPUBLIC_HIST_DT_1 != null)
                {
                    sbstrSQL.Append("a.PUBLIC_HIST_DT_1");
                }
                else
                {
                    sbstrSQL.Append("''");
                }
                sbstrSQL.Append(" as PUBLIC_HIST_DT_1,");
                if (this.groupPUBLIC_HIST_DT_2 != null)
                {
                    sbstrSQL.Append("a.PUBLIC_HIST_DT_2");
                }
                else
                {
                    sbstrSQL.Append("''");
                }
                sbstrSQL.Append(" as PUBLIC_HIST_DT_2,");
                if (this.groupPUBLIC_HIST_DT_3 != null)
                {
                    sbstrSQL.Append("a.PUBLIC_HIST_DT_3");
                }
                else
                {
                    sbstrSQL.Append("''");
                }
                sbstrSQL.Append(" as PUBLIC_HIST_DT_3,");
                if (this.groupPUBLIC_HIST_DT_4 != null)
                {
                    sbstrSQL.Append("a.PUBLIC_HIST_DT_4");
                }
                else
                {
                    sbstrSQL.Append("''");
                }
                sbstrSQL.Append(" as PUBLIC_HIST_DT_4,");
                if (this.groupPUBLIC_HIST_DT_5 != null)
                {
                    sbstrSQL.Append("a.PUBLIC_HIST_DT_5");
                }
                else
                {
                    sbstrSQL.Append("''");
                }
                sbstrSQL.Append(" as PUBLIC_HIST_DT_5,");
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
                sbstrSQL.Append(" FROM PUBLIC_HIST a Where 1=1  ");
                #region WHERE CONIDTION
                if (this.whereTRANS_DTE != null)
                {
                    sbstrSQL.Append(" and a.TRANS_DTE=@whereTRANS_DTE ");
                }
                if (this.wherePAY_TYPE != null)
                {
                    sbstrSQL.Append(" and a.PAY_TYPE=@wherePAY_TYPE ");
                }
                if (this.whereBU != null)
                {
                    sbstrSQL.Append(" and a.BU=@whereBU ");
                }
                if (this.whereACCT_NBR != null)
                {
                    sbstrSQL.Append(" and a.ACCT_NBR=@whereACCT_NBR ");
                }
                if (this.wherePRODUCT != null)
                {
                    sbstrSQL.Append(" and a.PRODUCT=@wherePRODUCT ");
                }
                if (this.whereCARD_PRODUCT != null)
                {
                    sbstrSQL.Append(" and a.CARD_PRODUCT=@whereCARD_PRODUCT ");
                }
                if (this.whereCURRENCY != null)
                {
                    sbstrSQL.Append(" and a.CURRENCY=@whereCURRENCY ");
                }
                if (this.wherePAY_CARD_NBR != null)
                {
                    sbstrSQL.Append(" and a.PAY_CARD_NBR=@wherePAY_CARD_NBR ");
                }
                if (this.whereEXPIR_DTE != null)
                {
                    sbstrSQL.Append(" and a.EXPIR_DTE=@whereEXPIR_DTE ");
                }
                if (this.whereCUST_SEQ != null)
                {
                    sbstrSQL.Append(" and a.CUST_SEQ=@whereCUST_SEQ ");
                }
                if (this.wherePAY_NBR != null)
                {
                    sbstrSQL.Append(" and a.PAY_NBR=@wherePAY_NBR ");
                }
                if (this.wherePAY_AMT > -1000000000000)
                {
                    sbstrSQL.Append(" and a.PAY_AMT=@wherePAY_AMT ");
                }
                if (this.wherePAY_DTE > dateStart)
                {
                    sbstrSQL.Append("  and a.PAY_DTE=@wherePAY_DTE ");
                }
                if (this.wherePAY_SEQ != null)
                {
                    sbstrSQL.Append(" and a.PAY_SEQ=@wherePAY_SEQ ");
                }
                if (this.wherePAY_RESULT != null)
                {
                    sbstrSQL.Append(" and a.PAY_RESULT=@wherePAY_RESULT ");
                }
                if (this.whereAUTH_CODE != null)
                {
                    sbstrSQL.Append(" and a.AUTH_CODE=@whereAUTH_CODE ");
                }
                if (this.whereERROR_REASON != null)
                {
                    sbstrSQL.Append(" and a.ERROR_REASON=@whereERROR_REASON ");
                }
                if (this.whereREVERSAL_FLAG != null)
                {
                    sbstrSQL.Append(" and a.REVERSAL_FLAG=@whereREVERSAL_FLAG ");
                }
                if (this.whereCTL_CODE != null)
                {
                    sbstrSQL.Append(" and a.CTL_CODE=@whereCTL_CODE ");
                }
                if (this.whereAUTH_RESP != null)
                {
                    sbstrSQL.Append(" and a.AUTH_RESP=@whereAUTH_RESP ");
                }
                if (this.whereCHANGE_NBR_NEW != null)
                {
                    sbstrSQL.Append(" and a.CHANGE_NBR_NEW=@whereCHANGE_NBR_NEW ");
                }
                if (this.whereFILE_TRANSFER_TYPE != null)
                {
                    sbstrSQL.Append(" and a.FILE_TRANSFER_TYPE=@whereFILE_TRANSFER_TYPE ");
                }
                if (this.wherePAY_CARD_NBR_ORI != null)
                {
                    sbstrSQL.Append(" and a.PAY_CARD_NBR_ORI=@wherePAY_CARD_NBR_ORI ");
                }
                if (this.wherePAY_ACCT_NBR_ORI != null)
                {
                    sbstrSQL.Append(" and a.PAY_ACCT_NBR_ORI=@wherePAY_ACCT_NBR_ORI ");
                }
                if (this.wherePUBLIC_HIST_FIELD_1 != null)
                {
                    sbstrSQL.Append(" and a.PUBLIC_HIST_FIELD_1=@wherePUBLIC_HIST_FIELD_1 ");
                }
                if (this.wherePUBLIC_HIST_FIELD_2 != null)
                {
                    sbstrSQL.Append(" and a.PUBLIC_HIST_FIELD_2=@wherePUBLIC_HIST_FIELD_2 ");
                }
                if (this.wherePUBLIC_HIST_FIELD_3 != null)
                {
                    sbstrSQL.Append(" and a.PUBLIC_HIST_FIELD_3=@wherePUBLIC_HIST_FIELD_3 ");
                }
                if (this.wherePUBLIC_HIST_FIELD_4 != null)
                {
                    sbstrSQL.Append(" and a.PUBLIC_HIST_FIELD_4=@wherePUBLIC_HIST_FIELD_4 ");
                }
                if (this.wherePUBLIC_HIST_FIELD_5 != null)
                {
                    sbstrSQL.Append(" and a.PUBLIC_HIST_FIELD_5=@wherePUBLIC_HIST_FIELD_5 ");
                }
                if (this.wherePUBLIC_HIST_AMT_1 > -1000000000000)
                {
                    sbstrSQL.Append(" and a.PUBLIC_HIST_AMT_1=@wherePUBLIC_HIST_AMT_1 ");
                }
                if (this.wherePUBLIC_HIST_AMT_2 > -1000000000000)
                {
                    sbstrSQL.Append(" and a.PUBLIC_HIST_AMT_2=@wherePUBLIC_HIST_AMT_2 ");
                }
                if (this.wherePUBLIC_HIST_AMT_3 > -1000000000000)
                {
                    sbstrSQL.Append(" and a.PUBLIC_HIST_AMT_3=@wherePUBLIC_HIST_AMT_3 ");
                }
                if (this.wherePUBLIC_HIST_AMT_4 > -1000000000000)
                {
                    sbstrSQL.Append(" and a.PUBLIC_HIST_AMT_4=@wherePUBLIC_HIST_AMT_4 ");
                }
                if (this.wherePUBLIC_HIST_AMT_5 > -1000000000000)
                {
                    sbstrSQL.Append(" and a.PUBLIC_HIST_AMT_5=@wherePUBLIC_HIST_AMT_5 ");
                }
                if (this.wherePUBLIC_HIST_DT_1 > dateStart)
                {
                    sbstrSQL.Append("  and a.PUBLIC_HIST_DT_1=@wherePUBLIC_HIST_DT_1 ");
                }
                if (this.wherePUBLIC_HIST_DT_2 > dateStart)
                {
                    sbstrSQL.Append("  and a.PUBLIC_HIST_DT_2=@wherePUBLIC_HIST_DT_2 ");
                }
                if (this.wherePUBLIC_HIST_DT_3 > dateStart)
                {
                    sbstrSQL.Append("  and a.PUBLIC_HIST_DT_3=@wherePUBLIC_HIST_DT_3 ");
                }
                if (this.wherePUBLIC_HIST_DT_4 > dateStart)
                {
                    sbstrSQL.Append("  and a.PUBLIC_HIST_DT_4=@wherePUBLIC_HIST_DT_4 ");
                }
                if (this.wherePUBLIC_HIST_DT_5 > dateStart)
                {
                    sbstrSQL.Append("  and a.PUBLIC_HIST_DT_5=@wherePUBLIC_HIST_DT_5 ");
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
                if (this.groupTRANS_DTE != null)
                {
                    sbstrSQL.Append("a.TRANS_DTE,");
                }
                if (this.groupPAY_TYPE != null)
                {
                    sbstrSQL.Append("a.PAY_TYPE,");
                }
                if (this.groupBU != null)
                {
                    sbstrSQL.Append("a.BU,");
                }
                if (this.groupACCT_NBR != null)
                {
                    sbstrSQL.Append("a.ACCT_NBR,");
                }
                if (this.groupPRODUCT != null)
                {
                    sbstrSQL.Append("a.PRODUCT,");
                }
                if (this.groupCARD_PRODUCT != null)
                {
                    sbstrSQL.Append("a.CARD_PRODUCT,");
                }
                if (this.groupCURRENCY != null)
                {
                    sbstrSQL.Append("a.CURRENCY,");
                }
                if (this.groupPAY_CARD_NBR != null)
                {
                    sbstrSQL.Append("a.PAY_CARD_NBR,");
                }
                if (this.groupEXPIR_DTE != null)
                {
                    sbstrSQL.Append("a.EXPIR_DTE,");
                }
                if (this.groupCUST_SEQ != null)
                {
                    sbstrSQL.Append("a.CUST_SEQ,");
                }
                if (this.groupPAY_NBR != null)
                {
                    sbstrSQL.Append("a.PAY_NBR,");
                }
                if (this.groupPAY_AMT != null)
                {
                    sbstrSQL.Append("a.PAY_AMT,");
                }
                if (this.groupPAY_DTE != null)
                {
                    sbstrSQL.Append("a.PAY_DTE,");
                }
                if (this.groupPAY_SEQ != null)
                {
                    sbstrSQL.Append("a.PAY_SEQ,");
                }
                if (this.groupPAY_RESULT != null)
                {
                    sbstrSQL.Append("a.PAY_RESULT,");
                }
                if (this.groupAUTH_CODE != null)
                {
                    sbstrSQL.Append("a.AUTH_CODE,");
                }
                if (this.groupERROR_REASON != null)
                {
                    sbstrSQL.Append("a.ERROR_REASON,");
                }
                if (this.groupREVERSAL_FLAG != null)
                {
                    sbstrSQL.Append("a.REVERSAL_FLAG,");
                }
                if (this.groupCTL_CODE != null)
                {
                    sbstrSQL.Append("a.CTL_CODE,");
                }
                if (this.groupAUTH_RESP != null)
                {
                    sbstrSQL.Append("a.AUTH_RESP,");
                }
                if (this.groupCHANGE_NBR_NEW != null)
                {
                    sbstrSQL.Append("a.CHANGE_NBR_NEW,");
                }
                if (this.groupFILE_TRANSFER_TYPE != null)
                {
                    sbstrSQL.Append("a.FILE_TRANSFER_TYPE,");
                }
                if (this.groupPAY_CARD_NBR_ORI != null)
                {
                    sbstrSQL.Append("a.PAY_CARD_NBR_ORI,");
                }
                if (this.groupPAY_ACCT_NBR_ORI != null)
                {
                    sbstrSQL.Append("a.PAY_ACCT_NBR_ORI,");
                }
                if (this.groupPUBLIC_HIST_FIELD_1 != null)
                {
                    sbstrSQL.Append("a.PUBLIC_HIST_FIELD_1,");
                }
                if (this.groupPUBLIC_HIST_FIELD_2 != null)
                {
                    sbstrSQL.Append("a.PUBLIC_HIST_FIELD_2,");
                }
                if (this.groupPUBLIC_HIST_FIELD_3 != null)
                {
                    sbstrSQL.Append("a.PUBLIC_HIST_FIELD_3,");
                }
                if (this.groupPUBLIC_HIST_FIELD_4 != null)
                {
                    sbstrSQL.Append("a.PUBLIC_HIST_FIELD_4,");
                }
                if (this.groupPUBLIC_HIST_FIELD_5 != null)
                {
                    sbstrSQL.Append("a.PUBLIC_HIST_FIELD_5,");
                }
                if (this.groupPUBLIC_HIST_AMT_1 != null)
                {
                    sbstrSQL.Append("a.PUBLIC_HIST_AMT_1,");
                }
                if (this.groupPUBLIC_HIST_AMT_2 != null)
                {
                    sbstrSQL.Append("a.PUBLIC_HIST_AMT_2,");
                }
                if (this.groupPUBLIC_HIST_AMT_3 != null)
                {
                    sbstrSQL.Append("a.PUBLIC_HIST_AMT_3,");
                }
                if (this.groupPUBLIC_HIST_AMT_4 != null)
                {
                    sbstrSQL.Append("a.PUBLIC_HIST_AMT_4,");
                }
                if (this.groupPUBLIC_HIST_AMT_5 != null)
                {
                    sbstrSQL.Append("a.PUBLIC_HIST_AMT_5,");
                }
                if (this.groupPUBLIC_HIST_DT_1 != null)
                {
                    sbstrSQL.Append("a.PUBLIC_HIST_DT_1,");
                }
                if (this.groupPUBLIC_HIST_DT_2 != null)
                {
                    sbstrSQL.Append("a.PUBLIC_HIST_DT_2,");
                }
                if (this.groupPUBLIC_HIST_DT_3 != null)
                {
                    sbstrSQL.Append("a.PUBLIC_HIST_DT_3,");
                }
                if (this.groupPUBLIC_HIST_DT_4 != null)
                {
                    sbstrSQL.Append("a.PUBLIC_HIST_DT_4,");
                }
                if (this.groupPUBLIC_HIST_DT_5 != null)
                {
                    sbstrSQL.Append("a.PUBLIC_HIST_DT_5,");
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
                if (sbstrSQL.ToString().Contains("@whereTRANS_DTE "))
                {
                    this.SelectOperator.SetValue("@whereTRANS_DTE", this.whereTRANS_DTE, SqlDbType.VarChar);
                }
                if (sbstrSQL.ToString().Contains("@wherePAY_TYPE "))
                {
                    this.SelectOperator.SetValue("@wherePAY_TYPE", this.wherePAY_TYPE, SqlDbType.VarChar);
                }
                if (sbstrSQL.ToString().Contains("@whereBU "))
                {
                    this.SelectOperator.SetValue("@whereBU", this.whereBU, SqlDbType.VarChar);
                }
                if (sbstrSQL.ToString().Contains("@whereACCT_NBR "))
                {
                    this.SelectOperator.SetValue("@whereACCT_NBR", this.whereACCT_NBR, SqlDbType.VarChar);
                }
                if (sbstrSQL.ToString().Contains("@wherePRODUCT "))
                {
                    this.SelectOperator.SetValue("@wherePRODUCT", this.wherePRODUCT, SqlDbType.VarChar);
                }
                if (sbstrSQL.ToString().Contains("@whereCARD_PRODUCT "))
                {
                    this.SelectOperator.SetValue("@whereCARD_PRODUCT", this.whereCARD_PRODUCT, SqlDbType.VarChar);
                }
                if (sbstrSQL.ToString().Contains("@whereCURRENCY "))
                {
                    this.SelectOperator.SetValue("@whereCURRENCY", this.whereCURRENCY, SqlDbType.VarChar);
                }
                if (sbstrSQL.ToString().Contains("@wherePAY_CARD_NBR "))
                {
                    this.SelectOperator.SetValue("@wherePAY_CARD_NBR", this.wherePAY_CARD_NBR, SqlDbType.VarChar);
                }
                if (sbstrSQL.ToString().Contains("@whereEXPIR_DTE "))
                {
                    this.SelectOperator.SetValue("@whereEXPIR_DTE", this.whereEXPIR_DTE, SqlDbType.VarChar);
                }
                if (sbstrSQL.ToString().Contains("@whereCUST_SEQ "))
                {
                    this.SelectOperator.SetValue("@whereCUST_SEQ", this.whereCUST_SEQ, SqlDbType.VarChar);
                }
                if (sbstrSQL.ToString().Contains("@wherePAY_NBR "))
                {
                    this.SelectOperator.SetValue("@wherePAY_NBR", this.wherePAY_NBR, SqlDbType.VarChar);
                }
                if (sbstrSQL.ToString().Contains("@wherePAY_AMT "))
                {
                    this.SelectOperator.SetValue("@wherePAY_AMT", this.wherePAY_AMT, SqlDbType.Decimal);
                }
                if (sbstrSQL.ToString().Contains("@wherePAY_DTE "))
                {
                    this.SelectOperator.SetValue("@wherePAY_DTE", this.wherePAY_DTE, SqlDbType.DateTime);
                }
                if (sbstrSQL.ToString().Contains("@wherePAY_SEQ "))
                {
                    this.SelectOperator.SetValue("@wherePAY_SEQ", this.wherePAY_SEQ, SqlDbType.VarChar);
                }
                if (sbstrSQL.ToString().Contains("@wherePAY_RESULT "))
                {
                    this.SelectOperator.SetValue("@wherePAY_RESULT", this.wherePAY_RESULT, SqlDbType.VarChar);
                }
                if (sbstrSQL.ToString().Contains("@whereAUTH_CODE "))
                {
                    this.SelectOperator.SetValue("@whereAUTH_CODE", this.whereAUTH_CODE, SqlDbType.VarChar);
                }
                if (sbstrSQL.ToString().Contains("@whereERROR_REASON "))
                {
                    this.SelectOperator.SetValue("@whereERROR_REASON", this.whereERROR_REASON, SqlDbType.VarChar);
                }
                if (sbstrSQL.ToString().Contains("@whereREVERSAL_FLAG "))
                {
                    this.SelectOperator.SetValue("@whereREVERSAL_FLAG", this.whereREVERSAL_FLAG, SqlDbType.VarChar);
                }
                if (sbstrSQL.ToString().Contains("@whereCTL_CODE "))
                {
                    this.SelectOperator.SetValue("@whereCTL_CODE", this.whereCTL_CODE, SqlDbType.VarChar);
                }
                if (sbstrSQL.ToString().Contains("@whereAUTH_RESP "))
                {
                    this.SelectOperator.SetValue("@whereAUTH_RESP", this.whereAUTH_RESP, SqlDbType.VarChar);
                }
                if (sbstrSQL.ToString().Contains("@whereCHANGE_NBR_NEW "))
                {
                    this.SelectOperator.SetValue("@whereCHANGE_NBR_NEW", this.whereCHANGE_NBR_NEW, SqlDbType.VarChar);
                }
                if (sbstrSQL.ToString().Contains("@whereFILE_TRANSFER_TYPE "))
                {
                    this.SelectOperator.SetValue("@whereFILE_TRANSFER_TYPE", this.whereFILE_TRANSFER_TYPE, SqlDbType.VarChar);
                }
                if (sbstrSQL.ToString().Contains("@wherePAY_CARD_NBR_ORI "))
                {
                    this.SelectOperator.SetValue("@wherePAY_CARD_NBR_ORI", this.wherePAY_CARD_NBR_ORI, SqlDbType.VarChar);
                }
                if (sbstrSQL.ToString().Contains("@wherePAY_ACCT_NBR_ORI "))
                {
                    this.SelectOperator.SetValue("@wherePAY_ACCT_NBR_ORI", this.wherePAY_ACCT_NBR_ORI, SqlDbType.VarChar);
                }
                if (sbstrSQL.ToString().Contains("@wherePUBLIC_HIST_FIELD_1 "))
                {
                    this.SelectOperator.SetValue("@wherePUBLIC_HIST_FIELD_1", this.wherePUBLIC_HIST_FIELD_1, SqlDbType.VarChar);
                }
                if (sbstrSQL.ToString().Contains("@wherePUBLIC_HIST_FIELD_2 "))
                {
                    this.SelectOperator.SetValue("@wherePUBLIC_HIST_FIELD_2", this.wherePUBLIC_HIST_FIELD_2, SqlDbType.VarChar);
                }
                if (sbstrSQL.ToString().Contains("@wherePUBLIC_HIST_FIELD_3 "))
                {
                    this.SelectOperator.SetValue("@wherePUBLIC_HIST_FIELD_3", this.wherePUBLIC_HIST_FIELD_3, SqlDbType.VarChar);
                }
                if (sbstrSQL.ToString().Contains("@wherePUBLIC_HIST_FIELD_4 "))
                {
                    this.SelectOperator.SetValue("@wherePUBLIC_HIST_FIELD_4", this.wherePUBLIC_HIST_FIELD_4, SqlDbType.VarChar);
                }
                if (sbstrSQL.ToString().Contains("@wherePUBLIC_HIST_FIELD_5 "))
                {
                    this.SelectOperator.SetValue("@wherePUBLIC_HIST_FIELD_5", this.wherePUBLIC_HIST_FIELD_5, SqlDbType.VarChar);
                }
                if (sbstrSQL.ToString().Contains("@wherePUBLIC_HIST_AMT_1 "))
                {
                    this.SelectOperator.SetValue("@wherePUBLIC_HIST_AMT_1", this.wherePUBLIC_HIST_AMT_1, SqlDbType.Decimal);
                }
                if (sbstrSQL.ToString().Contains("@wherePUBLIC_HIST_AMT_2 "))
                {
                    this.SelectOperator.SetValue("@wherePUBLIC_HIST_AMT_2", this.wherePUBLIC_HIST_AMT_2, SqlDbType.Decimal);
                }
                if (sbstrSQL.ToString().Contains("@wherePUBLIC_HIST_AMT_3 "))
                {
                    this.SelectOperator.SetValue("@wherePUBLIC_HIST_AMT_3", this.wherePUBLIC_HIST_AMT_3, SqlDbType.Decimal);
                }
                if (sbstrSQL.ToString().Contains("@wherePUBLIC_HIST_AMT_4 "))
                {
                    this.SelectOperator.SetValue("@wherePUBLIC_HIST_AMT_4", this.wherePUBLIC_HIST_AMT_4, SqlDbType.Decimal);
                }
                if (sbstrSQL.ToString().Contains("@wherePUBLIC_HIST_AMT_5 "))
                {
                    this.SelectOperator.SetValue("@wherePUBLIC_HIST_AMT_5", this.wherePUBLIC_HIST_AMT_5, SqlDbType.Decimal);
                }
                if (sbstrSQL.ToString().Contains("@wherePUBLIC_HIST_DT_1 "))
                {
                    this.SelectOperator.SetValue("@wherePUBLIC_HIST_DT_1", this.wherePUBLIC_HIST_DT_1, SqlDbType.DateTime);
                }
                if (sbstrSQL.ToString().Contains("@wherePUBLIC_HIST_DT_2 "))
                {
                    this.SelectOperator.SetValue("@wherePUBLIC_HIST_DT_2", this.wherePUBLIC_HIST_DT_2, SqlDbType.DateTime);
                }
                if (sbstrSQL.ToString().Contains("@wherePUBLIC_HIST_DT_3 "))
                {
                    this.SelectOperator.SetValue("@wherePUBLIC_HIST_DT_3", this.wherePUBLIC_HIST_DT_3, SqlDbType.DateTime);
                }
                if (sbstrSQL.ToString().Contains("@wherePUBLIC_HIST_DT_4 "))
                {
                    this.SelectOperator.SetValue("@wherePUBLIC_HIST_DT_4", this.wherePUBLIC_HIST_DT_4, SqlDbType.DateTime);
                }
                if (sbstrSQL.ToString().Contains("@wherePUBLIC_HIST_DT_5 "))
                {
                    this.SelectOperator.SetValue("@wherePUBLIC_HIST_DT_5", this.wherePUBLIC_HIST_DT_5, SqlDbType.DateTime);
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
                myTable = this.SelectOperator.GetDataTable("PUBLIC_HIST");
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


        #region delete_for_public(string PAY_SEQ_11)
        public string delete_for_public(string PAY_SEQ_11)
        {
            #region Modify History
            /// <history>
            /// <design>
            /// <name>Cybersoft.COCA DaoGenerator</name>
            /// <date>2011/02/08 下午 02:21:02</date>
            #endregion
            try
            {
                #region set SQL statement
                StringBuilder sbstrSQL = new StringBuilder();
                sbstrSQL.Append(" DELETE PUBLIC_HIST where ");
                sbstrSQL.Append(" SUBSTRING(PAY_SEQ,1,11) = @PAY_SEQ_11 AND PAY_RESULT = '' ");
                sbstrSQL.Append(" AND MNT_USER  = @whereMNT_USER                            ");
                #endregion
                this.DeleteOperator.SqlText = sbstrSQL.ToString();
                #region repalce SQL parameter
                if (sbstrSQL.ToString().Contains("@PAY_SEQ_11"))
                {
                    this.DeleteOperator.SetValue("@PAY_SEQ_11", PAY_SEQ_11);
                }
                if (sbstrSQL.ToString().Contains("@whereMNT_USER"))
                {
                    this.DeleteOperator.SetValue("@whereMNT_USER", this.whereMNT_USER);
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
        #region delete_for_public(string PAY_SEQ_11, string strFlag)
        public string delete_for_public(string PAY_SEQ_11, string strFlag)
        {
            #region Modify History
            /// <history>
            /// <design>
            /// <name>Cybersoft.COCA DaoGenerator</name>
            /// <date>2011/02/08 下午 02:21:02</date>
            #endregion
            try
            {
                #region set SQL statement
                StringBuilder sbstrSQL = new StringBuilder();
                sbstrSQL.Append(" DELETE PUBLIC_HIST where ");
                if (strFlag == "Y")
                {
                    sbstrSQL.Append(" SUBSTRING(PAY_SEQ,1,12) = @PAY_SEQ_11   ");
                }
                else
                {
                    sbstrSQL.Append(" SUBSTRING(PAY_SEQ,1,11) = @PAY_SEQ_11   ");
                }

                sbstrSQL.Append(" AND PAY_RESULT = ''                     ");
                if (this.whereMNT_USER != null)
                {
                    sbstrSQL.Append(" AND MNT_USER=@whereMNT_USER ");
                }
                if (this.whereFILE_TRANSFER_TYPE != null)
                {
                    sbstrSQL.Append(" AND FILE_TRANSFER_TYPE = @whereFILE_TRANSFER_TYPE                      ");
                }
                #endregion
                this.DeleteOperator.SqlText = sbstrSQL.ToString();
                #region repalce SQL parameter
                if (sbstrSQL.ToString().Contains("@PAY_SEQ_11"))
                {
                    this.DeleteOperator.SetValue("@PAY_SEQ_11", PAY_SEQ_11);
                }
                if (sbstrSQL.ToString().Contains("@whereFILE_TRANSFER_TYPE"))
                {
                    this.DeleteOperator.SetValue("@whereFILE_TRANSFER_TYPE", this.whereFILE_TRANSFER_TYPE);
                }
                if (sbstrSQL.ToString().Contains("@whereMNT_USER"))
                {
                    this.DeleteOperator.SetValue("@whereMNT_USER", this.whereMNT_USER);
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
        #region join_public_apply
        public string join_public_apply()
        {
            #region Modify History
            /// <history>
            /// <design>
            /// <name>Cybersoft MooreYang</name>
            /// <date>2011/2/9 上午 14:50:00</date>
            #endregion

            try
            {
                #region  set SQL statement
                StringBuilder sbstrSQL = new StringBuilder();
                sbstrSQL.Append("SELECT H.PAY_TYPE, H.PAY_NBR, H.PAY_AMT, H.PAY_SEQ, ");
                sbstrSQL.Append("       A.PAY_CARD_NBR, A.PAY_TYPE, A.BU, A.PRODUCT, A.CARD_PRODUCT, P.CARD_GROUP, ");
                sbstrSQL.Append("       A.ACCT_NBR, A.PAY_CARD_NBR_PREV, A.PAY_ACCT_NBR_PREV, A.CUST_SEQ, ");
                //sbstrSQL.Append("       A.EXPIR_DTE, A.APPLY_DTE, A.VAILD_FLAG, A.SEND_MSG_FLAG, C.CTL_CODE, D.CARD_VALID, ");
                sbstrSQL.Append("       A.EXPIR_DTE, A.APPLY_DTE, ISNULL(A.VAILD_FLAG,'') VAILD_FLAG, A.SEND_MSG_FLAG, C.CTL_CODE, ISNULL(D.CARD_VALID,'') CARD_VALID, ");
                sbstrSQL.Append("       ISNULL(Convert(varchar(8), C.EXPIR_DTE, 112), '19000101') CARD_EXPIR_DTE, ");
                sbstrSQL.Append("       C.OPEN_FLAG, C.EXPIR_DTE_LAST, C.PREV_OPEN, ISNULL(L.CREDIT_AVAIL, '0.00') CREDIT_AVAIL, ");
                sbstrSQL.Append("       ISNULL(C.OPEN_FLAG,'0')  OPEN_FLAG,  ISNULL(C.EXPIR_DTE_LAST, '1900-01-01') EXPIR_DTE_LAST, C.PREV_OPEN  ");
                //sbstrSQL.Append("(SELECT * FROM PUBLIC_HIST WHERE TRANS_DTE = @TRANS_DTE AND PAY_RESULT = '') H ");
                sbstrSQL.Append("FROM (SELECT * FROM PUBLIC_HIST WHERE TRANS_DTE = @TRANS_DTE AND PAY_RESULT = '') H ");
                //sbstrSQL.Append("LEFT JOIN (SELECT * FROM (SELECT *, ROW_NUMBER() OVER(PARTITION BY PAY_TYPE, PAY_CARD_NBR, PAY_NBR                          ");
                //sbstrSQL.Append("                                                          ORDER BY PAY_TYPE, PAY_CARD_NBR, PAY_NBR, VAILD_FLAG DESC) ROW_ID ");
                sbstrSQL.Append("LEFT JOIN (SELECT * FROM (SELECT *, ROW_NUMBER() OVER(PARTITION BY PAY_TYPE, PAY_NBR ,CARD_PRODUCT       ");
                sbstrSQL.Append("                                                          ORDER BY PAY_TYPE,PAY_NBR ,CARD_PRODUCT, VAILD_FLAG DESC) ROW_ID ");
                sbstrSQL.Append("                            FROM PUBLIC_APPLY) T                                                        ");
                sbstrSQL.Append("                    WHERE T.ROW_ID = 1                                                                  ");
                sbstrSQL.Append("          ) A ON A.PAY_TYPE = H.PAY_TYPE AND A.PAY_NBR = H.PAY_NBR                                  ");
                sbstrSQL.Append("             AND A.CARD_PRODUCT = H.CARD_PRODUCT AND A.PAY_CARD_NBR = H.PAY_CARD_NBR AND A.BU = H.BU    ");
                //sbstrSQL.Append("           FROM PUBLIC_APPLY) T WHERE T.ROW_ID = 1) A ON A.PAY_TYPE = H.PAY_TYPE AND A.PAY_NBR = H.PAY_NBR AND A.CARD_PRODUCT = H.CARD_PRODUCT AND SUBSTRING(A.PAY_CARD_NBR,1,13) = SUBSTRING(H.PAY_CARD_NBR,1,13) ");
                //sbstrSQL.Append("           FROM PUBLIC_APPLY) T WHERE T.ROW_ID = 1) A ON A.PAY_TYPE = H.PAY_TYPE AND A.PAY_NBR = H.PAY_NBR AND A.CARD_PRODUCT = H.CARD_PRODUCT ");
                //sbstrSQL.Append("LEFT JOIN CARD_INF C      ON  C.BU = A.BU AND C.CARD_PRODUCT = A.CARD_PRODUCT AND C.CARD_NBR = A.PAY_CARD_NBR_PREV ");
                sbstrSQL.Append("LEFT JOIN CARD_INF C      ON  C.BU = A.BU AND C.CARD_NBR = A.PAY_CARD_NBR_PREV ");
                sbstrSQL.Append("LEFT JOIN SETUP_PRODUCT P ON  P.BU = A.BU AND P.PRODUCT = A.CARD_PRODUCT ");
                sbstrSQL.Append("LEFT JOIN SETUP_CTLCODE D ON  D.CTL_CODE = C.CTL_CODE ");
                sbstrSQL.Append("LEFT JOIN CARD_LIMIT L    ON  L.BU = C.BU AND L.CARD_NBR = C.CARD_NBR ");
                sbstrSQL.Append("WHERE H.PAY_TYPE NOT IN ('0006', '0021', '0022', '0023') ");
                #endregion
                this.SelectOperator.SqlText = sbstrSQL.ToString();
                #region repalce SQL parameter
                if (sbstrSQL.ToString().Contains("@TRANS_DTE"))
                {
                    this.SelectOperator.SetValue("@TRANS_DTE", strWhereTRANS_DTE);
                }
                #endregion
                //myTable set to DataTable object
                myTable = this.SelectOperator.GetDataTable("PUBLIC_HIST");
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
        #region query_parking
        public string query_parking()
        {
            #region Modify History
            /// <history>
            /// <design>
            /// <name>Cybersoft MooreYang</name>
            /// <date>2011/2/9 上午 14:50:00</date>
            #endregion

            try
            {
                #region  set SQL statement
                StringBuilder sbstrSQL = new StringBuilder();
                sbstrSQL.Append("SELECT H.*, ISNULL(C.CARD_PRODUCT, '') CARD_PRODUCT, ISNULL(C.CTL_CODE, '') CTL_CODE, ISNULL(D.CARD_VALID, '') CARD_VALID, ISNULL(P.CARD_GROUP, '') CARD_GROUP, ");
                sbstrSQL.Append("       C.EXPIR_DTE CARD_EXPIR_DTE, OPEN_FLAG, C.EXPIR_DTE_LAST  CARD_EXPIR_DTE_LAST, PREV_OPEN, ");
                sbstrSQL.Append("       ISNULL(L.CREDIT_AVAIL, 0.00) CREDIT_AVAIL FROM ( ");
                sbstrSQL.Append("SELECT * FROM PUBLIC_HIST ");
                sbstrSQL.Append("WHERE PAY_TYPE IN ('0006', '0021', '0022', '0023') ");
                //sbstrSQL.Append("  AND MNT_DT = @whereMNT_DT AND MNT_USER = 'PBBPKI001') H ");
                sbstrSQL.Append("  AND TRANS_DTE = @TRANS_DTE AND PAY_RESULT = '') H ");
                sbstrSQL.Append("LEFT JOIN CARD_INF C ON C.CARD_NBR = H.PAY_CARD_NBR ");
                sbstrSQL.Append("LEFT JOIN SETUP_PRODUCT P ON P.BU = C.BU AND P.PRODUCT = C.CARD_PRODUCT ");
                sbstrSQL.Append("LEFT JOIN SETUP_CTLCODE D ON D.CTL_CODE = C.CTL_CODE ");
                sbstrSQL.Append("LEFT JOIN CARD_LIMIT L ON L.BU = C.BU AND L.CARD_NBR = C.CARD_NBR ");
                #endregion
                this.SelectOperator.SqlText = sbstrSQL.ToString();
                #region repalce SQL parameter
                //if (sbstrSQL.ToString().Contains("@whereMNT_DT"))
                //{
                //    this.SelectOperator.SetValue("@whereMNT_DT", this.whereMNT_DT);
                //}
                if (sbstrSQL.ToString().Contains("@TRANS_DTE"))
                {
                    this.SelectOperator.SetValue("@TRANS_DTE", this.whereTRANS_DTE);
                }
                #endregion
                //myTable set to DataTable object
                myTable = this.SelectOperator.GetDataTable("PUBLIC_HIST");
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
        #region query_parking_RTN(撈取停車費退費交易)
        public string query_parking_RTN()
        {
            #region Modify History
            /// <history>
            /// <design>
            /// <name>Cybersoft MooreYang</name>
            /// <date>2011/2/9 上午 14:50:00</date>
            #endregion

            try
            {
                #region  set SQL statement
                StringBuilder sbstrSQL = new StringBuilder();
                sbstrSQL.Append("SELECT H.*, ISNULL(C.CARD_PRODUCT, '') CARD_PRODUCT, ISNULL(C.CTL_CODE, '') CTL_CODE, ");
                sbstrSQL.Append("       ISNULL(C.EXPIR_DTE, '1900-01-01') CARD_EXPIR_DTE FROM ( ");
                sbstrSQL.Append("        SELECT * FROM PUBLIC_HIST ");
                sbstrSQL.Append("         WHERE PAY_TYPE IN ('0006', '0021', '0022', '0023') ");
                //sbstrSQL.Append("           AND REVERSAL_FLAG = 'R' AND MNT_USER = 'PBBPKI002') H ");
                sbstrSQL.Append("           AND REVERSAL_FLAG = 'R' AND TRANS_DTE = @TRANS_DTE AND PAY_RESULT = '') H ");
                sbstrSQL.Append("  LEFT JOIN CARD_INF C ON C.BU = H.BU AND C.CARD_PRODUCT = H.CARD_PRODUCT AND  C.CARD_NBR = H.PAY_CARD_NBR ");
                #endregion
                this.SelectOperator.SqlText = sbstrSQL.ToString();
                #region repalce SQL parameter
                //if (sbstrSQL.ToString().Contains("@whereMNT_DT"))
                //{
                //    this.SelectOperator.SetValue("@whereMNT_DT", this.whereMNT_DT);
                //}
                if (sbstrSQL.ToString().Contains("@TRANS_DTE"))
                {
                    this.SelectOperator.SetValue("@TRANS_DTE", this.whereTRANS_DTE);
                }
                #endregion
                //myTable set to DataTable object
                myTable = this.SelectOperator.GetDataTable("PUBLIC_HIST");
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
        #region query_card_inf
        public string query_card_inf(String BU, String CARD_PRODUCT, String ACCT_NBR, String EXP_CARD_NBR)
        {
            #region Modify History
            /// <history>
            /// <design>
            /// <name>Cybersoft MooreYang</name>
            /// <date>2011/2/9 上午 16:50:00</date>
            #endregion

            try
            {
                #region  set SQL statement
                //找出未轉卡之正卡且按扣款等級排序
                StringBuilder sbstrSQL = new StringBuilder();
                sbstrSQL.Append("SELECT * FROM CARD_INF C ");
                sbstrSQL.Append("LEFT JOIN SETUP_CTLCODE S ON S.CTL_CODE = C.CTL_CODE ");
                sbstrSQL.Append("LEFT JOIN CARD_LIMIT L ON L.BU = c.BU AND L.CARD_NBR = C.CARD_NBR ");
                sbstrSQL.Append("WHERE C.BU = @BU AND C.CARD_PRODUCT = @CARD_PRODUCT AND C.ACCT_NBR = @ACCT_NBR AND C.CARD_NBR != @EXP_CARD_NBR ");
                sbstrSQL.Append("  AND S.CARD_VALID = 'Y' AND C.CARD_FLAG = 'P' AND C.TC_CARD_NBR = '' ");
                //sbstrSQL.Append(" AND ((C.EXPIR_DTE > GETDATE() AND C.OPEN_FLAG != '0') OR (C.EXPIR_DTE_LAST > GETDATE() AND C.PREV_OPEN != '0')) ");
                sbstrSQL.Append("ORDER BY C.EXPIR_DTE DESC ");
                #endregion
                this.SelectOperator.SqlText = sbstrSQL.ToString();
                #region repalce SQL parameter
                if (sbstrSQL.ToString().Contains("@BU"))
                {
                    this.SelectOperator.SetValue("@BU", BU);
                }
                if (sbstrSQL.ToString().Contains("@CARD_PRODUCT"))
                {
                    this.SelectOperator.SetValue("@CARD_PRODUCT", CARD_PRODUCT);
                }
                if (sbstrSQL.ToString().Contains("@ACCT_NBR"))
                {
                    this.SelectOperator.SetValue("@ACCT_NBR", ACCT_NBR);
                }
                if (sbstrSQL.ToString().Contains("@EXP_CARD_NBR"))
                {
                    this.SelectOperator.SetValue("@EXP_CARD_NBR", EXP_CARD_NBR);
                }
                #endregion
                //myTable set to DataTable object
                myTable = this.SelectOperator.GetDataTable("PUBLIC_HIST");
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
        #region query_trans_dte
        public string query_trans_dte(String FLAG, String TRANS_DTE)
        {
            #region Modify History
            /// <history>
            /// <design>
            /// <name>Cybersoft MooreYang</name>
            /// <date>2011/2/9 上午 16:50:00</date>
            #endregion

            try
            {
                #region  set SQL statement
                StringBuilder sbstrSQL = new StringBuilder();
                sbstrSQL.Append("SELECT DISTINCT P.TRANS_DTE,P.PAY_TYPE,P.BU,P.ACCT_NBR,P.PAY_CARD_NBR,P.EXPIR_DTE,P.PAY_NBR,P.PAY_AMT,P.PAY_DTE,P.PAY_SEQ,P.PAY_RESULT,A.CUST_ID  ");
                //sbstrSQL.Append("FROM PUBLIC_HIST P  ");
                sbstrSQL.Append("FROM (select * from  PUBLIC_HIST  ");
                switch (FLAG)
                {
                    case "T":
                        sbstrSQL.Append("WHERE TRANS_DTE = @TRANS_DTE ");
                        break;
                    case "A":
                        sbstrSQL.Append("WHERE TRANS_DTE <= @TRANS_DTE ");
                        break;
                }
                //sbstrSQL.Append("AND (P.PAY_RESULT = 'N001' OR SUBSTRING(P.PAY_RESULT,1,1) = 'I') AND P.ACCT_NBR = A.USER_NBR_2");
                sbstrSQL.Append(" AND (PAY_RESULT = 'N001')  )P,ACCT_LINK A ");
                sbstrSQL.Append("WHERE P.ACCT_NBR = A.ACCT_NBR AND P.BU = A.BU AND A.CARD_FLAG = 'P'");
                sbstrSQL.Append("order by P.PAY_AMT,P.PAY_NBR    "); //此排序為的是盡量避免同卡號排在一起進行批次授權

                #endregion
                this.SelectOperator.SqlText = sbstrSQL.ToString();
                #region repalce SQL parameter
                if (sbstrSQL.ToString().Contains("@TRANS_DTE"))
                {
                    this.SelectOperator.SetValue("@TRANS_DTE", TRANS_DTE);
                }

                #endregion
                //myTable set to DataTable object
                myTable = this.SelectOperator.GetDataTable("PUBLIC_HIST");
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
        #region query_for_fee
        public string query_for_fee(DateTime PRE_MON_S, DateTime PRE_MON_E)
        {
            #region Modify History
            /// <history>
            /// <design>
            /// <name>Cybersoft MooreYang</name>
            /// <date>2011/2/27 上午 16:50:00</date>
            #endregion

            try
            {
                #region  set SQL statement
                ////統計上個月各公用事業代繳成功筆數
                ////20100705 以入帳日且代繳成功者做統計
                //StringBuilder sbstrSQL = new StringBuilder();
                ////sbstrSQL.Append("SELECT PAY_TYPE,PAY_DTE, COUNT(*) AS COUNT FROM PUBLIC_HIST  ");
                ////sbstrSQL.Append("WHERE PAY_DTE >= @PRE_MON_S AND PAY_DTE < @PRE_MON_E AND PAY_RESULT = '0000' ");
                ////sbstrSQL.Append("GROUP BY PAY_TYPE,PAY_DTE ");
                ////sbstrSQL.Append("ORDER BY PAY_DTE ");
                //sbstrSQL.Append("SELECT SUBSTRING(SOURCE_CODE,8,3) AS PAY_TYPE,POSTING_DTE AS PAY_DTE,COUNT(*) AS COUNT FROM TX_UNBILL  ");
                //sbstrSQL.Append("WHERE POSTING_DTE >= @PRE_MON_S AND POSTING_DTE < @PRE_MON_E AND SUBSTRING(SOURCE_CODE,1,2) = 'PU' ");
                //sbstrSQL.Append("GROUP BY SOURCE_CODE,POSTING_DTE ");
                //sbstrSQL.Append("UNION ");
                //sbstrSQL.Append("SELECT SUBSTRING(SOURCE_CODE,8,3) AS PAY_TYPE,POSTING_DTE AS PAY_DTE,COUNT(*) AS COUNT FROM TX_BILL ");
                //sbstrSQL.Append("WHERE POSTING_DTE >= @PRE_MON_S AND POSTING_DTE < @PRE_MON_E AND SUBSTRING(SOURCE_CODE,1,2) = 'PU' ");
                //sbstrSQL.Append("GROUP BY SOURCE_CODE,POSTING_DTE ");
                //sbstrSQL.Append("ORDER BY POSTING_DTE ");
                #endregion
                #region  set SQL statement
                //統計上個月各公用事業代繳成功筆數
                //以入帳日且代繳成功者做統計
                StringBuilder sbstrSQL = new StringBuilder();
                sbstrSQL.Append(" SELECT K.*,c.COST_AMT as FEE                                                                                                              ");
                sbstrSQL.Append("   FROM (                                                                                                              ");
                sbstrSQL.Append("          SELECT ISNULL(B.PAY_TYPE,'') PAY_TYPE,A.SOURCE_CODE,A.POSTING_DTE AS PAY_DTE,COUNT(*) AS COUNT               ");
                sbstrSQL.Append("           FROM (SELECT SOURCE_CODE,POSTING_DTE FROM  TX_UNBILL                                                        ");
                sbstrSQL.Append("                  WHERE POSTING_DTE >= @PRE_MON_S AND POSTING_DTE < @PRE_MON_E AND SUBSTRING(SOURCE_CODE,1,2) = 'PU'   ");
                sbstrSQL.Append("                 UNION                                                                                                 ");
                sbstrSQL.Append("                 SELECT SOURCE_CODE,POSTING_DTE FROM  TX_BILL                                                          ");
                sbstrSQL.Append("                  WHERE POSTING_DTE >= @PRE_MON_S AND POSTING_DTE < @PRE_MON_E AND SUBSTRING(SOURCE_CODE,1,2) = 'PU'   ");
                sbstrSQL.Append("                )A                                                                                                     ");
                sbstrSQL.Append("          join SETUP_PUBLIC  B on a.SOURCE_CODE = b.SOURCE_CODE                                                        ");
                sbstrSQL.Append("           GROUP BY PAY_TYPE,A.SOURCE_CODE,POSTING_DTE                                                                 ");
                sbstrSQL.Append("         )K                                                                                                            ");
                sbstrSQL.Append("   join SETUP_GL C ON K.SOURCE_CODE = C.SOURCE_CODE AND C.CODE = '0001' AND C.MT_TYPE = 'D'                            ");
                sbstrSQL.Append("   ORDER BY K.PAY_DTE                                                                                                  ");
                #endregion

                this.SelectOperator.SqlText = sbstrSQL.ToString();
                #region repalce SQL parameter
                if (sbstrSQL.ToString().Contains("@PRE_MON_S"))
                {
                    this.SelectOperator.SetValue("@PRE_MON_S", PRE_MON_S);
                }
                if (sbstrSQL.ToString().Contains("@PRE_MON_E"))
                {
                    this.SelectOperator.SetValue("@PRE_MON_E", PRE_MON_E);
                }

                #endregion
                //myTable set to DataTable object
                myTable = this.SelectOperator.GetDataTable("PUBLIC_HIST");
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
        #region query_for_date()
        public string query_for_date()
        {
            #region Modify History
            /// <history>
            /// <design>
            /// <name>Cybersoft.COCA DaoGenerator</name>
            /// <date>2011/3/7 上午 12:26:10</date>
            #endregion
            try
            {
                #region set SQL statement
                StringBuilder sbstrSQL = new StringBuilder();
                //sbstrSQL.Append("select TOP 300 b.CUST_ID,c.NAME,a.* ");
                sbstrSQL.Append("select TOP 300 b.CUST_ID,c.NAME,a.* ,d.DESCR AS STR_PAY_TYPE ,e.DESCR as PAY_RESULT_DESCR ");
                sbstrSQL.Append("from PUBLIC_HIST a ");
                sbstrSQL.Append("join ID_VIEW b on a.CUST_SEQ=b.CUST_SEQ ");
                sbstrSQL.Append("join CUST_INF c on a.BU=c.BU and a.CUST_SEQ=c.CUST_NBR ");
                sbstrSQL.Append("join SETUP_PUBLIC d on d.PAY_TYPE = a.PAY_TYPE ");
                sbstrSQL.Append("join SETUP_REJECT e ON e.REJECT_GROUP = 'PUBLIC' AND e.REJECT_CODE = a.PAY_RESULT ");
                sbstrSQL.Append("where 1=1 ");

                if (this.whereBU != null)
                {
                    sbstrSQL.Append(" and a.BU = @whereBU ");
                }
                if (this.whereACCT_NBR != null)
                {
                    sbstrSQL.Append(" and a.ACCT_NBR = @whereACCT_NBR ");
                }
                if (this.wherePAY_CARD_NBR != null)
                {
                    sbstrSQL.Append(" and a.PAY_CARD_NBR = @wherePAY_CARD_NBR ");
                }
                if (this.wherePAY_NBR != null)
                {
                    sbstrSQL.Append(" and a.PAY_NBR LIKE '%" + this.wherePAY_NBR + "%' ");
                }
                if (this.wherePAY_DTE_ST > dateStart)
                {
                    sbstrSQL.Append(" and a.PAY_DTE >= @wherePAY_DTE_ST ");
                }
                if (this.wherePAY_DTE_ED > dateStart)
                {
                    sbstrSQL.Append(" and a.PAY_DTE < @wherePAY_DTE_ED ");
                }
                if (this.whereTRANS_DTE_ST != null)
                {
                    sbstrSQL.Append(" and a.TRANS_DTE >= @whereTRANS_DTE_ST ");
                }
                if (this.whereTRANS_DTE_ED != null)
                {
                    sbstrSQL.Append(" and a.TRANS_DTE < @whereTRANS_DTE_ED ");
                }
                if (this.wherePAY_TYPE != null)
                {
                    sbstrSQL.Append(" and a.PAY_TYPE = @wherePAY_TYPE ");
                }
                if (this.wherePAY_RESULT != null)
                {
                    sbstrSQL.Append(" and a.PAY_RESULT = @wherePAY_RESULT ");
                }
                sbstrSQL.Append("order by a.TRANS_DTE DESC, a.PAY_SEQ ASC, a.PAY_TYPE ASC, a.PAY_NBR ASC ");
                #endregion
                this.SelectOperator.SqlText = sbstrSQL.ToString();
                #region replace SQL parameter
                if (sbstrSQL.ToString().Contains("@whereBU "))
                {
                    this.SelectOperator.SetValue("@whereBU", this.whereBU, SqlDbType.VarChar);
                }
                if (sbstrSQL.ToString().Contains("@whereACCT_NBR "))
                {
                    this.SelectOperator.SetValue("@whereACCT_NBR", this.whereACCT_NBR, SqlDbType.VarChar);
                }
                if (sbstrSQL.ToString().Contains("@wherePAY_CARD_NBR "))
                {
                    this.SelectOperator.SetValue("@wherePAY_CARD_NBR", this.wherePAY_CARD_NBR);
                }
                if (sbstrSQL.ToString().Contains("@wherePAY_DTE_ST "))
                {
                    this.SelectOperator.SetValue("@wherePAY_DTE_ST", this.wherePAY_DTE_ST);
                }
                if (sbstrSQL.ToString().Contains("@wherePAY_DTE_ED "))
                {
                    this.SelectOperator.SetValue("@wherePAY_DTE_ED", this.wherePAY_DTE_ED);
                }
                if (sbstrSQL.ToString().Contains("@whereTRANS_DTE_ST "))
                {
                    this.SelectOperator.SetValue("@whereTRANS_DTE_ST", this.whereTRANS_DTE_ST);
                }
                if (sbstrSQL.ToString().Contains("@whereTRANS_DTE_ED "))
                {
                    this.SelectOperator.SetValue("@whereTRANS_DTE_ED", this.whereTRANS_DTE_ED);
                }
                if (sbstrSQL.ToString().Contains("@wherePAY_TYPE "))
                {
                    this.SelectOperator.SetValue("@wherePAY_TYPE", this.wherePAY_TYPE);
                }
                if (sbstrSQL.ToString().Contains("@wherePAY_RESULT "))
                {
                    this.SelectOperator.SetValue("@wherePAY_RESULT", this.wherePAY_RESULT);
                }
                #endregion
                //myTable set to DataTable object
                myTable = this.SelectOperator.GetDataTable("PUBLIC_HIST");
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
        #region query_for_detail()
        public string query_for_detail()
        {
            #region Modify History
            /// <history>
            /// <design>
            /// <name>Cybersoft.COCA DaoGenerator</name>
            /// <date>2011/3/7 上午 12:26:10</date>
            #endregion
            try
            {
                #region set SQL statement
                StringBuilder sbstrSQL = new StringBuilder();
                //sbstrSQL.Append("select TOP 100 b.CUST_ID,c.NAME,a.* from PUBLIC_HIST a,ACCT_LINK b,CUST_INF c ");
                //sbstrSQL.Append("where a.CUST_SEQ = b.ACCT_NBR and b.CARD_FLAG = 'P' and a.CUST_SEQ = c.CUST_NBR ");
                sbstrSQL.Append("select TOP 100 b.CUST_ID,c.NAME,a.* from PUBLIC_HIST a,ID_VIEW b,CUST_INF c ");
                sbstrSQL.Append("where a.CUST_SEQ = b.CUST_SEQ and a.CUST_SEQ = c.CUST_NBR  ");
                sbstrSQL.Append("and a.TRANS_DTE = '" + whereTRANS_DTE + "' and a.PAY_SEQ = '" + wherePAY_SEQ + "'");
                #endregion
                this.SelectOperator.SqlText = sbstrSQL.ToString();
                //myTable set to DataTable object
                myTable = this.SelectOperator.GetDataTable("PUBLIC_HIST");
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
        #region query_HIST_MSG(string[] SET_PARAS)
        public string query_HIST_MSG(int intMSGCODE, string[] SET_PARAS)
        {
            #region Modify History
            /// <history>
            /// <design>
            /// <name>Cybersoft.COCA DaoGenerator</name>
            /// <date>2011/2/10 下午 04:02:21</date>
            #endregion
            try
            {
                #region set SQL statement
                StringBuilder sbstrSQL = new StringBuilder();
                sbstrSQL.Append("SELECT a.*, b.CUST_ID, c.NAME, REPLACE(c.CELL_PHONE_1,'-','') CELL_PHONE_1 FROM PUBLIC_HIST a ");
                sbstrSQL.Append("LEFT JOIN ACCT_LINK b ON b.BU = a.BU AND b.CUST_SEQ = a.CUST_SEQ ");
                sbstrSQL.Append("LEFT JOIN CUST_INF c ON c.BU = a.BU AND c.CUST_NBR = a.CUST_SEQ ");
                sbstrSQL.Append("where 1=1 and a.MNT_DT = @whereMNT_DT and a.MNT_USER = 'PBBUPH001' ");
                if (SET_PARAS[0] == "ERR0")
                {
                    sbstrSQL.Append(" and (a.PAY_RESULT != 'O000') ");
                }
                else
                {
                    for (int i = 0; i < intMSGCODE; i++)
                    {
                        if (i == 0)
                        {
                            sbstrSQL.Append(" and (");
                        }
                        if (i != 0)
                        {
                            sbstrSQL.Append(" or ");
                        }
                        sbstrSQL.Append(" a.PAY_RESULT = '" + SET_PARAS[i] + "'");
                    }
                    if (intMSGCODE > 0)
                    {
                        sbstrSQL.Append(" ) ");
                    }
                }
                sbstrSQL.Append("ORDER BY b.CUST_ID ");
                #endregion
                this.SelectOperator.SqlText = sbstrSQL.ToString();
                #region replace SQL parameter
                if (sbstrSQL.ToString().Contains("@whereMNT_DT "))
                {
                    this.SelectOperator.SetValue("@whereMNT_DT", this.whereMNT_DT);
                }
                #endregion
                //myTable set to DataTable object
                myTable = this.SelectOperator.GetDataTable("PUBLIC_HIST");
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
        #region query_for_REPORT()
        public string query_for_REPORT()
        {
            #region Modify History
            /// <history>
            /// <design>
            /// <name>Cybersoft.COCA DaoGenerator</name>
            /// <date>2011/3/7 上午 12:26:10</date>
            #endregion
            try
            {
                #region set SQL statement
                StringBuilder sbstrSQL = new StringBuilder();
                sbstrSQL.Append("SELECT *, ISNULL(C.NAME,'') NAME, ISNULL(A.CUST_ID,'') CUST_ID FROM PUBLIC_HIST H ");
                //sbstrSQL.Append("LEFT JOIN CUST_INF C ON C.BU = H.BU AND C.CUST_NBR = H.CUST_SEQ ");
                //sbstrSQL.Append("LEFT JOIN ACCT_LINK A ON A.BU = H.BU AND A.CUST_SEQ = H.CUST_SEQ ");
                sbstrSQL.Append("LEFT JOIN CUST_INF C ON C.BU = H.BU AND C.CUST_NBR = H.ACCT_NBR ");
                sbstrSQL.Append("LEFT JOIN ID_VIEW A  ON A.CUST_SEQ = H.ACCT_NBR ");
                sbstrSQL.Append("WHERE 1=1 ");
                if (this.wherePAY_SEQ != null)
                {
                    sbstrSQL.Append(" and H.PAY_SEQ=@wherePAY_SEQ ");
                }
                #endregion
                this.SelectOperator.SqlText = sbstrSQL.ToString();
                #region replace SQL parameter
                if (sbstrSQL.ToString().Contains("@wherePAY_SEQ "))
                {
                    this.SelectOperator.SetValue("@wherePAY_SEQ", this.wherePAY_SEQ);
                }
                #endregion
                //myTable set to DataTable object
                myTable = this.SelectOperator.GetDataTable("PUBLIC_HIST");
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
        #region query_for_MBS_PAYMENT()產生商家入扣檔,寫入MBS_PAYMENT
        public string query_for_MBS_PAYMENT()
        {
            #region Modify History
            /// <history>
            /// <design>
            /// <name>Cybersoft.COCA DaoGenerator</name>
            /// <date>2012/6/7 上午 12:26:10</date>
            #endregion
            try
            {
                #region set SQL statement
                StringBuilder sbstrSQL = new StringBuilder();
                sbstrSQL.Append("SELECT P.SEQ,P.PAY_TYPE,P.REVERSAL_FLAG, C.UT_MER_NO,P.CNT,P.PAY_AMT_TOT ,C.UT_REMARK ");
                sbstrSQL.Append("  FROM                                                                ");
                sbstrSQL.Append("   (select RANK() OVER (ORDER BY X.PAY_TYPE) SEQ , X.PAY_TYPE, X.REVERSAL_FLAG, COUNT(*) CNT, SUM(X.PAY_AMT) PAY_AMT_TOT  ");
                sbstrSQL.Append("      from                                                                                             ");
                sbstrSQL.Append("      (select PAY_TYPE , REVERSAL_FLAG, PAY_AMT                                                         ");
                sbstrSQL.Append("         from PUBLIC_HIST                                                                              ");
                sbstrSQL.Append("        where TRANS_DTE = @whereTRANS_DTE and PAY_RESULT = '0000'                                      ");
                sbstrSQL.Append("          AND PAY_TYPE IN ('0001','0002','0003','0004','0006','0008','0016','0021','0022','0023','0035') ");
                sbstrSQL.Append("      )X GROUP BY X.PAY_TYPE, X.REVERSAL_FLAG                                                           ");
                sbstrSQL.Append("   )P, MBS.dbo.MBS_UTLITY C    ");
                sbstrSQL.Append(" WHERE P.PAY_TYPE = C.UT_CODE  ");
                sbstrSQL.Append("UNION ALL ");
                sbstrSQL.Append("SELECT '99' SEQ, 'ACH1' PAY_TYPE, '' REVERSAL_FLAG, T1.UT_MER_NO, T1.CNT, T1.PAY_AMT_TOT, 'ACH1' REMARK ");
                sbstrSQL.Append("  FROM (");
                sbstrSQL.Append("         SELECT COUNT(*) CNT, SUM(PAY_AMT) PAY_AMT_TOT, '009518116090024' UT_MER_NO ");
                sbstrSQL.Append("           FROM PUBLIC_HIST ");
                sbstrSQL.Append("          WHERE TRANS_DTE = @whereTRANS_DTE and PAY_RESULT = '0000' ");
                sbstrSQL.Append("            AND PAY_TYPE NOT IN ('0001','0002','0003','0004','0006','0008','0016','0021','0022','0023','0035')) T1 ");
                #endregion
                this.SelectOperator.SqlText = sbstrSQL.ToString();
                #region replace SQL parameter
                if (sbstrSQL.ToString().Contains("@whereTRANS_DTE "))
                {
                    this.SelectOperator.SetValue("@whereTRANS_DTE", this.whereTRANS_DTE);
                }

                if (sbstrSQL.ToString().Contains("@wherePAY_RESULT "))
                {
                    this.SelectOperator.SetValue("@wherePAY_RESULT", this.wherePAY_RESULT);
                }

                #endregion
                //myTable set to DataTable object
                myTable = this.SelectOperator.GetDataTable("PUBLIC_HIST");
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
        #region query_for_MERCHANT_BOOK_UPDATE()產生商家入扣檔,寫入MERCHANT_BOOK_UPDATE
        public string query_for_MERCHANT_BOOK_UPDATE()
        {
            #region Modify History
            /// <history>
            /// <design>
            /// <name>Cybersoft.COCA DaoGenerator</name>
            /// <date>2012/6/7 上午 12:26:10</date>
            #endregion
            try
            {
                #region set SQL statement
                StringBuilder sbstrSQL = new StringBuilder();
                sbstrSQL.Append("SELECT P.SEQ,P.PAY_TYPE,P.REVERSAL_FLAG, C.UT_MER_NO,P.CNT,P.PAY_AMT_TOT ,C.UT_REMARK ,ISNULL(D.OPENNAME,'') DESCR,ISNULL(D.BUS_BANK_ACCOUNT,'') BANK_ACCT_NBR ");
                sbstrSQL.Append("  FROM                                                                ");
                sbstrSQL.Append("   (select RANK() OVER (ORDER BY X.PAY_TYPE) SEQ , X.PAY_TYPE, X.REVERSAL_FLAG, COUNT(*) CNT, SUM(X.PAY_AMT) PAY_AMT_TOT  ");
                sbstrSQL.Append("      from                                                                                             ");
                sbstrSQL.Append("      (select PAY_TYPE , REVERSAL_FLAG, PAY_AMT                                                         ");
                sbstrSQL.Append("         from PUBLIC_HIST                                                                              ");
                sbstrSQL.Append("        where                                                                                          ");
                sbstrSQL.Append("              TRANS_DTE = @whereTRANS_DTE and PAY_RESULT = '0000'  AND                                 ");
                sbstrSQL.Append("              PAY_TYPE IN ('0001','0002','0003','0004','0006','0008','0016','0021','0022','0023','0035') ");
                sbstrSQL.Append("      )X GROUP BY X.PAY_TYPE, X.REVERSAL_FLAG                                                           ");
                sbstrSQL.Append("   )P, MBS.dbo.MBS_UTLITY C LEFT JOIN MBS.dbo.MER_BAS D ON D.MER_NO = C.UT_MER_NO    ");
                sbstrSQL.Append(" WHERE P.PAY_TYPE = C.UT_CODE  ");
                sbstrSQL.Append("UNION ALL ");
                sbstrSQL.Append("SELECT '99' SEQ, 'ACH1' PAY_TYPE, '' REVERSAL_FLAG, T1.UT_MER_NO, T1.CNT, T1.PAY_AMT_TOT, 'ACH1' REMARK ,ISNULL(D.OPENNAME,'') DESCR,ISNULL(D.BUS_BANK_ACCOUNT,'') BANK_ACCT_NBR ");
                sbstrSQL.Append("  FROM (");
                sbstrSQL.Append("         SELECT COUNT(*) CNT, ISNULL(SUM(PAY_AMT),0) PAY_AMT_TOT, '009518116090024' UT_MER_NO ");
                sbstrSQL.Append("           FROM PUBLIC_HIST ");
                sbstrSQL.Append("          WHERE                                                     ");
                sbstrSQL.Append("                TRANS_DTE = @whereTRANS_DTE and PAY_RESULT = '0000' AND ");
                sbstrSQL.Append("                PAY_TYPE NOT IN ('0001','0002','0003','0004','0006','0008','0016','0021','0022','0023','0035')) T1  LEFT JOIN MBS.dbo.MER_BAS D ON D.mer_no = T1.UT_MER_NO    ");
                #endregion
                this.SelectOperator.SqlText = sbstrSQL.ToString();
                #region replace SQL parameter
                if (sbstrSQL.ToString().Contains("@whereTRANS_DTE "))
                {
                    this.SelectOperator.SetValue("@whereTRANS_DTE", this.whereTRANS_DTE);
                }

                if (sbstrSQL.ToString().Contains("@wherePAY_RESULT "))
                {
                    this.SelectOperator.SetValue("@wherePAY_RESULT", this.wherePAY_RESULT);
                }

                #endregion
                //myTable set to DataTable object
                myTable = this.SelectOperator.GetDataTable("PUBLIC_HIST");
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
        #region query_for_BXD0090C()產生代繳台電公司電費無法扣繳清單分行報表
        public string query_for_BXD0090C()
        {
            #region Modify History
            /// <history>
            /// <design>
            /// <name>Cybersoft.COCA DaoGenerator</name>
            /// <date>2012/6/12 上午 12:26:10</date>
            #endregion
            try
            {
                #region set SQL statement
                StringBuilder sbstrSQL = new StringBuilder();
                sbstrSQL.Append(" select SETUP_CODE.CODE AS [KEY],SETUP_CODE.ADD_VAL1 AS BR_NO, ");
                sbstrSQL.Append("  ISNULL(X.PAY_NBR,'==無該當==') AS PAY_NBR,                   ");
                sbstrSQL.Append("  ISNULL(X.PAY_AMT,0)       AS PAY_AMT ,                       ");
                sbstrSQL.Append("  ISNULL(X.PAY_DTE,'')      AS PAY_DTE ,                       ");
                sbstrSQL.Append("  ISNULL(X.PAY_ACCT_NBR,'') AS PAY_ACCT_NBR ,                  ");
                sbstrSQL.Append("  ISNULL(X.PAY_RESULT,'')   AS PAY_RESULT                      ");
                sbstrSQL.Append(" from SETUP_CODE                                               ");
                sbstrSQL.Append("   LEFT JOIN (                                                 ");
                sbstrSQL.Append("             SELECT SUBSTRING(PAY_NBR,1,2) AS EL_NO, PAY_NBR,CONVERT(varchar(7),(CONVERT(varchar(8),PAY_DTE,112)-19110000)) PAY_DTE,PAY_AMT,PAY_RESULT ,                      ");
                sbstrSQL.Append("                    CASE P.PRODUCT_SERVICE_3 WHEN NULL THEN '' ");
                sbstrSQL.Append("                    ELSE '77' + SUBSTRING(P.PRODUCT_SERVICE_3,1,2) + SUBSTRING(A.PAY_CARD_NBR,7,10) END PAY_ACCT_NBR ");
                sbstrSQL.Append("               FROM PUBLIC_HIST A LEFT JOIN SETUP_PRODUCT p                                                              ");
                sbstrSQL.Append("              ON P.BU = A.BU AND P.PRODUCT = A.CARD_PRODUCT                                                 ");
                sbstrSQL.Append("              WHERE A.PAY_TYPE = @wherePAY_TYPE  and a.PAY_RESULT != @wherePAY_RESULT  AND a.PAY_DTE = @wherePAY_DTE   ");
                sbstrSQL.Append("             ) X                                                                                               ");
                sbstrSQL.Append("        ON X.EL_NO = SETUP_CODE.CODE                                                                           ");
                sbstrSQL.Append(" where TYPE= 'EL_NO' AND TYPE_SUB = 'BRANCH'                                                                   ");
                sbstrSQL.Append(" ORDER BY SETUP_CODE.CODE                                                                                      ");

                #endregion
                this.SelectOperator.SqlText = sbstrSQL.ToString();
                #region replace SQL parameter
                if (sbstrSQL.ToString().Contains("@wherePAY_TYPE "))
                {
                    this.SelectOperator.SetValue("@wherePAY_TYPE", this.wherePAY_TYPE);
                }
                if (sbstrSQL.ToString().Contains("@wherePAY_RESULT "))
                {
                    this.SelectOperator.SetValue("@wherePAY_RESULT", this.wherePAY_RESULT);
                }
                if (sbstrSQL.ToString().Contains("@wherePAY_DTE "))
                {
                    this.SelectOperator.SetValue("@wherePAY_DTE", this.wherePAY_DTE);
                }

                #endregion
                //myTable set to DataTable object
                myTable = this.SelectOperator.GetDataTable("PUBLIC_HIST");
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
        #region query_for_BXD0012C()產生信用卡代繳電費(台電)扣帳資料報表
        public string query_for_BXD0012C()
        {
            #region Modify History
            /// <history>
            /// <design>
            /// <name>Cybersoft.COCA DaoGenerator</name>
            /// <date>2012/6/12 上午 12:26:10</date>
            #endregion
            try
            {
                #region set SQL statement
                StringBuilder sbstrSQL = new StringBuilder();
                sbstrSQL.Append(" select SETUP_CODE.ADD_VAL1 AS [KEY] ,SETUP_CODE.CODE AS BR_NO , ");
                sbstrSQL.Append("         ISNULL(X.ADDR_BRANCH,'5185')   AS ADDR_BRANCH ,       ");
                sbstrSQL.Append("         ISNULL(X.SUCC_CNT,0)   AS SUCC_CNT ,                  ");
                sbstrSQL.Append("         ISNULL(X.SUCC_AMT,0)   AS SUCC_AMT ,                  ");
                sbstrSQL.Append("         ISNULL(X.SUCC_CNT,0)*3   AS FEE_AMT ,                 ");
                sbstrSQL.Append("         ISNULL(X.FAIL_CNT,0)   AS FAIL_CNT ,                  ");
                sbstrSQL.Append("         ISNULL(X.FAIL_AMT,0)   AS FAIL_AMT,                   ");
                sbstrSQL.Append("         ISNULL(X.PAY_DTE,'19000101')   AS PAY_DTE         ");
                sbstrSQL.Append(" from SETUP_CODE                                               ");
                sbstrSQL.Append("   LEFT JOIN (                                                 ");
                sbstrSQL.Append("              SELECT BR_NO,EL_BR_NO,ADDR_BRANCH,SUM(SUCC_CNT) SUCC_CNT,SUM(SUCC_AMT) SUCC_AMT,SUM(FAIL_CNT) FAIL_CNT,SUM(FAIL_AMT) FAIL_AMT,PAY_DTE   ");
                sbstrSQL.Append("                FROM (select C.ADD_VAL1 AS BR_NO,SUBSTRING(A.PAY_NBR,1,2) EL_BR_NO,b.ADDR_BRANCH,A.PAY_DTE, ");
                sbstrSQL.Append("                             CASE  WHEN PAY_RESULT =  '0000' THEN 1 ELSE 0 END SUCC_CNT,          ");
                sbstrSQL.Append("                             CASE  WHEN PAY_RESULT =  '0000' THEN PAY_AMT ELSE 0 END SUCC_AMT,    ");
                sbstrSQL.Append("                             CASE  WHEN PAY_RESULT <> '0000' THEN 1 ELSE 0 END FAIL_CNT,          ");
                sbstrSQL.Append("                             CASE  WHEN PAY_RESULT <> '0000' THEN PAY_AMT ELSE 0 END FAIL_AMT     ");
                sbstrSQL.Append("                        from PUBLIC_HIST A,CUST_INF B,SETUP_CODE C                                ");
                sbstrSQL.Append("                       where A.PAY_TYPE = @wherePAY_TYPE  and A.PAY_DTE = @wherePAY_DTE           ");
                sbstrSQL.Append("                         and A.BU = B.BU and A.CUST_SEQ = B.CUST_NBR                              ");
                sbstrSQL.Append("                         and SUBSTRING(A.PAY_NBR,1,2) = C.CODE                                    ");
                sbstrSQL.Append("                         and C.TYPE= 'EL_NO' AND C.TYPE_SUB = 'BRANCH'                            ");
                sbstrSQL.Append("                     )B                                                                           ");
                sbstrSQL.Append("               GROUP BY BR_NO,EL_BR_NO,ADDR_BRANCH,PAY_DTE                                        ");
                sbstrSQL.Append("             )X ON X.EL_BR_NO = SETUP_CODE.CODE                                                   ");
                sbstrSQL.Append("  where TYPE= 'EL_NO' AND TYPE_SUB = 'BRANCH'                                                     ");
                sbstrSQL.Append("  ORDER BY SETUP_CODE.ADD_VAL1,SETUP_CODE.CODE,ADDR_BRANCH desc                                   ");
                #endregion
                this.SelectOperator.SqlText = sbstrSQL.ToString();
                #region replace SQL parameter
                if (sbstrSQL.ToString().Contains("@wherePAY_TYPE "))
                {
                    this.SelectOperator.SetValue("@wherePAY_TYPE", this.wherePAY_TYPE);
                }
                if (sbstrSQL.ToString().Contains("@wherePAY_DTE "))
                {
                    this.SelectOperator.SetValue("@wherePAY_DTE", this.wherePAY_DTE);
                }

                #endregion
                //myTable set to DataTable object
                myTable = this.SelectOperator.GetDataTable("PUBLIC_HIST");
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
        #region query_for_ARZ626( DateTime TODAY_PROCESS_DTE, DateTime NEXT_PROCESS_DTE)產生ACH信用卡代繳費用解繳彙總表
        public string query_for_ARZ626(DateTime TODAY_PROCESS_DTE, DateTime NEXT_PROCESS_DTE)
        {
            #region Modify History
            /// <history>
            /// <design>
            /// <name>Cybersoft.COCA DaoGenerator</name>
            /// <date>2012/6/12 上午 12:26:10</date>
            #endregion
            try
            {
                #region set SQL statement
                StringBuilder sbstrSQL = new StringBuilder();
                sbstrSQL.Append(" select B.UT_CODE AS PAY_TYPE,B.UT_NAME,ISNULL(A.CNT,0) CNT,ISNULL(A.PAY_AMT,0) PAY_AMT, ISNULL(C.BUS_BANK_ACCOUNT,'') BUS_BANK_ACCOUNT                                            ");
                sbstrSQL.Append(" from MBS.dbo.MBS_UTLITY B                                                                           ");
                sbstrSQL.Append("  left join (SELECT PAY_TYPE,COUNT(*) CNT,SUM(PAY_AMT) PAY_AMT FROM PUBLIC_HIST                      ");
                sbstrSQL.Append("              WHERE PAY_TYPE IN ('0031','0005','0007','0009','0010','0011','0012','0013','0014','0015','0017','0018','0024','0025','0027','0028','0032','0033','0034','0036')      ");
                sbstrSQL.Append("                AND PAY_RESULT = '0000'                                                  ");
                sbstrSQL.Append("                AND PAY_DTE > @TODAY_PROCESS_DTE AND PAY_DTE <= @NEXT_PROCESS_DTE  ");
                sbstrSQL.Append("              GROUP BY PAY_TYPE )A                            ");
                sbstrSQL.Append("         on A.PAY_TYPE = B.UT_CODE                            ");
                sbstrSQL.Append("  left join MBS.dbo.MER_BAS C  on  B.UT_MER_NO = C.MER_NO     ");
                sbstrSQL.Append("  where B.UT_CODE IN ('0031','0005','0007','0009','0010','0011','0012','0013','0014','0015','0017','0018','0024','0025','0027','0028','0032','0033','0034','0036')                 ");
                sbstrSQL.Append(" order by  B.UT_CODE                                          ");
                #endregion
                this.SelectOperator.SqlText = sbstrSQL.ToString();
                #region replace SQL parameter
                if (sbstrSQL.ToString().Contains("@TODAY_PROCESS_DTE "))
                {
                    this.SelectOperator.SetValue("@TODAY_PROCESS_DTE", TODAY_PROCESS_DTE);
                }
                if (sbstrSQL.ToString().Contains("@NEXT_PROCESS_DTE "))
                {
                    this.SelectOperator.SetValue("@NEXT_PROCESS_DTE", NEXT_PROCESS_DTE);
                }
                #endregion
                //myTable set to DataTable object
                myTable = this.SelectOperator.GetDataTable("PUBLIC_HIST");
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
        #region query_for_PBBSUM001(string TODAY_YYYYMMDD)產生公用事業代繳報表(含ACH總計項目)
        public string query_for_PBBSUM001(string TODAY_YYYYMMDD)
        {
            #region Modify History
            /// <history>
            /// <design>
            /// <name>Cybersoft.COCA DaoGenerator</name>
            /// <date>2012/6/12 上午 12:26:10</date>
            #endregion
            try
            {
                #region set SQL statement
                StringBuilder sbstrSQL = new StringBuilder();
                sbstrSQL.Append(" SELECT '" + TODAY_YYYYMMDD + "' PROCESS_DTE, T1.CODE CODE, T1.DESCR DESCR, ISNULL(T6.FEE, 0) FEE,                                                   ");
                sbstrSQL.Append(" ISNULL(T2.SUCC_CNT, 0) SUCC_CNT_1, ISNULL(T2.SUCC_AMT, 0.00) SUCC_AMT_1,                                                                            ");
                sbstrSQL.Append(" ROUND(ISNULL((ISNULL(T2.SUCC_CNT, 0)*ISNULL(T6.FEE, 0)), 0),0) SUCC_FEE_1,                                                                          ");
                sbstrSQL.Append(" ISNULL(ISNULL(T2.SUCC_AMT, 0.00)-ROUND((ISNULL(T2.SUCC_CNT, 0)*ISNULL(T6.FEE, 0)), 0),0) SUCC_AMT_T_1,                                              ");
                sbstrSQL.Append("                                                                                                                                                     ");
                sbstrSQL.Append(" ISNULL(T4.SUCC_CNT, 0) SUCC_CNT_2, ISNULL(T4.SUCC_AMT, 0.00) SUCC_AMT_2,                                                                            ");
                sbstrSQL.Append(" ROUND(ISNULL((ISNULL(T4.SUCC_CNT, 0)*ISNULL(T6.FEE, 0)), 0),0) SUCC_FEE_2,                                                                          ");
                sbstrSQL.Append(" ISNULL(ISNULL(T4.SUCC_AMT, 0.00)-ROUND((ISNULL(T4.SUCC_CNT, 0)*ISNULL(T6.FEE, 0)), 0),0) SUCC_AMT_T_2,                                              ");
                sbstrSQL.Append("                                                                                                                                                     ");
                sbstrSQL.Append(" ISNULL(T3.FAIL_CNT, 0) FAIL_CNT_3, ISNULL(T3.FAIL_AMT, 0.00) FAIL_AMT_3, 0 FAIL_AMT_T_3,                                                            ");
                sbstrSQL.Append(" ISNULL(T5.FAIL_CNT, 0) FAIL_CNT_4, ISNULL(T5.FAIL_AMT, 0.00) FAIL_AMT_4, 0 FAIL_AMT_T_4                                                             ");
                sbstrSQL.Append(" FROM ( SELECT SUBSTRING(CODE,7,4) CODE, DESCR                                                                                                       ");
                sbstrSQL.Append("           FROM SETUP_CODE                                                                                                                           ");
                sbstrSQL.Append("          WHERE TYPE = 'TX_SOURCE' AND CODE LIKE 'PU%'                                                                                               ");
                //sbstrSQL.Append("                                   AND SUBSTRING(CODE,7,4) IN('0001','0002','0003','0004','0006','0008','0016','0021','0022','0023','0035')        ");
                sbstrSQL.Append("                                   AND SUBSTRING(CODE,7,4) IN('0001','0002','0003','0004')                                                           ");
                sbstrSQL.Append("       ) T1                                                                                                                                          ");
                sbstrSQL.Append(" LEFT JOIN ( SELECT PAY_TYPE, COUNT(*) SUCC_CNT, SUM(PAY_AMT) SUCC_AMT                                                                               ");
                sbstrSQL.Append("               FROM PUBLIC_HIST                                                                                                                      ");
                sbstrSQL.Append("              WHERE REVERSAL_FLAG = '' AND  TRANS_DTE = '" + TODAY_YYYYMMDD + "' AND PAY_RESULT  = '0000'                                             ");
                sbstrSQL.Append("             GROUP BY PAY_TYPE) T2 ON T2.PAY_TYPE = T1.CODE                                                                                          ");
                sbstrSQL.Append(" LEFT JOIN ( SELECT PAY_TYPE, COUNT(*) FAIL_CNT, SUM(PAY_AMT) FAIL_AMT                                                                               ");
                sbstrSQL.Append("               FROM PUBLIC_HIST                                                                                                                      ");
                sbstrSQL.Append("              WHERE REVERSAL_FLAG = '' AND  TRANS_DTE = '" + TODAY_YYYYMMDD + "' AND PAY_RESULT != '0000'                                             ");
                sbstrSQL.Append("             GROUP BY PAY_TYPE) T3 ON T3.PAY_TYPE = T1.CODE                                                                                          ");
                sbstrSQL.Append(" LEFT JOIN ( SELECT PAY_TYPE, COUNT(*) SUCC_CNT, SUM(PAY_AMT) SUCC_AMT                                                                               ");
                sbstrSQL.Append("               FROM PUBLIC_HIST                                                                                                                      ");
                sbstrSQL.Append("              WHERE REVERSAL_FLAG = 'R' AND TRANS_DTE = '" + TODAY_YYYYMMDD + "' AND PAY_RESULT  = '0000'                                             ");
                sbstrSQL.Append("             GROUP BY PAY_TYPE) T4 ON T4.PAY_TYPE = T1.CODE                                                                                          ");
                sbstrSQL.Append(" LEFT JOIN ( SELECT PAY_TYPE, COUNT(*) FAIL_CNT, SUM(PAY_AMT) FAIL_AMT                                                                               ");
                sbstrSQL.Append("               FROM PUBLIC_HIST                                                                                                                      ");
                sbstrSQL.Append("              WHERE REVERSAL_FLAG = 'R' AND TRANS_DTE = '" + TODAY_YYYYMMDD + "' AND PAY_RESULT != '0000'                                             ");
                sbstrSQL.Append("             GROUP BY PAY_TYPE) T5 ON T5.PAY_TYPE = T1.CODE                                                                                          ");
                sbstrSQL.Append(" LEFT JOIN ( SELECT A.PAY_TYPE,B.COST_AMT AS FEE                                                                                                     ");
                sbstrSQL.Append("               FROM SETUP_PUBLIC a,SETUP_GL B                                                                                                        ");
                sbstrSQL.Append("              WHERE A.SOURCE_CODE = B.SOURCE_CODE AND B.MT_TYPE = 'D' ) T6 ON T6.PAY_TYPE = T1.CODE                                                  ");
                //sbstrSQL.Append(" union                                                                                                                                               ");
                //sbstrSQL.Append(" SELECT '" + TODAY_YYYYMMDD + "' PROCESS_DTE, '-' CODE, 'ACH 總項目' DESCR, SUM(0) FEE,                                                                  ");
                //sbstrSQL.Append(" SUM(ISNULL(T2.SUCC_CNT, 0)) SUCC_CNT_1, SUM(ISNULL(T2.SUCC_AMT, 0.00)) SUCC_AMT_1,                                                                  ");
                //sbstrSQL.Append(" SUM(0) SUCC_FEE_1,SUM(ISNULL(ISNULL(T2.SUCC_AMT, 0.00)- 0, 0)) SUCC_AMT_T_1,                                                                        ");
                //sbstrSQL.Append(" SUM(ISNULL(T4.SUCC_CNT, 0)) SUCC_CNT_2, SUM(ISNULL(T4.SUCC_AMT, 0.00)) SUCC_AMT_2,                                                                  ");
                //sbstrSQL.Append(" SUM(0) SUCC_FEE_2, SUM(0) SUCC_AMT_T_2,                                                                                                             ");
                //sbstrSQL.Append(" SUM(ISNULL(T3.FAIL_CNT, 0)) FAIL_CNT_3, SUM(ISNULL(T3.FAIL_AMT, 0.00)) FAIL_AMT_3, SUM(0) FAIL_AMT_T_3,                                             ");
                //sbstrSQL.Append(" SUM(ISNULL(T5.FAIL_CNT, 0)) FAIL_CNT_4, SUM(ISNULL(T5.FAIL_AMT, 0.00)) FAIL_AMT_4, SUM(0) FAIL_AMT_T_4                                              ");
                //sbstrSQL.Append(" FROM ( SELECT SUBSTRING(CODE,7,4) CODE, DESCR                                                                                                       ");
                //sbstrSQL.Append("          FROM SETUP_CODE                                                                                                                            ");
                //sbstrSQL.Append("         WHERE TYPE = 'TX_SOURCE' AND CODE LIKE 'PU%'                                                                                                ");
                //sbstrSQL.Append("                                  AND SUBSTRING(CODE,7,4) NOT IN ('0001','0002','0003','0004','0006','0008','0016','0021','0022','0023','0035')) T1  ");
                //sbstrSQL.Append(" LEFT JOIN ( SELECT PAY_TYPE, COUNT(*) SUCC_CNT, SUM(PAY_AMT) SUCC_AMT                                                                               ");
                //sbstrSQL.Append("               FROM PUBLIC_HIST                                                                                                                      ");
                //sbstrSQL.Append("              WHERE REVERSAL_FLAG = '' AND TRANS_DTE = '" + TODAY_YYYYMMDD + "' AND PAY_RESULT = '0000'                                                   ");
                //sbstrSQL.Append("             GROUP BY PAY_TYPE) T2 ON T2.PAY_TYPE = T1.CODE                                                                                          ");
                //sbstrSQL.Append(" LEFT JOIN ( SELECT PAY_TYPE, COUNT(*) FAIL_CNT, SUM(PAY_AMT) FAIL_AMT                                                                               ");
                //sbstrSQL.Append("               FROM PUBLIC_HIST                                                                                                                      ");
                //sbstrSQL.Append("              WHERE REVERSAL_FLAG = '' AND TRANS_DTE = '" + TODAY_YYYYMMDD + "' AND PAY_RESULT != '0000'                                                  ");
                //sbstrSQL.Append("             GROUP BY PAY_TYPE) T3 ON T3.PAY_TYPE = T1.CODE                                                                                          ");
                //sbstrSQL.Append(" LEFT JOIN ( SELECT PAY_TYPE, COUNT(*) SUCC_CNT, SUM(PAY_AMT) SUCC_AMT                                                                               ");
                //sbstrSQL.Append("               FROM PUBLIC_HIST                                                                                                                      ");
                //sbstrSQL.Append("              WHERE REVERSAL_FLAG = 'R' AND TRANS_DTE = '" + TODAY_YYYYMMDD + "' AND PAY_RESULT = '0000'                                                  ");
                //sbstrSQL.Append("             GROUP BY PAY_TYPE) T4 ON T4.PAY_TYPE = T1.CODE                                                                                          ");
                //sbstrSQL.Append(" LEFT JOIN ( SELECT PAY_TYPE, COUNT(*) FAIL_CNT, SUM(PAY_AMT) FAIL_AMT                                                                               ");
                //sbstrSQL.Append("               FROM PUBLIC_HIST                                                                                                                      ");
                //sbstrSQL.Append("              WHERE REVERSAL_FLAG = 'R' AND TRANS_DTE = '" + TODAY_YYYYMMDD + "' AND PAY_RESULT != '0000'                                                 ");
                //sbstrSQL.Append("             GROUP BY PAY_TYPE)T5 ON T5.PAY_TYPE = T1.CODE                                                                                           ");               

                #endregion
                this.SelectOperator.SqlText = sbstrSQL.ToString();

                //myTable set to DataTable object
                myTable = this.SelectOperator.GetDataTable("PUBLIC_HIST");
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
        //public string query_for_PBBSUM001(string TODAY_YYYYMMDD)
        //{
        //    #region Modify History
        //    /// <history>
        //    /// <design>
        //    /// <name>Cybersoft.COCA DaoGenerator</name>
        //    /// <date>2012/6/12 上午 12:26:10</date>
        //    #endregion
        //    try
        //    {
        //        #region set SQL statement
        //        StringBuilder sbstrSQL = new StringBuilder();
        //        sbstrSQL.Append(" SELECT '"+TODAY_YYYYMMDD+"' PROCESS_DTE, T1.CODE CODE, T1.DESCR DESCR, ISNULL(T6.FEE, 0) FEE,    ");
        //        sbstrSQL.Append(" ISNULL(T2.SUCC_CNT, 0) SUCC_CNT_1, ISNULL(T2.SUCC_AMT, 0.00) SUCC_AMT_1,   ");
        //        sbstrSQL.Append(" ROUND(ISNULL((ISNULL(T2.SUCC_CNT, 0)*ISNULL(T6.FEE, 0)), 0),0) SUCC_FEE_1,    ");
        //        sbstrSQL.Append(" ISNULL(ISNULL(T2.SUCC_AMT, 0.00)-ROUND((ISNULL(T2.SUCC_CNT, 0)*ISNULL(T6.FEE, 0)), 0),0) SUCC_AMT_T_1,   ");
        //        sbstrSQL.Append("  ");
        //        sbstrSQL.Append(" ISNULL(T4.SUCC_CNT, 0) SUCC_CNT_2, ISNULL(T4.SUCC_AMT, 0.00) SUCC_AMT_2,   ");
        //        sbstrSQL.Append(" ROUND(ISNULL((ISNULL(T4.SUCC_CNT, 0)*ISNULL(T6.FEE, 0)), 0),0) SUCC_FEE_2,    ");
        //        sbstrSQL.Append(" ISNULL(ISNULL(T4.SUCC_AMT, 0.00)-ROUND((ISNULL(T4.SUCC_CNT, 0)*ISNULL(T6.FEE, 0)), 0),0) SUCC_AMT_T_2,   ");
        //        sbstrSQL.Append("  ");
        //        sbstrSQL.Append(" ISNULL(T3.FAIL_CNT, 0) FAIL_CNT_3, ISNULL(T3.FAIL_AMT, 0.00) FAIL_AMT_3, 0 FAIL_AMT_T_3,      ");
        //        sbstrSQL.Append("  ");
        //        sbstrSQL.Append(" ISNULL(T5.FAIL_CNT, 0) FAIL_CNT_4, ISNULL(T5.FAIL_AMT, 0.00) FAIL_AMT_4, 0 FAIL_AMT_T_4   ");
        //        sbstrSQL.Append(" FROM (   ");
        //        sbstrSQL.Append(" SELECT SUBSTRING(CODE,7,4) CODE, DESCR FROM SETUP_CODE WHERE TYPE = 'TX_SOURCE' AND CODE LIKE 'PU%'   ");
        //        sbstrSQL.Append(" AND SUBSTRING(CODE,7,4) IN('0001','0002','0003','0004','0006','0008','0016','0021','0022','0023','0035')) T1   ");
        //        sbstrSQL.Append(" LEFT JOIN (   ");
        //        sbstrSQL.Append(" SELECT PAY_TYPE, COUNT(*) SUCC_CNT, SUM(PAY_AMT) SUCC_AMT    ");
        //        sbstrSQL.Append(" FROM PUBLIC_HIST   ");
        //        sbstrSQL.Append(" WHERE REVERSAL_FLAG = '' AND TRANS_DTE = '"+TODAY_YYYYMMDD+"' AND PAY_RESULT = '0000'   ");
        //        sbstrSQL.Append(" GROUP BY PAY_TYPE) T2 ON T2.PAY_TYPE = T1.CODE   ");
        //        sbstrSQL.Append(" LEFT JOIN (   ");
        //        sbstrSQL.Append(" SELECT PAY_TYPE, COUNT(*) FAIL_CNT, SUM(PAY_AMT) FAIL_AMT    ");
        //        sbstrSQL.Append(" FROM PUBLIC_HIST   ");
        //        sbstrSQL.Append(" WHERE REVERSAL_FLAG = '' AND TRANS_DTE = '"+TODAY_YYYYMMDD+"' AND PAY_RESULT != '0000'   ");
        //        sbstrSQL.Append(" GROUP BY PAY_TYPE) T3 ON T3.PAY_TYPE = T1.CODE   ");
        //        sbstrSQL.Append(" LEFT JOIN (   ");
        //        sbstrSQL.Append(" SELECT PAY_TYPE, COUNT(*) SUCC_CNT, SUM(PAY_AMT) SUCC_AMT    ");
        //        sbstrSQL.Append(" FROM PUBLIC_HIST   ");
        //        sbstrSQL.Append(" WHERE REVERSAL_FLAG = 'R' AND TRANS_DTE = '"+TODAY_YYYYMMDD+"' AND PAY_RESULT = '0000'   ");
        //        sbstrSQL.Append(" GROUP BY PAY_TYPE) T4 ON T4.PAY_TYPE = T1.CODE   ");
        //        sbstrSQL.Append(" LEFT JOIN (   ");
        //        sbstrSQL.Append(" SELECT PAY_TYPE, COUNT(*) FAIL_CNT, SUM(PAY_AMT) FAIL_AMT    ");
        //        sbstrSQL.Append(" FROM PUBLIC_HIST   ");
        //        sbstrSQL.Append(" WHERE REVERSAL_FLAG = 'R' AND TRANS_DTE = '"+TODAY_YYYYMMDD+"' AND PAY_RESULT != '0000'   ");
        //        sbstrSQL.Append(" GROUP BY PAY_TYPE) T5 ON T5.PAY_TYPE = T1.CODE   ");
        //        sbstrSQL.Append(" LEFT JOIN (   ");
        //        sbstrSQL.Append(" SELECT '0001' PAY_TYPE, 2.5 FEE   ");
        //        sbstrSQL.Append(" UNION ALL SELECT '0002' PAY_TYPE, 3 FEE    ");
        //        sbstrSQL.Append(" UNION ALL SELECT '0003' PAY_TYPE, 3 FEE    ");
        //        sbstrSQL.Append(" UNION ALL SELECT '0004' PAY_TYPE, 3 FEE    ");
        //        sbstrSQL.Append(" UNION ALL SELECT '0008' PAY_TYPE, 2.5 FEE    ");
        //        sbstrSQL.Append(" UNION ALL SELECT '0016' PAY_TYPE, 2.5 FEE    ");
        //        sbstrSQL.Append(" UNION ALL SELECT '0035' PAY_TYPE, 2.5 FEE ) T6 ON T6.PAY_TYPE = T1.CODE   ");
        //        sbstrSQL.Append("  ");
        //        sbstrSQL.Append(" UNION   ");
        //        sbstrSQL.Append(" SELECT '"+TODAY_YYYYMMDD+"' PROCESS_DTE, '-' CODE, 'ACH 總項目' DESCR, SUM(0) FEE,   ");
        //        sbstrSQL.Append(" SUM(ISNULL(T2.SUCC_CNT, 0)) SUCC_CNT_1, SUM(ISNULL(T2.SUCC_AMT, 0.00)) SUCC_AMT_1,  ");
        //        sbstrSQL.Append(" SUM(0) SUCC_FEE_1,SUM(ISNULL(ISNULL(T2.SUCC_AMT, 0.00)- 0, 0)) SUCC_AMT_T_1,  ");
        //        sbstrSQL.Append(" SUM(ISNULL(T4.SUCC_CNT, 0)) SUCC_CNT_2, SUM(ISNULL(T4.SUCC_AMT, 0.00)) SUCC_AMT_2, ");
        //        sbstrSQL.Append(" SUM(0) SUCC_FEE_2, SUM(0) SUCC_AMT_T_2,  ");
        //        sbstrSQL.Append(" SUM(ISNULL(T3.FAIL_CNT, 0)) FAIL_CNT_3, SUM(ISNULL(T3.FAIL_AMT, 0.00)) FAIL_AMT_3, SUM(0) FAIL_AMT_T_3,   ");
        //        sbstrSQL.Append(" SUM(ISNULL(T5.FAIL_CNT, 0)) FAIL_CNT_4, SUM(ISNULL(T5.FAIL_AMT, 0.00)) FAIL_AMT_4, SUM(0) FAIL_AMT_T_4  ");
        //        sbstrSQL.Append(" FROM (  ");
        //        sbstrSQL.Append(" SELECT SUBSTRING(CODE,7,4) CODE, DESCR FROM SETUP_CODE WHERE TYPE = 'TX_SOURCE' AND CODE LIKE 'PU%' AND  ");
        //        sbstrSQL.Append(" SUBSTRING(CODE,7,4) NOT IN ('0001','0002','0003','0004','0006','0008','0016','0021','0022','0023','0035')) T1  ");
        //        sbstrSQL.Append(" LEFT JOIN (  ");
        //        sbstrSQL.Append(" SELECT PAY_TYPE, COUNT(*) SUCC_CNT, SUM(PAY_AMT) SUCC_AMT   ");
        //        sbstrSQL.Append(" FROM PUBLIC_HIST  ");
        //        sbstrSQL.Append(" WHERE REVERSAL_FLAG = '' AND TRANS_DTE = '"+TODAY_YYYYMMDD+"' AND PAY_RESULT = '0000'  ");
        //        sbstrSQL.Append(" GROUP BY PAY_TYPE) T2 ON T2.PAY_TYPE = T1.CODE  ");
        //        sbstrSQL.Append(" LEFT JOIN (  ");
        //        sbstrSQL.Append(" SELECT PAY_TYPE, COUNT(*) FAIL_CNT, SUM(PAY_AMT) FAIL_AMT   ");
        //        sbstrSQL.Append(" FROM PUBLIC_HIST  ");
        //        sbstrSQL.Append(" WHERE REVERSAL_FLAG = '' AND TRANS_DTE = '"+TODAY_YYYYMMDD+"' AND PAY_RESULT != '0000'  ");
        //        sbstrSQL.Append(" GROUP BY PAY_TYPE) T3 ON T3.PAY_TYPE = T1.CODE  ");
        //        sbstrSQL.Append(" LEFT JOIN (  ");
        //        sbstrSQL.Append(" SELECT PAY_TYPE, COUNT(*) SUCC_CNT, SUM(PAY_AMT) SUCC_AMT   ");
        //        sbstrSQL.Append(" FROM PUBLIC_HIST  ");
        //        sbstrSQL.Append(" WHERE REVERSAL_FLAG = 'R' AND TRANS_DTE = '"+TODAY_YYYYMMDD+"' AND PAY_RESULT = '0000'  ");
        //        sbstrSQL.Append(" GROUP BY PAY_TYPE) T4 ON T4.PAY_TYPE = T1.CODE  ");
        //        sbstrSQL.Append(" LEFT JOIN (  ");
        //        sbstrSQL.Append(" SELECT PAY_TYPE, COUNT(*) FAIL_CNT, SUM(PAY_AMT) FAIL_AMT   ");
        //        sbstrSQL.Append(" FROM PUBLIC_HIST  ");
        //        sbstrSQL.Append(" WHERE REVERSAL_FLAG = 'R' AND TRANS_DTE = '"+TODAY_YYYYMMDD+"' AND PAY_RESULT != '0000'  ");
        //        sbstrSQL.Append(" GROUP BY PAY_TYPE)T5 ON T5.PAY_TYPE = T1.CODE   ");



        //        #endregion
        //        this.SelectOperator.SqlText = sbstrSQL.ToString();

        //        //myTable set to DataTable object
        //        myTable = this.SelectOperator.GetDataTable("PUBLIC_HIST");
        //        if (myTable.Rows.Count == 0)
        //        {
        //            msg_code = "F0023"; //not found
        //        }
        //        else
        //        {
        //            msg_code = "S0000"; //query success
        //        }
        //    }
        //    catch (SqlException e)
        //    {
        //        MSG.strMsg = Convert.ToString(e.Number) + '-' + e.Message;
        //        msg_code = MSG.getMsg();
        //    }
        //    return msg_code;
        //}
        #endregion
        #region query_for_PBBSUM001_ACH(string TODAY_YYYYMMDD)產生公用事業代繳報表(ACH)
        public string query_for_PBBSUM001_ACH(string TODAY_YYYYMMDD)
        {
            #region Modify History
            /// <history>
            /// <design>
            /// <name>Cybersoft.COCA DaoGenerator</name>
            /// <date>2012/6/12 上午 12:26:10</date>
            #endregion
            try
            {
                #region set SQL statement
                StringBuilder sbstrSQL = new StringBuilder();
                sbstrSQL.Append(" SELECT '" + TODAY_YYYYMMDD + "' 處理日期, T1.CODE 單位代碼, T1.DESCR 公用事業單位, 0 單筆手續費,  ");
                sbstrSQL.Append("        ISNULL(T2.SUCC_CNT, 0) 成功筆數, ISNULL(T2.SUCC_AMT, 0.00) 成功總金額, ");
                sbstrSQL.Append("               0 總手續費用,  ");
                sbstrSQL.Append("        ISNULL(ISNULL(T2.SUCC_AMT, 0.00)- 0, 0) 解繳金額_成功, ");
                sbstrSQL.Append("        ISNULL(T4.SUCC_CNT, 0) 退貨成功筆數, ISNULL(T4.SUCC_AMT, 0.00) 退貨成功總金額, ");
                sbstrSQL.Append("        ISNULL(T3.FAIL_CNT, 0) 失敗筆數, ISNULL(T3.FAIL_AMT, 0.00) 失敗總金額,  ");
                sbstrSQL.Append("        ISNULL(T5.FAIL_CNT, 0) 退貨失敗筆數, ISNULL(T5.FAIL_AMT, 0.00) 退貨失敗總金額 ");
                sbstrSQL.Append(" FROM ( ");
                sbstrSQL.Append(" SELECT SUBSTRING(CODE,7,4) CODE, DESCR FROM SETUP_CODE WHERE TYPE = 'TX_SOURCE' AND CODE LIKE 'PU%' AND ");
                sbstrSQL.Append("        SUBSTRING(CODE,7,4) NOT IN ('0001','0002','0003','0004','0006','0008','0016','0021','0022','0023','0035')) T1 ");
                sbstrSQL.Append(" LEFT JOIN ( ");
                sbstrSQL.Append(" SELECT PAY_TYPE, COUNT(*) SUCC_CNT, SUM(PAY_AMT) SUCC_AMT  ");
                sbstrSQL.Append(" FROM PUBLIC_HIST ");
                sbstrSQL.Append(" WHERE REVERSAL_FLAG = '' AND TRANS_DTE = '" + TODAY_YYYYMMDD + "' AND PAY_RESULT = '0000' ");
                sbstrSQL.Append(" GROUP BY PAY_TYPE) T2 ON T2.PAY_TYPE = T1.CODE ");
                sbstrSQL.Append(" LEFT JOIN ( ");
                sbstrSQL.Append(" SELECT PAY_TYPE, COUNT(*) FAIL_CNT, SUM(PAY_AMT) FAIL_AMT  ");
                sbstrSQL.Append(" FROM PUBLIC_HIST ");
                sbstrSQL.Append(" WHERE REVERSAL_FLAG = '' AND TRANS_DTE = '" + TODAY_YYYYMMDD + "' AND PAY_RESULT != '0000' ");
                sbstrSQL.Append(" GROUP BY PAY_TYPE) T3 ON T3.PAY_TYPE = T1.CODE ");
                sbstrSQL.Append(" LEFT JOIN ( ");
                sbstrSQL.Append(" SELECT PAY_TYPE, COUNT(*) SUCC_CNT, SUM(PAY_AMT) SUCC_AMT  ");
                sbstrSQL.Append(" FROM PUBLIC_HIST ");
                sbstrSQL.Append(" WHERE REVERSAL_FLAG = 'R' AND TRANS_DTE = '" + TODAY_YYYYMMDD + "' AND PAY_RESULT = '0000' ");
                sbstrSQL.Append(" GROUP BY PAY_TYPE) T4 ON T4.PAY_TYPE = T1.CODE ");
                sbstrSQL.Append(" LEFT JOIN ( ");
                sbstrSQL.Append(" SELECT PAY_TYPE, COUNT(*) FAIL_CNT, SUM(PAY_AMT) FAIL_AMT  ");
                sbstrSQL.Append(" FROM PUBLIC_HIST ");
                sbstrSQL.Append(" WHERE REVERSAL_FLAG = 'R' AND TRANS_DTE = '" + TODAY_YYYYMMDD + "' AND PAY_RESULT != '0000' ");
                sbstrSQL.Append(" GROUP BY PAY_TYPE)T5 ON T5.PAY_TYPE = T1.CODE ");

                sbstrSQL.Append(" UNION ALL ");
                sbstrSQL.Append("  SELECT '' 處理日期, '' 單位代碼, '總計' 公用事業單位, 0 單筆手續費,   ");
                sbstrSQL.Append("         sum(ISNULL(T2.SUCC_CNT, 0)) 成功筆數, sum(ISNULL(T2.SUCC_AMT, 0.00)) 成功總金額,  ");
                sbstrSQL.Append("                sum(0) 總手續費用,   ");
                sbstrSQL.Append("         sum(ISNULL(ISNULL(T2.SUCC_AMT, 0.00)- 0, 0)) 解繳金額_成功,  ");
                sbstrSQL.Append("         sum(ISNULL(T4.SUCC_CNT, 0)) 退貨成功筆數, sum(ISNULL(T4.SUCC_AMT, 0.00)) 退貨成功總金額,  ");
                sbstrSQL.Append("         sum(ISNULL(T3.FAIL_CNT, 0)) 失敗筆數, sum(ISNULL(T3.FAIL_AMT, 0.00)) 失敗總金額,   ");
                sbstrSQL.Append("         sum(ISNULL(T5.FAIL_CNT, 0)) 退貨失敗筆數, sum(ISNULL(T5.FAIL_AMT, 0.00)) 退貨失敗總金額  ");
                sbstrSQL.Append("  FROM (  ");
                sbstrSQL.Append("  SELECT SUBSTRING(CODE,7,4) CODE, DESCR FROM SETUP_CODE WHERE TYPE = 'TX_SOURCE' AND CODE LIKE 'PU%' AND  ");
                sbstrSQL.Append("         SUBSTRING(CODE,7,4) NOT IN ('0001','0002','0003','0004','0006','0008','0016','0021','0022','0023','0035')) T1  ");
                sbstrSQL.Append("  LEFT JOIN (  ");
                sbstrSQL.Append("  SELECT PAY_TYPE, COUNT(*) SUCC_CNT, SUM(PAY_AMT) SUCC_AMT   ");
                sbstrSQL.Append("  FROM PUBLIC_HIST  ");
                sbstrSQL.Append("  WHERE REVERSAL_FLAG = '' AND TRANS_DTE = '" + TODAY_YYYYMMDD + "' AND PAY_RESULT = '0000'  ");
                sbstrSQL.Append("  GROUP BY PAY_TYPE) T2 ON T2.PAY_TYPE = T1.CODE  ");
                sbstrSQL.Append("  LEFT JOIN (  ");
                sbstrSQL.Append("  SELECT PAY_TYPE, COUNT(*) FAIL_CNT, SUM(PAY_AMT) FAIL_AMT   ");
                sbstrSQL.Append("  FROM PUBLIC_HIST  ");
                sbstrSQL.Append("  WHERE REVERSAL_FLAG = '' AND TRANS_DTE = '" + TODAY_YYYYMMDD + "' AND PAY_RESULT != '0000'  ");
                sbstrSQL.Append("  GROUP BY PAY_TYPE) T3 ON T3.PAY_TYPE = T1.CODE  ");
                sbstrSQL.Append("  LEFT JOIN (  ");
                sbstrSQL.Append("  SELECT PAY_TYPE, COUNT(*) SUCC_CNT, SUM(PAY_AMT) SUCC_AMT   ");
                sbstrSQL.Append("  FROM PUBLIC_HIST  ");
                sbstrSQL.Append("  WHERE REVERSAL_FLAG = 'R' AND TRANS_DTE = '" + TODAY_YYYYMMDD + "' AND PAY_RESULT = '0000'  ");
                sbstrSQL.Append("  GROUP BY PAY_TYPE) T4 ON T4.PAY_TYPE = T1.CODE  ");
                sbstrSQL.Append("  LEFT JOIN (  ");
                sbstrSQL.Append("  SELECT PAY_TYPE, COUNT(*) FAIL_CNT, SUM(PAY_AMT) FAIL_AMT   ");
                sbstrSQL.Append("  FROM PUBLIC_HIST  ");
                sbstrSQL.Append("  WHERE REVERSAL_FLAG = 'R' AND TRANS_DTE = '" + TODAY_YYYYMMDD + "' AND PAY_RESULT != '0000'  ");
                sbstrSQL.Append("  GROUP BY PAY_TYPE)T5 ON T5.PAY_TYPE = T1.CODE ");


                #endregion
                this.SelectOperator.SqlText = sbstrSQL.ToString();
                //myTable set to DataTable object
                myTable = this.SelectOperator.GetDataTable("PUBLIC_HIST");
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
        #region query_for_PAY_NBR(string strPAY_TYPE, string strTRANS_DTE)
        public string query_for_PAY_NBR(string strPAY_TYPE, string strTRANS_DTE)
        {
            #region Modify History
            /// <history>
            /// <design>
            /// <name>Cybersoft.COCA DaoGenerator</name>
            /// <date>2012/6/12 上午 12:26:10</date>
            #endregion
            try
            {
                #region set SQL statement
                StringBuilder sbstrSQL = new StringBuilder();
                sbstrSQL.Append(" SELECT SUBSTRING(PAY_NBR,1,1) AS PAY_NBR_S ");
                sbstrSQL.Append(" FROM PUBLIC_HIST WHERE PAY_TYPE = '" + strPAY_TYPE + "' AND TRANS_DTE = '" + strTRANS_DTE + "'   ");
                sbstrSQL.Append(" GROUP BY SUBSTRING(PAY_NBR,1,1) ");
                sbstrSQL.Append(" ORDER BY SUBSTRING(PAY_NBR,1,1) ");

                #endregion
                this.SelectOperator.SqlText = sbstrSQL.ToString();

                //myTable set to DataTable object
                myTable = this.SelectOperator.GetDataTable("PUBLIC_HIST");
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
        #region query_for_CAD0001C(string strPAY_NBR_S)產生信用卡代繳水費(省水)手續費清單
        public string query_for_CAD0001C(string strPAY_NBR_S)
        {
            #region Modify History
            /// <history>
            /// <design>
            /// <name>Cybersoft.COCA DaoGenerator</name>
            /// <date>2012/6/12 上午 12:26:10</date>
            #endregion
            try
            {
                #region set SQL statement
                StringBuilder sbstrSQL = new StringBuilder();
                sbstrSQL.Append(" SELECT '" + strPAY_NBR_S + "' AS [KEY], S1.PAY_NBR_SS, ISNULL(S1.SUCC_CNT,0) SUCC_CNT, ISNULL(S1.SUCC_AMT,0) SUCC_AMT, ");
                sbstrSQL.Append(" ISNULL(F1.FAIL_CNT,0) FAIL_CNT, ISNULL(F1.FAIL_AMT,0) FAIL_AMT, ");
                sbstrSQL.Append(" (S1.SUCC_CNT * 3) AS FEE_AMT, (S1.SUCC_AMT - (S1.SUCC_CNT * 3)) AS NET_AMT ");
                sbstrSQL.Append(" FROM  ");
                sbstrSQL.Append(" ( ");
                sbstrSQL.Append(" SELECT SUBSTRING(PAY_NBR,1,2) AS PAY_NBR_SS, COUNT(*) AS SUCC_CNT, SUM(PAY_AMT) AS SUCC_AMT ");
                sbstrSQL.Append(" FROM PUBLIC_HIST   ");
                sbstrSQL.Append(" WHERE REVERSAL_FLAG = '' AND PAY_RESULT = '0000'  ");
                sbstrSQL.Append(" AND PAY_TYPE = @wherePAY_TYPE AND TRANS_DTE = @whereTRANS_DTE ");
                sbstrSQL.Append(" GROUP BY SUBSTRING(PAY_NBR,1,2) HAVING SUBSTRING(SUBSTRING(PAY_NBR,1,2),1,1) = '" + strPAY_NBR_S + "' ");
                sbstrSQL.Append(" ) S1 ");
                sbstrSQL.Append(" LEFT JOIN ");
                sbstrSQL.Append(" ( ");
                sbstrSQL.Append(" SELECT SUBSTRING(PAY_NBR,1,2) AS PAY_NBR_SS, COUNT(*) AS FAIL_CNT, SUM(PAY_AMT) AS FAIL_AMT ");
                sbstrSQL.Append(" FROM PUBLIC_HIST   ");
                sbstrSQL.Append(" WHERE REVERSAL_FLAG = '' AND PAY_RESULT != '0000'  ");
                sbstrSQL.Append(" AND PAY_TYPE = @wherePAY_TYPE AND TRANS_DTE = @whereTRANS_DTE ");
                sbstrSQL.Append(" GROUP BY SUBSTRING(PAY_NBR,1,2) HAVING SUBSTRING(SUBSTRING(PAY_NBR,1,2),1,1) = '" + strPAY_NBR_S + "' ");
                sbstrSQL.Append(" ) F1 ");
                sbstrSQL.Append(" ON S1.PAY_NBR_SS = F1.PAY_NBR_SS ");
                sbstrSQL.Append(" UNION ALL ");
                sbstrSQL.Append(" SELECT '" + strPAY_NBR_S + "' AS [KEY], F2.PAY_NBR_SS, ISNULL(S2.SUCC_CNT,0) SUCC_CNT, ISNULL(S2.SUCC_AMT,0) SUCC_AMT, ");
                sbstrSQL.Append(" ISNULL(F2.FAIL_CNT,0) FAIL_CNT, ISNULL(F2.FAIL_AMT,0) FAIL_AMT, ");
                sbstrSQL.Append(" (ISNULL(S2.SUCC_CNT,0) * 3) AS FEE_AMT, (ISNULL(S2.SUCC_AMT,0) - (ISNULL(S2.SUCC_CNT,0) * 3)) AS NET_AMT ");
                sbstrSQL.Append(" FROM ");
                sbstrSQL.Append(" ( ");
                sbstrSQL.Append(" SELECT SUBSTRING(PAY_NBR,1,2) AS PAY_NBR_SS, COUNT(*) AS SUCC_CNT, SUM(PAY_AMT) AS SUCC_AMT ");
                sbstrSQL.Append(" FROM PUBLIC_HIST ");
                sbstrSQL.Append(" WHERE REVERSAL_FLAG = '' AND PAY_RESULT = '0000'  ");
                sbstrSQL.Append(" AND PAY_TYPE = @wherePAY_TYPE AND TRANS_DTE = @whereTRANS_DTE ");
                sbstrSQL.Append(" GROUP BY SUBSTRING(PAY_NBR,1,2) HAVING SUBSTRING(SUBSTRING(PAY_NBR,1,2),1,1) = '" + strPAY_NBR_S + "' ");
                sbstrSQL.Append(" ) S2 ");
                sbstrSQL.Append(" RIGHT JOIN ");
                sbstrSQL.Append(" ( ");
                sbstrSQL.Append(" SELECT SUBSTRING(PAY_NBR,1,2) AS PAY_NBR_SS, COUNT(*) AS FAIL_CNT, SUM(PAY_AMT) AS FAIL_AMT ");
                sbstrSQL.Append(" FROM PUBLIC_HIST   ");
                sbstrSQL.Append(" WHERE REVERSAL_FLAG = '' AND PAY_RESULT != '0000'  ");
                sbstrSQL.Append(" AND PAY_TYPE = @wherePAY_TYPE AND TRANS_DTE = @whereTRANS_DTE ");
                sbstrSQL.Append(" GROUP BY SUBSTRING(PAY_NBR,1,2) HAVING SUBSTRING(SUBSTRING(PAY_NBR,1,2),1,1) = '" + strPAY_NBR_S + "' ");
                sbstrSQL.Append(" ) F2 ");
                sbstrSQL.Append(" ON S2.PAY_NBR_SS = F2.PAY_NBR_SS ");
                sbstrSQL.Append(" WHERE ISNULL(S2.SUCC_CNT,0) = 0 ");
                sbstrSQL.Append(" ORDER BY PAY_NBR_SS ");
                #endregion
                this.SelectOperator.SqlText = sbstrSQL.ToString();
                #region replace SQL parameter
                if (sbstrSQL.ToString().Contains("@wherePAY_TYPE "))
                {
                    this.SelectOperator.SetValue("@wherePAY_TYPE", this.wherePAY_TYPE);
                }
                if (sbstrSQL.ToString().Contains("@whereTRANS_DTE "))
                {
                    this.SelectOperator.SetValue("@whereTRANS_DTE", this.whereTRANS_DTE);
                }

                #endregion
                //myTable set to DataTable object
                myTable = this.SelectOperator.GetDataTable("PUBLIC_HIST");
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
        #region query_for_CAD0001ABD()產生信用卡代繳(陽明/欣高/竹名)瓦斯費用 明細
        public string query_for_CAD0001ABD()
        {
            #region Modify History
            /// <history>
            /// <design>
            /// <name>Cybersoft.COCA DaoGenerator</name>
            /// <date>2012/6/12 上午 12:26:10</date>
            #endregion
            try
            {
                #region set SQL statement
                StringBuilder sbstrSQL = new StringBuilder();
                sbstrSQL.Append(" SELECT (REPLICATE ('0' ,7 - LEN(T.SEQ))+T.SEQ) as ROW_NO,*  ");
                sbstrSQL.Append("  FROM ( ");
                sbstrSQL.Append("  SELECT CONVERT(VARCHAR(7),row_number() over (order by PAY_CARD_NBR)) as SEQ,  ");
                sbstrSQL.Append("  @wherePAY_TYPE AS [KEY],a.PAY_CARD_NBR AS CARD_NO,b.NAME,a.PAY_NBR AS PAY_NO,a.PAY_AMT AS AMT, ");
                sbstrSQL.Append("  CASE PAY_RESULT ");
                sbstrSQL.Append("       WHEN '0000' THEN 'S000-扣繳成功' ");
                sbstrSQL.Append("       WHEN 'I001' THEN 'I001-未申請代扣' ");
                sbstrSQL.Append("       WHEN 'I002' THEN 'I002-有申請代繳但已終止' ");
                sbstrSQL.Append("       WHEN 'I003' THEN 'I003-CTL_CODE有誤' ");
                sbstrSQL.Append("       WHEN 'I004' THEN 'I004-卡片未開卡' ");
                sbstrSQL.Append("       WHEN 'I005' THEN 'I005-卡片不存在主檔' ");
                sbstrSQL.Append("       WHEN 'I006' THEN 'I006-信用額度不足' ");
                sbstrSQL.Append("       WHEN 'I007' THEN 'I007-卡片已過期' ");
                sbstrSQL.Append("       ELSE PAY_RESULT + '-其他錯誤' END DESCR ");
                sbstrSQL.Append("  FROM PUBLIC_HIST a JOIN CUST_INF b  ");
                sbstrSQL.Append("  ON a.BU = b.BU AND a.CUST_SEQ = b.CUST_NBR  ");
                sbstrSQL.Append("  WHERE a.PAY_TYPE = @wherePAY_TYPE AND a.TRANS_DTE = @whereTRANS_DTE ) T ");


                #endregion
                this.SelectOperator.SqlText = sbstrSQL.ToString();
                #region replace SQL parameter
                if (sbstrSQL.ToString().Contains("@wherePAY_TYPE "))
                {
                    this.SelectOperator.SetValue("@wherePAY_TYPE", this.wherePAY_TYPE);
                }
                if (sbstrSQL.ToString().Contains("@whereTRANS_DTE "))
                {
                    this.SelectOperator.SetValue("@whereTRANS_DTE", this.whereTRANS_DTE);
                }

                #endregion
                //myTable set to DataTable object
                myTable = this.SelectOperator.GetDataTable("PUBLIC_HIST");
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
        #region query_for_CAD0001ABD_I()產生信用卡代繳(陽明/欣高/竹名)瓦斯費用 成功/失敗報表
        public string query_for_CAD0001ABD_I()
        {
            #region Modify History
            /// <history>
            /// <design>
            /// <name>Cybersoft.COCA DaoGenerator</name>
            /// <date>2012/6/12 上午 12:26:10</date>
            #endregion
            try
            {
                #region set SQL statement
                StringBuilder sbstrSQL = new StringBuilder();
                sbstrSQL.Append(" SELECT  @wherePAY_TYPE AS [KEY],* FROM  ");
                sbstrSQL.Append(" (  ");
                sbstrSQL.Append(" SELECT @wherePAY_TYPE AS PAY_TYPE, ISNULL(COUNT(*),0) AS SUB_SUCC_CNT, ISNULL(SUM(PAY_AMT),0) AS SUB_SUCC_AMT,  ");
                sbstrSQL.Append(" ROUND(ISNULL((COUNT(*) * 2.5),0),0) AS SUB_FEE_AMT, ISNULL(SUM(PAY_AMT),0) - ROUND(ISNULL((COUNT(*) * 2.5),0),0) AS SUB_NET_AMT ");
                sbstrSQL.Append(" FROM PUBLIC_HIST    ");
                sbstrSQL.Append(" WHERE REVERSAL_FLAG = '' AND PAY_RESULT = '0000'   ");
                sbstrSQL.Append(" AND PAY_TYPE = @wherePAY_TYPE AND TRANS_DTE = @whereTRANS_DTE  ");
                sbstrSQL.Append(" ) S1 ");
                sbstrSQL.Append(" LEFT JOIN ");
                sbstrSQL.Append(" ( ");
                sbstrSQL.Append(" SELECT @wherePAY_TYPE AS PAY_TYPE, ISNULL(COUNT(*),0) AS SUB_FAIL_CNT, ISNULL(SUM(PAY_AMT),0) AS SUB_FAIL_AMT  ");
                sbstrSQL.Append(" FROM PUBLIC_HIST    ");
                sbstrSQL.Append(" WHERE REVERSAL_FLAG = '' AND PAY_RESULT != '0000'   ");
                sbstrSQL.Append(" AND PAY_TYPE = @wherePAY_TYPE AND TRANS_DTE = @whereTRANS_DTE  ");
                sbstrSQL.Append(" ) F1 ");
                sbstrSQL.Append(" ON S1.PAY_TYPE = F1.PAY_TYPE ");
                sbstrSQL.Append(" LEFT JOIN ");
                sbstrSQL.Append(" ( ");
                sbstrSQL.Append(" SELECT @wherePAY_TYPE AS PAY_TYPE, ISNULL(COUNT(*),0) AS SUB_CNT, ISNULL(SUM(PAY_AMT),0) AS SUB_AMT  ");
                sbstrSQL.Append(" FROM PUBLIC_HIST    ");
                sbstrSQL.Append(" WHERE PAY_TYPE = @wherePAY_TYPE AND TRANS_DTE = @whereTRANS_DTE  ");
                sbstrSQL.Append(" ) T ");
                sbstrSQL.Append(" ON S1.PAY_TYPE = T.PAY_TYPE ");


                #endregion
                this.SelectOperator.SqlText = sbstrSQL.ToString();
                #region replace SQL parameter
                if (sbstrSQL.ToString().Contains("@wherePAY_TYPE "))
                {
                    this.SelectOperator.SetValue("@wherePAY_TYPE", this.wherePAY_TYPE);
                }
                if (sbstrSQL.ToString().Contains("@whereTRANS_DTE "))
                {
                    this.SelectOperator.SetValue("@whereTRANS_DTE", this.whereTRANS_DTE);
                }

                #endregion
                //myTable set to DataTable object
                myTable = this.SelectOperator.GetDataTable("PUBLIC_HIST");
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
        #region query_for_MBS_BankNbr()
        public string query_for_MBS_BankNbr()
        {
            #region Modify History
            /// <history>
            /// <design>
            /// <name>Cybersoft.COCA DaoGenerator</name>
            /// <date>2012/6/12 上午 12:26:10</date>
            #endregion
            try
            {
                #region set SQL statement
                StringBuilder sbstrSQL = new StringBuilder();
                sbstrSQL.Append(" SELECT b.BUS_BANK_ACCOUNT AS BANK_NBR  ");
                sbstrSQL.Append(" FROM MBS.dbo.MBS_UTLITY a JOIN MBS.dbo.MER_BAS b ");
                sbstrSQL.Append(" ON a.UT_CODE = @wherePAY_TYPE AND a.UT_MER_NO = b.MER_NO ");

                #endregion
                this.SelectOperator.SqlText = sbstrSQL.ToString();
                #region replace SQL parameter
                if (sbstrSQL.ToString().Contains("@wherePAY_TYPE "))
                {
                    this.SelectOperator.SetValue("@wherePAY_TYPE", this.wherePAY_TYPE);
                }
                #endregion
                //myTable set to DataTable object
                myTable = this.SelectOperator.GetDataTable("PUBLIC_HIST");
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
        #region query_CURR_AVAIL()
        public string query_CURR_AVAIL()
        {
            #region Modify History
            /// <history>
            /// <design>
            /// <name>Cybersoft MooreYang</name>
            /// <date>2011/2/9 上午 14:50:00</date>
            #endregion

            try
            {
                #region  set SQL statement
                StringBuilder sbstrSQL = new StringBuilder();
                sbstrSQL.Append("SELECT *, ROW_NUMBER() OVER(ORDER BY T2.PAY_CARD_NBR) AS ROW_COUNT FROM ( ");
                sbstrSQL.Append("  SELECT T1.*, CASE WHEN C.CREDIT_AVAIL > A.CREDIT_AVAIL THEN A.CREDIT_AVAIL ELSE C.CREDIT_AVAIL END CURR_CREDIT_AVAIL ");
                sbstrSQL.Append("    FROM ( ");
                sbstrSQL.Append("          SELECT PAY_CARD_NBR, COUNT(*) CNT, SUM(PAY_AMT) AMT FROM PUBLIC_HIST ");
                sbstrSQL.Append("           WHERE TRANS_DTE = @TRANS_DTE AND PAY_RESULT = 'S000' ");
                sbstrSQL.Append("           GROUP BY PAY_CARD_NBR) T1 ");
                sbstrSQL.Append("    LEFT JOIN CARD_LIMIT C ON C.CARD_NBR = T1.PAY_CARD_NBR ");
                sbstrSQL.Append("    LEFT JOIN CUST_LIMIT A ON A.CUST_NBR = C.ACCT_NBR) T2 ");
                sbstrSQL.Append(" WHERE T2.AMT > T2.CURR_CREDIT_AVAIL ");
                #endregion
                this.SelectOperator.SqlText = sbstrSQL.ToString();
                #region repalce SQL parameter
                if (sbstrSQL.ToString().Contains("@TRANS_DTE"))
                {
                    this.SelectOperator.SetValue("@TRANS_DTE", this.whereTRANS_DTE);
                }
                #endregion
                //myTable set to DataTable object
                myTable = this.SelectOperator.GetDataTable("PUBLIC_HIST");
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
        #region query_for_PAY_NBR_CHANGE() 取得進行台電換號明細
        public string query_for_PAY_NBR_CHANGE()
        {
            #region Modify History
            /// <history>
            /// <design>
            /// <name>Cybersoft</name>
            /// <date>2013/7/9 上午 14:50:00</date>
            #endregion

            try
            {
                #region  set SQL statement
                StringBuilder sbstrSQL = new StringBuilder();
                sbstrSQL.Append("SELECT * FROM PUBLIC_HIST ");
                sbstrSQL.Append(" WHERE  1=1   ");
                if (this.wherePAY_TYPE != null)
                {
                    sbstrSQL.Append("   AND  PAY_TYPE = @wherePAY_TYPE   ");
                }
                if (this.whereFILE_TRANSFER_TYPE != null)
                {
                    sbstrSQL.Append("   AND FILE_TRANSFER_TYPE=@whereFILE_TRANSFER_TYPE ");
                }
                sbstrSQL.Append("   AND TRANS_DTE = @whereTRANS_DTE ");
                sbstrSQL.Append("   AND CHANGE_NBR_NEW != '' ");
                sbstrSQL.Append(" ORDER BY PAY_SEQ ");
                #endregion
                this.SelectOperator.SqlText = sbstrSQL.ToString();
                #region replace SQL parameter
                //this.SelectOperator.SetValue("@wherePAY_TYPE", this.strWherePAY_TYPE);
                if (sbstrSQL.ToString().Contains("@wherePAY_TYPE "))
                {
                    this.SelectOperator.SetValue("@wherePAY_TYPE", this.wherePAY_TYPE, SqlDbType.VarChar);
                }
                if (sbstrSQL.ToString().Contains("@whereFILE_TRANSFER_TYPE "))
                {
                    this.SelectOperator.SetValue("@whereFILE_TRANSFER_TYPE", this.whereFILE_TRANSFER_TYPE, SqlDbType.VarChar);
                }
                this.SelectOperator.SetValue("@whereTRANS_DTE", this.whereTRANS_DTE, SqlDbType.VarChar);
                #endregion

                //myTable set to DataTable object
                myTable = this.SelectOperator.GetDataTable("PUBLIC_HIST");
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
        #region query_PUBLIC_AUTH
        public string query_PUBLIC_AUTH(String BU, String CARD_PRODUCT, String ACCT_NBR)
        {
            #region Modify History
            /// <history>
            /// <design>
            /// <name>Cybersoft MooreYang</name>
            /// <date>2011/2/9 上午 16:50:00</date>
            #endregion

            try
            {
                #region  set SQL statement
                //找出未轉卡之流通正卡且按扣款等級和到期日排序
                StringBuilder sbstrSQL = new StringBuilder();
                sbstrSQL.Append(" SELECT ISNULL(D.PRODUCT_SERVICE_3,'')PRODUCT_SERVICE_3,C.*                          ");
                sbstrSQL.Append("   FROM (SELECT * FROM  CARD_INF                                                     ");
                sbstrSQL.Append("          WHERE BU = @BU AND ACCT_NBR = @ACCT_NBR AND CARD_PRODUCT != @CARD_PRODUCT  ");
                sbstrSQL.Append("            AND CARD_FLAG = 'P' AND TC_CARD_NBR = ''  )C                         ");
                sbstrSQL.Append(" JOIN SETUP_CTLCODE S ON S.CTL_CODE = C.CTL_CODE AND S.CARD_VALID = 'Y'              ");
                sbstrSQL.Append(" LEFT JOIN SETUP_PRODUCT D ON D.PRODUCT = C.CARD_PRODUCT AND D.BU = C.BU             ");
                sbstrSQL.Append(" ORDER BY D.PRODUCT_SERVICE_3 ASC,C.EXPIR_DTE DESC                                   ");
                #endregion
                this.SelectOperator.SqlText = sbstrSQL.ToString();
                #region repalce SQL parameter
                if (sbstrSQL.ToString().Contains("@BU"))
                {
                    this.SelectOperator.SetValue("@BU", BU);
                }
                if (sbstrSQL.ToString().Contains("@CARD_PRODUCT"))
                {
                    this.SelectOperator.SetValue("@CARD_PRODUCT", CARD_PRODUCT);
                }
                if (sbstrSQL.ToString().Contains("@ACCT_NBR"))
                {
                    this.SelectOperator.SetValue("@ACCT_NBR", ACCT_NBR);
                }
                #endregion
                //myTable set to DataTable object
                myTable = this.SelectOperator.GetDataTable("PUBLIC_HIST");
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

        #region query_for_COLA_UPD_AUTH
        public string query_for_COLA_UPD_AUTH(string SEQ, string strTRANS_DTE)
        {
            #region Modify History
            /// <history>
            /// <design>
            /// <name>Cybersoft MooreYang</name>
            /// <date>2011/2/9 上午 16:50:00</date>
            #endregion

            try
            {
                #region  set SQL statement
                StringBuilder sbstrSQL = new StringBuilder();
                sbstrSQL.Append(" SELECT B.AUTH_CODE,B.RSP,B.CHECK_CODE,B.RSN_CODE,A.*                                                         ");
                sbstrSQL.Append("   FROM (select * from PUBLIC_HIST where TRANS_DTE = '" + strTRANS_DTE + "' and PAY_RESULT = 'N002' ) A       ");
                sbstrSQL.Append(" JOIN COLA_UPD_AUTH  B ON A.PAY_CARD_NBR = B.CARD_NBR AND A.PAY_AMT = B.AMT and b.SEQ = '" + SEQ + "'         ");
                #endregion
                this.SelectOperator.SqlText = sbstrSQL.ToString();
                #region repalce SQL parameter
                #endregion
                //myTable set to DataTable object
                myTable = this.SelectOperator.GetDataTable("PUBLIC_HIST");
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
        #region query_AUTH_RPT
        public string query_AUTH_RPT(String FLAG, String TRANS_DTE)
        {
            #region Modify History
            /// <history>
            /// <design>
            /// <name>Cybersoft MooreYang</name>
            /// <date>2013/7/9 上午 16:50:00</date>
            #endregion

            try
            {
                #region  set SQL statement
                StringBuilder sbstrSQL = new StringBuilder();
                sbstrSQL.Append("SELECT DISTINCT P.TRANS_DTE,P.PAY_TYPE,P.BU,P.ACCT_NBR,P.PAY_CARD_NBR,P.EXPIR_DTE,P.PAY_NBR,P.PAY_AMT,P.PAY_DTE,P.PAY_SEQ,P.PAY_RESULT,P.AUTH_CODE,P.ERROR_REASON,A.CUST_ID,B.DESCR, C.DESCR as PAY_RESULT_DESCR  ");
                sbstrSQL.Append("FROM (select * from  PUBLIC_HIST  ");
                switch (FLAG)
                {
                    case "T":
                        sbstrSQL.Append("where TRANS_DTE = @TRANS_DTE ");
                        break;
                    case "A":
                        sbstrSQL.Append("where TRANS_DTE <= @TRANS_DTE ");
                        break;
                }
                sbstrSQL.Append(" )P ");
                sbstrSQL.Append("     JOIN ID_VIEW A  ON P.ACCT_NBR=A.CUST_SEQ  ");
                sbstrSQL.Append("     JOIN SETUP_PUBLIC B ON P.PAY_TYPE=B.PAY_TYPE  ");
                sbstrSQL.Append("     LEFT JOIN SETUP_REJECT C ON C.REJECT_GROUP='PUBLIC' AND P.PAY_RESULT=C.REJECT_CODE   ");
                //sbstrSQL.Append("WHERE P.ACCT_NBR = A.CUST_SEQ ");
                //sbstrSQL.Append("  AND P.PAY_TYPE = B.PAY_TYPE ");
                sbstrSQL.Append(" order by P.PAY_RESULT , P.PAY_TYPE");
                #endregion
                this.SelectOperator.SqlText = sbstrSQL.ToString();
                #region repalce SQL parameter
                if (sbstrSQL.ToString().Contains("@TRANS_DTE"))
                {
                    this.SelectOperator.SetValue("@TRANS_DTE", TRANS_DTE);
                }

                #endregion
                //myTable set to DataTable object
                myTable = this.SelectOperator.GetDataTable("PUBLIC_HIST");
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
        #region query_online_upload()
        public string query_online_upload()
        {
            #region Modify History
            /// <history>
            /// <design>
            /// <name>Cybersoft.COCA DaoGenerator</name>
            /// <date>2012/2/24 下午 03:25:22</date>
            #endregion
            try
            {
                #region set SQL statement
                StringBuilder sbstrSQL = new StringBuilder();
                sbstrSQL.Append("SELECT a.* FROM PUBLIC_HIST a where 1=1 ");
                sbstrSQL.Append("   and (a.PAY_SEQ LIKE '" + this.@wherePAY_SEQ + "%') ");
                if (this.whereTRANS_DTE != null)
                {
                    sbstrSQL.Append(" and a.TRANS_DTE=@whereTRANS_DTE ");
                }
                if (this.wherePAY_TYPE != null)
                {
                    sbstrSQL.Append(" and a.PAY_TYPE=@wherePAY_TYPE ");
                }
                if (this.whereBU != null)
                {
                    sbstrSQL.Append(" and a.BU=@whereBU ");
                }
                if (this.whereACCT_NBR != null)
                {
                    sbstrSQL.Append(" and a.ACCT_NBR=@whereACCT_NBR ");
                }
                if (this.wherePRODUCT != null)
                {
                    sbstrSQL.Append(" and a.PRODUCT=@wherePRODUCT ");
                }
                if (this.whereCARD_PRODUCT != null)
                {
                    sbstrSQL.Append(" and a.CARD_PRODUCT=@whereCARD_PRODUCT ");
                }
                if (this.whereCURRENCY != null)
                {
                    sbstrSQL.Append(" and a.CURRENCY=@whereCURRENCY ");
                }
                if (this.wherePAY_CARD_NBR != null)
                {
                    sbstrSQL.Append(" and a.PAY_CARD_NBR=@wherePAY_CARD_NBR ");
                }
                if (this.whereEXPIR_DTE != null)
                {
                    sbstrSQL.Append(" and a.EXPIR_DTE=@whereEXPIR_DTE ");
                }
                if (this.whereCUST_SEQ != null)
                {
                    sbstrSQL.Append(" and a.CUST_SEQ=@whereCUST_SEQ ");
                }
                if (this.wherePAY_NBR != null)
                {
                    sbstrSQL.Append(" and a.PAY_NBR=@wherePAY_NBR ");
                }
                if (this.wherePAY_AMT > -1000000000000)
                {
                    sbstrSQL.Append(" and a.PAY_AMT=@wherePAY_AMT ");
                }
                if (this.wherePAY_DTE > dateStart)
                {
                    sbstrSQL.Append("  and a.PAY_DTE=@wherePAY_DTE ");
                }
                if (this.wherePAY_RESULT != null)
                {
                    sbstrSQL.Append(" and a.PAY_RESULT=@wherePAY_RESULT ");
                }
                if (this.whereAUTH_CODE != null)
                {
                    sbstrSQL.Append(" and a.AUTH_CODE=@whereAUTH_CODE ");
                }
                if (this.whereERROR_REASON != null)
                {
                    sbstrSQL.Append(" and a.ERROR_REASON=@whereERROR_REASON ");
                }
                if (this.whereREVERSAL_FLAG != null)
                {
                    sbstrSQL.Append(" and a.REVERSAL_FLAG=@whereREVERSAL_FLAG ");
                }
                if (this.whereCTL_CODE != null)
                {
                    sbstrSQL.Append(" and a.CTL_CODE=@whereCTL_CODE ");
                }
                if (this.whereAUTH_RESP != null)
                {
                    sbstrSQL.Append(" and a.AUTH_RESP=@whereAUTH_RESP ");
                }
                if (this.whereCHANGE_NBR_NEW != null)
                {
                    sbstrSQL.Append(" and a.CHANGE_NBR_NEW=@whereCHANGE_NBR_NEW ");
                }
                if (this.whereFILE_TRANSFER_TYPE != null)
                {
                    sbstrSQL.Append(" and a.FILE_TRANSFER_TYPE=@whereFILE_TRANSFER_TYPE ");
                }
                if (this.wherePUBLIC_HIST_FIELD_1 != null)
                {
                    sbstrSQL.Append(" and a.PUBLIC_HIST_FIELD_1=@wherePUBLIC_HIST_FIELD_1 ");
                }
                if (this.wherePUBLIC_HIST_FIELD_2 != null)
                {
                    sbstrSQL.Append(" and a.PUBLIC_HIST_FIELD_2=@wherePUBLIC_HIST_FIELD_2 ");
                }
                if (this.wherePUBLIC_HIST_FIELD_3 != null)
                {
                    sbstrSQL.Append(" and a.PUBLIC_HIST_FIELD_3=@wherePUBLIC_HIST_FIELD_3 ");
                }
                if (this.wherePUBLIC_HIST_FIELD_4 != null)
                {
                    sbstrSQL.Append(" and a.PUBLIC_HIST_FIELD_4=@wherePUBLIC_HIST_FIELD_4 ");
                }
                if (this.wherePUBLIC_HIST_FIELD_5 != null)
                {
                    sbstrSQL.Append(" and a.PUBLIC_HIST_FIELD_5=@wherePUBLIC_HIST_FIELD_5 ");
                }
                if (this.wherePUBLIC_HIST_AMT_1 > -1000000000000)
                {
                    sbstrSQL.Append(" and a.PUBLIC_HIST_AMT_1=@wherePUBLIC_HIST_AMT_1 ");
                }
                if (this.wherePUBLIC_HIST_AMT_2 > -1000000000000)
                {
                    sbstrSQL.Append(" and a.PUBLIC_HIST_AMT_2=@wherePUBLIC_HIST_AMT_2 ");
                }
                if (this.wherePUBLIC_HIST_AMT_3 > -1000000000000)
                {
                    sbstrSQL.Append(" and a.PUBLIC_HIST_AMT_3=@wherePUBLIC_HIST_AMT_3 ");
                }
                if (this.wherePUBLIC_HIST_AMT_4 > -1000000000000)
                {
                    sbstrSQL.Append(" and a.PUBLIC_HIST_AMT_4=@wherePUBLIC_HIST_AMT_4 ");
                }
                if (this.wherePUBLIC_HIST_AMT_5 > -1000000000000)
                {
                    sbstrSQL.Append(" and a.PUBLIC_HIST_AMT_5=@wherePUBLIC_HIST_AMT_5 ");
                }
                if (this.wherePUBLIC_HIST_DT_1 > dateStart)
                {
                    sbstrSQL.Append("  and a.PUBLIC_HIST_DT_1=@wherePUBLIC_HIST_DT_1 ");
                }
                if (this.wherePUBLIC_HIST_DT_2 > dateStart)
                {
                    sbstrSQL.Append("  and a.PUBLIC_HIST_DT_2=@wherePUBLIC_HIST_DT_2 ");
                }
                if (this.wherePUBLIC_HIST_DT_3 > dateStart)
                {
                    sbstrSQL.Append("  and a.PUBLIC_HIST_DT_3=@wherePUBLIC_HIST_DT_3 ");
                }
                if (this.wherePUBLIC_HIST_DT_4 > dateStart)
                {
                    sbstrSQL.Append("  and a.PUBLIC_HIST_DT_4=@wherePUBLIC_HIST_DT_4 ");
                }
                if (this.wherePUBLIC_HIST_DT_5 > dateStart)
                {
                    sbstrSQL.Append("  and a.PUBLIC_HIST_DT_5=@wherePUBLIC_HIST_DT_5 ");
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
                if (sbstrSQL.ToString().Contains("@whereTRANS_DTE "))
                {
                    this.SelectOperator.SetValue("@whereTRANS_DTE", this.whereTRANS_DTE);
                }
                if (sbstrSQL.ToString().Contains("@wherePAY_TYPE "))
                {
                    this.SelectOperator.SetValue("@wherePAY_TYPE", this.wherePAY_TYPE);
                }
                if (sbstrSQL.ToString().Contains("@whereBU "))
                {
                    this.SelectOperator.SetValue("@whereBU", this.whereBU, SqlDbType.VarChar);
                }
                if (sbstrSQL.ToString().Contains("@whereACCT_NBR "))
                {
                    this.SelectOperator.SetValue("@whereACCT_NBR", this.whereACCT_NBR, SqlDbType.VarChar);
                }
                if (sbstrSQL.ToString().Contains("@wherePRODUCT "))
                {
                    this.SelectOperator.SetValue("@wherePRODUCT", this.wherePRODUCT, SqlDbType.VarChar); ;
                }
                if (sbstrSQL.ToString().Contains("@whereCARD_PRODUCT "))
                {
                    this.SelectOperator.SetValue("@whereCARD_PRODUCT", this.whereCARD_PRODUCT);
                }
                if (sbstrSQL.ToString().Contains("@whereCURRENCY "))
                {
                    this.SelectOperator.SetValue("@whereCURRENCY", this.whereCURRENCY, SqlDbType.VarChar);
                }
                if (sbstrSQL.ToString().Contains("@wherePAY_CARD_NBR "))
                {
                    this.SelectOperator.SetValue("@wherePAY_CARD_NBR", this.wherePAY_CARD_NBR);
                }
                if (sbstrSQL.ToString().Contains("@whereEXPIR_DTE "))
                {
                    this.SelectOperator.SetValue("@whereEXPIR_DTE", this.whereEXPIR_DTE);
                }
                if (sbstrSQL.ToString().Contains("@whereCUST_SEQ "))
                {
                    this.SelectOperator.SetValue("@whereCUST_SEQ", this.whereCUST_SEQ);
                }
                if (sbstrSQL.ToString().Contains("@wherePAY_NBR "))
                {
                    this.SelectOperator.SetValue("@wherePAY_NBR", this.wherePAY_NBR);
                }
                if (sbstrSQL.ToString().Contains("@wherePAY_AMT "))
                {
                    this.SelectOperator.SetValue("@wherePAY_AMT", this.wherePAY_AMT);
                }
                if (sbstrSQL.ToString().Contains("@wherePAY_DTE "))
                {
                    this.SelectOperator.SetValue("@wherePAY_DTE", this.wherePAY_DTE);
                }
                if (sbstrSQL.ToString().Contains("@wherePAY_SEQ "))
                {
                    this.SelectOperator.SetValue("@wherePAY_SEQ", this.wherePAY_SEQ);
                }
                if (sbstrSQL.ToString().Contains("@wherePAY_RESULT "))
                {
                    this.SelectOperator.SetValue("@wherePAY_RESULT", this.wherePAY_RESULT);
                }
                if (sbstrSQL.ToString().Contains("@whereAUTH_CODE "))
                {
                    this.SelectOperator.SetValue("@whereAUTH_CODE", this.whereAUTH_CODE);
                }
                if (sbstrSQL.ToString().Contains("@whereERROR_REASON "))
                {
                    this.SelectOperator.SetValue("@whereERROR_REASON", this.whereERROR_REASON);
                }
                if (sbstrSQL.ToString().Contains("@whereREVERSAL_FLAG "))
                {
                    this.SelectOperator.SetValue("@whereREVERSAL_FLAG", this.whereREVERSAL_FLAG);
                }
                if (sbstrSQL.ToString().Contains("@whereCTL_CODE "))
                {
                    this.SelectOperator.SetValue("@whereCTL_CODE", this.whereCTL_CODE);
                }
                if (sbstrSQL.ToString().Contains("@whereAUTH_RESP "))
                {
                    this.SelectOperator.SetValue("@whereAUTH_RESP", this.whereAUTH_RESP);
                }
                if (sbstrSQL.ToString().Contains("@whereCHANGE_NBR_NEW "))
                {
                    this.SelectOperator.SetValue("@whereCHANGE_NBR_NEW", this.whereCHANGE_NBR_NEW);
                }
                if (sbstrSQL.ToString().Contains("@whereFILE_TRANSFER_TYPE "))
                {
                    this.SelectOperator.SetValue("@whereFILE_TRANSFER_TYPE", this.whereFILE_TRANSFER_TYPE);
                }
                if (sbstrSQL.ToString().Contains("@wherePUBLIC_HIST_FIELD_1 "))
                {
                    this.SelectOperator.SetValue("@wherePUBLIC_HIST_FIELD_1", this.wherePUBLIC_HIST_FIELD_1, SqlDbType.VarChar);
                }
                if (sbstrSQL.ToString().Contains("@wherePUBLIC_HIST_FIELD_2 "))
                {
                    this.SelectOperator.SetValue("@wherePUBLIC_HIST_FIELD_2", this.wherePUBLIC_HIST_FIELD_2, SqlDbType.VarChar);
                }
                if (sbstrSQL.ToString().Contains("@wherePUBLIC_HIST_FIELD_3 "))
                {
                    this.SelectOperator.SetValue("@wherePUBLIC_HIST_FIELD_3", this.wherePUBLIC_HIST_FIELD_3, SqlDbType.VarChar);
                }
                if (sbstrSQL.ToString().Contains("@wherePUBLIC_HIST_FIELD_4 "))
                {
                    this.SelectOperator.SetValue("@wherePUBLIC_HIST_FIELD_4", this.wherePUBLIC_HIST_FIELD_4, SqlDbType.VarChar);
                }
                if (sbstrSQL.ToString().Contains("@wherePUBLIC_HIST_FIELD_5 "))
                {
                    this.SelectOperator.SetValue("@wherePUBLIC_HIST_FIELD_5", this.wherePUBLIC_HIST_FIELD_5, SqlDbType.VarChar);
                }
                if (sbstrSQL.ToString().Contains("@wherePUBLIC_HIST_AMT_1 "))
                {
                    this.SelectOperator.SetValue("@wherePUBLIC_HIST_AMT_1", this.wherePUBLIC_HIST_AMT_1, SqlDbType.Decimal);
                }
                if (sbstrSQL.ToString().Contains("@wherePUBLIC_HIST_AMT_2 "))
                {
                    this.SelectOperator.SetValue("@wherePUBLIC_HIST_AMT_2", this.wherePUBLIC_HIST_AMT_2, SqlDbType.Decimal);
                }
                if (sbstrSQL.ToString().Contains("@wherePUBLIC_HIST_AMT_3 "))
                {
                    this.SelectOperator.SetValue("@wherePUBLIC_HIST_AMT_3", this.wherePUBLIC_HIST_AMT_3, SqlDbType.Decimal);
                }
                if (sbstrSQL.ToString().Contains("@wherePUBLIC_HIST_AMT_4 "))
                {
                    this.SelectOperator.SetValue("@wherePUBLIC_HIST_AMT_4", this.wherePUBLIC_HIST_AMT_4, SqlDbType.Decimal);
                }
                if (sbstrSQL.ToString().Contains("@wherePUBLIC_HIST_AMT_5 "))
                {
                    this.SelectOperator.SetValue("@wherePUBLIC_HIST_AMT_5", this.wherePUBLIC_HIST_AMT_5, SqlDbType.Decimal);
                }
                if (sbstrSQL.ToString().Contains("@wherePUBLIC_HIST_DT_1 "))
                {
                    this.SelectOperator.SetValue("@wherePUBLIC_HIST_DT_1", this.wherePUBLIC_HIST_DT_1, SqlDbType.DateTime);
                }
                if (sbstrSQL.ToString().Contains("@wherePUBLIC_HIST_DT_2 "))
                {
                    this.SelectOperator.SetValue("@wherePUBLIC_HIST_DT_2", this.wherePUBLIC_HIST_DT_2, SqlDbType.DateTime);
                }
                if (sbstrSQL.ToString().Contains("@wherePUBLIC_HIST_DT_3 "))
                {
                    this.SelectOperator.SetValue("@wherePUBLIC_HIST_DT_3", this.wherePUBLIC_HIST_DT_3, SqlDbType.DateTime);
                }
                if (sbstrSQL.ToString().Contains("@wherePUBLIC_HIST_DT_4 "))
                {
                    this.SelectOperator.SetValue("@wherePUBLIC_HIST_DT_4", this.wherePUBLIC_HIST_DT_4, SqlDbType.DateTime);
                }
                if (sbstrSQL.ToString().Contains("@wherePUBLIC_HIST_DT_5 "))
                {
                    this.SelectOperator.SetValue("@wherePUBLIC_HIST_DT_5", this.wherePUBLIC_HIST_DT_5, SqlDbType.DateTime);
                }
                if (sbstrSQL.ToString().Contains("@whereMNT_DT "))
                {
                    this.SelectOperator.SetValue("@whereMNT_DT", this.whereMNT_DT);
                }
                if (sbstrSQL.ToString().Contains("@whereMNT_USER "))
                {
                    this.SelectOperator.SetValue("@whereMNT_USER", this.whereMNT_USER);
                }
                #endregion
                //myTable set to DataTable object
                myTable = this.SelectOperator.GetDataTable("PUBLIC_HIST");
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
        #region update_for_rerun() - PBBAUP004
        public string update_for_rerun()
        {
            #region Modify History
            /// <history>
            /// <design>
            /// <name>Cybersoft.COCA DaoGenerator</name>
            /// <date>2014/5/2 下午 03:25:22</date>
            #endregion
            try
            {
                #region set SQL statement
                StringBuilder sbstrSQL = new StringBuilder();
                sbstrSQL.Append("UPDATE PUBLIC_HIST  ");                //update field
                sbstrSQL.Append("   set PAY_RESULT=@PAY_RESULT , AUTH_CODE ='',ERROR_REASON =''     ");
                sbstrSQL.Append(" where  TRANS_DTE=@whereTRANS_DTE                                  ");
                sbstrSQL.Append("    and MNT_DT=@whereMNT_DT                                        ");
                sbstrSQL.Append("    and MNT_USER=@whereMNT_USER                                    ");
                #endregion
                this.UpdateOperator.SqlText = sbstrSQL.ToString();
                #region replace SQL parameter
                if (sbstrSQL.ToString().Contains("@PAY_RESULT "))
                {
                    this.UpdateOperator.SetValue("@PAY_RESULT", this.PAY_RESULT, SqlDbType.VarChar);
                }
                if (sbstrSQL.ToString().Contains("@whereTRANS_DTE "))
                {
                    this.UpdateOperator.SetValue("@whereTRANS_DTE", this.whereTRANS_DTE, SqlDbType.VarChar);
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
    }
}

