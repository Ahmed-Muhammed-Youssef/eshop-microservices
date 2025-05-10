namespace Shopping.Web.Models.Basket
{
    public class ShoppingCartModel
    {
        public string Username { get; set; } = default!;
        public List<ShoppingCartItemModel> Items { get; set; } = [];
        public decimal TotalPrice => Items.Sum(i => i.Price * i.Quantity);
    }

    public record GetBasketResponse(ShoppingCartModel Cart);
    public record StoreBasketRequest(ShoppingCartModel Cart);
    public record StoreBasketResponse(string Username);
    public record DeleteBasketResponse(bool IsSuccess);
}
