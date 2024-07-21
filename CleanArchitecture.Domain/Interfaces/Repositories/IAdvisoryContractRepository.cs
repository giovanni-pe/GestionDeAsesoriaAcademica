using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CleanArchitecture.Domain.Entities;

namespace CleanArchitecture.Domain.Interfaces.Repositories
{
    public interface IAdvisoryContractRepository : IRepository<AdvisoryContract>
    {
        Task<IEnumerable<AdvisoryContract>> GetByResearchLineIdAsync(Guid researchLineId);
        Task AcceptAdvisoryAsync(Guid id, string acceptanceMessage);
    }
}
