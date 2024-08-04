using System.Threading.Tasks;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Interfaces.Repositories;
using CleanArchitecture.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Infrastructure.Repositories
{
    public sealed class CalendarTokenRepository : BaseRepository<CalendarToken>, ICalendarTokenRepository
    {
        private readonly DbContext _context;

        public CalendarTokenRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
      
        public async Task<CalendarToken> GetTokenByUserEmailAsync(string userEmail)
        {
            return await _context.Set<CalendarToken>()
                                 .FirstOrDefaultAsync(t => t.UserEmail == userEmail);
        }

        public async Task AddOrUpdateTokenAsync(CalendarToken token)
        {
            var existingToken = await GetTokenByUserEmailAsync(token.UserEmail);
            if (existingToken == null)
            {
                await _context.Set<CalendarToken>().AddAsync(token);
            }
            else
            {
                _context.Entry(existingToken).CurrentValues.SetValues(token);
            }
            await _context.SaveChangesAsync();
        }
    }
}
