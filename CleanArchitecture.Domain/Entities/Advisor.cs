using System;
using System.Collections.Generic;

namespace CleanArchitecture.Domain.Entities;

public class Advisor : Entity
{
    public Guid UserId { get; private set; }
    public virtual User User { get; private set; } = null!;
    public Guid ResearchGroupId { get; private set; }
    public virtual ResearchGroup ResearchGroup { get; private set; } = null!;
   // public virtual ICollection<AdvisoryContract> AdvisoryContracts { get; private set; } = new List<AdvisoryContract>();
   // public virtual ICollection<Appointment> Appointments { get; private set; } = new List<Appointment>();

    public Advisor(Guid id, Guid userId, Guid researchGroupId) : base(id)
    {
        UserId = userId;
        ResearchGroupId = researchGroupId;
    }

    public void SetUserId(Guid userId)
    {
        UserId = userId;
    }

    public void SetResearchGroupId(Guid researchGroupId)
    {
        ResearchGroupId = researchGroupId;
    }
}
