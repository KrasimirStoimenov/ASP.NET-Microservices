namespace Ordering.API.EventBusConsumer;

using System.Threading.Tasks;

using AutoMapper;

using EventBus.Messages.Events;

using MassTransit;

using MediatR;

using Ordering.Application.Features.Orders.Commands.CheckoutOrder;

public class BasketCheckoutConsumer : IConsumer<BasketCheckoutEvent>
{
    private readonly IMapper mapper;
    private readonly IMediator mediator;
    private readonly ILogger<BasketCheckoutConsumer> logger;

    public BasketCheckoutConsumer(IMapper mapper, IMediator mediator, ILogger<BasketCheckoutConsumer> logger)
    {
        this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        this.mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task Consume(ConsumeContext<BasketCheckoutEvent> context)
    {
        CheckoutOrderCommand command = this.mapper.Map<CheckoutOrderCommand>(context.Message);
        var newOrderId = await this.mediator.Send(command);

        this.logger.LogInformation($"BasketCheckoutEvent consumed successfully. Created Order Id: {newOrderId}");
    }
}
