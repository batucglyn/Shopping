using Basket.DTOs;
using Basket.Entities;
using MediatR;

namespace Basket.Commands.Basket
{
    public record class CheckOutBasketCommand(BasketCheckOutDto Dto):IRequest<Unit>
    {


    }
}
