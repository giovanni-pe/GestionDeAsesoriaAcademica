using System;

namespace CleanArchitecture.Shared.Events.Role;

public sealed class RoleUpdatedEvent : DomainEvent
{
    public string Name { get; set; }
    public string Description { get; set; }

    public RoleUpdatedEvent(Guid RoleId, string name,string description) : base(RoleId)
    {
        Name = name;
        Description = description;

    }
}