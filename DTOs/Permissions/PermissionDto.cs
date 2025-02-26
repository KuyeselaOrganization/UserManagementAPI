namespace UserManagementAPI.DTOs.Permissions;

public class PermissionDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty; // e.g., "USER_MANAGE", "REPORT_VIEW"
}
