using Ordering.Entities;

namespace Ordering.Data
{
    public class OrderContextSeed
    {
        public static async Task SeedAsync(OrderDbContext orderContext, ILogger<OrderContextSeed> logger)
        {
            if (!orderContext.Orders.Any())
            {
                orderContext.Orders.AddRange(GetOrders());
                await orderContext.SaveChangesAsync();
                logger.LogInformation($"Ordering Database: {typeof(OrderDbContext).Name} seeded!!!");
            }
        }

        private static IEnumerable<Order> GetOrders()
        {
            return new List<Order>
            {
                new()
                {
                    UserName = "batu",
                    FirstName = "Batuhan",
                    LastName = "Caglayan",
                    EmailAddress = "batucglyn@eCommerce.net",
                    AddressLine = "bursa,kentmeydanı",
                    Country = "Turkey",
                    TotalPrice = 750,
                    State = "KA",
                    ZipCode = "560001",

                    CardName = "Visa",
                    CardNumber = "4111111111111111",
                    CreatedBy = "Batuhan",
                    Expiration = "12/25",
                    Cvv = "123",
                    PaymentMethod = 1,
                    LastModifiedBy = "Batuhan",
                    LastModifiedDate = new DateTimeOffset(),
                }
            };
        }
    }
}
