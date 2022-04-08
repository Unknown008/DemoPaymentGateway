using PaymentNs.API.Extensions;
using PaymentNs.Infrastructure.Persistence;

namespace PaymentNs.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args)
                .Build()
                // Migrate db in case it is empty when it is run
                .MigrateDatabase<PaymentContext>((context, services) =>
                {
                    ILogger<PaymentContextSeed> logger = services.GetService<ILogger<PaymentContextSeed>>();
                    PaymentContextSeed.SeedAsync(context, logger)
                        .Wait();
                })
                .Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}