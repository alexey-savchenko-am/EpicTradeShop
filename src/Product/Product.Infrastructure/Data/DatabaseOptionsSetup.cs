using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Persistence;

namespace Product.Infrastructure.Data;

internal class DatabaseOptionsSetup
    : IConfigureOptions<DatabaseOptions>
{
    private const string ConfigurationSectionName = "DatabaseOptions";
    private readonly IConfiguration _configuration;

    public DatabaseOptionsSetup(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    public void Configure(DatabaseOptions options)
    {
        var connectionString = _configuration.GetConnectionString("ProductDb");
        if (connectionString is not null)
        {
            options.ConnectionString = connectionString;
        }

        _configuration.GetSection(ConfigurationSectionName).Bind(options);

    }
}
