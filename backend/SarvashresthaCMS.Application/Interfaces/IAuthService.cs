using SarvashresthaCMS.Application.DTOs.Auth;
using SarvashresthaCMS.Application.Common;
using System.Threading.Tasks;

namespace SarvashresthaCMS.Application.Interfaces;

public interface IAuthService
{
    Task<ServiceResponse<AuthResponse>> RegisterAsync(RegisterRequest request);
    Task<ServiceResponse<AuthResponse>> RegisterStaffAsync(RegisterStaffRequest request);
    Task<ServiceResponse<AuthResponse>> LoginAsync(LoginRequest request);
    Task<ServiceResponse<AuthResponse>> RefreshTokenAsync(RefreshTokenRequest request);
}
