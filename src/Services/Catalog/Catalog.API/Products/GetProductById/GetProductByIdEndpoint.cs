﻿
using Catalog.API.Models;

namespace Catalog.API.Products.GetProductById
{
    record GetProductByIdResponse(Product Product);
    public class GetProductByIdEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/products/{id}", async (Guid id,  ISender sender) => {
                try
                {
                    var result = await sender.Send(new GetProductByIdQuery(id));
                
                    var response = result.Adapt<GetProductByIdResponse>();

                    return Results.Ok(response);

                }
                catch (Exception)
                {
                    return Results.NotFound();
                }
            })
            .WithName("GetProductById")
            .Produces<GetProductByIdResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .WithSummary("Get Product By Id")
            .WithDescription("Get Product By Id");
        }
    }
}