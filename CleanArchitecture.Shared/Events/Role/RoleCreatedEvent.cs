using System;

namespace CleanArchitecture.Shared.Events.Role;

public sealed class RoleCreatedEvent : DomainEvent
{
    public string Name { get; set; }

    public RoleCreatedEvent(Guid RoleId, string name) : base(RoleId)
    {
        Name = name;
    }
}