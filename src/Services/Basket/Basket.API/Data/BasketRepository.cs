﻿namespace Basket.API.Data
{
    public class BasketRepository(IDocumentSession session) : IBasketRepository
    {
        public async Task<ShoppingCart> GetBasket(string username, CancellationToken cancellationToken = default)
        {
            return await session.LoadAsync<ShoppingCart>(username, cancellationToken) ?? throw new BasketNotFoundException(username);
        }
        public async Task<bool> DeleteBasket(string username, CancellationToken cancellationToken = default)
        {
            session.Delete<ShoppingCart>(username);
            await session.SaveChangesAsync(cancellationToken);
            return true;
        }
        public async Task<ShoppingCart> StoreBasket(ShoppingCart basket, CancellationToken cancellationToken = default)
        {
            session.Store(basket);
            await session.SaveChangesAsync(cancellationToken);
            return basket;
        }
    }
}
