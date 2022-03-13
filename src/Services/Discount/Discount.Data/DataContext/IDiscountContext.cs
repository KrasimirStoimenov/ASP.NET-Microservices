namespace Discount.Data.DataContext;

using System.Data;

public interface IDiscountContext
{
    IDbConnection OpenDbConnection();
}
