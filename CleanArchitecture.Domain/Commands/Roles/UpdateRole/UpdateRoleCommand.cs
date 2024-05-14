using System;

namespace CleanArchitecture.Domain.Commands.Roles.UpdateRole;

public sealed class UpdateRoleCommand : CommandBase
{
    private static readonly UpdateRoleCommandValidation s_validation = new();

    public string Name { get; }

    public UpdateRoleCommand(Guid RoleId, string name) : base(RoleId)
    {
        Name = name;
    }

    public override bool IsValid()
    {
        ValidationResult = s_validation.Validate(this);
        return ValidationResult.IsValid;
    }
}