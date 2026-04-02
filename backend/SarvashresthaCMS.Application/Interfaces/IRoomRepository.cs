using SarvashresthaCMS.Domain.Entities;

namespace SarvashresthaCMS.Application.Interfaces;

public interface IRoomRepository
{
    Task<IEnumerable<Room>> GetAllAsync();
    Task<Room?> GetByIdAsync(int id);
    Task<int> CreateAsync(Room room);
    Task<bool> UpdateAsync(Room room);
    Task<bool> DeleteAsync(int id);
    
    // Dashboard methods
    Task<double> GetOccupancyRateAsync();
    Task<IEnumerable<Room>> GetRecentRoomsAsync(int count);
}
