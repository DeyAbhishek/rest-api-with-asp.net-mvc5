using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TPOWeb.Startup))]
namespace TPOWeb
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
