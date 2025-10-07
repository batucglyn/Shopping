using Discount.Commands;
using Discount.DTOs;
using Discount.Extensions;
using Discount.Mappers;
using Discount.Repositories;
using Grpc.Core;
using MediatR;

namespace Discount.Handlers
{
    public class UpdateDiscountCommandHandler : IRequestHandler<UpdateDiscountCommand, CouponDto>
    {

        private readonly IDiscountRepository _discountRepository;

        public UpdateDiscountCommandHandler(IDiscountRepository discountRepository)
        {
            _discountRepository = discountRepository;
        }

        public async Task<CouponDto> Handle(UpdateDiscountCommand request, CancellationToken cancellationToken)
        {

            var validationErrors = new Dictionary<string, string>();

            if (string.IsNullOrWhiteSpace(request.ProductName))
                validationErrors["ProductName"] = "Product name must not be empty.";

            if (string.IsNullOrWhiteSpace(request.Description))
                validationErrors["Description"] = "Product description must not be empty.";

            if (request.Amount <= 0)
                validationErrors["Amount"] = "Amount must be greater than zero.";


            if (validationErrors.Any())
                throw GrpcErrorHelper.CreateValidationException(validationErrors);

            var coupon= request.ToEntity();

            var updated=await _discountRepository.UpdateDiscount(coupon);

            if (!updated)
            {

                throw new RpcException(new Status(StatusCode.NotFound, $"Failed updated product ={request.ProductName}"));
            }

            return coupon.ToDto();


        }
    }
}
