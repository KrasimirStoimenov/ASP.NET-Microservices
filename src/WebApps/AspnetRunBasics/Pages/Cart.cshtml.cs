namespace AspnetRunBasics;

using System;
using System.Linq;
using System.Threading.Tasks;

using AspnetRunBasics.Models;
using AspnetRunBasics.Services.Interfaces;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

public class CartModel : PageModel
{
    private readonly IBasketService basketService;

    public CartModel(IBasketService basketService)
    {
        this.basketService = basketService ?? throw new ArgumentNullException(nameof(basketService));
    }

    public BasketModel Cart { get; set; } = new BasketModel();

    public async Task<IActionResult> OnGetAsync()
    {
        string username = "ks";
        this.Cart = await this.basketService.GetBasketAsync(username);

        return Page();
    }

    public async Task<IActionResult> OnPostRemoveToCartAsync(string productId)
    {
        string username = "ks";
        BasketModel basket = await this.basketService.GetBasketAsync(username);

        BasketItemModel item = basket.ShoppingCartItems.Single(x => x.ProductId == productId);
        basket.ShoppingCartItems.Remove(item);

        BasketModel basketUpdated = await this.basketService.UpdateBasketAsync(basket);

        return RedirectToPage();
    }
}