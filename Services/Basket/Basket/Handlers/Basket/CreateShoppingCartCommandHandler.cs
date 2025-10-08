using Basket.Commands.Basket;
using Basket.GrpcService;
using Basket.Mappers;
using Basket.Repositories;
using Basket.Responses;
using MediatR;

namespace Basket.Handlers.Basket
{
    public class CreateShoppingCartCommandHandler : IRequestHandler<CreateShoppingCartCommand, ShoppingCartResponse>
    {

        private readonly IBasketRepositories _basketRepositories;
        private readonly DiscountGrpcService _discountGrpcService;
        public CreateShoppingCartCommandHandler(IBasketRepositories basketRepositories, DiscountGrpcService discountGrpcService)
        {
            _basketRepositories = basketRepositories;
            _discountGrpcService = discountGrpcService;
        }

        public async Task<ShoppingCartResponse> Handle(CreateShoppingCartCommand request, CancellationToken cancellationToken)
        {
            //apply grpc discount coupon
            foreach(var item in request.Items)
            {
                var coupon = await _discountGrpcService.GetDiscount(item.ProductName);
                item.Price -= coupon.Amount;

            }

            var shoppingCartEntity = request.ToEntity();

            var updatedCart=await _basketRepositories.UpsertBasket(shoppingCartEntity);

            return updatedCart.ToResponse();


        }
    }
}
