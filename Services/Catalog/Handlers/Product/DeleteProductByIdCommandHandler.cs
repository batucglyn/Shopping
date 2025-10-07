using Catalog.Commands.Product;
using Catalog.Repositories;
using MediatR;

namespace Catalog.Handlers.Product
{
    public class DeleteProductByIdCommandHandler : IRequestHandler<DeleteProducByIdCommand, bool>
    {

        private readonly IProductRepository _productRepository;

        public DeleteProductByIdCommandHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<bool> Handle(DeleteProducByIdCommand request, CancellationToken cancellationToken)
        {

            return await _productRepository.DeleteProduct(request.productId);
        }
    }
}
