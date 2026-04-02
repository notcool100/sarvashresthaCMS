using SarvashresthaCMS.Domain.Entities;

namespace SarvashresthaCMS.Application.Interfaces;

public interface IResortRepository
{
    Task<IEnumerable<Resort>> GetAllAsync();
    Task<Resort?> GetByIdAsync(int id);
    Task<int> CreateAsync(Resort resort);
    Task<bool> UpdateAsync(Resort resort);
    Task<bool> DeleteAsync(int id);
}
