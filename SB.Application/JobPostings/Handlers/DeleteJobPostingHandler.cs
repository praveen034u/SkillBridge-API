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
    public class DeleteJobPostingHandler : IRequestHandler<DeleteJobPostingCommand, bool>
    {
        private readonly SupabaseDbContext _context;

        public DeleteJobPostingHandler(SupabaseDbContext context) => _context = context;

        public async Task<bool> Handle(DeleteJobPostingCommand request, CancellationToken cancellationToken)
        {
            var job = await _context.JobPosting.FindAsync(new object[] { request.JobId }, cancellationToken);
            if (job == null) return false;

            _context.JobPosting.Remove(job);
            await _context.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}
