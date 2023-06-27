using dot48.Domain.Interfaces.Repository;
using dot48.Infra.Persistency.Contexts;

namespace dot48.Infra.Persistency
{
    public sealed class UnitOfWork : IUnitOfWork
    {
        
        private readonly dot48DbContext context;
        public UnitOfWork(dot48DbContext context)
        {
            this.context = context;
        }
        public void Commit()
        {
            this.context.SaveChanges();
        }
        public void Dispose()
        {
            Dispose(true);
        }
        private void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.context?.Dispose();
            }
        }
    }
}
