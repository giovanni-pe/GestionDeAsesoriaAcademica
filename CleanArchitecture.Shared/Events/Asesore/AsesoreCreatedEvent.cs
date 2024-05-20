using System;

namespace CleanArchitecture.Shared.Events.Asesore;

public sealed class AsesoreCreatedEvent : DomainEvent
{
    public string Nombre { get; set; }
    

    public AsesoreCreatedEvent(Guid AsesoreId, string nombre) : base(AsesoreId)
    {
        Nombre = nombre;
        
    }
}