namespace UserManagementAPI.DTOs.User;

using UserManagementAPI.Enums;

public class UserDto
{
    public string Id { get; set; } = string.Empty;
    public string FullName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public bool IsActive { get; set; }
    public UserType UserType { get; set; }
}
