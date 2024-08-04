using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Interfaces.Repositories;
using CleanArchitecture.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System;
using System.Linq;

namespace CleanArchitecture.Infrastructure.Repositories;

public sealed class StudentRepository : BaseRepository<Student>, IStudentRepository

{
    private readonly ApplicationDbContext _context;
    public StudentRepository(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }
    public async Task<Student?> GetByUserIdAsync(Guid userId)
    {
        return await _context.Students.SingleOrDefaultAsync(s => s.UserId == userId);
    }
   

}