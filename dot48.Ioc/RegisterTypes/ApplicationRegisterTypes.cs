using dot48.Application.Interfaces;
using dot48.Application.Services;
using dot48.Ioc.Extensions;
using Microsoft.Practices.Unity;

namespace dot48.Ioc.RegisterTypes
{
    public static class ApplicationRegisterTypes
    {
        public static void RegisterTypes(IUnityContainer container)
        {
            container.RegisterTypeAsHierarchicalLifetimeManager<IUserService, UserService>();
        }
    }
}
