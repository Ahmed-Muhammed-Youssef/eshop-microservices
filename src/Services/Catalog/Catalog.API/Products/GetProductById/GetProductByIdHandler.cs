namespace Catalog.API.Products.GetProductById
{
    public record GetProductByIdQuery(Guid Id) : IQuery<GetProductByIdResult>;
    public record GetProductByIdResult(Product Product);
    public class GetProductByIdHandler(IDocumentSession documentSession) : IQueryHandler<GetProductByIdQuery, GetProductByIdResult>
    {
        private readonly IDocumentSession _documentSession = documentSession;

        public async Task<GetProductByIdResult> Handle(GetProductByIdQuery query, CancellationToken cancellationToken)
        {
            var product = await _documentSession.LoadAsync<Product>(query.Id, cancellationToken) ?? throw new ProductNotFoundException(query.Id);

            return new GetProductByIdResult(product);
        }
       
    }
}
