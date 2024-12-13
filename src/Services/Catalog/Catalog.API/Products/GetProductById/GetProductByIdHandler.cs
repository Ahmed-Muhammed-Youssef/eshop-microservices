using Catalog.API.Exceptions;

namespace Catalog.API.Products.GetProductById
{
    public record GetProductByIdQuery(Guid Id) : IQuery<GetProductByIdResult>;
    public record GetProductByIdResult(Product Product);
    public class GetProductByIdHandler(IDocumentSession documentSession, ILogger<GetProductByIdHandler> logger) : IQueryHandler<GetProductByIdQuery, GetProductByIdResult>
    {
        private readonly IDocumentSession _documentSession = documentSession;
        private readonly ILogger<GetProductByIdHandler> _logger = logger;

        public async Task<GetProductByIdResult> Handle(GetProductByIdQuery query, CancellationToken cancellationToken)
        {
            _logger.LogInformation(nameof(GetProductByIdHandler) + " is called with query {@Query}", query);

            var product = await _documentSession.LoadAsync<Product>(query.Id, cancellationToken) ?? throw new ProductNotFoundException(query.Id);

            return new GetProductByIdResult(product);
        }
       
    }
}
