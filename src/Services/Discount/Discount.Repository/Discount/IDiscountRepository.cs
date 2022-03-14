namespace Discount.Repositories.Discount;

using Data.Models;

public interface IDiscountRepository
{
    Task<CouponDataModel> GetDiscount(string productName);

    Task<bool> CreateDiscount(CouponDataModel coupon);

    Task<bool> UpdateDiscount(CouponDataModel coupon);

    Task<bool> DeleteDiscount(string productName);
}
