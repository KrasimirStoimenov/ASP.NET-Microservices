namespace Discount.Repositories.SqlConstants;
public class DiscountSqlConstants
{
    public static readonly string GetDiscount = $@"
        SELECT * FROM Coupon 
        WHERE ProductName = @ProductName";

    public static readonly string CreateDiscount = $@"
        INSERT INTO Coupon (ProductName, Description, Amount) 
        VALUES (@ProductName, @Description, @Amount)";

    public static readonly string UpdateDiscount = $@"
        UPDATE Coupon 
        SET ProductName=@ProductName, Description = @Description, Amount = @Amount 
        WHERE Id = @Id";

    public static readonly string DeleteDiscount = $@"
        DELETE FROM Coupon 
        WHERE ProductName = @ProductName";
}
