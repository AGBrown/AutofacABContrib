using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Remember.MvcWeb.Startup))]
namespace Remember.MvcWeb
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
