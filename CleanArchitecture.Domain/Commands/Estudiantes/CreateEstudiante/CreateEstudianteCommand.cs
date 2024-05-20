using System;

namespace CleanArchitecture.Domain.Commands.Estudiantes.CreateEstudiante;

public sealed class CreateEstudianteCommand : CommandBase
{
    private static readonly CreateEstudianteCommandValidation s_validation = new();

    public string FirstName { get; }
    public string LastName { get; }

    public CreateEstudianteCommand(Guid EstudianteId, string firstName,string lastName) : base(EstudianteId)
    {
        FirstName = firstName;
        LastName = lastName;
    }

    public override bool IsValid()
    {
        ValidationResult = s_validation.Validate(this);
        return ValidationResult.IsValid;
    }
}