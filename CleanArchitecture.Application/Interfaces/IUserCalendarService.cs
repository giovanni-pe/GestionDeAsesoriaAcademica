using System;
using System.Threading.Tasks;
using CleanArchitecture.Application.ViewModels.Sorting;
using CleanArchitecture.Application.ViewModels;
using CleanArchitecture.Application.ViewModels.UserCalendars;

namespace CleanArchitecture.Application.Interfaces
{
    public interface IUserCalendarService
    {
        Task<Guid> CreateUserCalendarAsync(CreateUserCalendarViewModel calendarViewModel);
        Task<PagedResult<UserCalendarViewModel>> GetUserCalendarsByUserIdAsync(Guid UserId, int status, int pageNumber, int pageSize, SortQuery? sortQuery = null, bool includeDeleted = false, string? searchTerm = null);
      
        //Task UpdateUserCalendarAsync(UpdateUserCalendarViewModel calendarViewModel);
        //Task DeleteUserCalendarAsync(Guid calendarId);
        //Task<UserCalendarViewModel?> GetUserCalendarByIdAsync(Guid calendarId);
    }
}

