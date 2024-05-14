using System;

namespace CleanArchitecture.Shared.Events.ResearchLine;

public sealed class ResearchLineUpdatedEvent : DomainEvent
{
    public string Name { get; set; }

    public ResearchLineUpdatedEvent(Guid ResearchLineId, string name) : base(ResearchLineId)
    {
        Name = name;
    }
}