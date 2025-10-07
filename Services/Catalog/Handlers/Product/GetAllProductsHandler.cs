using Catalog.DTOs;
using Catalog.Mappers;
using Catalog.Queries.Product;
using Catalog.Repositories;
using Catalog.Specification;
using MediatR;

namespace Catalog.Handlers.Product
{
    public class GetAllProductsHandler : IRequestHandler<GetAllProductsQuery, Pagination<ProductResponse>>
    {

        private readonly IProductRepository _productRepository;

        public GetAllProductsHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<Pagination<ProductResponse>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {

            var productsList = await _productRepository.GetProducts(request.catalogSpecParams);
            var productResponseList = productsList.ToResponse();
            return productResponseList;

        }
    }
}
