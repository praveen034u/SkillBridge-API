using Azure;
using Azure.Search.Documents;
using Azure.Search.Documents.Models;
using Microsoft.Extensions.Options;
using SB.Domain;
using SB.Domain.Model;
using SB.Infrastructure.Repositories.Interfaces;

namespace SB.Infrastructure.Repositories.Implementation;




public class JobSearchRepository : IJobSearchRepository
{
    private readonly SearchClient _searchClient;

    public JobSearchRepository(IOptions<AzureCognitiveSearch> settings)
    {
        var endpoint = new Uri($"https://{settings.Value.ServiceName}.search.windows.net");
        var credential = new AzureKeyCredential(settings.Value.ApiKey);

        _searchClient = new SearchClient(endpoint, settings.Value.IndexNameJob, credential);
    }

    public async Task<List<JobPosting>> SearchJobsBySkillsAsync(string query)
    {
        var options = new SearchOptions
        {
            IncludeTotalCount = true,
            QueryType = SearchQueryType.Simple,
            SearchFields = { "Skills" },  // 🔹 Searches within the "skills" field
            Select = { "id", "Title", "Description", "Skills" },
            Size = 10  // 🔹 Returns max 10 results
        };
        await RunIndexer();

        var response = await _searchClient.SearchAsync<JobPosting>(query, options);

        return response.Value.GetResults().Select(r => r.Document).ToList();
    }
    public async Task RunIndexer()
    {
        string searchServiceName = "searchskillservice";
        string indexerName = "skillsearchindexer";
        string apiKey = "W2v9pWw62YtaBu3UVnzZeGqjHwbbULhdlagl1My0UpAzSeACjX15";

        string url = $"https://{searchServiceName}.search.windows.net/indexers/{indexerName}/run?api-version=2023-07-01-Preview";

        using HttpClient client = new HttpClient();
        client.DefaultRequestHeaders.Add("api-key", apiKey);

        HttpResponseMessage response = await client.PostAsync(url, null);

        if (response.IsSuccessStatusCode)
        {
            Console.WriteLine("Indexer triggered successfully.");
        }
        else
        {
            string error = await response.Content.ReadAsStringAsync();
            Console.WriteLine($"Error triggering indexer: {error}");
        }
    }
}

