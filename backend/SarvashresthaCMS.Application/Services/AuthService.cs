using SarvashresthaCMS.Application.Common;
using SarvashresthaCMS.Application.DTOs.Auth;
using SarvashresthaCMS.Application.Interfaces;
using SarvashresthaCMS.Domain.Entities;
using SarvashresthaCMS.Domain.Enums;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SarvashresthaCMS.Application.Services;

public class AuthService(IUserRepository userRepository, IJwtTokenGenerator jwtTokenGenerator) : IAuthService
{
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IJwtTokenGenerator _jwtTokenGenerator = jwtTokenGenerator;

    public async Task<ServiceResponse<AuthResponse>> RegisterAsync(RegisterRequest request)
    {
        var existingUser = await _userRepository.GetByEmailAsync(request.Email);
        if (existingUser != null)
            return ServiceResponse<AuthResponse>.Fail("Email already exists.");

        var user = new User
        {
            Username = request.Username,
            Email = request.Email,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password),
            Role = UserRole.Customer
        };

        var userId = await _userRepository.CreateAsync(user);
        user.Id = userId;

        var accessToken = _jwtTokenGenerator.GenerateAccessToken(user);
        var refreshToken = _jwtTokenGenerator.GenerateRefreshToken();

        await _userRepository.UpdateRefreshTokenAsync(userId, refreshToken, DateTime.UtcNow.AddDays(7));

        var data = new AuthResponse(accessToken, refreshToken, user.Username, user.Email, user.Role);
        return ServiceResponse<AuthResponse>.Ok(data, "Registration successful.");
    }

    public async Task<ServiceResponse<AuthResponse>> RegisterStaffAsync(RegisterStaffRequest request)
    {
        var existingUser = await _userRepository.GetByEmailAsync(request.Email);
        if (existingUser != null)
            return ServiceResponse<AuthResponse>.Fail("User with this email already exists.");

        var user = new User
        {
            Username = request.Username,
            Email = request.Email,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password),
            Role = request.Role // Explicitly allow passing the Role
        };

        var userId = await _userRepository.CreateAsync(user);
        user.Id = userId;

        var accessToken = _jwtTokenGenerator.GenerateAccessToken(user);
        var refreshToken = _jwtTokenGenerator.GenerateRefreshToken();

        await _userRepository.UpdateRefreshTokenAsync(userId, refreshToken, DateTime.UtcNow.AddDays(7));

        var data = new AuthResponse(accessToken, refreshToken, user.Username, user.Email, user.Role);
        return ServiceResponse<AuthResponse>.Ok(data, $"Staff/Admin user '{user.Username}' created successfully.");
    }

    public async Task<ServiceResponse<AuthResponse>> LoginAsync(LoginRequest request)
    {
        var user = await _userRepository.GetByEmailAsync(request.Email);
        if (user == null || !BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
            return ServiceResponse<AuthResponse>.Fail("Invalid email or password.");

        var accessToken = _jwtTokenGenerator.GenerateAccessToken(user);
        var refreshToken = _jwtTokenGenerator.GenerateRefreshToken();

        await _userRepository.UpdateRefreshTokenAsync(user.Id, refreshToken, DateTime.UtcNow.AddDays(7));

        var data = new AuthResponse(accessToken, refreshToken, user.Username, user.Email, user.Role);
        return ServiceResponse<AuthResponse>.Ok(data, "Login successful.");
    }

    public async Task<ServiceResponse<AuthResponse>> RefreshTokenAsync(RefreshTokenRequest request)
    {
        var principal = _jwtTokenGenerator.GetPrincipalFromExpiredToken(request.AccessToken);
        if (principal == null)
            return ServiceResponse<AuthResponse>.Fail("Invalid access token.");

        var userIdString = principal.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(userIdString) || !int.TryParse(userIdString, out var userId))
            return ServiceResponse<AuthResponse>.Fail("Invalid access token claims.");

        var refreshTokenUser = await _userRepository.GetByRefreshTokenAsync(request.RefreshToken);
        if (refreshTokenUser == null || refreshTokenUser.RefreshTokenExpiryTime <= DateTime.UtcNow)
            return ServiceResponse<AuthResponse>.Fail("Invalid or expired refresh token.");

        if (refreshTokenUser.Id != userId)
            return ServiceResponse<AuthResponse>.Fail("Refresh token does not belong to the provided access token.");

        var newAccessToken = _jwtTokenGenerator.GenerateAccessToken(refreshTokenUser);
        var newRefreshToken = _jwtTokenGenerator.GenerateRefreshToken();

        await _userRepository.UpdateRefreshTokenAsync(refreshTokenUser.Id, newRefreshToken, DateTime.UtcNow.AddDays(7));

        var data = new AuthResponse(newAccessToken, newRefreshToken, refreshTokenUser.Username, refreshTokenUser.Email, refreshTokenUser.Role);
        return ServiceResponse<AuthResponse>.Ok(data, "Token refreshed successfully.");
    }
}
