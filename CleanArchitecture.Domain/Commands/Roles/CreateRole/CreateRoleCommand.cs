using System;

namespace CleanArchitecture.Domain.Commands.Roles.CreateRole;

public sealed class CreateRoleCommand : CommandBase
{
    private static readonly CreateRoleCommandValidation s_validation = new();

    public string Name { get; }
    public string Description { get; }

    public CreateRoleCommand(Guid RoleId, string name,string description) : base(RoleId)
    {
        Name = name;
        Description= description;
    }

    public override bool IsValid()
    {
        ValidationResult = s_validation.Validate(this);
        return ValidationResult.IsValid;
    }
}