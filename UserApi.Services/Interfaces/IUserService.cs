using UserApi.Core.Models;

namespace UserApi.Services.Interfaces;

public interface IUserService
{
    Task<int> CreateUser(User user);
    Task<int> UpdateUser(User user);
    Task<IEnumerable<User>> GetAllUsers();
    Task<User> GetUserById(int id);    
}
