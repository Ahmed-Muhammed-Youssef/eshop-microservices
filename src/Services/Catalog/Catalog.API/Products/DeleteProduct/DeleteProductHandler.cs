
namespace Catalog.API.Products.DeleteProduct
{
    public record DeleteProductCommand(Guid Id) : ICommand<DeleteProductResult>;
    public record DeleteProductResult(bool IsSuccess);

    public class DeleteProductCommandValidator : AbstractValidator<DeleteProductCommand>
    {
        public DeleteProductCommandValidator()
        {
            RuleFor(c => c.Id).NotEmpty().WithMessage("Id is required");
        }
    }
    public class DeleteProductHandler(IDocumentSession documentSession, ILogger<DeleteProductHandler> logger) : ICommandHandler<DeleteProductCommand, DeleteProductResult>
    {
        private readonly IDocumentSession _documentSession = documentSession;
        private readonly ILogger<DeleteProductHandler> _logger = logger;

        public async Task<DeleteProductResult> Handle(DeleteProductCommand command, CancellationToken cancellationToken)
        {
            _logger.LogInformation(nameof(DeleteProductHandler) + " is called with command {@Command}", command);
            _documentSession.Delete<Product>(command.Id);

            await _documentSession.SaveChangesAsync(cancellationToken);

            return new DeleteProductResult(true);
        }
    }
}
