using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Configuration;
using SB.Infrastructure.Repositories.Interfaces;
using Container = Microsoft.Azure.Cosmos.Container;

namespace SB.Infrastructure.Repositories.Implementation
{
    public class UserRepository<T> : IUserRepository<T> where T : class
    {
        private readonly CosmosClient _cosmosClient;
        private readonly Container _container;

        //public UserRepository(CosmosClient cosmosClient, string databaseName, string containerName)
        //{
        //    _cosmosClient = cosmosClient;
        //    _container = _cosmosClient.GetContainer(databaseName, containerName);
        //}

        public UserRepository(CosmosClient cosmosClient, IConfiguration configuration)
        {
            var databaseName = configuration["CosmosDb:DatabaseName"];
            var containerName = configuration["CosmosDb:ContainerName"];
            _container = cosmosClient.GetContainer(databaseName, containerName);
        }


        public async Task<IEnumerable<User>> GetAllAsync()
        {
            var query = _container.GetItemQueryIterator<User>();
            var results = new List<User>();
            while (query.HasMoreResults)
            {
                var response = await query.ReadNextAsync();
                results.AddRange(response);
            }
            return results;
        }

        public async Task<User> GetByIdAsync(string id)
        {
            try
            {
                return await _container.ReadItemAsync<User>(id, new PartitionKey(id));
            }
            catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return null;
            }
        }

        public async Task AddAsync(User user)
        {
            await _container.CreateItemAsync(user, new PartitionKey(user.Id));
        }

        public async Task UpdateAsync(User user)
        {
            await _container.UpsertItemAsync(user, new PartitionKey(user.Id));
        }

        public async Task DeleteAsync(string id)
        {
            await _container.DeleteItemAsync<User>(id, new PartitionKey(id));
        }


        public Task AddAsync(Domain.Entities.User user)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Domain.Entities.User user)
        {
            throw new NotImplementedException();
        }

        Task<IEnumerable<T>> IUserRepository<T>.GetAllAsync()
        {
            throw new NotImplementedException();
        }

        Task<T> IUserRepository<T>.GetByIdAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task AddAsync(T user)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(T user)
        {
            throw new NotImplementedException();
        }
    }

}
