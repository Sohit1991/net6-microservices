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
                orderContext.Orders.AddRange(GetPreConfiguredOrders());
                await orderContext.SaveChangesAsync();
                logger.LogInformation($"Seed database associate with context {typeof(OrderContext).Name}");
            }
        }
        private static IEnumerable<Order> GetPreConfiguredOrders()
        {
            return new List<Order>()
            {
                new Order() {
                    UserName="sohit",
                    FirstName="Sohi",
                    LastName="Khanchu",
                    EmailAddress="sohitkhanchi@gmail.com",
                    AddressLine="Vpo Ahar",
                    Country="India",
                    TotalPrice=350,
                    CVV="123",
                    CardName="sohit",
                    CardNumber="123456789",
                    Expiration="0925",
                    PaymentMethod=1,
                    State="Haryana",
                    ZipCode="132107",
                    LastModifiedBy="sohit",
                    LastModifiedDate=DateTime.Now

                    
                }
            };
        }
    }
}
