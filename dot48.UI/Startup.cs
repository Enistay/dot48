using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(dot48.UI.Startup))]
namespace dot48.UI
{   

    public sealed partial class Startup
    {
        public static void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}