using Azure.Search.Documents.Models;
using Azure.Search.Documents;
using Azure;
using Microsoft.Extensions.Options;
using SB.Domain.Model;
using SB.Infrastructure.Repositories.Interfaces;
using System;
using Azure;
using Azure.Search.Documents;
using Azure.Search.Documents.Models;
using Microsoft.Extensions.Options;
using SB.Domain.Model;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SB.Domain;

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

        var response = await _searchClient.SearchAsync<JobPosting>(query, options);

        return response.Value.GetResults().Select(r => r.Document).ToList();
    }
}

