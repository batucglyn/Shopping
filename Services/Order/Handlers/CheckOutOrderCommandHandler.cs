using MediatR;
using Ordering.Commands;
using Ordering.Mappers;
using Ordering.Repositories;

namespace Ordering.Handlers
{
    public class CheckOutOrderCommandHandler : IRequestHandler<CheckOutOrderCommand, Guid>
    {

        private readonly IOrderRepository _orderRepository;
        private readonly ILogger<CheckOutOrderCommandHandler> _logger;
        public CheckOutOrderCommandHandler(IOrderRepository orderRepository, ILogger<CheckOutOrderCommandHandler> logger)
        {
            _orderRepository = orderRepository;
            _logger = logger;
        }

        public async Task<Guid> Handle(CheckOutOrderCommand request, CancellationToken cancellationToken)
        {

            var orderEntity= request.ToEntity();
            var order= await _orderRepository.AddAsync(orderEntity);

            var outboxMessage = order.ToOutboxMessage();
            await _orderRepository.AddOutboxMessageAsync(outboxMessage);


            _logger.LogInformation($"Order with Id{order.Id}successfully Order with outbox message created.");
            return order.Id;




        }
    }
}
