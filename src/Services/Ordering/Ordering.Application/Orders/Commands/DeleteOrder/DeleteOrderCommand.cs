using FluentValidation;

public record DeleteOrderResult(bool IsSuccess);
public record DeleteOrderCommand(Guid OrderId) : ICommand<DeleteOrderResult>;
public class DeleteOrderCommandValidator : AbstractValidator<DeleteOrderCommand>
{
    public DeleteOrderCommandValidator()
    {
        RuleFor(o => o.OrderId).NotEmpty().WithMessage("OrderId is required");
    }
}