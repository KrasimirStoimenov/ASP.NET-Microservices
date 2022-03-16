namespace Discount.Service.AutoMappingProfile;

using AutoMapper;

using Discount.Data.Models;
using Discount.Service.Discounts.Models;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        this.CreateMap<CouponInputModel, CouponInputDataModel>();
        this.CreateMap<CouponModel, CouponDataModel>();
        this.CreateMap<CouponDataModel, CouponModel>();
    }
}
