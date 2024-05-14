using System;

namespace CleanArchitecture.Domain.Commands.Roles.DeleteRole;

public sealed class DeleteRoleCommand : CommandBase
{
    private static readonly DeleteRoleCommandValidation s_validation = new();

    public DeleteRoleCommand(Guid RoleId) : base(RoleId)
    {
    }

    public override bool IsValid()
    {
        ValidationResult = s_validation.Validate(this);
        return ValidationResult.IsValid;
    }
}