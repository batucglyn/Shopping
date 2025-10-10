using MediatR;
using Ordering.DTOs;
using Ordering.Mappers;
using Ordering.Queries;
using Ordering.Repositories;

namespace Ordering.Handlers
{
    public class GetOrderListQueryHandler : IRequestHandler<GetOrderListQuery, List<OrderDTO>>
    {

        private readonly IOrderRepository _orderRepository;

        public GetOrderListQueryHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<List<OrderDTO>> Handle(GetOrderListQuery request, CancellationToken cancellationToken)
        {
         var orders=  await _orderRepository.GetOrderByUserNameAsync(request.userName);

            return orders.Select(o => o.ToDto()).ToList();



        }
    }
}
