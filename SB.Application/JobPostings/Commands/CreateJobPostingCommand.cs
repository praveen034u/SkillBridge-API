using MediatR;
using SB.Domain.Model;

namespace SB.Application.JobPostings.Commands
{
    //public class CreateJobPostingCommand : IRequest<JobPosting>
    //{
    //    public string EmployerId { get; set; }
    //    public string Title { get; set; }
    //    public string Description { get; set; }
    //    public List<string> RequiredSkills { get; set; }
    //    public string Location { get; set; }
    //    public string JobType { get; set; }
    //    public decimal Salary { get; set; }
    //}
    public record CreateJobPostingCommand(
        Guid EmployerId,
        string Title,
        string Description,
        List<string> RequiredSkills,
        string Location,
        string EmploymentType,
        SalaryRange SalaryRange,
        int? ExperienceRequired,
        DateTime? ApplicationDeadline
    ) : IRequest<Guid>;
}





