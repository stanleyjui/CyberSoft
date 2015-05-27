using System;
using System.Data;
using System.Collections;
using System.Configuration;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Text;

/// <summary>
/// PUBLIC_LISTDao，Provide PUBLIC_LISTCreate/Read/Update/Delete Function
/// </summary>

namespace Cybersoft.Data.DAL
{
    public partial class testPUBLIC_LISTDao : Cybersoft.Data.DAOBase
    {
        #region Modify History
        /// <history>
        /// <design>
        /// <name>Cybersoft.COCA DaoGenerator</name>
        /// <date>2014/12/24 上午 11:10:48</date>
        #endregion
        #region DataBase message convert
        Cybersoft.Dao.Core.MSG_DB MSG = new Cybersoft.Dao.Core.MSG_DB();
        #endregion
        #region Property(Field)
        private string PROCESS_DTE = null;
        public string strPROCESS_DTE
        {
            get { return PROCESS_DTE; }
            set { PROCESS_DTE = value; }
        }
        private string RETURN_DTE = null;
        public string strRETURN_DTE
        {
            get { return RETURN_DTE; }
            set { RETURN_DTE = value; }
        }
        private string PAY_TYPE = null;
        public string strPAY_TYPE
        {
            get { return PAY_TYPE; }
            set { PAY_TYPE = value; }
        }
        private string PAY_SEQ = null;
        public string strPAY_SEQ
        {
            get { return PAY_SEQ; }
            set { PAY_SEQ = value; }
        }
        private string PAY_DATA_AREA = null;
        public string strPAY_DATA_AREA
        {
            get { return PAY_DATA_AREA; }
            set { PAY_DATA_AREA = value; }
        }
        private string FILE_TRANSFER_TYPE = null;
        public string strFILE_TRANSFER_TYPE
        {
            get { return FILE_TRANSFER_TYPE; }
            set { FILE_TRANSFER_TYPE = value; }
        }
        private string PUBLIC_LIST_FIELD_1 = null;
        public string strPUBLIC_LIST_FIELD_1
        {
            get { return PUBLIC_LIST_FIELD_1; }
            set { PUBLIC_LIST_FIELD_1 = value; }
        }
        private string PUBLIC_LIST_FIELD_2 = null;
        public string strPUBLIC_LIST_FIELD_2
        {
            get { return PUBLIC_LIST_FIELD_2; }
            set { PUBLIC_LIST_FIELD_2 = value; }
        }
        private string PUBLIC_LIST_FIELD_3 = null;
        public string strPUBLIC_LIST_FIELD_3
        {
            get { return PUBLIC_LIST_FIELD_3; }
            set { PUBLIC_LIST_FIELD_3 = value; }
        }
        private string PUBLIC_LIST_FIELD_4 = null;
        public string strPUBLIC_LIST_FIELD_4
        {
            get { return PUBLIC_LIST_FIELD_4; }
            set { PUBLIC_LIST_FIELD_4 = value; }
        }
        private string PUBLIC_LIST_FIELD_5 = null;
        public string strPUBLIC_LIST_FIELD_5
        {
            get { return PUBLIC_LIST_FIELD_5; }
            set { PUBLIC_LIST_FIELD_5 = value; }
        }
        private decimal PUBLIC_LIST_AMT_1 = -1000000000000;
        public decimal decPUBLIC_LIST_AMT_1
        {
            get { return PUBLIC_LIST_AMT_1; }
            set { PUBLIC_LIST_AMT_1 = value; }
        }
        private decimal PUBLIC_LIST_AMT_2 = -1000000000000;
        public decimal decPUBLIC_LIST_AMT_2
        {
            get { return PUBLIC_LIST_AMT_2; }
            set { PUBLIC_LIST_AMT_2 = value; }
        }
        private decimal PUBLIC_LIST_AMT_3 = -1000000000000;
        public decimal decPUBLIC_LIST_AMT_3
        {
            get { return PUBLIC_LIST_AMT_3; }
            set { PUBLIC_LIST_AMT_3 = value; }
        }
        private decimal PUBLIC_LIST_AMT_4 = -1000000000000;
        public decimal decPUBLIC_LIST_AMT_4
        {
            get { return PUBLIC_LIST_AMT_4; }
            set { PUBLIC_LIST_AMT_4 = value; }
        }
        private decimal PUBLIC_LIST_AMT_5 = -1000000000000;
        public decimal decPUBLIC_LIST_AMT_5
        {
            get { return PUBLIC_LIST_AMT_5; }
            set { PUBLIC_LIST_AMT_5 = value; }
        }
        private DateTime PUBLIC_LIST_DT_1 = new DateTime(1900, 1, 1);
        public DateTime datetimePUBLIC_LIST_DT_1
        {
            get { return PUBLIC_LIST_DT_1; }
            set { PUBLIC_LIST_DT_1 = value; }
        }
        private DateTime PUBLIC_LIST_DT_2 = new DateTime(1900, 1, 1);
        public DateTime datetimePUBLIC_LIST_DT_2
        {
            get { return PUBLIC_LIST_DT_2; }
            set { PUBLIC_LIST_DT_2 = value; }
        }
        private DateTime PUBLIC_LIST_DT_3 = new DateTime(1900, 1, 1);
        public DateTime datetimePUBLIC_LIST_DT_3
        {
            get { return PUBLIC_LIST_DT_3; }
            set { PUBLIC_LIST_DT_3 = value; }
        }
        private DateTime PUBLIC_LIST_DT_4 = new DateTime(1900, 1, 1);
        public DateTime datetimePUBLIC_LIST_DT_4
        {
            get { return PUBLIC_LIST_DT_4; }
            set { PUBLIC_LIST_DT_4 = value; }
        }
        private DateTime PUBLIC_LIST_DT_5 = new DateTime(1900, 1, 1);
        public DateTime datetimePUBLIC_LIST_DT_5
        {
            get { return PUBLIC_LIST_DT_5; }
            set { PUBLIC_LIST_DT_5 = value; }
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
        private string wherePROCESS_DTE = null;
        public string strWherePROCESS_DTE
        {
            get { return wherePROCESS_DTE; }
            set { wherePROCESS_DTE = value; }
        }
        private string whereRETURN_DTE = null;
        public string strWhereRETURN_DTE
        {
            get { return whereRETURN_DTE; }
            set { whereRETURN_DTE = value; }
        }
        private string wherePAY_TYPE = null;
        public string strWherePAY_TYPE
        {
            get { return wherePAY_TYPE; }
            set { wherePAY_TYPE = value; }
        }
        private string wherePAY_SEQ = null;
        public string strWherePAY_SEQ
        {
            get { return wherePAY_SEQ; }
            set { wherePAY_SEQ = value; }
        }
        private string wherePAY_DATA_AREA = null;
        public string strWherePAY_DATA_AREA
        {
            get { return wherePAY_DATA_AREA; }
            set { wherePAY_DATA_AREA = value; }
        }
        private string whereFILE_TRANSFER_TYPE = null;
        public string strWhereFILE_TRANSFER_TYPE
        {
            get { return whereFILE_TRANSFER_TYPE; }
            set { whereFILE_TRANSFER_TYPE = value; }
        }
        private string wherePUBLIC_LIST_FIELD_1 = null;
        public string strWherePUBLIC_LIST_FIELD_1
        {
            get { return wherePUBLIC_LIST_FIELD_1; }
            set { wherePUBLIC_LIST_FIELD_1 = value; }
        }
        private string wherePUBLIC_LIST_FIELD_2 = null;
        public string strWherePUBLIC_LIST_FIELD_2
        {
            get { return wherePUBLIC_LIST_FIELD_2; }
            set { wherePUBLIC_LIST_FIELD_2 = value; }
        }
        private string wherePUBLIC_LIST_FIELD_3 = null;
        public string strWherePUBLIC_LIST_FIELD_3
        {
            get { return wherePUBLIC_LIST_FIELD_3; }
            set { wherePUBLIC_LIST_FIELD_3 = value; }
        }
        private string wherePUBLIC_LIST_FIELD_4 = null;
        public string strWherePUBLIC_LIST_FIELD_4
        {
            get { return wherePUBLIC_LIST_FIELD_4; }
            set { wherePUBLIC_LIST_FIELD_4 = value; }
        }
        private string wherePUBLIC_LIST_FIELD_5 = null;
        public string strWherePUBLIC_LIST_FIELD_5
        {
            get { return wherePUBLIC_LIST_FIELD_5; }
            set { wherePUBLIC_LIST_FIELD_5 = value; }
        }
        private decimal wherePUBLIC_LIST_AMT_1 = -1000000000000;
        public decimal decWherePUBLIC_LIST_AMT_1
        {
            get { return wherePUBLIC_LIST_AMT_1; }
            set { wherePUBLIC_LIST_AMT_1 = value; }
        }
        private decimal wherePUBLIC_LIST_AMT_2 = -1000000000000;
        public decimal decWherePUBLIC_LIST_AMT_2
        {
            get { return wherePUBLIC_LIST_AMT_2; }
            set { wherePUBLIC_LIST_AMT_2 = value; }
        }
        private decimal wherePUBLIC_LIST_AMT_3 = -1000000000000;
        public decimal decWherePUBLIC_LIST_AMT_3
        {
            get { return wherePUBLIC_LIST_AMT_3; }
            set { wherePUBLIC_LIST_AMT_3 = value; }
        }
        private decimal wherePUBLIC_LIST_AMT_4 = -1000000000000;
        public decimal decWherePUBLIC_LIST_AMT_4
        {
            get { return wherePUBLIC_LIST_AMT_4; }
            set { wherePUBLIC_LIST_AMT_4 = value; }
        }
        private decimal wherePUBLIC_LIST_AMT_5 = -1000000000000;
        public decimal decWherePUBLIC_LIST_AMT_5
        {
            get { return wherePUBLIC_LIST_AMT_5; }
            set { wherePUBLIC_LIST_AMT_5 = value; }
        }
        private DateTime wherePUBLIC_LIST_DT_1 = new DateTime(1900, 1, 1);
        public DateTime DateTimeWherePUBLIC_LIST_DT_1
        {
            get { return wherePUBLIC_LIST_DT_1; }
            set { wherePUBLIC_LIST_DT_1 = value; }
        }
        private DateTime wherePUBLIC_LIST_DT_2 = new DateTime(1900, 1, 1);
        public DateTime DateTimeWherePUBLIC_LIST_DT_2
        {
            get { return wherePUBLIC_LIST_DT_2; }
            set { wherePUBLIC_LIST_DT_2 = value; }
        }
        private DateTime wherePUBLIC_LIST_DT_3 = new DateTime(1900, 1, 1);
        public DateTime DateTimeWherePUBLIC_LIST_DT_3
        {
            get { return wherePUBLIC_LIST_DT_3; }
            set { wherePUBLIC_LIST_DT_3 = value; }
        }
        private DateTime wherePUBLIC_LIST_DT_4 = new DateTime(1900, 1, 1);
        public DateTime DateTimeWherePUBLIC_LIST_DT_4
        {
            get { return wherePUBLIC_LIST_DT_4; }
            set { wherePUBLIC_LIST_DT_4 = value; }
        }
        private DateTime wherePUBLIC_LIST_DT_5 = new DateTime(1900, 1, 1);
        public DateTime DateTimeWherePUBLIC_LIST_DT_5
        {
            get { return wherePUBLIC_LIST_DT_5; }
            set { wherePUBLIC_LIST_DT_5 = value; }
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
        private string initPROCESS_DTE = "";
        public string strinitPROCESS_DTE
        {
            get { return initPROCESS_DTE; }
            set { initPROCESS_DTE = value; }
        }
        private string initRETURN_DTE = "";
        public string strinitRETURN_DTE
        {
            get { return initRETURN_DTE; }
            set { initRETURN_DTE = value; }
        }
        private string initPAY_TYPE = "";
        public string strinitPAY_TYPE
        {
            get { return initPAY_TYPE; }
            set { initPAY_TYPE = value; }
        }
        private string initPAY_SEQ = "";
        public string strinitPAY_SEQ
        {
            get { return initPAY_SEQ; }
            set { initPAY_SEQ = value; }
        }
        private string initPAY_DATA_AREA = "";
        public string strinitPAY_DATA_AREA
        {
            get { return initPAY_DATA_AREA; }
            set { initPAY_DATA_AREA = value; }
        }
        private string initFILE_TRANSFER_TYPE = "";
        public string strinitFILE_TRANSFER_TYPE
        {
            get { return initFILE_TRANSFER_TYPE; }
            set { initFILE_TRANSFER_TYPE = value; }
        }
        private string initPUBLIC_LIST_FIELD_1 = "";
        public string strinitPUBLIC_LIST_FIELD_1
        {
            get { return initPUBLIC_LIST_FIELD_1; }
            set { initPUBLIC_LIST_FIELD_1 = value; }
        }
        private string initPUBLIC_LIST_FIELD_2 = "";
        public string strinitPUBLIC_LIST_FIELD_2
        {
            get { return initPUBLIC_LIST_FIELD_2; }
            set { initPUBLIC_LIST_FIELD_2 = value; }
        }
        private string initPUBLIC_LIST_FIELD_3 = "";
        public string strinitPUBLIC_LIST_FIELD_3
        {
            get { return initPUBLIC_LIST_FIELD_3; }
            set { initPUBLIC_LIST_FIELD_3 = value; }
        }
        private string initPUBLIC_LIST_FIELD_4 = "";
        public string strinitPUBLIC_LIST_FIELD_4
        {
            get { return initPUBLIC_LIST_FIELD_4; }
            set { initPUBLIC_LIST_FIELD_4 = value; }
        }
        private string initPUBLIC_LIST_FIELD_5 = "";
        public string strinitPUBLIC_LIST_FIELD_5
        {
            get { return initPUBLIC_LIST_FIELD_5; }
            set { initPUBLIC_LIST_FIELD_5 = value; }
        }
        private decimal initPUBLIC_LIST_AMT_1 = 0;
        public decimal decinitPUBLIC_LIST_AMT_1
        {
            get { return initPUBLIC_LIST_AMT_1; }
            set { initPUBLIC_LIST_AMT_1 = value; }
        }
        private decimal initPUBLIC_LIST_AMT_2 = 0;
        public decimal decinitPUBLIC_LIST_AMT_2
        {
            get { return initPUBLIC_LIST_AMT_2; }
            set { initPUBLIC_LIST_AMT_2 = value; }
        }
        private decimal initPUBLIC_LIST_AMT_3 = 0;
        public decimal decinitPUBLIC_LIST_AMT_3
        {
            get { return initPUBLIC_LIST_AMT_3; }
            set { initPUBLIC_LIST_AMT_3 = value; }
        }
        private decimal initPUBLIC_LIST_AMT_4 = 0;
        public decimal decinitPUBLIC_LIST_AMT_4
        {
            get { return initPUBLIC_LIST_AMT_4; }
            set { initPUBLIC_LIST_AMT_4 = value; }
        }
        private decimal initPUBLIC_LIST_AMT_5 = 0;
        public decimal decinitPUBLIC_LIST_AMT_5
        {
            get { return initPUBLIC_LIST_AMT_5; }
            set { initPUBLIC_LIST_AMT_5 = value; }
        }
        private DateTime initPUBLIC_LIST_DT_1 = new DateTime(1900, 1, 1);
        public DateTime DateTimeinitPUBLIC_LIST_DT_1
        {
            get { return initPUBLIC_LIST_DT_1; }
            set { initPUBLIC_LIST_DT_1 = value; }
        }
        private DateTime initPUBLIC_LIST_DT_2 = new DateTime(1900, 1, 1);
        public DateTime DateTimeinitPUBLIC_LIST_DT_2
        {
            get { return initPUBLIC_LIST_DT_2; }
            set { initPUBLIC_LIST_DT_2 = value; }
        }
        private DateTime initPUBLIC_LIST_DT_3 = new DateTime(1900, 1, 1);
        public DateTime DateTimeinitPUBLIC_LIST_DT_3
        {
            get { return initPUBLIC_LIST_DT_3; }
            set { initPUBLIC_LIST_DT_3 = value; }
        }
        private DateTime initPUBLIC_LIST_DT_4 = new DateTime(1900, 1, 1);
        public DateTime DateTimeinitPUBLIC_LIST_DT_4
        {
            get { return initPUBLIC_LIST_DT_4; }
            set { initPUBLIC_LIST_DT_4 = value; }
        }
        private DateTime initPUBLIC_LIST_DT_5 = new DateTime(1900, 1, 1);
        public DateTime DateTimeinitPUBLIC_LIST_DT_5
        {
            get { return initPUBLIC_LIST_DT_5; }
            set { initPUBLIC_LIST_DT_5 = value; }
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
        private string groupPROCESS_DTE = null;
        public string strGroupPROCESS_DTE
        {
            get { return groupPROCESS_DTE; }
            set { groupPROCESS_DTE = value; }
        }
        private string groupRETURN_DTE = null;
        public string strGroupRETURN_DTE
        {
            get { return groupRETURN_DTE; }
            set { groupRETURN_DTE = value; }
        }
        private string groupPAY_TYPE = null;
        public string strGroupPAY_TYPE
        {
            get { return groupPAY_TYPE; }
            set { groupPAY_TYPE = value; }
        }
        private string groupPAY_SEQ = null;
        public string strGroupPAY_SEQ
        {
            get { return groupPAY_SEQ; }
            set { groupPAY_SEQ = value; }
        }
        private string groupPAY_DATA_AREA = null;
        public string strGroupPAY_DATA_AREA
        {
            get { return groupPAY_DATA_AREA; }
            set { groupPAY_DATA_AREA = value; }
        }
        private string groupFILE_TRANSFER_TYPE = null;
        public string strGroupFILE_TRANSFER_TYPE
        {
            get { return groupFILE_TRANSFER_TYPE; }
            set { groupFILE_TRANSFER_TYPE = value; }
        }
        private string groupPUBLIC_LIST_FIELD_1 = null;
        public string strGroupPUBLIC_LIST_FIELD_1
        {
            get { return groupPUBLIC_LIST_FIELD_1; }
            set { groupPUBLIC_LIST_FIELD_1 = value; }
        }
        private string groupPUBLIC_LIST_FIELD_2 = null;
        public string strGroupPUBLIC_LIST_FIELD_2
        {
            get { return groupPUBLIC_LIST_FIELD_2; }
            set { groupPUBLIC_LIST_FIELD_2 = value; }
        }
        private string groupPUBLIC_LIST_FIELD_3 = null;
        public string strGroupPUBLIC_LIST_FIELD_3
        {
            get { return groupPUBLIC_LIST_FIELD_3; }
            set { groupPUBLIC_LIST_FIELD_3 = value; }
        }
        private string groupPUBLIC_LIST_FIELD_4 = null;
        public string strGroupPUBLIC_LIST_FIELD_4
        {
            get { return groupPUBLIC_LIST_FIELD_4; }
            set { groupPUBLIC_LIST_FIELD_4 = value; }
        }
        private string groupPUBLIC_LIST_FIELD_5 = null;
        public string strGroupPUBLIC_LIST_FIELD_5
        {
            get { return groupPUBLIC_LIST_FIELD_5; }
            set { groupPUBLIC_LIST_FIELD_5 = value; }
        }
        private string groupPUBLIC_LIST_AMT_1 = null;
        public string strGroupPUBLIC_LIST_AMT_1
        {
            get { return groupPUBLIC_LIST_AMT_1; }
            set { groupPUBLIC_LIST_AMT_1 = value; }
        }
        private string groupPUBLIC_LIST_AMT_2 = null;
        public string strGroupPUBLIC_LIST_AMT_2
        {
            get { return groupPUBLIC_LIST_AMT_2; }
            set { groupPUBLIC_LIST_AMT_2 = value; }
        }
        private string groupPUBLIC_LIST_AMT_3 = null;
        public string strGroupPUBLIC_LIST_AMT_3
        {
            get { return groupPUBLIC_LIST_AMT_3; }
            set { groupPUBLIC_LIST_AMT_3 = value; }
        }
        private string groupPUBLIC_LIST_AMT_4 = null;
        public string strGroupPUBLIC_LIST_AMT_4
        {
            get { return groupPUBLIC_LIST_AMT_4; }
            set { groupPUBLIC_LIST_AMT_4 = value; }
        }
        private string groupPUBLIC_LIST_AMT_5 = null;
        public string strGroupPUBLIC_LIST_AMT_5
        {
            get { return groupPUBLIC_LIST_AMT_5; }
            set { groupPUBLIC_LIST_AMT_5 = value; }
        }
        private string groupPUBLIC_LIST_DT_1 = null;
        public string strGroupPUBLIC_LIST_DT_1
        {
            get { return groupPUBLIC_LIST_DT_1; }
            set { groupPUBLIC_LIST_DT_1 = value; }
        }
        private string groupPUBLIC_LIST_DT_2 = null;
        public string strGroupPUBLIC_LIST_DT_2
        {
            get { return groupPUBLIC_LIST_DT_2; }
            set { groupPUBLIC_LIST_DT_2 = value; }
        }
        private string groupPUBLIC_LIST_DT_3 = null;
        public string strGroupPUBLIC_LIST_DT_3
        {
            get { return groupPUBLIC_LIST_DT_3; }
            set { groupPUBLIC_LIST_DT_3 = value; }
        }
        private string groupPUBLIC_LIST_DT_4 = null;
        public string strGroupPUBLIC_LIST_DT_4
        {
            get { return groupPUBLIC_LIST_DT_4; }
            set { groupPUBLIC_LIST_DT_4 = value; }
        }
        private string groupPUBLIC_LIST_DT_5 = null;
        public string strGroupPUBLIC_LIST_DT_5
        {
            get { return groupPUBLIC_LIST_DT_5; }
            set { groupPUBLIC_LIST_DT_5 = value; }
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
            /// <date>2014/12/24 上午 11:10:48</date>
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
            /// <date>2014/12/24 上午 11:10:48</date>
            #endregion
            PROCESS_DTE = null;
            RETURN_DTE = null;
            PAY_TYPE = null;
            PAY_SEQ = null;
            PAY_DATA_AREA = null;
            FILE_TRANSFER_TYPE = null;
            PUBLIC_LIST_FIELD_1 = null;
            PUBLIC_LIST_FIELD_2 = null;
            PUBLIC_LIST_FIELD_3 = null;
            PUBLIC_LIST_FIELD_4 = null;
            PUBLIC_LIST_FIELD_5 = null;
            PUBLIC_LIST_AMT_1 = -1000000000000;
            PUBLIC_LIST_AMT_2 = -1000000000000;
            PUBLIC_LIST_AMT_3 = -1000000000000;
            PUBLIC_LIST_AMT_4 = -1000000000000;
            PUBLIC_LIST_AMT_5 = -1000000000000;
            PUBLIC_LIST_DT_1 = dateStart;
            PUBLIC_LIST_DT_2 = dateStart;
            PUBLIC_LIST_DT_3 = dateStart;
            PUBLIC_LIST_DT_4 = dateStart;
            PUBLIC_LIST_DT_5 = dateStart;
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
            /// <date>2014/12/24 上午 11:10:48</date>
            #endregion
            wherePROCESS_DTE = null;
            whereRETURN_DTE = null;
            wherePAY_TYPE = null;
            wherePAY_SEQ = null;
            wherePAY_DATA_AREA = null;
            whereFILE_TRANSFER_TYPE = null;
            wherePUBLIC_LIST_FIELD_1 = null;
            wherePUBLIC_LIST_FIELD_2 = null;
            wherePUBLIC_LIST_FIELD_3 = null;
            wherePUBLIC_LIST_FIELD_4 = null;
            wherePUBLIC_LIST_FIELD_5 = null;
            wherePUBLIC_LIST_AMT_1 = -1000000000000;
            wherePUBLIC_LIST_AMT_2 = -1000000000000;
            wherePUBLIC_LIST_AMT_3 = -1000000000000;
            wherePUBLIC_LIST_AMT_4 = -1000000000000;
            wherePUBLIC_LIST_AMT_5 = -1000000000000;
            wherePUBLIC_LIST_DT_1 = dateStart;
            wherePUBLIC_LIST_DT_2 = dateStart;
            wherePUBLIC_LIST_DT_3 = dateStart;
            wherePUBLIC_LIST_DT_4 = dateStart;
            wherePUBLIC_LIST_DT_5 = dateStart;
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
            /// <date>2014/12/24 上午 11:10:48</date>
            #endregion
            PROCESS_DTE = initPROCESS_DTE;
            RETURN_DTE = initRETURN_DTE;
            PAY_TYPE = initPAY_TYPE;
            PAY_SEQ = initPAY_SEQ;
            PAY_DATA_AREA = initPAY_DATA_AREA;
            FILE_TRANSFER_TYPE = initFILE_TRANSFER_TYPE;
            PUBLIC_LIST_FIELD_1 = initPUBLIC_LIST_FIELD_1;
            PUBLIC_LIST_FIELD_2 = initPUBLIC_LIST_FIELD_2;
            PUBLIC_LIST_FIELD_3 = initPUBLIC_LIST_FIELD_3;
            PUBLIC_LIST_FIELD_4 = initPUBLIC_LIST_FIELD_4;
            PUBLIC_LIST_FIELD_5 = initPUBLIC_LIST_FIELD_5;
            PUBLIC_LIST_AMT_1 = initPUBLIC_LIST_AMT_1;
            PUBLIC_LIST_AMT_2 = initPUBLIC_LIST_AMT_2;
            PUBLIC_LIST_AMT_3 = initPUBLIC_LIST_AMT_3;
            PUBLIC_LIST_AMT_4 = initPUBLIC_LIST_AMT_4;
            PUBLIC_LIST_AMT_5 = initPUBLIC_LIST_AMT_5;
            PUBLIC_LIST_DT_1 = initPUBLIC_LIST_DT_1;
            PUBLIC_LIST_DT_2 = initPUBLIC_LIST_DT_2;
            PUBLIC_LIST_DT_3 = initPUBLIC_LIST_DT_3;
            PUBLIC_LIST_DT_4 = initPUBLIC_LIST_DT_4;
            PUBLIC_LIST_DT_5 = initPUBLIC_LIST_DT_5;
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
            /// <date>2014/12/24 上午 11:10:48</date>
            #endregion
            try
            {
                //建立 DataRow物件
                DataRow DR = myTable.NewRow();
                //欄位搬初始值迴圈
                DR["PROCESS_DTE"] = initPROCESS_DTE;
                DR["RETURN_DTE"] = initRETURN_DTE;
                DR["PAY_TYPE"] = initPAY_TYPE;
                DR["PAY_SEQ"] = initPAY_SEQ;
                DR["PAY_DATA_AREA"] = initPAY_DATA_AREA;
                DR["FILE_TRANSFER_TYPE"] = initFILE_TRANSFER_TYPE;
                DR["PUBLIC_LIST_FIELD_1"] = initPUBLIC_LIST_FIELD_1;
                DR["PUBLIC_LIST_FIELD_2"] = initPUBLIC_LIST_FIELD_2;
                DR["PUBLIC_LIST_FIELD_3"] = initPUBLIC_LIST_FIELD_3;
                DR["PUBLIC_LIST_FIELD_4"] = initPUBLIC_LIST_FIELD_4;
                DR["PUBLIC_LIST_FIELD_5"] = initPUBLIC_LIST_FIELD_5;
                DR["PUBLIC_LIST_AMT_1"] = initPUBLIC_LIST_AMT_1;
                DR["PUBLIC_LIST_AMT_2"] = initPUBLIC_LIST_AMT_2;
                DR["PUBLIC_LIST_AMT_3"] = initPUBLIC_LIST_AMT_3;
                DR["PUBLIC_LIST_AMT_4"] = initPUBLIC_LIST_AMT_4;
                DR["PUBLIC_LIST_AMT_5"] = initPUBLIC_LIST_AMT_5;
                DR["PUBLIC_LIST_DT_1"] = initPUBLIC_LIST_DT_1;
                DR["PUBLIC_LIST_DT_2"] = initPUBLIC_LIST_DT_2;
                DR["PUBLIC_LIST_DT_3"] = initPUBLIC_LIST_DT_3;
                DR["PUBLIC_LIST_DT_4"] = initPUBLIC_LIST_DT_4;
                DR["PUBLIC_LIST_DT_5"] = initPUBLIC_LIST_DT_5;
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
            /// <date>2014/12/24 上午 11:10:48</date>
            #endregion
            try
            {
                //建立 Table物件
                myTable = new DataTable();
                //建立 Table欄位
                myTable.Columns.Add("PROCESS_DTE", typeof(string));
                myTable.Columns.Add("RETURN_DTE", typeof(string));
                myTable.Columns.Add("PAY_TYPE", typeof(string));
                myTable.Columns.Add("PAY_SEQ", typeof(string));
                myTable.Columns.Add("PAY_DATA_AREA", typeof(string));
                myTable.Columns.Add("FILE_TRANSFER_TYPE", typeof(string));
                myTable.Columns.Add("PUBLIC_LIST_FIELD_1", typeof(string));
                myTable.Columns.Add("PUBLIC_LIST_FIELD_2", typeof(string));
                myTable.Columns.Add("PUBLIC_LIST_FIELD_3", typeof(string));
                myTable.Columns.Add("PUBLIC_LIST_FIELD_4", typeof(string));
                myTable.Columns.Add("PUBLIC_LIST_FIELD_5", typeof(string));
                myTable.Columns.Add("PUBLIC_LIST_AMT_1", typeof(decimal));
                myTable.Columns.Add("PUBLIC_LIST_AMT_2", typeof(decimal));
                myTable.Columns.Add("PUBLIC_LIST_AMT_3", typeof(decimal));
                myTable.Columns.Add("PUBLIC_LIST_AMT_4", typeof(decimal));
                myTable.Columns.Add("PUBLIC_LIST_AMT_5", typeof(decimal));
                myTable.Columns.Add("PUBLIC_LIST_DT_1", typeof(DateTime));
                myTable.Columns.Add("PUBLIC_LIST_DT_2", typeof(DateTime));
                myTable.Columns.Add("PUBLIC_LIST_DT_3", typeof(DateTime));
                myTable.Columns.Add("PUBLIC_LIST_DT_4", typeof(DateTime));
                myTable.Columns.Add("PUBLIC_LIST_DT_5", typeof(DateTime));
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
            /// <date>2014/12/24 上午 11:10:48</date>
            #endregion
            try
            {
                int rowCount = Cybersoft.Data.DAL.Common.BatchInsert(myTable, "PUBLIC_LIST");
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
            /// <date>2014/12/24 上午 11:10:48</date>
            #endregion
            try
            {
                #region set SQL statement
                StringBuilder sbstrSQL = new StringBuilder();
                sbstrSQL.Append("SELECT a.PROCESS_DTE,a.RETURN_DTE,a.PAY_TYPE,a.PAY_SEQ,a.PAY_DATA_AREA,a.FILE_TRANSFER_TYPE,a.PUBLIC_LIST_FIELD_1,a.PUBLIC_LIST_FIELD_2,a.PUBLIC_LIST_FIELD_3,a.PUBLIC_LIST_FIELD_4,a.PUBLIC_LIST_FIELD_5,a.PUBLIC_LIST_AMT_1,a.PUBLIC_LIST_AMT_2,a.PUBLIC_LIST_AMT_3,a.PUBLIC_LIST_AMT_4,a.PUBLIC_LIST_AMT_5,a.PUBLIC_LIST_DT_1,a.PUBLIC_LIST_DT_2,a.PUBLIC_LIST_DT_3,a.PUBLIC_LIST_DT_4,a.PUBLIC_LIST_DT_5,a.MNT_DT,a.MNT_USER FROM PUBLIC_LIST a where 1=1 ");
                if (this.wherePROCESS_DTE != null)
                {
                    sbstrSQL.Append(" and a.PROCESS_DTE=@wherePROCESS_DTE ");
                }
                if (this.whereRETURN_DTE != null)
                {
                    sbstrSQL.Append(" and a.RETURN_DTE=@whereRETURN_DTE ");
                }
                if (this.wherePAY_TYPE != null)
                {
                    sbstrSQL.Append(" and a.PAY_TYPE=@wherePAY_TYPE ");
                }
                if (this.wherePAY_SEQ != null)
                {
                    sbstrSQL.Append(" and a.PAY_SEQ=@wherePAY_SEQ ");
                }
                if (this.wherePAY_DATA_AREA != null)
                {
                    sbstrSQL.Append(" and a.PAY_DATA_AREA=@wherePAY_DATA_AREA ");
                }
                if (this.whereFILE_TRANSFER_TYPE != null)
                {
                    sbstrSQL.Append(" and a.FILE_TRANSFER_TYPE=@whereFILE_TRANSFER_TYPE ");
                }
                if (this.wherePUBLIC_LIST_FIELD_1 != null)
                {
                    sbstrSQL.Append(" and a.PUBLIC_LIST_FIELD_1=@wherePUBLIC_LIST_FIELD_1 ");
                }
                if (this.wherePUBLIC_LIST_FIELD_2 != null)
                {
                    sbstrSQL.Append(" and a.PUBLIC_LIST_FIELD_2=@wherePUBLIC_LIST_FIELD_2 ");
                }
                if (this.wherePUBLIC_LIST_FIELD_3 != null)
                {
                    sbstrSQL.Append(" and a.PUBLIC_LIST_FIELD_3=@wherePUBLIC_LIST_FIELD_3 ");
                }
                if (this.wherePUBLIC_LIST_FIELD_4 != null)
                {
                    sbstrSQL.Append(" and a.PUBLIC_LIST_FIELD_4=@wherePUBLIC_LIST_FIELD_4 ");
                }
                if (this.wherePUBLIC_LIST_FIELD_5 != null)
                {
                    sbstrSQL.Append(" and a.PUBLIC_LIST_FIELD_5=@wherePUBLIC_LIST_FIELD_5 ");
                }
                if (this.wherePUBLIC_LIST_AMT_1 > -1000000000000)
                {
                    sbstrSQL.Append(" and a.PUBLIC_LIST_AMT_1=@wherePUBLIC_LIST_AMT_1 ");
                }
                if (this.wherePUBLIC_LIST_AMT_2 > -1000000000000)
                {
                    sbstrSQL.Append(" and a.PUBLIC_LIST_AMT_2=@wherePUBLIC_LIST_AMT_2 ");
                }
                if (this.wherePUBLIC_LIST_AMT_3 > -1000000000000)
                {
                    sbstrSQL.Append(" and a.PUBLIC_LIST_AMT_3=@wherePUBLIC_LIST_AMT_3 ");
                }
                if (this.wherePUBLIC_LIST_AMT_4 > -1000000000000)
                {
                    sbstrSQL.Append(" and a.PUBLIC_LIST_AMT_4=@wherePUBLIC_LIST_AMT_4 ");
                }
                if (this.wherePUBLIC_LIST_AMT_5 > -1000000000000)
                {
                    sbstrSQL.Append(" and a.PUBLIC_LIST_AMT_5=@wherePUBLIC_LIST_AMT_5 ");
                }
                if (this.wherePUBLIC_LIST_DT_1 > dateStart)
                {
                    sbstrSQL.Append("  and a.PUBLIC_LIST_DT_1=@wherePUBLIC_LIST_DT_1 ");
                }
                if (this.wherePUBLIC_LIST_DT_2 > dateStart)
                {
                    sbstrSQL.Append("  and a.PUBLIC_LIST_DT_2=@wherePUBLIC_LIST_DT_2 ");
                }
                if (this.wherePUBLIC_LIST_DT_3 > dateStart)
                {
                    sbstrSQL.Append("  and a.PUBLIC_LIST_DT_3=@wherePUBLIC_LIST_DT_3 ");
                }
                if (this.wherePUBLIC_LIST_DT_4 > dateStart)
                {
                    sbstrSQL.Append("  and a.PUBLIC_LIST_DT_4=@wherePUBLIC_LIST_DT_4 ");
                }
                if (this.wherePUBLIC_LIST_DT_5 > dateStart)
                {
                    sbstrSQL.Append("  and a.PUBLIC_LIST_DT_5=@wherePUBLIC_LIST_DT_5 ");
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
                if (sbstrSQL.ToString().Contains("@wherePROCESS_DTE "))
                {
                    this.SelectOperator.SetValue("@wherePROCESS_DTE", this.wherePROCESS_DTE, SqlDbType.VarChar);
                }
                if (sbstrSQL.ToString().Contains("@whereRETURN_DTE "))
                {
                    this.SelectOperator.SetValue("@whereRETURN_DTE", this.whereRETURN_DTE, SqlDbType.VarChar);
                }
                if (sbstrSQL.ToString().Contains("@wherePAY_TYPE "))
                {
                    this.SelectOperator.SetValue("@wherePAY_TYPE", this.wherePAY_TYPE, SqlDbType.VarChar);
                }
                if (sbstrSQL.ToString().Contains("@wherePAY_SEQ "))
                {
                    this.SelectOperator.SetValue("@wherePAY_SEQ", this.wherePAY_SEQ, SqlDbType.VarChar);
                }
                if (sbstrSQL.ToString().Contains("@wherePAY_DATA_AREA "))
                {
                    this.SelectOperator.SetValue("@wherePAY_DATA_AREA", this.wherePAY_DATA_AREA, SqlDbType.VarChar);
                }
                if (sbstrSQL.ToString().Contains("@whereFILE_TRANSFER_TYPE "))
                {
                    this.SelectOperator.SetValue("@whereFILE_TRANSFER_TYPE", this.whereFILE_TRANSFER_TYPE, SqlDbType.VarChar);
                }
                if (sbstrSQL.ToString().Contains("@wherePUBLIC_LIST_FIELD_1 "))
                {
                    this.SelectOperator.SetValue("@wherePUBLIC_LIST_FIELD_1", this.wherePUBLIC_LIST_FIELD_1, SqlDbType.VarChar);
                }
                if (sbstrSQL.ToString().Contains("@wherePUBLIC_LIST_FIELD_2 "))
                {
                    this.SelectOperator.SetValue("@wherePUBLIC_LIST_FIELD_2", this.wherePUBLIC_LIST_FIELD_2, SqlDbType.VarChar);
                }
                if (sbstrSQL.ToString().Contains("@wherePUBLIC_LIST_FIELD_3 "))
                {
                    this.SelectOperator.SetValue("@wherePUBLIC_LIST_FIELD_3", this.wherePUBLIC_LIST_FIELD_3, SqlDbType.VarChar);
                }
                if (sbstrSQL.ToString().Contains("@wherePUBLIC_LIST_FIELD_4 "))
                {
                    this.SelectOperator.SetValue("@wherePUBLIC_LIST_FIELD_4", this.wherePUBLIC_LIST_FIELD_4, SqlDbType.VarChar);
                }
                if (sbstrSQL.ToString().Contains("@wherePUBLIC_LIST_FIELD_5 "))
                {
                    this.SelectOperator.SetValue("@wherePUBLIC_LIST_FIELD_5", this.wherePUBLIC_LIST_FIELD_5, SqlDbType.VarChar);
                }
                if (sbstrSQL.ToString().Contains("@wherePUBLIC_LIST_AMT_1 "))
                {
                    this.SelectOperator.SetValue("@wherePUBLIC_LIST_AMT_1", this.wherePUBLIC_LIST_AMT_1, SqlDbType.Decimal);
                }
                if (sbstrSQL.ToString().Contains("@wherePUBLIC_LIST_AMT_2 "))
                {
                    this.SelectOperator.SetValue("@wherePUBLIC_LIST_AMT_2", this.wherePUBLIC_LIST_AMT_2, SqlDbType.Decimal);
                }
                if (sbstrSQL.ToString().Contains("@wherePUBLIC_LIST_AMT_3 "))
                {
                    this.SelectOperator.SetValue("@wherePUBLIC_LIST_AMT_3", this.wherePUBLIC_LIST_AMT_3, SqlDbType.Decimal);
                }
                if (sbstrSQL.ToString().Contains("@wherePUBLIC_LIST_AMT_4 "))
                {
                    this.SelectOperator.SetValue("@wherePUBLIC_LIST_AMT_4", this.wherePUBLIC_LIST_AMT_4, SqlDbType.Decimal);
                }
                if (sbstrSQL.ToString().Contains("@wherePUBLIC_LIST_AMT_5 "))
                {
                    this.SelectOperator.SetValue("@wherePUBLIC_LIST_AMT_5", this.wherePUBLIC_LIST_AMT_5, SqlDbType.Decimal);
                }
                if (sbstrSQL.ToString().Contains("@wherePUBLIC_LIST_DT_1 "))
                {
                    this.SelectOperator.SetValue("@wherePUBLIC_LIST_DT_1", this.wherePUBLIC_LIST_DT_1, SqlDbType.DateTime);
                }
                if (sbstrSQL.ToString().Contains("@wherePUBLIC_LIST_DT_2 "))
                {
                    this.SelectOperator.SetValue("@wherePUBLIC_LIST_DT_2", this.wherePUBLIC_LIST_DT_2, SqlDbType.DateTime);
                }
                if (sbstrSQL.ToString().Contains("@wherePUBLIC_LIST_DT_3 "))
                {
                    this.SelectOperator.SetValue("@wherePUBLIC_LIST_DT_3", this.wherePUBLIC_LIST_DT_3, SqlDbType.DateTime);
                }
                if (sbstrSQL.ToString().Contains("@wherePUBLIC_LIST_DT_4 "))
                {
                    this.SelectOperator.SetValue("@wherePUBLIC_LIST_DT_4", this.wherePUBLIC_LIST_DT_4, SqlDbType.DateTime);
                }
                if (sbstrSQL.ToString().Contains("@wherePUBLIC_LIST_DT_5 "))
                {
                    this.SelectOperator.SetValue("@wherePUBLIC_LIST_DT_5", this.wherePUBLIC_LIST_DT_5, SqlDbType.DateTime);
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
                myTable = this.SelectOperator.GetDataTable("PUBLIC_LIST");
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
            /// <date>2014/12/24 上午 11:10:48</date>
            #endregion
            try
            {
                #region set SQL statement
                StringBuilder sbstrSQL = new StringBuilder();
                sbstrSQL.Append("INSERT INTO PUBLIC_LIST (PROCESS_DTE,RETURN_DTE,PAY_TYPE,PAY_SEQ,PAY_DATA_AREA,FILE_TRANSFER_TYPE,PUBLIC_LIST_FIELD_1,PUBLIC_LIST_FIELD_2,PUBLIC_LIST_FIELD_3,PUBLIC_LIST_FIELD_4,PUBLIC_LIST_FIELD_5,PUBLIC_LIST_AMT_1,PUBLIC_LIST_AMT_2,PUBLIC_LIST_AMT_3,PUBLIC_LIST_AMT_4,PUBLIC_LIST_AMT_5,PUBLIC_LIST_DT_1,PUBLIC_LIST_DT_2,PUBLIC_LIST_DT_3,PUBLIC_LIST_DT_4,PUBLIC_LIST_DT_5,MNT_DT,MNT_USER) VALUES (@PROCESS_DTE ,@RETURN_DTE ,@PAY_TYPE ,@PAY_SEQ ,@PAY_DATA_AREA ,@FILE_TRANSFER_TYPE ,@PUBLIC_LIST_FIELD_1 ,@PUBLIC_LIST_FIELD_2 ,@PUBLIC_LIST_FIELD_3 ,@PUBLIC_LIST_FIELD_4 ,@PUBLIC_LIST_FIELD_5 ,@PUBLIC_LIST_AMT_1 ,@PUBLIC_LIST_AMT_2 ,@PUBLIC_LIST_AMT_3 ,@PUBLIC_LIST_AMT_4 ,@PUBLIC_LIST_AMT_5 ,@PUBLIC_LIST_DT_1 ,@PUBLIC_LIST_DT_2 ,@PUBLIC_LIST_DT_3 ,@PUBLIC_LIST_DT_4 ,@PUBLIC_LIST_DT_5 ,@MNT_DT ,@MNT_USER )");
                #endregion
                this.InsertOperator.SqlText = sbstrSQL.ToString();
                #region replace SQL parameter
                this.InsertOperator.SetValue("@PROCESS_DTE", this.PROCESS_DTE, SqlDbType.VarChar);
                this.InsertOperator.SetValue("@RETURN_DTE", this.RETURN_DTE, SqlDbType.VarChar);
                this.InsertOperator.SetValue("@PAY_TYPE", this.PAY_TYPE, SqlDbType.VarChar);
                this.InsertOperator.SetValue("@PAY_SEQ", this.PAY_SEQ, SqlDbType.VarChar);
                this.InsertOperator.SetValue("@PAY_DATA_AREA", this.PAY_DATA_AREA, SqlDbType.VarChar);
                this.InsertOperator.SetValue("@FILE_TRANSFER_TYPE", this.FILE_TRANSFER_TYPE, SqlDbType.VarChar);
                this.InsertOperator.SetValue("@PUBLIC_LIST_FIELD_1", this.PUBLIC_LIST_FIELD_1, SqlDbType.VarChar);
                this.InsertOperator.SetValue("@PUBLIC_LIST_FIELD_2", this.PUBLIC_LIST_FIELD_2, SqlDbType.VarChar);
                this.InsertOperator.SetValue("@PUBLIC_LIST_FIELD_3", this.PUBLIC_LIST_FIELD_3, SqlDbType.VarChar);
                this.InsertOperator.SetValue("@PUBLIC_LIST_FIELD_4", this.PUBLIC_LIST_FIELD_4, SqlDbType.VarChar);
                this.InsertOperator.SetValue("@PUBLIC_LIST_FIELD_5", this.PUBLIC_LIST_FIELD_5, SqlDbType.VarChar);
                this.InsertOperator.SetValue("@PUBLIC_LIST_AMT_1", this.PUBLIC_LIST_AMT_1, SqlDbType.Decimal);
                this.InsertOperator.SetValue("@PUBLIC_LIST_AMT_2", this.PUBLIC_LIST_AMT_2, SqlDbType.Decimal);
                this.InsertOperator.SetValue("@PUBLIC_LIST_AMT_3", this.PUBLIC_LIST_AMT_3, SqlDbType.Decimal);
                this.InsertOperator.SetValue("@PUBLIC_LIST_AMT_4", this.PUBLIC_LIST_AMT_4, SqlDbType.Decimal);
                this.InsertOperator.SetValue("@PUBLIC_LIST_AMT_5", this.PUBLIC_LIST_AMT_5, SqlDbType.Decimal);
                this.InsertOperator.SetValue("@PUBLIC_LIST_DT_1", this.PUBLIC_LIST_DT_1, SqlDbType.DateTime);
                this.InsertOperator.SetValue("@PUBLIC_LIST_DT_2", this.PUBLIC_LIST_DT_2, SqlDbType.DateTime);
                this.InsertOperator.SetValue("@PUBLIC_LIST_DT_3", this.PUBLIC_LIST_DT_3, SqlDbType.DateTime);
                this.InsertOperator.SetValue("@PUBLIC_LIST_DT_4", this.PUBLIC_LIST_DT_4, SqlDbType.DateTime);
                this.InsertOperator.SetValue("@PUBLIC_LIST_DT_5", this.PUBLIC_LIST_DT_5, SqlDbType.DateTime);
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
            /// <date>2014/12/24 上午 11:10:48</date>
            #endregion
            try
            {
                #region set SQL statement
                StringBuilder sbstrSQL = new StringBuilder();
                sbstrSQL.Append("UPDATE PUBLIC_LIST set ");                //update field
                if (this.PROCESS_DTE != null)
                {
                    sbstrSQL.Append(" PROCESS_DTE=@PROCESS_DTE ,");
                }
                if (this.RETURN_DTE != null)
                {
                    sbstrSQL.Append(" RETURN_DTE=@RETURN_DTE ,");
                }
                if (this.PAY_TYPE != null)
                {
                    sbstrSQL.Append(" PAY_TYPE=@PAY_TYPE ,");
                }
                if (this.PAY_SEQ != null)
                {
                    sbstrSQL.Append(" PAY_SEQ=@PAY_SEQ ,");
                }
                if (this.PAY_DATA_AREA != null)
                {
                    sbstrSQL.Append(" PAY_DATA_AREA=@PAY_DATA_AREA ,");
                }
                if (this.FILE_TRANSFER_TYPE != null)
                {
                    sbstrSQL.Append(" FILE_TRANSFER_TYPE=@FILE_TRANSFER_TYPE ,");
                }
                if (this.PUBLIC_LIST_FIELD_1 != null)
                {
                    sbstrSQL.Append(" PUBLIC_LIST_FIELD_1=@PUBLIC_LIST_FIELD_1 ,");
                }
                if (this.PUBLIC_LIST_FIELD_2 != null)
                {
                    sbstrSQL.Append(" PUBLIC_LIST_FIELD_2=@PUBLIC_LIST_FIELD_2 ,");
                }
                if (this.PUBLIC_LIST_FIELD_3 != null)
                {
                    sbstrSQL.Append(" PUBLIC_LIST_FIELD_3=@PUBLIC_LIST_FIELD_3 ,");
                }
                if (this.PUBLIC_LIST_FIELD_4 != null)
                {
                    sbstrSQL.Append(" PUBLIC_LIST_FIELD_4=@PUBLIC_LIST_FIELD_4 ,");
                }
                if (this.PUBLIC_LIST_FIELD_5 != null)
                {
                    sbstrSQL.Append(" PUBLIC_LIST_FIELD_5=@PUBLIC_LIST_FIELD_5 ,");
                }
                if (this.PUBLIC_LIST_AMT_1 > -1000000000000)
                {
                    sbstrSQL.Append(" PUBLIC_LIST_AMT_1=@PUBLIC_LIST_AMT_1 ,");
                }
                if (this.PUBLIC_LIST_AMT_2 > -1000000000000)
                {
                    sbstrSQL.Append(" PUBLIC_LIST_AMT_2=@PUBLIC_LIST_AMT_2 ,");
                }
                if (this.PUBLIC_LIST_AMT_3 > -1000000000000)
                {
                    sbstrSQL.Append(" PUBLIC_LIST_AMT_3=@PUBLIC_LIST_AMT_3 ,");
                }
                if (this.PUBLIC_LIST_AMT_4 > -1000000000000)
                {
                    sbstrSQL.Append(" PUBLIC_LIST_AMT_4=@PUBLIC_LIST_AMT_4 ,");
                }
                if (this.PUBLIC_LIST_AMT_5 > -1000000000000)
                {
                    sbstrSQL.Append(" PUBLIC_LIST_AMT_5=@PUBLIC_LIST_AMT_5 ,");
                }
                if (this.PUBLIC_LIST_DT_1 > dateStart)
                {
                    if (this.PUBLIC_LIST_DT_1.ToString("yyyyMMdd") == "19000101")
                    {
                        sbstrSQL.Append(" PUBLIC_LIST_DT_1= '19000101' ,");
                    }
                    else
                    {
                        sbstrSQL.Append(" PUBLIC_LIST_DT_1=@PUBLIC_LIST_DT_1 ,");
                    }
                }
                if (this.PUBLIC_LIST_DT_2 > dateStart)
                {
                    if (this.PUBLIC_LIST_DT_2.ToString("yyyyMMdd") == "19000101")
                    {
                        sbstrSQL.Append(" PUBLIC_LIST_DT_2= '19000101' ,");
                    }
                    else
                    {
                        sbstrSQL.Append(" PUBLIC_LIST_DT_2=@PUBLIC_LIST_DT_2 ,");
                    }
                }
                if (this.PUBLIC_LIST_DT_3 > dateStart)
                {
                    if (this.PUBLIC_LIST_DT_3.ToString("yyyyMMdd") == "19000101")
                    {
                        sbstrSQL.Append(" PUBLIC_LIST_DT_3= '19000101' ,");
                    }
                    else
                    {
                        sbstrSQL.Append(" PUBLIC_LIST_DT_3=@PUBLIC_LIST_DT_3 ,");
                    }
                }
                if (this.PUBLIC_LIST_DT_4 > dateStart)
                {
                    if (this.PUBLIC_LIST_DT_4.ToString("yyyyMMdd") == "19000101")
                    {
                        sbstrSQL.Append(" PUBLIC_LIST_DT_4= '19000101' ,");
                    }
                    else
                    {
                        sbstrSQL.Append(" PUBLIC_LIST_DT_4=@PUBLIC_LIST_DT_4 ,");
                    }
                }
                if (this.PUBLIC_LIST_DT_5 > dateStart)
                {
                    if (this.PUBLIC_LIST_DT_5.ToString("yyyyMMdd") == "19000101")
                    {
                        sbstrSQL.Append(" PUBLIC_LIST_DT_5= '19000101' ,");
                    }
                    else
                    {
                        sbstrSQL.Append(" PUBLIC_LIST_DT_5=@PUBLIC_LIST_DT_5 ,");
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
                if (this.wherePROCESS_DTE != null)
                {
                    sbstrSQL.Append(" and PROCESS_DTE=@wherePROCESS_DTE ");
                }
                if (this.whereRETURN_DTE != null)
                {
                    sbstrSQL.Append(" and RETURN_DTE=@whereRETURN_DTE ");
                }
                if (this.wherePAY_TYPE != null)
                {
                    sbstrSQL.Append(" and PAY_TYPE=@wherePAY_TYPE ");
                }
                if (this.wherePAY_SEQ != null)
                {
                    sbstrSQL.Append(" and PAY_SEQ=@wherePAY_SEQ ");
                }
                if (this.wherePAY_DATA_AREA != null)
                {
                    sbstrSQL.Append(" and PAY_DATA_AREA=@wherePAY_DATA_AREA ");
                }
                if (this.whereFILE_TRANSFER_TYPE != null)
                {
                    sbstrSQL.Append(" and FILE_TRANSFER_TYPE=@whereFILE_TRANSFER_TYPE ");
                }
                if (this.wherePUBLIC_LIST_FIELD_1 != null)
                {
                    sbstrSQL.Append(" and PUBLIC_LIST_FIELD_1=@wherePUBLIC_LIST_FIELD_1 ");
                }
                if (this.wherePUBLIC_LIST_FIELD_2 != null)
                {
                    sbstrSQL.Append(" and PUBLIC_LIST_FIELD_2=@wherePUBLIC_LIST_FIELD_2 ");
                }
                if (this.wherePUBLIC_LIST_FIELD_3 != null)
                {
                    sbstrSQL.Append(" and PUBLIC_LIST_FIELD_3=@wherePUBLIC_LIST_FIELD_3 ");
                }
                if (this.wherePUBLIC_LIST_FIELD_4 != null)
                {
                    sbstrSQL.Append(" and PUBLIC_LIST_FIELD_4=@wherePUBLIC_LIST_FIELD_4 ");
                }
                if (this.wherePUBLIC_LIST_FIELD_5 != null)
                {
                    sbstrSQL.Append(" and PUBLIC_LIST_FIELD_5=@wherePUBLIC_LIST_FIELD_5 ");
                }
                if (this.wherePUBLIC_LIST_AMT_1 > -1000000000000)
                {
                    sbstrSQL.Append(" and PUBLIC_LIST_AMT_1=@wherePUBLIC_LIST_AMT_1 ");
                }
                if (this.wherePUBLIC_LIST_AMT_2 > -1000000000000)
                {
                    sbstrSQL.Append(" and PUBLIC_LIST_AMT_2=@wherePUBLIC_LIST_AMT_2 ");
                }
                if (this.wherePUBLIC_LIST_AMT_3 > -1000000000000)
                {
                    sbstrSQL.Append(" and PUBLIC_LIST_AMT_3=@wherePUBLIC_LIST_AMT_3 ");
                }
                if (this.wherePUBLIC_LIST_AMT_4 > -1000000000000)
                {
                    sbstrSQL.Append(" and PUBLIC_LIST_AMT_4=@wherePUBLIC_LIST_AMT_4 ");
                }
                if (this.wherePUBLIC_LIST_AMT_5 > -1000000000000)
                {
                    sbstrSQL.Append(" and PUBLIC_LIST_AMT_5=@wherePUBLIC_LIST_AMT_5 ");
                }
                if (this.wherePUBLIC_LIST_DT_1 > dateStart)
                {
                    sbstrSQL.Append(" and PUBLIC_LIST_DT_1=@wherePUBLIC_LIST_DT_1 ");
                }
                if (this.wherePUBLIC_LIST_DT_2 > dateStart)
                {
                    sbstrSQL.Append(" and PUBLIC_LIST_DT_2=@wherePUBLIC_LIST_DT_2 ");
                }
                if (this.wherePUBLIC_LIST_DT_3 > dateStart)
                {
                    sbstrSQL.Append(" and PUBLIC_LIST_DT_3=@wherePUBLIC_LIST_DT_3 ");
                }
                if (this.wherePUBLIC_LIST_DT_4 > dateStart)
                {
                    sbstrSQL.Append(" and PUBLIC_LIST_DT_4=@wherePUBLIC_LIST_DT_4 ");
                }
                if (this.wherePUBLIC_LIST_DT_5 > dateStart)
                {
                    sbstrSQL.Append(" and PUBLIC_LIST_DT_5=@wherePUBLIC_LIST_DT_5 ");
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
                if (sbstrSQL.ToString().Contains("@PROCESS_DTE "))
                {
                    this.UpdateOperator.SetValue("@PROCESS_DTE", this.PROCESS_DTE, SqlDbType.VarChar);
                }
                if (sbstrSQL.ToString().Contains("@RETURN_DTE "))
                {
                    this.UpdateOperator.SetValue("@RETURN_DTE", this.RETURN_DTE, SqlDbType.VarChar);
                }
                if (sbstrSQL.ToString().Contains("@PAY_TYPE "))
                {
                    this.UpdateOperator.SetValue("@PAY_TYPE", this.PAY_TYPE, SqlDbType.VarChar);
                }
                if (sbstrSQL.ToString().Contains("@PAY_SEQ "))
                {
                    this.UpdateOperator.SetValue("@PAY_SEQ", this.PAY_SEQ, SqlDbType.VarChar);
                }
                if (sbstrSQL.ToString().Contains("@PAY_DATA_AREA "))
                {
                    this.UpdateOperator.SetValue("@PAY_DATA_AREA", this.PAY_DATA_AREA, SqlDbType.VarChar);
                }
                if (sbstrSQL.ToString().Contains("@FILE_TRANSFER_TYPE "))
                {
                    this.UpdateOperator.SetValue("@FILE_TRANSFER_TYPE", this.FILE_TRANSFER_TYPE, SqlDbType.VarChar);
                }
                if (sbstrSQL.ToString().Contains("@PUBLIC_LIST_FIELD_1 "))
                {
                    this.UpdateOperator.SetValue("@PUBLIC_LIST_FIELD_1", this.PUBLIC_LIST_FIELD_1, SqlDbType.VarChar);
                }
                if (sbstrSQL.ToString().Contains("@PUBLIC_LIST_FIELD_2 "))
                {
                    this.UpdateOperator.SetValue("@PUBLIC_LIST_FIELD_2", this.PUBLIC_LIST_FIELD_2, SqlDbType.VarChar);
                }
                if (sbstrSQL.ToString().Contains("@PUBLIC_LIST_FIELD_3 "))
                {
                    this.UpdateOperator.SetValue("@PUBLIC_LIST_FIELD_3", this.PUBLIC_LIST_FIELD_3, SqlDbType.VarChar);
                }
                if (sbstrSQL.ToString().Contains("@PUBLIC_LIST_FIELD_4 "))
                {
                    this.UpdateOperator.SetValue("@PUBLIC_LIST_FIELD_4", this.PUBLIC_LIST_FIELD_4, SqlDbType.VarChar);
                }
                if (sbstrSQL.ToString().Contains("@PUBLIC_LIST_FIELD_5 "))
                {
                    this.UpdateOperator.SetValue("@PUBLIC_LIST_FIELD_5", this.PUBLIC_LIST_FIELD_5, SqlDbType.VarChar);
                }
                if (sbstrSQL.ToString().Contains("@PUBLIC_LIST_AMT_1 "))
                {
                    this.UpdateOperator.SetValue("@PUBLIC_LIST_AMT_1", this.PUBLIC_LIST_AMT_1, SqlDbType.Decimal);
                }
                if (sbstrSQL.ToString().Contains("@PUBLIC_LIST_AMT_2 "))
                {
                    this.UpdateOperator.SetValue("@PUBLIC_LIST_AMT_2", this.PUBLIC_LIST_AMT_2, SqlDbType.Decimal);
                }
                if (sbstrSQL.ToString().Contains("@PUBLIC_LIST_AMT_3 "))
                {
                    this.UpdateOperator.SetValue("@PUBLIC_LIST_AMT_3", this.PUBLIC_LIST_AMT_3, SqlDbType.Decimal);
                }
                if (sbstrSQL.ToString().Contains("@PUBLIC_LIST_AMT_4 "))
                {
                    this.UpdateOperator.SetValue("@PUBLIC_LIST_AMT_4", this.PUBLIC_LIST_AMT_4, SqlDbType.Decimal);
                }
                if (sbstrSQL.ToString().Contains("@PUBLIC_LIST_AMT_5 "))
                {
                    this.UpdateOperator.SetValue("@PUBLIC_LIST_AMT_5", this.PUBLIC_LIST_AMT_5, SqlDbType.Decimal);
                }
                if (sbstrSQL.ToString().Contains("@PUBLIC_LIST_DT_1 "))
                {
                    this.UpdateOperator.SetValue("@PUBLIC_LIST_DT_1", this.PUBLIC_LIST_DT_1, SqlDbType.DateTime);
                }
                if (sbstrSQL.ToString().Contains("@PUBLIC_LIST_DT_2 "))
                {
                    this.UpdateOperator.SetValue("@PUBLIC_LIST_DT_2", this.PUBLIC_LIST_DT_2, SqlDbType.DateTime);
                }
                if (sbstrSQL.ToString().Contains("@PUBLIC_LIST_DT_3 "))
                {
                    this.UpdateOperator.SetValue("@PUBLIC_LIST_DT_3", this.PUBLIC_LIST_DT_3, SqlDbType.DateTime);
                }
                if (sbstrSQL.ToString().Contains("@PUBLIC_LIST_DT_4 "))
                {
                    this.UpdateOperator.SetValue("@PUBLIC_LIST_DT_4", this.PUBLIC_LIST_DT_4, SqlDbType.DateTime);
                }
                if (sbstrSQL.ToString().Contains("@PUBLIC_LIST_DT_5 "))
                {
                    this.UpdateOperator.SetValue("@PUBLIC_LIST_DT_5", this.PUBLIC_LIST_DT_5, SqlDbType.DateTime);
                }
                if (sbstrSQL.ToString().Contains("@MNT_DT "))
                {
                    this.UpdateOperator.SetValue("@MNT_DT", this.MNT_DT, SqlDbType.DateTime);
                }
                if (sbstrSQL.ToString().Contains("@MNT_USER "))
                {
                    this.UpdateOperator.SetValue("@MNT_USER", this.MNT_USER, SqlDbType.VarChar);
                }
                if (sbstrSQL.ToString().Contains("@wherePROCESS_DTE "))
                {
                    this.UpdateOperator.SetValue("@wherePROCESS_DTE", this.wherePROCESS_DTE, SqlDbType.VarChar);
                }
                if (sbstrSQL.ToString().Contains("@whereRETURN_DTE "))
                {
                    this.UpdateOperator.SetValue("@whereRETURN_DTE", this.whereRETURN_DTE, SqlDbType.VarChar);
                }
                if (sbstrSQL.ToString().Contains("@wherePAY_TYPE "))
                {
                    this.UpdateOperator.SetValue("@wherePAY_TYPE", this.wherePAY_TYPE, SqlDbType.VarChar);
                }
                if (sbstrSQL.ToString().Contains("@wherePAY_SEQ "))
                {
                    this.UpdateOperator.SetValue("@wherePAY_SEQ", this.wherePAY_SEQ, SqlDbType.VarChar);
                }
                if (sbstrSQL.ToString().Contains("@wherePAY_DATA_AREA "))
                {
                    this.UpdateOperator.SetValue("@wherePAY_DATA_AREA", this.wherePAY_DATA_AREA, SqlDbType.VarChar);
                }
                if (sbstrSQL.ToString().Contains("@whereFILE_TRANSFER_TYPE "))
                {
                    this.UpdateOperator.SetValue("@whereFILE_TRANSFER_TYPE", this.whereFILE_TRANSFER_TYPE, SqlDbType.VarChar);
                }
                if (sbstrSQL.ToString().Contains("@wherePUBLIC_LIST_FIELD_1 "))
                {
                    this.UpdateOperator.SetValue("@wherePUBLIC_LIST_FIELD_1", this.wherePUBLIC_LIST_FIELD_1, SqlDbType.VarChar);
                }
                if (sbstrSQL.ToString().Contains("@wherePUBLIC_LIST_FIELD_2 "))
                {
                    this.UpdateOperator.SetValue("@wherePUBLIC_LIST_FIELD_2", this.wherePUBLIC_LIST_FIELD_2, SqlDbType.VarChar);
                }
                if (sbstrSQL.ToString().Contains("@wherePUBLIC_LIST_FIELD_3 "))
                {
                    this.UpdateOperator.SetValue("@wherePUBLIC_LIST_FIELD_3", this.wherePUBLIC_LIST_FIELD_3, SqlDbType.VarChar);
                }
                if (sbstrSQL.ToString().Contains("@wherePUBLIC_LIST_FIELD_4 "))
                {
                    this.UpdateOperator.SetValue("@wherePUBLIC_LIST_FIELD_4", this.wherePUBLIC_LIST_FIELD_4, SqlDbType.VarChar);
                }
                if (sbstrSQL.ToString().Contains("@wherePUBLIC_LIST_FIELD_5 "))
                {
                    this.UpdateOperator.SetValue("@wherePUBLIC_LIST_FIELD_5", this.wherePUBLIC_LIST_FIELD_5, SqlDbType.VarChar);
                }
                if (sbstrSQL.ToString().Contains("@wherePUBLIC_LIST_AMT_1 "))
                {
                    this.UpdateOperator.SetValue("@wherePUBLIC_LIST_AMT_1", this.wherePUBLIC_LIST_AMT_1, SqlDbType.Decimal);
                }
                if (sbstrSQL.ToString().Contains("@wherePUBLIC_LIST_AMT_2 "))
                {
                    this.UpdateOperator.SetValue("@wherePUBLIC_LIST_AMT_2", this.wherePUBLIC_LIST_AMT_2, SqlDbType.Decimal);
                }
                if (sbstrSQL.ToString().Contains("@wherePUBLIC_LIST_AMT_3 "))
                {
                    this.UpdateOperator.SetValue("@wherePUBLIC_LIST_AMT_3", this.wherePUBLIC_LIST_AMT_3, SqlDbType.Decimal);
                }
                if (sbstrSQL.ToString().Contains("@wherePUBLIC_LIST_AMT_4 "))
                {
                    this.UpdateOperator.SetValue("@wherePUBLIC_LIST_AMT_4", this.wherePUBLIC_LIST_AMT_4, SqlDbType.Decimal);
                }
                if (sbstrSQL.ToString().Contains("@wherePUBLIC_LIST_AMT_5 "))
                {
                    this.UpdateOperator.SetValue("@wherePUBLIC_LIST_AMT_5", this.wherePUBLIC_LIST_AMT_5, SqlDbType.Decimal);
                }
                if (sbstrSQL.ToString().Contains("@wherePUBLIC_LIST_DT_1 "))
                {
                    this.UpdateOperator.SetValue("@wherePUBLIC_LIST_DT_1", this.wherePUBLIC_LIST_DT_1, SqlDbType.DateTime);
                }
                if (sbstrSQL.ToString().Contains("@wherePUBLIC_LIST_DT_2 "))
                {
                    this.UpdateOperator.SetValue("@wherePUBLIC_LIST_DT_2", this.wherePUBLIC_LIST_DT_2, SqlDbType.DateTime);
                }
                if (sbstrSQL.ToString().Contains("@wherePUBLIC_LIST_DT_3 "))
                {
                    this.UpdateOperator.SetValue("@wherePUBLIC_LIST_DT_3", this.wherePUBLIC_LIST_DT_3, SqlDbType.DateTime);
                }
                if (sbstrSQL.ToString().Contains("@wherePUBLIC_LIST_DT_4 "))
                {
                    this.UpdateOperator.SetValue("@wherePUBLIC_LIST_DT_4", this.wherePUBLIC_LIST_DT_4, SqlDbType.DateTime);
                }
                if (sbstrSQL.ToString().Contains("@wherePUBLIC_LIST_DT_5 "))
                {
                    this.UpdateOperator.SetValue("@wherePUBLIC_LIST_DT_5", this.wherePUBLIC_LIST_DT_5, SqlDbType.DateTime);
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
            /// <date>2014/12/24 上午 11:10:48</date>
            #endregion
            try
            {
                #region set SQL statement
                StringBuilder sbstrSQL = new StringBuilder();
                sbstrSQL.Append(" DELETE PUBLIC_LIST where 1=1 ");
                if (this.wherePROCESS_DTE != null)
                {
                    sbstrSQL.Append(" and PROCESS_DTE=@wherePROCESS_DTE ");
                }
                if (this.whereRETURN_DTE != null)
                {
                    sbstrSQL.Append(" and RETURN_DTE=@whereRETURN_DTE ");
                }
                if (this.wherePAY_TYPE != null)
                {
                    sbstrSQL.Append(" and PAY_TYPE=@wherePAY_TYPE ");
                }
                if (this.wherePAY_SEQ != null)
                {
                    sbstrSQL.Append(" and PAY_SEQ=@wherePAY_SEQ ");
                }
                if (this.wherePAY_DATA_AREA != null)
                {
                    sbstrSQL.Append(" and PAY_DATA_AREA=@wherePAY_DATA_AREA ");
                }
                if (this.whereFILE_TRANSFER_TYPE != null)
                {
                    sbstrSQL.Append(" and FILE_TRANSFER_TYPE=@whereFILE_TRANSFER_TYPE ");
                }
                if (this.wherePUBLIC_LIST_FIELD_1 != null)
                {
                    sbstrSQL.Append(" and PUBLIC_LIST_FIELD_1=@wherePUBLIC_LIST_FIELD_1 ");
                }
                if (this.wherePUBLIC_LIST_FIELD_2 != null)
                {
                    sbstrSQL.Append(" and PUBLIC_LIST_FIELD_2=@wherePUBLIC_LIST_FIELD_2 ");
                }
                if (this.wherePUBLIC_LIST_FIELD_3 != null)
                {
                    sbstrSQL.Append(" and PUBLIC_LIST_FIELD_3=@wherePUBLIC_LIST_FIELD_3 ");
                }
                if (this.wherePUBLIC_LIST_FIELD_4 != null)
                {
                    sbstrSQL.Append(" and PUBLIC_LIST_FIELD_4=@wherePUBLIC_LIST_FIELD_4 ");
                }
                if (this.wherePUBLIC_LIST_FIELD_5 != null)
                {
                    sbstrSQL.Append(" and PUBLIC_LIST_FIELD_5=@wherePUBLIC_LIST_FIELD_5 ");
                }
                if (this.wherePUBLIC_LIST_AMT_1 > -1000000000000)
                {
                    sbstrSQL.Append(" and PUBLIC_LIST_AMT_1=@wherePUBLIC_LIST_AMT_1 ");
                }
                if (this.wherePUBLIC_LIST_AMT_2 > -1000000000000)
                {
                    sbstrSQL.Append(" and PUBLIC_LIST_AMT_2=@wherePUBLIC_LIST_AMT_2 ");
                }
                if (this.wherePUBLIC_LIST_AMT_3 > -1000000000000)
                {
                    sbstrSQL.Append(" and PUBLIC_LIST_AMT_3=@wherePUBLIC_LIST_AMT_3 ");
                }
                if (this.wherePUBLIC_LIST_AMT_4 > -1000000000000)
                {
                    sbstrSQL.Append(" and PUBLIC_LIST_AMT_4=@wherePUBLIC_LIST_AMT_4 ");
                }
                if (this.wherePUBLIC_LIST_AMT_5 > -1000000000000)
                {
                    sbstrSQL.Append(" and PUBLIC_LIST_AMT_5=@wherePUBLIC_LIST_AMT_5 ");
                }
                if (this.wherePUBLIC_LIST_DT_1 > dateStart)
                {
                    sbstrSQL.Append(" and PUBLIC_LIST_DT_1=@wherePUBLIC_LIST_DT_1 ");
                }
                if (this.wherePUBLIC_LIST_DT_2 > dateStart)
                {
                    sbstrSQL.Append(" and PUBLIC_LIST_DT_2=@wherePUBLIC_LIST_DT_2 ");
                }
                if (this.wherePUBLIC_LIST_DT_3 > dateStart)
                {
                    sbstrSQL.Append(" and PUBLIC_LIST_DT_3=@wherePUBLIC_LIST_DT_3 ");
                }
                if (this.wherePUBLIC_LIST_DT_4 > dateStart)
                {
                    sbstrSQL.Append(" and PUBLIC_LIST_DT_4=@wherePUBLIC_LIST_DT_4 ");
                }
                if (this.wherePUBLIC_LIST_DT_5 > dateStart)
                {
                    sbstrSQL.Append(" and PUBLIC_LIST_DT_5=@wherePUBLIC_LIST_DT_5 ");
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
                if (sbstrSQL.ToString().Contains("@wherePROCESS_DTE "))
                {
                    this.DeleteOperator.SetValue("@wherePROCESS_DTE", this.wherePROCESS_DTE, SqlDbType.VarChar);
                }
                if (sbstrSQL.ToString().Contains("@whereRETURN_DTE "))
                {
                    this.DeleteOperator.SetValue("@whereRETURN_DTE", this.whereRETURN_DTE, SqlDbType.VarChar);
                }
                if (sbstrSQL.ToString().Contains("@wherePAY_TYPE "))
                {
                    this.DeleteOperator.SetValue("@wherePAY_TYPE", this.wherePAY_TYPE, SqlDbType.VarChar);
                }
                if (sbstrSQL.ToString().Contains("@wherePAY_SEQ "))
                {
                    this.DeleteOperator.SetValue("@wherePAY_SEQ", this.wherePAY_SEQ, SqlDbType.VarChar);
                }
                if (sbstrSQL.ToString().Contains("@wherePAY_DATA_AREA "))
                {
                    this.DeleteOperator.SetValue("@wherePAY_DATA_AREA", this.wherePAY_DATA_AREA, SqlDbType.VarChar);
                }
                if (sbstrSQL.ToString().Contains("@whereFILE_TRANSFER_TYPE "))
                {
                    this.DeleteOperator.SetValue("@whereFILE_TRANSFER_TYPE", this.whereFILE_TRANSFER_TYPE, SqlDbType.VarChar);
                }
                if (sbstrSQL.ToString().Contains("@wherePUBLIC_LIST_FIELD_1 "))
                {
                    this.DeleteOperator.SetValue("@wherePUBLIC_LIST_FIELD_1", this.wherePUBLIC_LIST_FIELD_1, SqlDbType.VarChar);
                }
                if (sbstrSQL.ToString().Contains("@wherePUBLIC_LIST_FIELD_2 "))
                {
                    this.DeleteOperator.SetValue("@wherePUBLIC_LIST_FIELD_2", this.wherePUBLIC_LIST_FIELD_2, SqlDbType.VarChar);
                }
                if (sbstrSQL.ToString().Contains("@wherePUBLIC_LIST_FIELD_3 "))
                {
                    this.DeleteOperator.SetValue("@wherePUBLIC_LIST_FIELD_3", this.wherePUBLIC_LIST_FIELD_3, SqlDbType.VarChar);
                }
                if (sbstrSQL.ToString().Contains("@wherePUBLIC_LIST_FIELD_4 "))
                {
                    this.DeleteOperator.SetValue("@wherePUBLIC_LIST_FIELD_4", this.wherePUBLIC_LIST_FIELD_4, SqlDbType.VarChar);
                }
                if (sbstrSQL.ToString().Contains("@wherePUBLIC_LIST_FIELD_5 "))
                {
                    this.DeleteOperator.SetValue("@wherePUBLIC_LIST_FIELD_5", this.wherePUBLIC_LIST_FIELD_5, SqlDbType.VarChar);
                }
                if (sbstrSQL.ToString().Contains("@wherePUBLIC_LIST_AMT_1 "))
                {
                    this.DeleteOperator.SetValue("@wherePUBLIC_LIST_AMT_1", this.wherePUBLIC_LIST_AMT_1, SqlDbType.Decimal);
                }
                if (sbstrSQL.ToString().Contains("@wherePUBLIC_LIST_AMT_2 "))
                {
                    this.DeleteOperator.SetValue("@wherePUBLIC_LIST_AMT_2", this.wherePUBLIC_LIST_AMT_2, SqlDbType.Decimal);
                }
                if (sbstrSQL.ToString().Contains("@wherePUBLIC_LIST_AMT_3 "))
                {
                    this.DeleteOperator.SetValue("@wherePUBLIC_LIST_AMT_3", this.wherePUBLIC_LIST_AMT_3, SqlDbType.Decimal);
                }
                if (sbstrSQL.ToString().Contains("@wherePUBLIC_LIST_AMT_4 "))
                {
                    this.DeleteOperator.SetValue("@wherePUBLIC_LIST_AMT_4", this.wherePUBLIC_LIST_AMT_4, SqlDbType.Decimal);
                }
                if (sbstrSQL.ToString().Contains("@wherePUBLIC_LIST_AMT_5 "))
                {
                    this.DeleteOperator.SetValue("@wherePUBLIC_LIST_AMT_5", this.wherePUBLIC_LIST_AMT_5, SqlDbType.Decimal);
                }
                if (sbstrSQL.ToString().Contains("@wherePUBLIC_LIST_DT_1 "))
                {
                    this.DeleteOperator.SetValue("@wherePUBLIC_LIST_DT_1", this.wherePUBLIC_LIST_DT_1, SqlDbType.DateTime);
                }
                if (sbstrSQL.ToString().Contains("@wherePUBLIC_LIST_DT_2 "))
                {
                    this.DeleteOperator.SetValue("@wherePUBLIC_LIST_DT_2", this.wherePUBLIC_LIST_DT_2, SqlDbType.DateTime);
                }
                if (sbstrSQL.ToString().Contains("@wherePUBLIC_LIST_DT_3 "))
                {
                    this.DeleteOperator.SetValue("@wherePUBLIC_LIST_DT_3", this.wherePUBLIC_LIST_DT_3, SqlDbType.DateTime);
                }
                if (sbstrSQL.ToString().Contains("@wherePUBLIC_LIST_DT_4 "))
                {
                    this.DeleteOperator.SetValue("@wherePUBLIC_LIST_DT_4", this.wherePUBLIC_LIST_DT_4, SqlDbType.DateTime);
                }
                if (sbstrSQL.ToString().Contains("@wherePUBLIC_LIST_DT_5 "))
                {
                    this.DeleteOperator.SetValue("@wherePUBLIC_LIST_DT_5", this.wherePUBLIC_LIST_DT_5, SqlDbType.DateTime);
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
            /// <date>2014/12/24 上午 11:10:48</date>
            #endregion
            try
            {
                #region 宣告MNT_TODAYDAO並放入初始值
                Cybersoft.Dao.Core.MNT_TODAYDao MNT_TODAY = new Cybersoft.Dao.Core.MNT_TODAYDao();
                MNT_TODAY.table_define();
                MNT_TODAY.strinitTBL_NAME = "PUBLIC_LIST";
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
                if (this.PROCESS_DTE != null && this.PROCESS_DTE != Convert.ToString(myTable.Rows[0]["PROCESS_DTE"]))
                {
                    MNT_TODAY.initInsert_row();
                    MNT_TODAY.resultTable.Rows[MNT_Count]["FIELD_NAME"] = "PROCESS_DTE";
                    MNT_TODAY.resultTable.Rows[MNT_Count]["VARCHAR_BEFORE"] = Convert.ToString(myTable.Rows[0]["PROCESS_DTE"]);
                    MNT_TODAY.resultTable.Rows[MNT_Count]["VARCHAR_AFTER"] = this.PROCESS_DTE;
                    MNT_Count++;
                }
                if (this.RETURN_DTE != null && this.RETURN_DTE != Convert.ToString(myTable.Rows[0]["RETURN_DTE"]))
                {
                    MNT_TODAY.initInsert_row();
                    MNT_TODAY.resultTable.Rows[MNT_Count]["FIELD_NAME"] = "RETURN_DTE";
                    MNT_TODAY.resultTable.Rows[MNT_Count]["VARCHAR_BEFORE"] = Convert.ToString(myTable.Rows[0]["RETURN_DTE"]);
                    MNT_TODAY.resultTable.Rows[MNT_Count]["VARCHAR_AFTER"] = this.RETURN_DTE;
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
                if (this.PAY_SEQ != null && this.PAY_SEQ != Convert.ToString(myTable.Rows[0]["PAY_SEQ"]))
                {
                    MNT_TODAY.initInsert_row();
                    MNT_TODAY.resultTable.Rows[MNT_Count]["FIELD_NAME"] = "PAY_SEQ";
                    MNT_TODAY.resultTable.Rows[MNT_Count]["VARCHAR_BEFORE"] = Convert.ToString(myTable.Rows[0]["PAY_SEQ"]);
                    MNT_TODAY.resultTable.Rows[MNT_Count]["VARCHAR_AFTER"] = this.PAY_SEQ;
                    MNT_Count++;
                }
                if (this.PAY_DATA_AREA != null && this.PAY_DATA_AREA != Convert.ToString(myTable.Rows[0]["PAY_DATA_AREA"]))
                {
                    MNT_TODAY.initInsert_row();
                    MNT_TODAY.resultTable.Rows[MNT_Count]["FIELD_NAME"] = "PAY_DATA_AREA";
                    MNT_TODAY.resultTable.Rows[MNT_Count]["VARCHAR_BEFORE"] = Convert.ToString(myTable.Rows[0]["PAY_DATA_AREA"]);
                    MNT_TODAY.resultTable.Rows[MNT_Count]["VARCHAR_AFTER"] = this.PAY_DATA_AREA;
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
                if (this.PUBLIC_LIST_FIELD_1 != null && this.PUBLIC_LIST_FIELD_1 != Convert.ToString(myTable.Rows[0]["PUBLIC_LIST_FIELD_1"]))
                {
                    MNT_TODAY.initInsert_row();
                    MNT_TODAY.resultTable.Rows[MNT_Count]["FIELD_NAME"] = "PUBLIC_LIST_FIELD_1";
                    MNT_TODAY.resultTable.Rows[MNT_Count]["VARCHAR_BEFORE"] = Convert.ToString(myTable.Rows[0]["PUBLIC_LIST_FIELD_1"]);
                    MNT_TODAY.resultTable.Rows[MNT_Count]["VARCHAR_AFTER"] = this.PUBLIC_LIST_FIELD_1;
                    MNT_Count++;
                }
                if (this.PUBLIC_LIST_FIELD_2 != null && this.PUBLIC_LIST_FIELD_2 != Convert.ToString(myTable.Rows[0]["PUBLIC_LIST_FIELD_2"]))
                {
                    MNT_TODAY.initInsert_row();
                    MNT_TODAY.resultTable.Rows[MNT_Count]["FIELD_NAME"] = "PUBLIC_LIST_FIELD_2";
                    MNT_TODAY.resultTable.Rows[MNT_Count]["VARCHAR_BEFORE"] = Convert.ToString(myTable.Rows[0]["PUBLIC_LIST_FIELD_2"]);
                    MNT_TODAY.resultTable.Rows[MNT_Count]["VARCHAR_AFTER"] = this.PUBLIC_LIST_FIELD_2;
                    MNT_Count++;
                }
                if (this.PUBLIC_LIST_FIELD_3 != null && this.PUBLIC_LIST_FIELD_3 != Convert.ToString(myTable.Rows[0]["PUBLIC_LIST_FIELD_3"]))
                {
                    MNT_TODAY.initInsert_row();
                    MNT_TODAY.resultTable.Rows[MNT_Count]["FIELD_NAME"] = "PUBLIC_LIST_FIELD_3";
                    MNT_TODAY.resultTable.Rows[MNT_Count]["VARCHAR_BEFORE"] = Convert.ToString(myTable.Rows[0]["PUBLIC_LIST_FIELD_3"]);
                    MNT_TODAY.resultTable.Rows[MNT_Count]["VARCHAR_AFTER"] = this.PUBLIC_LIST_FIELD_3;
                    MNT_Count++;
                }
                if (this.PUBLIC_LIST_FIELD_4 != null && this.PUBLIC_LIST_FIELD_4 != Convert.ToString(myTable.Rows[0]["PUBLIC_LIST_FIELD_4"]))
                {
                    MNT_TODAY.initInsert_row();
                    MNT_TODAY.resultTable.Rows[MNT_Count]["FIELD_NAME"] = "PUBLIC_LIST_FIELD_4";
                    MNT_TODAY.resultTable.Rows[MNT_Count]["VARCHAR_BEFORE"] = Convert.ToString(myTable.Rows[0]["PUBLIC_LIST_FIELD_4"]);
                    MNT_TODAY.resultTable.Rows[MNT_Count]["VARCHAR_AFTER"] = this.PUBLIC_LIST_FIELD_4;
                    MNT_Count++;
                }
                if (this.PUBLIC_LIST_FIELD_5 != null && this.PUBLIC_LIST_FIELD_5 != Convert.ToString(myTable.Rows[0]["PUBLIC_LIST_FIELD_5"]))
                {
                    MNT_TODAY.initInsert_row();
                    MNT_TODAY.resultTable.Rows[MNT_Count]["FIELD_NAME"] = "PUBLIC_LIST_FIELD_5";
                    MNT_TODAY.resultTable.Rows[MNT_Count]["VARCHAR_BEFORE"] = Convert.ToString(myTable.Rows[0]["PUBLIC_LIST_FIELD_5"]);
                    MNT_TODAY.resultTable.Rows[MNT_Count]["VARCHAR_AFTER"] = this.PUBLIC_LIST_FIELD_5;
                    MNT_Count++;
                }
                if (this.PUBLIC_LIST_AMT_1 > -1000000000000 && this.PUBLIC_LIST_AMT_1 != Convert.ToDecimal(myTable.Rows[0]["PUBLIC_LIST_AMT_1"]))
                {
                    MNT_TODAY.initInsert_row();
                    MNT_TODAY.resultTable.Rows[MNT_Count]["FIELD_NAME"] = "PUBLIC_LIST_AMT_1";
                    MNT_TODAY.resultTable.Rows[MNT_Count]["DECIMAL_BEFORE"] = Convert.ToDecimal(myTable.Rows[0]["PUBLIC_LIST_AMT_1"]);
                    MNT_TODAY.resultTable.Rows[MNT_Count]["DECIMAL_AFTER"] = this.PUBLIC_LIST_AMT_1;
                    MNT_Count++;
                }
                if (this.PUBLIC_LIST_AMT_2 > -1000000000000 && this.PUBLIC_LIST_AMT_2 != Convert.ToDecimal(myTable.Rows[0]["PUBLIC_LIST_AMT_2"]))
                {
                    MNT_TODAY.initInsert_row();
                    MNT_TODAY.resultTable.Rows[MNT_Count]["FIELD_NAME"] = "PUBLIC_LIST_AMT_2";
                    MNT_TODAY.resultTable.Rows[MNT_Count]["DECIMAL_BEFORE"] = Convert.ToDecimal(myTable.Rows[0]["PUBLIC_LIST_AMT_2"]);
                    MNT_TODAY.resultTable.Rows[MNT_Count]["DECIMAL_AFTER"] = this.PUBLIC_LIST_AMT_2;
                    MNT_Count++;
                }
                if (this.PUBLIC_LIST_AMT_3 > -1000000000000 && this.PUBLIC_LIST_AMT_3 != Convert.ToDecimal(myTable.Rows[0]["PUBLIC_LIST_AMT_3"]))
                {
                    MNT_TODAY.initInsert_row();
                    MNT_TODAY.resultTable.Rows[MNT_Count]["FIELD_NAME"] = "PUBLIC_LIST_AMT_3";
                    MNT_TODAY.resultTable.Rows[MNT_Count]["DECIMAL_BEFORE"] = Convert.ToDecimal(myTable.Rows[0]["PUBLIC_LIST_AMT_3"]);
                    MNT_TODAY.resultTable.Rows[MNT_Count]["DECIMAL_AFTER"] = this.PUBLIC_LIST_AMT_3;
                    MNT_Count++;
                }
                if (this.PUBLIC_LIST_AMT_4 > -1000000000000 && this.PUBLIC_LIST_AMT_4 != Convert.ToDecimal(myTable.Rows[0]["PUBLIC_LIST_AMT_4"]))
                {
                    MNT_TODAY.initInsert_row();
                    MNT_TODAY.resultTable.Rows[MNT_Count]["FIELD_NAME"] = "PUBLIC_LIST_AMT_4";
                    MNT_TODAY.resultTable.Rows[MNT_Count]["DECIMAL_BEFORE"] = Convert.ToDecimal(myTable.Rows[0]["PUBLIC_LIST_AMT_4"]);
                    MNT_TODAY.resultTable.Rows[MNT_Count]["DECIMAL_AFTER"] = this.PUBLIC_LIST_AMT_4;
                    MNT_Count++;
                }
                if (this.PUBLIC_LIST_AMT_5 > -1000000000000 && this.PUBLIC_LIST_AMT_5 != Convert.ToDecimal(myTable.Rows[0]["PUBLIC_LIST_AMT_5"]))
                {
                    MNT_TODAY.initInsert_row();
                    MNT_TODAY.resultTable.Rows[MNT_Count]["FIELD_NAME"] = "PUBLIC_LIST_AMT_5";
                    MNT_TODAY.resultTable.Rows[MNT_Count]["DECIMAL_BEFORE"] = Convert.ToDecimal(myTable.Rows[0]["PUBLIC_LIST_AMT_5"]);
                    MNT_TODAY.resultTable.Rows[MNT_Count]["DECIMAL_AFTER"] = this.PUBLIC_LIST_AMT_5;
                    MNT_Count++;
                }
                if (this.PUBLIC_LIST_DT_1 > dateStart && this.PUBLIC_LIST_DT_1 != Convert.ToDateTime(myTable.Rows[0]["PUBLIC_LIST_DT_1"]))
                {
                    if (this.PUBLIC_LIST_DT_1.ToString("yyyyMMdd") == "19000101" &&
                        Convert.ToDateTime(myTable.Rows[0]["PUBLIC_LIST_DT_1"]) == dateStart)
                    {
                    }
                    else
                    {
                        MNT_TODAY.initInsert_row();
                        MNT_TODAY.resultTable.Rows[MNT_Count]["FIELD_NAME"] = "PUBLIC_LIST_DT_1";
                        MNT_TODAY.resultTable.Rows[MNT_Count]["DT_BEFORE"] = Convert.ToDateTime(myTable.Rows[0]["PUBLIC_LIST_DT_1"]);
                        if (this.PUBLIC_LIST_DT_1.ToString("yyyyMMdd") == "19000101")
                        {
                            MNT_TODAY.resultTable.Rows[MNT_Count]["DT_AFTER"] = "1900-01-01";
                        }
                        else
                        {
                            MNT_TODAY.resultTable.Rows[MNT_Count]["DT_AFTER"] = this.PUBLIC_LIST_DT_1;
                        }
                        MNT_Count++;
                    }
                }
                if (this.PUBLIC_LIST_DT_2 > dateStart && this.PUBLIC_LIST_DT_2 != Convert.ToDateTime(myTable.Rows[0]["PUBLIC_LIST_DT_2"]))
                {
                    if (this.PUBLIC_LIST_DT_2.ToString("yyyyMMdd") == "19000101" &&
                        Convert.ToDateTime(myTable.Rows[0]["PUBLIC_LIST_DT_2"]) == dateStart)
                    {
                    }
                    else
                    {
                        MNT_TODAY.initInsert_row();
                        MNT_TODAY.resultTable.Rows[MNT_Count]["FIELD_NAME"] = "PUBLIC_LIST_DT_2";
                        MNT_TODAY.resultTable.Rows[MNT_Count]["DT_BEFORE"] = Convert.ToDateTime(myTable.Rows[0]["PUBLIC_LIST_DT_2"]);
                        if (this.PUBLIC_LIST_DT_2.ToString("yyyyMMdd") == "19000101")
                        {
                            MNT_TODAY.resultTable.Rows[MNT_Count]["DT_AFTER"] = "1900-01-01";
                        }
                        else
                        {
                            MNT_TODAY.resultTable.Rows[MNT_Count]["DT_AFTER"] = this.PUBLIC_LIST_DT_2;
                        }
                        MNT_Count++;
                    }
                }
                if (this.PUBLIC_LIST_DT_3 > dateStart && this.PUBLIC_LIST_DT_3 != Convert.ToDateTime(myTable.Rows[0]["PUBLIC_LIST_DT_3"]))
                {
                    if (this.PUBLIC_LIST_DT_3.ToString("yyyyMMdd") == "19000101" &&
                        Convert.ToDateTime(myTable.Rows[0]["PUBLIC_LIST_DT_3"]) == dateStart)
                    {
                    }
                    else
                    {
                        MNT_TODAY.initInsert_row();
                        MNT_TODAY.resultTable.Rows[MNT_Count]["FIELD_NAME"] = "PUBLIC_LIST_DT_3";
                        MNT_TODAY.resultTable.Rows[MNT_Count]["DT_BEFORE"] = Convert.ToDateTime(myTable.Rows[0]["PUBLIC_LIST_DT_3"]);
                        if (this.PUBLIC_LIST_DT_3.ToString("yyyyMMdd") == "19000101")
                        {
                            MNT_TODAY.resultTable.Rows[MNT_Count]["DT_AFTER"] = "1900-01-01";
                        }
                        else
                        {
                            MNT_TODAY.resultTable.Rows[MNT_Count]["DT_AFTER"] = this.PUBLIC_LIST_DT_3;
                        }
                        MNT_Count++;
                    }
                }
                if (this.PUBLIC_LIST_DT_4 > dateStart && this.PUBLIC_LIST_DT_4 != Convert.ToDateTime(myTable.Rows[0]["PUBLIC_LIST_DT_4"]))
                {
                    if (this.PUBLIC_LIST_DT_4.ToString("yyyyMMdd") == "19000101" &&
                        Convert.ToDateTime(myTable.Rows[0]["PUBLIC_LIST_DT_4"]) == dateStart)
                    {
                    }
                    else
                    {
                        MNT_TODAY.initInsert_row();
                        MNT_TODAY.resultTable.Rows[MNT_Count]["FIELD_NAME"] = "PUBLIC_LIST_DT_4";
                        MNT_TODAY.resultTable.Rows[MNT_Count]["DT_BEFORE"] = Convert.ToDateTime(myTable.Rows[0]["PUBLIC_LIST_DT_4"]);
                        if (this.PUBLIC_LIST_DT_4.ToString("yyyyMMdd") == "19000101")
                        {
                            MNT_TODAY.resultTable.Rows[MNT_Count]["DT_AFTER"] = "1900-01-01";
                        }
                        else
                        {
                            MNT_TODAY.resultTable.Rows[MNT_Count]["DT_AFTER"] = this.PUBLIC_LIST_DT_4;
                        }
                        MNT_Count++;
                    }
                }
                if (this.PUBLIC_LIST_DT_5 > dateStart && this.PUBLIC_LIST_DT_5 != Convert.ToDateTime(myTable.Rows[0]["PUBLIC_LIST_DT_5"]))
                {
                    if (this.PUBLIC_LIST_DT_5.ToString("yyyyMMdd") == "19000101" &&
                        Convert.ToDateTime(myTable.Rows[0]["PUBLIC_LIST_DT_5"]) == dateStart)
                    {
                    }
                    else
                    {
                        MNT_TODAY.initInsert_row();
                        MNT_TODAY.resultTable.Rows[MNT_Count]["FIELD_NAME"] = "PUBLIC_LIST_DT_5";
                        MNT_TODAY.resultTable.Rows[MNT_Count]["DT_BEFORE"] = Convert.ToDateTime(myTable.Rows[0]["PUBLIC_LIST_DT_5"]);
                        if (this.PUBLIC_LIST_DT_5.ToString("yyyyMMdd") == "19000101")
                        {
                            MNT_TODAY.resultTable.Rows[MNT_Count]["DT_AFTER"] = "1900-01-01";
                        }
                        else
                        {
                            MNT_TODAY.resultTable.Rows[MNT_Count]["DT_AFTER"] = this.PUBLIC_LIST_DT_5;
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
            /// <date>2014/12/24 上午 11:10:48</date>
            #endregion
            try
            {
                #region 宣告MNT_TODAYDAO並放入初始值
                Cybersoft.Dao.Core.MNT_TODAYDao MNT_TODAY = new Cybersoft.Dao.Core.MNT_TODAYDao();
                MNT_TODAY.table_define();
                MNT_TODAY.strinitTBL_NAME = "PUBLIC_LIST";
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
            /// <date>2014/12/24 上午 11:10:48</date>
            #endregion
            try
            {
                #region set SQL statement
                StringBuilder sbstrSQL = new StringBuilder();
                sbstrSQL.Append("SELECT ");
                #endregion
                #region GROUP BY
                if (this.groupPROCESS_DTE != null)
                {
                    sbstrSQL.Append("a.PROCESS_DTE");
                }
                else
                {
                    sbstrSQL.Append("''");
                }
                sbstrSQL.Append(" as PROCESS_DTE,");
                if (this.groupRETURN_DTE != null)
                {
                    sbstrSQL.Append("a.RETURN_DTE");
                }
                else
                {
                    sbstrSQL.Append("''");
                }
                sbstrSQL.Append(" as RETURN_DTE,");
                if (this.groupPAY_TYPE != null)
                {
                    sbstrSQL.Append("a.PAY_TYPE");
                }
                else
                {
                    sbstrSQL.Append("''");
                }
                sbstrSQL.Append(" as PAY_TYPE,");
                if (this.groupPAY_SEQ != null)
                {
                    sbstrSQL.Append("a.PAY_SEQ");
                }
                else
                {
                    sbstrSQL.Append("''");
                }
                sbstrSQL.Append(" as PAY_SEQ,");
                if (this.groupPAY_DATA_AREA != null)
                {
                    sbstrSQL.Append("a.PAY_DATA_AREA");
                }
                else
                {
                    sbstrSQL.Append("''");
                }
                sbstrSQL.Append(" as PAY_DATA_AREA,");
                if (this.groupFILE_TRANSFER_TYPE != null)
                {
                    sbstrSQL.Append("a.FILE_TRANSFER_TYPE");
                }
                else
                {
                    sbstrSQL.Append("''");
                }
                sbstrSQL.Append(" as FILE_TRANSFER_TYPE,");
                if (this.groupPUBLIC_LIST_FIELD_1 != null)
                {
                    sbstrSQL.Append("a.PUBLIC_LIST_FIELD_1");
                }
                else
                {
                    sbstrSQL.Append("''");
                }
                sbstrSQL.Append(" as PUBLIC_LIST_FIELD_1,");
                if (this.groupPUBLIC_LIST_FIELD_2 != null)
                {
                    sbstrSQL.Append("a.PUBLIC_LIST_FIELD_2");
                }
                else
                {
                    sbstrSQL.Append("''");
                }
                sbstrSQL.Append(" as PUBLIC_LIST_FIELD_2,");
                if (this.groupPUBLIC_LIST_FIELD_3 != null)
                {
                    sbstrSQL.Append("a.PUBLIC_LIST_FIELD_3");
                }
                else
                {
                    sbstrSQL.Append("''");
                }
                sbstrSQL.Append(" as PUBLIC_LIST_FIELD_3,");
                if (this.groupPUBLIC_LIST_FIELD_4 != null)
                {
                    sbstrSQL.Append("a.PUBLIC_LIST_FIELD_4");
                }
                else
                {
                    sbstrSQL.Append("''");
                }
                sbstrSQL.Append(" as PUBLIC_LIST_FIELD_4,");
                if (this.groupPUBLIC_LIST_FIELD_5 != null)
                {
                    sbstrSQL.Append("a.PUBLIC_LIST_FIELD_5");
                }
                else
                {
                    sbstrSQL.Append("''");
                }
                sbstrSQL.Append(" as PUBLIC_LIST_FIELD_5,");
                if (this.groupPUBLIC_LIST_AMT_1 != null)
                {
                    sbstrSQL.Append("a.PUBLIC_LIST_AMT_1");
                }
                else
                {
                    sbstrSQL.Append("''");
                }
                sbstrSQL.Append(" as PUBLIC_LIST_AMT_1,");
                if (this.groupPUBLIC_LIST_AMT_2 != null)
                {
                    sbstrSQL.Append("a.PUBLIC_LIST_AMT_2");
                }
                else
                {
                    sbstrSQL.Append("''");
                }
                sbstrSQL.Append(" as PUBLIC_LIST_AMT_2,");
                if (this.groupPUBLIC_LIST_AMT_3 != null)
                {
                    sbstrSQL.Append("a.PUBLIC_LIST_AMT_3");
                }
                else
                {
                    sbstrSQL.Append("''");
                }
                sbstrSQL.Append(" as PUBLIC_LIST_AMT_3,");
                if (this.groupPUBLIC_LIST_AMT_4 != null)
                {
                    sbstrSQL.Append("a.PUBLIC_LIST_AMT_4");
                }
                else
                {
                    sbstrSQL.Append("''");
                }
                sbstrSQL.Append(" as PUBLIC_LIST_AMT_4,");
                if (this.groupPUBLIC_LIST_AMT_5 != null)
                {
                    sbstrSQL.Append("a.PUBLIC_LIST_AMT_5");
                }
                else
                {
                    sbstrSQL.Append("''");
                }
                sbstrSQL.Append(" as PUBLIC_LIST_AMT_5,");
                if (this.groupPUBLIC_LIST_DT_1 != null)
                {
                    sbstrSQL.Append("a.PUBLIC_LIST_DT_1");
                }
                else
                {
                    sbstrSQL.Append("''");
                }
                sbstrSQL.Append(" as PUBLIC_LIST_DT_1,");
                if (this.groupPUBLIC_LIST_DT_2 != null)
                {
                    sbstrSQL.Append("a.PUBLIC_LIST_DT_2");
                }
                else
                {
                    sbstrSQL.Append("''");
                }
                sbstrSQL.Append(" as PUBLIC_LIST_DT_2,");
                if (this.groupPUBLIC_LIST_DT_3 != null)
                {
                    sbstrSQL.Append("a.PUBLIC_LIST_DT_3");
                }
                else
                {
                    sbstrSQL.Append("''");
                }
                sbstrSQL.Append(" as PUBLIC_LIST_DT_3,");
                if (this.groupPUBLIC_LIST_DT_4 != null)
                {
                    sbstrSQL.Append("a.PUBLIC_LIST_DT_4");
                }
                else
                {
                    sbstrSQL.Append("''");
                }
                sbstrSQL.Append(" as PUBLIC_LIST_DT_4,");
                if (this.groupPUBLIC_LIST_DT_5 != null)
                {
                    sbstrSQL.Append("a.PUBLIC_LIST_DT_5");
                }
                else
                {
                    sbstrSQL.Append("''");
                }
                sbstrSQL.Append(" as PUBLIC_LIST_DT_5,");
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
                sbstrSQL.Append(" FROM PUBLIC_LIST a Where 1=1  ");
                #region WHERE CONIDTION
                if (this.wherePROCESS_DTE != null)
                {
                    sbstrSQL.Append(" and a.PROCESS_DTE=@wherePROCESS_DTE ");
                }
                if (this.whereRETURN_DTE != null)
                {
                    sbstrSQL.Append(" and a.RETURN_DTE=@whereRETURN_DTE ");
                }
                if (this.wherePAY_TYPE != null)
                {
                    sbstrSQL.Append(" and a.PAY_TYPE=@wherePAY_TYPE ");
                }
                if (this.wherePAY_SEQ != null)
                {
                    sbstrSQL.Append(" and a.PAY_SEQ=@wherePAY_SEQ ");
                }
                if (this.wherePAY_DATA_AREA != null)
                {
                    sbstrSQL.Append(" and a.PAY_DATA_AREA=@wherePAY_DATA_AREA ");
                }
                if (this.whereFILE_TRANSFER_TYPE != null)
                {
                    sbstrSQL.Append(" and a.FILE_TRANSFER_TYPE=@whereFILE_TRANSFER_TYPE ");
                }
                if (this.wherePUBLIC_LIST_FIELD_1 != null)
                {
                    sbstrSQL.Append(" and a.PUBLIC_LIST_FIELD_1=@wherePUBLIC_LIST_FIELD_1 ");
                }
                if (this.wherePUBLIC_LIST_FIELD_2 != null)
                {
                    sbstrSQL.Append(" and a.PUBLIC_LIST_FIELD_2=@wherePUBLIC_LIST_FIELD_2 ");
                }
                if (this.wherePUBLIC_LIST_FIELD_3 != null)
                {
                    sbstrSQL.Append(" and a.PUBLIC_LIST_FIELD_3=@wherePUBLIC_LIST_FIELD_3 ");
                }
                if (this.wherePUBLIC_LIST_FIELD_4 != null)
                {
                    sbstrSQL.Append(" and a.PUBLIC_LIST_FIELD_4=@wherePUBLIC_LIST_FIELD_4 ");
                }
                if (this.wherePUBLIC_LIST_FIELD_5 != null)
                {
                    sbstrSQL.Append(" and a.PUBLIC_LIST_FIELD_5=@wherePUBLIC_LIST_FIELD_5 ");
                }
                if (this.wherePUBLIC_LIST_AMT_1 > -1000000000000)
                {
                    sbstrSQL.Append(" and a.PUBLIC_LIST_AMT_1=@wherePUBLIC_LIST_AMT_1 ");
                }
                if (this.wherePUBLIC_LIST_AMT_2 > -1000000000000)
                {
                    sbstrSQL.Append(" and a.PUBLIC_LIST_AMT_2=@wherePUBLIC_LIST_AMT_2 ");
                }
                if (this.wherePUBLIC_LIST_AMT_3 > -1000000000000)
                {
                    sbstrSQL.Append(" and a.PUBLIC_LIST_AMT_3=@wherePUBLIC_LIST_AMT_3 ");
                }
                if (this.wherePUBLIC_LIST_AMT_4 > -1000000000000)
                {
                    sbstrSQL.Append(" and a.PUBLIC_LIST_AMT_4=@wherePUBLIC_LIST_AMT_4 ");
                }
                if (this.wherePUBLIC_LIST_AMT_5 > -1000000000000)
                {
                    sbstrSQL.Append(" and a.PUBLIC_LIST_AMT_5=@wherePUBLIC_LIST_AMT_5 ");
                }
                if (this.wherePUBLIC_LIST_DT_1 > dateStart)
                {
                    sbstrSQL.Append("  and a.PUBLIC_LIST_DT_1=@wherePUBLIC_LIST_DT_1 ");
                }
                if (this.wherePUBLIC_LIST_DT_2 > dateStart)
                {
                    sbstrSQL.Append("  and a.PUBLIC_LIST_DT_2=@wherePUBLIC_LIST_DT_2 ");
                }
                if (this.wherePUBLIC_LIST_DT_3 > dateStart)
                {
                    sbstrSQL.Append("  and a.PUBLIC_LIST_DT_3=@wherePUBLIC_LIST_DT_3 ");
                }
                if (this.wherePUBLIC_LIST_DT_4 > dateStart)
                {
                    sbstrSQL.Append("  and a.PUBLIC_LIST_DT_4=@wherePUBLIC_LIST_DT_4 ");
                }
                if (this.wherePUBLIC_LIST_DT_5 > dateStart)
                {
                    sbstrSQL.Append("  and a.PUBLIC_LIST_DT_5=@wherePUBLIC_LIST_DT_5 ");
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
                if (this.groupPROCESS_DTE != null)
                {
                    sbstrSQL.Append("a.PROCESS_DTE,");
                }
                if (this.groupRETURN_DTE != null)
                {
                    sbstrSQL.Append("a.RETURN_DTE,");
                }
                if (this.groupPAY_TYPE != null)
                {
                    sbstrSQL.Append("a.PAY_TYPE,");
                }
                if (this.groupPAY_SEQ != null)
                {
                    sbstrSQL.Append("a.PAY_SEQ,");
                }
                if (this.groupPAY_DATA_AREA != null)
                {
                    sbstrSQL.Append("a.PAY_DATA_AREA,");
                }
                if (this.groupFILE_TRANSFER_TYPE != null)
                {
                    sbstrSQL.Append("a.FILE_TRANSFER_TYPE,");
                }
                if (this.groupPUBLIC_LIST_FIELD_1 != null)
                {
                    sbstrSQL.Append("a.PUBLIC_LIST_FIELD_1,");
                }
                if (this.groupPUBLIC_LIST_FIELD_2 != null)
                {
                    sbstrSQL.Append("a.PUBLIC_LIST_FIELD_2,");
                }
                if (this.groupPUBLIC_LIST_FIELD_3 != null)
                {
                    sbstrSQL.Append("a.PUBLIC_LIST_FIELD_3,");
                }
                if (this.groupPUBLIC_LIST_FIELD_4 != null)
                {
                    sbstrSQL.Append("a.PUBLIC_LIST_FIELD_4,");
                }
                if (this.groupPUBLIC_LIST_FIELD_5 != null)
                {
                    sbstrSQL.Append("a.PUBLIC_LIST_FIELD_5,");
                }
                if (this.groupPUBLIC_LIST_AMT_1 != null)
                {
                    sbstrSQL.Append("a.PUBLIC_LIST_AMT_1,");
                }
                if (this.groupPUBLIC_LIST_AMT_2 != null)
                {
                    sbstrSQL.Append("a.PUBLIC_LIST_AMT_2,");
                }
                if (this.groupPUBLIC_LIST_AMT_3 != null)
                {
                    sbstrSQL.Append("a.PUBLIC_LIST_AMT_3,");
                }
                if (this.groupPUBLIC_LIST_AMT_4 != null)
                {
                    sbstrSQL.Append("a.PUBLIC_LIST_AMT_4,");
                }
                if (this.groupPUBLIC_LIST_AMT_5 != null)
                {
                    sbstrSQL.Append("a.PUBLIC_LIST_AMT_5,");
                }
                if (this.groupPUBLIC_LIST_DT_1 != null)
                {
                    sbstrSQL.Append("a.PUBLIC_LIST_DT_1,");
                }
                if (this.groupPUBLIC_LIST_DT_2 != null)
                {
                    sbstrSQL.Append("a.PUBLIC_LIST_DT_2,");
                }
                if (this.groupPUBLIC_LIST_DT_3 != null)
                {
                    sbstrSQL.Append("a.PUBLIC_LIST_DT_3,");
                }
                if (this.groupPUBLIC_LIST_DT_4 != null)
                {
                    sbstrSQL.Append("a.PUBLIC_LIST_DT_4,");
                }
                if (this.groupPUBLIC_LIST_DT_5 != null)
                {
                    sbstrSQL.Append("a.PUBLIC_LIST_DT_5,");
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
                if (sbstrSQL.ToString().Contains("@wherePROCESS_DTE "))
                {
                    this.SelectOperator.SetValue("@wherePROCESS_DTE", this.wherePROCESS_DTE, SqlDbType.VarChar);
                }
                if (sbstrSQL.ToString().Contains("@whereRETURN_DTE "))
                {
                    this.SelectOperator.SetValue("@whereRETURN_DTE", this.whereRETURN_DTE, SqlDbType.VarChar);
                }
                if (sbstrSQL.ToString().Contains("@wherePAY_TYPE "))
                {
                    this.SelectOperator.SetValue("@wherePAY_TYPE", this.wherePAY_TYPE, SqlDbType.VarChar);
                }
                if (sbstrSQL.ToString().Contains("@wherePAY_SEQ "))
                {
                    this.SelectOperator.SetValue("@wherePAY_SEQ", this.wherePAY_SEQ, SqlDbType.VarChar);
                }
                if (sbstrSQL.ToString().Contains("@wherePAY_DATA_AREA "))
                {
                    this.SelectOperator.SetValue("@wherePAY_DATA_AREA", this.wherePAY_DATA_AREA, SqlDbType.VarChar);
                }
                if (sbstrSQL.ToString().Contains("@whereFILE_TRANSFER_TYPE "))
                {
                    this.SelectOperator.SetValue("@whereFILE_TRANSFER_TYPE", this.whereFILE_TRANSFER_TYPE, SqlDbType.VarChar);
                }
                if (sbstrSQL.ToString().Contains("@wherePUBLIC_LIST_FIELD_1 "))
                {
                    this.SelectOperator.SetValue("@wherePUBLIC_LIST_FIELD_1", this.wherePUBLIC_LIST_FIELD_1, SqlDbType.VarChar);
                }
                if (sbstrSQL.ToString().Contains("@wherePUBLIC_LIST_FIELD_2 "))
                {
                    this.SelectOperator.SetValue("@wherePUBLIC_LIST_FIELD_2", this.wherePUBLIC_LIST_FIELD_2, SqlDbType.VarChar);
                }
                if (sbstrSQL.ToString().Contains("@wherePUBLIC_LIST_FIELD_3 "))
                {
                    this.SelectOperator.SetValue("@wherePUBLIC_LIST_FIELD_3", this.wherePUBLIC_LIST_FIELD_3, SqlDbType.VarChar);
                }
                if (sbstrSQL.ToString().Contains("@wherePUBLIC_LIST_FIELD_4 "))
                {
                    this.SelectOperator.SetValue("@wherePUBLIC_LIST_FIELD_4", this.wherePUBLIC_LIST_FIELD_4, SqlDbType.VarChar);
                }
                if (sbstrSQL.ToString().Contains("@wherePUBLIC_LIST_FIELD_5 "))
                {
                    this.SelectOperator.SetValue("@wherePUBLIC_LIST_FIELD_5", this.wherePUBLIC_LIST_FIELD_5, SqlDbType.VarChar);
                }
                if (sbstrSQL.ToString().Contains("@wherePUBLIC_LIST_AMT_1 "))
                {
                    this.SelectOperator.SetValue("@wherePUBLIC_LIST_AMT_1", this.wherePUBLIC_LIST_AMT_1, SqlDbType.Decimal);
                }
                if (sbstrSQL.ToString().Contains("@wherePUBLIC_LIST_AMT_2 "))
                {
                    this.SelectOperator.SetValue("@wherePUBLIC_LIST_AMT_2", this.wherePUBLIC_LIST_AMT_2, SqlDbType.Decimal);
                }
                if (sbstrSQL.ToString().Contains("@wherePUBLIC_LIST_AMT_3 "))
                {
                    this.SelectOperator.SetValue("@wherePUBLIC_LIST_AMT_3", this.wherePUBLIC_LIST_AMT_3, SqlDbType.Decimal);
                }
                if (sbstrSQL.ToString().Contains("@wherePUBLIC_LIST_AMT_4 "))
                {
                    this.SelectOperator.SetValue("@wherePUBLIC_LIST_AMT_4", this.wherePUBLIC_LIST_AMT_4, SqlDbType.Decimal);
                }
                if (sbstrSQL.ToString().Contains("@wherePUBLIC_LIST_AMT_5 "))
                {
                    this.SelectOperator.SetValue("@wherePUBLIC_LIST_AMT_5", this.wherePUBLIC_LIST_AMT_5, SqlDbType.Decimal);
                }
                if (sbstrSQL.ToString().Contains("@wherePUBLIC_LIST_DT_1 "))
                {
                    this.SelectOperator.SetValue("@wherePUBLIC_LIST_DT_1", this.wherePUBLIC_LIST_DT_1, SqlDbType.DateTime);
                }
                if (sbstrSQL.ToString().Contains("@wherePUBLIC_LIST_DT_2 "))
                {
                    this.SelectOperator.SetValue("@wherePUBLIC_LIST_DT_2", this.wherePUBLIC_LIST_DT_2, SqlDbType.DateTime);
                }
                if (sbstrSQL.ToString().Contains("@wherePUBLIC_LIST_DT_3 "))
                {
                    this.SelectOperator.SetValue("@wherePUBLIC_LIST_DT_3", this.wherePUBLIC_LIST_DT_3, SqlDbType.DateTime);
                }
                if (sbstrSQL.ToString().Contains("@wherePUBLIC_LIST_DT_4 "))
                {
                    this.SelectOperator.SetValue("@wherePUBLIC_LIST_DT_4", this.wherePUBLIC_LIST_DT_4, SqlDbType.DateTime);
                }
                if (sbstrSQL.ToString().Contains("@wherePUBLIC_LIST_DT_5 "))
                {
                    this.SelectOperator.SetValue("@wherePUBLIC_LIST_DT_5", this.wherePUBLIC_LIST_DT_5, SqlDbType.DateTime);
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
                myTable = this.SelectOperator.GetDataTable("PUBLIC_LIST");
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

        //#region query_for_out(String PAY_TYPE, string TODAY_PROCESS_DTE)
        //public string query_for_out(String PAY_TYPE, string TODAY_PROCESS_DTE)
        //{
        //    #region Modify History
        //    /// <history>
        //    /// <design>
        //    /// <name>Cybersoft MooreYang</name>
        //    /// <date>2011/2/9 上午 14:50:00</date>
        //    #endregion

        //    try
        //    {
        //        #region  set SQL statement
        //        StringBuilder sbstrSQL = new StringBuilder();
        //        //sbstrSQL.Append("SELECT * FROM PUBLIC_LIST WHERE PAY_TYPE = @PAY_TYPE AND RETURN_DTE > @TODAY_PROCESS_DTE AND RETURN_DTE <= @NEXT_PROCESS_DTE ");
        //        sbstrSQL.Append(" SELECT a.*  ");
        //        sbstrSQL.Append("  FROM PUBLIC_LIST a  ");
        //        sbstrSQL.Append("WHERE a.PAY_TYPE = @PAY_TYPE   AND   a.PROCESS_DTE = @TODAY_PROCESS_DTE ");
        //        if (strFlag == "T")
        //        {
        //            sbstrSQL.Append("   AND SUBSTRING(PAY_SEQ,12,1) = 'T' ");   //補執行時此欄為"T"
        //        }
        //        else
        //        {
        //            sbstrSQL.Append("   AND SUBSTRING(PAY_SEQ,12,1) != 'T' ");   //例行執行時此欄不可為"T"
        //        }
        //        sbstrSQL.Append(" AND SUBSTRING(a.PAY_SEQ,12,1) != 'R' ");                     
        //        sbstrSQL.Append("ORDER BY a.PAY_SEQ ");
        //        #endregion
        //        this.SelectOperator.SqlText = sbstrSQL.ToString();
        //        #region replace SQL parameter
        //        if (sbstrSQL.ToString().Contains("@TODAY_PROCESS_DTE "))
        //        {
        //            this.SelectOperator.SetValue("@TODAY_PROCESS_DTE", TODAY_PROCESS_DTE);
        //        }
        //        if (sbstrSQL.ToString().Contains("@PAY_TYPE "))
        //        {
        //            this.SelectOperator.SetValue("@PAY_TYPE", PAY_TYPE);
        //        }
        //        #endregion

        //        //myTable set to DataTable object
        //        myTable = this.SelectOperator.GetDataTable("PUBLIC_LIST");
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
        //    finally
        //    {

        //    }
        //    return msg_code;
        //}
        //#endregion      
        #region query_for_out(string TODAY_PROCESS_DTE, string strFlag)
        public string query_for_out(string TODAY_PROCESS_DTE, string strFlag)
        {
            #region Modify History
            /// <history>
            /// <design>
            /// <name>Cybersoft </name>
            /// <date>2014/11/09 </date>
            #endregion

            try
            {
                #region  set SQL statement
                StringBuilder sbstrSQL = new StringBuilder();
                //sbstrSQL.Append("SELECT * FROM PUBLIC_LIST WHERE PAY_TYPE = @PAY_TYPE AND RETURN_DTE > @TODAY_PROCESS_DTE AND RETURN_DTE <= @NEXT_PROCESS_DTE ");
                sbstrSQL.Append(" SELECT a.*  ");
                sbstrSQL.Append("  FROM  PUBLIC_LIST a  ");
                sbstrSQL.Append(" WHERE  1=1   ");
                sbstrSQL.Append("     AND  a.PROCESS_DTE = @TODAY_PROCESS_DTE ");
                if (strFlag == "T")
                {
                    sbstrSQL.Append("   AND SUBSTRING(PAY_SEQ,12,1) = 'T' ");   //補執行時此欄為"T"
                }
                else
                {
                    sbstrSQL.Append("   AND SUBSTRING(PAY_SEQ,12,1) != 'T' ");   //例行執行時此欄不可為"T"
                }
                sbstrSQL.Append(" AND SUBSTRING(a.PAY_SEQ,12,1) != 'R' ");
                if (this.wherePAY_TYPE != null)
                {
                    sbstrSQL.Append(" and a.PAY_TYPE=@wherePAY_TYPE ");
                }
                sbstrSQL.Append("ORDER BY a.PAY_SEQ ");
                #endregion
                this.SelectOperator.SqlText = sbstrSQL.ToString();
                #region replace SQL parameter
                if (sbstrSQL.ToString().Contains("@TODAY_PROCESS_DTE "))
                {
                    this.SelectOperator.SetValue("@TODAY_PROCESS_DTE", TODAY_PROCESS_DTE);
                }
                if (sbstrSQL.ToString().Contains("@wherePAY_TYPE "))
                {
                    this.SelectOperator.SetValue("@wherePAY_TYPE", this.wherePAY_TYPE);
                }
                #endregion

                //myTable set to DataTable object
                myTable = this.SelectOperator.GetDataTable("PUBLIC_LIST");
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
        #region query_for_out_RTN
        public string query_for_out_RTN(String PAY_TYPE, DateTime TODAY_PROCESS_DTE)
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
                //sbstrSQL.Append("SELECT * FROM PUBLIC_LIST WHERE PAY_TYPE = @PAY_TYPE AND RETURN_DTE > @TODAY_PROCESS_DTE AND RETURN_DTE <= @NEXT_PROCESS_DTE ");
                sbstrSQL.Append("SELECT * FROM PUBLIC_LIST WHERE PAY_TYPE = @PAY_TYPE AND PROCESS_DTE = @TODAY_PROCESS_DTE ");
                sbstrSQL.Append("   AND SUBSTRING(PAY_SEQ,12,1) = 'R'");
                sbstrSQL.Append(" ORDER BY PAY_SEQ ");
                #endregion
                this.SelectOperator.SqlText = sbstrSQL.ToString();
                #region replace SQL parameter
                if (sbstrSQL.ToString().Contains("@TODAY_PROCESS_DTE "))
                {
                    this.SelectOperator.SetValue("@TODAY_PROCESS_DTE", TODAY_PROCESS_DTE);
                }
                //if (sbstrSQL.ToString().Contains("@NEXT_PROCESS_DTE "))
                //{
                //    this.SelectOperator.SetValue("@NEXT_PROCESS_DTE", NEXT_PROCESS_DTE);
                //}
                if (sbstrSQL.ToString().Contains("@PAY_TYPE "))
                {
                    this.SelectOperator.SetValue("@PAY_TYPE", PAY_TYPE);
                }
                #endregion

                //myTable set to DataTable object
                myTable = this.SelectOperator.GetDataTable("PUBLIC_LIST");
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
        #region query_for_ACH
        public string query_for_ACH(String PAY_TYPE, DateTime TODAY_PROCESS_DTE)
        {
            #region Modify History
            /// <history>
            /// <design>取得當日ACH被動行資料
            /// <name>Cybersoft MooreYang</name>
            /// <date>2011/2/9 上午 14:50:00</date>
            #endregion

            try
            {
                #region  set SQL statement
                StringBuilder sbstrSQL = new StringBuilder();
                //sbstrSQL.Append("SELECT * FROM PUBLIC_LIST WHERE PAY_TYPE IN ( " + PAY_TYPE + " ) AND RETURN_DTE > @TODAY_PROCESS_DTE AND RETURN_DTE <= @NEXT_PROCESS_DTE ");
                sbstrSQL.Append("SELECT * FROM PUBLIC_LIST WHERE PAY_TYPE IN ( " + PAY_TYPE + " ) AND PROCESS_DTE = @TODAY_PROCESS_DTE ");
                sbstrSQL.Append("ORDER BY PAY_SEQ ");
                #endregion
                this.SelectOperator.SqlText = sbstrSQL.ToString();
                #region replace SQL parameter
                if (sbstrSQL.ToString().Contains("@TODAY_PROCESS_DTE "))
                {
                    this.SelectOperator.SetValue("@TODAY_PROCESS_DTE", TODAY_PROCESS_DTE);
                }
                //if (sbstrSQL.ToString().Contains("@NEXT_PROCESS_DTE "))
                //{
                //    this.SelectOperator.SetValue("@NEXT_PROCESS_DTE", NEXT_PROCESS_DTE);
                //}
                #endregion

                //myTable set to DataTable object
                myTable = this.SelectOperator.GetDataTable("PUBLIC_LIST");
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
                sbstrSQL.Append(" DELETE PUBLIC_LIST where ");
                if (strFlag == "Y")   //補執行
                {
                    sbstrSQL.Append(" SUBSTRING(PAY_SEQ,1,12) = @PAY_SEQ_11");
                }
                else
                {
                    sbstrSQL.Append(" SUBSTRING(PAY_SEQ,1,11) = @PAY_SEQ_11");
                    sbstrSQL.Append(" and SUBSTRING(PAY_SEQ,12,1) != 'T'             ");
                }
                if (this.wherePROCESS_DTE != null)
                {
                    sbstrSQL.Append(" and PROCESS_DTE=@wherePROCESS_DTE ");
                }
                if (this.whereRETURN_DTE != null)
                {
                    sbstrSQL.Append(" and RETURN_DTE=@whereRETURN_DTE ");
                }
                if (this.wherePAY_TYPE != null)
                {
                    sbstrSQL.Append(" and PAY_TYPE=@wherePAY_TYPE ");
                }
                if (this.whereFILE_TRANSFER_TYPE != null)
                {
                    sbstrSQL.Append(" and FILE_TRANSFER_TYPE=@whereFILE_TRANSFER_TYPE ");
                }
                if (this.whereMNT_USER != null)
                {
                    sbstrSQL.Append(" and MNT_USER=@whereMNT_USER ");
                }

                #endregion
                this.DeleteOperator.SqlText = sbstrSQL.ToString();
                #region repalce SQL parameter
                if (sbstrSQL.ToString().Contains("@PAY_SEQ_11"))
                {
                    this.DeleteOperator.SetValue("@PAY_SEQ_11", PAY_SEQ_11);
                }
                if (sbstrSQL.ToString().Contains("@wherePROCESS_DTE "))
                {
                    this.DeleteOperator.SetValue("@wherePROCESS_DTE", this.wherePROCESS_DTE);
                }
                if (sbstrSQL.ToString().Contains("@whereRETURN_DTE "))
                {
                    this.DeleteOperator.SetValue("@whereRETURN_DTE", this.whereRETURN_DTE);
                }
                if (sbstrSQL.ToString().Contains("@wherePAY_TYPE "))
                {
                    this.DeleteOperator.SetValue("@wherePAY_TYPE", this.wherePAY_TYPE);
                }
                if (sbstrSQL.ToString().Contains("@whereFILE_TRANSFER_TYPE "))
                {
                    this.DeleteOperator.SetValue("@whereFILE_TRANSFER_TYPE", this.whereFILE_TRANSFER_TYPE);
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
                MSG.strMsg = Convert.ToString(e.Number) + '-' + e.Message;
                msg_code = MSG.getMsg();
            }
            return msg_code;
        }
        #endregion
        #region query_for_PHONE_CHANGE() 取得中華電信扣款檔換號資訊
        public string query_for_PHONE_CHANGE()
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
                sbstrSQL.Append("SELECT * FROM PUBLIC_LIST ");
                sbstrSQL.Append(" WHERE PAY_TYPE = @wherePAY_TYPE AND PROCESS_DTE = @TODAY_PROCESS_DTE ");
                sbstrSQL.Append("   AND SUBSTRING(PAY_DATA_AREA,81,1) = 'C' ");
                sbstrSQL.Append(" ORDER BY PAY_SEQ ");
                #endregion
                this.SelectOperator.SqlText = sbstrSQL.ToString();
                #region replace SQL parameter
                this.SelectOperator.SetValue("@wherePAY_TYPE", this.strWherePAY_TYPE);
                this.SelectOperator.SetValue("@TODAY_PROCESS_DTE", this.strWherePROCESS_DTE);
                #endregion

                //myTable set to DataTable object
                myTable = this.SelectOperator.GetDataTable("PUBLIC_LIST");
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
        #region query_online_upload_H()
        public string query_online_upload_H()
        {
            #region Modify History
            /// <history>
            /// <design>
            /// <name>Cybersoft.COCA DaoGenerator</name>
            /// <date>2012/6/21 上午 09:20:15</date>
            #endregion
            try
            {
                #region set SQL statement
                StringBuilder sbstrSQL = new StringBuilder();
                sbstrSQL.Append("SELECT a.* FROM PUBLIC_LIST a where 1=1 ");
                sbstrSQL.Append(" and (a.PAY_DATA_AREA  like '1%') ");
                sbstrSQL.Append(" and a.PAY_SEQ=@wherePAY_SEQ ");
                if (this.wherePROCESS_DTE != null)
                {
                    sbstrSQL.Append(" and a.PROCESS_DTE=@wherePROCESS_DTE ");
                }
                if (this.whereRETURN_DTE != null)
                {
                    sbstrSQL.Append(" and a.RETURN_DTE=@whereRETURN_DTE ");
                }
                if (this.wherePAY_TYPE != null)
                {
                    sbstrSQL.Append(" and a.PAY_TYPE=@wherePAY_TYPE ");
                }
                if (this.whereFILE_TRANSFER_TYPE != null)
                {
                    sbstrSQL.Append(" and a.FILE_TRANSFER_TYPE=@whereFILE_TRANSFER_TYPE ");
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
                if (sbstrSQL.ToString().Contains("@wherePROCESS_DTE "))
                {
                    this.SelectOperator.SetValue("@wherePROCESS_DTE", this.wherePROCESS_DTE);
                }
                if (sbstrSQL.ToString().Contains("@whereRETURN_DTE "))
                {
                    this.SelectOperator.SetValue("@whereRETURN_DTE", this.whereRETURN_DTE);
                }
                if (sbstrSQL.ToString().Contains("@wherePAY_TYPE "))
                {
                    this.SelectOperator.SetValue("@wherePAY_TYPE", this.wherePAY_TYPE);
                }
                if (sbstrSQL.ToString().Contains("@wherePAY_SEQ "))
                {
                    this.SelectOperator.SetValue("@wherePAY_SEQ", this.wherePAY_SEQ);
                }
                if (sbstrSQL.ToString().Contains("@wherePAY_DATA_AREA "))
                {
                    this.SelectOperator.SetValue("@wherePAY_DATA_AREA", this.wherePAY_DATA_AREA);
                }
                if (sbstrSQL.ToString().Contains("@whereFILE_TRANSFER_TYPE "))
                {
                    this.SelectOperator.SetValue("@whereFILE_TRANSFER_TYPE", this.whereFILE_TRANSFER_TYPE);
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
                myTable = this.SelectOperator.GetDataTable("PUBLIC_LIST");
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
        #region query_online_upload_D()
        public string query_online_upload_D()
        {
            #region Modify History
            /// <history>
            /// <design>
            /// <name>Cybersoft.COCA DaoGenerator</name>
            /// <date>2012/6/21 上午 09:20:15</date>
            #endregion
            try
            {
                #region set SQL statement
                StringBuilder sbstrSQL = new StringBuilder();
                sbstrSQL.Append("SELECT a.* FROM PUBLIC_LIST a where 1=1 ");
                sbstrSQL.Append("  and (a.PAY_DATA_AREA  LIKE '2%') ");
                sbstrSQL.Append(" and (a.PAY_SEQ LIKE '" + this.@wherePAY_SEQ + "%') ");
                if (this.wherePROCESS_DTE != null)
                {
                    sbstrSQL.Append(" and a.PROCESS_DTE=@wherePROCESS_DTE ");
                }
                if (this.whereRETURN_DTE != null)
                {
                    sbstrSQL.Append(" and a.RETURN_DTE=@whereRETURN_DTE ");
                }
                if (this.wherePAY_TYPE != null)
                {
                    sbstrSQL.Append(" and a.PAY_TYPE=@wherePAY_TYPE ");
                }
                if (this.whereFILE_TRANSFER_TYPE != null)
                {
                    sbstrSQL.Append(" and a.FILE_TRANSFER_TYPE=@whereFILE_TRANSFER_TYPE ");
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
                if (sbstrSQL.ToString().Contains("@wherePROCESS_DTE "))
                {
                    this.SelectOperator.SetValue("@wherePROCESS_DTE", this.wherePROCESS_DTE);
                }
                if (sbstrSQL.ToString().Contains("@whereRETURN_DTE "))
                {
                    this.SelectOperator.SetValue("@whereRETURN_DTE", this.whereRETURN_DTE);
                }
                if (sbstrSQL.ToString().Contains("@wherePAY_TYPE "))
                {
                    this.SelectOperator.SetValue("@wherePAY_TYPE", this.wherePAY_TYPE);
                }
                if (sbstrSQL.ToString().Contains("@wherePAY_SEQ "))
                {
                    this.SelectOperator.SetValue("@wherePAY_SEQ", this.wherePAY_SEQ);
                }
                if (sbstrSQL.ToString().Contains("@wherePAY_DATA_AREA "))
                {
                    this.SelectOperator.SetValue("@wherePAY_DATA_AREA", this.wherePAY_DATA_AREA);
                }
                if (sbstrSQL.ToString().Contains("@whereFILE_TRANSFER_TYPE "))
                {
                    this.SelectOperator.SetValue("@whereFILE_TRANSFER_TYPE", this.whereFILE_TRANSFER_TYPE);
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
                myTable = this.SelectOperator.GetDataTable("PUBLIC_LIST");
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


