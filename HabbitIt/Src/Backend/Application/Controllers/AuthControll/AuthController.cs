using HabbitIt.Dto.Login;
using HabbitIt.Dto.RegisterDto;
using HabbitIt.Services.Interfaces.Auth;
using Microsoft.AspNetCore.Mvc;

namespace HabbitIt.Application.Controllers.AuthControll;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("register")]
    public async Task<ActionResult> Register([FromBody] RegisterRequestDto registerRequestDto)
    {
        try
        {
           var response = await _authService.RegisterAsync(registerRequestDto);
           return Ok(response);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        } 
    }

    [HttpPost("login")]
    public async Task<ActionResult> Login([FromBody] LoginRequestDto loginRequestDto)
    {
        try
        {
           var response = await _authService.LoginAsync(loginRequestDto);
           return Ok(response);
        }
        catch (Exception ex)
        {
            return Unauthorized(new { error = ex.Message});
        }
    }
    
}