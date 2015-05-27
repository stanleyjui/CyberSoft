using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using CARDWeb;
using CARDWeb.CARD.src;
using CARDWeb.CARD.usercontrols;


namespace CyberTestWeb.src
{
	/// <summary>
	/// PAOREI001,續卡參數設定畫面
	/// </summary>
	public partial class PAOREI001 : Cybersoft.Web.UI.LanguagePage
	{
		static readonly Cybersoft.Coca.Log.Logger logger = Cybersoft.Log.LogManager<Cybersoft.Coca.Log.Logger>.GetLogger(System.Reflection.MethodInfo.GetCurrentMethod().ReflectedType.Name);
		//@@放行異動1
		protected void Page_Init(object sender, EventArgs e)
		{
			try
			{
				//@@@放行
				approveControl.AddUserControl(this.PnlDetail, this.Page);
				//@@@放行
			}
			catch (Exception)
			{
				throw;
			}
		}
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
                    //紀錄資料存取物件:DB Log@@@
                    Msg.writeQueryLog("", "list", "gridview");
					Page.Form.Attributes.Add("enctype", "multipart/form-data");
				}
				PostBackTrigger PBTrigger = new PostBackTrigger();
				PBTrigger.ControlID = this.itnSave.UniqueID;
				UpdatePanel UPanel = this.Master.FindControl("uplcontent") as UpdatePanel;
				UPanel.Triggers.Add(PBTrigger);
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
					PanelControl.setInitFields(this.PnlContent);
				}
				else           //清除PnlDetail
				{
					PanelControl.setInitFields(this.PnlDetail);
					PanelControl.setInitFields(this.Panel1);
					PanelControl.setInitFields(this.Panel2);
				}
			}
			catch (Exception e_page)
			{
				logger.strJobQueue = e_page.ToString();
				logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
			}
		}

		//@@放行異動2
		private void loadGridData()
		{
			try
			{
				#region 呼叫SETUP_REISSUEDao
                Cybersoft.Data.DAL.SETUP_REISSUEDao SETUP_REISSUE = new Cybersoft.Data.DAL.SETUP_REISSUEDao();
                DataTable resultTableFromSETUP_REISSUE = new DataTable();
				//@@@放行
				//清除初始值
				Session["DataTableMNT_TODAY_APPROVE"] = null;
				#region 由其他頁面轉址的明細頁面所帶的查詢條件
				//此處判斷RedirectDetail為"Y"即為其他頁面轉址進入
				if ("Y".Equals(Convert.ToString(Session["SessionRedirectDetail"])))
                {
                    #region 取得SETUP_REISSUE所需的KEY

					#endregion
					//清除Session
					Session["SessionRedirectDetail"] = "";
				}
				#endregion
				//@@@放行
                string strResultFromSETUP_REISSUE = SETUP_REISSUE.query();
                //紀錄資料存取物件:DB Log@@@
                sessionControl.dOP = SETUP_REISSUE.SelectOperator;
                switch (strResultFromSETUP_REISSUE)
				{
					case "S0000": //成功
                        resultTableFromSETUP_REISSUE = SETUP_REISSUE.resultTable;
                        resultTableFromSETUP_REISSUE.Columns.Add("REVIEW_TYPE_DESCR", typeof(string));
                        #region 編輯欄位
                        for (int j = 0; j < resultTableFromSETUP_REISSUE.Rows.Count; j++)
                        {
                            string REVIEW_TYPE = Convert.ToString(resultTableFromSETUP_REISSUE.Rows[j]["REVIEW_TYPE"]);
                            //續卡覆審類別
                            resultTableFromSETUP_REISSUE.Rows[j]["REVIEW_TYPE_DESCR"] = this.Lang.Get("REVIEW_TYPE." + REVIEW_TYPE);
                        }
                        #endregion
						//暫存資料
                        Session["DataTableSETUP_REISSUE"] = resultTableFromSETUP_REISSUE;
						//將Grid欄位設為資料庫值
                        this.gdvdata.DataSource = resultTableFromSETUP_REISSUE;
						this.gdvdata.DataBind();
						break;
					case "F0023": //查無資料
						this.gdvdata.DataSource = null;
						this.gdvdata.DataBind();

						return;
					default: //資料庫錯誤
						break;
				}
				#endregion
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
				Msg.getMsg("S0000", System.Reflection.MethodInfo.GetCurrentMethod().Name, Master);
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
				initData(1);
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
					//設定欄位Enable屬性
					PanelControl.setFieldsEnable(this.PnlDetail, true);
					PanelControl.setFieldsEnable(this.Panel1, true);
					PanelControl.setFieldsEnable(this.Panel2, true);
					//設定按鍵屬性
					PanelControl.setButtonEnable(this.PnlDetail, System.Reflection.MethodInfo.GetCurrentMethod().Name);
					//設定主要鍵欄位不能修改
                    this.ddlREVIEW_TYPE.Enabled = false;
                    PanelControl.setTextBoxEnable(this.txtREVIEW_DAY, false);
					//設定游標位置
					this.txtDESCR.Focus();
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
		protected void itnInsert_Click(object sender, ImageClickEventArgs e)
		{
			try
			{
				Session["SessionMode"] = "INSERT_MODE";
				//隱藏查詢區
				this.PnlContent.Visible = false;
				//顯示明細畫面
				this.PnlDetail.Visible = true;
				//設定欄位Enable屬性
				PanelControl.setFieldsEnable(this.PnlDetail, true);
				PanelControl.setFieldsEnable(this.Panel1, true);
				PanelControl.setFieldsEnable(this.Panel2, true);
				//設定按鍵屬性
				PanelControl.setButtonEnable(this.PnlDetail, System.Reflection.MethodInfo.GetCurrentMethod().Name);
				//呼叫initData
				initData(1);
				//設定游標位置
                this.ddlREVIEW_TYPE.Focus();
				//設定訊息區內容
				Msg.getMsg("MODE", System.Reflection.MethodInfo.GetCurrentMethod().Name, Master);


			}
			catch (Exception e_page)
			{
				Msg.getMsg("F9999", System.Reflection.MethodInfo.GetCurrentMethod().Name, Master);
				logger.strJobQueue = e_page.ToString();
				logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
			}
		}
		protected void itnCopy_Click(object sender, ImageClickEventArgs e)
		{
			try
			{
				Session["SessionMode"] = "COPY_MODE";
				//呼叫setDataFromDb讀取資料庫資料及顯示在畫面欄位上
				if (setDataFromDb("insert"))
				{
					//隱藏查詢區
					this.PnlContent.Visible = false;
					//設定欄位Enable屬性
					PanelControl.setFieldsEnable(this.PnlDetail, true);
					PanelControl.setFieldsEnable(this.Panel1, true);
					PanelControl.setFieldsEnable(this.Panel2, true);
					//設定按鍵屬性
					PanelControl.setButtonEnable(this.PnlDetail, System.Reflection.MethodInfo.GetCurrentMethod().Name);
					//異動人員.時間及鍵值不"COPY_MODE"
					this.lblmnt_user.Text = "";
					this.lblmnt_dt.Text = "";
                    this.txtREVIEW_DAY.Text = "";
					//設定游標位置
                    this.ddlREVIEW_TYPE.Focus();
					//設定訊息區內容
					Msg.getMsg("MODE", System.Reflection.MethodInfo.GetCurrentMethod().Name, Master);

				}
				else
				{
					//隱藏畫面編緝區
					this.PnlDetail.Visible = false;
					//設定訊息區內容(請先選擇項目再按"COPY_MODE")
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
		
		//@@放行異動3
		protected void itnSave_Click(object sender, ImageClickEventArgs e)
		{
			try
			{
				#region 確定頁面通過檢查後才往下
				if (this.Page.IsValid == false)
				{
					return;
				}
                string strCheckResult = FieldsCheck();
                if (!"".Equals(strCheckResult))
                {
                    Msg.getMsg(strCheckResult, System.Reflection.MethodInfo.GetCurrentMethod().Name, Master);
                    return;
                }
				#endregion
				#region 初始處理
                Cybersoft.Data.DAL.SETUP_REISSUEDao SETUP_REISSUE = new Cybersoft.Data.DAL.SETUP_REISSUEDao();
                DataTable resultTableFromSETUP_REISSUE = (DataTable)Session["DataTableSETUP_REISSUE"];
                SETUP_REISSUE.init();
				if (Convert.ToString(Session["SessionMode"]) == "COPY_MODE" ||
				   Convert.ToString(Session["SessionMode"]) == "INSERT_MODE")
				{
                    SETUP_REISSUE.initInsert();
				}
				#endregion
				//@@放行-S
				//確認刪除放行usercontrol之MNT_TODAY及SETUP_BONUS刪除
				if (!approve_Check())
				{
					return;
				}
				//@@放行-E
				#region 其他欄位
                SETUP_REISSUE.strREVIEW_TYPE = this.ddlREVIEW_TYPE.SelectedItem.Value;
                SETUP_REISSUE.strREVIEW_DAY = this.txtREVIEW_DAY.Text;
				SETUP_REISSUE.strDESCR = this.txtDESCR.Text;
                SETUP_REISSUE.decCARD_REVIEW_TERM = Convert.ToDecimal(this.ddlCARD_REVIEW_TERM.SelectedValue.Trim());
                SETUP_REISSUE.strCHECK_ACCT_CTLCODE = this.ddlCHECK_ACCT_CONTROLCODE.SelectedValue;
                SETUP_REISSUE.strCHECK_CARD_CTLCODE = this.ddlCHECK_CARD_CONTROLCODE.SelectedValue;
                SETUP_REISSUE.strCHECK_DELQ = this.ddlCHECK_DELQ.SelectedValue;
                if ("Y".Equals(this.ddlCHECK_DELQ.SelectedValue))
                {
                    if (!"".Equals(this.txtCHECK_DELQ_CNT.Text))
                    {
                        SETUP_REISSUE.strCHECK_DELQ_CNT = this.txtCHECK_DELQ_CNT.Text;
                    }
                    if (!"".Equals(this.txtCHECK_DELQ_STMT_BAL.Text))
                    {
                        SETUP_REISSUE.decCHECK_DELQ_STMT_BAL = Convert.ToDecimal(this.txtCHECK_DELQ_STMT_BAL.Text);
                    }
                }
                SETUP_REISSUE.strCHECK_LIMIT = this.ddlCHECK_LIMIT.SelectedValue;
                if ("Y".Equals(this.ddlCHECK_LIMIT.SelectedValue))
                {
                    if (!"".Equals(this.txtCHECK_CREDIT_LIMIT.Text))
                    {
                        SETUP_REISSUE.decCHECK_CREDIT_LIMIT = Convert.ToDecimal(this.txtCHECK_CREDIT_LIMIT.Text);
                    }
                }
                SETUP_REISSUE.strCHECK_CONSUME = this.ddlCHECK_CONSUME.SelectedValue;
                if ("Y".Equals(this.ddlCHECK_CONSUME.SelectedValue))
                {
                    if (!"".Equals(this.ddlCHECK_CONSUME_MONTH.SelectedValue))
                    {
                        SETUP_REISSUE.strCHECK_CONSUME_MONTH = this.ddlCHECK_CONSUME_MONTH.SelectedValue;
                    }
                    else
                    {
                        SETUP_REISSUE.strCHECK_CONSUME_MONTH = "12";
                    }
                }
                SETUP_REISSUE.strCHECK_FOREIGNER = this.ddlCHECK_FOREIGNER.SelectedValue;
                if ("Y".Equals(this.ddlCHECK_FOREIGNER.SelectedValue))
                {
                    if (!"".Equals(this.txtCHECK_FOREIGNER_NATIONALITY.Text))
                    {
                        SETUP_REISSUE.strCHECK_FOREIGNER_NATIONALITY = this.txtCHECK_FOREIGNER_NATIONALITY.Text;
                    }
                    else
                    {
                        SETUP_REISSUE.strCHECK_FOREIGNER_NATIONALITY = "TW";
                    }
                }
                SETUP_REISSUE.strCHECK_ARC_EXPIR = this.ddlCHECK_ARC_EXPIR.SelectedValue;
                if ("Y".Equals(this.ddlCHECK_ARC_EXPIR.SelectedValue))
                {
                    if (!"".Equals(this.ddlCHECK_ARC_EXPIR_MONTH.SelectedValue))
                    {
                        SETUP_REISSUE.strCHECK_ARC_EXPIR_MONTH = this.ddlCHECK_ARC_EXPIR_MONTH.SelectedValue;
                    }
                    else
                    {
                        SETUP_REISSUE.strCHECK_ARC_EXPIR_MONTH = "12";
                    }
                }
                SETUP_REISSUE.strCHECK_OPENED = this.ddlCHECK_OPENED.SelectedValue;
                SETUP_REISSUE.strCHECK_MAGNETIC_STRIPE = this.ddlCHECK_MAGNETIC_STRIPE.SelectedValue;
                SETUP_REISSUE.strVALID_FLAG = Convert.ToString(this.ddlVALID_FLAG.SelectedItem.Value);
				
				SETUP_REISSUE.strMNT_USER = Convert.ToString(Session["SessionAccount"]);
				SETUP_REISSUE.datetimeMNT_DT = System.DateTime.Now;
				#endregion
				#region key process

				#endregion
				#region 依目前模式進行資料存取
				string strResultFromSETUP_REISSUE = "";
				if (Convert.ToString(Session["SessionMode"]) == "COPY_MODE" ||
					Convert.ToString(Session["SessionMode"]) == "INSERT_MODE")
				{

					strResultFromSETUP_REISSUE = SETUP_REISSUE.insert(ControlTWA.SessionToHash());
                    //紀錄資料存取物件:DB Log@@@
                    sessionControl.dOP = SETUP_REISSUE.InsertOperator;
				}
				else
				{
                    SETUP_REISSUE.strWhereREVIEW_TYPE = Convert.ToString(resultTableFromSETUP_REISSUE.Rows[0]["REVIEW_TYPE"]);
                    SETUP_REISSUE.strWhereREVIEW_DAY = Convert.ToString(resultTableFromSETUP_REISSUE.Rows[0]["REVIEW_DAY"]);
					SETUP_REISSUE.resultTable = resultTableFromSETUP_REISSUE;  //修改前欄位
					strResultFromSETUP_REISSUE = SETUP_REISSUE.update(ControlTWA.SessionToHash());
                    //紀錄資料存取物件:DB Log@@@
                    sessionControl.dOP = SETUP_REISSUE.UpdateOperator;
				}
				#endregion
				//@@@放行
				#region 訊息處理
				switch (strResultFromSETUP_REISSUE)
				{
					case "S0000": //成功
						//顯示查詢區
						this.PnlContent.Visible = true;
						//隱藏畫面編輯區
						this.PnlDetail.Visible = false;
						//設定訊息區內容
						Msg.getMsg(strResultFromSETUP_REISSUE, System.Reflection.MethodInfo.GetCurrentMethod().Name, Master);
						//呼叫資料庫資料及呈現於畫面上
						loadGridData();
						//重新reload Application
						ControlTWA.ResetApplication(false, "SETUP_REISSUE");
						break;
					//@@@放行
					case "S0002": //資料待放行
						//顯示查詢區
						this.PnlContent.Visible = true;
						//隱藏畫面編輯區
						this.PnlDetail.Visible = false;
						//設定訊息區內容
						Msg.getMsg(strResultFromSETUP_REISSUE, System.Reflection.MethodInfo.GetCurrentMethod().Name, Master);
						//呼叫資料庫資料及呈現於畫面上
						loadGridData();
						break;
					//@@@放行
					default: //資料庫錯誤
						Msg.getMsg(strResultFromSETUP_REISSUE, System.Reflection.MethodInfo.GetCurrentMethod().Name, Master);
						break;
				}
				#endregion
				//@@@放行
			}
			catch (Exception e_page)
			{
				Msg.getMsg("F9999", System.Reflection.MethodInfo.GetCurrentMethod().Name, Master);
				logger.strJobQueue = e_page.ToString();
				logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
			}
		}

		//@@放行異動9 8
		bool approve_Check()
		{
			try
			{
				#region 放行usercontrol之MNT_TODAY及SETUP_REISSUE刪除
                CARDWeb.CARD.usercontrols.MNT_LOG uscMNT_TODAY = this.PnlDetail.FindControl("uscMNT_TODAY") as CARDWeb.CARD.usercontrols.MNT_LOG;
				if (uscMNT_TODAY != null)
				{
					//取得被刪除的待放行欄位筆數/是否全部刪除
					int deleteRow_CNT = approveControl.deleteMNT_LOG(uscMNT_TODAY);
					//撤銷新增類的申請(刪除待放行資料包含新增)，處理完畢後跳出
					#region INSERT資料刪除
					if (approveControl.delete_INSERT)
					{
						string strResultFromSETUP_REISSUE_D = approveUpdate_Delete("delete");
						//設定訊息區內容
						Msg.getMsg(strResultFromSETUP_REISSUE_D, "delete", Master);
						return false;
					}
					#endregion
					//撤銷修改類的申請(刪除待放行資料)，處理完畢後重新進入明細頁面，跳出資料已刪除，請確認！
					#region 判斷是否有刪除非INSERT資料
					if (deleteRow_CNT > 0) 
					{
						//放行異動8
						//判別若把所有待放行修改"全部刪除"時，將主要資料表狀態設定為原始值
						if (approveControl.delete_ALL)
						{
							approveUpdate_Delete("update");
						}
						//放行異動8
						setDataFromDb("update");
						ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "key", "setTimeout(function(){alert('" + this.Lang.Get("MSG.PAGE.DELETE_CONFIRM") + "');},0)", true);
						return false;
					}
					#endregion
				}
				#endregion
				return true;
			}
			catch (Exception)
			{
				throw;
			}
		}
		//@@放行異動10 
		string approveUpdate_Delete(string action)
		{
			try
			{
				//SETUP_REISSUE刪除或修改
				Cybersoft.Data.DAL.SETUP_REISSUEDao SETUP_REISSUE = new Cybersoft.Data.DAL.SETUP_REISSUEDao();
				string strResultFromSETUP_REISSUE = "";
				SETUP_REISSUE.init();
				#region KEY
                SETUP_REISSUE.strWhereREVIEW_TYPE = this.ddlREVIEW_TYPE.SelectedValue;
                SETUP_REISSUE.strWhereREVIEW_DAY = this.txtREVIEW_DAY.Text;
				#endregion
				if ("delete".Equals(action.ToLower()))
				{
					strResultFromSETUP_REISSUE = SETUP_REISSUE.delete();
                    //紀錄資料存取物件:DB Log@@@
                    sessionControl.dOP = SETUP_REISSUE.DeleteOperator;
                    Msg.writeQueryLog(strResultFromSETUP_REISSUE, "delete", "");
					#region 訊息處理
					switch (strResultFromSETUP_REISSUE)
					{
						case "S0000": //成功
							//顯示查詢區
							this.PnlContent.Visible = true;
							//隱藏畫面編輯區
							this.PnlDetail.Visible = false;
							//呼叫資料庫資料及呈現於畫面上
							loadGridData();
							break;
						default: //資料庫錯誤
							break;
					}
					#endregion
				}
				else
				{
					SETUP_REISSUE.strPOST_RESULT = "00";
					strResultFromSETUP_REISSUE = SETUP_REISSUE.update();
                    //紀錄資料存取物件:DB Log@@@
                    sessionControl.dOP = SETUP_REISSUE.UpdateOperator;
                    Msg.writeQueryLog(strResultFromSETUP_REISSUE, "update", "");
				}
				return strResultFromSETUP_REISSUE;
			}
			catch (Exception)
			{
				throw;
			}
		}
		//@@放行異動4
		protected void itnApprove_Click(object sender, ImageClickEventArgs e)
		{
			try
			{
				//確定頁面通過檢查後才往下
				#region 確定頁面通過檢查後才往下
				if (this.Page.IsValid == false)
				{
					return;
				}
				#endregion
				//確認是否有資料需要放行
				#region 確認是否有資料需要放行
				DataTable resultTableFromMNT_TODAY = (DataTable)Session["DataTableMNT_TODAY_APPROVE"];
				if (resultTableFromMNT_TODAY == null)
				{
					Msg.getMsg("F0023", System.Reflection.MethodInfo.GetCurrentMethod().Name, Master);
					return;
				}
				#endregion

				//進行放行處理
				Session["SessionAPPROVE_CLICK"] = "Y";
				itnSave_Click("Approve", new ImageClickEventArgs(0, 0));
				Session["SessionAPPROVE_CLICK"] = "N";
			}
			catch (Exception e_page)
			{
				Msg.getMsg("F9999", System.Reflection.MethodInfo.GetCurrentMethod().Name, Master);
				logger.strJobQueue = e_page.ToString();
				logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
			}
		}
		protected void itnCheck_Click(object sender, ImageClickEventArgs e)
		{
			try
			{
				//確定頁面通過檢查後才往下
				#region 確定頁面通過檢查後才往下
				if (this.Page.IsValid == false)
				{
					return;
				}
				#endregion
				//確認是否有資料需要放行
				#region 確認是否有資料需要放行
				DataTable resultTableFromMNT_TODAY = (DataTable)Session["DataTableMNT_TODAY_APPROVE"];
				if (resultTableFromMNT_TODAY == null)
				{
					Msg.getMsg("F0023", System.Reflection.MethodInfo.GetCurrentMethod().Name, Master);
					return;
				}
				#endregion

				//進行放行處理
				Session["SessionCHECK_CLICK"] = "Y";
				itnSave_Click("Check", new ImageClickEventArgs(0, 0));
				Session["SessionCHECK_CLICK"] = "N";
			}
			catch (Exception e_page)
			{
				Msg.getMsg("F9999", System.Reflection.MethodInfo.GetCurrentMethod().Name, Master);
				logger.strJobQueue = e_page.ToString();
				logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
			}
		}
        protected void gdvdata_RowCreated(object sender, GridViewRowEventArgs e)
        {
            #region Gridview header處理
            if (e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells[1].Visible = false;
            }
            #endregion
            if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Footer)
            {
                //隱藏cell1
                e.Row.Cells[1].Visible = false;
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
				//取得資料
				DataTable resultTableFromSETUP_REISSUE = (DataTable)Session["DataTableSETUP_REISSUE"];
				//重新指定資料來源
				gdvdata.DataSource = resultTableFromSETUP_REISSUE;
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
				//取得資料
				DataTable resultTableFromSETUP_REISSUE = (DataTable)Session["DataTableSETUP_REISSUE"];
				//指定排序方向
				resultTableFromSETUP_REISSUE.DefaultView.Sort = sortDirection;
				//回寫暫存資料
				Session["DataTableSETUP_REISSUE"] = resultTableFromSETUP_REISSUE.DefaultView.ToTable();
				//重新指定資料來源
				gdvdata.DataSource = resultTableFromSETUP_REISSUE;
				//將資料來源與GridView繫在一起
				gdvdata.DataBind();
			}
			catch (Exception e_page)
			{
				logger.strJobQueue = e_page.ToString();
				logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
			}
		}
		//@@放行異動7
		protected void gdvdata_DataBound(object sender, EventArgs e)
		{
			//待放行的資料將資料行背景變色，否則清空欄位
			try
			{
				approveControl.approveGridview(this.gdvdata);
			}
			catch (Exception e_page)
			{
				logger.strJobQueue = e_page.ToString();
				logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
			}
		}
		//@@放行異動6
		private bool setDataFromDb(string action)
		{
			try
			{
				//顯示畫面編輯區
				this.PnlDetail.Visible = true;
				//設定初始值
				initData(1);
				bool isFound = false;
				for (int i = 0; i < this.gdvdata.Rows.Count; i++)
				{
					RadioButton rtnselected = (RadioButton)this.gdvdata.Rows[i].FindControl("rtnselection");
					if (rtnselected.Checked)
					{
						isFound = true;
						//@@放行
						#region 待審核待放行資料不可"COPY_MODE"
						if (Session["SessionMode"].ToString() == "COPY_MODE")
						{
							if (gdvdata.Rows[i].Cells[gdvdata.Rows[i].Cells.Count - 1].Text.Contains("*"))
							{
								Session["SessionPAGE_RC"] = "I0052";
								return false;
							}
						}
						#endregion
						#region 資料查詢
						Cybersoft.Data.DAL.SETUP_REISSUEDao SETUP_REISSUE = new Cybersoft.Data.DAL.SETUP_REISSUEDao();
						DataTable resultTableFromSETUP_REISSUE = new DataTable();
						SETUP_REISSUE.init();
						//查詢鍵值
						#region 查詢鍵值
                        SETUP_REISSUE.strWhereREVIEW_TYPE = gdvdata.Rows[i].Cells[1].Text;
                        if (!"&nbsp;".Equals(gdvdata.Rows[i].Cells[3].Text))
                        {
                            SETUP_REISSUE.strWhereREVIEW_DAY = gdvdata.Rows[i].Cells[3].Text;
                        }
						#endregion
						string strResultFromSETUP_REISSUE = SETUP_REISSUE.query();
                        //紀錄資料存取物件:DB Log@@@
                        sessionControl.dOP = SETUP_REISSUE.SelectOperator;
						switch (strResultFromSETUP_REISSUE)
						{
							case "S0000": //成功
								//@@@放行
								//需要放行時且具有放行權限或有修改權限時
								if (approveControl.init(action))
								{
									//取得鍵值查詢資料(一定要和儲存時寫入的鍵值相同)
									#region Key Process
									#endregion
									//讀取資料同時將資料繫結在Table中
									SETUP_REISSUE.resultTable = approveControl.approveDataTable("SETUP_REISSUE", SETUP_REISSUE.resultTable, this.PnlDetail, this.Page);
								}
								//@@@放行
								resultTableFromSETUP_REISSUE = SETUP_REISSUE.resultTable;
								Session["DataTableSETUP_REISSUE"] = resultTableFromSETUP_REISSUE;
								break;
							case "F0023": //查無資料
								break;
							default: //資料庫錯誤
								break;
						}
						#endregion

						PanelControl.setFieldsFromDB(this.Panel1, resultTableFromSETUP_REISSUE);
						PanelControl.setFieldsFromDB(this.Panel2, resultTableFromSETUP_REISSUE);
                        this.ddlCHECK_ACCT_CONTROLCODE.SelectedIndex = this.ddlCHECK_ACCT_CONTROLCODE.Items.IndexOf(this.ddlCHECK_ACCT_CONTROLCODE.Items.FindByValue(resultTableFromSETUP_REISSUE.Rows[0]["CHECK_ACCT_CTLCODE"].ToString()));
                        this.ddlCHECK_CARD_CONTROLCODE.SelectedIndex = this.ddlCHECK_CARD_CONTROLCODE.Items.IndexOf(this.ddlCHECK_CARD_CONTROLCODE.Items.FindByValue(resultTableFromSETUP_REISSUE.Rows[0]["CHECK_CARD_CTLCODE"].ToString()));

						#region key process

						#endregion
						break;
					}
				}
				return isFound;
			}
			catch (Exception e_page)
			{
				logger.strJobQueue = e_page.ToString();
				logger.writeSysout(Cybersoft.Coca.Log.LevelEnum.Debug);
				return false;
			}
		}

        private string FieldsCheck()
        {
            #region 新增、複製時檢核
            if (Convert.ToString(Session["SessionMode"]) == "COPY_MODE" ||
                Convert.ToString(Session["SessionMode"]) == "INSERT_MODE")
            {
                Cybersoft.Data.DAL.SETUP_REISSUEDao SETUP_REISSUE = new Cybersoft.Data.DAL.SETUP_REISSUEDao();
                DataTable SETUP_REISSUE_Check = new DataTable();
                string RC = "";
                //檢核不可有第二筆Y (年度覆審)
                if (this.ddlREVIEW_TYPE.SelectedValue == "Y")
                {
                    SETUP_REISSUE.init();
                    SETUP_REISSUE.strWhereREVIEW_TYPE = this.ddlREVIEW_TYPE.SelectedValue.ToString();
                    SETUP_REISSUE.strWhereVALID_FLAG = "Y";
                    RC = SETUP_REISSUE.query();
                    switch (RC)
                    {
                        case "S0000": //成功
                            this.ddlREVIEW_TYPE.Focus();
                            return "已有一筆年度續卡參數，請使用修改!";
                        case "F0023": //查無資料
                            break;
                        default: //資料庫錯誤
                            this.ddlREVIEW_TYPE.Focus();
                            return "參數資料有誤";
                    }
                }
                
                //檢核重複
                SETUP_REISSUE.init();
                SETUP_REISSUE.strWhereREVIEW_TYPE = this.ddlREVIEW_TYPE.SelectedValue.ToString();
                SETUP_REISSUE.strWhereREVIEW_DAY = this.txtREVIEW_DAY.Text;
                RC = SETUP_REISSUE.query();
                switch (RC)
                {
                    case "S0000": //成功
                        this.ddlREVIEW_TYPE.Focus();
                        return "參數重複，請使用修改!";
                    case "F0023": //查無資料
                        break;
                    default: //資料庫錯誤
                        this.ddlREVIEW_TYPE.Focus();
                        return "參數資料有誤";
                }
            }
            #endregion

            #region 欄位對應正確性檢核
            //帳齡狀態
            if ("Y".Equals(this.ddlCHECK_DELQ.SelectedValue.ToString()))
            {
                if ("".Equals(this.txtCHECK_DELQ_CNT.Text) && "0".Equals(this.txtCHECK_DELQ_STMT_BAL.Text))
                {
                    this.ddlCHECK_DELQ.Focus();
                    return "帳齡狀態檢核參數有誤";
                }
            }
            
            //卡片額度
            if ("Y".Equals(this.ddlCHECK_LIMIT.SelectedValue.ToString()))
            {
                if ("".Equals(this.txtCHECK_CREDIT_LIMIT.Text))
                {
                    this.ddlCHECK_LIMIT.Focus();
                    return "卡片額度檢核參數有誤";
                }
            }

            //外國人持卡
            if ("Y".Equals(this.ddlCHECK_FOREIGNER.SelectedValue.ToString()))
            {
                if ("".Equals(this.txtCHECK_FOREIGNER_NATIONALITY.Text))
                {
                    this.ddlCHECK_FOREIGNER.Focus();
                    return "外國人持卡檢核參數有誤";
                }
            }
            #endregion
            
            return "";
        }

        protected override void OnPreInit(EventArgs e)
        {
            MasterPageFile = MasterPageVirtualPathProvider.MasterPageFileLocation;
            base.OnPreInit(e);
            initSignOn.run();
        }
	}
}
