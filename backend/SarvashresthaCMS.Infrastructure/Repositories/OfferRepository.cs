using Dapper;
using SarvashresthaCMS.Application.Interfaces;
using SarvashresthaCMS.Domain.Entities;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace SarvashresthaCMS.Infrastructure.Repositories;

public class OfferRepository(IDbConnectionFactory dbConnectionFactory) : IOfferRepository
{
    private readonly IDbConnectionFactory _dbConnectionFactory = dbConnectionFactory;

    public async Task<IEnumerable<Offer>> GetAllAsync()
    {
        using var connection = _dbConnectionFactory.CreateConnection();
        return await connection.QueryAsync<Offer>("SELECT * FROM get_all_offers()", commandType: CommandType.Text);
    }

    public async Task<Offer?> GetByIdAsync(int id)
    {
        using var connection = _dbConnectionFactory.CreateConnection();
        return await connection.QueryFirstOrDefaultAsync<Offer>(
            "SELECT * FROM get_offer_by_id(@Id)",
            new { Id = id },
            commandType: CommandType.Text
        );
    }

    public async Task<IEnumerable<int>> GetRoomIdsAsync(int offerId)
    {
        using var connection = _dbConnectionFactory.CreateConnection();
        return await connection.QueryAsync<int>(
            "SELECT room_id FROM offer_rooms WHERE offer_id = @offer_id",
            new { offer_id = offerId }
        );
    }

    public async Task<int> CreateAsync(Offer offer, IEnumerable<int> roomIds)
    {
        using var connection = _dbConnectionFactory.CreateConnection();
        using var transaction = connection.BeginTransaction();
        try
        {
            var parameters = new DynamicParameters();
            parameters.Add("p_title", offer.Title);
            parameters.Add("p_description", offer.Description);
            parameters.Add("p_discount_type", offer.DiscountType);
            parameters.Add("p_discount_value", offer.DiscountValue);
            parameters.Add("p_start_date", offer.StartDate);
            parameters.Add("p_end_date", offer.EndDate);
            parameters.Add("p_is_active", offer.IsActive);
            parameters.Add("p_applies_to_all_rooms", offer.AppliesToAllRooms);
            parameters.Add("p_image_url", offer.ImageUrl);

            var offerId = await connection.ExecuteScalarAsync<int>(
                "SELECT create_offer(@p_title, @p_description, @p_discount_type, @p_discount_value, @p_start_date, @p_end_date, @p_is_active, @p_applies_to_all_rooms, @p_image_url)",
                parameters,
                transaction,
                commandType: CommandType.Text
            );

            if (!offer.AppliesToAllRooms && roomIds.Any())
            {
                var rows = roomIds.Select(roomId => new { offer_id = offerId, room_id = roomId });
                await connection.ExecuteAsync(
                    "INSERT INTO offer_rooms (offer_id, room_id) VALUES (@offer_id, @room_id)",
                    rows,
                    transaction
                );
            }

            transaction.Commit();
            return offerId;
        }
        catch
        {
            transaction.Rollback();
            throw;
        }
    }

    public async Task<bool> UpdateAsync(Offer offer, IEnumerable<int> roomIds)
    {
        using var connection = _dbConnectionFactory.CreateConnection();
        using var transaction = connection.BeginTransaction();
        try
        {
            var parameters = new DynamicParameters();
            parameters.Add("p_id", offer.Id);
            parameters.Add("p_title", offer.Title);
            parameters.Add("p_description", offer.Description);
            parameters.Add("p_discount_type", offer.DiscountType);
            parameters.Add("p_discount_value", offer.DiscountValue);
            parameters.Add("p_start_date", offer.StartDate);
            parameters.Add("p_end_date", offer.EndDate);
            parameters.Add("p_is_active", offer.IsActive);
            parameters.Add("p_applies_to_all_rooms", offer.AppliesToAllRooms);
            parameters.Add("p_image_url", offer.ImageUrl);

            var rowsAffected = await connection.ExecuteAsync(
                "SELECT update_offer(@p_id, @p_title, @p_description, @p_discount_type, @p_discount_value, @p_start_date, @p_end_date, @p_is_active, @p_applies_to_all_rooms, @p_image_url)",
                parameters,
                transaction,
                commandType: CommandType.Text
            );

            await connection.ExecuteAsync(
                "DELETE FROM offer_rooms WHERE offer_id = @offer_id",
                new { offer_id = offer.Id },
                transaction
            );

            if (!offer.AppliesToAllRooms && roomIds.Any())
            {
                var rows = roomIds.Select(roomId => new { offer_id = offer.Id, room_id = roomId });
                await connection.ExecuteAsync(
                    "INSERT INTO offer_rooms (offer_id, room_id) VALUES (@offer_id, @room_id)",
                    rows,
                    transaction
                );
            }

            transaction.Commit();
            return rowsAffected > 0;
        }
        catch
        {
            transaction.Rollback();
            throw;
        }
    }

    public async Task<bool> DeleteAsync(int id)
    {
        using var connection = _dbConnectionFactory.CreateConnection();
        using var transaction = connection.BeginTransaction();
        try
        {
            await connection.ExecuteAsync(
                "DELETE FROM offer_rooms WHERE offer_id = @offer_id",
                new { offer_id = id },
                transaction
            );

            var rowsAffected = await connection.ExecuteAsync(
                "SELECT delete_offer(@p_id)",
                new { p_id = id },
                transaction,
                commandType: CommandType.Text
            );

            transaction.Commit();
            return rowsAffected > 0;
        }
        catch
        {
            transaction.Rollback();
            throw;
        }
    }
}
