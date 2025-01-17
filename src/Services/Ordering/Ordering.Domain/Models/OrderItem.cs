using Ordering.Domain.Abstractions;

namespace Ordering.Domain.Models
{
    public class OrderItem : Entity<Guid>
    {
        public Guid OrderId { get; private set; }
        public Guid ProductId { get; private set; }
        public decimal Price { get; private set; }
        public int Quantity { get; private set; }
    }
}
