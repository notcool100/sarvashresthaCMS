using Dapper;
using SarvashresthaCMS.Application.Interfaces;
using SarvashresthaCMS.Domain.Entities;
using System.Data;

namespace SarvashresthaCMS.Infrastructure.Repositories;

public class ResortRepository(IDbConnectionFactory dbConnectionFactory) : IResortRepository
{
    private readonly IDbConnectionFactory _dbConnectionFactory = dbConnectionFactory;

    public async Task<IEnumerable<Resort>> GetAllAsync()
    {
        using var connection = _dbConnectionFactory.CreateConnection();
        // Assuming database procedure: get_all_resorts()
        return await connection.QueryAsync<Resort>("SELECT * FROM get_all_resorts()", commandType: CommandType.Text);
    }

    public async Task<Resort?> GetByIdAsync(int id)
    {
        using var connection = _dbConnectionFactory.CreateConnection();
        // Assuming database procedure: get_resort_by_id(p_id)
        return await connection.QueryFirstOrDefaultAsync<Resort>("SELECT * FROM get_resort_by_id(@Id)", new { Id = id }, commandType: CommandType.Text);
    }

    public async Task<int> CreateAsync(Resort resort)
    {
        using var connection = _dbConnectionFactory.CreateConnection();
        // Assuming database procedure: create_resort(...)
        var parameters = new DynamicParameters();
        parameters.Add("p_name", resort.Name);
        parameters.Add("p_location", resort.Location);
        parameters.Add("p_description", resort.Description);
        parameters.Add("p_price_per_night", resort.PricePerNight);
        parameters.Add("p_capacity", resort.Capacity);
        parameters.Add("p_is_available", resort.IsAvailable);

        return await connection.ExecuteScalarAsync<int>("SELECT create_resort(@p_name, @p_location, @p_description, @p_price_per_night, @p_capacity, @p_is_available)", parameters, commandType: CommandType.Text);
    }

    public async Task<bool> UpdateAsync(Resort resort)
    {
        using var connection = _dbConnectionFactory.CreateConnection();
        // Assuming database procedure: update_resort(...)
        var parameters = new DynamicParameters();
        parameters.Add("p_id", resort.Id);
        parameters.Add("p_name", resort.Name);
        parameters.Add("p_location", resort.Location);
        parameters.Add("p_description", resort.Description);
        parameters.Add("p_price_per_night", resort.PricePerNight);
        parameters.Add("p_capacity", resort.Capacity);
        parameters.Add("p_is_available", resort.IsAvailable);

        var rowsAffected = await connection.ExecuteAsync("SELECT update_resort(@p_id, @p_name, @p_location, @p_description, @p_price_per_night, @p_capacity, @p_is_available)", parameters, commandType: CommandType.Text);
        return rowsAffected > 0;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        using var connection = _dbConnectionFactory.CreateConnection();
        // Assuming database procedure: delete_resort(p_id)
        var rowsAffected = await connection.ExecuteAsync("SELECT delete_resort(@Id)", new { Id = id }, commandType: CommandType.Text);
        return rowsAffected > 0;
    }
}
