namespace Ordering.Domain.Models
{
    public class Order : Aggregate<Guid>
    {
        private readonly List<OrderItem> _orderItems = [];
        public IReadOnlyList<OrderItem> OrderItems => _orderItems.AsReadOnly();
        public Guid CustomerId { get; private set; } = default;
        public string OrderName { get; private set; } = default!;
        public Address ShippingAddress { get; private set; } = default!;
        public Address BillingAddress { get; private set; } = default!;
        public Payment Payment { get; private set; } = default!;
        public OrderStatus Status { get; private set; } = OrderStatus.Pending;
        public decimal TotalPrice()
        {
            return _orderItems.Sum(x => x.Price * x.Quantity);
        }

        public static Order Create(Guid id, Guid customerId, string orderName, Address shippingAddress, Address billingAddress, Payment payment)
        {
            ArgumentNullException.ThrowIfNull(id);
            ArgumentNullException.ThrowIfNull(customerId);
            ArgumentException.ThrowIfNullOrEmpty(orderName);
            ArgumentNullException.ThrowIfNull(shippingAddress);
            ArgumentNullException.ThrowIfNull(billingAddress);
            ArgumentNullException.ThrowIfNull(payment);


            Order order = new Order() { Id = id, CustomerId = customerId, OrderName = orderName, ShippingAddress = shippingAddress, BillingAddress = billingAddress, Payment = payment, Status = OrderStatus.Pending };
            order.AddDominEvent(new OrderCreatedEvent(order));
           
            return order;
        }

        public void Update(string orderName, Address shippingAddress, Address billingAddress, Payment payment, OrderStatus status)
        {
            OrderName = orderName;
            ShippingAddress = shippingAddress;
            BillingAddress = billingAddress;
            Payment = payment;
            Status = status;

            this.AddDominEvent(new OrderUpdatedEvent(this));
        }

        public void Add(Guid productId, int quantity, decimal price)
        {
            ArgumentNullException.ThrowIfNull(productId);
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(quantity);

            OrderItem orderItem = new(orderId: Id, productId: productId, price: price, quantity: quantity);

            _orderItems.Add(orderItem);
        }

        public void Remove(Guid productId) 
        {
            OrderItem? orderItem = _orderItems.FirstOrDefault(x => x.ProductId == productId);
            if (orderItem != null) 
            {
                _orderItems.Remove(orderItem);
            }
        }

    }
}
