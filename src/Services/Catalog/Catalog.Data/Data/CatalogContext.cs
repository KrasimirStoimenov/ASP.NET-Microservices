namespace Catalog.Data.Data;

using Catalog.Data.Data.Interfaces;
using Catalog.Data.Models;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

public class CatalogContext : ICatalogContext
{
    public CatalogContext(IConfiguration configuration)
    {
        var client = new MongoClient(configuration.GetSection("DatabaseSettings")["ConnectionString"]);
        var database = client.GetDatabase(configuration.GetSection("DatabaseSettings")["DatabaseName"]);

        Products = database.GetCollection<ProductDataModel>(configuration.GetSection("DatabaseSettings")["CollectionName"]);

        CatalogContextSeeder.SeedData(Products);
    }
    public IMongoCollection<ProductDataModel> Products { get; }
}
