using SarvashresthaCMS.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SarvashresthaCMS.Application.Interfaces;

public interface IBookingRepository
{
    Task<IEnumerable<Booking>> GetAllAsync();
    Task<Booking?> GetByIdAsync(int id);
    Task<int> CreateAsync(Booking booking);
    Task<bool> UpdateStatusAsync(int id, string status);
    Task<bool> DeleteAsync(int id);
    Task<Booking?> GetbyuserId(int userId);

    // Dashboard methods
    Task<decimal> GetTotalRevenueAsync();
    Task<int> GetCountByStatusAsync(string status);
    Task<int> GetNewBookingsCountAsync(int days);
    Task<IEnumerable<(string Month, decimal Revenue)>> GetMonthlyRevenueAsync(int months);
    Task<IEnumerable<Booking>> GetRecentBookingsAsync(int count);
}
