namespace Ordering.Application.Features.Orders.Commands.UpdateOrder;

using System.Threading;
using System.Threading.Tasks;

using AutoMapper;

using MediatR;

using Microsoft.Extensions.Logging;

using Ordering.Application.Exceptions;
using Ordering.Application.Interfaces.Persistence;
using Ordering.Domain.Entities;

public class UpdateOrderCommandHandler : IRequestHandler<UpdateOrderCommand>
{
    private readonly IOrderRepository orderRepository;
    private readonly IMapper mapper;
    private readonly ILogger<UpdateOrderCommand> logger;

    public UpdateOrderCommandHandler(
            IOrderRepository orderRepository,
            IMapper mapper,
            ILogger<UpdateOrderCommand> logger)
    {
        this.orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));
        this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<Unit> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
    {
        var orderToUpdate = await this.orderRepository.GetByIdAsync(request.Id);
        if (orderToUpdate == null)
        {
            throw new NotFoundException(nameof(Order), request.Id);
        }

        this.mapper.Map(request, orderToUpdate, typeof(UpdateOrderCommand), typeof(Order));

        await this.orderRepository.UpdateAsync(orderToUpdate);

        this.logger.LogInformation($"Order {orderToUpdate.Id} is successfully updated.");

        return Unit.Value;
    }
}
