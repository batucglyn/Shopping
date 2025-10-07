using MediatR;

namespace Basket.Commands.Basket
{
    public record  DeleteBasketByUserNameCommand(string userName):IRequest<Unit>;
   
}
