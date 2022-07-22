namespace AspnetRunBasics;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using AspnetRunBasics.Models;
using AspnetRunBasics.Services.Interfaces;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

public class ProductModel : PageModel
{
    private readonly ICatalogService catalogService;
    private readonly IBasketService basketService;

    public ProductModel(ICatalogService catalogService, IBasketService basketService)
    {
        this.catalogService = catalogService ?? throw new ArgumentNullException(nameof(catalogService));
        this.basketService = basketService ?? throw new ArgumentNullException(nameof(basketService));
    }

    public IEnumerable<string> CategoryList { get; set; } = new List<string>();
    public IEnumerable<CatalogModel> ProductList { get; set; } = new List<CatalogModel>();


    [BindProperty(SupportsGet = true)]
    public string SelectedCategory { get; set; }

    public async Task<IActionResult> OnGetAsync(string categoryName)
    {
        IEnumerable<CatalogModel> productList = await this.catalogService.GetCatalogAsync();
        this.CategoryList = productList.Select(x => x.Category).Distinct();

        if (!string.IsNullOrWhiteSpace(categoryName))
        {
            this.ProductList = productList.Where(c => c.Category == categoryName);
            this.SelectedCategory = categoryName;
        }
        else
        {
            this.ProductList = productList;
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
            Quantity = 1,
            Color = "Black"
        });

        BasketModel basketUpdated = await this.basketService.UpdateBasketAsync(basket);

        return RedirectToPage("Cart");
    }
}