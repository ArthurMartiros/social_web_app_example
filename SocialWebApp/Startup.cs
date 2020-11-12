using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SocialWebApp.Startup))]
namespace SocialWebApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
