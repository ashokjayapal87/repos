using Microsoft.Extensions.Logging;
using UserApi.Core.Interfaces;
using UserApi.Core.Models;
using UserApi.Services.Interfaces;

namespace UserApi.Services.Services;

public class UserService : IUserService
{
    #region Variables

    private readonly IUserRepository _userRepository;
    private readonly ILogger<UserService> _logger;

    #endregion

    #region Constructor
    public UserService(IUserRepository userRepository, ILogger<UserService> logger)
    {
        _userRepository = userRepository;
        _logger = logger;
    }
    #endregion

    #region Public Methods
    public async Task<IEnumerable<User>> GetAllUsers()
    {
        var userList = await _userRepository.GetAllAsync();

        if (userList == null || userList.Count() <= 0)
        {
            _logger.LogInformation("No User(s) found");
        }

        return userList;
    }

    public async Task<int> CreateUser(User user)
    {
        user.CreatedDate = user.CreatedDate == null ? DateTime.UtcNow : user.CreatedDate;
        user.ModifiedDate = user.ModifiedDate == null ? DateTime.UtcNow : user.ModifiedDate;

        var userInsertResult = await _userRepository.CreateAsync(user);

        if (userInsertResult.UserId == 0)
        {
            _logger.LogInformation("Failed: Unable to create new user");
        }

        return userInsertResult.UserId;
    }

    public async Task<int> UpdateUser(User user)
    {
        var userUpdateResult = await _userRepository.UpdateAsync(user);

        if (userUpdateResult == null)
        {
            _logger.LogInformation("Failed: Unable to update user details");
        }

        return userUpdateResult.UserId;
    }

    public async Task<User> GetUserById(int userId)
    {
        User? userResult = new();

        userResult = await _userRepository.GetIdAsync(userId);

        if (userResult == null)
        {
            _logger.LogInformation("Data Not Found: Invalid user id : " + userId);
        }

        return userResult;
    }

    #endregion
}
