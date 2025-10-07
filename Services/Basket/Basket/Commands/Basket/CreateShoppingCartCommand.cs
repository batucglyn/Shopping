using Basket.DTOs;
using Basket.Responses;
using MediatR;

namespace Basket.Commands.Basket
{
    public record class CreateShoppingCartCommand(string UserName,List<CreateShoppingCartItemDTO> Items):IRequest<ShoppingCartResponse>;
   
}
