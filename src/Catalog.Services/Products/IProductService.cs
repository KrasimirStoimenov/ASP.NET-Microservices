namespace Catalog.Services.Products;

using Catalog.Services.Products.Models;

public interface IProductService
{
    Task<ProductModel> GetProductAsync(string id);

    Task<IEnumerable<ProductModel>> GetProductsAsync();

    Task<IEnumerable<ProductModel>> GetProductsByNameAsync(string name);

    Task<IEnumerable<ProductModel>> GetProductsByCategoryAsync(string category);

    Task CreateProductAsync(ProductModel product);

    Task<bool> UpdateProductAsync(ProductModel product);

    Task<bool> DeleteProductAsync(string id);
}
