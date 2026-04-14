using Dapper;
using SarvashresthaCMS.Application.Interfaces;
using SarvashresthaCMS.Domain.Entities;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace SarvashresthaCMS.Infrastructure.Repositories;

public class GalleryRepository(IDbConnectionFactory dbConnectionFactory) : IGalleryRepository
{
    private readonly IDbConnectionFactory _dbConnectionFactory = dbConnectionFactory;

    public async Task<IEnumerable<GalleryItem>> GetAllAsync()
    {
        using var connection = _dbConnectionFactory.CreateConnection();
        return await connection.QueryAsync<GalleryItem>("SELECT * FROM get_all_gallery_images()", commandType: CommandType.Text);
    }

    public async Task<int> CreateAsync(GalleryItem item)
    {
        using var connection = _dbConnectionFactory.CreateConnection();
        var parameters = new DynamicParameters();
        parameters.Add("p_image_url", item.ImageUrl);
        parameters.Add("p_alt_text", item.AltText);
        parameters.Add("p_category", item.Category);
        parameters.Add("p_display_order", item.DisplayOrder);

        return await connection.ExecuteScalarAsync<int>("SELECT create_gallery_image(@p_image_url, @p_alt_text, @p_category, @p_display_order)", parameters, commandType: CommandType.Text);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        using var connection = _dbConnectionFactory.CreateConnection();
        return await connection.ExecuteScalarAsync<bool>("SELECT delete_gallery_image(@Id)", new { Id = id }, commandType: CommandType.Text);
    }
}
