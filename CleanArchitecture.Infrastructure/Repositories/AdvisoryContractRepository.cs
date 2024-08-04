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
    public async Task AcceptAdvisoryAsync(Guid id, string acceptanceMessage)
    {
        var advisoryContract = await _context.AdvisoryContracts.FindAsync(id);
        if (advisoryContract == null)
            throw new KeyNotFoundException("Advisory Contract not found");

        advisoryContract.SetMessage(acceptanceMessage);
        advisoryContract.SetStatus(1); // Assuming 1 means accepted
        _context.AdvisoryContracts.Update(advisoryContract);
       await _context.SaveChangesAsync();
    }

}

