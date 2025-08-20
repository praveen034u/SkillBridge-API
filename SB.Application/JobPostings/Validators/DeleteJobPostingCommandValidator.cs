using FluentValidation;
using SB.Application.JobPostings.Commands;

namespace SB.Application.JobPostings.Validators
{
    public class DeleteJobPostingCommandValidator : AbstractValidator<DeleteJobPostingCommand>
    {
        public DeleteJobPostingCommandValidator()
        {
            RuleFor(x => x.JobId).NotEmpty();
        }
    }
}
