using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(marimba_web.Startup))]
namespace marimba_web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
