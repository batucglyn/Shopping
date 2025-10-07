using Basket.Commands.Basket;
using Basket.Mappers;
using Basket.Repositories;
using Basket.Responses;
using MediatR;

namespace Basket.Handlers.Basket
{
    public class CreateShoppingCartCommandHandler : IRequestHandler<CreateShoppingCartCommand, ShoppingCartResponse>
    {

        private readonly IBasketRepositories _basketRepositories;

        public CreateShoppingCartCommandHandler(IBasketRepositories basketRepositories)
        {
            _basketRepositories = basketRepositories;
        }

        public async Task<ShoppingCartResponse> Handle(CreateShoppingCartCommand request, CancellationToken cancellationToken)
        {

            var shoppingCartEntity = request.ToEntity();

            var updatedCart=await _basketRepositories.UpsertBasket(shoppingCartEntity);

            return updatedCart.ToResponse();


        }
    }
}
