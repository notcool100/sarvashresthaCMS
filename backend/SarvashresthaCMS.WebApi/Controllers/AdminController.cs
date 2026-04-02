using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SarvashresthaCMS.Application.Common;
using SarvashresthaCMS.Application.DTOs.Auth;
using SarvashresthaCMS.Application.Interfaces;
using System.Threading.Tasks;

namespace SarvashresthaCMS.WebApi.Controllers;

[Authorize(Roles = "Admin")]
[ApiController]
[Route("api/[controller]")]
public class AdminController(IAuthService authService) : ControllerBase
{
    private readonly IAuthService _authService = authService;

    [HttpPost("register-staff")]
    public async Task<ActionResult<ServiceResponse<AuthResponse>>> RegisterStaff([FromBody] RegisterStaffRequest request)
    {
        var response = await _authService.RegisterStaffAsync(request);
        if (!response.Success) return BadRequest(response);
        return Ok(response);
    }

    // You can add more admin-only endpoints here (e.g., GetUsers, DeleteUser, etc.)
}
