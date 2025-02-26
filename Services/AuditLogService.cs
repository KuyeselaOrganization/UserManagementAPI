namespace UserManagementAPI.Services;

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using UserManagementAPI.Data;
using UserManagementAPI.DTOs.Audit;
using UserManagementAPI.Models;

public class AuditLogService
{
    private readonly AppDbContext _context;

    public AuditLogService(AppDbContext context)
    {
        _context = context;
    }

    // 🔹 Get all audit logs
    public async Task<List<AuditLogDto>> GetAllAuditLogs()
    {
        return await _context
            .AuditLogs.OrderByDescending(a => a.TimeStamp)
            .Select(a => new AuditLogDto
            {
                UserId = a.UserId,
                Action = a.Action,
                IPAddress = a.IPAddress,
                Timestamp = a.TimeStamp,
            })
            .ToListAsync();
    }

    // 🔹 Get audit logs for a specific user
    public async Task<List<AuditLogDto>> GetUserAuditLogs(string userId)
    {
        return await _context
            .AuditLogs.Where(a => a.UserId == userId)
            .OrderByDescending(a => a.TimeStamp)
            .Select(a => new AuditLogDto
            {
                UserId = a.UserId,
                Action = a.Action,
                IPAddress = a.IPAddress,
                Timestamp = a.TimeStamp,
            })
            .ToListAsync();
    }

    // 🔹 Log an action (Used internally by services)
    public async Task LogAction(string userId, string action, string ipAddress)
    {
        var log = new AuditLog
        {
            UserId = userId,
            Action = action,
            IPAddress = ipAddress,
            TimeStamp = DateTime.UtcNow,
        };

        _context.AuditLogs.Add(log);
        await _context.SaveChangesAsync();
    }
}
