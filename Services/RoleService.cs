namespace UserManagementAPI.Services;

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using UserManagementAPI.DTOs.Common;
using UserManagementAPI.DTOs.Role;
using UserManagementAPI.Models;

public class RoleService
{
    private readonly RoleManager<ApplicationRole> _roleManager;

    public RoleService(RoleManager<ApplicationRole> roleManager)
    {
        _roleManager = roleManager;
    }

    // 🔹 Get all roles
    public async Task<List<RoleDto>> GetAllRoles()
    {
        var roles = _roleManager
            .Roles.Select(r => new RoleDto { Id = r.Id, Name = r.Name })
            .ToList();
        return roles;
    }

    // 🔹 Create a new role
    public async Task<ServiceResponse<string>> CreateRole(CreateRoleDto model)
    {
        if (await _roleManager.RoleExistsAsync(model.Name))
        {
            return new ServiceResponse<string>
            {
                Success = false,
                Errors = new List<string> { "Role already exists" },
            };
        }

        var role = new ApplicationRole
        {
            Name = model.Name,
            Description = model.Description,
            IsConfigurable = true,
        };
        var result = await _roleManager.CreateAsync(role);

        return result.Succeeded
            ? new ServiceResponse<string> { Success = true, Data = "Role created successfully" }
            : new ServiceResponse<string>
            {
                Success = false,
                Errors = result.Errors.Select(e => e.Description).ToList(),
            };
    }
}
