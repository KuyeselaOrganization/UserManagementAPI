namespace UserManagementAPI.DTOs.Auth;

using System.ComponentModel.DataAnnotations;
using UserManagementAPI.Enums;

public class RegisterDto
{
    [Required]
    public string FullName { get; set; } = string.Empty;

    [Required, EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Required, MinLength(8)]
    public string Password { get; set; } = string.Empty;

    [Required]
    public UserType UserType { get; set; }
}
