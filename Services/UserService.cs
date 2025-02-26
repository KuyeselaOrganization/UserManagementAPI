namespace UserManagementAPI.Services;

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using UserManagementAPI.DTOs.Common;
using UserManagementAPI.DTOs.User;
using UserManagementAPI.Models;

public class UserService
{
    private readonly UserManager<ApplicationUser> _userManager;

    public UserService(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

    // 🔹 Get all users
    public async Task<List<UserDto>> GetAllUsers()
    {
        var users = _userManager
            .Users.Select(user => new UserDto
            {
                Id = user.Id,
                FullName = user.FullName,
                Email = user.Email,
                IsActive = user.IsActive,
                UserType = user.UserType,
            })
            .ToList();

        return users;
    }

    // 🔹 Get a single user
    public async Task<UserDto?> GetUserById(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
            return null;

        return new UserDto
        {
            Id = user.Id,
            FullName = user.FullName,
            Email = user.Email,
            IsActive = user.IsActive,
            UserType = user.UserType,
        };
    }

    // 🔹 Update a user profile
    public async Task<ServiceResponse<string>> UpdateUser(UserUpdateDto model)
    {
        var user = await _userManager.FindByIdAsync(model.UserId);
        if (user == null)
        {
            return new ServiceResponse<string>
            {
                Success = false,
                Errors = new List<string> { "User not found" },
            };
        }

        user.FullName = model.FullName;
        user.Email = model.Email;

        var result = await _userManager.UpdateAsync(user);
        return result.Succeeded
            ? new ServiceResponse<string> { Success = true, Data = "Profile updated successfully" }
            : new ServiceResponse<string>
            {
                Success = false,
                Errors = result.Errors.Select(e => e.Description).ToList(),
            };
    }
}
