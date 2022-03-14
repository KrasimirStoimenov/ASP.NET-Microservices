namespace Discount.Repositories.Discount;

using System.Data.SqlClient;
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

        SqlParameter productNameParam = new SqlParameter("@ProductName", productName);

        string query = DiscountSqlConstants.GetDiscount;
        SqlParameter[] sqlParams = new SqlParameter[] { productNameParam };

        CouponDataModel coupon = await connection
            .QueryFirstOrDefaultAsync<CouponDataModel>(query, productNameParam);

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

    public async Task<bool> CreateDiscount(CouponDataModel coupon)
    {
        using var connection = this.context.OpenDbConnection();

        SqlParameter productNameParam = new SqlParameter("@ProductName", coupon.ProductName);
        SqlParameter descriptionParam = new SqlParameter("@Description", coupon.Description);
        SqlParameter amountParam = new SqlParameter("@Amount", coupon.Amount);

        string query = DiscountSqlConstants.CreateDiscount;
        SqlParameter[] sqlParams = new SqlParameter[] { productNameParam, descriptionParam, amountParam };

        int affected = await connection
            .ExecuteAsync(query, sqlParams);

        return affected > 0;
    }

    public async Task<bool> UpdateDiscount(CouponDataModel coupon)
    {
        using var connection = this.context.OpenDbConnection();

        SqlParameter couponIdParam = new SqlParameter("@Id", coupon.Id);
        SqlParameter productNameParam = new SqlParameter("@ProductName", coupon.ProductName);
        SqlParameter descriptionParam = new SqlParameter("@Description", coupon.Description);
        SqlParameter amountParam = new SqlParameter("@Amount", coupon.Amount);

        string query = DiscountSqlConstants.UpdateDiscount;
        SqlParameter[] sqlParams = new SqlParameter[] { couponIdParam, productNameParam, descriptionParam, amountParam };

        int affected = await connection
            .ExecuteAsync(query, sqlParams);

        return affected > 0;
    }

    public async Task<bool> DeleteDiscount(string productName)
    {
        using var connection = this.context.OpenDbConnection();

        SqlParameter productNameParam = new SqlParameter("@ProductName", productName);

        string query = DiscountSqlConstants.DeleteDiscount;
        SqlParameter[] sqlParams = new SqlParameter[] { productNameParam };

        int affected = await connection
            .ExecuteAsync(query, sqlParams);

        return affected > 0;
    }
}
