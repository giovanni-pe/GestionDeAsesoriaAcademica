using CleanArchitecture.Domain.Errors;
using FluentValidation;

namespace CleanArchitecture.Domain.Commands.ResearchGroups.DeleteResearchGroup;

public sealed class DeleteAppointmentCommandValidation : AbstractValidator<DeleteResearchGroupCommand>
{
    public DeleteAppointmentCommandValidation()
    {
        AddRuleForId();
    }

    private void AddRuleForId()
    {
        RuleFor(cmd => cmd.AggregateId)
            .NotEmpty()
            .WithErrorCode(DomainErrorCodes.ResearchGroup.EmptyId)
            .WithMessage("ResearchGroup id may not be empty");
    }
}