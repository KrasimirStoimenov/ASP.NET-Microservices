namespace Discount.Data.DataContext;

using System.Data;

using Microsoft.Extensions.Configuration;

using Npgsql;

public class DiscountContext : IDiscountContext
{
    private readonly IConfiguration configuration;

    public DiscountContext(IConfiguration configuration)
    {
        this.configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
    }

    public IDbConnection OpenDbConnection()
    {
        var connectionString = this.configuration.GetSection("DatabaseSettings")["ConnectionString"];
        var connection = new NpgsqlConnection(connectionString);
        connection.Open();
        return connection;
    }
}
