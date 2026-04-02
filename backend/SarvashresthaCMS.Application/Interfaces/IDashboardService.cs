using SarvashresthaCMS.Application.DTOs.Dashboard;
using System.Threading.Tasks;

namespace SarvashresthaCMS.Application.Interfaces;

public interface IDashboardService
{
    Task<DashboardDto> GetDashboardDataAsync();
}
