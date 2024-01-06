using System.Data;

namespace Product.Application.Abstract;

public interface IDbConnectionFactory
{
    IDbConnection GetConnection();
}
