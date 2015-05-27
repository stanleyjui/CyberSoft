using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CyberTestWeb.src
{
    public static class initSignOn
    {
        public static void run()
        {
            if (HttpContext.Current.Session["Sessionrights_group"] == null)
            {
                Cybersoft.Data.DAL.ZZ_ACCOUNTDao ZZ_ACCOUNT = new Cybersoft.Data.DAL.ZZ_ACCOUNTDao();
                string USER_ACCT = "";
                try
                {
                    USER_ACCT = System.Web.Configuration.WebConfigurationManager.AppSettings["USER_ACCT"];
                }
                catch(Exception)
                {
                    USER_ACCT = "CYBERSOFT";
                }
                ZZ_ACCOUNT.strWhereUSER_ACCT = USER_ACCT;
                string RC_ZZ_ACCOUNT = ZZ_ACCOUNT.query();
                //共用區域建立 Session Control
                CARDWeb.CARD.src.ControlTWA.SetSession(ZZ_ACCOUNT.resultTable.Rows[0]);
                //設定Application區域
                //紀錄帳號已登入.最近操作時間及電腦ip
                HttpContext.Current.Application.Add(USER_ACCT, "登入");
                HttpContext.Current.Application.Add(USER_ACCT + "dt", System.DateTime.Now);
                HttpContext.Current.Application.Add(USER_ACCT + "sid", HttpContext.Current.Session.SessionID.ToString());
                HttpContext.Current.Application.Add(USER_ACCT + "ip", HttpContext.Current.Session["SessionUserComputerName"].ToString());
                HttpContext.Current.Session["SessionFN_ITEM"] = "1111111111111";
                HttpContext.Current.Session["SessionFN_CODE"] = System.IO.Path.GetFileName(HttpContext.Current.Request.PhysicalPath) + ".aspx";

                Cybersoft.Data.DAL.ZZ_TXNLOGDao TXNLOG = new Cybersoft.Data.DAL.ZZ_TXNLOGDao();
                //初始化log
                TXNLOG.initInsert();
                //其他Query log欄位
                //使用者帳號
                TXNLOG.strUSER_ACCT = Convert.ToString(System.Web.HttpContext.Current.Session["SessionAccount"]);
                //功能名稱
                TXNLOG.strFN_NAME = "";
                //程式名稱
                TXNLOG.strFN_CODE = Convert.ToString(System.Web.HttpContext.Current.Session["SessionFN_CODE"]);
                //按鈕
                TXNLOG.strLOG_BUTTON = "AUTO SIGN ON";
                //執行結果
                TXNLOG.strLOG_DESCR = "Sign on Success";
                //連線電腦ip
                TXNLOG.strLOG_IP = Convert.ToString(System.Web.HttpContext.Current.Session["SessionUserComputerName"]);
                //紀錄時間
                TXNLOG.datetimeLOG_DT = DateTime.Now;
                //寫入Query log
                //TXNLOG.insert();
                TXNLOG.insert();
             
            }
        }
    }
}