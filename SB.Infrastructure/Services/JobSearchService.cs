using System;
using Azure;
using Azure.Search.Documents;
using Azure.Search.Documents.Models;
using Microsoft.Extensions.Options;
using SB.Domain.Model;
using System.Collections.Generic;
using System.Threading.Tasks;
using SB.Domain;

namespace SB.Infrastructure.Services
{

    public class JobSearchService
    {
        private readonly SearchClient _searchClient;

        public JobSearchService(IOptions<AzureCognitiveSearch> settings)
        {
           // var endpoint = new Uri($"https://{settings.Value.ServiceName}.search.windows.net");
            var endpoint = new Uri($"https://{settings.Value.ServiceName}.search.windows.net");
            var credential = new AzureKeyCredential(settings.Value.ApiKey);
           // var credential = new AzureKeyCredential(settings.Value.ApiKey);
            _searchClient = new SearchClient(endpoint, settings.Value.IndexName, credential);
        }


        public async Task<List<JobSearchModel>> SearchJobsAsync(string query)
        {
           
            var options = new SearchOptions
            {
                IncludeTotalCount = true
            };

            var response = await _searchClient.SearchAsync<JobSearchModel>(query, options);
            List<JobSearchModel> results = new List<JobSearchModel>();

            await foreach (var result in response.Value.GetResultsAsync())
            {
                results.Add(result.Document);
            }

            return results;
        }

     
    }

}
