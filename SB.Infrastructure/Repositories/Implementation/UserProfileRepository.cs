using System.Threading.Tasks;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Options;
using SB.Domain;
using SB.Domain.Model;
using SB.Infrastructure.Persistence;
using SB.Infrastructure.Repositories.Interfaces;

namespace SB.Infrastructure.Repositories.Implementation
{
  
    public class UserProfileRepository : IUserProfileRepository
    {
        private readonly Container _container;

        public UserProfileRepository(IOptions<CosmosDb> settings, CosmosClient cosmosClient)
        {
            var database = cosmosClient.GetDatabase(settings.Value.DatabaseName);
            _container = database.GetContainer(settings.Value.ContainerName);
        }

        public async Task<Domain.Entities.User> GetUserProfileByIdAsync1(string userId)
        {
            //try
            //{
            //    ItemResponse<Domain.Entities.User> response = await _container.ReadItemAsync<SB.Domain.Entities.User>(userId,new PartitionKey("categortId"));
            //    return response.Resource;
            //}
            //catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            //{
            //    return null; // User not found
            //}

            try
            {
                ItemResponse<SB.Domain.Entities.User> response = await _container.ReadItemAsync<SB.Domain.Entities.User>(userId,new PartitionKey("categortId"));
                return response.Resource;
            }
            catch (CosmosException ex)
            {
                Console.WriteLine($"CosmosDB Error: {ex.StatusCode}");
                Console.WriteLine($"ActivityId: {ex.ActivityId}");
                Console.WriteLine($"Diagnostics: {ex.Diagnostics}");
            }
            return null;
        }

        public async Task<Domain.Entities.User> GetUserProfileByIdAsync(string userId)
        {
            var query = new QueryDefinition("SELECT * FROM c WHERE c.id = @id")
                        .WithParameter("@id", userId);

            using FeedIterator<Domain.Entities.User> resultSet = _container.GetItemQueryIterator<Domain.Entities.User>(query);
            try
            {
                if (resultSet.HasMoreResults)
                {
                    FeedResponse<Domain.Entities.User> response = await resultSet.ReadNextAsync();
                    return response.FirstOrDefault();
                } 
            }
            catch (CosmosException ex)
            {
                Console.WriteLine($"Error: {ex.StatusCode}, SubStatus: {ex.SubStatusCode}, Message: {ex.Message}");
            }
           
            return null;
        }
    }

}
