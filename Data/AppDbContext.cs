namespace UserManagementAPI.Data;

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using UserManagementAPI.Models;

public class AppDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string>
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options) { }

    public DbSet<Permission> Permissions { get; set; }
    public DbSet<RolePermission> RolePermissions { get; set; }
    public DbSet<IdentificationDocument> IdentificationDocuments { get; set; }
    public DbSet<SystemConfig> SystemConfigs { get; set; }
    public DbSet<AuditLog> AuditLogs { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder
            .Entity<ApplicationRole>()
            .HasData(
                new ApplicationRole
                {
                    Id = "1",
                    Name = "Administrator",
                    NormalizedName = "ADMINISTRATOR",
                    Description = "Full system access",
                    IsConfigurable = false,
                }
            );

        // seed default permissions
        builder
            .Entity<Permission>()
            .HasData(
                new Permission
                {
                    Id = 1,
                    Name = "Manage Users",
                    Code = "USER_MANAGE",
                },
                new Permission
                {
                    Id = 2,
                    Name = "View Reports",
                    Code = "REPORT_VIEW",
                }
            );
    }
}
