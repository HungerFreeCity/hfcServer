using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(hfcServer.Startup))]
namespace hfcServer
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
