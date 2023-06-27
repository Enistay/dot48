using dot48.Ioc;
using Microsoft.Owin.Security;
using Microsoft.Practices.Unity;
using System.Web;
using System.Web.Http;
using Unity.WebApi;

namespace dot48.UI
{
    public static class UnityConfig
    {
        public static IUnityContainer Container { get; private set; }

        public static void RegisterComponents()
        {
			Container = new UnityContainer();

            UnityStartup.InitializeUnityContext(Container);

            System.Web.Mvc.DependencyResolver.SetResolver(new Unity.Mvc5.UnityDependencyResolver(Container));
            Container.RegisterType<IAuthenticationManager>(new InjectionFactory(o => HttpContext.Current.GetOwinContext().Authentication));

            // Resolve Dependencies for WebApi Applications
            GlobalConfiguration.Configuration.DependencyResolver = new Unity.WebApi.UnityDependencyResolver(Container);

        }
    }
}