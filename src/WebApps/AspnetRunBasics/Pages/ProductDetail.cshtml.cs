namespace AspnetRunBasics;

using System;
using System.Threading.Tasks;

using AspnetRunBasics.Models;
using AspnetRunBasics.Services.Interfaces;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

public class ProductDetailModel : PageModel
{
    private readonly ICatalogService catalogService;
    private readonly IBasketService basketService;

    public ProductDetailModel(ICatalogService catalogService, IBasketService basketService)
    {
        this.catalogService = catalogService ?? throw new ArgumentNullException(nameof(catalogService));
        this.basketService = basketService ?? throw new ArgumentNullException(nameof(basketService));
    }

    public CatalogModel Product { get; set; }

    [BindProperty]
    public string Color { get; set; }

    [BindProperty]
    public int Quantity { get; set; }

    public async Task<IActionResult> OnGetAsync(string productId)
    {
        if (productId == null)
        {
            return NotFound();
        }

        Product = await this.catalogService.GetCatalogAsync(productId);

        if (Product == null)
        {
            return NotFound();
        }
        return Page();
    }

    public async Task<IActionResult> OnPostAddToCartAsync(string productId)
    {
        CatalogModel product = await this.catalogService.GetCatalogAsync(productId);

        string username = "ks";
        BasketModel basket = await this.basketService.GetBasketAsync(username);

        basket.ShoppingCartItems.Add(new BasketItemModel
        {
            ProductId = productId,
            ProductName = product.Name,
            Price = product.Price,
            Quantity = this.Quantity,
            Color = this.Color
        });

        BasketModel basketUpdated = await this.basketService.UpdateBasketAsync(basket);

        return RedirectToPage("Cart");
    }
}