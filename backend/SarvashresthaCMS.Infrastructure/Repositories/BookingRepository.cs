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
        return await connection.QueryAsync<Booking>(
            "SELECT * FROM get_all_bookings()", 
            commandType: CommandType.Text);
    }

    public async Task<Booking?> GetByIdAsync(int id)
    {
        using var connection = _dbConnectionFactory.CreateConnection();
        return await connection.QueryFirstOrDefaultAsync<Booking>(
            "SELECT * FROM get_booking_by_id(@Id)", 
            new { Id = id }, 
            commandType: CommandType.Text);
    }

    public async Task<int> CreateAsync(Booking booking)
    {
        using var connection = _dbConnectionFactory.CreateConnection();
        var parameters = new DynamicParameters();
        parameters.Add("p_resort_id", booking.ResortId);
        parameters.Add("p_guest_name", booking.GuestName);
        parameters.Add("p_guest_email", booking.guestEmail);
        parameters.Add("p_check_in", booking.CheckIn);
        parameters.Add("p_check_out", booking.CheckOut);
        parameters.Add("p_total_price", booking.TotalPrice);

        return await connection.ExecuteScalarAsync<int>(
            "SELECT create_booking(@p_resort_id, @p_guest_name, @p_guest_email, @p_check_in, @p_check_out, @p_total_price)", 
            parameters, 
            commandType: CommandType.Text);
    }

    public async Task<bool> UpdateStatusAsync(int id, string status)
    {
        using var connection = _dbConnectionFactory.CreateConnection();
        var rowsAffected = await connection.ExecuteAsync(
            "SELECT update_booking_status(@Id, @Status)", 
            new { Id = id, Status = status }, 
            commandType: CommandType.Text);
            
        return rowsAffected > 0;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        using var connection = _dbConnectionFactory.CreateConnection();
        var rowsAffected = await connection.ExecuteAsync(
            "SELECT delete_booking(@Id)", 
            new { Id = id }, 
            commandType: CommandType.Text);
            
        return rowsAffected > 0;
    }
}
