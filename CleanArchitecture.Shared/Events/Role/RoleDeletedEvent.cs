using System;

namespace CleanArchitecture.Shared.Events.Role;

public sealed class RoleDeletedEvent : DomainEvent
{
    public RoleDeletedEvent(Guid RoleId) : base(RoleId)
    {
    }
}