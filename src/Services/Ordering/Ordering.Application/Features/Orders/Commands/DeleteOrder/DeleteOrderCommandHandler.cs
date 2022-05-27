namespace Ordering.Application.Features.Orders.Commands.DeleteOrder;

using System.Threading;
using System.Threading.Tasks;

using AutoMapper;

using MediatR;

using Microsoft.Extensions.Logging;

using Ordering.Application.Interfaces.Persistence;

public class DeleteOrderCommandHandler : IRequestHandler<DeleteOrderCommand>
{
    private readonly IOrderRepository orderRepository;
    private readonly IMapper mapper;
    private readonly ILogger<DeleteOrderCommand> logger;

    public DeleteOrderCommandHandler(
            IOrderRepository orderRepository,
            IMapper mapper,
            ILogger<DeleteOrderCommand> logger)
    {
        this.orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));
        this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<Unit> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
    {
        var orderToDelete = await this.orderRepository.GetByIdAsync(request.Id);

        if (orderToDelete == null)
        {
            this.logger.LogError("Order not exist on database");
            //throw new NotFoundException(nameof(Order), request.Id);
        }

        await this.orderRepository.DeleteAsync(orderToDelete);

        this.logger.LogInformation($"Order {orderToDelete.Id} is successfully deleted.");

        return Unit.Value;
    }
}
