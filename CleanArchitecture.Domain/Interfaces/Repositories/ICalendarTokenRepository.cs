using System.Threading.Tasks;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Interfaces.Repositories;

namespace CleanArchitecture.Domain.Interfaces.Repositories
{
    public interface ICalendarTokenRepository: IRepository<CalendarToken>
    {
        Task AddOrUpdateTokenAsync(CalendarToken token);
        Task<CalendarToken> GetTokenByUserEmailAsync(string userEmail);
    }
}
