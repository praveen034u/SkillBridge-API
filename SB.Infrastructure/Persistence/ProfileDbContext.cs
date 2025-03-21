
using Microsoft.Azure.Cosmos;
using SB.Domain.Entities;


namespace SB.Infrastructure.Persistence;

public class ProfileDbContext
{
   

    private readonly CosmosClient _client;
    private readonly Container _profileContainer;

    public ProfileDbContext(CosmosClient client, string databaseName)
    {
        _client = client;
        _profileContainer = _client.GetContainer(databaseName, "SB_Container"); // Hardcoding container if it's fixed
    }

    //public ProfileDbContext(string connectionString, string databaseName)
    //{
    //    _client = new CosmosClient(connectionString);
    //    _profileContainer = _client.GetContainer(databaseName, "Profiles");
    //}

    public async Task AddProfileAsync(Profile profile)
    {
        await _profileContainer.CreateItemAsync(profile, new PartitionKey(profile.Id));
    }
}

