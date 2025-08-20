using MediatR;
using Microsoft.EntityFrameworkCore;
using SB.Application.JobPostings.Queries;
using SB.Domain.Model;
using SB.Infrastructure;
using SB.Infrastructure.Persistence;

namespace SB.Application.JobPostings.Handlers
{
    public class GetJobPostingByIdHandler : IRequestHandler<GetJobPostingByIdQuery, JobPosting?>
    {
        private readonly SupabaseDbContext _context;

        public GetJobPostingByIdHandler(SupabaseDbContext context) => _context = context;

        public async Task<JobPosting?> Handle(GetJobPostingByIdQuery request, CancellationToken cancellationToken)
        {
            return await _context.JobPosting
                                 .AsNoTracking()
                                 .FirstOrDefaultAsync(j => j.JobId == request.JobId, cancellationToken);
        }
    }
}
