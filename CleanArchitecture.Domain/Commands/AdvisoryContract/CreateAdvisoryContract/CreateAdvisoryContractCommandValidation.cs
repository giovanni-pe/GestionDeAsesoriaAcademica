using CleanArchitecture.Domain.Constants;
using CleanArchitecture.Domain.Errors;
using FluentValidation;

namespace CleanArchitecture.Domain.Commands.AdvisoryContracts.CreateAdvisoryContract;

public sealed class CreateAdvisoryContractCommandValidation : AbstractValidator<CreateAdvisoryContractCommand>
{
    public CreateAdvisoryContractCommandValidation()
    {
        AddRuleForId();
        AddRuleForCode();
    }

    private void AddRuleForId()
    {
        RuleFor(cmd => cmd.AggregateId)
            .NotEmpty()
            .WithErrorCode(DomainErrorCodes.AdvisoryContract.EmptyId)
            .WithMessage("AdvisoryContract id may not be empty");
    }

    private void AddRuleForCode()
    {
        RuleFor(cmd => cmd.Status)
            .NotEmpty()
            .WithErrorCode(DomainErrorCodes.AdvisoryContract.EmptyCode)
            .WithMessage("Code may not be empty")
            .WithErrorCode(DomainErrorCodes.AdvisoryContract.CodeExceedsMaxLength)
            .WithMessage($"Code may not be longer than {MaxLengths.AdvisoryContract.Status} characters");
    }
}