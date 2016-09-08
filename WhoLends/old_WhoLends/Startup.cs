using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WhoLends.Startup))]
namespace WhoLends
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
