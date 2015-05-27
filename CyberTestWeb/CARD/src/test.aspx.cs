using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CyberTestWeb.CARD.src
{
    public partial class test : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string SessionAccount = Convert.ToString(Session["SessionAccount"]);
            this.lblTest.Text = string.IsNullOrEmpty(SessionAccount) ? "取不到帳號" : string.Format("帳號是{0}", SessionAccount);
        }
    }
}