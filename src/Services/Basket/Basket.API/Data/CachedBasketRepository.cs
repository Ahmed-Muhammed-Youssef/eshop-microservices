namespace Basket.API.Data
{
    public class CachedBasketRepository(IBasketRepository basketRepository) : IBasketRepository
    {
        public async Task<ShoppingCart> GetBasket(string username, CancellationToken cancellationToken = default)
        {
            return await basketRepository.GetBasket(username, cancellationToken);
        }
        public async Task<ShoppingCart> StoreBasket(ShoppingCart basket, CancellationToken cancellationToken = default)
        {
            return await basketRepository.StoreBasket(basket, cancellationToken);
        }
        public async Task<bool> DeleteBasket(string username, CancellationToken cancellationToken = default)
        {
            return await basketRepository.DeleteBasket(username, cancellationToken);
        }
    }
}
