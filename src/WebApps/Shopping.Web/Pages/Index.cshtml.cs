using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shopping.Web.Models.Basket;
using Shopping.Web.Models.Catalog;
using Shopping.Web.Services;

namespace Shopping.Web.Pages
{
    public class IndexModel(ILogger<IndexModel> logger, ICatalogService catalogService, IBasketService basketService) : PageModel
    {
        public IEnumerable<ProductModel> ProductList { get; set; } = [];

        public async Task<IActionResult> OnGetAsync()
        {
            logger.LogInformation("Index page loading...");
            
            var result = await catalogService.GetProducts();
            ProductList = result.Products;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(Guid productId)
        {
            logger.LogInformation("Adding product to basket...");
            
            var productResponse = await catalogService.GetProduct(productId);

            var basket = await basketService.LoadUserBasket();

            basket.Items.Add(new ShoppingCartItemModel
            {
                ProductId = productId,
                ProductName = productResponse.Product.Name,
                Price = productResponse.Product.Price,
                Quantity = 1,
                Color = "Black"
            });

            await basketService.StoreBasket(new StoreBasketRequest(basket));

            return RedirectToPage("Cart");
        }

    }
}
