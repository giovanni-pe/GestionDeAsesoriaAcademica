using System;

namespace CleanArchitecture.Domain.Commands.Asesores.DeleteAsesore;

public sealed class DeleteAsesoreCommand : CommandBase
{
    private static readonly DeleteAsesoreCommandValidation s_validation = new();

    public DeleteAsesoreCommand(Guid AsesoreId) : base(AsesoreId)
    {
    }

    public override bool IsValid()
    {
        ValidationResult = s_validation.Validate(this);
        return ValidationResult.IsValid;
    }
}