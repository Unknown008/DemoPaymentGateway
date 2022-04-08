using Microsoft.Extensions.Logging;
using PaymentNs.Domain.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentNs.Infrastructure.Persistence
{
    public class PaymentContextSeed
    {
        public static async Task SeedAsync(PaymentContext paymentContext, ILogger<PaymentContextSeed> logger)
        {
            if (!paymentContext.Payments.Any())
            {
                paymentContext.Payments.AddRange(GetPreconfiguredPayments());
                await paymentContext.SaveChangesAsync();
                logger.LogInformation("Seed database associated with context {DbContextName}", typeof(PaymentContext).Name);
            }
        }

        private static IEnumerable<Payment> GetPreconfiguredPayments()
        {
            return new List<Payment>
            {
                new Payment() {
                    CardNumber = "4111111111111111",
                    ExpiryMonth = 12,
                    ExpiryYear = 2022,
                    Amount = 100,
                    Currency = "MUR",
                    CVV = "123",
                    PaymentStatus = "Complete",
                    BankTransactionId = 1
                }
            };
        }
    }
}