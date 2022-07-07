namespace AspnetRunBasics.Services.Interfaces;

using System.Collections.Generic;
using System.Threading.Tasks;

using AspnetRunBasics.Models;

public interface ICatalogService
{
    Task<CatalogModel> GetCatalogAsync(string id);

    Task<IEnumerable<CatalogModel>> GetCatalogAsync();

    Task<IEnumerable<CatalogModel>> GetCatalogByCategoryAsync(string category);

    Task<CatalogModel> CreateCatalogAsync(CatalogModel model);
}
