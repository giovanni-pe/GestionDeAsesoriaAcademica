using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Interfaces.Repositories;
using CleanArchitecture.Infrastructure.Database;

namespace CleanArchitecture.Infrastructure.Repositories;

public sealed class AsesoreRepository : BaseRepository<Asesore>, IAsesoreRepository
{
    public AsesoreRepository(ApplicationDbContext context) : base(context)
    {
    }
}