using UserApi.Core.Interfaces;
using UserApi.Core.Models;
using UserApi.Models;

namespace UserApi.Infrastructure.Repositories;

public class UserRepository : GenericRepository<User>, IUserRepository
{
    public UserRepository(DbContextClass context) : base(context)
    {
    }
}
