using Discount.DTOs;
using Discount.Entities;
using Discount.Extensions;
using Discount.Mappers;
using Discount.Queries;
using Discount.Repositories;
using Grpc.Core;
using MediatR;

namespace Discount.Handlers
{
    public class GetDiscountQueryHandler : IRequestHandler<GetDiscountQuery, CouponDto>
    {

        private readonly IDiscountRepository _discountRepository;

        public GetDiscountQueryHandler(IDiscountRepository discountRepository)
        {
            _discountRepository = discountRepository;
        }

        public async Task<CouponDto> Handle(GetDiscountQuery request, CancellationToken cancellationToken)
        {


            if (string.IsNullOrWhiteSpace(request.productName))
            {
                var validationErrors = new Dictionary<string, string>
                {
                    { "ProductName", "ProductName must not be empty." }
                };

                throw GrpcErrorHelper.CreateValidationException(validationErrors);
            }

            var productName = request.productName.Trim();

            var coupon = await _discountRepository.GetDiscount(productName);

            if (coupon is null ||
                string.Equals(coupon.ProductName, "No Discount", StringComparison.OrdinalIgnoreCase))
            {
                return new CouponDto(
                    0,
                    productName,
                    "Kupon Bulunamadı",
                    0
                );
            }
            return coupon.ToDto();
        }
    }
}
