using System;

namespace CleanArchitecture.Domain.Commands.Asesores.UpdateAsesore;

public sealed class UpdateAsesoreCommand : CommandBase
{
    private static readonly UpdateAsesoreCommandValidation s_validation = new();

    public string Nombre { get; }
    

    public UpdateAsesoreCommand(Guid AsesoreId, string nombre) : base(AsesoreId)
    {
        Nombre = nombre;
        
    }

    public override bool IsValid()
    {
        ValidationResult = s_validation.Validate(this);
        return ValidationResult.IsValid;
    }
}