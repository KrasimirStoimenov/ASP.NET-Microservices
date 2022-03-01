namespace Catalog.Repositories.Product;

using Catalog.Data.Data.Interfaces;
using Catalog.Data.Models;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

public class ProductRepository : IProductRepository
{
    private readonly ICatalogContext context;

    public ProductRepository(ICatalogContext context)
    {
        this.context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<ProductDataModel> GetProduct(string id)
        => await this.context
                        .Products
                        .Find(p => p.Id == id)
                        .FirstOrDefaultAsync();

    public async Task<IEnumerable<ProductDataModel>> GetProducts()
        => await this.context
                        .Products
                        .Find(p => true)
                        .ToListAsync();

    public async Task<IEnumerable<ProductDataModel>> GetProductsByName(string name)
    {
        //FilterDefinition<Product> filter = Builders<Product>.Filter.Eq(p => p.Name, name);
        return await this.context
                        .Products
                        .Find(p => p.Name == name)
                        .ToListAsync();
    }

    public async Task<IEnumerable<ProductDataModel>> GetProductsByCategory(string category)
        => await this.context
                        .Products
                        .Find(p => p.Category == category)
                        .ToListAsync();

    public async Task CreateProduct(ProductDataModel product)
    {
        await this.context.Products.InsertOneAsync(product);
    }

    public async Task<bool> UpdateProduct(ProductDataModel product)
    {
        var updateResult = await this.context
                                        .Products
                                        .ReplaceOneAsync(filter: p => p.Id == product.Id, replacement: product);

        return updateResult.IsAcknowledged
                && updateResult.ModifiedCount > 0;
    }

    public async Task<bool> DeleteProduct(string id)
    {
        var deleteResult = await this.context
                                        .Products
                                        .DeleteOneAsync(p => p.Id == id);

        return deleteResult.IsAcknowledged
                && deleteResult.DeletedCount > 0;
    }
}
