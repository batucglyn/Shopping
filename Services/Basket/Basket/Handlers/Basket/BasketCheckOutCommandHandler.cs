using Basket.Commands.Basket;
using Basket.Mappers;
using Basket.Queries.Basket;
using MassTransit;
using MediatR;

namespace Basket.Handlers.Basket
{
    public class BasketCheckOutCommandHandler : IRequestHandler<CheckOutBasketCommand, Unit>
    {

        private readonly IMediator _mediator;
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly ILogger<BasketCheckOutCommandHandler> _logger;
        public BasketCheckOutCommandHandler(IMediator mediator, IPublishEndpoint publishEndpoint, ILogger<BasketCheckOutCommandHandler> logger)
        {
            _mediator = mediator;
            _publishEndpoint = publishEndpoint;
            _logger = logger;
        }

        public async Task<Unit> Handle(CheckOutBasketCommand request, CancellationToken cancellationToken)
        {
           

            var dto= request.Dto;
             var basketResponse=   await _mediator.Send(new GetBasketByUserNameQuery(dto.UserName),cancellationToken);
            if (basketResponse is null || !basketResponse.Items.Any())
            {
                throw new InvalidOperationException("Basket not found or Basket empty");
            }
            var basket = basketResponse.ToEntity();

            //Map
            var evt = dto.ToBasketCheckoutEvent(basket);
            _logger.LogInformation("Publishing BasketCheckoutEvent for {User}", basket.UserName);
            await _publishEndpoint.Publish(evt, cancellationToken);

            //delete the basket
            await _mediator.Send(new DeleteBasketByUserNameCommand(dto.UserName), cancellationToken);
            return Unit.Value;
        }
    }
}
