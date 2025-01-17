namespace Ordering.Domain.Models
{
    public class Product : Entity<Guid>
    {
        public string Name { get; private set; } = default!;
        public decimal Price { get; private set; }  

        public static Product Create(Guid id, string name, decimal price)
        {
            ArgumentNullException.ThrowIfNull(id);
            ArgumentException.ThrowIfNullOrEmpty(name);
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(price);

            return new Product { Id = id, Name = name, Price = price };
        }
    }
}
