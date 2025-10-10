using Ordering.Entities;

namespace Ordering.Repositories
{
    public interface IOrderRepository:IAsyncRepository<Order>
    {
        Task<IEnumerable<Order>> GetOrderByUserNameAsync(string username);

    }
}
