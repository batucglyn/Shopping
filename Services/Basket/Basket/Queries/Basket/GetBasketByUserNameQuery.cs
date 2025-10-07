using Basket.Entities;
using Basket.Responses;
using MediatR;

namespace Basket.Queries.Basket
{
    public record class GetBasketByUserNameQuery(string userName):IRequest<ShoppingCartResponse>
    {













    }
}
