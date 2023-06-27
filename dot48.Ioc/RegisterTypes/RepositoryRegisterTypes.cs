using dot48.Domain.Interfaces.Repository;
using dot48.Infra.Persistency.Repositories;
using dot48.Ioc.Extensions;
using Microsoft.Practices.Unity;

namespace dot48.Ioc.RegisterTypes
{
    public static class RepositoryRegisterTypes
    {
        public static void RegisterTypes(IUnityContainer container)
        {
            container.RegisterTypeAsHierarchicalLifetimeManager<IUserRepository, UserRepository>();
            container.RegisterTypeAsHierarchicalLifetimeManager<IProfileRepository, ProfileRepository>();
        }
    }
}
