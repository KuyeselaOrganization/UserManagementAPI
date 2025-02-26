namespace UserManagementAPI.DTOs.Role;

using System.ComponentModel.DataAnnotations;

public class CreateRoleDto
{
    [Required]
    public string Name { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;
}
