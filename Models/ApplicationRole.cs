namespace UserManagementAPI.Models;

using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

public class ApplicationRole : IdentityRole
{
    public string Description { get; set; } = string.Empty;
    public bool IsConfigurable { get; set; } = true; // Admin-defined roles can be updated

    public ICollection<RolePermission> Permissions { get; set; } = new List<RolePermission>();
}
