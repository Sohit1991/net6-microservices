using Shopping.Aggregator.Extensions;
using Shopping.Aggregator.Models;

namespace Shopping.Aggregator.Services
{
    public class CatalogService : ICatalogService
    {
        private readonly HttpClient _client;

        public CatalogService(HttpClient client)
        {
            _client = client;
        }

        public async Task<IEnumerable<CatalogModel>> GetCatalog()
        {
            var respone = await _client.GetAsync("/api/v1/Catalog");
            return await respone.ReadContentAs<List<CatalogModel>>();
        }

        public async Task<CatalogModel> GetCatalog(string id)
        {
            var respone = await _client.GetAsync($"/api/v1/Catalog/{id}");
            return await respone.ReadContentAs<CatalogModel>();
        }

        public async Task<IEnumerable<CatalogModel>> GetCatalogByCategory(string category)
        {
            var respone = await _client.GetAsync($"/api/v1/Catalog/GetProductByCategory/{category}");
            return await respone.ReadContentAs<List<CatalogModel>>();
        }
    }
}
