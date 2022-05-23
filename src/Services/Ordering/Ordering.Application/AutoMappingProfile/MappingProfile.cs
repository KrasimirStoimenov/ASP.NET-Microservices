namespace Ordering.Application.AutoMappingProfile;

using AutoMapper;

using Ordering.Application.Features.Orders.Commands.CheckoutOrder;
using Ordering.Application.Features.Orders.Queries.GetOrdersList;
using Ordering.Domain.Entities;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        this.CreateMap<Order, OrderDataModel>(); // .ReverseMap();
        this.CreateMap<OrderDataModel, Order>();
        this.CreateMap<Order, CheckoutOrderCommand>();
        this.CreateMap<CheckoutOrderCommand, Order>();
    }
}
