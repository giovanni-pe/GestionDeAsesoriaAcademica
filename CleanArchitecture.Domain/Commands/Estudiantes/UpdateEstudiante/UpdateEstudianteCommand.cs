using System;

namespace CleanArchitecture.Domain.Commands.Estudiantes.UpdateEstudiante;

public sealed class UpdateEstudianteCommand : CommandBase
{
    private static readonly UpdateEstudianteCommandValidation s_validation = new();

    public string FirstName { get; }
    

    public UpdateEstudianteCommand(Guid EstudianteId, string firstName) : base(EstudianteId)
    {
        FirstName = firstName;
        
    }

    public override bool IsValid()
    {
        ValidationResult = s_validation.Validate(this);
        return ValidationResult.IsValid;
    }
}