namespace Discount.Service.Discounts;

using Discount.Service.Discounts.Models;

public interface IDiscountService
{
    Task<CouponModel> GetDiscountAsync(string productName);

    Task<bool> CreateDiscountAsync(CouponModel coupon);

    Task<bool> UpdateDiscountAsync(CouponModel coupon);

    Task<bool> DeleteDiscountAsync(string productName);
}
