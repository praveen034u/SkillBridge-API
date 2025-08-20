using SB.Domain.Model;

namespace SB.Application.Services.Interface
{
    public interface IJobPostingRepository
    {
        Task<JobPosting> CreateJobPostingAsync(JobPosting job);
        Task<JobPosting> GetJobPostingByIdAsync(string jobId);
        Task<List<JobPosting>> GetAllJobPostingsAsync();
        Task<bool> DeleteJobPostingAsync(string jobId);
        Task<bool> UpdateJobPostingAsync(JobPosting jobPosting);
    }
}

