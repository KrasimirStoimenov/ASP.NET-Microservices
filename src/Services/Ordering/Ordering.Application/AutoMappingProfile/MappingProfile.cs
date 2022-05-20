namespace Ordering.Application.AutoMappingProfile;

using AutoMapper;

using Ordering.Application.Features.Orders.Queries.GetOrdersList;
using Ordering.Domain.Entities;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        this.CreateMap<Order, OrderDto>(); // .ReverseMap();
        this.CreateMap<OrderDto, Order>();
    }
}
