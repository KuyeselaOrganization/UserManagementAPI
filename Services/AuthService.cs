namespace UserManagementAPI.Services;

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using UserManagementAPI.DTOs.Auth;
using UserManagementAPI.DTOs.Common;
using UserManagementAPI.Models;

public class AuthService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IConfiguration _configuration;

    public AuthService(UserManager<ApplicationUser> userManager, IConfiguration configuration)
    {
        _userManager = userManager;
        _configuration = configuration;
    }

    public async Task<ServiceResponse<string>> RegisterUser(RegisterDto registerDTO)
    {
        var user = new ApplicationUser
        {
            UserName = registerDTO.Email,
            Email = registerDTO.Email,
            FullName = registerDTO.FullName,
            UserType = registerDTO.UserType,
        };

        var result = await _userManager.CreateAsync(user, registerDTO.Password);
        if (!result.Succeeded)
        {
            return new ServiceResponse<string>
            {
                Success = false,
                Errors = result.Errors.Select(e => e.Description).ToList(),
            };
        }

        return new ServiceResponse<string>
        {
            Success = true,
            Data = "User Registered Successfully",
        };
    }

    // Authenticate User
    public async Task<ApplicationUser?> AuthenticateUser(LoginDto loginDTO)
    {
        var user = await _userManager.FindByEmailAsync(loginDTO.Email);
        if (user == null)
        {
            return null;
        }

        var result = await _userManager.CheckPasswordAsync(user, loginDTO.Password);
        if (!result)
        {
            return null;
        }

        return user;
    }

    // Generate JWT Token
    public Task<string> GenerateJwtToken(ApplicationUser user)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id),
            new Claim(ClaimTypes.Email, user.Email ?? ""),
            new Claim("FullName", user.FullName),
        };

        var secret =
            Environment.GetEnvironmentVariable("JWT_SECRET")
            ?? _configuration["JwtSettings:Secret"];
        var expirationInMinutes =
            Environment.GetEnvironmentVariable("JWT_EXPIRATION_IN_MINUTES")
            ?? _configuration["JwtSettings:ExpirationInMinutes"]
            ?? "60";
        if (string.IsNullOrEmpty(secret))
        {
            throw new InvalidOperationException("JWT secret is not configured.");
        }
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            _configuration["JwtSettings:Issuer"],
            _configuration["JwtSettings:Issuer"],
            claims,
            expires: DateTime.Now.AddMinutes(int.Parse(expirationInMinutes)),
            signingCredentials: creds
        );

        return Task.FromResult(new JwtSecurityTokenHandler().WriteToken(token));
    }
}
