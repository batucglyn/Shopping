using Microsoft.EntityFrameworkCore;
using Ordering.Entities;

namespace Ordering.Data
{
    public class OrderDbContext:DbContext
    {
        public OrderDbContext(DbContextOptions<OrderDbContext>options):base(options) { }                 
        
        public DbSet<Order> Orders { get; set; }




        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var entry in ChangeTracker.Entries<BaseEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedDate = DateTimeOffset.UtcNow;
                        entry.Entity.CreatedBy = "Batu"; //TODO: Replace this with User from IUserService
                        break;
                    case EntityState.Modified:
                        entry.Entity.LastModifiedDate = DateTimeOffset.UtcNow;
                        entry.Entity.LastModifiedBy = "Batu";
                        break;
                }
            }


            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
