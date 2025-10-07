using MediatR;

namespace Discount.Commands
{
    public record class DeleteDiscountCommand(string ProductName):IRequest<bool>;
   



}
