using Catalog.DTOs;
using Catalog.Mappers;
using Catalog.Queries.Product;
using Catalog.Repositories;
using MediatR;

namespace Catalog.Handlers.Product
{
    public class GetProductsByNameHandler : IRequestHandler<GetProductsByNameQuery, IList<ProductResponse>>
    {
        private readonly IProductRepository _productRepository;

        public GetProductsByNameHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<IList<ProductResponse>> Handle(GetProductsByNameQuery request, CancellationToken cancellationToken)
        {

                var productList =await _productRepository.GetProducstByName(request.name);

            return productList.ProductsToProductsResponseList().ToList();

        }
    }
}
