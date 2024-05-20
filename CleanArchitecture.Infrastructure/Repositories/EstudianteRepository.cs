using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Interfaces.Repositories;
using CleanArchitecture.Infrastructure.Database;

namespace CleanArchitecture.Infrastructure.Repositories;

public sealed class EstudianteRepository : BaseRepository<Estudiante>, IEstudianteRepository
{
    public EstudianteRepository(ApplicationDbContext context) : base(context)
    {
    }
}