using Dapper;
using SarvashresthaCMS.Application.Interfaces;
using SarvashresthaCMS.Domain.Entities;
using SarvashresthaCMS.Domain.Enums;
using System.Data;
using System.Threading.Tasks;

namespace SarvashresthaCMS.Infrastructure.Repositories;

public class UserRepository(IDbConnectionFactory dbConnectionFactory) : IUserRepository
{
    private readonly IDbConnectionFactory _dbConnectionFactory = dbConnectionFactory;

    public async Task<User?> GetByEmailAsync(string email)
    {
        using var connection = _dbConnectionFactory.CreateConnection();
        return await connection.QueryFirstOrDefaultAsync<User>(
            "SELECT * FROM get_user_by_email(@Email)", 
            new { Email = email }, 
            commandType: CommandType.Text);
    }

    public async Task<User?> GetByIdAsync(int id)
    {
        using var connection = _dbConnectionFactory.CreateConnection();
        return await connection.QueryFirstOrDefaultAsync<User>(
            "SELECT * FROM get_user_by_id(@Id)", 
            new { Id = id }, 
            commandType: CommandType.Text);
    }

    public async Task<int> CreateAsync(User user)
    {
        using var connection = _dbConnectionFactory.CreateConnection();
        var parameters = new DynamicParameters();
        parameters.Add("p_username", user.Username);
        parameters.Add("p_email", user.Email);
        parameters.Add("p_password_hash", user.PasswordHash);
        parameters.Add("p_role_id", (int)user.Role); // Renamed parameter to p_role_id

        return await connection.ExecuteScalarAsync<int>(
            "SELECT create_user(@p_username, @p_email, @p_password_hash, @p_role_id)", 
            parameters, 
            commandType: CommandType.Text);
    }

    public async Task UpdateRefreshTokenAsync(int userId, string? refreshToken, DateTime? expiryTime)
    {
        using var connection = _dbConnectionFactory.CreateConnection();
        await connection.ExecuteAsync(
            "SELECT update_user_refresh_token(@UserId, @RefreshToken, @ExpiryTime)", 
            new { UserId = userId, RefreshToken = refreshToken, ExpiryTime = expiryTime }, 
            commandType: CommandType.Text);
    }

    public async Task<User?> GetByRefreshTokenAsync(string refreshToken)
    {
        using var connection = _dbConnectionFactory.CreateConnection();
        return await connection.QueryFirstOrDefaultAsync<User>(
            "SELECT * FROM get_user_by_refresh_token(@RefreshToken)", 
            new { RefreshToken = refreshToken }, 
            commandType: CommandType.Text);
    }
}
