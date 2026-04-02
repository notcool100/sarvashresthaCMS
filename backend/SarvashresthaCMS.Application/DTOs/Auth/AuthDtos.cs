using SarvashresthaCMS.Domain.Enums;

namespace SarvashresthaCMS.Application.DTOs.Auth;

public record LoginRequest(string Email, string Password);
public record RegisterRequest(string Username, string Email, string Password, UserRole Role = UserRole.Customer);
public record RefreshTokenRequest(string AccessToken, string RefreshToken);
public record AuthResponse(string AccessToken, string RefreshToken, string Username, string Email, UserRole Role);
