using System;

namespace CleanArchitecture.Shared.Events.Asesore;

public sealed class AsesoreUpdatedEvent : DomainEvent
{
    public string Nombre { get; set; }
    

    public AsesoreUpdatedEvent(Guid AsesoreId, string nombre) : base(AsesoreId)
    {
        Nombre = nombre;
        
    }
}