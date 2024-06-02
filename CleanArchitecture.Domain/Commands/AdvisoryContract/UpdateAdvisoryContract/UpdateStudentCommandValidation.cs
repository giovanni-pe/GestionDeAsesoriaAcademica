using CleanArchitecture.Domain.Constants;
using CleanArchitecture.Domain.Errors;
using FluentValidation;

namespace CleanArchitecture.Domain.Commands.AdvisoryContracts.UpdateAdvisoryContract;

public sealed class UpdateAdvisoryContractCommandValidation : AbstractValidator<UpdateAdvisoryContractCommand>
{
    public UpdateAdvisoryContractCommandValidation()
    {
        AddRuleForId();
        AddRuleForName();
    }

    private void AddRuleForId()
    {
        RuleFor(cmd => cmd.AggregateId)
            .NotEmpty()
            .WithErrorCode(DomainErrorCodes.AdvisoryContract.EmptyId)
            .WithMessage("AdvisoryContract id may not be empty");
    }

    private void AddRuleForName()
    {
        RuleFor(cmd => cmd.Status)
            .NotEmpty()
            .WithErrorCode(DomainErrorCodes.AdvisoryContract.EmptyCode)
            .WithMessage("Name may not be empty")
            .MaximumLength(MaxLengths.AdvisoryContract.Status)
            .WithErrorCode(DomainErrorCodes.AdvisoryContract.CodeExceedsMaxLength)
            .WithMessage($"Name may not be longer than {MaxLengths.AdvisoryContract.Status} characters");
    }
}