namespace UserManagementAPI.Controllers;

using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserManagementAPI.DTOs.Role;
using UserManagementAPI.Services;

[ApiController]
[Route("api/roles")]
[Authorize(Roles = "Administrator")]
public class RoleController : ControllerBase
{
    private readonly RoleService _roleService;

    public RoleController(RoleService roleService)
    {
        _roleService = roleService;
    }

    // 🔹 Get all roles
    [HttpGet]
    public async Task<ActionResult<List<RoleDto>>> GetAllRoles()
    {
        return Ok(await _roleService.GetAllRoles());
    }

    // 🔹 Create a new role
    [HttpPost]
    public async Task<IActionResult> CreateRole([FromBody] CreateRoleDto model)
    {
        var response = await _roleService.CreateRole(model);
        return response.Success ? Ok(response) : BadRequest(response);
    }
}
