using System;

namespace CleanArchitecture.Shared.Events.Asesore;

public sealed class AsesoreDeletedEvent : DomainEvent
{
    public AsesoreDeletedEvent(Guid AsesoreId) : base(AsesoreId)
    {
    }
}