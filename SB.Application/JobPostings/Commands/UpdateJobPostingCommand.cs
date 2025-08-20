using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SB.Application.JobPostings.Commands
{
    public record UpdateJobPostingCommand(
        Guid JobId,
        string Title,
        string Description,
        string RequiredSkills,
        string Location,
        string EmploymentType,
        string SalaryRange,
        int? ExperienceRequired,
        DateTime? ApplicationDeadline,
        string Status
    ) : IRequest<bool>;
}
