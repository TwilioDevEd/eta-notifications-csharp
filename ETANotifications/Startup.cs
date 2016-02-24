using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ETANotifications.Startup))]
namespace ETANotifications
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
