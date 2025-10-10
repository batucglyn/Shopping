using MediatR;
using Ordering.Commands;
using Ordering.Entities;
using Ordering.Exceptions;
using Ordering.Repositories;

namespace Ordering.Handlers
{
    public class DeleteOrderCommandHandler : IRequestHandler<DeleteOrderCommand, Unit>
    {

        private readonly IOrderRepository _orderRepository;

        private readonly ILogger<DeleteOrderCommandHandler> _logger;
        public DeleteOrderCommandHandler(IOrderRepository orderRepository, ILogger<DeleteOrderCommandHandler> logger)
        {
            _orderRepository = orderRepository;
            _logger = logger;
        }

        public async Task<Unit> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
        {


            var matchedOrder= await _orderRepository.GetByIdAsync(request.Id);
            if (matchedOrder == null) {

                throw new OrderNotFoundException(nameof(Order), request.Id);
            
            }

            _logger.LogInformation($"Order with Id {request.Id} has been deleted successfully");
            await _orderRepository.DeleteAsync(matchedOrder);

            return Unit.Value;

        }
    }
}
