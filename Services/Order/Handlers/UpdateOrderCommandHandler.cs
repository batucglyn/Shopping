using MediatR;
using Ordering.Commands;
using Ordering.Entities;
using Ordering.Exceptions;
using Ordering.Mappers;
using Ordering.Repositories;

namespace Ordering.Handlers
{
    public class UpdateOrderCommandHandler : IRequestHandler<UpdateOrderCommand, Unit>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ILogger<UpdateOrderCommandHandler> _logger;
        public UpdateOrderCommandHandler(IOrderRepository orderRepository, ILogger<UpdateOrderCommandHandler> logger)
        {
            _orderRepository = orderRepository;
            _logger = logger;
        }

       
        public async Task<Unit> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
        {

            var orderToUpdate = await _orderRepository.GetByIdAsync(request.Id);
            if (orderToUpdate == null)
            {
                throw new OrderNotFoundException(nameof(Order), request.Id);
            }
            orderToUpdate.MapUpdate(request);

            await _orderRepository.UpdateAsync(orderToUpdate);
            _logger.LogInformation($"Order {orderToUpdate.Id} is successfully updated.");
            return Unit.Value;
        }





    }
 }

