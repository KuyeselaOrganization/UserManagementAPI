namespace UserManagementAPI.DTOs.Permissions;

using System.ComponentModel.DataAnnotations;

public class AssignPermissionDto
{
    [Required]
    public int PermissionId { get; set; }

    [Required]
    public string TargetId { get; set; } = string.Empty; // Role ID or User ID

    [Required]
    public string TargetType { get; set; } = "Role"; // Either "Role" or "User"
}
