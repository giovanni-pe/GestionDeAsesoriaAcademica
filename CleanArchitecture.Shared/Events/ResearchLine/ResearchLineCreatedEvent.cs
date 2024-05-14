using System;

namespace CleanArchitecture.Shared.Events.ResearchLine;

public sealed class ResearchLineCreatedEvent : DomainEvent
{
    public string Name { get; set; }

    public ResearchLineCreatedEvent(Guid ResearchLineId, string name) : base(ResearchLineId)
    {
        Name = name;
    }
}