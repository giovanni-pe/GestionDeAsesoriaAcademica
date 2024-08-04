using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Interfaces.Repositories;
using CleanArchitecture.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Infrastructure.Repositories
{
    public sealed class UserCalendarRepository : BaseRepository<UserCalendar>, IUserCalendarRepository
    {
        private readonly DbContext _context;

        public UserCalendarRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
       
        public async Task<UserCalendar> GetCalendarByIdAsync(Guid calendarId)
        {
            return await _context.Set<UserCalendar>().FindAsync(calendarId);
        }

        public async Task<IEnumerable<UserCalendar>> GetCalendarsByUserIdAsync(Guid userId)
        {
            return await _context.Set<UserCalendar>()
                                 .Where(uc => uc.UserId == userId)
                                 .ToListAsync();
        }
       
        public async Task AddCalendarAsync(UserCalendar calendar)
        {
            await _context.Set<UserCalendar>().AddAsync(calendar);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateCalendarAsync(UserCalendar calendar)
        {
            _context.Set<UserCalendar>().Update(calendar);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteCalendarAsync(Guid calendarId)
        {
            var calendar = await GetCalendarByIdAsync(calendarId);
            if (calendar != null)
            {
                _context.Set<UserCalendar>().Remove(calendar);
                await _context.SaveChangesAsync();
            }
        }
        public async Task<CalendarToken> GetCalendarTokenByEmailAsync(string email)
        {
            return await _context.Set<CalendarToken>()
                                 .Include(ct => ct.User)
                                 .FirstOrDefaultAsync(ct => ct.User.Email == email);
        }
        public async Task<IEnumerable<UserCalendar>> GetCalendarsByProfessorIdAsync(Guid professorId)
        {
       
            var professor = await _context.Set<Professor>()
                                          .FirstOrDefaultAsync(profe => profe.Id == professorId);
            if (professor == null)
            {
                return Enumerable.Empty<UserCalendar>();
            }
            return await _context.Set<UserCalendar>()
                                 .Where(uc => uc.UserId == professor.UserId)
                                 .ToListAsync();    }

    }
}
