namespace AspnetRunBasics.Pages;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using AspnetRunBasics.Models;
using AspnetRunBasics.Services.Interfaces;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

public class IndexModel : PageModel
{
    private readonly ICatalogService catalogService;
    private readonly IBasketService basketService;

    public IndexModel(ICatalogService catalogService, IBasketService basketService)
    {
        this.catalogService = catalogService ?? throw new ArgumentNullException(nameof(catalogService));
        this.basketService = basketService ?? throw new ArgumentNullException(nameof(basketService));
    }

    public IEnumerable<CatalogModel> ProductList { get; set; } = new List<CatalogModel>();

    public async Task<IActionResult> OnGetAsync()
    {
        this.ProductList = await this.catalogService.GetCatalogAsync();
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
            Quantity = 1,
            Color = "Black"
        });

        BasketModel basketUpdated = await this.basketService.UpdateBasketAsync(basket);

        return RedirectToPage("Cart");
    }
}
