using System;
using Microsoft.Practices.Unity;

namespace dot48.Ioc.Extensions
{
    public static class UnityContainerExtension
    {        
        public static IUnityContainer RegisterTypeAsHierarchicalLifetimeManager<TFrom, TTo>(this IUnityContainer container)
            where TTo : TFrom
        {
            return container.RegisterTypeAsHierarchicalLifetimeManager(typeof(TFrom), typeof(TTo));
        }
        
        public static IUnityContainer RegisterTypeAsHierarchicalLifetimeManager(this IUnityContainer container, Type from, Type to)
        {
            using (var hierarchicalLifetime = new HierarchicalLifetimeManager())
            {
                return container.RegisterType(from, to, hierarchicalLifetime);
            }
        }
    }
}
