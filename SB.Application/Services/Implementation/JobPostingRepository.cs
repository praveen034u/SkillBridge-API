using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Options;
using SB.Application.Services.Interface;
using SB.Domain.Model;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Options;

using SB.Domain.Model;
using SB.Domain;


namespace SB.Application.Services.Implementation
{
    public class JobPostingRepository : IJobPostingRepository
    {
        private readonly Container _container;

        public JobPostingRepository(IOptions<CosmosDb> settings, CosmosClient cosmosClient)
        {
            _container = cosmosClient.GetContainer("SB_database","SB_Container");//, "JobPostings");
           // _container = await database.Database.CreateContainerIfNotExistsAsync("JobPostings", "/categoryId");
        }

        public async Task<JobPosting> CreateJobPostingAsync(JobPosting job)
        {
           // await _container.CreateItemAsync(job, new PartitionKey(job.Id));
            await _container.CreateItemAsync(job, new PartitionKey(job.CategoryId.ToString()));
            return job;
        }

        public async Task<JobPosting> GetJobPostingByIdAsync(string jobId)
        {
            try
            {
                ItemResponse<JobPosting> response = await _container.ReadItemAsync<JobPosting>(jobId, new PartitionKey(jobId));
                return response.Resource;
            }
            catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return null;
            }
        }

        public async Task<List<JobPosting>> GetAllJobPostingsAsync()
        {
            var query = _container.GetItemQueryIterator<JobPosting>("SELECT * FROM c");
            List<JobPosting> results = new List<JobPosting>();
            while (query.HasMoreResults)
            {
                foreach (var item in await query.ReadNextAsync())
                {
                    results.Add(item);
                }
            }
            return results;
        }

        public async Task DeleteJobPostingAsync(string jobId)
        {
            await _container.DeleteItemAsync<JobPosting>(jobId, new PartitionKey(jobId));
        }
    }
}

