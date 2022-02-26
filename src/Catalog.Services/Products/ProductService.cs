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

    public async Task<ProductModel> GetProduct(string id)
    {
        ProductDataModel dataModel = await this.repository.GetProduct(id);

        ProductModel mappedModel = this.mapper.Map<ProductModel>(dataModel);

        return mappedModel;
    }

    public async Task<IEnumerable<ProductModel>> GetProducts()
    {
        IEnumerable<ProductDataModel> dataModel = await this.repository.GetProducts();

        ICollection<ProductModel> mappedModel = this.mapper.Map<ICollection<ProductModel>>(dataModel);

        return mappedModel;
    }

    public async Task<IEnumerable<ProductModel>> GetProductsByName(string name)
    {
        IEnumerable<ProductDataModel> dataModel = await this.repository.GetProductsByName(name);

        ICollection<ProductModel> mappedModel = this.mapper.Map<ICollection<ProductModel>>(dataModel);

        return mappedModel;
    }

    public async Task<IEnumerable<ProductModel>> GetProductsByCategory(string category)
    {
        IEnumerable<ProductDataModel> dataModel = await this.repository.GetProductsByCategory(category);

        ICollection<ProductModel> mappedModel = this.mapper.Map<ICollection<ProductModel>>(dataModel);

        return mappedModel;
    }

    public async Task CreateProduct(ProductModel product)
    {
        ProductDataModel dataModel = this.mapper.Map<ProductDataModel>(product);

        await this.repository.CreateProduct(dataModel);
    }

    public async Task<bool> UpdateProduct(ProductModel product)
    {
        ProductDataModel dataModel = this.mapper.Map<ProductDataModel>(product);

        return await this.repository.UpdateProduct(dataModel);
    }

    public async Task<bool> DeleteProduct(string id)
        => await this.repository.DeleteProduct(id);
}
