using CleanArchitecture.Domain.Constants;
using CleanArchitecture.Domain.Errors;
using FluentValidation;

namespace CleanArchitecture.Domain.Commands.Roles.UpdateRole;

public sealed class UpdateRoleCommandValidation : AbstractValidator<UpdateRoleCommand>
{
    public UpdateRoleCommandValidation()
    {
        AddRuleForId();
        AddRuleForName();
    }

    private void AddRuleForId()
    {
        RuleFor(cmd => cmd.AggregateId)
            .NotEmpty()
            .WithErrorCode(DomainErrorCodes.Role.EmptyId)
            .WithMessage("Role id may not be empty");
    }

    private void AddRuleForName()
    {
        RuleFor(cmd => cmd.Name)
            .NotEmpty()
            .WithErrorCode(DomainErrorCodes.Role.EmptyName)
            .WithMessage("Name may not be empty")
            .MaximumLength(MaxLengths.Role.Name)
            .WithErrorCode(DomainErrorCodes.Role.NameExceedsMaxLength)
            .WithMessage($"Name may not be longer than {MaxLengths.Role.Name} characters");
    }
}