namespace Catalog.Services.Products;

using Catalog.Services.Products.Models;

public interface IProductService
{
    Task<ProductModel> GetProduct(string id);

    Task<IEnumerable<ProductModel>> GetProducts();

    Task<IEnumerable<ProductModel>> GetProductsByName(string name);

    Task<IEnumerable<ProductModel>> GetProductsByCategory(string category);

    Task CreateProduct(ProductModel product);

    Task<bool> UpdateProduct(ProductModel product);

    Task<bool> DeleteProduct(string id);
}
