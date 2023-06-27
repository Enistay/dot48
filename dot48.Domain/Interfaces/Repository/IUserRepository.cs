using dot48.Domain.Entities;
using dot48.Domain.Interfaces.Repository.Base;

namespace dot48.Domain.Interfaces.Repository
{
    public interface IUserRepository : IBaseRepository<User, int>
    {
    }
}
