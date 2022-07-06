namespace Shopping.Aggregator.Services;

using System.Collections.Generic;
using System.Threading.Tasks;

using Shopping.Aggregator.Extensions;
using Shopping.Aggregator.Models;
using Shopping.Aggregator.Services.Interfaces;

public class CatalogService : ICatalogService
{
    private readonly HttpClient client;

    public CatalogService(HttpClient client)
    {
        this.client = client ?? throw new ArgumentNullException(nameof(client));
    }

    public async Task<CatalogModel> GetCatalog(string id)
    {
        var response = await this.client.GetAsync($"/api/v1/Catalog/{id}");
        return await response.ReadContentAs<CatalogModel>();
    }

    public async Task<IEnumerable<CatalogModel>> GetCatalog()
    {
        var response = await this.client.GetAsync("/api/v1/Catalog");
        return await response.ReadContentAs<IEnumerable<CatalogModel>>();
    }

    public async Task<IEnumerable<CatalogModel>> GetCatalogByCategory(string category)
    {
        var response = await this.client.GetAsync($"/api/v1/Catalog/GetProductsByCategory/{category}");
        return await response.ReadContentAs<IEnumerable<CatalogModel>>();
    }

    public async Task<IEnumerable<CatalogModel>> GetCatalogByName(string name)
    {
        var response = await this.client.GetAsync($"/api/v1/Catalog/GetProductsByName/{name}");
        return await response.ReadContentAs<IEnumerable<CatalogModel>>();
    }
}
