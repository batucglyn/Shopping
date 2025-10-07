using Basket.Commands.Basket;
using Basket.Repositories;
using MediatR;

namespace Basket.Handlers.Basket
{
    public class DeleteBasketByUserNameCommandHandler : IRequestHandler<DeleteBasketByUserNameCommand, Unit>
    {
        private readonly IBasketRepositories _basketRepositories;

        public DeleteBasketByUserNameCommandHandler(IBasketRepositories basketRepositories)
        {
            _basketRepositories = basketRepositories;
        }

        public async Task<Unit> Handle(DeleteBasketByUserNameCommand request, CancellationToken cancellationToken)
        {
            await _basketRepositories.DeleteBasket(request.userName);
            return Unit.Value;
                
        }
    }
}
