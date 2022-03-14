namespace Discount.Service.Discounts;

using Discount.Service.Discounts.Models;

public interface IDiscountService
{
    Task<CouponModel> GetDiscount(string productName);

    Task<bool> CreateDiscount(CouponModel coupon);

    Task<bool> UpdateDiscount(CouponModel coupon);

    Task<bool> DeleteDiscount(string productName);
}
