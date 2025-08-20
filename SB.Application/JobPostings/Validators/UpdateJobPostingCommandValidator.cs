using FluentValidation;
using SB.Application.JobPostings.Commands;

namespace SB.Application.JobPostings.Validators
{

    public class UpdateJobPostingCommandValidator : AbstractValidator<UpdateJobPostingCommand>
    {
        public UpdateJobPostingCommandValidator()
        {
            RuleFor(x => x.JobId).NotEmpty();
            RuleFor(x => x.Title).NotEmpty().MaximumLength(255);
            RuleFor(x => x.Description).NotEmpty();
        }
    }
}
