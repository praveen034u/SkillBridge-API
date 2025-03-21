using Microsoft.Azure.Cosmos;
using SB.Domain.Entities;
using User = SB.Domain.Entities.User;

namespace SB.Infrastructure.Persistence;

public class CosmosDbContext
{

    private readonly CosmosClient _client;
    private readonly Container _userContainer;
    

    public CosmosDbContext(CosmosClient client, string databaseName)
    {
        _client = client;
        _userContainer = _client.GetContainer(databaseName, "SB_Container"); // Hardcoding container if it's fixed
    }
    public Container GetContainer()
    {
        return _userContainer;
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
            catch (CosmosException ex)
            {
                Console.WriteLine($"Error: {ex.StatusCode}, SubStatus: {ex.SubStatusCode}, Message: {ex.Message}");
            }
            catch (Exception ex)
            {

                throw;
            }

        }

        return null;
    }
}





