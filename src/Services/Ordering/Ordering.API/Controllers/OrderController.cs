namespace Ordering.API.Controllers;

using MediatR;

using Microsoft.AspNetCore.Mvc;

using Ordering.Application.Features.Orders.Commands.CheckoutOrder;
using Ordering.Application.Features.Orders.Commands.DeleteOrder;
using Ordering.Application.Features.Orders.Commands.UpdateOrder;
using Ordering.Application.Features.Orders.Queries.GetOrdersList;

[ApiController]
[Route("api/v1/[controller]")]
public class OrderController : ControllerBase
{
    private readonly IMediator mediator;

    public OrderController(IMediator mediator)
    {
        this.mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<OrderModel>), StatusCodes.Status200OK)]
    [Route("{username}")]
    public async Task<ActionResult<IEnumerable<OrderModel>>> GetOrdersByUsername(
        [FromRoute] string username)
    {
        var query = new GetOrdersListQuery(username);
        var orders = await this.mediator.Send(query);

        return this.Ok(orders);
    }

    //just for testing purpose, checkout order will be triggered from RabbitMQ
    [HttpPost]
    [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
    public async Task<ActionResult<int>> CheckoutOrder(
        [FromBody] CheckoutOrderCommand command)
    {
        var result = await this.mediator.Send(command);

        return this.Ok(result);
    }

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult> UpdateOrder(
        [FromBody] UpdateOrderCommand command)
    {
        await this.mediator.Send(command);
        return this.NoContent();
    }

    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    [Route("{id}")]
    public async Task<ActionResult> DeleteOrder(
        [FromRoute] int id)
    {
        var command = new DeleteOrderCommand()
        {
            Id = id
        };

        await this.mediator.Send(command);
        return this.NoContent();
    }
}
