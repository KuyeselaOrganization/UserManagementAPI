namespace UserManagementAPI.Services;

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using UserManagementAPI.Data;
using UserManagementAPI.DTOs.Common;
using UserManagementAPI.DTOs.Permissions;
using UserManagementAPI.Models;

public class PermissionService
{
    private readonly AppDbContext _context;
    private readonly RoleManager<ApplicationRole> _roleManager;
    private readonly UserManager<ApplicationUser> _userManager;

    public PermissionService(
        AppDbContext context,
        RoleManager<ApplicationRole> roleManager,
        UserManager<ApplicationUser> userManager
    )
    {
        _context = context;
        _roleManager = roleManager;
        _userManager = userManager;
    }

    // 🔹 Get all permissions
    public async Task<List<PermissionDto>> GetAllPermissions()
    {
        return await _context
            .Permissions.Select(p => new PermissionDto
            {
                Id = p.Id,
                Name = p.Name,
                Code = p.Code,
            })
            .ToListAsync();
    }

    // 🔹 Create a new permission
    public async Task<ServiceResponse<string>> CreatePermission(CreatePermissionDto model)
    {
        if (await _context.Permissions.AnyAsync(p => p.Code == model.Code))
        {
            return new ServiceResponse<string>
            {
                Success = false,
                Errors = new List<string> { "Permission already exists." },
            };
        }

        var permission = new Permission { Name = model.Name, Code = model.Code };
        _context.Permissions.Add(permission);
        await _context.SaveChangesAsync();

        return new ServiceResponse<string>
        {
            Success = true,
            Data = "Permission created successfully.",
        };
    }

    // 🔹 Assign a permission to a role or user
    public async Task<ServiceResponse<string>> AssignPermission(AssignPermissionDto model)
    {
        var permission = await _context.Permissions.FindAsync(model.PermissionId);
        if (permission == null)
        {
            return new ServiceResponse<string>
            {
                Success = false,
                Errors = new List<string> { "Permission not found." },
            };
        }

        if (model.TargetType == "Role")
        {
            var role = await _roleManager.FindByIdAsync(model.TargetId);
            if (role == null)
            {
                return new ServiceResponse<string>
                {
                    Success = false,
                    Errors = new List<string> { "Role not found." },
                };
            }

            var rolePermission = new RolePermission
            {
                RoleId = role.Id,
                PermissionId = permission.Id,
            };
            _context.RolePermissions.Add(rolePermission);
        }
        else if (model.TargetType == "User")
        {
            var user = await _userManager.FindByIdAsync(model.TargetId);
            if (user == null)
            {
                return new ServiceResponse<string>
                {
                    Success = false,
                    Errors = new List<string> { "User not found." },
                };
            }

            var userPermission = new UserPermission
            {
                UserId = user.Id,
                PermissionId = permission.Id,
            };
            _context.UserPermissions.Add(userPermission);
        }

        await _context.SaveChangesAsync();
        return new ServiceResponse<string>
        {
            Success = true,
            Data = "Permission assigned successfully.",
        };
    }

    // 🔹 Revoke a permission from a role or user
    public async Task<ServiceResponse<string>> RevokePermission(RevokePermissionDto model)
    {
        if (model.TargetType == "Role")
        {
            var rolePermission = await _context.RolePermissions.FirstOrDefaultAsync(rp =>
                rp.RoleId == model.TargetId && rp.PermissionId == model.PermissionId
            );

            if (rolePermission != null)
            {
                _context.RolePermissions.Remove(rolePermission);
                await _context.SaveChangesAsync();
                return new ServiceResponse<string>
                {
                    Success = true,
                    Data = "Permission revoked from role.",
                };
            }
        }
        else if (model.TargetType == "User")
        {
            var userPermission = await _context.UserPermissions.FirstOrDefaultAsync(up =>
                up.UserId == model.TargetId && up.PermissionId == model.PermissionId
            );

            if (userPermission != null)
            {
                _context.UserPermissions.Remove(userPermission);
                await _context.SaveChangesAsync();
                return new ServiceResponse<string>
                {
                    Success = true,
                    Data = "Permission revoked from user.",
                };
            }
        }

        return new ServiceResponse<string>
        {
            Success = false,
            Errors = new List<string> { "Permission not found for the target." },
        };
    }
}
