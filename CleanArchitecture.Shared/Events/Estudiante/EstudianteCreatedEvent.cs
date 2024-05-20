using System;

namespace CleanArchitecture.Shared.Events.Estudiante;

public sealed class EstudianteCreatedEvent : DomainEvent
{
    public string FirstName { get; set; }
    

    public EstudianteCreatedEvent(Guid EstudianteId, string firstName) : base(EstudianteId)
    {
        FirstName = firstName;
        
    }
}