namespace UserManagementAPI.Services;

using System;
using System.Threading.Tasks;
using UserManagementAPI.Data;
using UserManagementAPI.Models;

public class AuditLogService
{
    private readonly AppDbContext _context;

    public AuditLogService(AppDbContext context)
    {
        _context = context;
    }

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
