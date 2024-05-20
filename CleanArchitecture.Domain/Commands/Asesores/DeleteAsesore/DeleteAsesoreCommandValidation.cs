using CleanArchitecture.Domain.Errors;
using FluentValidation;

namespace CleanArchitecture.Domain.Commands.Asesores.DeleteAsesore;

public sealed class DeleteAsesoreCommandValidation : AbstractValidator<DeleteAsesoreCommand>
{
    public DeleteAsesoreCommandValidation()
    {
        AddRuleForId();
    }

    private void AddRuleForId()
    {
        RuleFor(cmd => cmd.AggregateId)
            .NotEmpty()
            .WithErrorCode(DomainErrorCodes.Asesore.EmptyId)
            .WithMessage("Asesore id may not be empty");
    }
}