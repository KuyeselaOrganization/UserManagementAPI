namespace UserManagementAPI.Controllers;

using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserManagementAPI.DTOs.Audit;
using UserManagementAPI.Services;

[ApiController]
[Route("api/audit-logs")]
[Authorize(Roles = "Administrator")]
public class AuditLogController : ControllerBase
{
    private readonly AuditLogService _auditLogService;

    public AuditLogController(AuditLogService auditLogService)
    {
        _auditLogService = auditLogService;
    }

    // 🔹 Get all audit logs
    [HttpGet]
    public async Task<ActionResult<List<AuditLogDto>>> GetAllAuditLogs()
    {
        var logs = await _auditLogService.GetAllAuditLogs();
        return Ok(logs);
    }

    // 🔹 Get logs for a specific user
    [HttpGet("{userId}")]
    public async Task<ActionResult<List<AuditLogDto>>> GetUserAuditLogs(string userId)
    {
        var logs = await _auditLogService.GetUserAuditLogs(userId);
        return logs.Count > 0 ? Ok(logs) : NotFound("No logs found for the specified user.");
    }
}
