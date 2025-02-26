namespace UserManagementAPI.Controllers;

using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UserManagementAPI.DTOs.Auth;
using UserManagementAPI.DTOs.Common;
using UserManagementAPI.Services;

[ApiController]
[Route("api/v1/auth")]
public class AuthController : ControllerBase
{
    private readonly AuthService _authService;

    public AuthController(AuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterDto registerDTO)
    {
        var response = await _authService.RegisterUser(registerDTO);
        return response.Success ? Ok(response) : BadRequest(response);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto loginDTO)
    {
        var user = await _authService.AuthenticateUser(loginDTO);
        if (user == null)
        {
            return Unauthorized(new { Message = "Invalid credentials" });
        }

        var token = _authService.GenerateJwtToken(user);
        return Ok(new { Token = token });
    }
}
