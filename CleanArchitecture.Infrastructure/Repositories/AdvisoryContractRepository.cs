using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Interfaces.Repositories;
using CleanArchitecture.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CleanArchitecture.Infrastructure.Repositories;

public sealed class AdvisoryContractRepository : BaseRepository<AdvisoryContract>, IAdvisoryContractRepository
{
    private readonly ApplicationDbContext _context;

    public AdvisoryContractRepository(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<IEnumerable<AdvisoryContract>> GetByResearchLineIdAsync(Guid researchLineId)
    {
        return await _context.AdvisoryContracts
                             .Where(ac => ac.ResearchLineId == researchLineId)
                             .ToListAsync();
    }
}

