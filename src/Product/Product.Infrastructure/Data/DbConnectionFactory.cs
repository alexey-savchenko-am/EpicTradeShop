using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using Persistence;
using Product.Application.Abstract;
using System.Data;

namespace Product.Infrastructure.Data;

internal sealed class DbConnectionFactory
    : IDbConnectionFactory
{
    private readonly IOptions<DatabaseOptions> _options;

    public DbConnectionFactory(IOptions<DatabaseOptions> options)
    {
        _options = options;
    }
    public IDbConnection GetConnection()
    {
        var connection = new SqlConnection(_options.Value.ConnectionString);
        connection.Open();
        return connection;
    }
}
