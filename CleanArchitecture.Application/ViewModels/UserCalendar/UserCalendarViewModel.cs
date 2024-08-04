using CleanArchitecture.Domain.Entities;
using System;

namespace CleanArchitecture.Application.ViewModels.UserCalendars
{
    public sealed class UserCalendarViewModel
    {
        public Guid CalendarId { get; set; }
        public Guid UserId { get; set; }
        public string CalendarName { get; set; }
        public string TimeZone { get; set; }
        public string IframeUrl { get; set; }
        public string Description { get; set; }
        public static UserCalendarViewModel FromUserCalendar(UserCalendar userCalendar)
        {
            return new UserCalendarViewModel
            {
                CalendarId = userCalendar.Id,
                UserId = userCalendar.UserId,
                CalendarName = userCalendar.CalendarName,
                TimeZone = userCalendar.TimeZone,
                Description = userCalendar.Description,
                IframeUrl= userCalendar.IframeUrl,
            };
        }
    }
}
