using DataAccess.Model;
using Microsoft.Azure.Cosmos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class Repository : IRepository
    {
        private Container _container;
        public Repository(CosmosClient dbClient, string databaseName, string containerName)
        {
            _container = dbClient.GetContainer(databaseName, containerName);
        }
        public async Task AddProductAsync(Product item)
        {
            await _container.CreateItemAsync<Product>(item, new PartitionKey(item.Id));
        }

        public async Task DeleteProductAsync(string id)
        {
            await _container.DeleteItemAsync<Product>(id, new PartitionKey(id));
        }

        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            var query = _container.GetItemQueryIterator<Product>();
            List<Product> results = new List<Product>();
            while (query.HasMoreResults)
            {
                var response = await query.ReadNextAsync();
                results.AddRange(response.ToList());
            }
            return results;
        }

        public async Task<Product> GetProductByIdAsync(string id)
        {
            try
            {
                ItemResponse<Product> response = await _container.ReadItemAsync<Product>(id, new
                PartitionKey(id));
                return response.Resource;
            }
            catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return null;
            }
        }

        public async Task<IEnumerable<Product>> GetProductsAsync(string queryString)
        {
            var query = _container.GetItemQueryIterator<Product>(new QueryDefinition(queryString));
            List<Product> results = new List<Product>();
            while (query.HasMoreResults)
            {
                var response = await query.ReadNextAsync();
                results.AddRange(response.ToList());
            }
            return results;
        }

        public async Task UpdateProductAsync(string id, Product item)
        {
            await _container.UpsertItemAsync<Product>(item, new PartitionKey(id));
        }
    }
}
