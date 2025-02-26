namespace UserManagementAPI.Models;

using System;
using Microsoft.AspNetCore.Identity;
using UserManagementAPI.Enums;

public class ApplicationUser : IdentityUser
{
    public string FullName { get; set; } = string.Empty;
    public UserType UserType { get; set; } // Individual, Business, Government
    public bool IsActive { get; set; } = true;

    // Optional: Identification linkage if needed
    public string? IdentificationId { get; set; } // Links to Identification model if needed

    // public IdentificationDocument? Identification { get; set; }

    // Timestamps
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
}
