namespace Basket.API.AutoMappingProfile;

using AutoMapper;

using Basket.API.Models;

using EventBus.Messages.Events;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        this.CreateMap<BasketCheckout, BasketCheckoutEvent>().ReverseMap();
    }
}
