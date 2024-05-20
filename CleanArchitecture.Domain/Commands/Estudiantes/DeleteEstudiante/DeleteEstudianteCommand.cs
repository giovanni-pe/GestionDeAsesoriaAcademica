using System;

namespace CleanArchitecture.Domain.Commands.Estudiantes.DeleteEstudiante;

public sealed class DeleteEstudianteCommand : CommandBase
{
    private static readonly DeleteEstudianteCommandValidation s_validation = new();

    public DeleteEstudianteCommand(Guid EstudianteId) : base(EstudianteId)
    {
    }

    public override bool IsValid()
    {
        ValidationResult = s_validation.Validate(this);
        return ValidationResult.IsValid;
    }
}