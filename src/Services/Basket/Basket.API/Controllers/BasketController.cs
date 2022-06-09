namespace Basket.API.Controllers;

using AutoMapper;

using Basket.API.GrpcServices;
using Basket.API.Models;
using Basket.API.Repositories;

using EventBus.Messages.Events;

using MassTransit;

using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/v1/[controller]")]
public class BasketController : ControllerBase
{
    private readonly IBasketRepository repository;
    private readonly DiscountGrpcService discountGrpcService;
    private readonly IPublishEndpoint publishEndpoint;
    private readonly IMapper mapper;

    public BasketController(
        IBasketRepository repository,
        DiscountGrpcService discountGrpcService,
        IPublishEndpoint publishEndpoint,
        IMapper mapper)
    {
        this.repository = repository ?? throw new ArgumentNullException(nameof(repository));
        this.discountGrpcService = discountGrpcService ?? throw new ArgumentNullException(nameof(discountGrpcService));
        this.publishEndpoint = publishEndpoint ?? throw new ArgumentNullException(nameof(publishEndpoint));
        this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    [HttpGet]
    [ProducesResponseType(typeof(ShoppingCart), StatusCodes.Status200OK)]
    [Route("{username}")]
    public async Task<ActionResult<ShoppingCart>> GetBasket([FromRoute] string username)
    {
        ShoppingCart basket = await this.repository.GetBasket(username);

        return this.Ok(basket ?? new ShoppingCart(username));
    }

    [HttpPost]
    [ProducesResponseType(typeof(ShoppingCart), StatusCodes.Status200OK)]
    public async Task<ActionResult<ShoppingCart>> UpdateBasket([FromBody] ShoppingCart basket)
    {
        //communicate with Discount.Grpc
        //calculate latest prices of product into shopping cart
        //consume Discount.Grpc

        foreach (var item in basket.ShoppingCartItems)
        {
            var coupon = await this.discountGrpcService.GetDiscountAsync(item.ProductName);
            item.Price -= coupon.Amount;
        }

        return this.Ok(await this.repository.UpdateBasket(basket));
    }

    [HttpDelete]
    [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
    [Route("{username}")]
    public async Task<IActionResult> DeleteBasket(string username)
    {
        await this.repository.DeleteBasket(username);

        return this.Ok();
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status202Accepted)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [Route("[action]")]
    public async Task<IActionResult> Checkout([FromBody] BasketCheckout basketCheckout)
    {
        //get existing basket for username with total price
        ShoppingCart basket = await this.repository.GetBasket(basketCheckout.Username);
        if (basket == null)
        {
            return this.BadRequest();
        }

        //create basketChechout event and set TotalPrice on basketCheckout eventMessage
        BasketCheckoutEvent eventMessage = this.mapper.Map<BasketCheckoutEvent>(basketCheckout);
        eventMessage.TotalPrice = basket.TotalPrice;

        //send checkout event to rabbitMQ
        await this.publishEndpoint.Publish(eventMessage);

        //remove basket
        await this.repository.DeleteBasket(basket.Username);

        return this.Accepted();
    }
}
