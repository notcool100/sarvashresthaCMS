using Dapper;
using SarvashresthaCMS.Application.Interfaces;
using SarvashresthaCMS.Domain.Entities;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace SarvashresthaCMS.Infrastructure.Repositories;

public class BookingRepository(IDbConnectionFactory dbConnectionFactory) : IBookingRepository
{
    private readonly IDbConnectionFactory _dbConnectionFactory = dbConnectionFactory;

    public async Task<IEnumerable<Booking>> GetAllAsync()
    {
        using var connection = _dbConnectionFactory.CreateConnection();
        return await connection.QueryAsync<Booking>("SELECT * FROM get_all_bookings()", commandType: CommandType.Text);
    }

    public async Task<Booking?> GetByIdAsync(int id)
    {
        using var connection = _dbConnectionFactory.CreateConnection();
        return await connection.QueryFirstOrDefaultAsync<Booking>(
            "SELECT * FROM get_booking_by_id(@p_id)",
            new { p_id = id },
            commandType: CommandType.Text
        );
    }

    public async Task<int> CreateAsync(Booking booking)
    {
        using var connection = _dbConnectionFactory.CreateConnection();
        var parameters = new
        {
            p_resort_id = booking.ResortId,
            p_guest_name = booking.GuestName,
            p_guest_email = booking.guestEmail,
            p_check_in = booking.CheckIn,
            p_check_out = booking.CheckOut,
            p_total_price = booking.TotalPrice
        };

        return await connection.ExecuteScalarAsync<int>(
            "SELECT create_booking(@p_resort_id, @p_guest_name, @p_guest_email, @p_check_in, @p_check_out, @p_total_price)",
            parameters,
            commandType: CommandType.Text
        );
    }

    public async Task<bool> UpdateStatusAsync(int id, string status)
    {
        using var connection = _dbConnectionFactory.CreateConnection();
        // Since update_booking_status now returns INTEGER, we use ExecuteScalarAsync to get the result.
        var affectedRows = await connection.ExecuteScalarAsync<int>(
            "SELECT update_booking_status(@p_id, @p_status)",
            new { p_id = id, p_status = status },
            commandType: CommandType.Text
        );
        return affectedRows > 0;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        using var connection = _dbConnectionFactory.CreateConnection();
        // Since delete_booking now returns INTEGER, we use ExecuteScalarAsync to get the result.
        var affectedRows = await connection.ExecuteScalarAsync<int>(
            "SELECT delete_booking(@p_id)",
            new { p_id = id },
            commandType: CommandType.Text
        );
        return affectedRows > 0;
    }
}
