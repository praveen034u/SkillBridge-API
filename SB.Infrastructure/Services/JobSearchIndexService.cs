using Azure.Search.Documents.Indexes.Models;
using Azure.Search.Documents.Indexes;
using Azure;
using Microsoft.Extensions.Options;
using SB.Domain.Model;

using SB.Domain;

namespace SB.Infrastructure.Services;

public class JobSearchIndexService
{
    private readonly SearchIndexClient _indexClient;
    private readonly string _indexName;

    public JobSearchIndexService(IOptions<AzureCognitiveSearch> settings)
    {
        var endpoint = new Uri($"https://{settings.Value.ServiceName}.search.windows.net");
        var credential = new AzureKeyCredential(settings.Value.ApiKey);
        _indexClient = new SearchIndexClient(endpoint, credential);
        _indexName = settings.Value.IndexName;
    }

    public async Task CreateIndexAsync()
    {
        var definition = new SearchIndex(_indexName)
        {
            Fields = new FieldBuilder().Build(typeof(JobSearchModel))
        };

        await _indexClient.CreateOrUpdateIndexAsync(definition);
    }
}

