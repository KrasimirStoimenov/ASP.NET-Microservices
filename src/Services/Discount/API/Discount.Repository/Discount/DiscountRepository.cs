namespace Discount.Repositories.Discount;
using System.Threading.Tasks;

using Dapper;

using Data.DataContext;
using Data.Models;

using SqlConstants;

public class DiscountRepository : IDiscountRepository
{
    private readonly IDiscountContext context;

    public DiscountRepository(IDiscountContext context)
    {
        this.context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<CouponDataModel> GetDiscount(string productName)
    {
        using var connection = this.context.OpenDbConnection();

        string query = DiscountSqlConstants.GetDiscount;

        CouponDataModel coupon = await connection
            .QueryFirstOrDefaultAsync<CouponDataModel>(query, new { ProductName = productName });

        if (coupon == null)
        {
            return new CouponDataModel
            {
                ProductName = "No Discount",
                Amount = 0,
                Description = "No Discount Desc"
            };
        }

        return coupon;
    }

    public async Task<bool> CreateDiscount(CouponInputDataModel coupon)
    {
        using var connection = this.context.OpenDbConnection();

        string query = DiscountSqlConstants.CreateDiscount;

        int affected = await connection
            .ExecuteAsync(query, new { ProductName = coupon.ProductName, Description = coupon.Description, Amount = coupon.Amount });

        return affected > 0;
    }

    public async Task<bool> UpdateDiscount(CouponDataModel coupon)
    {
        using var connection = this.context.OpenDbConnection();

        string query = DiscountSqlConstants.UpdateDiscount;

        var affected = await connection
            .ExecuteAsync(query, new { ProductName = coupon.ProductName, Description = coupon.Description, Amount = coupon.Amount, Id = coupon.Id });

        return affected > 0;
    }

    public async Task<bool> DeleteDiscount(string productName)
    {
        using var connection = this.context.OpenDbConnection();

        string query = DiscountSqlConstants.DeleteDiscount;

        var affected = await connection
            .ExecuteAsync(query, new { ProductName = productName });

        return affected > 0;
    }
}
