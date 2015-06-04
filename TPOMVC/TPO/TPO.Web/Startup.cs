using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TPO.Web.Startup))]
namespace TPO.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            //ConfigureAuth(app);
            AutoMapperConfig.Initialize();
        }
    }
}
