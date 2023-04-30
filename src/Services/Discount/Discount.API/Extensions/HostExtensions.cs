using Npgsql;

namespace Discount.API.Extensions
{
    public static class HostExtensions
    {
        public static IHost MigrateDatabase<TContext>(this IHost host, int? retry = 0)
        {
            int retryForAvilablity = retry.Value;
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var configuration = services.GetRequiredService<IConfiguration>();
                var logger = services.GetRequiredService<ILogger<TContext>>();
                try
                {
                    logger.LogInformation("Starting MIgrated postresql database");
                    using var connection = new NpgsqlConnection
                    (configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
                    logger.LogInformation($"connection String: {connection.ConnectionString}");
                    connection.Open();
                    using var command = new NpgsqlCommand
                    {
                        Connection = connection
                    };

                    command.CommandText = "DROP TABLE IF EXISTS Coupon";
                    command.ExecuteNonQuery();
                    command.CommandText = @"CREATE TABLE Coupon (ID SERIAL PRIMARY KEY NOT NULL,
                                            ProductName varchar(24) NOT NULL,Description TEXT,Amount INT)";
                    command.ExecuteNonQuery();
                    command.CommandText = "INSERT INTO Coupon (ProductName,description,amount) Values('IPhone X','IPhone Discount',150)";
                    command.ExecuteNonQuery();
                    command.CommandText = "INSERT INTO Coupon (ProductName,description,amount) Values('Samsung 10','Samsung Discount',100)";
                    command.ExecuteNonQuery();
                    logger.LogInformation("MIgrated postresql database");
                }
                catch (Exception)
                {
                    logger.LogError("Error during MIgration");

                    if (retryForAvilablity < 5)
                    {
                        retryForAvilablity++;
                        Thread.Sleep(2000);
                        MigrateDatabase<TContext>(host, retryForAvilablity);
                    }
                }
            }
            return host;
        }
    }
}
