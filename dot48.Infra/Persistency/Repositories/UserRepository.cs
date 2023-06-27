using dot48.Domain.Entities;
using dot48.Domain.Interfaces.Repository;
using dot48.Infra.Persistency.Contexts;

namespace dot48.Infra.Persistency.Repositories
{
    public class UserRepository : BaseRepository<User, int>, IUserRepository
    {
        public UserRepository(dot48DbContext context) : base(context)
        {
        }
    }
}
