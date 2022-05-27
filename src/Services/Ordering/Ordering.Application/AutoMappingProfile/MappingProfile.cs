namespace Ordering.Application.AutoMappingProfile;

using AutoMapper;

using Ordering.Application.Features.Orders.Commands.CheckoutOrder;
using Ordering.Application.Features.Orders.Commands.UpdateOrder;
using Ordering.Application.Features.Orders.Queries.GetOrdersList;
using Ordering.Domain.Entities;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        this.CreateMap<Order, OrderDataModel>().ReverseMap();
        this.CreateMap<Order, CheckoutOrderCommand>().ReverseMap();
        this.CreateMap<Order, UpdateOrderCommand>().ReverseMap();
    }
}
