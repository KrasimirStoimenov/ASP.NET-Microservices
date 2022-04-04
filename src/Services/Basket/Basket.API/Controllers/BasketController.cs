namespace Basket.API.Controllers;

using Basket.API.GrpcServices;
using Basket.API.Models;
using Basket.API.Repositories;

using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/v1/[controller]")]
public class BasketController : ControllerBase
{
    private readonly IBasketRepository repository;
    private readonly DiscountGrpcService discountGrpcService;

    public BasketController(
        IBasketRepository repository,
        DiscountGrpcService discountGrpcService)
    {
        this.repository = repository ?? throw new ArgumentNullException(nameof(repository));
        this.discountGrpcService = discountGrpcService ?? throw new ArgumentNullException(nameof(discountGrpcService));
    }

    [HttpGet]
    [ProducesResponseType(typeof(ShoppingCart), 200)]
    [Route("{username}")]
    public async Task<ActionResult<ShoppingCart>> GetBasket([FromRoute] string username)
    {
        ShoppingCart basket = await this.repository.GetBasket(username);

        return this.Ok(basket ?? new ShoppingCart(username));
    }

    [HttpPost]
    [ProducesResponseType(typeof(ShoppingCart), 200)]
    public async Task<ActionResult<ShoppingCart>> UpdateBasket([FromBody] ShoppingCart basket)
    {
        foreach (var item in basket.ShoppingCartItems)
        {
            var coupon = await this.discountGrpcService.GetDiscountAsync(item.ProductName);
            item.Price -= coupon.Amount;
        }

        return this.Ok(await this.repository.UpdateBasket(basket));
    }

    [HttpDelete]
    [ProducesResponseType(typeof(void), 200)]
    [Route("{username}")]
    public async Task<IActionResult> DeleteBasket(string username)
    {
        await this.repository.DeleteBasket(username);

        return this.Ok();
    }
}
