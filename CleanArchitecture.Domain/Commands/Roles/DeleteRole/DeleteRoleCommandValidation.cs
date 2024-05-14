using CleanArchitecture.Domain.Errors;
using FluentValidation;

namespace CleanArchitecture.Domain.Commands.Roles.DeleteRole;

public sealed class DeleteRoleCommandValidation : AbstractValidator<DeleteRoleCommand>
{
    public DeleteRoleCommandValidation()
    {
        AddRuleForId();
    }

    private void AddRuleForId()
    {
        RuleFor(cmd => cmd.AggregateId)
            .NotEmpty()
            .WithErrorCode(DomainErrorCodes.Role.EmptyId)
            .WithMessage("Role id may not be empty");
    }
}