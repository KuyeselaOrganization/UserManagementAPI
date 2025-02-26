namespace UserManagementAPI.Controllers;

using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserManagementAPI.DTOs.SystemConfig;
using UserManagementAPI.Services;

[ApiController]
[Route("api/v1/config")]
[Authorize(Roles = "Administrator")]
public class SystemConfigController : ControllerBase
{
    private readonly SystemConfigService _systemConfigService;

    public SystemConfigController(SystemConfigService systemConfigService)
    {
        _systemConfigService = systemConfigService;
    }

    // 🔹 Get system configuration
    [HttpGet]
    public async Task<ActionResult<SystemConfigDto>> GetConfig()
    {
        return Ok(await _systemConfigService.GetSystemConfig());
    }

    // 🔹 Update system configuration
    [HttpPut]
    public async Task<IActionResult> UpdateConfig([FromBody] SystemConfigDto model)
    {
        await _systemConfigService.UpdateSystemConfig(model);
        return Ok("System configuration updated");
    }
}
