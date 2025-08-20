using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SB.Application.JobPostings.Commands;
using SB.Application.Services.Interface;
using SB.Domain.Model;
using SB.Infrastructure;
using SB.Infrastructure.Persistence;

namespace SB.Application.JobPostings.Handlers
{

    public class CreateJobPostingHandler : IRequestHandler<CreateJobPostingCommand, Guid> // IRequestHandler<CreateJobPostingCommand, JobPosting>
    {
        private readonly IJobPostingRepository _repository;
        private readonly SupabaseDbContext _dbContext;

        public CreateJobPostingHandler(SupabaseDbContext dbContext)
        {
            //_repository = repository;
            _dbContext = dbContext;
        }

        public async Task<Guid> Handle(CreateJobPostingCommand request, CancellationToken cancellationToken)
        {
            var jobPosting = new JobPosting
            {
                JobId = Guid.NewGuid(),
                EmployerId = request.EmployerId,
                Title = request.Title,
                Description = request.Description,
                RequiredSkills = request.RequiredSkills,
                Location = request.Location,
                EmploymentType = request.EmploymentType,
                SalaryRange = request.SalaryRange,
                ExperienceRequired = request.ExperienceRequired,
                ApplicationDeadline = request.ApplicationDeadline,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            _dbContext.JobPosting.Add(jobPosting);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return jobPosting.JobId;
        }


        //public async Task<JobPosting> Handle(CreateJobPostingCommand request, CancellationToken cancellationToken)
        //{
        //    var jobPosting = new JobPosting
        //    {
        //        EmployerId = request.EmployerId,
        //        Title = request.Title,
        //        Description = request.Description,
        //        Skills = request.RequiredSkills.First(),
        //        Location = request.Location,
        //        JobType = request.JobType,
        //        Salary = request.Salary
        //    };

        //    return await _repository.CreateJobPostingAsync(jobPosting);
        //}
    }

}
