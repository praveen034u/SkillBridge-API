using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SB.Application.JobPostings.Commands
{
    public record DeleteJobPostingCommand(Guid JobId) : IRequest<bool>;
}
