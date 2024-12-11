using Catalog.API.Models;

namespace Catalog.API.Products.GetProducts
{
    public record GetProductsQuery() : IQuery<GetProductsResult>;
    public record GetProductsResult(IEnumerable<Product> products);
    internal class GetProductsHandler(IDocumentSession documentSession, ILogger<GetProductsHandler> logger) : IQueryHandler<GetProductsQuery, GetProductsResult>
    {
        private readonly IDocumentSession _documentSession = documentSession;
        private readonly ILogger<GetProductsHandler> _logger = logger;

        public async Task<GetProductsResult> Handle(GetProductsQuery query, CancellationToken cancellationToken)
        {
            _logger.LogInformation(nameof(GetProductsHandler) + " is called with query {@Query}", query);

            var products = await _documentSession.Query<Product>().ToListAsync(cancellationToken);

            return new GetProductsResult(products);
        }
    }
}
