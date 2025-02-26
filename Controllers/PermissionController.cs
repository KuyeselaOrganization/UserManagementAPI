namespace UserManagementAPI.Controllers;

using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserManagementAPI.DTOs.Permissions;
using UserManagementAPI.Services;

[ApiController]
[Route("api/v1/permissions")]
[Authorize(Roles = "Administrator")]
public class PermissionController : ControllerBase
{
    private readonly PermissionService _permissionService;

    public PermissionController(PermissionService permissionService)
    {
        _permissionService = permissionService;
    }

    // 🔹 Get all permissions
    [HttpGet]
    public async Task<ActionResult<List<PermissionDto>>> GetAllPermissions()
    {
        return Ok(await _permissionService.GetAllPermissions());
    }

    // 🔹 Create a new permission
    [HttpPost]
    public async Task<IActionResult> CreatePermission([FromBody] CreatePermissionDto model)
    {
        var response = await _permissionService.CreatePermission(model);
        return response.Success ? Ok(response) : BadRequest(response);
    }

    // 🔹 Assign permission to a role or user
    [HttpPost("assign")]
    public async Task<IActionResult> AssignPermission([FromBody] AssignPermissionDto model)
    {
        var response = await _permissionService.AssignPermission(model);
        return response.Success ? Ok(response) : BadRequest(response);
    }
}
