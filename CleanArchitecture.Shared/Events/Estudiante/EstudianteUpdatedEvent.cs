using System;

namespace CleanArchitecture.Shared.Events.Estudiante;

public sealed class EstudianteUpdatedEvent : DomainEvent
{
    public string FirstName { get; set; }
    

    public EstudianteUpdatedEvent(Guid EstudianteId, string firstName) : base(EstudianteId)
    {
        FirstName = firstName;
        
    }
}