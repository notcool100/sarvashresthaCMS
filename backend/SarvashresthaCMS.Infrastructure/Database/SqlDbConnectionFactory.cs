using System.Data;
using Microsoft.Extensions.Configuration;
using Npgsql;
using SarvashresthaCMS.Application.Interfaces;

namespace SarvashresthaCMS.Infrastructure.Database;

public class SqlDbConnectionFactory(IConfiguration configuration) : IDbConnectionFactory
{
    public IDbConnection CreateConnection()
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection") 
                               ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
        return new NpgsqlConnection(connectionString);
    }
}
