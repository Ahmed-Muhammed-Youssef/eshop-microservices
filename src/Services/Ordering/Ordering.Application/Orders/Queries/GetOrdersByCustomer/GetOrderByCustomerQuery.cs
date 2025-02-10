using FluentValidation;

namespace Ordering.Application.Orders.Queries.GetOrdersByCustomer
{
    public record GetOrdersByCustomerQuery(Guid CustomerId) : IQuery<GetOrderByCustomerQueryResult>;
    public record GetOrderByCustomerQueryResult(IEnumerable<OrderDto> Orders);
    public class GetOrderByCustomerValidator : AbstractValidator<GetOrdersByCustomerQuery>
    {
        public GetOrderByCustomerValidator()
        {
            RuleFor(o => o.CustomerId).NotEmpty().WithMessage("CustomerId is required");
        }
    }
}
