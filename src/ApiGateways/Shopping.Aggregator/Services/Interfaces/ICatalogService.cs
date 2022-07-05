namespace Shopping.Aggregator.Services.Interfaces;

using Shopping.Aggregator.Models;

public interface ICatalogService
{
    Task<CatalogModel> GetCatalog(string id);

    Task<IEnumerable<CatalogModel>> GetCatalog();

    Task<IEnumerable<CatalogModel>> GetCatalogByCategory(string category);
}
