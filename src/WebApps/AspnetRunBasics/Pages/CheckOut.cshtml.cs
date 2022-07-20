namespace AspnetRunBasics;

using System;
using System.Threading.Tasks;

using AspnetRunBasics.Models;
using AspnetRunBasics.Services.Interfaces;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

public class CheckOutModel : PageModel
{
    private readonly IBasketService basketService;
    private readonly IOrderService orderService;

    public CheckOutModel(IBasketService basketService, IOrderService orderService)
    {
        this.basketService = basketService ?? throw new ArgumentNullException(nameof(basketService));
        this.orderService = orderService ?? throw new ArgumentNullException(nameof(orderService));
    }

    [BindProperty]
    public BasketCheckoutModel Order { get; set; }

    public BasketModel Cart { get; set; } = new BasketModel();

    public async Task<IActionResult> OnGetAsync()
    {
        string username = "ks";

        this.Cart = await this.basketService.GetBasketAsync(username);

        return Page();
    }

    public async Task<IActionResult> OnPostCheckOutAsync()
    {
        string username = "ks";
        this.Cart = await this.basketService.GetBasketAsync(username);

        if (!ModelState.IsValid)
        {
            return Page();
        }

        this.Order.Username = username;
        this.Order.TotalPrice = this.Cart.TotalPrice;

        await this.basketService.CheckoutBasketAsync(this.Order);

        return RedirectToPage("Confirmation", "OrderSubmitted");
    }
}