namespace UserManagementAPI.Models;

using System;
using UserManagementAPI.Enums;

public class IdentificationDocument
{
    public int Id { get; set; }
    public string UserId { get; set; }
    public ApplicationUser User { get; set; }

    public IdentificationType Type { get; set; }
    public string DocumentNumber { get; set; } = string.Empty;
    public string FilePath { get; set; } = string.Empty;

    public DateOnly IssuedDate { get; set; }
    public DateOnly ExpiryDate { get; set; }
}
