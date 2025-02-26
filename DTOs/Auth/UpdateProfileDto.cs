namespace UserManagementAPI.DTOs.Auth;

using System.ComponentModel.DataAnnotations;

public class UpdateProfileDto
{
    [Required]
    public string UserId { get; set; } = string.Empty;

    [Required]
    public string FullName { get; set; } = string.Empty;

    [Required, EmailAddress]
    public string Email { get; set; } = string.Empty;
}
