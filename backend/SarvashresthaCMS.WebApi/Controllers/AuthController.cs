using Microsoft.AspNetCore.Mvc;
using SarvashresthaCMS.Application.Common;
using SarvashresthaCMS.Application.DTOs.Auth;
using SarvashresthaCMS.Application.Interfaces;
using System.Threading.Tasks;

namespace SarvashresthaCMS.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController(IAuthService authService) : ControllerBase
{
    private readonly IAuthService _authService = authService;

    [HttpPost("register")]
    public async Task<ActionResult<ServiceResponse<AuthResponse>>> Register([FromBody] RegisterRequest request)
    {
        var response = await _authService.RegisterAsync(request);
        if (!response.Success) return BadRequest(response);
        return Ok(response);
    }

    [HttpPost("login")]
    public async Task<ActionResult<ServiceResponse<AuthResponse>>> Login([FromBody] LoginRequest request)
    {
        var response = await _authService.LoginAsync(request);
        if (!response.Success) return Unauthorized(response);
        return Ok(response);
    }

    [HttpPost("refresh-token")]
    public async Task<ActionResult<ServiceResponse<AuthResponse>>> RefreshToken([FromBody] RefreshTokenRequest request)
    {
        var response = await _authService.RefreshTokenAsync(request);
        if (!response.Success) return Unauthorized(response);
        return Ok(response);
    }
}
