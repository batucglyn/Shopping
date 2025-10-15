using Microsoft.EntityFrameworkCore;
using Ordering.Data;
using Ordering.Entities;
using System.Linq.Expressions;

namespace Ordering.Repositories
{
    public class OrderRepository : BaseRepository<Order>, IOrderRepository
    {
        public OrderRepository(OrderDbContext dbContext):base(dbContext) { } //base repoya ilet

       
        public async Task<IEnumerable<Order>> GetOrderByUserNameAsync(string username)
        {
           var orders=await _dbContext.Orders.AsNoTracking().
                 Where(o => o.UserName == username).
                 ToListAsync();

            return orders;
        }
        public async Task AddOutboxMessageAsync(OutboxMessage outboxMessage)
        {
            await _dbContext.OutboxMessages.AddAsync(outboxMessage);
           await _dbContext.SaveChangesAsync();
        }
    }
}
