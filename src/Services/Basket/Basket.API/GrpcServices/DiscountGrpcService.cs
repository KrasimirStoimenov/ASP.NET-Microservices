namespace Basket.API.GrpcServices;

using System;

using Discount.Grpc;

public class DiscountGrpcService
{
    private readonly DiscountProtoService.DiscountProtoServiceClient discountProtoService;

    public DiscountGrpcService(DiscountProtoService.DiscountProtoServiceClient discountProtoService)
    {
        this.discountProtoService = discountProtoService ?? throw new ArgumentNullException(nameof(discountProtoService));
    }

    public async Task<GrpcCouponModel> GetDiscountAsync(string productName)
    {
        var request = new GetDiscountRequest
        {
            ProductName = productName
        };

        return await this.discountProtoService.GetDiscountAsync(request);
    }
}
