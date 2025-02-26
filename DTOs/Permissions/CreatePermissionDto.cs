namespace UserManagementAPI.DTOs.Permissions;

using System.ComponentModel.DataAnnotations;

public class CreatePermissionDto
{
    [Required]
    public string Name { get; set; } = string.Empty;

    [Required]
    public string Code { get; set; } = string.Empty;
}
