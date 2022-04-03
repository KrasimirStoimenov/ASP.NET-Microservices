namespace Discount.API.Controllers;

using Discount.Service.Discounts;
using Discount.Service.Discounts.Models;

using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/v1/[controller]")]
public class DiscountController : ControllerBase
{
    private readonly IDiscountService discountService;

    public DiscountController(IDiscountService discountService)
    {
        this.discountService = discountService ?? throw new ArgumentNullException(nameof(discountService));
    }

    [HttpGet]
    [ProducesResponseType(typeof(CouponModel), 200)]
    [Route("{productName}")]
    public async Task<ActionResult<CouponModel>> GetDiscount(
        [FromRoute] string productName)
    {
        CouponModel result = await this.discountService.GetDiscountAsync(productName);

        return this.Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(CouponInputModel), 200)]
    public async Task<ActionResult<CouponInputModel>> CreateDiscount(
        [FromBody] CouponInputModel coupon)
    {
        await this.discountService.CreateDiscountAsync(coupon);

        return this.CreatedAtAction("GetDiscount", new { productName = coupon.ProductName }, coupon);
    }

    [HttpPut]
    [ProducesResponseType(typeof(CouponModel), 200)]
    public async Task<ActionResult<CouponModel>> UpdateDiscount(
        [FromBody] CouponModel coupon)
    {
        return this.Ok(await this.discountService.UpdateDiscountAsync(coupon));
    }

    [HttpDelete]
    [ProducesResponseType(typeof(CouponModel), 200)]
    [Route("{productName}")]
    public async Task<ActionResult<CouponModel>> DeleteDisount(
        [FromRoute] string productName)
    {
        return this.Ok(await this.discountService.DeleteDiscountAsync(productName));
    }
}
