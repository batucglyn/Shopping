using Catalog.DTOs;
using Catalog.Mappers;
using Catalog.Queries.Product;
using Catalog.Repositories;
using MediatR;

namespace Catalog.Handlers.Product
{
    public class GetProductByBrandNameHandler : IRequestHandler<GetProductByBrandNameQuery, IList<ProductResponse>>
    {
        private readonly IProductRepository _productRepository;

        public GetProductByBrandNameHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<IList<ProductResponse>> Handle(GetProductByBrandNameQuery request, CancellationToken cancellationToken)
        {
           var products= await  _productRepository.GetProductsByBrand(request.brandName);

            return products.ProductsToProductsResponseList().ToList();

        }
    }
}
