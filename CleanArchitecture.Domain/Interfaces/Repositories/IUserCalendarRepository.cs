using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CleanArchitecture.Domain.Entities;

namespace CleanArchitecture.Domain.Interfaces.Repositories
{
    public interface IUserCalendarRepository : IRepository<UserCalendar>
    {
        Task<UserCalendar> GetCalendarByIdAsync(Guid calendarId);
        Task<IEnumerable<UserCalendar>> GetCalendarsByUserIdAsync(Guid userId);
        Task AddCalendarAsync(UserCalendar calendar);
        Task UpdateCalendarAsync(UserCalendar calendar);
        Task DeleteCalendarAsync(Guid calendarId);
        public Task<CalendarToken> GetCalendarTokenByEmailAsync(string email);
        public Task<IEnumerable<UserCalendar>> GetCalendarsByProfessorIdAsync(Guid professorId);
    }
}
