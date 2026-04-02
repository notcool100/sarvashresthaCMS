using SarvashresthaCMS.Domain.Entities;
using System.Collections.Generic;
using System.Security.Claims;

namespace SarvashresthaCMS.Application.Interfaces;

public interface IJwtTokenGenerator
{
    string GenerateAccessToken(User user);
    string GenerateRefreshToken();
    ClaimsPrincipal? GetPrincipalFromExpiredToken(string token);
}
