namespace UserManagementAPI.DTOs.Role;

using System.ComponentModel.DataAnnotations;

public class UpdateRoleDto
{
    [Required]
    public string Id { get; set; } = string.Empty;

    [Required]
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}
