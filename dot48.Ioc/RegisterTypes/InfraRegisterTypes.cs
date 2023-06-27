using dot48.Domain.Interfaces.Repository;
using dot48.Infra.Persistency;
using dot48.Infra.Persistency.Contexts;
using dot48.Ioc.Extensions;
using Microsoft.Practices.Unity;

namespace dot48.Ioc.RegisterTypes
{
    public static class InfraRegisterTypes
    {
        public static void RegisterTypes(IUnityContainer container)
        {  
            container.RegisterTypeAsHierarchicalLifetimeManager<dot48DbContext, dot48DbContext>();
            container.RegisterTypeAsHierarchicalLifetimeManager<IUnitOfWork, UnitOfWork>();
        }
    }
}
