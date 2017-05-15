using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Unity_MVC.Startup))]
namespace Unity_MVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
