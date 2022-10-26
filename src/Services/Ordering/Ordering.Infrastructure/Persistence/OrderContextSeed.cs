using Microsoft.Extensions.Logging;
using Ordering.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Infrastructure.Persistence
{
    public class OrderContextSeed
    {
        public static async Task SeedAsync(OrderContext orderContext, ILogger<OrderContextSeed> logger)
        {
            if (!orderContext.Orders.Any())
            {
                orderContext.Orders.AddRange(GetPrefonfiguredOrders());
                await orderContext.SaveChangesAsync();
                logger.LogInformation("Seed databaase associated with context {DbContextName}", typeof(OrderContext).Name);
            }
        }

        private static IEnumerable<Order> GetPrefonfiguredOrders()
        {
            return new List<Order> {
                new Order() {
                    UserName = "dkhan",
                    FirstName= "danish",
                    LastName = "khan",
                    EmailAddress = "dkhan895@gmail.com",
                    AddressLine = "Noida",
                    Country = "India",
                    TotalPrice = 450
                }
            };
        }
    }
}
