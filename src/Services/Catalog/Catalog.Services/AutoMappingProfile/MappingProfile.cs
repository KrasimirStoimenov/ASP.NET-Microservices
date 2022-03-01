namespace Catalog.Services.AutoMappingProfile;

using AutoMapper;
using Catalog.Data.Models;
using Catalog.Services.Products.Models;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        this.CreateMap<ProductModel, ProductDataModel>();
        this.CreateMap<ProductDataModel, ProductModel>();
    }
}
