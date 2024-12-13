

namespace Catalog.API.Products.CreateProduct
{
    public record CreateProductCommand(string Name, List<string> Category, string Description, string ImageFile, decimal Price) : ICommand<CreateProductResult>;
    public record CreateProductResult(Guid Id);

    public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
    {
        public CreateProductCommandValidator()
        {
            RuleFor(c => c.Name).NotEmpty().WithMessage("Name is required");
            RuleFor(c => c.Category).NotEmpty().WithMessage("Category is required");
            RuleFor(c => c.ImageFile).NotEmpty().WithMessage("ImageFile is required");
            RuleFor(c => c.Price).GreaterThan(0).WithMessage("Price must be greater than 0");
        }

    }

    internal class CreateProductCommandHandler(IDocumentSession documentSession, IValidator<CreateProductCommand> validator) : ICommandHandler<CreateProductCommand, CreateProductResult>
    {
        private readonly IDocumentSession _documentSession = documentSession;
        private readonly IValidator<CreateProductCommand> _validator = validator;

        public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
        {
            var result = await _validator.ValidateAsync(command, cancellationToken);

            var errors = result.Errors.Select(e => e.ErrorMessage).ToList();

            if (errors.Count != 0) 
            {
                throw new ValidationException(errors.FirstOrDefault());
            }

            Product product = new()
            {
                Name = command.Name,
                Category = command.Category,
                Description = command.Description,
                ImageFile = command.ImageFile,
                Price = command.Price
            };

            _documentSession.Store(product);

            await _documentSession.SaveChangesAsync(cancellationToken);

            return new CreateProductResult(Id: product.Id);
        }
    }
}
