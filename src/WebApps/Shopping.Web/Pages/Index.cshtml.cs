using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shopping.Web.Models.Catalog;
using Shopping.Web.Services;

namespace Shopping.Web.Pages
{
    public class IndexModel(ILogger<IndexModel> logger, ICatalogService catalogService) : PageModel
    {
        public IEnumerable<ProductModel> ProductList { get; set; } = [];

        public async Task OnGetAsync()
        {
            logger.LogInformation("Index page loading...");
            try
            {
                var result = await catalogService.GetProducts();
                ProductList = result.Products;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error fetching products.");
            }
        }
    }
}
