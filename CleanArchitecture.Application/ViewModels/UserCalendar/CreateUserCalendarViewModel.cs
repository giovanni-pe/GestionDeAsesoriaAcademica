using CleanArchitecture.Domain.Entities;
using System;

namespace CleanArchitecture.Application.ViewModels.UserCalendars
{
    public class CreateUserCalendarViewModel
    {
        public Guid UserId { get; set; }
        public string CalendarName { get; set; }
        public string TimeZone { get; set; }
        public string Description { get; set; }
        public static UserCalendarViewModel FromUserCalendar(UserCalendar userCalendar)
        {
            return new UserCalendarViewModel
            {
                UserId = userCalendar.UserId,
                CalendarName = userCalendar.CalendarName,
                TimeZone= userCalendar.TimeZone,
                Description = userCalendar.Description
            };
        }
    }
}
