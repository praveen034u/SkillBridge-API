using MediatR;
using SB.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SB.Application.JobPostings.Queries
{
    public record GetJobPostingByIdQuery(Guid JobId) : IRequest<JobPosting>;
}
