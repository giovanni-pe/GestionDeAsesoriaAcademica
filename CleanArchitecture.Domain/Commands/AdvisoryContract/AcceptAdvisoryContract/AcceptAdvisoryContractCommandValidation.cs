using FluentValidation;

namespace CleanArchitecture.Domain.Commands.AdvisoryContracts.AcceptAdvisoryContract
{
    public sealed class AcceptAdvisoryContractCommandValidation : AbstractValidator<AcceptAdvisoryContractCommand>
    {
        public AcceptAdvisoryContractCommandValidation()
        {
            RuleFor(cmd => cmd.AdvisoryContractId)
                .NotEmpty()
                .WithMessage("AdvisoryContract id may not be empty");

            RuleFor(cmd => cmd.AcceptanceMessage)
                .NotEmpty()
                .WithMessage("Acceptance message may not be empty")
                .MaximumLength(250)
                .WithMessage("Acceptance message may not be longer than 250 characters");
        }
    }
}
