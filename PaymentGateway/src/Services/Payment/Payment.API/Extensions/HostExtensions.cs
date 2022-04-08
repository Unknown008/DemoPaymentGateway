using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace PaymentNs.API.Extensions
{
    public static class HostExtensions
    {
        public static IHost MigrateDatabase<TContext>(
            this IHost host,
            Action<TContext, IServiceProvider> seeder,
            int? retry = 0
        ) where TContext : DbContext
        {
            int retryForAvailability = retry.Value;

            using (var scope = host.Services.CreateScope())
            {
                IServiceProvider services = scope.ServiceProvider;
                ILogger<TContext> logger = services.GetRequiredService<ILogger<TContext>>();
                TContext context = services.GetService<TContext>();

                try
                {
                    logger.LogInformation("Migrating database associated with context {DbContextName}", typeof(TContext).Name);

                    InvokeSeeder(seeder, context, services);

                    logger.LogInformation("Migrated database associated with context {DbContextName}", typeof(TContext).Name);
                }
                catch (SqlException ex)
                {
                    logger.LogError(ex, "An error occurred while migrating the database used on context {DbContextName}", typeof(TContext).Name);

                    // Retry in case db is not ready
                    if (retryForAvailability < 10)
                    {
                        retryForAvailability++;
                        Thread.Sleep(2000);
                        MigrateDatabase(host, seeder, retryForAvailability);
                    }
                }
            }
            return host;
        }

        private static void InvokeSeeder<TContext>(
            Action<TContext, IServiceProvider> seeder,
            TContext context,
            IServiceProvider services
        ) where TContext : DbContext
        {
            context.Database.Migrate();
            seeder(context, services);
        }
    }
}
