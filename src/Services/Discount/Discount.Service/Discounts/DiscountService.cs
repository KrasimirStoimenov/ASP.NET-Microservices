namespace Discount.Service.Discounts;

using System.Threading.Tasks;

using AutoMapper;

using Discount.Data.Models;
using Discount.Repositories.Discount;
using Discount.Service.Discounts.Models;

public class DiscountService : IDiscountService
{
    private readonly IDiscountRepository repository;
    private readonly IMapper mapper;

    public DiscountService(IDiscountRepository repository, IMapper mapper)
    {
        this.repository = repository ?? throw new ArgumentNullException(nameof(repository));
        this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<CouponModel> GetDiscountAsync(string productName)
    {
        CouponDataModel dataModel = await this.repository.GetDiscount(productName);

        CouponModel mappedModel = this.mapper.Map<CouponModel>(dataModel);

        return mappedModel;
    }

    public async Task<bool> CreateDiscountAsync(CouponModel coupon)
    {
        CouponDataModel dataModel = this.mapper.Map<CouponDataModel>(coupon);

        return await this.repository.CreateDiscount(dataModel);
    }

    public async Task<bool> UpdateDiscountAsync(CouponModel coupon)
    {
        CouponDataModel dataModel = this.mapper.Map<CouponDataModel>(coupon);

        return await this.repository.UpdateDiscount(dataModel);
    }

    public async Task<bool> DeleteDiscountAsync(string productName)
        => await this.repository.DeleteDiscount(productName);
}
