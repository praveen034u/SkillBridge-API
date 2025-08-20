using MediatR;
using SB.Application.JobPostings.Commands;
using SB.Infrastructure;
using SB.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SB.Application.JobPostings.Handlers
{
    public class UpdateJobPostingHandler : IRequestHandler<UpdateJobPostingCommand, bool>
    {
        private readonly SupabaseDbContext _context;

        public UpdateJobPostingHandler(SupabaseDbContext context) => _context = context;

        public async Task<bool> Handle(UpdateJobPostingCommand request, CancellationToken cancellationToken)
        {
            var job = await _context.JobPosting.FindAsync(new object[] { request.JobId }, cancellationToken);
            if (job == null) return false;

            job.Title = request.Title;
            job.Description = request.Description;
            job.RequiredSkills = request.RequiredSkills;
            job.Location = request.Location;
            job.EmploymentType = request.EmploymentType;
            job.SalaryRange = request.SalaryRange;
            job.ExperienceRequired = request.ExperienceRequired;
            job.ApplicationDeadline = request.ApplicationDeadline;
            job.Status = request.Status;
            job.UpdatedAt = DateTime.UtcNow;
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}
