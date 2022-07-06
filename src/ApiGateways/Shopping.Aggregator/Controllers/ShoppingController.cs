namespace Shopping.Aggregator.Controllers;

using System;

using Microsoft.AspNetCore.Mvc;

using Shopping.Aggregator.Models;
using Shopping.Aggregator.Services.Interfaces;

[ApiController]
[Route("api/v1/[controller]")]
public class ShoppingController : ControllerBase
{
    private readonly ICatalogService catalogService;
    private readonly IBasketService basketService;
    private readonly IOrderService orderService;

    public ShoppingController(ICatalogService catalogService, IBasketService basketService, IOrderService orderService)
    {
        this.catalogService = catalogService ?? throw new ArgumentNullException(nameof(catalogService));
        this.basketService = basketService ?? throw new ArgumentNullException(nameof(basketService));
        this.orderService = orderService ?? throw new ArgumentNullException(nameof(orderService));
    }

    [HttpGet]
    [ProducesResponseType(typeof(ShoppingModel), StatusCodes.Status200OK)]
    [Route("{username}")]
    public async Task<ActionResult<ShoppingModel>> GetShopping(string username)
    {
        // get basket for username
        // iterate basket items and consume products with basket item productId member
        // map product related members into basketitem dto with extended columns
        // consume ordering microservice in order to retrieve order list
        // return root ShoppingModel dto class which including all responses

        BasketModel basket = await this.basketService.GetBasket(username);

        foreach (var item in basket.ShoppingCartItems)
        {
            CatalogModel product = await this.catalogService.GetCatalog(item.ProductId);

            //set additional product field into basket item
            item.ProductName = product.Name;
            item.Category = product.Category;
            item.Summary = product.Summary;
            item.Description = product.Description;
            item.ImageFile = product.ImageFile;
        }

        IEnumerable<OrderResponseModel> orders = await this.orderService.GetOrdersByUsername(username);

        ShoppingModel shoppingModel = new ShoppingModel
        {
            Username = username,
            BasketWithProducts = basket,
            Orders = orders
        };

        return Ok(shoppingModel);
    }
}
