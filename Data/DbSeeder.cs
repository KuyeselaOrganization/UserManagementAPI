namespace UserManagementAPI.Data;

using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using UserManagementAPI.Enums;
using UserManagementAPI.Models;

public static class DbSeeder
{
    public static async Task SeedRolesAndAdminUser(
        UserManager<ApplicationUser> userManager,
        RoleManager<ApplicationRole> roleManager
    )
    {
        // ✅ Ensure Administrator Role Exists
        if (!await roleManager.RoleExistsAsync("Administrator"))
        {
            await roleManager.CreateAsync(
                new ApplicationRole
                {
                    Name = "Administrator",
                    NormalizedName = "ADMINISTRATOR",
                    Description = "Full system access",
                    IsConfigurable = false,
                }
            );
        }

        // ✅ Ensure Default Admin User Exists
        string adminEmail = "admin@system.com";
        var adminUser = await userManager.FindByEmailAsync(adminEmail);
        if (adminUser == null)
        {
            var newAdmin = new ApplicationUser
            {
                UserName = adminEmail,
                Email = adminEmail,
                FullName = "System Administrator",
                UserType = UserType.Admin,
                EmailConfirmed = true,
            };

            var result = await userManager.CreateAsync(newAdmin, "Admin@123");
            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(newAdmin, "Administrator");
            }
        }
    }
}
