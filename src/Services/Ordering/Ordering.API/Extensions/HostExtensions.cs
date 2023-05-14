using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Ordering.Infrastructure.Persistence;

namespace Ordering.API.Extensions
{
    public static class HostExtensions
    {
        public static IHost MigrateDatabase<TContext>(this IHost host,
                                                      Action<TContext, IServiceProvider> seeder,
                                                      int? retry = 0) where TContext : DbContext
        {
            int retryForAVilablity = retry.Value;
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var logger = services.GetRequiredService<ILogger<TContext>>();
               
                var context = services.GetService<TContext>();

                try
                {
                    logger.LogInformation($"Migrating database associated with context {typeof(TContext).Name}");

                    InvokeSeeder(seeder, context, services);

                    logger.LogInformation($"Migrated database associated with context {typeof(TContext).Name}");
                }
                catch (SqlException ex)
                {
                    logger.LogError($"An error occuureed while migrating database for context {typeof(TContext)}");
                    if (retryForAVilablity < 4)
                    {
                        retryForAVilablity++;
                        Thread.Sleep(2000);
                        MigrateDatabase<TContext>(host, seeder, retryForAVilablity);
                    }

                }
            }

            return host;
        }

        private static void InvokeSeeder<TContext>(Action<TContext, IServiceProvider> seeder, 
                                                    TContext context,
                                                    IServiceProvider services) where TContext : DbContext
        {
            context.Database.Migrate();
            seeder(context, services);
        }
    }
}
