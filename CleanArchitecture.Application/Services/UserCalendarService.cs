using System;
using System.Threading.Tasks;
using CleanArchitecture.Application.Interfaces;
using CleanArchitecture.Application.ViewModels;
using CleanArchitecture.Domain.Commands.Calendars.CreateCalendar;
using CleanArchitecture.Application.ViewModels.UserCalendars;
//using CleanArchitecture.Domain.Commands.Calendars.UpdateCalendar;
//using CleanArchitecture.Domain.Commands.Calendars.DeleteCalendar;
using CleanArchitecture.Domain.Interfaces;
//using CleanArchitecture.Domain.Queries.Calendars;
using Microsoft.Extensions.Caching.Distributed;
using CleanArchitecture.Application.ViewModels.Sorting;

namespace CleanArchitecture.Application.Services
{
    public sealed class UserCalendarService : IUserCalendarService
    {
        private readonly IMediatorHandler _bus;
        private readonly IDistributedCache _distributedCache;

        public UserCalendarService(IMediatorHandler bus, IDistributedCache distributedCache)
        {
            _bus = bus;
            _distributedCache = distributedCache;
        }

        public async Task<Guid> CreateUserCalendarAsync(CreateUserCalendarViewModel calendarViewModel)
        {
            var calendarId = Guid.NewGuid();
            await _bus.SendCommandAsync(new CreateCalendarCommand(
                calendarViewModel.UserId,
                calendarViewModel.CalendarName,
                calendarViewModel.TimeZone,
                calendarViewModel.Description));

            return calendarId;
        }
        public async Task<PagedResult<UserCalendarViewModel>> GetUserCalendarsByUserIdAsync(Guid UserId, int status, int pageNumber, int pageSize, SortQuery? sortQuery = null, bool includeDeleted = false, string? searchTerm = null)
        {
            var userCalendars = await _bus.QueryAsync(new GetUserCalendarsByUserIdQuery(UserId, status, pageNumber, pageSize, sortQuery, includeDeleted, searchTerm));
            return userCalendars;
        }
            //public async Task UpdateUserCalendarAsync(UpdateUserCalendarViewModel calendarViewModel)
            //{
            //    await _bus.SendCommandAsync(new UpdateCalendarCommand(
            //        calendarViewModel.CalendarId,
            //        calendarViewModel.CalendarName,
            //        calendarViewModel.TimeZone,
            //        calendarViewModel.Description));
            //}

            //public async Task DeleteUserCalendarAsync(Guid calendarId)
            //{
            //    await _bus.SendCommandAsync(new DeleteCalendarCommand(calendarId));
            //}

            //public async Task<UserCalendarViewModel?> GetUserCalendarByIdAsync(Guid calendarId)
            //{
            //    var cacheKey = $"UserCalendar_{calendarId}";
            //    var cachedCalendar = await _distributedCache.GetStringAsync(cacheKey);

            //    if (!string.IsNullOrEmpty(cachedCalendar))
            //    {
            //        return System.Text.Json.JsonSerializer.Deserialize<UserCalendarViewModel>(cachedCalendar);
            //    }

            //    var calendar = await _bus.QueryAsync(new GetCalendarByIdQuery(calendarId));

            //    if (calendar != null)
            //    {
            //        var cacheOptions = new DistributedCacheEntryOptions
            //        {
            //            SlidingExpiration = TimeSpan.FromDays(3),
            //            AbsoluteExpirationRelativeToNow = TimeSpan.FromDays(30)
            //        };
            //        await _distributedCache.SetStringAsync(cacheKey, System.Text.Json.JsonSerializer.Serialize(calendar), cacheOptions);
            //    }

            //    return calendar;
            //}
        }
    }
