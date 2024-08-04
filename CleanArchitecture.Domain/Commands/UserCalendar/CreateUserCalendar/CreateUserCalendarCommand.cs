using MediatR;
using System;

namespace CleanArchitecture.Domain.Commands.Calendars.CreateCalendar
{
    public sealed class CreateCalendarCommand :CommandBase
    {
        public Guid UserId { get; }
        public string CalendarName { get; }
        public string TimeZone { get; }
        public string Description { get; }

        public CreateCalendarCommand(Guid userId, string calendarName, string timeZone, string description = null): base(userId)
        {
            UserId = userId;
            CalendarName = calendarName;
            TimeZone = timeZone;
            Description = description;
        }

        public override bool IsValid()
        {
            throw new NotImplementedException();
        }
    }
}
