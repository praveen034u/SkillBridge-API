using Microsoft.Azure.Cosmos;
using SB.Infrastructure.Repositories.Interfaces;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Container = Microsoft.Azure.Cosmos.Container;

namespace SB.Infrastructure.Repositories.Implementation
{
    public class UserRepository : IUserRepository
    {
        private readonly CosmosClient _cosmosClient;
        private readonly Container _container;

        public UserRepository(CosmosClient cosmosClient, string databaseName, string containerName)
        {
            _cosmosClient = cosmosClient;
            _container = _cosmosClient.GetContainer(databaseName, containerName);
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

        Task<IEnumerable<Domain.Entities.User>> IUserRepository.GetAllAsync()
        {
            throw new NotImplementedException();
        }

        Task<Domain.Entities.User> IUserRepository.GetByIdAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task AddAsync(Domain.Entities.User user)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Domain.Entities.User user)
        {
            throw new NotImplementedException();
        }
    }

}
