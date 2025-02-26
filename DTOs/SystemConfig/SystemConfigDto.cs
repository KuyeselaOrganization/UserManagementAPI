namespace UserManagementAPI.DTOs.SystemConfig;

public class SystemConfigDto
{
    public bool EnableMFA { get; set; } = false;
    public bool EnableIdentification { get; set; } = false;
    public bool AllowUserRegistration { get; set; } = true;
    public bool RequireAdminApproval { get; set; } = false;
}
