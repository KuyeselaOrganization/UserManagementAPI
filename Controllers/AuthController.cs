namespace UserManagementAPI.Controllers;

using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using UserManagementAPI.DTOs.Auth;
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

    [AllowAnonymous]
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterDto registerDTO)
    {
        var response = await _authService.RegisterUser(registerDTO);
        return response.Success ? Ok(response) : BadRequest(response);
    }

    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto loginDTO)
    {
        var user = await _authService.AuthenticateUser(loginDTO);
        if (user == null)
        {
            return Unauthorized(new { Message = "Invalid credentials" });
        }

        try
        {
            var token = await _authService.GenerateJwtToken(user);
            return Ok(token);
        }
        catch (System.ArgumentOutOfRangeException ex)
        {
            Console.WriteLine(ex.Message);
            return StatusCode(
                500,
                new
                {
                    Message = "An error occured while processing your request. Please contact the admin for help if the problem persists.",
                }
            );
        }
        catch (System.Exception ex)
        {
            Console.WriteLine(ex.Message);
            return BadRequest(
                new
                {
                    Message = "An error occured while processing your request. Please try again.",
                }
            );
        }
    }
}
