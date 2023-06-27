using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Owin;
using System;

namespace dot48.UI
{

    public sealed partial class Startup
    {      
        public static void ConfigureAuth(IAppBuilder app)
        {            
            var cookieSecure = CookieSecureOption.Always;
#if DEBUG
            //cookieSecure = CookieSecureOption.Never;
#endif

            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/SignIn"),
                SlidingExpiration = true,
                ExpireTimeSpan = TimeSpan.FromMinutes(20d),
                AuthenticationMode = AuthenticationMode.Active,
                CookieSecure = cookieSecure                
            });
        }
    }
}