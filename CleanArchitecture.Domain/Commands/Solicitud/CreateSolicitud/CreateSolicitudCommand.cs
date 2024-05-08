using System;

namespace CleanArchitecture.Domain.Commands.Solicituds.CreateSolicitud;

public sealed class CreateSolicitudCommand : CommandBase
{
    private static readonly CreateSolicitudCommandValidation s_validation = new();

    public string Name { get; }

    public CreateSolicitudCommand(Guid SolicitudId, string name) : base(SolicitudId)
    {
        Name = name;
    }

    public override bool IsValid()
    {
        ValidationResult = s_validation.Validate(this);
        return ValidationResult.IsValid;
    }
}