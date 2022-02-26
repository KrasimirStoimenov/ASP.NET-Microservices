namespace Catalog.API.Repositories;

using Catalog.API.Entities;

public interface IProductRepository
{
    Task<Product> GetProduct(string id);

    Task<IEnumerable<Product>> GetProducts();

    Task<IEnumerable<Product>> GetProductsByName(string name);

    Task<IEnumerable<Product>> GetProductsByCategory(string category);

    Task CreateProduct(Product product);

    Task<bool> UpdateProduct(Product product);

    Task<bool> DeleteProduct(string id);

}
