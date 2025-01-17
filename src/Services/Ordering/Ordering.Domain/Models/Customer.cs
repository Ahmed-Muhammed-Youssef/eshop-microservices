namespace Ordering.Domain.Models
{
    public class Customer : Entity<Guid>
    {
        public string Name { get; private set; } = default!;
        public string Email { get; private set; } = default!;
        public static Customer Create(Guid id, string name, string email)
        {
            ArgumentNullException.ThrowIfNull(name);
            ArgumentNullException.ThrowIfNull(email);

            return new Customer { Id = id, Name = name, Email = email };
        }
    }
}
