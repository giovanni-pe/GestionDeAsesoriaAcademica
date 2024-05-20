using CleanArchitecture.Domain.Constants;
using CleanArchitecture.Domain.Errors;
using FluentValidation;

namespace CleanArchitecture.Domain.Commands.Estudiantes.CreateEstudiante;

public sealed class CreateEstudianteCommandValidation : AbstractValidator<CreateEstudianteCommand>
{
    public CreateEstudianteCommandValidation()
    {
        AddRuleForId();
        AddRuleForFirstName();
    }

    private void AddRuleForId()
    {
        RuleFor(cmd => cmd.AggregateId)
            .NotEmpty()
            .WithErrorCode(DomainErrorCodes.Estudiante.EmptyId)
            .WithMessage("estudiante id may not be empty");
    }

   
    private void AddRuleForFirstName()
    {
        RuleFor(cmd => cmd.FirstName)
            .NotEmpty()
            .WithErrorCode(DomainErrorCodes.Estudiante.EmptyFirstName)
            .WithMessage("Name may not be empty")
            .MaximumLength(MaxLengths.Estudiante.FirstName)
            .WithErrorCode(DomainErrorCodes.Estudiante.FirstNameExceedsMaxLength)
            .WithMessage($"Name may not be longer than {MaxLengths.Estudiante.FirstName} characters");
    }
}