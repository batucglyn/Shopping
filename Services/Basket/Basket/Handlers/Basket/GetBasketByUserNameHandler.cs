using Basket.Mappers;
using Basket.Queries.Basket;
using Basket.Repositories;
using Basket.Responses;
using MediatR;

namespace Basket.Handlers.Basket
{
    public class GetBasketByUserNameHandler : IRequestHandler<GetBasketByUserNameQuery, ShoppingCartResponse>
    {
        private readonly IBasketRepositories _basketRepositories;

        public GetBasketByUserNameHandler(IBasketRepositories basketRepositories)
        {
            _basketRepositories = basketRepositories;
        }

        public async Task<ShoppingCartResponse> Handle(GetBasketByUserNameQuery request, CancellationToken cancellationToken)
        {
            var shoppingCart = await _basketRepositories.GetBasket(request.userName);
            if (shoppingCart == null) {

                return new ShoppingCartResponse(request.userName)
                {
                    Items = new List<ShoppingCartItemResponse>()
                };
            
            
            }

            return shoppingCart.ToResponse();

        }
    }
}
