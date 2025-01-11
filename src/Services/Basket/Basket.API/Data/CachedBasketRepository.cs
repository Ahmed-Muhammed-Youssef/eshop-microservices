using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace Basket.API.Data
{
    public class CachedBasketRepository(IBasketRepository basketRepository, IDistributedCache cache) : IBasketRepository
    {
        public async Task<ShoppingCart> GetBasket(string username, CancellationToken cancellationToken = default)
        {
            string? cachedBasket = await cache.GetStringAsync(username, cancellationToken);

            if (cachedBasket is null) {
                var basket = await basketRepository.GetBasket(username, cancellationToken);
                await cache.SetStringAsync(username, JsonSerializer.Serialize(basket), cancellationToken);
                return basket;
            }
            else
            {
                return JsonSerializer.Deserialize<ShoppingCart>(cachedBasket) ?? throw new Exception("failed to serialize data");
            }
        }
        public async Task<ShoppingCart> StoreBasket(ShoppingCart basket, CancellationToken cancellationToken = default)
        {
            
            basket = await basketRepository.StoreBasket(basket, cancellationToken);
            await cache.SetStringAsync(basket.Username, JsonSerializer.Serialize(basket), cancellationToken);

            return basket;
        }
        public async Task<bool> DeleteBasket(string username, CancellationToken cancellationToken = default)
        {
            bool isSuccess = await basketRepository.DeleteBasket(username, cancellationToken);

            if(isSuccess)
            {
                await cache.RemoveAsync(username, cancellationToken);
            }

            return isSuccess;
        }
    }
}
