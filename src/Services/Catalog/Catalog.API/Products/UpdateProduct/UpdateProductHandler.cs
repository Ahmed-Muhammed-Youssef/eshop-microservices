namespace Catalog.API.Products.UpdateProduct
{
    public record UpdateProductCommand(Guid Id, string Name, List<string> Category, string ImageFile, string Description, decimal Price) : ICommand<UpdateProductResult>;
    public record UpdateProductResult(bool IsSuccess);

    internal class UpdateProductHandler(IDocumentSession documentSession, ILogger<UpdateProductHandler> logger) : ICommandHandler<UpdateProductCommand, UpdateProductResult>
    {
        private readonly IDocumentSession _documentSession = documentSession;
        private readonly ILogger<UpdateProductHandler> _logger = logger;

        public async Task<UpdateProductResult> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
        {
            _logger.LogInformation(nameof(UpdateProductHandler) + " is called with the command {@Command}", command);

            var product = await _documentSession.LoadAsync<Product>(command.Id, cancellationToken);

            if (product is null)
            {
                throw new Exception("Product not found");
            }
            
            product.Name = command.Name;
            product.Category = command.Category;
            product.ImageFile = command.ImageFile;
            product.Description = command.Description;
            product.Price = command.Price;

            _documentSession.Update(product);
            await _documentSession.SaveChangesAsync(cancellationToken);

            return new UpdateProductResult(true);
        }
    }
}
