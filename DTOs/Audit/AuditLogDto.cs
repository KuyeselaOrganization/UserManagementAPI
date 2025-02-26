namespace UserManagementAPI.DTOs.Audit;

using System;

public class AuditLogDto
{
    public string UserId { get; set; } = string.Empty;
    public string Action { get; set; } = string.Empty;
    public string IPAddress { get; set; } = string.Empty;
    public DateTime Timestamp { get; set; }
}
