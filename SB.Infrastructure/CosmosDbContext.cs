using Microsoft.Azure.Cosmos;
using SB.Domain.Entities;
using User = SB.Domain.Entities.User;

namespace SB.Infrastructure.Persistence;

public class CosmosDbContext
{
    private readonly CosmosClient _client;
    private readonly Container _userContainer;

    public CosmosDbContext(string connectionString, string databaseName)
    {
        _client = new CosmosClient(connectionString);
        _userContainer = _client.GetContainer(databaseName, "SB_Container");
    }

    public async Task AddUserAsync(User user)
    {
        await _userContainer.CreateItemAsync(user, new PartitionKey(user.categoryId.ToString()));
    }

    public async Task<User?> GetUserByEmailAsync(string email)
    {
        var query = new QueryDefinition("SELECT * FROM c WHERE c.email = @Email")
            .WithParameter("@Email", email);
       
        var iterator = _userContainer.GetItemQueryIterator<User>(query);
        if (iterator.HasMoreResults)
        {
            try
            {
                var response = await iterator.ReadNextAsync();

                return response.FirstOrDefault();
            }
            catch (Exception ex)
            {

                throw;
            }
           
        }

        return null;
    }
}



