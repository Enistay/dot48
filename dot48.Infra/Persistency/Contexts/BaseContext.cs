using System.Data.Entity;

namespace dot48.Infra.Persistency.Contexts
{
    public class BaseContext : DbContext
    {
        public BaseContext(string connectionStringName) : base(connectionStringName)
        {
            this.Configuration.ProxyCreationEnabled = false;
            this.Configuration.LazyLoadingEnabled = false;
        }
    }
}
