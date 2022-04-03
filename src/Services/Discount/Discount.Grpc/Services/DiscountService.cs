namespace Discount.Grpc.Services;

using System;
using System.Threading.Tasks;

using AutoMapper;

using Discount.Service.AutoMappingProfile;
using Discount.Service.Discounts;
using Discount.Service.Discounts.Models;

using global::Grpc.Core;

public class DiscountService : DiscountProtoService.DiscountProtoServiceBase
{
    private readonly IDiscountService discountService;
    private readonly IMapper mapper;
    private readonly ILogger<DiscountService> logger;

    public DiscountService(
        IDiscountService discountService,
        IMapper mapper,
        ILogger<DiscountService> logger)
    {
        this.discountService = discountService ?? throw new ArgumentNullException(nameof(discountService));
        this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        this.logger = logger ?? throw new ArgumentNullException(nameof(logger));

        var configuration = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<MappingProfile>();
            cfg.CreateMap<CouponModel, GrpcCouponModel>();
            cfg.CreateMap<GrpcCouponModel, CouponModel>();
            cfg.CreateMap<CouponInputModel, GrpcCouponModel>();
            cfg.CreateMap<GrpcCouponModel, CouponInputModel>();
        });
    }

    public override async Task<GrpcCouponModel> GetDisocount(GetDiscountRequest request, ServerCallContext context)
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
