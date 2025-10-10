using Microsoft.EntityFrameworkCore;
using Ordering.Data;
using Ordering.Repositories;

namespace Ordering.Extensions
{
    public static class InfraServices
    {
        public static IServiceCollection AddInfraServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<OrderDbContext>(options => options.UseSqlServer(
                configuration.GetConnectionString("OrderingConnectionString"),
                sqlServerOptions => sqlServerOptions.EnableRetryOnFailure()));
            services.AddScoped(typeof(IAsyncRepository<>), typeof(BaseRepository<>));
            services.AddScoped<IOrderRepository, OrderRepository>();
            return services;
        }
    }
}
