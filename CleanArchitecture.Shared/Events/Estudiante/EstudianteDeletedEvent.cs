using System;

namespace CleanArchitecture.Shared.Events.Estudiante;

public sealed class EstudianteDeletedEvent : DomainEvent
{
    public EstudianteDeletedEvent(Guid EstudianteId) : base(EstudianteId)
    {
    }
}