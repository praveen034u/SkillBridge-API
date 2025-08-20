using MediatR;
using Microsoft.EntityFrameworkCore;
using SB.Application.JobPostings.Queries;
using SB.Domain.Model;
using SB.Infrastructure;
using SB.Infrastructure.Persistence;

namespace SB.Application.JobPostings.Handlers
{
    public class GetJobPostingsHandler : IRequestHandler<GetJobPostingsQuery, IEnumerable<JobPosting>>
    {
        private readonly SupabaseDbContext _context;

        public GetJobPostingsHandler(SupabaseDbContext context) => _context = context;

        public async Task<IEnumerable<JobPosting>> Handle(GetJobPostingsQuery request, CancellationToken cancellationToken)
        {
            return await _context.JobPosting
                                 .AsNoTracking()
                                 .ToListAsync(cancellationToken);
        }
    }
}
