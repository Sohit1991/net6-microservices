using Catalog.API.Entities;
using MongoDB.Driver;

namespace Catalog.API.Data
{
    public class CatalogContextSeed
    {
        public static void SeedData(IMongoCollection<Product> productCollection)
        {
            bool existProduct = productCollection.Find(p => true).Any();
            if (!existProduct)
            {
                productCollection.InsertManyAsync(GetPreConfiguredProducts());
            }
        }

        private static IEnumerable<Product> GetPreConfiguredProducts()
        {
            return new List<Product>()
            {
                new Product()
                {
                    Id = "64450a2d087d282869f4e309",
                    Name = "IPhone X",
                    Category = "Smart Phone",
                    Summary = "This phone is company's biggest change to flagship smartphones",
                    Description = "Iphone x 64 Mega Pixel",
                    ImageFile = "product-1.png",
                    Price = 655.67M
                },
                new Product()
                {
                    Id = "64450a2d087d282869f4e310",
                    Name = "Samsung 10",
                    Category = "Laptop",
                    Summary = "This phone is company's biggest change to flagship smartphones",
                    Description = "Samsung 10 smart phone",
                    ImageFile = "product-2.png",
                    Price = 840.25M
                },
                new Product()
                {
                    Id = "64450a2d087d282869f4e311",
                    Name = "HTC U11+ Plus",
                    Category = "Laptop",
                    Summary = "This phone is company's biggest change to flagship smartphones",
                    Description = "HTC U11 Plus 256 GB",
                    ImageFile = "product-3.png",
                    Price = 640.25M
                },
                new Product()
                {
                    Id = "64450a2d087d282869f4e312",
                    Name = "LG G7 ThinQ",
                    Category = "Home Kitchen",
                    Summary = "This home kitchen item is company's biggest change to flagship smartphones",
                    Description = "LG G7 ThinQ",
                    ImageFile = "product-4.png",
                    Price = 1640.25M
                },
                new Product()
                {
                    Id = "64450a2d087d282869f4e313",
                    Name = "LG Window AC",
                    Category = "Home",
                    Summary = "This home AC item is company's biggest change to flagship smartphones",
                    Description = "LG Window AC",
                    ImageFile = "product-5.png",
                    Price = 6640.25M
                }

            };
        }
    }
}