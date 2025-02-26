namespace UserManagementAPI.DTOs.User;

using System.ComponentModel.DataAnnotations;

public class UserUpdateDto
{
    [Required]
    public string UserId { get; set; } = string.Empty;

    [Required]
    public string FullName { get; set; } = string.Empty;

    [Required]
    public string Email { get; set; } = string.Empty;
}
