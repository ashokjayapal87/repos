using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using UserApi.Core.Models;
using UserApi.Services.Interfaces;

namespace UserApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    #region Private Variables

    private readonly IUserService _userService;
    private readonly ILogger<UserController> _logger;
    private IValidator<User> _validator;

    #endregion

    #region Constructor

    public UserController(
        IUserService userService,
        IValidator<User> validator,
        ILogger<UserController> logger)
    {
        _userService = userService;
        _validator = validator;
        _logger = logger;
    }

    #endregion

    #region Public Methods

    /// <summary>
    /// Get all User data
    /// </summary>
    /// <returns></returns>
    [HttpGet("GetAllUsers")]
    public async Task<IEnumerable<User>?> GetAllUsers()
    {
        _logger.LogInformation("User Controller: GetAllUsers");

        return await _userService.GetAllUsers();
    }

    /// <summary>
    /// Create User
    /// </summary>
    /// <param name="user"></param>
    /// <returns></returns>
    [HttpPost("CreateUser")]
    public async Task<IActionResult> CreateUser(User user)
    {
        int userId = 0;

        //log message
        _logger.LogInformation("User Controller: Create User");

        //Validator using Fluent Validation
        ValidationResult result = await _validator.ValidateAsync(user);

        if (result.IsValid)
        {
            //Call CreateUser in UserService
            userId = await _userService.CreateUser(user);
        }

        if (userId > 0)
        {
            _logger.LogInformation("User Controller: New User created with id: " + userId);
        }

        return Ok(userId);
    }

    /// <summary>
    /// Update User
    /// </summary>
    /// <param name="user"></param>
    /// <returns></returns>
    [HttpPost("UpdateUser")]
    public async Task<IActionResult> UpdateUser(User user)
    {
        //logger
        _logger.LogInformation("User Controller: Update User");

        var userId = user.UserId;

        if (userId <= 0)
        {
            return BadRequest("UserId is null or invalid");
        }

        //Check user exists and get user details
        var userDetails = await _userService.GetUserById(userId);

        if (userDetails == null)
        {
            return NotFound("User Not Found");
        }

        //Map user Details for Update
        userDetails.FirstName = user.FirstName;
        userDetails.MiddleName = user.MiddleName;
        userDetails.LastName = user.LastName;

        userDetails.Suffix = user.Suffix;
        userDetails.Address = user.Address;
        userDetails.Email = user.Email;

        userDetails.ModifiedDate = DateTime.UtcNow;

        //Call UserService to Update User details
        int userUpdateResult = await _userService.UpdateUser(userDetails);

        return Ok(userUpdateResult);
    }

    /// <summary>
    /// Get User details by userid
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    [HttpGet("GetUserById")]
    public async Task<IActionResult> GetUserById(int userId)
    {
        _logger.LogInformation("User Controller: GetUserById");

        if (userId <= 0)
        {
            return BadRequest("UserId is null or invalid");
        }

        var user = await _userService.GetUserById(userId);

        if (user == null)
        {
            return NotFound("Data Not Found: Invalid user id : " + userId);
        }

        return Ok(user);
    }

    #endregion

}
