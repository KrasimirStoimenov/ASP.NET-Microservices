namespace Ordering.API.EventBusConsumer;

using System.Threading.Tasks;

using EventBus.Messages.Events;

using MassTransit;

public class BasketCheckoutConsumer : IConsumer<BasketCheckoutEvent>
{
    public Task Consume(ConsumeContext<BasketCheckoutEvent> context)
    {
        throw new NotImplementedException();
    }
}
