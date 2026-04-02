using System.Data;

namespace SarvashresthaCMS.Application.Interfaces;

public interface IDbConnectionFactory
{
    IDbConnection CreateConnection();
}
