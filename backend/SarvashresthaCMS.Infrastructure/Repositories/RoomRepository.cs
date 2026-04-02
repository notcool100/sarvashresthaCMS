using Dapper;
using SarvashresthaCMS.Application.Interfaces;
using SarvashresthaCMS.Domain.Entities;
using System.Data;

namespace SarvashresthaCMS.Infrastructure.Repositories;

public class RoomRepository(IDbConnectionFactory dbConnectionFactory) : IRoomRepository
{
    private readonly IDbConnectionFactory _dbConnectionFactory = dbConnectionFactory;

    public async Task<IEnumerable<Room>> GetAllAsync()
    {
        using var connection = _dbConnectionFactory.CreateConnection();
        // Assuming database procedure: get_all_rooms()
        return await connection.QueryAsync<Room>("SELECT * FROM get_all_rooms()", commandType: CommandType.Text);
    }

    public async Task<Room?> GetByIdAsync(int id)
    {
        using var connection = _dbConnectionFactory.CreateConnection();
        // Assuming database procedure: get_room_by_id(p_id)
        return await connection.QueryFirstOrDefaultAsync<Room>("SELECT * FROM get_room_by_id(@Id)", new { Id = id }, commandType: CommandType.Text);
    }

    public async Task<int> CreateAsync(Room room)
    {
        using var connection = _dbConnectionFactory.CreateConnection();
        // Assuming database procedure: create_room(...)
        var parameters = new DynamicParameters();
        parameters.Add("p_name", room.Name);
        parameters.Add("p_location", room.Location);
        parameters.Add("p_description", room.Description);
        parameters.Add("p_price_per_night", room.PricePerNight);
        parameters.Add("p_capacity", room.Capacity);
        parameters.Add("p_is_available", room.IsAvailable);
        parameters.Add("p_view_type", room.ViewType);
        parameters.Add("p_amenities", room.Amenities);
        parameters.Add("p_image_urls", room.ImageUrls);
        parameters.Add("p_bed_type", room.BedType);
        parameters.Add("p_size_sqft", room.SizeSqFt);

        return await connection.ExecuteScalarAsync<int>("SELECT create_room(@p_name, @p_location, @p_description, @p_price_per_night, @p_capacity, @p_is_available, @p_view_type, @p_amenities, @p_image_urls, @p_bed_type, @p_size_sqft)", parameters, commandType: CommandType.Text);
    }

    public async Task<bool> UpdateAsync(Room room)
    {
        using var connection = _dbConnectionFactory.CreateConnection();
        // Assuming database procedure: update_room(...)
        var parameters = new DynamicParameters();
        parameters.Add("p_id", room.Id);
        parameters.Add("p_name", room.Name);
        parameters.Add("p_location", room.Location);
        parameters.Add("p_description", room.Description);
        parameters.Add("p_price_per_night", room.PricePerNight);
        parameters.Add("p_capacity", room.Capacity);
        parameters.Add("p_is_available", room.IsAvailable);
        parameters.Add("p_view_type", room.ViewType);
        parameters.Add("p_amenities", room.Amenities);
        parameters.Add("p_image_urls", room.ImageUrls);
        parameters.Add("p_bed_type", room.BedType);
        parameters.Add("p_size_sqft", room.SizeSqFt);

        var rowsAffected = await connection.ExecuteAsync("SELECT update_room(@p_id, @p_name, @p_location, @p_description, @p_price_per_night, @p_capacity, @p_is_available, @p_view_type, @p_amenities, @p_image_urls, @p_bed_type, @p_size_sqft)", parameters, commandType: CommandType.Text);
        return rowsAffected > 0;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        using var connection = _dbConnectionFactory.CreateConnection();
        // Assuming database procedure: delete_room(p_id)
        var rowsAffected = await connection.ExecuteAsync("SELECT delete_room(@Id)", new { Id = id }, commandType: CommandType.Text);
        return rowsAffected > 0;
    }

    public async Task<double> GetOccupancyRateAsync()
    {
        using var connection = _dbConnectionFactory.CreateConnection();
        // Occupancy rate = (Booked Rooms / Total Rooms) * 100
        // A room is considered "booked" if it has an active booking today.
        var sql = @"
            SELECT 
                (CAST(COUNT(DISTINCT room_id) AS DOUBLE PRECISION) / 
                NULLIF((SELECT COUNT(*) FROM rooms), 0)) * 100
            FROM bookings
            WHERE status IN ('Confirmed', 'Completed')
            AND CURRENT_DATE BETWEEN check_in AND check_out";
        
        return await connection.ExecuteScalarAsync<double>(sql);
    }

    public async Task<IEnumerable<Room>> GetRecentRoomsAsync(int count)
    {
        using var connection = _dbConnectionFactory.CreateConnection();
        return await connection.QueryAsync<Room>(
            "SELECT * FROM rooms ORDER BY created_at DESC LIMIT @p_count",
            new { p_count = count }
        );
    }
}
