using Discount.DTOs;
using MediatR;

namespace Discount.Commands
{
    public record class CreateDiscountCommand(string ProductName,string Description ,int Amount):IRequest<CouponDto>;
    
    
}
