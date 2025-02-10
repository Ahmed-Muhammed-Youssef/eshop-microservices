using FluentValidation;

namespace Ordering.Application.Orders.Queries.GetOrdersByName
{
    public record GetOrderByNameQueryResult(IEnumerable<OrderDto> Orders);
    public record GetOrderByNameQuery(string Name) : IQuery<GetOrderByNameQueryResult>;
    public class GetOrderByNameValidator : AbstractValidator<GetOrderByNameQuery>
    {
        public GetOrderByNameValidator()
        {
            RuleFor(o => o.Name).NotEmpty().WithMessage("Name is required");
        }
    }
}
