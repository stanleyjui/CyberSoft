using System;
using System.IO;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.DataVisualization.Charting;
using System.Drawing;
using iTextSharp.text;
using iTextSharp.text.pdf;
using CARDWeb;
using CARDWeb.CARD.src;

namespace CyberTestWeb.src
{
    public partial class TPOTXN002 : Cybersoft.Web.UI.LanguagePage
    {
        Boolean formtag = false;
        string strResultFromTX_UNBILL = "";
        static readonly Cybersoft.Coca.Log.Logger logger = Cybersoft.Log.LogManager<Cybersoft.Coca.Log.Logger>.GetLogger(System.Reflection.MethodInfo.GetCurrentMethod().ReflectedType.Name);
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!Page.IsPostBack)
                {
                    //呼叫初始值method
                    initData(0);
                    //呼叫資料庫資料及呈現於畫面上
                    loadGridData();
                    //有查詢條件時就紀錄log:DB Log@@@
                    #region 有查詢條件時就紀錄log
                    if (Convert.ToString(Session["keyACCT_NBR"]) != "")
                    {
                        Msg.writeQueryLog("", "list", "gridview");
                    }
                    #endregion
                }
                //隱藏下載區域
                HtmlGenericControl divDownLoad = (HtmlGenericControl)this.Master.FindControl("divDownLoad");
                divDownLoad.Style["visibility"] = "hidden";

            }
            catch (Exception e_page)
            {
                Msg.getMsg("F9999", System.Reflection.MethodInfo.GetCurrentMethod().Name, Master);
                logger.strJobQueue = e_page.ToString();
                logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
            }
        }
        private void initData(int flag)
        {
            try
            {
                if (flag == 0) //清除PnlContent
                {
                    //自動設定欄位初始值
                    PanelControl.setInitFields(this.PnlContent);
                    //查詢條件給初始值
                    txtwherePOSTING_DTE_FR.Text = "";
                    txtwherePOSTING_DTE_TO.Text = "";
                }
                else           //清除PnlDetail
                {
                    PanelControl.setInitFields(this.PnlDetail);
                    PanelControl.setInitFields(this.Panel1);
                    PanelControl.setInitFields(this.Panel2);
                    PanelControl.setInitFields(this.Panel3);
                }
            }
            catch (Exception e_page)
            {
                logger.strJobQueue = e_page.ToString();
                logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
            }

        }
        private void loadGridData()
        {
            try
            {
                #region 呼叫TX_UNBILLDao
                Cybersoft.Data.DAL.TX_UNBILLDao TX_UNBILL = new Cybersoft.Data.DAL.TX_UNBILLDao();
                DataTable resultTableFromTX_UNBILL = new DataTable();

                #region KEY值
                //事業單位
                #region 事業單位
                if (Convert.ToString(Session["keyBU"]) != "")
                {
                    TX_UNBILL.strWhereBU = Convert.ToString(Session["keyBU"]);
                }
                #endregion
                //產品碼
                #region 產品碼
                if (Convert.ToString(Session["keyPRODUCT"]) != "")
                {
                    TX_UNBILL.strWherePRODUCT = Convert.ToString(Session["keyPRODUCT"]);
                    if (Convert.ToString(Session["keyPRODUCT"]) != "000")
                    {
                        TX_UNBILL.strWhereCARD_PRODUCT = Convert.ToString(Session["keyPRODUCT"]);
                    }
                }
                #endregion
                //帳號
                #region 帳號
                if (Convert.ToString(Session["keyACCT_NBR"]) != "")
                {
                    TX_UNBILL.strWhereACCT_NBR = Convert.ToString(Session["keyACCT_NBR"]);
                }
                #endregion
                //卡號
                #region 卡號
                if (Convert.ToString(Session["keyCARD_NBR"]) != "")
                {
                    TX_UNBILL.strWhereCARD_NBR = Convert.ToString(Session["keyCARD_NBR"]);
                }
                if (Convert.ToString(Session["keyACCT_NBR"]) == "" && Convert.ToString(Session["keyCARD_NBR"]) == "")
                {
                    TX_UNBILL.strWhereBU = "000";
                    this.gdvdata.DataSource = null;
                    this.gdvdata.DataBind();
                    strResultFromTX_UNBILL = "F0023";
                    return;
                }
                #endregion
                //入帳日期
                #region 入帳日期
                DataTable SETUP_SYSTEM = (DataTable)System.Web.HttpContext.Current.Application["DataTableSYSTEM"];
                if (this.txtwherePOSTING_DTE_FR.Text == "")
                {
                    this.txtwherePOSTING_DTE_FR.Text = Convert.ToDateTime(SETUP_SYSTEM.Rows[0]["NEXT_PROCESS_DTE"]).AddYears(-1).ToString("yyyy/MM/dd");
                }
                TX_UNBILL.DateTimeWherePOSTING_DTE_FR = Convert.ToDateTime(txtwherePOSTING_DTE_FR.Text);
                if (this.txtwherePOSTING_DTE_TO.Text == "")
                {
                    this.txtwherePOSTING_DTE_TO.Text = Convert.ToDateTime(SETUP_SYSTEM.Rows[0]["NEXT_PROCESS_DTE"]).ToString("yyyy/MM/dd");
                }
                TX_UNBILL.DateTimeWherePOSTING_DTE_TO = Convert.ToDateTime(txtwherePOSTING_DTE_TO.Text);
                #endregion


                #endregion
                strResultFromTX_UNBILL = TX_UNBILL.query_by_tx_bill("Y", "Y", "Y");
                //紀錄資料存取物件:DB Log@@@
                sessionControl.dOP = TX_UNBILL.SelectOperator;
                switch (strResultFromTX_UNBILL)
                {
                    case "S0000": //成功
                        //resultTableFromTX_UNBILL 變數為 DataTable 物件
                        resultTableFromTX_UNBILL = TX_UNBILL.resultTable;
                        //查詢選項
                        #region 查詢選項
                        switch (ddlOPTION.SelectedValue)
                        {
                            case "USER":
                                resultTableFromTX_UNBILL.DefaultView.RowFilter = " MT_TYPE <> 'I' AND USER_CHAR_1 <> 'Y'  ";
                                resultTableFromTX_UNBILL = resultTableFromTX_UNBILL.DefaultView.ToTable();

                                break;

                            case "PMT":
                                resultTableFromTX_UNBILL.DefaultView.RowFilter = " MT_TYPE = 'C' AND CODE = '0092'  ";
                                resultTableFromTX_UNBILL = resultTableFromTX_UNBILL.DefaultView.ToTable();

                                break;

                        }

                        #endregion
                        //取得說明
                        #region 手動取得MCC CODE/CARD PRODUCT/TX_SOURCE 說明/STMT_MONTH
                        resultTableFromTX_UNBILL.Columns.Add("MCC_CODE_DESCR", typeof(string));
                        resultTableFromTX_UNBILL.Columns.Add("CARD_PRODUCT_DESCR", typeof(string));
                        resultTableFromTX_UNBILL.Columns.Add("TYPE_CODE", typeof(string));
                        resultTableFromTX_UNBILL.Columns.Add("STMT_MONTH", typeof(string));
                        for (int i = 0; i < resultTableFromTX_UNBILL.Rows.Count; i++)
                        {
                            //特店消費類別
                            string MCC_CODE = Convert.ToString(resultTableFromTX_UNBILL.Rows[i]["MCC_CODE"]);
                            resultTableFromTX_UNBILL.Rows[i]["MCC_CODE_DESCR"] = ControlTWA.GetMCC_DESCR(MCC_CODE);
                            //卡別
                            string CARD_PRODUCT = Convert.ToString(resultTableFromTX_UNBILL.Rows[i]["CARD_PRODUCT"]);
                            resultTableFromTX_UNBILL.Rows[i]["CARD_PRODUCT_DESCR"] = ControlTWA.GetPRODUCT_DESCR(CARD_PRODUCT);
                            //借貸
                            resultTableFromTX_UNBILL.Rows[i]["TYPE_CODE"] = Convert.ToString(resultTableFromTX_UNBILL.Rows[i]["MT_TYPE"]) + Convert.ToString(resultTableFromTX_UNBILL.Rows[i]["CODE"]).Substring(2, 2);
                            //若為繳款或交易調整，金額顯示為負值
                            if (Convert.ToString(resultTableFromTX_UNBILL.Rows[i]["MT_TYPE"]) == "C")
                            {
                                resultTableFromTX_UNBILL.Rows[i]["AMT"] = Convert.ToDecimal(resultTableFromTX_UNBILL.Rows[i]["AMT"]) * -1;
                            }
                            //卡號編輯
                            string CARD_NBR = Convert.ToString(resultTableFromTX_UNBILL.Rows[i]["CARD_NBR"]);
                            if (CARD_NBR.Length >= 15)
                            {
                                resultTableFromTX_UNBILL.Rows[i]["CARD_NBR"] = CARD_NBR.Substring(0, 4) + "-" + CARD_NBR.Substring(4, 4) + "-" + CARD_NBR.Substring(8, 4) + "-" + CARD_NBR.Substring(12, CARD_NBR.Length - 12);
                            }
                            //STMT_MONTH
                            #region STMT_MONTH
                            DateTime STMT_DTE = Convert.ToDateTime(resultTableFromTX_UNBILL.Rows[i]["STMT_DTE"]);
                            if (STMT_DTE == new DateTime(1900, 1, 1))
                            {
                                //未出帳單
                                resultTableFromTX_UNBILL.Rows[i]["STMT_MONTH"] = "*";
                            }
                            else
                            {
                                resultTableFromTX_UNBILL.Rows[i]["STMT_MONTH"] = STMT_DTE.Month.ToString("00");
                            }
                            #endregion
                        }
                        #endregion
                        //將Grid欄位設為資料庫值
                        resultTableFromTX_UNBILL = resultTableFromTX_UNBILL.DefaultView.ToTable();
                        //暫存資料
                        Session["DataTableTX_UNBILL"] = resultTableFromTX_UNBILL;
                        this.gdvdata.DataSource = resultTableFromTX_UNBILL;
                        this.gdvdata.DataBind();
                        break;
                    case "F0023": //查無資料

                        this.gdvdata.DataSource = null;
                        this.gdvdata.DataBind();

                        return;
                    default: //資料庫錯誤
                        return;
                }
                #endregion
                //隱藏Page_Init新增控制項
                visibleControl();
            }
            catch (Exception e_page)
            {
                logger.strJobQueue = e_page.ToString();
                logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
            }
        }
        protected void itnQuery_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                //呼叫資料庫資料及呈現於畫面上
                loadGridData();
                //取得回應訊息
                Msg.getMsg(strResultFromTX_UNBILL, System.Reflection.MethodInfo.GetCurrentMethod().Name, Master);
            }
            catch (Exception e_page)
            {
                Msg.getMsg("F9999", System.Reflection.MethodInfo.GetCurrentMethod().Name, Master);
                logger.strJobQueue = e_page.ToString();
                logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
            }
        }
        protected void itnClear_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                //呼叫initData
                initData(0);
                //取得回應訊息
                Msg.getMsg("S0000", System.Reflection.MethodInfo.GetCurrentMethod().Name, Master);
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected void itnTest_Click(object sender, EventArgs e)
        {
            try
            {
                Msg.getMsg("S0000", System.Reflection.MethodInfo.GetCurrentMethod().Name, Master);
                int i = 0;
                if (string.IsNullOrEmpty(Convert.ToString(Session["v"])))
                {
                    Session["v"] = "1";
                }
                else
                {
                    i += Convert.ToInt32(Session["v"]);
                }
                this.Label1.Text = DateTime.Now.AddDays(+1).ToShortDateString();

            }
            catch (Exception)
            {
                throw;
            }
        }
        protected void itnDetail_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                Session["SessionMode"] = "DETAIL_MODE";
                //呼叫setDataFromDb讀取資料庫資料及顯示在畫面欄位上
                if (setDataFromDb("detail"))
                {
                    //隱藏查詢區
                    this.PnlContent.Visible = false;
                    //設定欄位Enable屬性
                    PanelControl.setFieldsEnable(this.PnlDetail, false);
                    PanelControl.setFieldsEnable(this.Panel1, false);
                    PanelControl.setFieldsEnable(this.Panel2, false);
                    PanelControl.setFieldsEnable(this.Panel3, false);
                    //設定按鍵屬性
                    PanelControl.setButtonEnable(this.PnlDetail, System.Reflection.MethodInfo.GetCurrentMethod().Name);
                    //設定訊息區內容
                    Msg.getMsg("MODE", System.Reflection.MethodInfo.GetCurrentMethod().Name, Master);
                }
                else
                {
                    //隱藏畫面編緝區
                    this.PnlDetail.Visible = false;
                    //設定訊息區內容(請先選擇項目再按明細)
                    Msg.getMsg("I0023", System.Reflection.MethodInfo.GetCurrentMethod().Name, Master);
                }
            }
            catch (Exception e_page)
            {
                Msg.getMsg("F9999", System.Reflection.MethodInfo.GetCurrentMethod().Name, Master);
                logger.strJobQueue = e_page.ToString();
                logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
            }
        }
        protected void itnCancel_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                //呼叫資料庫資料及呈現於畫面上
                loadGridData();
                //顯示查詢區
                this.PnlContent.Visible = true;
                //隱藏資料編輯區
                this.PnlDetail.Visible = false;
                //設定訊息區內容
                Msg.getMsg("S0000", System.Reflection.MethodInfo.GetCurrentMethod().Name, Master);
            }
            catch (Exception e_page)
            {
                Msg.getMsg("F9999", System.Reflection.MethodInfo.GetCurrentMethod().Name, Master);
                logger.strJobQueue = e_page.ToString();
                logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
            }
        }
        protected void itnUpdate_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                Session["SessionMode"] = "UPDATE_MODE";
                //呼叫setDataFromDb讀取資料庫資料及顯示在畫面欄位上
                if (setDataFromDb("update"))
                {
                    //隱藏查詢區
                    this.PnlContent.Visible = false;
                    //設定欄位Enable屬性 -- 全部鎖定，只能修改TX_RATE
                    PanelControl.setFieldsEnable(this.PnlDetail, false);
                    PanelControl.setFieldsEnable(this.Panel1, false);
                    PanelControl.setFieldsEnable(this.Panel2, false);
                    PanelControl.setFieldsEnable(this.Panel3, false);
                    //設定按鍵屬性
                    PanelControl.setButtonEnable(this.PnlDetail, System.Reflection.MethodInfo.GetCurrentMethod().Name);
                    //設定可修改欄位
                    if (!"C".Equals(this.lblFILE_SOURCE.Text))
                    {
                        PanelControl.setTextBoxEnable(this.txtTX_RATE, true);
                    }
                    //設定游標位置
                    this.txtTX_RATE.Focus();
                    //設定訊息區內容
                    Msg.getMsg("MODE", System.Reflection.MethodInfo.GetCurrentMethod().Name, Master);
                }
                else
                {
                    //隱藏畫面編緝區
                    this.PnlDetail.Visible = false;
                    //設定訊息區內容(請先選擇項目再按修改)
                    Msg.getMsg("I0023", System.Reflection.MethodInfo.GetCurrentMethod().Name, Master);
                }
            }
            catch (Exception e_page)
            {
                Msg.getMsg("F9999", System.Reflection.MethodInfo.GetCurrentMethod().Name, Master);
                logger.strJobQueue = e_page.ToString();
                logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
            }
        }
        protected void itnSave_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                #region 確定頁面通過檢查後才往下
                if (this.Page.IsValid == false)
                {
                    return;
                }
                #endregion
                DataTable resultTableFromTX_UNBILL = Session["DataTableTX_UNBILL"] as DataTable;
                string strResultFromTX_UNBILL = "";
                #region key process
                Session["keyBU"] = Convert.ToString(resultTableFromTX_UNBILL.Rows[0]["BU"]);
                Session["keyPRODUCT"] = Convert.ToString(resultTableFromTX_UNBILL.Rows[0]["PRODUCT"]);
                Session["keyACCT_NBR"] = Convert.ToString(resultTableFromTX_UNBILL.Rows[0]["ACCT_NBR"]);
                Session["keyCURRENCY"] = Convert.ToString(resultTableFromTX_UNBILL.Rows[0]["CURRENCY"]);
                Session["keyCARD_NBR"] = Convert.ToString(resultTableFromTX_UNBILL.Rows[0]["CARD_NBR"]);
                #endregion
                if ("A".Equals(this.lblFILE_SOURCE.Text)) //TX_UNBILL
                {
                    Cybersoft.Data.DAL.TX_UNBILLDao TX_UNBILL = new Cybersoft.Data.DAL.TX_UNBILLDao();
                    TX_UNBILL.init();
                    #region 其他欄位
                    TX_UNBILL.decTX_RATE = Convert.ToDecimal(this.txtTX_RATE.Text) / 100;
                    TX_UNBILL.strMNT_USER = Convert.ToString(Session["SessionAccount"]);
                    TX_UNBILL.datetimeMNT_DT = DateTime.Now;
                    #endregion
                    #region 依目前模式進行資料存取
                    TX_UNBILL.strWhereBU = Convert.ToString(resultTableFromTX_UNBILL.Rows[0]["BU"]);
                    TX_UNBILL.strWherePRODUCT = Convert.ToString(resultTableFromTX_UNBILL.Rows[0]["PRODUCT"]);
                    TX_UNBILL.strWhereACCT_NBR = Convert.ToString(resultTableFromTX_UNBILL.Rows[0]["ACCT_NBR"]);
                    TX_UNBILL.strWhereCURRENCY = Convert.ToString(resultTableFromTX_UNBILL.Rows[0]["CURRENCY"]);
                    TX_UNBILL.DateTimeWherePOSTING_DTE = Convert.ToDateTime(this.txtPOSTING_DTE.Text);
                    TX_UNBILL.DateTimeWhereEFF_DTE = Convert.ToDateTime(this.txtEFF_DTE.Text);
                    TX_UNBILL.strWhereCODE = this.txtCODE.Text;
                    TX_UNBILL.strWhereSEQ = Convert.ToString(resultTableFromTX_UNBILL.Rows[0]["SEQ"]);
                    TX_UNBILL.strWhereARN = this.txtARN.Text;
                    TX_UNBILL.strWherePOST_RESULT = Convert.ToString(resultTableFromTX_UNBILL.Rows[0]["POST_RESULT"]);
                    TX_UNBILL.resultTable = resultTableFromTX_UNBILL;  //修改前欄位
                    strResultFromTX_UNBILL = TX_UNBILL.update(ControlTWA.SessionToHash());
                    //紀錄資料存取物件:DB Log@@@
                    sessionControl.dOP = TX_UNBILL.UpdateOperator;
                    #endregion
                }
                else //TX_BILL
                {
                    Cybersoft.Data.DAL.TX_BILLDao TX_BILL = new Cybersoft.Data.DAL.TX_BILLDao();
                    TX_BILL.init();
                    #region 其他欄位
                    TX_BILL.decTX_RATE = Convert.ToDecimal(this.txtTX_RATE.Text) / 100;
                    TX_BILL.strMNT_USER = Convert.ToString(Session["SessionAccount"]);
                    TX_BILL.datetimeMNT_DT = DateTime.Now;
                    #endregion
                    #region 依目前模式進行資料存取
                    TX_BILL.strWhereBU = Convert.ToString(resultTableFromTX_UNBILL.Rows[0]["BU"]);
                    TX_BILL.strWherePRODUCT = Convert.ToString(resultTableFromTX_UNBILL.Rows[0]["PRODUCT"]);
                    TX_BILL.strWhereACCT_NBR = Convert.ToString(resultTableFromTX_UNBILL.Rows[0]["ACCT_NBR"]);
                    TX_BILL.strWhereCURRENCY = Convert.ToString(resultTableFromTX_UNBILL.Rows[0]["CURRENCY"]);
                    TX_BILL.DateTimeWherePOSTING_DTE = Convert.ToDateTime(this.txtPOSTING_DTE.Text);
                    TX_BILL.DateTimeWhereEFF_DTE = Convert.ToDateTime(this.txtEFF_DTE.Text);
                    TX_BILL.strWhereCODE = this.txtCODE.Text;
                    TX_BILL.strWhereSEQ = Convert.ToString(resultTableFromTX_UNBILL.Rows[0]["SEQ"]);
                    TX_BILL.strWhereARN = this.txtARN.Text;
                    TX_BILL.strWherePOST_RESULT = Convert.ToString(resultTableFromTX_UNBILL.Rows[0]["POST_RESULT"]);
                    TX_BILL.resultTable = resultTableFromTX_UNBILL;  //修改前欄位
                    strResultFromTX_UNBILL = TX_BILL.update(ControlTWA.SessionToHash());
                    //紀錄資料存取物件:DB Log@@@
                    sessionControl.dOP = TX_BILL.UpdateOperator;
                    #endregion
                }
                #region 訊息處理
                switch (strResultFromTX_UNBILL)
                {
                    case "S0000": //成功
                        //顯示查詢區
                        this.PnlContent.Visible = true;
                        //隱藏畫面編輯區
                        this.PnlDetail.Visible = false;
                        //呼叫資料庫資料及呈現於畫面上
                        loadGridData();
                        //設定訊息區內容
                        Msg.getMsg(strResultFromTX_UNBILL, System.Reflection.MethodInfo.GetCurrentMethod().Name, Master);
                        break;
                    default: //資料庫錯誤
                        Msg.getMsg(strResultFromTX_UNBILL, System.Reflection.MethodInfo.GetCurrentMethod().Name, Master);
                        break;
                }
                #endregion
            }
            catch (Exception e_page)
            {
                Msg.getMsg("F9999", System.Reflection.MethodInfo.GetCurrentMethod().Name, Master);
                logger.strJobQueue = e_page.ToString();
                logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
            }
        }
        protected void itnPrint_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                //取消分頁，即設定 GridView.AllowPaging=False
                #region 取消分頁
                this.gdvdata.PageSize = 1000;
                DataTable resultTableFromTX_BILL = (DataTable)Session["DataTableTX_UNBILL"];
                this.gdvdata.DataSource = resultTableFromTX_BILL;
                this.gdvdata.DataBind();
                #endregion
                //this.Master.ItnPrint_Click(sender, e);
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "page_print", "window.open('../Print.aspx','Print','width=1024px,scrollbars=768');", true);
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected void gdvdata_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //給每個rtnseleciton綁定setRadio事件
            try
            {
                //製作gridview光棒效果
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    ((RadioButton)e.Row.FindControl("rtnselection")).Attributes.Add("onclick", "setRadio(this,'gdvdata','rtnselection')");
                    int intPageIndex = (e.Row.DataItemIndex + 1) % this.gdvdata.PageSize; if (intPageIndex == 0) { intPageIndex = this.gdvdata.PageSize; }
                    if (intPageIndex == 1)
                    {
                        ((RadioButton)e.Row.FindControl("rtnselection")).Checked = true;
                    }
                    e.Row.Attributes.Add("onclick", "setRadio2(" + intPageIndex.ToString() + ",'gdvdata','rtnselection')");
                    e.Row.Attributes.Add("onmouseover", PanelControl.strRowOnmouseover);
                    e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=currentcolor;");

                }
            }
            catch (Exception e_page)
            {
                logger.strJobQueue = e_page.ToString();
                logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
            }

        }
        protected void gdvdata_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                //將PageIndex指定為目前使用者點選的頁碼
                gdvdata.PageIndex = e.NewPageIndex;
                //重新指定資料來源
                DataTable resultTableFromTX_UNBILL = (DataTable)Session["DataTableTX_UNBILL"];
                gdvdata.DataSource = resultTableFromTX_UNBILL;
                //將資料來源與GridView繫在一起
                gdvdata.DataBind();
            }
            catch (Exception e_page)
            {
                logger.strJobQueue = e_page.ToString();
                logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
            }
        }
        protected void gdvdata_Sorting(object sender, GridViewSortEventArgs e)
        {
            try
            {
                string sortDirection;
                string sortExpression = e.SortExpression.ToString();
                if (ViewState[sortExpression] == null)
                {
                    ViewState[sortExpression] = "Desc";
                    sortDirection = sortExpression + " Desc";
                }
                else
                {
                    //ASC<-->DESC(對調)
                    if (ViewState[sortExpression].ToString() == "Asc")
                    {
                        ViewState[sortExpression] = "Desc";
                        sortDirection = sortExpression + " Desc";
                    }
                    else
                    {
                        ViewState[sortExpression] = "Asc";
                        sortDirection = sortExpression + " Asc";
                    }
                }
                //指定排序方向
                DataTable resultTableFromTX_UNBILL = (DataTable)Session["DataTableTX_UNBILL"];
                resultTableFromTX_UNBILL.DefaultView.Sort = sortDirection;
                //暫存資料
                Session["DataTableTX_UNBILL"] = resultTableFromTX_UNBILL.DefaultView.ToTable();
                //重新指定資料來源
                gdvdata.DataSource = resultTableFromTX_UNBILL;
                //將資料來源與GridView繫在一起
                gdvdata.DataBind();
            }
            catch (Exception e_page)
            {
                logger.strJobQueue = e_page.ToString();
                logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
            }
        }
        protected void gdvdata_RowCreated(object sender, GridViewRowEventArgs e)
        {
            try
            {
                #region Gridview header處理
                if (e.Row.RowType == DataControlRowType.Header)
                {
                    e.Row.Cells[1].Visible = false;
                    if (formtag == false)
                    {
                        e.Row.Cells[2].Visible = false;
                        e.Row.Cells[3].Visible = false;
                        e.Row.Cells[4].Visible = false;
                    }
                    e.Row.Cells[5].Visible = false;
                }
                #endregion
                if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Footer)
                {
                    //隱藏cell1-3
                    e.Row.Cells[1].Visible = false;
                    if (formtag == false)
                    {
                        e.Row.Cells[2].Visible = false;
                        e.Row.Cells[3].Visible = false;
                        e.Row.Cells[4].Visible = false;
                    }
                    e.Row.Cells[5].Visible = false;

                    //處理行數
                    int row_total_count = gdvdata.Controls[0].Controls.Count;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        private bool setDataFromDb(string action)
        {
            try
            {
                //顯示畫面編輯區
                this.PnlDetail.Visible = true;
                //設定初始值
                initData(1);
                //設定初始值
                bool isFound = false;
                //取得資料
                DataTable resultTableFromTX_UNBILL = (DataTable)Session["DataTableTX_UNBILL"];
                //取得目前頁數對應之筆數
                int k = this.gdvdata.PageIndex * this.gdvdata.PageSize;

                for (int i = 0; i < this.gdvdata.Rows.Count; i++)
                {
                    RadioButton rtnselected = (RadioButton)this.gdvdata.Rows[i].FindControl("rtnselection");
                    if (rtnselected.Checked)
                    {
                        isFound = true;

                        //選取轉分期或帳務調整
                        #region 選取轉分期或帳務調整
                        if (action == "TRANSFER")
                        {
                            //建立交易資料表
                            #region 建立交易資料表
                            DataTable DataTableINST_TX_DATA = new DataTable();
                            DataTableINST_TX_DATA.TableName = "TX";
                            DataTableINST_TX_DATA.Columns.Add("BU", typeof(string));
                            DataTableINST_TX_DATA.Columns.Add("CARD_NBR", typeof(string));
                            DataTableINST_TX_DATA.Columns.Add("EFF_DTE", typeof(DateTime));
                            DataTableINST_TX_DATA.Columns.Add("AMT", typeof(decimal));
                            DataTableINST_TX_DATA.Columns.Add("AUTH_CODE", typeof(string));
                            DataTableINST_TX_DATA.Columns.Add("MER_NBR", typeof(string));
                            DataTableINST_TX_DATA.Columns.Add("DESCR", typeof(string));
                            DataTableINST_TX_DATA.Columns.Add("ARN", typeof(string));
                            DataTableINST_TX_DATA.Columns.Add("MCC_CODE", typeof(string));
                            DataTableINST_TX_DATA.Columns.Add("COUNTRY_CODE", typeof(string));
                            DataTableINST_TX_DATA.Columns.Add("CITY_CODE", typeof(string));
                            DataTableINST_TX_DATA.Columns.Add("ORIG_CURRENCY", typeof(string));
                            DataTableINST_TX_DATA.Columns.Add("ORIG_AMT", typeof(decimal));
                            DataTableINST_TX_DATA.Columns.Add("USER_AMT_4", typeof(decimal));
                            DataTableINST_TX_DATA.Columns.Add("ACQ_BANK_NBR", typeof(string));
                            DataTableINST_TX_DATA.Columns.Add("POS_NBR", typeof(string));
                            DataTableINST_TX_DATA.Columns.Add("SEQ", typeof(string));
                            DataTableINST_TX_DATA.Columns.Add("MT_TYPE", typeof(string));
                            DataTableINST_TX_DATA.Columns.Add("CODE", typeof(string));
                            DataTableINST_TX_DATA.Columns.Add("POSTING_DTE", typeof(DateTime));
                            DataTableINST_TX_DATA.Columns.Add("STMT_DTE", typeof(DateTime));
                            #endregion
                            DataRow RowTX = DataTableINST_TX_DATA.NewRow();
                            RowTX["BU"] = resultTableFromTX_UNBILL.Rows[k + i]["BU"];
                            //卡號
                            RowTX["CARD_NBR"] = resultTableFromTX_UNBILL.Rows[k + i]["CARD_NBR"];
                            //消費日期
                            RowTX["EFF_DTE"] = resultTableFromTX_UNBILL.Rows[k + i]["EFF_DTE"];
                            //分期金額
                            RowTX["AMT"] = resultTableFromTX_UNBILL.Rows[k + i]["AMT"];
                            //消費摘要
                            RowTX["DESCR"] = resultTableFromTX_UNBILL.Rows[k + i]["DESCR"];
                            //授權碼
                            RowTX["AUTH_CODE"] = resultTableFromTX_UNBILL.Rows[k + i]["AUTH_CODE"];
                            //特店代號
                            RowTX["MER_NBR"] = resultTableFromTX_UNBILL.Rows[k + i]["MER_NBR"];
                            //微縮影編號
                            RowTX["ARN"] = resultTableFromTX_UNBILL.Rows[k + i]["ARN"];
                            //交易國別
                            RowTX["COUNTRY_CODE"] = resultTableFromTX_UNBILL.Rows[k + i]["COUNTRY_CODE"];
                            //交易城市別
                            RowTX["CITY_CODE"] = resultTableFromTX_UNBILL.Rows[k + i]["CITY_CODE"];
                            //原始交易幣別
                            RowTX["ORIG_CURRENCY"] = resultTableFromTX_UNBILL.Rows[k + i]["ORIG_CURRENCY"];
                            //原始交易金額
                            RowTX["ORIG_AMT"] = resultTableFromTX_UNBILL.Rows[k + i]["ORIG_AMT"];
                            //約當台幣金額
                            RowTX["USER_AMT_4"] = resultTableFromTX_UNBILL.Rows[k + i]["USER_AMT_4"];
                            //收單行
                            RowTX["ACQ_BANK_NBR"] = resultTableFromTX_UNBILL.Rows[k + i]["ACQ_BANK_NBR"];
                            //終端機代號
                            RowTX["POS_NBR"] = resultTableFromTX_UNBILL.Rows[k + i]["POS_NBR"];
                            //序號
                            RowTX["SEQ"] = resultTableFromTX_UNBILL.Rows[k + i]["SEQ"];
                            //借貸別
                            RowTX["MT_TYPE"] = resultTableFromTX_UNBILL.Rows[k + i]["MT_TYPE"];
                            //交易碼
                            RowTX["CODE"] = resultTableFromTX_UNBILL.Rows[k + i]["CODE"];
                            //入帳日期
                            RowTX["POSTING_DTE"] = resultTableFromTX_UNBILL.Rows[k + i]["POSTING_DTE"];
                            //帳單日期
                            RowTX["STMT_DTE"] = resultTableFromTX_UNBILL.Rows[k + i]["STMT_DTE"];
                            DataTableINST_TX_DATA.Rows.Add(RowTX);
                            Session["DataTableINST_TX_DATA"] = DataTableINST_TX_DATA;//分期建檔使用
                            return isFound;
                        }
                        #endregion

                        #region 資料查詢
                        Cybersoft.Data.DAL.TX_UNBILLDao TX_UNBILL = new Cybersoft.Data.DAL.TX_UNBILLDao();

                        TX_UNBILL.init();
                        //事業單位
                        TX_UNBILL.strWhereBU = Convert.ToString(resultTableFromTX_UNBILL.Rows[k + i]["BU"]);
                        //帳號
                        TX_UNBILL.strWhereACCT_NBR = Convert.ToString(resultTableFromTX_UNBILL.Rows[k + i]["ACCT_NBR"]);
                        //序號
                        TX_UNBILL.strWhereSEQ = Convert.ToString(resultTableFromTX_UNBILL.Rows[k + i]["SEQ"]);
                        //入帳日期
                        TX_UNBILL.DateTimeWherePOSTING_DTE = Convert.ToDateTime(resultTableFromTX_UNBILL.Rows[k + i]["POSTING_DTE"]);

                        string strResultFromTX_UNBILL = TX_UNBILL.query_by_tx_bill("Y", "Y", "Y");
                        //紀錄資料存取物件:DB Log@@@
                        sessionControl.dOP = TX_UNBILL.SelectOperator;

                        switch (strResultFromTX_UNBILL)
                        {
                            case "S0000": //成功
                                resultTableFromTX_UNBILL = TX_UNBILL.resultTable;
                                Session["DataTableTX_UNBILL"] = resultTableFromTX_UNBILL;
                                #region 手動取得MCC CODE/CARD PRODUCT
                                resultTableFromTX_UNBILL.Columns.Add("MCC_CODE_DESCR", typeof(string));
                                resultTableFromTX_UNBILL.Columns.Add("CARD_PRODUCT_DESCR", typeof(string));
                                resultTableFromTX_UNBILL.Columns.Add("TYPE_CODE", typeof(string));
                                for (int j = 0; j < resultTableFromTX_UNBILL.Rows.Count; j++)
                                {
                                    //特店類型
                                    string MCC_CODE = Convert.ToString(resultTableFromTX_UNBILL.Rows[j]["MCC_CODE"]);
                                    resultTableFromTX_UNBILL.Rows[j]["MCC_CODE_DESCR"] = ControlTWA.GetMCC_DESCR(MCC_CODE);
                                    //產品別
                                    string CARD_PRODUCT = Convert.ToString(resultTableFromTX_UNBILL.Rows[j]["CARD_PRODUCT"]);
                                    resultTableFromTX_UNBILL.Rows[j]["CARD_PRODUCT_DESCR"] = ControlTWA.GetPRODUCT_DESCR(CARD_PRODUCT);
                                    //交易碼
                                    resultTableFromTX_UNBILL.Rows[j]["TYPE_CODE"] = Convert.ToString(resultTableFromTX_UNBILL.Rows[j]["MT_TYPE"]) + Convert.ToString(resultTableFromTX_UNBILL.Rows[j]["CODE"]).Substring(2, 2);
                                }
                                #endregion
                                break;
                            case "F0023": //查無資料
                                break;
                            default: //資料庫錯誤
                                break;
                        }
                        #endregion

                        PanelControl.setFieldsFromDB(this.PnlDetail, resultTableFromTX_UNBILL);
                        PanelControl.setFieldsFromDB(this.Panel1, resultTableFromTX_UNBILL);
                        PanelControl.setFieldsFromDB(this.Panel2, resultTableFromTX_UNBILL);
                        PanelControl.setFieldsFromDB(this.Panel3, resultTableFromTX_UNBILL);

                        //來源碼
                        this.txtSOURCE_CODE.Text = ControlTWA.GetTX_SOURCE_DESCR(this.txtSOURCE_CODE.Text) + "(" + this.txtSOURCE_CODE.Text + ")";
                        //消費類別
                        this.txtMCC_CODE.Text = ControlTWA.GetMCC_DESCR(this.txtMCC_CODE.Text) + "(" + this.txtMCC_CODE.Text + ")";
                        //國別說明
                        this.txtCOUNTRY_CODE.Text = ControlTWA.GetCOUNTRY_DESCR(this.txtCOUNTRY_CODE.Text) + "(" + this.txtCOUNTRY_CODE.Text + ")";
                        //分期期數
                        this.txtINST_TERM.Text = Convert.ToString(resultTableFromTX_UNBILL.Rows[0]["INST_TERM_POST"]) + "/" + txtINST_TERM.Text;
                        //計算匯率 
                        #region 計算匯率
                        DateTime datestart = new DateTime(1900, 1, 1);
                        if (Convert.ToDateTime(resultTableFromTX_UNBILL.Rows[0]["ORIG_DTE"]) > datestart)
                        {
                            decimal convRATE = Convert.ToDecimal(resultTableFromTX_UNBILL.Rows[0]["AMT"]) / Convert.ToDecimal(resultTableFromTX_UNBILL.Rows[0]["ORIG_AMT"]);
                            this.txtCONV.Text = Convert.ToDateTime(resultTableFromTX_UNBILL.Rows[0]["ORIG_DTE"]).ToString("yyyy/MM/dd") + " " + convRATE.ToString("###,##0.00");
                        }
                        #endregion
                        break;
                    }
                }
                return isFound;
            }
            catch (Exception e_page)
            {
                Msg.getMsg("F9999", System.Reflection.MethodInfo.GetCurrentMethod().Name, Master);
                logger.strJobQueue = e_page.ToString();
                logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
                return false;
            }
        }
        protected void itnDOWNLOAD_Click(object sender, System.EventArgs e)
        {
            try
            {
                formtag = true;
                StringWriter swriter = new StringWriter();
                HtmlTextWriter hwriter = new HtmlTextWriter(swriter);

                //取消分頁，即設定 GridView.AllowPaging=False
                this.gdvdata.AllowPaging = false;
                this.gdvdata.Columns[0].Visible = false;
                this.gdvdata.Columns[2].Visible = true;
                this.gdvdata.Columns[2].ItemStyle.Width = 120;
                this.gdvdata.Columns[3].Visible = true;
                this.gdvdata.Columns[3].ItemStyle.Width = 100;
                this.gdvdata.Columns[4].Visible = true;
                this.gdvdata.Columns[4].ItemStyle.Width = 60;
                DataTable resultTableFromTX_UNBILL = (DataTable)Session["DataTableTX_UNBILL"];
                this.gdvdata.DataSource = resultTableFromTX_UNBILL;
                this.gdvdata.DataBind();

                this.gdvdata.RenderControl(hwriter);
                Session["dataEXCEL"] = swriter.ToString();
                Session["DataDOWNLOAD"] = "EXCEL";

                //還原分頁，即設定 GridView.AllowPaging=True。 
                this.gdvdata.AllowPaging = true;
                this.gdvdata.Columns[0].Visible = true;
                this.gdvdata.Columns[2].Visible = false;
                this.gdvdata.Columns[3].Visible = false;
                this.gdvdata.Columns[4].Visible = false;
                this.gdvdata.DataSource = resultTableFromTX_UNBILL;
                this.gdvdata.DataBind();

                //開啟下載區域
                HtmlGenericControl divDownLoad = (HtmlGenericControl)this.Master.FindControl("divDownLoad");
                divDownLoad.Style["visibility"] = "visible";
                Label lblDownLoad = (Label)this.Master.FindControl("lblDownLoad");
                lblDownLoad.Text = this.Lang.Get("MSG.PAGE.DOWNLOAD.EXCEL");




            }
            catch (Exception e_page)
            {
                Msg.getMsg("F9999", System.Reflection.MethodInfo.GetCurrentMethod().Name, Master);
                logger.strJobQueue = e_page.ToString();
                logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
            }
        }
        protected void itnAdjust_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                //呼叫setDataFromDb讀取資料庫資料及顯示在畫面欄位上
                if (setDataFromDb("TRANSFER"))
                {
                    DataTable DataTableTX = (DataTable)Session["DataTableINST_TX_DATA"];
                    Session["DataTableTX"] = DataTableTX.Rows[0];
                    Response.Redirect("../TP/TPOADJ001.aspx", false);
                }
                else
                {
                    //隱藏畫面編緝區
                    this.PnlDetail.Visible = false;
                    //設定訊息區內容(請先選擇項目再按明細)
                    Msg.getMsg("I0023", System.Reflection.MethodInfo.GetCurrentMethod().Name, Master);
                }
            }
            catch (Exception e_page)
            {
                Msg.getMsg("F9999", System.Reflection.MethodInfo.GetCurrentMethod().Name, Master);
                logger.strJobQueue = e_page.ToString();
                logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
            }
        }
        protected void itnInst_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                //呼叫setDataFromDb讀取資料庫資料及顯示在畫面欄位上
                if (setDataFromDb("TRANSFER"))
                {
                    DataRow DataTableINST_TX_DATA = (DataRow)Session["DataTableINST_TX_DATA"];
                    //非一般消費，不可分期
                    if (DataTableINST_TX_DATA["CODE"].ToString() != "0001")
                    {
                        //隱藏畫面編緝區
                        this.PnlDetail.Visible = false;
                        //設定訊息區內容(請先選擇項目再按明細)
                        Msg.getMsg("I1038", System.Reflection.MethodInfo.GetCurrentMethod().Name, Master);
                    }
                    else
                    {
                        Response.Redirect("../TP/TPOISA001.aspx?FN_NAME=TR");
                    }
                }
                else
                {
                    //隱藏畫面編緝區
                    this.PnlDetail.Visible = false;
                    //設定訊息區內容(請先選擇項目再按明細)
                    Msg.getMsg("I0023", System.Reflection.MethodInfo.GetCurrentMethod().Name, Master);
                }
            }
            catch (Exception e_page)
            {
                Msg.getMsg("F9999", System.Reflection.MethodInfo.GetCurrentMethod().Name, Master);
                logger.strJobQueue = e_page.ToString();
                logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
            }
        }
        public override void VerifyRenderingInServerForm(Control control)
        {
            try
            {
                if (formtag)
                {

                }
                else
                {
                    base.VerifyRenderingInServerForm(control);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            try
            {
                Chart chtPieMcc = new Chart();
                chtPieMcc.ID = "chtPieMcc";
                chtPieMcc.Visible = false;
                chtPieMcc.EnableViewState = true;
                this.PnlContent.Controls.Add(chtPieMcc);

                Chart chtColumnMcc = new Chart();
                chtColumnMcc.ID = "chtColumnMcc";
                chtColumnMcc.Visible = false;
                chtColumnMcc.EnableViewState = true;
                this.PnlContent.Controls.Add(chtColumnMcc);

                Chart chtPieDate = new Chart();
                chtPieDate.ID = "chtPieDate";
                chtPieDate.Visible = false;
                chtPieDate.EnableViewState = true;
                this.PnlContent.Controls.Add(chtPieDate);

                Chart chtColumnDate = new Chart();
                chtColumnDate.ID = "chtColumnDate";
                chtColumnDate.Visible = false;
                chtColumnDate.EnableViewState = true;
                this.PnlContent.Controls.Add(chtColumnDate);

                ImageButton itnDownload2 = new ImageButton();
                itnDownload2.ID = "itnDownload2";
                itnDownload2.ImageUrl = "../../images/itn/itnDownload.jpg";
                itnDownload2.Attributes["onmouseover"] = "EvImageOverChange(this, 'in','../../images/itn/itnDownload(mouse).jpg');";
                itnDownload2.Attributes["onmouseout"] = "EvImageOverChange(this, 'out','" + itnDownload2.ImageUrl + "');";
                itnDownload2.Visible = false;
                itnDownload2.EnableViewState = true;
                itnDownload2.Click += itnDownload2_Click;
                this.PnlContent.Controls.Add(itnDownload2);
            }
            catch (Exception)
            {
                throw;
            }
        }

        //查詢分析圖表
        protected void itnAnalysis_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                Cybersoft.Data.DAL.TX_UNBILLDao TX_UNBILL = new Cybersoft.Data.DAL.TX_UNBILLDao();
                #region KEY值
                //事業單位
                #region 事業單位
                if (Convert.ToString(Session["keyBU"]) != "")
                {
                    TX_UNBILL.strWhereBU = Convert.ToString(Session["keyBU"]);
                }
                #endregion
                //產品碼
                #region 產品碼
                if (Convert.ToString(Session["keyPRODUCT"]) != "")
                {
                    TX_UNBILL.strWherePRODUCT = Convert.ToString(Session["keyPRODUCT"]);
                    if (Convert.ToString(Session["keyPRODUCT"]) != "000")
                    {
                        TX_UNBILL.strWhereCARD_PRODUCT = Convert.ToString(Session["keyPRODUCT"]);
                    }
                }
                #endregion
                //帳號
                #region 帳號
                if (Convert.ToString(Session["keyACCT_NBR"]) != "")
                {
                    TX_UNBILL.strWhereACCT_NBR = Convert.ToString(Session["keyACCT_NBR"]);
                }
                #endregion
                //卡號
                #region 卡號
                if (Convert.ToString(Session["keyCARD_NBR"]) != "")
                {
                    TX_UNBILL.strWhereCARD_NBR = Convert.ToString(Session["keyCARD_NBR"]);
                }
                if (Convert.ToString(Session["keyACCT_NBR"]) == "" && Convert.ToString(Session["keyCARD_NBR"]) == "")
                {
                    TX_UNBILL.strWhereBU = "000";
                    this.gdvdata.DataSource = null;
                    this.gdvdata.DataBind();
                    strResultFromTX_UNBILL = "F0023";
                    return;
                }
                #endregion
                //入帳日期
                #region 入帳日期
                if (this.txtwherePOSTING_DTE_FR.Text == "")
                {
                    this.txtwherePOSTING_DTE_FR.Text = DateTime.Now.AddYears(-1).ToString("yyyy/MM/dd");
                }
                TX_UNBILL.DateTimeWherePOSTING_DTE_FR = Convert.ToDateTime(txtwherePOSTING_DTE_FR.Text);
                if (this.txtwherePOSTING_DTE_TO.Text == "")
                {
                    DataTable SETUP_SYSTEM = (DataTable)System.Web.HttpContext.Current.Application["DataTableSYSTEM"];
                    this.txtwherePOSTING_DTE_TO.Text = Convert.ToDateTime(SETUP_SYSTEM.Rows[0]["NEXT_PROCESS_DTE"]).ToString("yyyy/MM/dd");
                }
                TX_UNBILL.DateTimeWherePOSTING_DTE_TO = Convert.ToDateTime(txtwherePOSTING_DTE_TO.Text);
                #endregion
                #endregion
                //圖表
                #region 圖表
                //Group by MCC_CODE
                #region Group by MCC_CODE
                strResultFromTX_UNBILL = TX_UNBILL.query_for_chart();
                if ("S0000".Equals(strResultFromTX_UNBILL))
                {
                    Session["DataTableCHART_MCC"] = TX_UNBILL.resultTable;
                    loadChartPieByMcc(TX_UNBILL.resultTable);
                    loadChartColumnByMcc(TX_UNBILL.resultTable);
                }
                else
                {
                    return;
                }
                #endregion
                //Group by POSTING_DTE
                #region Group by POSTING_DTE
                strResultFromTX_UNBILL = TX_UNBILL.query_for_chart2();
                if ("S0000".Equals(strResultFromTX_UNBILL))
                {
                    Session["DataTableCHART_POSTING_DTE"] = TX_UNBILL.resultTable;
                    loadChartPieByDate(TX_UNBILL.resultTable);
                    loadChartColumnByDate(TX_UNBILL.resultTable);
                }
                else
                {
                    return;
                }
                #endregion
                #endregion
                //下載按鈕
                #region 下載按鈕
                ImageButton itnDownload2 = this.PnlContent.FindControl("itnDownload2") as ImageButton;
                if (itnDownload2 == null)
                {
                    return;
                }
                itnDownload2.Visible = true;
                #endregion
            }
            catch (Exception e_page)
            {
                logger.strJobQueue = e_page.ToString();
                logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
            }
        }

        //消費金額圓餅圖Group by POSTING_DTE
        private void loadChartPieByDate(DataTable dt)
        {
            try
            {
                //取得Chart
                #region 取得Chart
                Chart chtPieDate = this.PnlContent.FindControl("chtPieDate") as Chart;
                if (chtPieDate == null)
                {
                    return;
                }
                chtPieDate.ChartAreas.Clear();
                chtPieDate.Series.Clear();
                chtPieDate.Titles.Clear();
                #endregion
                //圖表區域集合
                chtPieDate.ChartAreas.Add("ChartArea1");
                //數據序列集合
                chtPieDate.Series.Add("Series1");
                //設定Chart
                #region 設定Chart
                chtPieDate.Width = 478;
                chtPieDate.Height = 400;
                Title Title1 = new Title();
                decimal Dec = Convert.ToDecimal(dt.Compute("SUM(TTL_AMT)", ""));
                Title1.Text = this.Lang.Get("TTL_AMT") + "($" + Dec.ToString(Convert.ToString(Session["SessionAMT_FORMAT"])) + ")";
                Title1.Alignment = ContentAlignment.MiddleCenter;
                Title1.Font = new System.Drawing.Font("Trebuchet MS", 14F, FontStyle.Bold);
                chtPieDate.Titles.Add(Title1);
                #endregion
                //設定ChartArea1
                #region 設定ChartArea1
                chtPieDate.ChartAreas[0].Area3DStyle.Enable3D = true;
                chtPieDate.ChartAreas[0].AxisX.Interval = 1;
                #endregion
                //設定Series1
                #region 設定Series1
                chtPieDate.Series[0].ChartType = SeriesChartType.Pie;
                chtPieDate.Series[0].Points.DataBindXY(dt.DefaultView, "POST_ACTION.S", dt.DefaultView, "TTL_AMT");
                chtPieDate.Series[0].Label = "#VALX\n$#VALY{##,###,###,##0}\n#PERCENT{P1}";
                //字體設定
                chtPieDate.Series[0].Font = new System.Drawing.Font("Trebuchet MS", 8);
                chtPieDate.Series[0].Points.FindMaxByValue().LabelForeColor = System.Drawing.Color.Red;
                chtPieDate.Series[0].BorderColor = System.Drawing.Color.FromArgb(255, 101, 101, 101);
                //數值顯示在圓餅外
                chtPieDate.Series[0]["PieLabelStyle"] = "Outside";
                //設定圓餅效果，
                chtPieDate.Series[0]["PieDrawingStyle"] = "Default";
                #endregion
                chtPieDate.Visible = true;
            }
            catch (Exception e_page)
            {
                logger.strJobQueue = e_page.ToString();
                logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
            }
        }

        //消費金額長條圖Group by POSTING_DTE
        private void loadChartColumnByDate(DataTable dt)
        {
            try
            {
                //取得Chart
                #region 取得Chart
                Chart chtColumnDate = this.PnlContent.FindControl("chtColumnDate") as Chart;
                if (chtColumnDate == null)
                {
                    return;
                }
                chtColumnDate.ChartAreas.Clear();
                chtColumnDate.Series.Clear();
                chtColumnDate.Titles.Clear();
                #endregion
                string[] XValue = dt.AsEnumerable().Select(row => row.Field<string>("POST_ACTION.S")).ToArray();
                decimal[] YValue = dt.AsEnumerable().Select(row => row.Field<decimal>("TTL_AMT")).ToArray();
                //圖表區域集合
                chtColumnDate.ChartAreas.Add("ChartArea1");
                //數據序列集合
                chtColumnDate.Series.Add("Series1");
                //設定Chart
                #region 設定Chart
                chtColumnDate.Width = 478;
                chtColumnDate.Height = 400;
                Title Title1 = new Title();
                decimal Dec = Convert.ToDecimal(dt.Compute("SUM(TTL_AMT)", ""));
                Title1.Text = this.Lang.Get("TTL_AMT") + "($" + Dec.ToString(Convert.ToString(Session["SessionAMT_FORMAT"])) + ")";
                Title1.Alignment = ContentAlignment.MiddleCenter;
                Title1.Font = new System.Drawing.Font("Trebuchet MS", 14F, FontStyle.Bold);
                chtColumnDate.Titles.Add(Title1);
                #endregion
                //設定ChartArea
                #region 設定ChartArea
                chtColumnDate.ChartAreas[0].AxisX.LabelAutoFitStyle = LabelAutoFitStyles.LabelsAngleStep30;
                chtColumnDate.ChartAreas[0].AxisY.LabelStyle.Format = "##,###,###,##0";
                chtColumnDate.ChartAreas[0].AxisX2.Enabled = AxisEnabled.False; //隱藏 X2 標示
                chtColumnDate.ChartAreas[0].AxisY2.Enabled = AxisEnabled.False; //隱藏 Y2 標示
                chtColumnDate.ChartAreas[0].AxisY2.MajorGrid.Enabled = false;   //隱藏 Y2 軸線
                chtColumnDate.ChartAreas[0].AxisX.MajorGrid.Enabled = false;   //隱藏 X 軸線
                chtColumnDate.ChartAreas[0].AxisY.MajorGrid.Enabled = false;   //隱藏 Y 軸線
                #endregion
                //設定Series
                #region 設定Series
                chtColumnDate.Series[0].ChartType = SeriesChartType.Column; //橫條圖
                chtColumnDate.Series[0].Points.DataBindXY(XValue, YValue);
                Random Rnd = new Random();
                for (int i = 0; i < XValue.Length; i++)
                {
                    chtColumnDate.Series[0].Points[i].Color = System.Drawing.Color.FromArgb(Rnd.Next(0, 256), Rnd.Next(0, 256), Rnd.Next(0, 256));
                }
                chtColumnDate.Series[0].LabelFormat = "$" + "##,###,###,##0"; //金錢格式
                chtColumnDate.Series[0].LabelAngle = 45;
                chtColumnDate.Series[0].MarkerSize = 18; //Label 範圍大小
                chtColumnDate.Series[0].LabelForeColor = System.Drawing.Color.Black; //字體顏色
                chtColumnDate.Series[0].Font = new System.Drawing.Font("Trebuchet MS", 8);
                chtColumnDate.Series[0].IsValueShownAsLabel = true; // Show data points labels
                chtColumnDate.Series[0].BorderColor = System.Drawing.Color.FromArgb(255, 101, 101, 101);
                #endregion
                chtColumnDate.Visible = true;
            }
            catch (Exception e_page)
            {
                logger.strJobQueue = e_page.ToString();
                logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
            }
        }

        //消費金額圓餅圖Group by MCC_CODE
        private void loadChartPieByMcc(DataTable dt)
        {
            try
            {
                //取得Chart
                #region 取得Chart
                Chart chtPieMcc = this.PnlContent.FindControl("chtPieMcc") as Chart;
                if (chtPieMcc == null)
                {
                    return;
                }
                chtPieMcc.ChartAreas.Clear();
                chtPieMcc.Series.Clear();
                chtPieMcc.Titles.Clear();
                #endregion
                //圖表區域集合
                chtPieMcc.ChartAreas.Add("ChartArea1");
                //數據序列集合
                chtPieMcc.Series.Add("Series1");
                //設定Chart
                #region 設定Chart
                chtPieMcc.Width = 478;
                chtPieMcc.Height = 400;
                Title Title1 = new Title();
                decimal Dec = Convert.ToDecimal(dt.Compute("SUM(TTL_AMT)", ""));
                Title1.Text = this.Lang.Get("TTL_AMT") + "($" + Dec.ToString(Convert.ToString(Session["SessionAMT_FORMAT"])) + ")";
                Title1.Alignment = ContentAlignment.MiddleCenter;
                Title1.Font = new System.Drawing.Font("Trebuchet MS", 14F, FontStyle.Bold);
                chtPieMcc.Titles.Add(Title1);
                #endregion
                //設定ChartArea1
                #region 設定ChartArea1
                chtPieMcc.ChartAreas[0].Area3DStyle.Enable3D = true;
                chtPieMcc.ChartAreas[0].AxisX.Interval = 1;
                #endregion
                //設定Series1
                #region 設定Series1
                chtPieMcc.Series[0].ChartType = SeriesChartType.Pie;
                chtPieMcc.Series[0].Points.DataBindXY(dt.DefaultView, "DESCR", dt.DefaultView, "TTL_AMT");
                chtPieMcc.Series[0].Label = "#VALX\n$#VALY{##,###,###,##0}\n#PERCENT{P1}";
                //字體設定
                chtPieMcc.Series[0].Font = new System.Drawing.Font("Trebuchet MS", 8);
                chtPieMcc.Series[0].Points.FindMaxByValue().LabelForeColor = System.Drawing.Color.Red;
                chtPieMcc.Series[0].BorderColor = System.Drawing.Color.FromArgb(255, 101, 101, 101);
                //數值顯示在圓餅外
                chtPieMcc.Series[0]["PieLabelStyle"] = "Outside";
                //設定圓餅效果，
                chtPieMcc.Series[0]["PieDrawingStyle"] = "Default";
                #endregion
                chtPieMcc.Visible = true;
            }
            catch (Exception e_page)
            {
                logger.strJobQueue = e_page.ToString();
                logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
            }
        }

        //消費金額長條圖Group by MCC_CODE
        private void loadChartColumnByMcc(DataTable dt)
        {
            try
            {
                //取得Chart
                #region 取得Chart
                Chart chtColumnMcc = this.PnlContent.FindControl("chtColumnMcc") as Chart;
                if (chtColumnMcc == null)
                {
                    return;
                }
                chtColumnMcc.ChartAreas.Clear();
                chtColumnMcc.Series.Clear();
                chtColumnMcc.Titles.Clear();
                #endregion
                string[] XValue = dt.AsEnumerable().Select(row => row.Field<string>("DESCR")).ToArray();
                decimal[] YValue = dt.AsEnumerable().Select(row => row.Field<decimal>("TTL_AMT")).ToArray();
                //圖表區域集合
                chtColumnMcc.ChartAreas.Add("ChartArea1");
                //數據序列集合
                chtColumnMcc.Series.Add("Series1");
                //設定Chart
                #region 設定Chart
                chtColumnMcc.Width = 478;
                chtColumnMcc.Height = 400;
                Title Title1 = new Title();
                decimal Dec = Convert.ToDecimal(dt.Compute("SUM(TTL_AMT)", ""));
                Title1.Text = this.Lang.Get("TTL_AMT") + "($" + Dec.ToString(Convert.ToString(Session["SessionAMT_FORMAT"])) + ")";
                Title1.Alignment = ContentAlignment.MiddleCenter;
                Title1.Font = new System.Drawing.Font("Trebuchet MS", 14F, FontStyle.Bold);
                chtColumnMcc.Titles.Add(Title1);
                #endregion
                //設定ChartArea
                #region 設定ChartArea
                chtColumnMcc.ChartAreas[0].AxisX.LabelAutoFitStyle = LabelAutoFitStyles.LabelsAngleStep30;
                chtColumnMcc.ChartAreas[0].AxisY.LabelStyle.Format = "##,###,###,##0";
                chtColumnMcc.ChartAreas[0].AxisX2.Enabled = AxisEnabled.False; //隱藏 X2 標示
                chtColumnMcc.ChartAreas[0].AxisY2.Enabled = AxisEnabled.False; //隱藏 Y2 標示
                chtColumnMcc.ChartAreas[0].AxisY2.MajorGrid.Enabled = false;   //隱藏 Y2 軸線
                chtColumnMcc.ChartAreas[0].AxisX.MajorGrid.Enabled = false;   //隱藏 X 軸線
                chtColumnMcc.ChartAreas[0].AxisY.MajorGrid.Enabled = false;   //隱藏 Y 軸線
                #endregion
                //設定Series
                #region 設定Series
                chtColumnMcc.Series[0].ChartType = SeriesChartType.Column; //橫條圖
                chtColumnMcc.Series[0].Points.DataBindXY(XValue, YValue);
                Random Rnd = new Random();
                for (int i = 0; i < XValue.Length; i++)
                {
                    chtColumnMcc.Series[0].Points[i].Color = System.Drawing.Color.FromArgb(Rnd.Next(0, 256), Rnd.Next(0, 256), Rnd.Next(0, 256));
                }
                chtColumnMcc.Series[0].LabelFormat = "$" + "##,###,###,##0"; //金錢格式
                chtColumnMcc.Series[0].LabelAngle = 45;
                chtColumnMcc.Series[0].MarkerSize = 18; //Label 範圍大小
                chtColumnMcc.Series[0].LabelForeColor = System.Drawing.Color.Black; //字體顏色
                chtColumnMcc.Series[0].Font = new System.Drawing.Font("Trebuchet MS", 8);
                chtColumnMcc.Series[0].IsValueShownAsLabel = true; // Show data points labels
                chtColumnMcc.Series[0].BorderColor = System.Drawing.Color.FromArgb(255, 101, 101, 101);
                #endregion
                chtColumnMcc.Visible = true;
            }
            catch (Exception e_page)
            {
                logger.strJobQueue = e_page.ToString();
                logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
            }
        }

        //圖表下載
        protected void itnDownload2_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                //PDF設定
                #region PAGE設定
                pdfPage PPage = new pdfPage();
                Document Doc = new Document(PageSize.A4, -50, -50, 100, 50);
                MemoryStream MStreamPdf = new MemoryStream();
                PdfWriter PWriter = PdfWriter.GetInstance(Doc, MStreamPdf);
                PWriter.ViewerPreferences = PdfWriter.PageLayoutSinglePage;
                PPage.reportNAME = this.Lang.Get("TX_BILL") + " "
                                 + this.Lang.Get("POSTING_DTE") + ":" + this.txtwherePOSTING_DTE_FR.Text + "~" + this.txtwherePOSTING_DTE_TO.Text;
                PWriter.PageEvent = PPage;
                Paragraph PBlank = new Paragraph(" ", PPage.detail_header);
                PBlank.Leading = 25;
                Doc.Open();
                #endregion
                //MCC_CODE
                #region MCC_CODE
                //TABLE設定
                #region TABLE設定
                PdfPTable PPTableMcc = new PdfPTable(new float[] { 1, 3, 3, 3 });
                PPTableMcc.HorizontalAlignment = Element.ALIGN_CENTER;
                PPTableMcc.DefaultCell.VerticalAlignment = Element.ALIGN_MIDDLE;
                PPTableMcc.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;
                #endregion
                //取得來源
                #region 取得來源
                DataTable DataTableChartMcc = Session["DataTableCHART_MCC"] as DataTable;
                #endregion
                //表頭
                #region 表頭
                for (int Index = 0; Index < DataTableChartMcc.Columns.Count; Index++)
                {
                    PPTableMcc.AddCell(new Phrase(this.Lang.Get(DataTableChartMcc.Columns[Index].ColumnName), PPage.detail_header));
                }
                PPTableMcc.HeaderRows = 1;
                #endregion
                //內容
                #region 內容
                for (int i = 0; i < DataTableChartMcc.Rows.Count; i++)
                {
                    for (int j = 0; j < DataTableChartMcc.Columns.Count; j++)
                    {
                        PPTableMcc.AddCell(new Phrase(Convert.ToString(DataTableChartMcc.Rows[i][j]), PPage.detail_body));
                    }
                }
                #endregion
                Doc.Add(PPTableMcc);
                //圖表
                #region 圖表
                //取得Chart
                #region 取得Chart
                Chart chtPieMcc = this.PnlContent.FindControl("chtPieMcc") as Chart;
                if (chtPieMcc == null)
                {
                    return;
                }
                Chart chtColumnMcc = this.PnlContent.FindControl("chtColumnMcc") as Chart;
                if (chtColumnMcc == null)
                {
                    return;
                }
                #endregion
                //消費金額圓餅圖Group by MCC_CODE
                #region 消費金額圓餅圖Group by MCC_CODE
                MemoryStream MStreamPieMcc = new MemoryStream();
                chtPieMcc.SaveImage(MStreamPieMcc);
                byte[] BufPieMcc = MStreamPieMcc.ToArray();
                iTextSharp.text.Image ImgPieMcc = iTextSharp.text.Image.GetInstance(BufPieMcc);
                ImgPieMcc.Alignment = iTextSharp.text.Image.MIDDLE_ALIGN;
                #endregion
                //消費金額長條圖Group by MCC_CODE
                #region 消費金額長條圖Group by MCC_CODE
                MemoryStream MStreamColumnMcc = new MemoryStream();
                chtColumnMcc.SaveImage(MStreamColumnMcc);
                byte[] BufColumnMcc = MStreamColumnMcc.ToArray();
                iTextSharp.text.Image ImgColumnMcc = iTextSharp.text.Image.GetInstance(BufColumnMcc);
                ImgColumnMcc.Alignment = iTextSharp.text.Image.MIDDLE_ALIGN;
                #endregion
                PdfPTable PPTableMcc1 = new PdfPTable(2);
                PPTableMcc1.AddCell(ImgPieMcc);
                PPTableMcc1.AddCell(ImgColumnMcc);
                #endregion
                Doc.Add(PPTableMcc1);
                #endregion
                Doc.Add(PBlank);
                //POSTING_DTE
                #region POSTING_DTE
                //TABLE設定
                #region TABLE設定
                PdfPTable PPTableDate = new PdfPTable(new float[] { 1, 3, 3, 3 });
                PPTableDate.HorizontalAlignment = Element.ALIGN_CENTER;
                PPTableDate.DefaultCell.VerticalAlignment = Element.ALIGN_MIDDLE;
                PPTableDate.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;
                #endregion
                //取得來源
                #region 取得來源
                DataTable DataTableChartPosting_Dte = Session["DataTableCHART_POSTING_DTE"] as DataTable;
                #endregion
                //表頭
                #region 表頭
                for (int Index = 0; Index < DataTableChartPosting_Dte.Columns.Count; Index++)
                {
                    PPTableDate.AddCell(new Phrase(this.Lang.Get(DataTableChartPosting_Dte.Columns[Index].ColumnName), PPage.detail_header));
                }
                PPTableDate.HeaderRows = 1;
                #endregion
                //內容
                #region 內容
                for (int i = 0; i < DataTableChartPosting_Dte.Rows.Count; i++)
                {
                    for (int j = 0; j < DataTableChartPosting_Dte.Columns.Count; j++)
                    {
                        PPTableDate.AddCell(new Phrase(Convert.ToString(DataTableChartPosting_Dte.Rows[i][j]), PPage.detail_body));
                    }
                }
                #endregion
                Doc.Add(PPTableDate);
                //圖表
                #region 圖表
                //取得Chart
                #region 取得Chart
                Chart chtPieDate = this.PnlContent.FindControl("chtPieDate") as Chart;
                if (chtPieDate == null)
                {
                    return;
                }
                Chart chtColumnDate = this.PnlContent.FindControl("chtColumnDate") as Chart;
                if (chtColumnDate == null)
                {
                    return;
                }
                #endregion
                //消費金額圓餅圖Group by POSTING_DTE
                #region 消費金額圓餅圖Group by POSTING_DTE
                MemoryStream MStreamPieDate = new MemoryStream();
                chtPieDate.SaveImage(MStreamPieDate);
                byte[] BufPieDate = MStreamPieDate.ToArray();
                iTextSharp.text.Image ImgPieDate = iTextSharp.text.Image.GetInstance(BufPieDate);
                ImgPieDate.Alignment = iTextSharp.text.Image.MIDDLE_ALIGN;
                #endregion
                //消費金額長條圖Group by POSTING_DTE
                #region 消費金額長條圖Group by POSTING_DTE
                MemoryStream MStreamColumnDate = new MemoryStream();
                chtColumnDate.SaveImage(MStreamColumnDate);
                byte[] BufColumnDate = MStreamColumnDate.ToArray();
                iTextSharp.text.Image ImgColumnDate = iTextSharp.text.Image.GetInstance(BufColumnDate);
                ImgColumnDate.Alignment = iTextSharp.text.Image.MIDDLE_ALIGN;
                #endregion
                PdfPTable PPTableDate1 = new PdfPTable(2);
                PPTableDate1.AddCell(ImgPieDate);
                PPTableDate1.AddCell(ImgColumnDate);
                #endregion
                Doc.Add(PPTableDate1);
                #endregion

                Session["dataPDF"] = MStreamPdf;
                Session["DataDOWNLOAD"] = "PDF";
                Doc.Close();
                PWriter.Close();
                MStreamPdf.Close();
                MStreamColumnMcc.Close();

                //開啟下載區域
                HtmlGenericControl divDownLoad = (HtmlGenericControl)this.Master.FindControl("divDownLoad");
                divDownLoad.Style["visibility"] = "visible";
                Label lblDownLoad = (Label)this.Master.FindControl("lblDownLoad");
                lblDownLoad.Text = this.Lang.Get("MSG.PAGE.DOWNLOAD.PDF");
            }
            catch (Exception e_page)
            {
                logger.strJobQueue = e_page.ToString();
                logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
            }
        }

        //隱藏Page_Init新增控制項
        private void visibleControl()
        {
            try
            {
                Chart chtPieMcc = this.PnlContent.FindControl("chtPieMcc") as Chart;
                if (chtPieMcc != null)
                {
                    chtPieMcc.Visible = false;
                }
                Chart chtColumnMcc = this.PnlContent.FindControl("chtColumnMcc") as Chart;
                if (chtColumnMcc != null)
                {
                    chtColumnMcc.Visible = false;
                }
                Chart chtPieDate = this.PnlContent.FindControl("chtPieDate") as Chart;
                if (chtPieDate != null)
                {
                    chtPieDate.Visible = false;
                }
                Chart chtColumnDate = this.PnlContent.FindControl("chtColumnDate") as Chart;
                if (chtColumnDate != null)
                {
                    chtColumnDate.Visible = false;
                }
                ImageButton itnDownload2 = this.PnlContent.FindControl("itnDownload2") as ImageButton;
                if (itnDownload2 != null)
                {
                    itnDownload2.Visible = false;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected override void OnPreInit(EventArgs e)
        {
            MasterPageFile = MasterPageVirtualPathProvider.MasterPageFileLocation;
            base.OnPreInit(e);
            initSignOn.run();
        }
    }
}