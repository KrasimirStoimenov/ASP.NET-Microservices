namespace AspnetRunBasics;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using AspnetRunBasics.Models;
using AspnetRunBasics.Services.Interfaces;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

public class OrderModel : PageModel
{
    private readonly IOrderService orderService;

    public OrderModel(IOrderService orderService)
    {
        this.orderService = orderService ?? throw new ArgumentNullException(nameof(orderService));
    }

    public IEnumerable<OrderResponseModel> Orders { get; set; } = new List<OrderResponseModel>();

    public async Task<IActionResult> OnGetAsync()
    {
        string username = "ks";

        this.Orders = await this.orderService.GetOrdersByUsernameAsync(username);

        return Page();
    }
}