namespace UserManagementAPI.Models;

public class RolePermission
{
    public int Id { get; set; }

    public string RoleId { get; set; }
    public ApplicationRole Role { get; set; }
    public int PermissionId { get; set; }
    public Permission Permission { get; set; }
}
