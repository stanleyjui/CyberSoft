using System;
using System.Data;
using System.Collections;
using System.Configuration;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Text;

/// <summary>
/// PUBLIC_APPLY_HISTDao，Provide PUBLIC_APPLY_HISTCreate/Read/Update/Delete Function
/// </summary>

namespace Cybersoft.Data.DAL
{
    public partial class testPUBLIC_APPLY_HISTDao : Cybersoft.Data.DAOBase
    {
        #region Modify History
        /// <history>
        /// <design>
        /// <name>Cybersoft.COCA DaoGenerator</name>
        /// <date>2010/11/29 下午 12:02:43</date>
        #endregion
        #region DataBase message convert
        Cybersoft.Dao.Core.MSG_DB MSG = new Cybersoft.Dao.Core.MSG_DB();
        #endregion
        #region Property(Field)
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
           private string ACCT_FLAG = null;
           public string strACCT_FLAG
           {
                get { return ACCT_FLAG; }
                set { ACCT_FLAG = value; }
           }
           private string STATUS = null;
           public string strSTATUS
           {
                get { return STATUS; }
                set { STATUS = value; }
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
           private string CARD_PRODUCT = null;
           public string strCARD_PRODUCT
           {
                get { return CARD_PRODUCT; }
                set { CARD_PRODUCT = value; }
           }
           private string CUST_SEQ = null;
           public string strCUST_SEQ
           {
                get { return CUST_SEQ; }
                set { CUST_SEQ = value; }
           }
           private string ASSIGN_CARD_FLAG = null;
           public string strASSIGN_CARD_FLAG
           {
                get { return ASSIGN_CARD_FLAG; }
                set { ASSIGN_CARD_FLAG = value; }
           }
           private DateTime APPL_DTE = new DateTime(1900, 1, 1);
           public DateTime datetimeAPPL_DTE
           {
                get { return APPL_DTE; }
                set { APPL_DTE = value; }
           }
           private DateTime START_DTE = new DateTime(1900, 1, 1);
           public DateTime datetimeSTART_DTE
           {
                get { return START_DTE; }
                set { START_DTE = value; }
           }
           private DateTime EXPIR_DTE = new DateTime(1900, 1, 1);
           public DateTime datetimeEXPIR_DTE
           {
                get { return EXPIR_DTE; }
                set { EXPIR_DTE = value; }
           }
           private DateTime FIRST_DTE = new DateTime(1900, 1, 1);
           public DateTime datetimeFIRST_DTE
           {
                get { return FIRST_DTE; }
                set { FIRST_DTE = value; }
           }
           private string APPL_NBR = null;
           public string strAPPL_NBR
           {
                get { return APPL_NBR; }
                set { APPL_NBR = value; }
           }
           private string TRANS_FLAG = null;
           public string strTRANS_FLAG
           {
               get { return TRANS_FLAG; }
               set { TRANS_FLAG = value; }
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
           private string whereACCT_FLAG = null;
           public string strWhereACCT_FLAG
           {
                get { return whereACCT_FLAG; }
                set { whereACCT_FLAG = value; }
           }
           private string whereSTATUS = null;
           public string strWhereSTATUS
           {
                get { return whereSTATUS; }
                set { whereSTATUS = value; }
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
           private string whereCARD_PRODUCT = null;
           public string strWhereCARD_PRODUCT
           {
                get { return whereCARD_PRODUCT; }
                set { whereCARD_PRODUCT = value; }
           }
           private string whereCUST_SEQ = null;
           public string strWhereCUST_SEQ
           {
                get { return whereCUST_SEQ; }
                set { whereCUST_SEQ = value; }
           }
           private string whereASSIGN_CARD_FLAG = null;
           public string strWhereASSIGN_CARD_FLAG
           {
                get { return whereASSIGN_CARD_FLAG; }
                set { whereASSIGN_CARD_FLAG = value; }
           }
           private DateTime whereAPPL_DTE = new DateTime(1900, 1, 1);
           public DateTime DateTimeWhereAPPL_DTE
           {
                get { return whereAPPL_DTE; }
                set { whereAPPL_DTE = value; }
           }
           private DateTime whereFromAPPL_DTE = new DateTime(1900, 1, 1);
           public DateTime DateTimeWhereFromAPPL_DTE
           {
               get { return whereFromAPPL_DTE; }
               set { whereFromAPPL_DTE = value; }
           }
           private DateTime whereToAPPL_DTE = new DateTime(1900, 1, 1);
           public DateTime DateTimeWhereToAPPL_DTE
           {
               get { return whereToAPPL_DTE; }
               set { whereToAPPL_DTE = value; }
           }
           private DateTime whereSTART_DTE = new DateTime(1900, 1, 1);
           public DateTime DateTimeWhereSTART_DTE
           {
                get { return whereSTART_DTE; }
                set { whereSTART_DTE = value; }
           }
           private DateTime whereFromSTART_DTE = new DateTime(1900, 1, 1);
           public DateTime DateTimeWhereFromSTART_DTE
           {
               get { return whereFromSTART_DTE; }
               set { whereFromSTART_DTE = value; }
           }
           private DateTime whereToSTART_DTE = new DateTime(1900, 1, 1);
           public DateTime DateTimeWhereToSTART_DTE
           {
               get { return whereToSTART_DTE; }
               set { whereToSTART_DTE = value; }
           }
           private DateTime whereEXPIR_DTE = new DateTime(1900, 1, 1);
           public DateTime DateTimeWhereEXPIR_DTE
           {
                get { return whereEXPIR_DTE; }
                set { whereEXPIR_DTE = value; }
           }
           private DateTime whereFromEXPIR_DTE = new DateTime(1900, 1, 1);
           public DateTime DateTimeWhereFromEXPIR_DTE
           {
               get { return whereFromEXPIR_DTE; }
               set { whereFromEXPIR_DTE = value; }
           }
           private DateTime whereToEXPIR_DTE = new DateTime(1900, 1, 1);
           public DateTime DateTimeWhereToEXPIR_DTE
           {
               get { return whereToEXPIR_DTE; }
               set { whereToEXPIR_DTE = value; }
           }
           private DateTime whereFIRST_DTE = new DateTime(1900, 1, 1);
           public DateTime DateTimeWhereFIRST_DTE
           {
                get { return whereFIRST_DTE; }
                set { whereFIRST_DTE = value; }
           }
           private string whereAPPL_NBR = null;
           public string strWhereAPPL_NBR
           {
                get { return whereAPPL_NBR; }
                set { whereAPPL_NBR = value; }
           }
           private string whereTRANS_FLAG = null;
           public string strWhereTRANS_FLAG
           {
               get { return whereTRANS_FLAG; }
               set { whereTRANS_FLAG = value; }
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
           private string initACCT_FLAG = "";
           public string strinitACCT_FLAG
           {
                get { return initACCT_FLAG; }
                set { initACCT_FLAG = value; }
           }
           private string initSTATUS = "N";
           public string strinitSTATUS
           {
                get { return initSTATUS; }
                set { initSTATUS = value; }
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
           private string initCARD_PRODUCT = "";
           public string strinitCARD_PRODUCT
           {
                get { return initCARD_PRODUCT; }
                set { initCARD_PRODUCT = value; }
           }
           private string initCUST_SEQ = "";
           public string strinitCUST_SEQ
           {
                get { return initCUST_SEQ; }
                set { initCUST_SEQ = value; }
           }
           private string initASSIGN_CARD_FLAG = "";
           public string strinitASSIGN_CARD_FLAG
           {
                get { return initASSIGN_CARD_FLAG; }
                set { initASSIGN_CARD_FLAG = value; }
           }
           private DateTime initAPPL_DTE = new DateTime(1900, 1, 1);
           public DateTime DateTimeinitAPPL_DTE
           {
                get { return initAPPL_DTE; }
                set { initAPPL_DTE = value; }
           }
           private DateTime initSTART_DTE = new DateTime(1900, 1, 1);
           public DateTime DateTimeinitSTART_DTE
           {
                get { return initSTART_DTE; }
                set { initSTART_DTE = value; }
           }
           private DateTime initEXPIR_DTE = new DateTime(1900, 1, 1);
           public DateTime DateTimeinitEXPIR_DTE
           {
                get { return initEXPIR_DTE; }
                set { initEXPIR_DTE = value; }
           }
           private DateTime initFIRST_DTE = new DateTime(1900, 1, 1);
           public DateTime DateTimeinitFIRST_DTE
           {
                get { return initFIRST_DTE; }
                set { initFIRST_DTE = value; }
           }
           private string initAPPL_NBR = "";
           public string strinitAPPL_NBR
           {
                get { return initAPPL_NBR; }
                set { initAPPL_NBR = value; }
           }
           private string initTRANS_FLAG = "";
           public string strinitTRANS_FLAG
           {
               get { return initTRANS_FLAG; }
               set { initTRANS_FLAG = value; }
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
        #region counter
        private  int  InsCnt; //insert counter
        public int intInsCnt
        {
            get { return InsCnt; }
            set { InsCnt = value; }
        } 
        private  int  UptCnt; //update counter
        public int intUptCnt
        {
            get { return UptCnt; }
            set { UptCnt = value; }
        }
        private  int  DelCnt; //delete counter
        public int intDelCnt
        {
            get { return DelCnt; }
            set { DelCnt = value; }
        } 
        #endregion
        #region init value/ Property(DataTable)
        DateTime dateStart = new DateTime(1900, 1, 1); //datetime 
        private  string msg_code;   //message code return
        private  DataTable myTable; //DataTable
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
        /// <date>2010/11/29 下午 12:02:43</date>
        #endregion
           PAY_TYPE = null;
           PAY_NBR = null;
           ACCT_FLAG = null;
           STATUS = null;
           BU = null;
           ACCT_NBR = null;
           PRODUCT = null;
           CURRENCY = null;
           PAY_CARD_NBR = null;
           CARD_PRODUCT = null;
           CUST_SEQ = null;
           ASSIGN_CARD_FLAG = null;
           APPL_DTE = dateStart;
           START_DTE = dateStart;
           EXPIR_DTE = dateStart;
           FIRST_DTE = dateStart;
           APPL_NBR = null;
           TRANS_FLAG = null;
           MNT_DT = dateStart;
           MNT_USER = null;
           wherePAY_TYPE = null;
           wherePAY_NBR = null;
           whereACCT_FLAG = null;
           whereSTATUS = null;
           whereBU = null;
           whereACCT_NBR = null;
           wherePRODUCT = null;
           whereCURRENCY = null;
           wherePAY_CARD_NBR = null;
           whereCARD_PRODUCT = null;
           whereCUST_SEQ = null;
           whereASSIGN_CARD_FLAG = null;
           whereAPPL_DTE = dateStart;
           whereSTART_DTE = dateStart;
           whereEXPIR_DTE = dateStart;
           whereFIRST_DTE = dateStart;
           whereAPPL_NBR = null;
           whereTRANS_FLAG = null;
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
        /// <date>2010/11/29 下午 12:02:43</date>
        #endregion
           PAY_TYPE = initPAY_TYPE;
           PAY_NBR = initPAY_NBR;
           ACCT_FLAG = initACCT_FLAG;
           STATUS = initSTATUS;
           BU = initBU;
           ACCT_NBR = initACCT_NBR;
           PRODUCT = initPRODUCT;
           CURRENCY = initCURRENCY;
           PAY_CARD_NBR = initPAY_CARD_NBR;
           CARD_PRODUCT = initCARD_PRODUCT;
           CUST_SEQ = initCUST_SEQ;
           ASSIGN_CARD_FLAG = initASSIGN_CARD_FLAG;
           APPL_DTE = initAPPL_DTE;
           START_DTE = initSTART_DTE;
           EXPIR_DTE = initEXPIR_DTE;
           FIRST_DTE = initFIRST_DTE;
           APPL_NBR = initAPPL_NBR;
           TRANS_FLAG = initTRANS_FLAG;
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
        /// <date>2010/11/29 下午 12:02:43</date>
        #endregion
        try
        {
            //建立 DataRow物件
            DataRow DR = myTable.NewRow();
            //欄位搬初始值迴圈
            DR["PAY_TYPE"] = initPAY_TYPE;
            DR["PAY_NBR"] = initPAY_NBR;
            DR["ACCT_FLAG"] = initACCT_FLAG;
            DR["STATUS"] = initSTATUS;
            DR["BU"] = initBU;
            DR["ACCT_NBR"] = initACCT_NBR;
            DR["PRODUCT"] = initPRODUCT;
            DR["CURRENCY"] = initCURRENCY;
            DR["PAY_CARD_NBR"] = initPAY_CARD_NBR;
            DR["CARD_PRODUCT"] = initCARD_PRODUCT;
            DR["CUST_SEQ"] = initCUST_SEQ;
            DR["ASSIGN_CARD_FLAG"] = initASSIGN_CARD_FLAG;
            DR["APPL_DTE"] = initAPPL_DTE;
            DR["START_DTE"] = initSTART_DTE;
            DR["EXPIR_DTE"] = initEXPIR_DTE;
            DR["FIRST_DTE"] = initFIRST_DTE;
            DR["APPL_NBR"] = initAPPL_NBR;
            DR["TRANS_FLAG"] = initTRANS_FLAG;
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
        /// <date>2010/11/29 下午 12:02:43</date>
        #endregion
        try
        {
            //建立 Table物件
            myTable = new DataTable();
            //建立 Table欄位
                myTable.Columns.Add("PAY_TYPE", typeof(string));
                myTable.Columns.Add("PAY_NBR", typeof(string));
                myTable.Columns.Add("ACCT_FLAG", typeof(string));
                myTable.Columns.Add("STATUS", typeof(string));
                myTable.Columns.Add("BU", typeof(string));
                myTable.Columns.Add("ACCT_NBR", typeof(string));
                myTable.Columns.Add("PRODUCT", typeof(string));
                myTable.Columns.Add("CURRENCY", typeof(string));
                myTable.Columns.Add("PAY_CARD_NBR", typeof(string));
                myTable.Columns.Add("CARD_PRODUCT", typeof(string));
                myTable.Columns.Add("CUST_SEQ", typeof(string));
                myTable.Columns.Add("ASSIGN_CARD_FLAG", typeof(string));
                myTable.Columns.Add("APPL_DTE", typeof(DateTime));
                myTable.Columns.Add("START_DTE", typeof(DateTime));
                myTable.Columns.Add("EXPIR_DTE", typeof(DateTime));
                myTable.Columns.Add("FIRST_DTE", typeof(DateTime));
                myTable.Columns.Add("APPL_NBR", typeof(string));
                myTable.Columns.Add("TRANS_FLAG", typeof(string));
                myTable.Columns.Add("MNT_DT", typeof(DateTime));
                myTable.Columns.Add("MNT_USER", typeof(string));
            }
            catch (Exception e)
            {
                throw e;
            }
            
        }
        #endregion
        #region insert_by_DT
        public string insert_by_DT()
        {
        #region Modify History
        /// <history>
        /// <design>
        /// <name>Cybersoft.COCA DaoGenerator</name>
        /// <date>2010/11/29 下午 12:02:43</date>
        #endregion
        try
        {
        //Get Connection string
            int rowCount = Cybersoft.Data.DAL.Common.BatchInsert(myTable, "PUBLIC_APPLY_HIST");
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
            MSG.strMsg =Convert.ToString(e.Number) +'-'+ e.Message;
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
        /// <date>2010/11/29 下午 12:02:43</date>
        #endregion
            try
            {
                #region set SQL statement
                StringBuilder sbstrSQL = new StringBuilder();
                sbstrSQL.Append("SELECT a.PAY_TYPE,a.PAY_NBR,a.ACCT_FLAG,a.STATUS,a.BU,a.ACCT_NBR,a.PRODUCT,a.CURRENCY,a.PAY_CARD_NBR,a.CARD_PRODUCT,a.CUST_SEQ,a.ASSIGN_CARD_FLAG,a.APPL_DTE,a.START_DTE,a.EXPIR_DTE,a.FIRST_DTE,a.APPL_NBR,a.TRANS_FLAG,a.MNT_DT,a.MNT_USER FROM PUBLIC_APPLY_HIST a where 1=1 ");
                if (this.wherePAY_TYPE!= null)
                {
                    sbstrSQL.Append(" and a.PAY_TYPE=@wherePAY_TYPE ");
                }
                if (this.wherePAY_NBR!= null)
                {
                    sbstrSQL.Append(" and a.PAY_NBR=@wherePAY_NBR ");
                }
                if (this.whereACCT_FLAG!= null)
                {
                    sbstrSQL.Append(" and a.ACCT_FLAG=@whereACCT_FLAG ");
                }
                if (this.whereSTATUS!= null)
                {
                    sbstrSQL.Append(" and a.STATUS=@whereSTATUS ");
                }
                if (this.whereBU!= null)
                {
                    sbstrSQL.Append(" and a.BU=@whereBU ");
                }
                if (this.whereACCT_NBR!= null)
                {
                    sbstrSQL.Append(" and a.ACCT_NBR=@whereACCT_NBR ");
                }
                if (this.wherePRODUCT!= null)
                {
                    sbstrSQL.Append(" and a.PRODUCT=@wherePRODUCT ");
                }
                if (this.whereCURRENCY!= null)
                {
                    sbstrSQL.Append(" and a.CURRENCY=@whereCURRENCY ");
                }
                if (this.wherePAY_CARD_NBR!= null)
                {
                    sbstrSQL.Append(" and a.PAY_CARD_NBR=@wherePAY_CARD_NBR ");
                }
                if (this.whereCARD_PRODUCT!= null)
                {
                    sbstrSQL.Append(" and a.CARD_PRODUCT=@whereCARD_PRODUCT ");
                }
                if (this.whereCUST_SEQ!= null)
                {
                    sbstrSQL.Append(" and a.CUST_SEQ=@whereCUST_SEQ ");
                }
                if (this.whereASSIGN_CARD_FLAG!= null)
                {
                    sbstrSQL.Append(" and a.ASSIGN_CARD_FLAG=@whereASSIGN_CARD_FLAG ");
                }
                if (this.whereAPPL_DTE > dateStart)
                {
                    sbstrSQL.Append("  and a.APPL_DTE=@whereAPPL_DTE ");
                }
                if (this.whereSTART_DTE > dateStart)
                {
                    sbstrSQL.Append("  and a.START_DTE=@whereSTART_DTE ");
                }
                if (this.whereEXPIR_DTE > dateStart)
                {
                    sbstrSQL.Append("  and a.EXPIR_DTE=@whereEXPIR_DTE ");
                }
                if (this.whereFIRST_DTE > dateStart)
                {
                    sbstrSQL.Append("  and a.FIRST_DTE=@whereFIRST_DTE ");
                }
                if (this.whereAPPL_NBR!= null)
                {
                    sbstrSQL.Append(" and a.APPL_NBR=@whereAPPL_NBR ");
                }
                if (this.whereTRANS_FLAG != null)
                {
                    sbstrSQL.Append(" and a.TRANS_FLAG=@whereTRANS_FLAG ");
                }
                if (this.whereMNT_DT > dateStart)
                {
                    sbstrSQL.Append("  and a.MNT_DT=@whereMNT_DT ");
                }
                if (this.whereMNT_USER!= null)
                {
                    sbstrSQL.Append(" and a.MNT_USER=@whereMNT_USER ");
                }
                #endregion
                this.SelectOperator.SqlText = sbstrSQL.ToString() ;
                #region replace SQL parameter
                if (sbstrSQL.ToString().Contains("@wherePAY_TYPE "))
                {
                    this.SelectOperator.SetValue("@wherePAY_TYPE", this.wherePAY_TYPE);
                }
                if (sbstrSQL.ToString().Contains("@wherePAY_NBR "))
                {
                    this.SelectOperator.SetValue("@wherePAY_NBR", this.wherePAY_NBR);
                }
                if (sbstrSQL.ToString().Contains("@whereACCT_FLAG "))
                {
                    this.SelectOperator.SetValue("@whereACCT_FLAG", this.whereACCT_FLAG);
                }
                if (sbstrSQL.ToString().Contains("@whereSTATUS "))
                {
                    this.SelectOperator.SetValue("@whereSTATUS", this.whereSTATUS);
                }
                if (sbstrSQL.ToString().Contains("@whereBU "))
                {
                    this.SelectOperator.SetValue("@whereBU", this.whereBU);
                }
                if (sbstrSQL.ToString().Contains("@whereACCT_NBR "))
                {
                    this.SelectOperator.SetValue("@whereACCT_NBR", this.whereACCT_NBR);
                }
                if (sbstrSQL.ToString().Contains("@wherePRODUCT "))
                {
                    this.SelectOperator.SetValue("@wherePRODUCT", this.wherePRODUCT);
                }
                if (sbstrSQL.ToString().Contains("@whereCURRENCY "))
                {
                    this.SelectOperator.SetValue("@whereCURRENCY", this.whereCURRENCY);
                }
                if (sbstrSQL.ToString().Contains("@wherePAY_CARD_NBR "))
                {
                    this.SelectOperator.SetValue("@wherePAY_CARD_NBR", this.wherePAY_CARD_NBR);
                }
                if (sbstrSQL.ToString().Contains("@whereCARD_PRODUCT "))
                {
                    this.SelectOperator.SetValue("@whereCARD_PRODUCT", this.whereCARD_PRODUCT);
                }
                if (sbstrSQL.ToString().Contains("@whereCUST_SEQ "))
                {
                    this.SelectOperator.SetValue("@whereCUST_SEQ", this.whereCUST_SEQ);
                }
                if (sbstrSQL.ToString().Contains("@whereASSIGN_CARD_FLAG "))
                {
                    this.SelectOperator.SetValue("@whereASSIGN_CARD_FLAG", this.whereASSIGN_CARD_FLAG);
                }
                if (sbstrSQL.ToString().Contains("@whereAPPL_DTE "))
                {
                    this.SelectOperator.SetValue("@whereAPPL_DTE", this.whereAPPL_DTE);
                }
                if (sbstrSQL.ToString().Contains("@whereSTART_DTE "))
                {
                    this.SelectOperator.SetValue("@whereSTART_DTE", this.whereSTART_DTE);
                }
                if (sbstrSQL.ToString().Contains("@whereEXPIR_DTE "))
                {
                    this.SelectOperator.SetValue("@whereEXPIR_DTE", this.whereEXPIR_DTE);
                }
                if (sbstrSQL.ToString().Contains("@whereFIRST_DTE "))
                {
                    this.SelectOperator.SetValue("@whereFIRST_DTE", this.whereFIRST_DTE);
                }
                if (sbstrSQL.ToString().Contains("@whereAPPL_NBR "))
                {
                    this.SelectOperator.SetValue("@whereAPPL_NBR", this.whereAPPL_NBR);
                }
                if (sbstrSQL.ToString().Contains("@whereTRANS_FLAG "))
                {
                    this.SelectOperator.SetValue("@whereTRANS_FLAG", this.whereTRANS_FLAG);
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
                myTable = this.SelectOperator.GetDataTable("PUBLIC_APPLY_HIST");
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
                MSG.strMsg =Convert.ToString(e.Number) +'-'+ e.Message;
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
            /// <date>2010/11/29 下午 12:02:43</date>
            #endregion
            try
            {
                #region set SQL statement
              StringBuilder sbstrSQL = new StringBuilder();
              sbstrSQL.Append("INSERT INTO PUBLIC_APPLY_HIST (PAY_TYPE,PAY_NBR,ACCT_FLAG,STATUS,BU,ACCT_NBR,PRODUCT,CURRENCY,PAY_CARD_NBR,CARD_PRODUCT,CUST_SEQ,ASSIGN_CARD_FLAG,APPL_DTE,START_DTE,EXPIR_DTE,FIRST_DTE,APPL_NBR,TRANS_FLAG,MNT_DT,MNT_USER) VALUES (@PAY_TYPE ,@PAY_NBR ,@ACCT_FLAG ,@STATUS ,@BU ,@ACCT_NBR ,@PRODUCT ,@CURRENCY ,@PAY_CARD_NBR ,@CARD_PRODUCT ,@CUST_SEQ ,@ASSIGN_CARD_FLAG ,@APPL_DTE ,@START_DTE ,@EXPIR_DTE ,@FIRST_DTE ,@APPL_NBR ,@TRANS_FLAG,@MNT_DT ,@MNT_USER )");
                #endregion
                this.InsertOperator.SqlText = sbstrSQL.ToString();
                #region replace SQL parameter
                this.InsertOperator.SetValue("@PAY_TYPE", this.PAY_TYPE);
                this.InsertOperator.SetValue("@PAY_NBR", this.PAY_NBR);
                this.InsertOperator.SetValue("@ACCT_FLAG", this.ACCT_FLAG);
                this.InsertOperator.SetValue("@STATUS", this.STATUS);
                this.InsertOperator.SetValue("@BU", this.BU);
                this.InsertOperator.SetValue("@ACCT_NBR", this.ACCT_NBR);
                this.InsertOperator.SetValue("@PRODUCT", this.PRODUCT);
                this.InsertOperator.SetValue("@CURRENCY", this.CURRENCY);
                this.InsertOperator.SetValue("@PAY_CARD_NBR", this.PAY_CARD_NBR);
                this.InsertOperator.SetValue("@CARD_PRODUCT", this.CARD_PRODUCT);
                this.InsertOperator.SetValue("@CUST_SEQ", this.CUST_SEQ);
                this.InsertOperator.SetValue("@ASSIGN_CARD_FLAG", this.ASSIGN_CARD_FLAG);
                this.InsertOperator.SetValue("@APPL_DTE", this.APPL_DTE);
                this.InsertOperator.SetValue("@START_DTE", this.START_DTE);
                this.InsertOperator.SetValue("@EXPIR_DTE", this.EXPIR_DTE);
                this.InsertOperator.SetValue("@FIRST_DTE", this.FIRST_DTE);
                this.InsertOperator.SetValue("@APPL_NBR", this.APPL_NBR);
                this.InsertOperator.SetValue("@TRANS_FLAG", this.TRANS_FLAG);
                this.InsertOperator.SetValue("@MNT_DT", this.MNT_DT);
                this.InsertOperator.SetValue("@MNT_USER", this.MNT_USER);
                #endregion
                InsCnt = this.InsertOperator.Execute();
                msg_code = "S0000"; //success
            }
            catch (SqlException e)
            {
                MSG.strMsg =Convert.ToString(e.Number) +'-'+ e.Message;
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
            /// <date>2010/11/29 下午 12:02:43</date>
            #endregion
            try
            {
                 #region set SQL statement
                 StringBuilder sbstrSQL = new StringBuilder();
                sbstrSQL.Append("UPDATE PUBLIC_APPLY_HIST set ");                //update field
                if (this.PAY_TYPE != null)
                {
                    sbstrSQL.Append(" PAY_TYPE=@PAY_TYPE ,");
                }
                if (this.PAY_NBR != null)
                {
                    sbstrSQL.Append(" PAY_NBR=@PAY_NBR ,");
                }
                if (this.ACCT_FLAG != null)
                {
                    sbstrSQL.Append(" ACCT_FLAG=@ACCT_FLAG ,");
                }
                if (this.STATUS != null)
                {
                    sbstrSQL.Append(" STATUS=@STATUS ,");
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
                if (this.CURRENCY != null)
                {
                    sbstrSQL.Append(" CURRENCY=@CURRENCY ,");
                }
                if (this.PAY_CARD_NBR != null)
                {
                    sbstrSQL.Append(" PAY_CARD_NBR=@PAY_CARD_NBR ,");
                }
                if (this.CARD_PRODUCT != null)
                {
                    sbstrSQL.Append(" CARD_PRODUCT=@CARD_PRODUCT ,");
                }
                if (this.CUST_SEQ != null)
                {
                    sbstrSQL.Append(" CUST_SEQ=@CUST_SEQ ,");
                }
                if (this.ASSIGN_CARD_FLAG != null)
                {
                    sbstrSQL.Append(" ASSIGN_CARD_FLAG=@ASSIGN_CARD_FLAG ,");
                }
                if (this.APPL_DTE > dateStart)
                {
                    sbstrSQL.Append(" APPL_DTE=@APPL_DTE ,");
                }
                if (this.START_DTE > dateStart)
                {
                    sbstrSQL.Append(" START_DTE=@START_DTE ,");
                }
                if (this.EXPIR_DTE > dateStart)
                {
                    sbstrSQL.Append(" EXPIR_DTE=@EXPIR_DTE ,");
                }
                if (this.FIRST_DTE > dateStart)
                {
                    sbstrSQL.Append(" FIRST_DTE=@FIRST_DTE ,");
                }
                if (this.APPL_NBR!= null)
                {
                    sbstrSQL.Append(" APPL_NBR=@APPL_NBR ,");
                }
                if (this.TRANS_FLAG != null)
                {
                    sbstrSQL.Append(" TRANS_FLAG=@TRANS_FLAG ,");
                }
                if (this.MNT_DT > dateStart)
                {
                    sbstrSQL.Append(" MNT_DT=@MNT_DT ,");
                }
                if (this.MNT_USER != null)
                {
                    sbstrSQL.Append(" MNT_USER=@MNT_USER ,");
                }
                sbstrSQL.Remove(sbstrSQL.ToString().Length - 1,1); //移除最後一個逗號
                sbstrSQL.Append(" where 1=1 ");
                if (this.wherePAY_TYPE!= null)
                {
                    sbstrSQL.Append(" and PAY_TYPE=@wherePAY_TYPE ");
                }
                if (this.wherePAY_NBR!= null)
                {
                    sbstrSQL.Append(" and PAY_NBR=@wherePAY_NBR ");
                }
                if (this.whereACCT_FLAG!= null)
                {
                    sbstrSQL.Append(" and ACCT_FLAG=@whereACCT_FLAG ");
                }
                if (this.whereSTATUS!= null)
                {
                    sbstrSQL.Append(" and STATUS=@whereSTATUS ");
                }
                if (this.whereBU!= null)
                {
                    sbstrSQL.Append(" and BU=@whereBU ");
                }
                if (this.whereACCT_NBR!= null)
                {
                    sbstrSQL.Append(" and ACCT_NBR=@whereACCT_NBR ");
                }
                if (this.wherePRODUCT!= null)
                {
                    sbstrSQL.Append(" and PRODUCT=@wherePRODUCT ");
                }
                if (this.whereCURRENCY!= null)
                {
                    sbstrSQL.Append(" and CURRENCY=@whereCURRENCY ");
                }
                if (this.wherePAY_CARD_NBR!= null)
                {
                    sbstrSQL.Append(" and PAY_CARD_NBR=@wherePAY_CARD_NBR ");
                }
                if (this.whereCARD_PRODUCT!= null)
                {
                    sbstrSQL.Append(" and CARD_PRODUCT=@whereCARD_PRODUCT ");
                }
                if (this.whereCUST_SEQ!= null)
                {
                    sbstrSQL.Append(" and CUST_SEQ=@whereCUST_SEQ ");
                }
                if (this.whereASSIGN_CARD_FLAG!= null)
                {
                    sbstrSQL.Append(" and ASSIGN_CARD_FLAG=@whereASSIGN_CARD_FLAG ");
                }
                if (this.whereAPPL_DTE > dateStart)
                {
                    sbstrSQL.Append(" and APPL_DTE=@whereAPPL_DTE ");
                }
                if (this.whereSTART_DTE > dateStart)
                {
                    sbstrSQL.Append(" and START_DTE=@whereSTART_DTE ");
                }
                if (this.whereEXPIR_DTE > dateStart)
                {
                    sbstrSQL.Append(" and EXPIR_DTE=@whereEXPIR_DTE ");
                }
                if (this.whereFIRST_DTE > dateStart)
                {
                    sbstrSQL.Append(" and FIRST_DTE=@whereFIRST_DTE ");
                }
                if (this.whereAPPL_NBR!= null)
                {
                    sbstrSQL.Append(" and APPL_NBR=@whereAPPL_NBR ");
                }
                if (this.whereTRANS_FLAG != null)
                {
                    sbstrSQL.Append(" and TRANS_FLAG=@whereTRANS_FLAG ");
                }
                if (this.whereMNT_DT > dateStart)
                {
                    sbstrSQL.Append(" and MNT_DT=@whereMNT_DT ");
                }
                if (this.whereMNT_USER!= null)
                {
                    sbstrSQL.Append(" and MNT_USER=@whereMNT_USER ");
                }
                #endregion
                this.UpdateOperator.SqlText = sbstrSQL.ToString();
                #region replace SQL parameter
                if (sbstrSQL.ToString().Contains("@PAY_TYPE "))
                {
                    this.UpdateOperator.SetValue("@PAY_TYPE", this.PAY_TYPE);
                }
                if (sbstrSQL.ToString().Contains("@PAY_NBR "))
                {
                    this.UpdateOperator.SetValue("@PAY_NBR", this.PAY_NBR);
                }
                if (sbstrSQL.ToString().Contains("@ACCT_FLAG "))
                {
                    this.UpdateOperator.SetValue("@ACCT_FLAG", this.ACCT_FLAG);
                }
                if (sbstrSQL.ToString().Contains("@STATUS "))
                {
                    this.UpdateOperator.SetValue("@STATUS", this.STATUS);
                }
                if (sbstrSQL.ToString().Contains("@BU "))
                {
                    this.UpdateOperator.SetValue("@BU", this.BU);
                }
                if (sbstrSQL.ToString().Contains("@ACCT_NBR "))
                {
                    this.UpdateOperator.SetValue("@ACCT_NBR", this.ACCT_NBR);
                }
                if (sbstrSQL.ToString().Contains("@PRODUCT "))
                {
                    this.UpdateOperator.SetValue("@PRODUCT", this.PRODUCT);
                }
                if (sbstrSQL.ToString().Contains("@CURRENCY "))
                {
                    this.UpdateOperator.SetValue("@CURRENCY", this.CURRENCY);
                }
                if (sbstrSQL.ToString().Contains("@PAY_CARD_NBR "))
                {
                    this.UpdateOperator.SetValue("@PAY_CARD_NBR", this.PAY_CARD_NBR);
                }
                if (sbstrSQL.ToString().Contains("@CARD_PRODUCT "))
                {
                    this.UpdateOperator.SetValue("@CARD_PRODUCT", this.CARD_PRODUCT);
                }
                if (sbstrSQL.ToString().Contains("@CUST_SEQ "))
                {
                    this.UpdateOperator.SetValue("@CUST_SEQ", this.CUST_SEQ);
                }
                if (sbstrSQL.ToString().Contains("@ASSIGN_CARD_FLAG "))
                {
                    this.UpdateOperator.SetValue("@ASSIGN_CARD_FLAG", this.ASSIGN_CARD_FLAG);
                }
                if (sbstrSQL.ToString().Contains("@APPL_DTE "))
                {
                    this.UpdateOperator.SetValue("@APPL_DTE", this.APPL_DTE);
                }
                if (sbstrSQL.ToString().Contains("@START_DTE "))
                {
                    this.UpdateOperator.SetValue("@START_DTE", this.START_DTE);
                }
                if (sbstrSQL.ToString().Contains("@EXPIR_DTE "))
                {
                    this.UpdateOperator.SetValue("@EXPIR_DTE", this.EXPIR_DTE);
                }
                if (sbstrSQL.ToString().Contains("@FIRST_DTE "))
                {
                    this.UpdateOperator.SetValue("@FIRST_DTE", this.FIRST_DTE);
                }
                if (sbstrSQL.ToString().Contains("@APPL_NBR "))
                {
                    this.UpdateOperator.SetValue("@APPL_NBR", this.APPL_NBR);
                }
                if (sbstrSQL.ToString().Contains("@TRANS_FLAG "))
                {
                    this.UpdateOperator.SetValue("@TRANS_FLAG", this.TRANS_FLAG);
                }
                if (sbstrSQL.ToString().Contains("@MNT_DT "))
                {
                    this.UpdateOperator.SetValue("@MNT_DT", this.MNT_DT);
                }
                if (sbstrSQL.ToString().Contains("@MNT_USER "))
                {
                    this.UpdateOperator.SetValue("@MNT_USER", this.MNT_USER);
                }
                if (sbstrSQL.ToString().Contains("@wherePAY_TYPE "))
                {
                    this.UpdateOperator.SetValue("@wherePAY_TYPE", this.wherePAY_TYPE);
                }
                if (sbstrSQL.ToString().Contains("@wherePAY_NBR "))
                {
                    this.UpdateOperator.SetValue("@wherePAY_NBR", this.wherePAY_NBR);
                }
                if (sbstrSQL.ToString().Contains("@whereACCT_FLAG "))
                {
                    this.UpdateOperator.SetValue("@whereACCT_FLAG", this.whereACCT_FLAG);
                }
                if (sbstrSQL.ToString().Contains("@whereSTATUS "))
                {
                    this.UpdateOperator.SetValue("@whereSTATUS", this.whereSTATUS);
                }
                if (sbstrSQL.ToString().Contains("@whereBU "))
                {
                    this.UpdateOperator.SetValue("@whereBU", this.whereBU);
                }
                if (sbstrSQL.ToString().Contains("@whereACCT_NBR "))
                {
                    this.UpdateOperator.SetValue("@whereACCT_NBR", this.whereACCT_NBR);
                }
                if (sbstrSQL.ToString().Contains("@wherePRODUCT "))
                {
                    this.UpdateOperator.SetValue("@wherePRODUCT", this.wherePRODUCT);
                }
                if (sbstrSQL.ToString().Contains("@whereCURRENCY "))
                {
                    this.UpdateOperator.SetValue("@whereCURRENCY", this.whereCURRENCY);
                }
                if (sbstrSQL.ToString().Contains("@wherePAY_CARD_NBR "))
                {
                    this.UpdateOperator.SetValue("@wherePAY_CARD_NBR", this.wherePAY_CARD_NBR);
                }
                if (sbstrSQL.ToString().Contains("@whereCARD_PRODUCT "))
                {
                    this.UpdateOperator.SetValue("@whereCARD_PRODUCT", this.whereCARD_PRODUCT);
                }
                if (sbstrSQL.ToString().Contains("@whereCUST_SEQ "))
                {
                    this.UpdateOperator.SetValue("@whereCUST_SEQ", this.whereCUST_SEQ);
                }
                if (sbstrSQL.ToString().Contains("@whereASSIGN_CARD_FLAG "))
                {
                    this.UpdateOperator.SetValue("@whereASSIGN_CARD_FLAG", this.whereASSIGN_CARD_FLAG);
                }
                if (sbstrSQL.ToString().Contains("@whereAPPL_DTE "))
                {
                    this.UpdateOperator.SetValue("@whereAPPL_DTE", this.whereAPPL_DTE);
                }
                if (sbstrSQL.ToString().Contains("@whereSTART_DTE "))
                {
                    this.UpdateOperator.SetValue("@whereSTART_DTE", this.whereSTART_DTE);
                }
                if (sbstrSQL.ToString().Contains("@whereEXPIR_DTE "))
                {
                    this.UpdateOperator.SetValue("@whereEXPIR_DTE", this.whereEXPIR_DTE);
                }
                if (sbstrSQL.ToString().Contains("@whereFIRST_DTE "))
                {
                    this.UpdateOperator.SetValue("@whereFIRST_DTE", this.whereFIRST_DTE);
                }
                if (sbstrSQL.ToString().Contains("@whereAPPL_NBR "))
                {
                    this.UpdateOperator.SetValue("@whereAPPL_NBR", this.whereAPPL_NBR);
                }
                if (sbstrSQL.ToString().Contains("@whereTRANS_FLAG "))
                {
                    this.UpdateOperator.SetValue("@whereTRANS_FLAG", this.whereTRANS_FLAG);
                }
                if (sbstrSQL.ToString().Contains("@whereMNT_DT "))
                {
                    this.UpdateOperator.SetValue("@whereMNT_DT", this.whereMNT_DT);
                }
                if (sbstrSQL.ToString().Contains("@whereMNT_USER "))
                {
                    this.UpdateOperator.SetValue("@whereMNT_USER", this.whereMNT_USER);
                }
                #endregion
                UptCnt = this.UpdateOperator.Execute();
                msg_code = "S0000"; //update success
            }
            catch (SqlException e)
            {
                MSG.strMsg =Convert.ToString(e.Number) +'-'+ e.Message;
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
           /// <date>2010/11/29 下午 12:02:43</date>
           #endregion
           try
           {
                #region set SQL statement
                StringBuilder sbstrSQL = new StringBuilder();
                sbstrSQL.Append(" DELETE PUBLIC_APPLY_HIST where 1=1 "); 
                if (this.wherePAY_TYPE!= null)
                {
                    sbstrSQL.Append(" and PAY_TYPE=@wherePAY_TYPE ");
                }
                if (this.wherePAY_NBR!= null)
                {
                    sbstrSQL.Append(" and PAY_NBR=@wherePAY_NBR ");
                }
                if (this.whereACCT_FLAG!= null)
                {
                    sbstrSQL.Append(" and ACCT_FLAG=@whereACCT_FLAG ");
                }
                if (this.whereSTATUS!= null)
                {
                    sbstrSQL.Append(" and STATUS=@whereSTATUS ");
                }
                if (this.whereBU!= null)
                {
                    sbstrSQL.Append(" and BU=@whereBU ");
                }
                if (this.whereACCT_NBR!= null)
                {
                    sbstrSQL.Append(" and ACCT_NBR=@whereACCT_NBR ");
                }
                if (this.wherePRODUCT!= null)
                {
                    sbstrSQL.Append(" and PRODUCT=@wherePRODUCT ");
                }
                if (this.whereCURRENCY!= null)
                {
                    sbstrSQL.Append(" and CURRENCY=@whereCURRENCY ");
                }
                if (this.wherePAY_CARD_NBR!= null)
                {
                    sbstrSQL.Append(" and PAY_CARD_NBR=@wherePAY_CARD_NBR ");
                }
                if (this.whereCARD_PRODUCT!= null)
                {
                    sbstrSQL.Append(" and CARD_PRODUCT=@whereCARD_PRODUCT ");
                }
                if (this.whereCUST_SEQ!= null)
                {
                    sbstrSQL.Append(" and CUST_SEQ=@whereCUST_SEQ ");
                }
                if (this.whereASSIGN_CARD_FLAG!= null)
                {
                    sbstrSQL.Append(" and ASSIGN_CARD_FLAG=@whereASSIGN_CARD_FLAG ");
                }
                if (this.whereAPPL_DTE > dateStart)
                {
                    sbstrSQL.Append(" and APPL_DTE=@whereAPPL_DTE ");
                }
                if (this.whereSTART_DTE > dateStart)
                {
                    sbstrSQL.Append(" and START_DTE=@whereSTART_DTE ");
                }
                if (this.whereEXPIR_DTE > dateStart)
                {
                    sbstrSQL.Append(" and EXPIR_DTE=@whereEXPIR_DTE ");
                }
                if (this.whereFIRST_DTE > dateStart)
                {
                    sbstrSQL.Append(" and FIRST_DTE=@whereFIRST_DTE ");
                }
                if (this.whereAPPL_NBR!= null)
                {
                    sbstrSQL.Append(" and APPL_NBR=@whereAPPL_NBR ");
                }
                if (this.whereTRANS_FLAG != null)
                {
                    sbstrSQL.Append(" and TRANS_FLAG=@whereTRANS_FLAG ");
                }
                if (this.whereMNT_DT > dateStart)
                {
                    sbstrSQL.Append(" and MNT_DT=@whereMNT_DT ");
                }
                if (this.whereMNT_USER!= null)
                {
                    sbstrSQL.Append(" and MNT_USER=@whereMNT_USER ");
                }
                #endregion
                this.DeleteOperator.SqlText = sbstrSQL.ToString();
                #region repalce SQL parameter
                if (sbstrSQL.ToString().Contains("@wherePAY_TYPE "))
                {
                    this.DeleteOperator.SetValue("@wherePAY_TYPE", this.wherePAY_TYPE);
                }
                if (sbstrSQL.ToString().Contains("@wherePAY_NBR "))
                {
                    this.DeleteOperator.SetValue("@wherePAY_NBR", this.wherePAY_NBR);
                }
                if (sbstrSQL.ToString().Contains("@whereACCT_FLAG "))
                {
                    this.DeleteOperator.SetValue("@whereACCT_FLAG", this.whereACCT_FLAG);
                }
                if (sbstrSQL.ToString().Contains("@whereSTATUS "))
                {
                    this.DeleteOperator.SetValue("@whereSTATUS", this.whereSTATUS);
                }
                if (sbstrSQL.ToString().Contains("@whereBU "))
                {
                    this.DeleteOperator.SetValue("@whereBU", this.whereBU);
                }
                if (sbstrSQL.ToString().Contains("@whereACCT_NBR "))
                {
                    this.DeleteOperator.SetValue("@whereACCT_NBR", this.whereACCT_NBR);
                }
                if (sbstrSQL.ToString().Contains("@wherePRODUCT "))
                {
                    this.DeleteOperator.SetValue("@wherePRODUCT", this.wherePRODUCT);
                }
                if (sbstrSQL.ToString().Contains("@whereCURRENCY "))
                {
                    this.DeleteOperator.SetValue("@whereCURRENCY", this.whereCURRENCY);
                }
                if (sbstrSQL.ToString().Contains("@wherePAY_CARD_NBR "))
                {
                    this.DeleteOperator.SetValue("@wherePAY_CARD_NBR", this.wherePAY_CARD_NBR);
                }
                if (sbstrSQL.ToString().Contains("@whereCARD_PRODUCT "))
                {
                    this.DeleteOperator.SetValue("@whereCARD_PRODUCT", this.whereCARD_PRODUCT);
                }
                if (sbstrSQL.ToString().Contains("@whereCUST_SEQ "))
                {
                    this.DeleteOperator.SetValue("@whereCUST_SEQ", this.whereCUST_SEQ);
                }
                if (sbstrSQL.ToString().Contains("@whereASSIGN_CARD_FLAG "))
                {
                    this.DeleteOperator.SetValue("@whereASSIGN_CARD_FLAG", this.whereASSIGN_CARD_FLAG);
                }
                if (sbstrSQL.ToString().Contains("@whereAPPL_DTE "))
                {
                    this.DeleteOperator.SetValue("@whereAPPL_DTE", this.whereAPPL_DTE);
                }
                if (sbstrSQL.ToString().Contains("@whereSTART_DTE "))
                {
                    this.DeleteOperator.SetValue("@whereSTART_DTE", this.whereSTART_DTE);
                }
                if (sbstrSQL.ToString().Contains("@whereEXPIR_DTE "))
                {
                    this.DeleteOperator.SetValue("@whereEXPIR_DTE", this.whereEXPIR_DTE);
                }
                if (sbstrSQL.ToString().Contains("@whereFIRST_DTE "))
                {
                    this.DeleteOperator.SetValue("@whereFIRST_DTE", this.whereFIRST_DTE);
                }
                if (sbstrSQL.ToString().Contains("@whereAPPL_NBR "))
                {
                    this.DeleteOperator.SetValue("@whereAPPL_NBR", this.whereAPPL_NBR);
                }
                if (sbstrSQL.ToString().Contains("@whereTRANS_FLAG "))
                {
                    this.DeleteOperator.SetValue("@whereTRANS_FLAG", this.whereTRANS_FLAG);
                }
                if (sbstrSQL.ToString().Contains("@whereMNT_DT "))
                {
                    this.DeleteOperator.SetValue("@whereMNT_DT", this.whereMNT_DT);
                }
                if (sbstrSQL.ToString().Contains("@whereMNT_USER "))
                {
                    this.DeleteOperator.SetValue("@whereMNT_USER", this.whereMNT_USER);
                }
                #endregion
                DelCnt = this.DeleteOperator.Execute();
                msg_code = "S0000"; //delete success
            }
            catch (SqlException e)
            {
                MSG.strMsg =Convert.ToString(e.Number) +'-'+ e.Message;
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
            /// <date>2010/11/29 下午 12:02:43</date>
            #endregion
                try
                {
                    #region 宣告MNT_TODAYDAO並放入初始值
                    Cybersoft.Dao.Core.MNT_TODAYDao MNT_TODAY = new Cybersoft.Dao.Core.MNT_TODAYDao();
                    MNT_TODAY.table_define();
                    MNT_TODAY.strinitTBL_NAME = "PUBLIC_APPLY_HIST";
                    MNT_TODAY.strinitPOST_RESULT = "00";
                    MNT_TODAY.strinitMNT_PGM =  Convert.ToString(HashTB["SessionFN_CODE"]);
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
                    #endregion
                    #region 寫入log
                    int MNT_Count = 0;
                        //修改欄位部分
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
                    if (this.ACCT_FLAG != null && this.ACCT_FLAG != Convert.ToString(myTable.Rows[0]["ACCT_FLAG"]))
                    {
                        MNT_TODAY.initInsert_row();
                        MNT_TODAY.resultTable.Rows[MNT_Count]["FIELD_NAME"] = "ACCT_FLAG";
                        MNT_TODAY.resultTable.Rows[MNT_Count]["VARCHAR_BEFORE"] = Convert.ToString(myTable.Rows[0]["ACCT_FLAG"]);
                        MNT_TODAY.resultTable.Rows[MNT_Count]["VARCHAR_AFTER"] = this.ACCT_FLAG;
                        MNT_Count++;
                    }
                    if (this.STATUS != null && this.STATUS != Convert.ToString(myTable.Rows[0]["STATUS"]))
                    {
                        MNT_TODAY.initInsert_row();
                        MNT_TODAY.resultTable.Rows[MNT_Count]["FIELD_NAME"] = "STATUS";
                        MNT_TODAY.resultTable.Rows[MNT_Count]["VARCHAR_BEFORE"] = Convert.ToString(myTable.Rows[0]["STATUS"]);
                        MNT_TODAY.resultTable.Rows[MNT_Count]["VARCHAR_AFTER"] = this.STATUS;
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
                    if (this.CARD_PRODUCT != null && this.CARD_PRODUCT != Convert.ToString(myTable.Rows[0]["CARD_PRODUCT"]))
                    {
                        MNT_TODAY.initInsert_row();
                        MNT_TODAY.resultTable.Rows[MNT_Count]["FIELD_NAME"] = "CARD_PRODUCT";
                        MNT_TODAY.resultTable.Rows[MNT_Count]["VARCHAR_BEFORE"] = Convert.ToString(myTable.Rows[0]["CARD_PRODUCT"]);
                        MNT_TODAY.resultTable.Rows[MNT_Count]["VARCHAR_AFTER"] = this.CARD_PRODUCT;
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
                    if (this.ASSIGN_CARD_FLAG != null && this.ASSIGN_CARD_FLAG != Convert.ToString(myTable.Rows[0]["ASSIGN_CARD_FLAG"]))
                    {
                        MNT_TODAY.initInsert_row();
                        MNT_TODAY.resultTable.Rows[MNT_Count]["FIELD_NAME"] = "ASSIGN_CARD_FLAG";
                        MNT_TODAY.resultTable.Rows[MNT_Count]["VARCHAR_BEFORE"] = Convert.ToString(myTable.Rows[0]["ASSIGN_CARD_FLAG"]);
                        MNT_TODAY.resultTable.Rows[MNT_Count]["VARCHAR_AFTER"] = this.ASSIGN_CARD_FLAG;
                        MNT_Count++;
                    }
                    if (this.APPL_DTE > dateStart && this.APPL_DTE != Convert.ToDateTime(myTable.Rows[0]["APPL_DTE"]))
                    {
                        MNT_TODAY.initInsert_row();
                        MNT_TODAY.resultTable.Rows[MNT_Count]["FIELD_NAME"] = "APPL_DTE";
                        MNT_TODAY.resultTable.Rows[MNT_Count]["DT_BEFORE"] = Convert.ToDateTime(myTable.Rows[0]["APPL_DTE"]);
                        MNT_TODAY.resultTable.Rows[MNT_Count]["DT_AFTER"] = this.APPL_DTE;
                        MNT_Count++;
                    }
                    if (this.START_DTE > dateStart && this.START_DTE != Convert.ToDateTime(myTable.Rows[0]["START_DTE"]))
                    {
                        MNT_TODAY.initInsert_row();
                        MNT_TODAY.resultTable.Rows[MNT_Count]["FIELD_NAME"] = "START_DTE";
                        MNT_TODAY.resultTable.Rows[MNT_Count]["DT_BEFORE"] = Convert.ToDateTime(myTable.Rows[0]["START_DTE"]);
                        MNT_TODAY.resultTable.Rows[MNT_Count]["DT_AFTER"] = this.START_DTE;
                        MNT_Count++;
                    }
                    if (this.EXPIR_DTE > dateStart && this.EXPIR_DTE != Convert.ToDateTime(myTable.Rows[0]["EXPIR_DTE"]))
                    {
                        MNT_TODAY.initInsert_row();
                        MNT_TODAY.resultTable.Rows[MNT_Count]["FIELD_NAME"] = "EXPIR_DTE";
                        MNT_TODAY.resultTable.Rows[MNT_Count]["DT_BEFORE"] = Convert.ToDateTime(myTable.Rows[0]["EXPIR_DTE"]);
                        MNT_TODAY.resultTable.Rows[MNT_Count]["DT_AFTER"] = this.EXPIR_DTE;
                        MNT_Count++;
                    }
                    if (this.FIRST_DTE > dateStart && this.FIRST_DTE != Convert.ToDateTime(myTable.Rows[0]["FIRST_DTE"]))
                    {
                        MNT_TODAY.initInsert_row();
                        MNT_TODAY.resultTable.Rows[MNT_Count]["FIELD_NAME"] = "FIRST_DTE";
                        MNT_TODAY.resultTable.Rows[MNT_Count]["DT_BEFORE"] = Convert.ToDateTime(myTable.Rows[0]["FIRST_DTE"]);
                        MNT_TODAY.resultTable.Rows[MNT_Count]["DT_AFTER"] = this.FIRST_DTE;
                        MNT_Count++;
                    }
                    if (this.APPL_NBR != null && this.APPL_NBR != Convert.ToString(myTable.Rows[0]["APPL_NBR"]))
                    {
                        MNT_TODAY.initInsert_row();
                        MNT_TODAY.resultTable.Rows[MNT_Count]["FIELD_NAME"] = "APPL_NBR";
                        MNT_TODAY.resultTable.Rows[MNT_Count]["VARCHAR_BEFORE"] = Convert.ToString(myTable.Rows[0]["APPL_NBR"]);
                        MNT_TODAY.resultTable.Rows[MNT_Count]["VARCHAR_AFTER"] = this.APPL_NBR;
                        MNT_Count++;
                    }
                    #endregion
                    if (MNT_TODAY.resultTable.Rows.Count == 0)  //沒有欄位有異動
                    {
                        msg_code = "S0001";
                    }
                    else
                    {
                        msg_code = update();
                        MNT_TODAY.insert_by_DT();
                    }
                }
                catch (SqlException e)
                {
                    MSG.strMsg =Convert.ToString(e.Number) +'-'+ e.Message;
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
        /// <date>2010/11/29 下午 12:02:43</date>
        #endregion
        try
        {
            #region 宣告MNT_TODAYDAO並放入初始值
            Cybersoft.Dao.Core.MNT_TODAYDao MNT_TODAY = new Cybersoft.Dao.Core.MNT_TODAYDao();
            MNT_TODAY.table_define();
            MNT_TODAY.strinitTBL_NAME = "PUBLIC_APPLY_HIST";
            MNT_TODAY.strinitPOST_RESULT = "00";
            MNT_TODAY.strinitMNT_PGM =  Convert.ToString(HashTB["SessionFN_CODE"]);
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
            #endregion
            #region 寫入log
            //修改欄位部分
            MNT_TODAY.initInsert_row();
            MNT_TODAY.resultTable.Rows[0]["FIELD_NAME"] = "INSERT";
            #endregion
            msg_code = insert();
                if (msg_code == "S0000")
            MNT_TODAY.insert_by_DT();
        }
        catch (SqlException e)
        {
            MSG.strMsg =Convert.ToString(e.Number) +'-'+ e.Message;
            msg_code = MSG.getMsg();
        }
        finally
        {
            
        }
        return msg_code;
        }
        #endregion
        #region query_for_collect()
        public string query_for_collect()
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
                sbstrSQL.Append("select * from PUBLIC_APPLY ");
                sbstrSQL.Append("where ( SUBSTRING(PAY_NBR,5,12) = '" + strWherePAY_NBR + "' or PAY_NBR = '" + strWherePAY_NBR.Trim() + "' ) ");
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
        #region query_for_HIST()
        public string query_for_HIST()
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
                sbstrSQL.Append("SELECT * FROM PUBLIC_APPLY_HIST ");
                sbstrSQL.Append("WHERE PAY_TYPE = '" + strWherePAY_TYPE + "' AND PAY_NBR = '" + strWherePAY_NBR + "' AND CUST_SEQ = '" + strWhereCUST_SEQ + "' ");
                #endregion
                this.SelectOperator.SqlText = sbstrSQL.ToString();
                //myTable set to DataTable object
                myTable = this.SelectOperator.GetDataTable("PUBLIC_APPLY_HIST_HIST");
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
        #region query_Feedback_CNT(string strCUST_SEQ)
        public string query_Feedback_CNT(string strCUST_SEQ)
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
                sbstrSQL.Append("SELECT CUST_SEQ, COUNT(*) AS CNT FROM PUBLIC_APPLY_HIST ");
                sbstrSQL.Append("WHERE APPL_NBR <> '' AND CUST_SEQ = '" + strCUST_SEQ + "' ");
                sbstrSQL.Append("GROUP BY CUST_SEQ ");
                sbstrSQL.Append("ORDER BY CUST_SEQ ");
                #endregion
                this.SelectOperator.SqlText = sbstrSQL.ToString();
                //myTable set to DataTable object
                myTable = this.SelectOperator.GetDataTable("PUBLIC_APPLY_HIST");
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
        #region query_for_APPLY_DTE()
        public string query_for_APPLY_DTE(string strPARTIAL_PAY_NBR)
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
                sbstrSQL.Append("SELECT CUST_ID,* FROM PUBLIC_APPLY_HIST H ");
                sbstrSQL.Append("LEFT JOIN ACCT_LINK L ON L.BU = H.BU AND L.CUST_SEQ = H.CUST_SEQ ");
                sbstrSQL.Append("WHERE H.CUST_SEQ = '" + strWhereCUST_SEQ + "' AND PAY_NBR LIKE '" + strPARTIAL_PAY_NBR + "' ");
                sbstrSQL.Append("AND APPL_DTE < '2011-06-01' ");
                sbstrSQL.Append("ORDER BY APPL_NBR ASC, APPL_DTE DESC ");
                #endregion
                this.SelectOperator.SqlText = sbstrSQL.ToString();
                //myTable set to DataTable object
                myTable = this.SelectOperator.GetDataTable("PUBLIC_APPLY_HIST");
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
    }
}

