using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SarvashresthaCMS.Application.DTOs.Dashboard;
using SarvashresthaCMS.Application.Interfaces;
using System.Threading.Tasks;

namespace SarvashresthaCMS.WebApi.Controllers;

[Authorize(Roles = "Admin")]
[ApiController]
[Route("api/[controller]")]
public class DashboardController(IDashboardService dashboardService) : ControllerBase
{
    private readonly IDashboardService _dashboardService = dashboardService;

    [HttpGet]
    public async Task<ActionResult<DashboardDto>> GetDashboardData()
    {
        var data = await _dashboardService.GetDashboardDataAsync();
        return Ok(data);
    }
}
