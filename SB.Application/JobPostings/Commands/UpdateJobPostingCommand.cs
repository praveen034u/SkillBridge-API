using MediatR;
using SB.Domain.Model;
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
        List<string> RequiredSkills,
        string Location,
        string EmploymentType,
        SalaryRange SalaryRange,
        int? ExperienceRequired,
        DateTime? ApplicationDeadline,
        string Status
    ) : IRequest<bool>;
}
