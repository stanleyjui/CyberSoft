using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CyberTestWeb.Startup))]
namespace CyberTestWeb
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
