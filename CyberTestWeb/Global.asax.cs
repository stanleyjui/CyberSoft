using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;
using CARDWeb;
using System.Web.Hosting;

namespace CyberTestWeb
{
    public class Global : HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {
            // 應用程式啟動時執行的程式碼
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            System.IO.Directory.CreateDirectory(Server.MapPath("CARD/TempImages"));
            string RC = CARDWeb.CARD.src.ControlTWA.ResetApplication(true, "");

            MasterPageVirtualPathProvider vpp = new MasterPageVirtualPathProvider();
            HostingEnvironment.RegisterVirtualPathProvider(vpp);

            //VirtualPathProviders vpp1 = new VirtualPathProviders("~/MasterPageDir/TabHeaderTemplate.ascx", "CARDWeb.CARD.usercontrols.TabHeaderTemplate.ascx");
            //HostingEnvironment.RegisterVirtualPathProvider(vpp1);

        }
    }
}