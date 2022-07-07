namespace AspnetRunBasics.Services;

using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

using AspnetRunBasics.Extensions;
using AspnetRunBasics.Models;
using AspnetRunBasics.Services.Interfaces;

public class CatalogService : ICatalogService
{
    private readonly HttpClient client;

    public CatalogService(HttpClient client)
    {
        this.client = client ?? throw new ArgumentNullException(nameof(client));
    }

    public async Task<CatalogModel> GetCatalogAsync(string id)
    {
        var response = await this.client.GetAsync($"/Catalog/{id}");
        return await response.ReadContentAs<CatalogModel>();
    }

    public async Task<IEnumerable<CatalogModel>> GetCatalogAsync()
    {
        var response = await this.client.GetAsync("/Catalog");
        return await response.ReadContentAs<IEnumerable<CatalogModel>>();
    }

    public async Task<IEnumerable<CatalogModel>> GetCatalogByCategoryAsync(string category)
    {
        var response = await this.client.GetAsync($"/Catalog/GetProductsByCategory/{category}");
        return await response.ReadContentAs<IEnumerable<CatalogModel>>();
    }
    public async Task<IEnumerable<CatalogModel>> GetCatalogByNameAsync(string name)
    {
        var response = await this.client.GetAsync($"/Catalog/GetProductsByName/{name}");
        return await response.ReadContentAs<IEnumerable<CatalogModel>>();
    }

    public async Task<CatalogModel> CreateCatalogAsync(CatalogModel model)
    {
        var response = await this.client.PostAsJson("/Catalog", model);
        if (response.IsSuccessStatusCode)
        {
            return await response.ReadContentAs<CatalogModel>();
        }
        else
        {
            throw new Exception("Something went wrong when calling api.");
        }
    }
}
