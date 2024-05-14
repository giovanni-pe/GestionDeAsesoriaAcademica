using System;

namespace CleanArchitecture.Shared.Events.ResearchLine;

public sealed class ResearchLineDeletedEvent : DomainEvent
{
    public ResearchLineDeletedEvent(Guid ResearchLineId) : base(ResearchLineId)
    {
    }
}