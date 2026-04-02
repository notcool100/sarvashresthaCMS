using SarvashresthaCMS.Domain.Entities;
using System.Threading.Tasks;

namespace SarvashresthaCMS.Application.Interfaces;

public interface IUserRepository
{
    Task<User?> GetByEmailAsync(string email);
    Task<User?> GetByIdAsync(int id);
    Task<int> CreateAsync(User user);
    Task UpdateRefreshTokenAsync(int userId, string? refreshToken, DateTime? expiryTime);
    Task<User?> GetByRefreshTokenAsync(string refreshToken);
}
