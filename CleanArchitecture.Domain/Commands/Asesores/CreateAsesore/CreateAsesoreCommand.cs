using System;

namespace CleanArchitecture.Domain.Commands.Asesores.CreateAsesore;

public sealed class CreateAsesoreCommand : CommandBase
{
    private static readonly CreateAsesoreCommandValidation s_validation = new();

    public string Nombre { get; }
    public string Apellido { get; }

    public CreateAsesoreCommand(Guid AsesoreId, string nombre,string apellido) : base(AsesoreId)
    {
        Nombre = nombre;
        Apellido = apellido;
    }

    public override bool IsValid()
    {
        ValidationResult = s_validation.Validate(this);
        return ValidationResult.IsValid;
    }
}