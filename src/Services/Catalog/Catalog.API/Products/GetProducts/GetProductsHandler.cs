namespace Catalog.API.Products.GetProducts
{
    public record GetProductsQuery() : IQuery<GetProductsResult>;
    public record GetProductsResult(IEnumerable<Product> products);
    internal class GetProductsHandler(IDocumentSession documentSession) : IQueryHandler<GetProductsQuery, GetProductsResult>
    {
        private readonly IDocumentSession _documentSession = documentSession;
        public async Task<GetProductsResult> Handle(GetProductsQuery query, CancellationToken cancellationToken)
        {
            var products = await _documentSession.Query<Product>().ToListAsync(cancellationToken);

            return new GetProductsResult(products);
        }
    }
}
