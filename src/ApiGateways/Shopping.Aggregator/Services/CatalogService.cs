namespace Shopping.Aggregator.Services;

using System.Collections.Generic;
using System.Threading.Tasks;

using Shopping.Aggregator.Models;
using Shopping.Aggregator.Services.Interfaces;

public class CatalogService : ICatalogService
{
    private readonly HttpClient client;

    public CatalogService(HttpClient client)
    {
        this.client = client ?? throw new ArgumentNullException(nameof(client));
    }

    public Task<CatalogModel> GetCatalog(string id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<CatalogModel>> GetCatalog()
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<CatalogModel>> GetCatalogByCategory(string category)
    {
        throw new NotImplementedException();
    }
}
