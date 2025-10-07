using Discount.Commands;
using Discount.Extensions;
using Discount.Repositories;
using MediatR;

namespace Discount.Handlers
{
    public class DeleteDiscountCommandHandler : IRequestHandler<DeleteDiscountCommand, bool>
    {

        private readonly IDiscountRepository _discountRepository;

        public DeleteDiscountCommandHandler(IDiscountRepository discountRepository)
        {
            _discountRepository = discountRepository;
        }

        public async Task<bool> Handle(DeleteDiscountCommand request, CancellationToken cancellationToken)
        {

            if (string.IsNullOrWhiteSpace(request.ProductName))
            {
                var validationErrors = new Dictionary<string, string>
                    {
                        { "ProductName", "Product name must not be empty." }
                    };

                throw GrpcErrorHelper.CreateValidationException(validationErrors);
            }

            var deleted = await _discountRepository.DeleteDiscount(request.ProductName);

            return deleted;






        }
    }
}
