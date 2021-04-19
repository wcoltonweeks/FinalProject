using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(FinalProject.UI.MVC.Startup))]
namespace FinalProject.UI.MVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
