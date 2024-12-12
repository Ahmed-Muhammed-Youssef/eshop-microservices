
using Catalog.API.Models;

namespace Catalog.API.Products.GetProductByCategory
{
    public record GetProductByCategoryResponse(IEnumerable<Product> Products);
    public class GetProductByCategoryEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/products/category/{categoty}", async (string categoty, ISender sender) => {
                GetProductByCategoryResult result = await sender.Send(new GetProductByCategoryQuery(categoty));

                var response = result.Adapt<GetProductByCategoryResponse>();

                return Results.Ok(response);
            });
        }
    }
}
