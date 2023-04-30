using Dapper;
using Discount.Grpc.Entities;
using Npgsql;

namespace Discount.Grpc.Repositories
{
    public class DiscountRepository : IDiscountRepository
    {
        private readonly IConfiguration _configuration;

        public DiscountRepository(IConfiguration configuration)
        {
            this._configuration = configuration;

        }

        public async Task<Coupon> GetDiscount(string productName)
        {
            using var connection = new NpgsqlConnection
                    (_configuration.GetValue<string>("DatabaseSettings:ConnectionString"));

            var coupon = await connection.QueryFirstOrDefaultAsync<Coupon>
                ("Select * from coupon Where ProductName=@ProductName",
                    new { ProductName = productName });

            if (coupon == null)
            {
                return new Coupon()
                {
                    Amount = 0,
                    Description = "No Discount Desc",
                    ProductName = "No Discount",
                };
            }
            return coupon;
        }

        public async Task<bool> CreateDiscount(Coupon coupon)
        {
            using var connection = new NpgsqlConnection
                    (_configuration.GetValue<string>("DatabaseSettings:ConnectionString"));

            var affected = await connection.ExecuteAsync(
                "INSERT INTO Coupon (ProductName,description,amount) Values(@ProductName,@description,@amount)",
                new
                {
                    ProductName = coupon.ProductName,
                    amount = coupon.Amount,
                    description = coupon.Description
                });
            if (affected == 0)
                return false;
            return true;
        }


        public async Task<bool> UpdateDiscount(Coupon coupon)
        {
            using var connection = new NpgsqlConnection
                    (_configuration.GetValue<string>("DatabaseSettings:ConnectionString"));

            var affected = await connection.ExecuteAsync(
                "UPDATE Coupon SET ProductName=@ProductName,description=@description,amount=@amount Where Id=@id",
                new
                {
                    ProductName = coupon.ProductName,
                    amount = coupon.Amount,
                    description = coupon.Description,
                    Id = coupon.Id
                });
            if (affected == 0)
                return false;
            return true;
        }


        public async Task<bool> DeleteDiscount(string productName)
        {
            using var connection = new NpgsqlConnection
                    (_configuration.GetValue<string>("DatabaseSettings:ConnectionString"));

            var affected = await connection.ExecuteAsync(
                "DELETE FROM Coupon WHERE ProductName=@ProductName",
                new
                {
                    ProductName = productName

                });
            if (affected == 0)
                return false;
            return true;
        }


    }
}
