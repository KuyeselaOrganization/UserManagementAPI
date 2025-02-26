namespace UserManagementAPI.DTOs.Identification;

using System.ComponentModel.DataAnnotations;
using UserManagementAPI.Enums;

public class IdentificationDto
{
    [Required]
    public string UserId { get; set; } = string.Empty;

    [Required]
    public IdentificationType Type { get; set; }

    [Required]
    public string DocumentNumber { get; set; } = string.Empty;

    public string? FilePath { get; set; }
}
