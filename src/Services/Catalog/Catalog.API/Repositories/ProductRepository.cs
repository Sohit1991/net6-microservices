using Catalog.API.Data;
using Catalog.API.Entities;
using MongoDB.Driver;
using System.Formats.Asn1;
using System.Xml.Linq;

namespace Catalog.API.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ICatalogContext _catalogContext;

        public ProductRepository(ICatalogContext catalogContext)
        {
            this._catalogContext = catalogContext ?? throw new ArgumentNullException(nameof(catalogContext));
        }

        public async Task<IEnumerable<Product>> GetProductsAsync()
        {
            return await _catalogContext.Products.
                Find(p => true)
                .ToListAsync();
        }

        public async Task<Product> GetProductAsync(string id)
        {
            return await _catalogContext.Products.
                Find(p => true)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Product>> GetProductByNameAsync(string name)
        {
            FilterDefinition<Product> filter = Builders<Product>.Filter.Eq(p => p.Name, name);
            return await _catalogContext
                          .Products
                          .Find(filter)
                          .ToListAsync();
        }
        public async Task<IEnumerable<Product>> GetProductByCategoryAsync(string categoryName)
        {
            FilterDefinition<Product> filter = Builders<Product>.Filter.Eq(p => p.Name, categoryName);
            return await _catalogContext
                          .Products
                          .Find(filter)
                          .ToListAsync();
        }


        public async Task CreateProduct(Product product)
        {
            await _catalogContext.Products.InsertOneAsync(product);

        }
        public async Task<bool> UpdateProduct(Product product)
        {
            var updateResult =
                            await _catalogContext.Products
                                    .ReplaceOneAsync(filter: g => g.Id == product.Id, replacement: product);
            return updateResult.IsAcknowledged
                        && updateResult.ModifiedCount > 0;

        }
        public async Task<bool> DeleteProduct(string id)
        {
            FilterDefinition<Product> filter = Builders<Product>.Filter.Eq(p => p.Id, id);
            var deleteResult =
                               await _catalogContext.Products
                                       .DeleteOneAsync(filter);
            return deleteResult.IsAcknowledged
                        && deleteResult.DeletedCount > 0;
        }


    }
}
