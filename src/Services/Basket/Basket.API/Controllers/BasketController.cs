namespace Basket.API.Controllers;

using Basket.API.Models;
using Basket.API.Repositories;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/v1/[controller]")]
public class BasketController : ControllerBase
{
    private readonly IBasketRepository repository;

    public BasketController(IBasketRepository repository)
    {
        this.repository = repository ?? throw new ArgumentNullException(nameof(repository));
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
