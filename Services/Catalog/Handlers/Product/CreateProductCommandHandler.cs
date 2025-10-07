using Catalog.Commands.Product;
using Catalog.DTOs;
using Catalog.Entities;
using Catalog.Mappers;
using Catalog.Repositories;
using MediatR;

namespace Catalog.Handlers.Product
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, ProductResponse>
    {
        private readonly IProductRepository _productRepository;

        public CreateProductCommandHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<ProductResponse> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var brand=await _productRepository.GetBrandByIdAsync(request.BrandId);
            var type=await _productRepository.GetTypeByIdAsync(request.TypeId);

            if(brand ==null ||type == null)
            {

                throw new ApplicationException("Invalid brand or type specified");

            }

           var  productEntity= request.ToEntity(brand, type);
           var product= await _productRepository.CreateProduct(productEntity);

            return product.ProductToProductResponse();

        }
    }
}
