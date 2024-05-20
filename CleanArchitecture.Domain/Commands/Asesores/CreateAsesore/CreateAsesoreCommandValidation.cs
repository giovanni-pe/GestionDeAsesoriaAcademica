using CleanArchitecture.Domain.Constants;
using CleanArchitecture.Domain.Errors;
using FluentValidation;

namespace CleanArchitecture.Domain.Commands.Asesores.CreateAsesore;

public sealed class CreateAsesoreCommandValidation : AbstractValidator<CreateAsesoreCommand>
{
    public CreateAsesoreCommandValidation()
    {
        AddRuleForId();
        AddRuleForNombre();
    }

    private void AddRuleForId()
    {
        RuleFor(cmd => cmd.AggregateId)
            .NotEmpty()
            .WithErrorCode(DomainErrorCodes.Asesore.EmptyId)
            .WithMessage("Asesore id may not be empty");
    }

   
    private void AddRuleForNombre()
    {
        RuleFor(cmd => cmd.Nombre)
            .NotEmpty()
            .WithErrorCode(DomainErrorCodes.Asesore.EmptyNombre)
            .WithMessage("Name may not be empty")
            .MaximumLength(MaxLengths.Asesore.Nombre)
            .WithErrorCode(DomainErrorCodes.Asesore.NombreExceedsMaxLength)
            .WithMessage($"Name may not be longer than {MaxLengths.Asesore.Nombre} characters");
    }
}