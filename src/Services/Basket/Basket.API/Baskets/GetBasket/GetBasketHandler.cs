using Basket.API.Models;
using BuildingBlocks.CQRS;

namespace Basket.API.Baskets.GetBasket
{
    public record GetBasketQuery(string Username) : IQuery<GetBasketResult>;
    public record GetBasketResult(ShoppingCart Cart);
    public class GetBasketQueryHandler : IQueryHandler<GetBasketQuery, GetBasketResult>
    {
        public Task<GetBasketResult> Handle(GetBasketQuery query, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
