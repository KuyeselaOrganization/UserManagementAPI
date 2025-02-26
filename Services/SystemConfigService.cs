namespace UserManagementAPI.Services;

using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using UserManagementAPI.Data;
using UserManagementAPI.DTOs.SystemConfig;
using UserManagementAPI.Models;

public class SystemConfigService
{
    private readonly AppDbContext _context;

    public SystemConfigService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<SystemConfigDto> GetSystemConfig()
    {
        var config = await _context.SystemConfigs.FirstOrDefaultAsync() ?? new SystemConfig();
        return new SystemConfigDto
        {
            EnableMFA = config.EnableMFA,
            EnableIdentification = config.EnableIdentification,
            AllowUserRegistration = config.AllowUserRegistration,
            RequireAdminApproval = config.RequireAdminApproval,
        };
    }

    public async Task UpdateSystemConfig(SystemConfigDto model)
    {
        var config = await _context.SystemConfigs.FirstOrDefaultAsync() ?? new SystemConfig();
        config.EnableMFA = model.EnableMFA;
        config.EnableIdentification = model.EnableIdentification;
        config.AllowUserRegistration = model.AllowUserRegistration;
        config.RequireAdminApproval = model.RequireAdminApproval;

        _context.SystemConfigs.Update(config);
        await _context.SaveChangesAsync();
    }
}
