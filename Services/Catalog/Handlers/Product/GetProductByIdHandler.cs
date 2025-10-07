using Catalog.DTOs;
using Catalog.Queries.Product;
using Catalog.Repositories;
using MediatR;
using Catalog.Mappers;
namespace Catalog.Handlers.Product
{
    public class GetProductByIdHandler : IRequestHandler<GetProductByIdQuery, ProductResponse>
    {
        private readonly IProductRepository _pruductRepository;

        public GetProductByIdHandler(IProductRepository pruductRepository)
        {
            _pruductRepository = pruductRepository;
        }

        public async Task<ProductResponse> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {

            var product=await _pruductRepository.GetProductById(request.id);

            
             var productResponse =product.ProductToProductResponse();

            return productResponse;
        }
    }
}
