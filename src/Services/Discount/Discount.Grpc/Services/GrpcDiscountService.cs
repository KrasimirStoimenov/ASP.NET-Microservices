namespace Discount.Grpc.Services;

using System;
using System.Threading.Tasks;

using AutoMapper;

using Discount.Service.Discounts;
using Discount.Service.Discounts.Models;

using global::Grpc.Core;

public class GrpcDiscountService : DiscountProtoService.DiscountProtoServiceBase
{
    private readonly IDiscountService discountService;
    private readonly IMapper mapper;
    private readonly ILogger<GrpcDiscountService> logger;

    public GrpcDiscountService(
        IDiscountService discountService,
        IMapper mapper,
        ILogger<GrpcDiscountService> logger)
    {
        this.discountService = discountService ?? throw new ArgumentNullException(nameof(discountService));
        this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public override async Task<GrpcCouponModel> GetDiscount(GetDiscountRequest request, ServerCallContext context)
    {
        CouponModel coupon = await this.discountService.GetDiscountAsync(request.ProductName);

        if (coupon == null)
        {
            throw new RpcException(new Status(StatusCode.NotFound, $"Discount with ProductName : {request.ProductName} is not found."));
        }

        this.logger.LogInformation($"Discount is retrieved for ProductName : {coupon.ProductName}, Amount : {coupon.Amount}");

        GrpcCouponModel grpcModel = this.mapper.Map<GrpcCouponModel>(coupon);
        return grpcModel;
    }

    public override async Task<GrpcCouponModel> CreateDiscount(CreateDiscountRequest request, ServerCallContext context)
    {
        CouponInputModel coupon = this.mapper.Map<CouponInputModel>(request.GrpcCouponModel);

        await this.discountService.CreateDiscountAsync(coupon);

        this.logger.LogInformation($"Discount is successfully updated. ProductName : {coupon.ProductName}");

        GrpcCouponModel grpcModel = this.mapper.Map<GrpcCouponModel>(coupon);
        return grpcModel;
    }

    public override async Task<GrpcCouponModel> UpdateDiscount(UpdateDiscountRequest request, ServerCallContext context)
    {
        CouponModel coupon = this.mapper.Map<CouponModel>(request.GrpcCouponModel);
        await this.discountService.UpdateDiscountAsync(coupon);

        this.logger.LogInformation($"Discount is successfully updated. ProductName : {coupon.ProductName}");

        GrpcCouponModel grpcModel = this.mapper.Map<GrpcCouponModel>(coupon);
        return grpcModel;
    }

    public override async Task<DeleteDiscountResponse> DeleteDiscount(DeleteDiscountRequest request, ServerCallContext context)
    {
        var deleted = await this.discountService.DeleteDiscountAsync(request.ProductName);
        var response = new DeleteDiscountResponse
        {
            Success = deleted
        };

        return response;
    }
}
