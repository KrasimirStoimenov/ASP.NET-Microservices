namespace Catalog.Repositories.Product;

using Catalog.Data.Models;

public interface IProductRepository
{
    Task<ProductDataModel> GetProduct(string id);

    Task<IEnumerable<ProductDataModel>> GetProducts();

    Task<IEnumerable<ProductDataModel>> GetProductsByName(string name);

    Task<IEnumerable<ProductDataModel>> GetProductsByCategory(string category);

    Task CreateProduct(ProductDataModel product);

    Task<bool> UpdateProduct(ProductDataModel product);

    Task<bool> DeleteProduct(string id);

}
