using FluentValidation;

namespace Ordering.Application.Orders.Commands.UpdateOrder
{
    public record UpdateOrderCommand(OrderDto Order) : ICommand<UpdateOrderCommandResult>;
    public record UpdateOrderCommandResult(bool IsSuccess);

    public class UpdateOrderCommandValidator : AbstractValidator<UpdateOrderCommand>
    {
        public UpdateOrderCommandValidator()
        {
            RuleFor(o => o.Order.OrderName).NotEmpty().WithMessage("Name is required");
            RuleFor(o => o.Order.CustomerId).NotNull().WithMessage("CustomerId is required");
            RuleFor(o => o.Order.OrderItems).NotEmpty().WithMessage("OrderItems should not be empty");
        }
    }
}
