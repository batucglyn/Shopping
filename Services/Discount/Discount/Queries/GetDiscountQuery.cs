using Discount.DTOs;
using MediatR;

namespace Discount.Queries
{
    public record class GetDiscountQuery(string productName ):IRequest<CouponDto>
    {

    }
}
