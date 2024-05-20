using CleanArchitecture.Domain.Errors;
using FluentValidation;

namespace CleanArchitecture.Domain.Commands.Estudiantes.DeleteEstudiante;

public sealed class DeleteEstudianteCommandValidation : AbstractValidator<DeleteEstudianteCommand>
{
    public DeleteEstudianteCommandValidation()
    {
        AddRuleForId();
    }

    private void AddRuleForId()
    {
        RuleFor(cmd => cmd.AggregateId)
            .NotEmpty()
            .WithErrorCode(DomainErrorCodes.Estudiante.EmptyId)
            .WithMessage("Estudiante id may not be empty");
    }
}