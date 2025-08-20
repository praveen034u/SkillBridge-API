using FluentValidation;
using SB.Application.JobPostings.Commands;

namespace SB.Application.JobPostings.Validators
{
    public class CreateJobPostingCommandValidator : AbstractValidator<CreateJobPostingCommand>
    {
        public CreateJobPostingCommandValidator()
        {
            RuleFor(x => x.Title).NotEmpty().MaximumLength(255);
            RuleFor(x => x.Description).NotEmpty();
        }
    }
}
