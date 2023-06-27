using dot48.Ioc.RegisterTypes;
using Microsoft.Practices.Unity;

namespace dot48.Ioc
{
    public static class UnityStartup
    {
        public static void InitializeUnityContext(IUnityContainer container)
        {
            if (container != null)
            {
                ApplicationRegisterTypes.RegisterTypes(container);
                InfraRegisterTypes.RegisterTypes(container);               
                RepositoryRegisterTypes.RegisterTypes(container);
            }
        }
    }
}
