using Microsoft.Azure.Cosmos;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using SB.Application.Services.Interface;
using SB.Domain;
using SB.Domain.Model;
using SB.Infrastructure;


namespace SB.Application.Services.Implementation
{
    public class JobPostingRepository : IJobPostingRepository
    {
        private readonly SupabaseDbContext _context;

        public JobPostingRepository(SupabaseDbContext context)
        {
            _context = context;
        }

        public async Task<JobPosting> CreateJobPostingAsync(JobPosting job)
        {
            _context.JobPosting.Add(job);
            await _context.SaveChangesAsync();
            return job;
        }

        public async Task<JobPosting?> GetJobPostingByIdAsync(string jobId)
        {
            if (!Guid.TryParse(jobId, out var guidId))
                return null;
            return await _context.JobPosting.FirstOrDefaultAsync(j => j.JobId == guidId);
        }

        public async Task<List<JobPosting>> GetAllJobPostingsAsync()
        {
            return await _context.JobPosting.ToListAsync();
        }

        public async Task<bool> DeleteJobPostingAsync(string jobId)
        {
            if (!Guid.TryParse(jobId, out var guidId))
                return false;
            var job = await _context.JobPosting.FirstOrDefaultAsync(j => j.JobId == guidId);
            if (job == null) return false;

            _context.JobPosting.Remove(job);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateJobPostingAsync(JobPosting jobPosting)
        {
            if (!_context.JobPosting.Any(j => j.JobId == jobPosting.JobId))
                return false;

            _context.Entry(jobPosting).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return true;
        }
    }
}

