using Ordering.Domain.Models;

namespace Ordering.API.Extensions
{
    public class InitialData
    {
        public static IEnumerable<Customer> Customers =>
            [
                Customer.Create(Guid.NewGuid(), "Ahmed Youssef", "ahmed@gmail.com"),
                Customer.Create(Guid.NewGuid(), "Zaki Youssef", "zaki@gmail.com"),
                Customer.Create(Guid.NewGuid(), "Ali Ali", "Ali@gmail.com")
            ];
    }
}
