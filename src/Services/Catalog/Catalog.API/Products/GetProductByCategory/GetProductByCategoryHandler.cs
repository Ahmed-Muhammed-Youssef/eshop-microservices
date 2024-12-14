namespace Catalog.API.Products.GetProductByCategory
{
    public record GetProductByCategoryQuery(string Category) : IQuery<GetProductByCategoryResult>;
    public record GetProductByCategoryResult(IEnumerable<Product> Products);
    internal class GetProductByCategoryHandler(IDocumentSession doumentSession) : IQueryHandler<GetProductByCategoryQuery, GetProductByCategoryResult>
    {
        private readonly IDocumentSession _doumentSession = doumentSession;

        public async Task<GetProductByCategoryResult> Handle(GetProductByCategoryQuery query, CancellationToken cancellationToken)
        {
            IEnumerable<Product> products = await _doumentSession.Query<Product>().Where(p => p.Category.Contains(query.Category)).ToListAsync(token: cancellationToken);

            return new GetProductByCategoryResult(products);
        }
    }
}
