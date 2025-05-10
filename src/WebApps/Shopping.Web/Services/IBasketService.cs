using Refit;
using Shopping.Web.Models.Basket;
using System.Net;

namespace Shopping.Web.Services
{
    public interface IBasketService
    {
        [Get("/basket-service/basket/{username}")]
        Task<GetBasketResponse> GetBasket(string username);

        [Post("/basket-service/basket")]
        Task<StoreBasketResponse> StoreBasket(StoreBasketRequest request);

        [Delete("/basket-service/basket/{username}")]
        Task<DeleteBasketResponse> DeleteBasket(string username);

        [Post("/basket-service/basket/checkout")]
        Task<CheckoutBasketResponse> CheckoutBasket(CheckoutBasketRequest request);
        public async Task<ShoppingCartModel> LoadUserBasket()
        {
            // Get Basket If Not Exist Create New Basket with Default Logged In User Name: swn
            var username = "swn";
            ShoppingCartModel basket;

            try
            {
                var getBasketResponse = await GetBasket(username);
                basket = getBasketResponse.Cart;
            }
            catch (ApiException apiException) when (apiException.StatusCode == HttpStatusCode.NotFound)
            {
                basket = new ShoppingCartModel
                {
                    Username = username,
                    Items = []
                };
            }

            return basket;
        }
    }
}
