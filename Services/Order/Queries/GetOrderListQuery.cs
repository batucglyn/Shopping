using MediatR;
using Ordering.DTOs;

namespace Ordering.Queries
{
    public record class GetOrderListQuery(string userName):IRequest<List<OrderDTO>>;
   
}
