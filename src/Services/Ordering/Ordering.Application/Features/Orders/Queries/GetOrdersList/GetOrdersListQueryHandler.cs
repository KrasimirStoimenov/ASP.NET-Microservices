﻿namespace Ordering.Application.Features.Orders.Queries.GetOrdersList;

using System.Threading;

using AutoMapper;

using MediatR;

using Ordering.Application.Interfaces.Persistence;

public class GetOrdersListQueryHandler : IRequestHandler<GetOrdersListQuery, ICollection<OrderModel>>
{
    private readonly IOrderRepository orderRepository;
    private readonly IMapper mapper;
    public GetOrdersListQueryHandler(IOrderRepository orderRepository, IMapper mapper)
    {
        this.orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));
        this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<ICollection<OrderModel>> Handle(GetOrdersListQuery request, CancellationToken cancellationToken)
    {
        var orders = await this.orderRepository.GetOrdersByUsernameAsync(request.Username);

        var mappedModels = this.mapper.Map<ICollection<OrderModel>>(orders);

        return mappedModels;
    }
}
