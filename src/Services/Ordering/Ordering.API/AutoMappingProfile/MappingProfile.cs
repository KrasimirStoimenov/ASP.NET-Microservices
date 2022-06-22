namespace Ordering.API.AutoMappingProfile;

using AutoMapper;


using EventBus.Messages.Events;

using Ordering.Application.Features.Orders.Commands.CheckoutOrder;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        this.CreateMap<CheckoutOrderCommand, BasketCheckoutEvent>().ReverseMap();
    }
}
