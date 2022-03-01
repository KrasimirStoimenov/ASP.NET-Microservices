namespace Catalog.Data.Data.Interfaces;

using Catalog.Data.Models;
using MongoDB.Driver;

public interface ICatalogContext
{
    IMongoCollection<ProductDataModel> Products { get; }
}
