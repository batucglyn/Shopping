using Catalog.Commands.Product;
using Catalog.Mappers;
using Catalog.Repositories;
using MediatR;

namespace Catalog.Handlers.Product
{
    public class UpdateProductCommandHandler:IRequestHandler<UpdateProductCommand,bool>
    {

        private readonly IProductRepository _productRepository;

        public UpdateProductCommandHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<bool> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {


            var existing= await _productRepository.GetProductById(request.Id);
            if (existing == null) {

                throw new KeyNotFoundException($"Product with Id {request.Id} not found ");

            
            }

            var brand =await _productRepository.GetBrandByIdAsync(request.BrandId);
            var type =await _productRepository.GetTypeByIdAsync(request.TypeId);
            if (brand == null || type == null)
            {

                throw new ApplicationException("Invalid brand or type specified");

            }


           var product = request.ToUpdateEntity(existing, brand, type);

            return await _productRepository.UpdateProduct(product);
 



        }
    }
}
