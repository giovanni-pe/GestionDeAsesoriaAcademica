using CleanArchitecture.Domain.Constants;
using CleanArchitecture.Domain.Errors;
using FluentValidation;

namespace CleanArchitecture.Domain.Commands.Solicituds.CreateSolicitud;

public sealed class CreateSolicitudCommandValidation : AbstractValidator<CreateSolicitudCommand>
{
    public CreateSolicitudCommandValidation()
    {
     
    }

    
}