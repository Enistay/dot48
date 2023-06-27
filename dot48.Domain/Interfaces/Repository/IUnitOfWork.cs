using System;

namespace dot48.Domain.Interfaces.Repository
{
    public interface IUnitOfWork : IDisposable
    {
        void Commit();
    }
}
