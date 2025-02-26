namespace UserManagementAPI.Models;

using System;

public class AuditLog
{
    public int Id { get; set; }
    public string UserId { get; set; }
    public ApplicationUser User { get; set; }
    public string Action { get; set; } = string.Empty;
    public string IPAddress { get; set; } = string.Empty;
    public DateTime TimeStamp { get; set; } = DateTime.UtcNow;
}
