namespace Catalog.Services.Products;

using AutoMapper;
using Catalog.Data.Models;
using Catalog.Repositories.Product;
using Catalog.Services.Products.Models;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

public class ProductService : IProductService
{
    private readonly IProductRepository repository;
    private readonly ILogger<ProductService> logger;
    private readonly IMapper mapper;

    public ProductService(
        IProductRepository repository,
        ILogger<ProductService> logger,
        IMapper mapper)
    {
        this.repository = repository ?? throw new ArgumentNullException(nameof(repository));
        this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<ProductModel> GetProductAsync(string id)
    {
        ProductDataModel dataModel = await this.repository.GetProduct(id);

        if (dataModel == null)
        {
            this.logger.LogError($"Product with id: {id}, not found.");
        }

        ProductModel mappedModel = this.mapper.Map<ProductModel>(dataModel);

        return mappedModel;
    }

    public async Task<IEnumerable<ProductModel>> GetProductsAsync()
    {
        IEnumerable<ProductDataModel> dataModel = await this.repository.GetProducts();

        IEnumerable<ProductModel> mappedModel = this.mapper.Map<IEnumerable<ProductModel>>(dataModel);

        return mappedModel;
    }

    public async Task<IEnumerable<ProductModel>> GetProductsByNameAsync(string name)
    {
        IEnumerable<ProductDataModel> dataModel = await this.repository.GetProductsByName(name);

        IEnumerable<ProductModel> mappedModel = this.mapper.Map<IEnumerable<ProductModel>>(dataModel);

        return mappedModel;
    }

    public async Task<IEnumerable<ProductModel>> GetProductsByCategoryAsync(string category)
    {
        IEnumerable<ProductDataModel> dataModel = await this.repository.GetProductsByCategory(category);

        IEnumerable<ProductModel> mappedModel = this.mapper.Map<IEnumerable<ProductModel>>(dataModel);

        return mappedModel;
    }

    public async Task CreateProductAsync(ProductModel product)
    {
        ProductDataModel dataModel = this.mapper.Map<ProductDataModel>(product);

        await this.repository.CreateProduct(dataModel);
    }

    public async Task<bool> UpdateProductAsync(ProductModel product)
    {
        ProductDataModel dataModel = this.mapper.Map<ProductDataModel>(product);

        return await this.repository.UpdateProduct(dataModel);
    }

    public async Task<bool> DeleteProductAsync(string id)
        => await this.repository.DeleteProduct(id);
}
