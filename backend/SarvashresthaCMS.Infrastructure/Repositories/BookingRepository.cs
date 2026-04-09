using Dapper;
using SarvashresthaCMS.Application.Interfaces;
using SarvashresthaCMS.Domain.Entities;
using System.Collections.Generic;
using System.Data;
using System.Linq;
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
            p_room_id = booking.RoomId,
            p_offer_id = booking.OfferId,
            p_guest_name = booking.GuestName,
            p_guest_email = booking.guestEmail,
            p_check_in = booking.CheckIn,
            p_check_out = booking.CheckOut,
            p_total_price = booking.TotalPrice,
            p_discount_amount = booking.DiscountAmount,
            p_final_price = booking.FinalPrice,
            p_user_id = booking.UserId
        };

        return await connection.ExecuteScalarAsync<int>(
            "SELECT create_booking(@p_room_id, @p_offer_id, @p_guest_name, @p_guest_email, @p_check_in, @p_check_out, @p_total_price, @p_discount_amount, @p_final_price, @p_user_id)",
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

    public async Task<decimal> GetTotalRevenueAsync()
    {
        using var connection = _dbConnectionFactory.CreateConnection();
        return await connection.ExecuteScalarAsync<decimal>(
            "SELECT COALESCE(SUM(total_price), 0) FROM bookings WHERE status IN ('Confirmed', 'Completed')"
        );
    }

    public async Task<int> GetCountByStatusAsync(string status)
    {
        using var connection = _dbConnectionFactory.CreateConnection();
        return await connection.ExecuteScalarAsync<int>(
            "SELECT COUNT(*) FROM bookings WHERE status = @p_status",
            new { p_status = status }
        );
    }

    public async Task<int> GetNewBookingsCountAsync(int days)
    {
        using var connection = _dbConnectionFactory.CreateConnection();
        return await connection.ExecuteScalarAsync<int>(
            "SELECT COUNT(*) FROM bookings WHERE created_at >= CURRENT_DATE - INTERVAL '1 day' * @p_days",
            new { p_days = days }
        );
    }

    public async Task<IEnumerable<(string Month, decimal Revenue)>> GetMonthlyRevenueAsync(int months)
    {
        using var connection = _dbConnectionFactory.CreateConnection();
        var sql = @"
            SELECT 
                TO_CHAR(date_trunc('month', created_at), 'Mon') as Month,
                SUM(total_price) as Revenue
            FROM bookings
            WHERE created_at >= CURRENT_DATE - INTERVAL '1 month' * @p_months
            AND status IN ('Confirmed', 'Completed')
            GROUP BY date_trunc('month', created_at)
            ORDER BY date_trunc('month', created_at)";

        var results = await connection.QueryAsync<dynamic>(sql, new { p_months = months });
        return results.Select(r => ((string)r.month, (decimal)r.revenue));
    }

    public async Task<IEnumerable<Booking>> GetRecentBookingsAsync(int count)
    {
        using var connection = _dbConnectionFactory.CreateConnection();
        return await connection.QueryAsync<Booking>(
            "SELECT * FROM bookings ORDER BY created_at DESC LIMIT @p_count",
            new { p_count = count }
        );
    }
}
