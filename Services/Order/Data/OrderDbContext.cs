using Microsoft.EntityFrameworkCore;
using Ordering.Entities;

namespace Ordering.Data
{
    public class OrderDbContext:DbContext
    {
        public OrderDbContext(DbContextOptions<OrderDbContext>options):base(options) { }                 
        
        public DbSet<Order> Orders { get; set; }
        public DbSet<OutboxMessage> OutboxMessages { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<OutboxMessage>(builder => {
                builder.HasKey(x => x.Id);
                builder.HasIndex(x => x.CorrelationId);
                builder.Property(x => x.Type).IsRequired();
                builder.Property(x => x.Content).IsRequired();
                builder.Property(x => x.OccurredOn).IsRequired();
                builder.Property(x => x.ProcessedOn).IsRequired(false);
      
                });
            modelBuilder.Entity<Order>()
                .Property(o => o.Status).HasConversion<string>();



        }
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
