namespace Catalog.API.Controllers;

using Catalog.Services.Products;
using Catalog.Services.Products.Models;

using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/v1/[controller]")]
public class CatalogController : ControllerBase
{
    private readonly IProductService productService;

    public CatalogController(IProductService productService)
    {
        this.productService = productService ?? throw new ArgumentNullException(nameof(productService));
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<ProductModel>), 200)]
    public async Task<ActionResult<IEnumerable<ProductModel>>> GetProducts()
    {
        IEnumerable<ProductModel> result = await this.productService.GetProductsAsync();

        return this.Ok(result);
    }

    [HttpGet]
    [ProducesResponseType(typeof(ProductModel), 200)]
    [Route("{id}")]
    public async Task<ActionResult<ProductModel>> GetProductById
        ([FromRoute] string id)
    {
        ProductModel result = await this.productService.GetProductAsync(id);

        return this.Ok(result);
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<ProductModel>), 200)]
    [Route("[action]/{category}")]
    public async Task<ActionResult<IEnumerable<ProductModel>>> GetProductsByCategory
        ([FromRoute] string category)
    {
        IEnumerable<ProductModel> result = await this.productService.GetProductsByCategoryAsync(category);

        return this.Ok(result);
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<ProductModel>), 200)]
    [Route("[action]/{name}")]
    public async Task<ActionResult<IEnumerable<ProductModel>>> GetProductsByName
        ([FromRoute] string name)
    {
        IEnumerable<ProductModel> result = await this.productService.GetProductsByNameAsync(name);

        return this.Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(ProductModel), 200)]
    public async Task<ActionResult<ProductModel>> CreateProduct
        ([FromBody] ProductModel product)
    {
        await this.productService.CreateProductAsync(product);

        return this.CreatedAtAction("GetProductById", new { id = product.Id }, product);
    }

    [HttpPut]
    [ProducesResponseType(typeof(void), 200)]
    public async Task<IActionResult> UpdateProduct
        ([FromBody] ProductModel product)
    {
        await this.productService.UpdateProductAsync(product);

        return Ok();
    }

    [HttpDelete]
    [ProducesResponseType(typeof(void), 200)]
    [Route("{id}")]
    public async Task<IActionResult> DeleteProduct
        ([FromRoute] string id)
    {
        await this.productService.DeleteProductAsync(id);
        return Ok();
    }

}
