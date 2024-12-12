namespace Catalog.API.Products.GetProductByCategory
{
    public record GetProductByCategoryQuery(string Category) : IQuery<GetProductByCategoryResult>;
    public record GetProductByCategoryResult(IEnumerable<Product> Products);
    internal class GetProductByCategoryHandler(IDocumentSession doumentSession, ILogger<GetProductByCategoryHandler> logger) : IQueryHandler<GetProductByCategoryQuery, GetProductByCategoryResult>
    {
        private readonly IDocumentSession _doumentSession = doumentSession;
        private readonly ILogger<GetProductByCategoryHandler> _logger = logger;

        public async Task<GetProductByCategoryResult> Handle(GetProductByCategoryQuery query, CancellationToken cancellationToken)
        {
            _logger.LogInformation(nameof(GetProductByCategoryHandler) + " is called with query {@Query}", query);

            IEnumerable<Product> products = await _doumentSession.Query<Product>().Where(p => p.Category.Contains(query.Category)).ToListAsync(token: cancellationToken);

            return new GetProductByCategoryResult(products);
        }
    }
}
