using System;

namespace CleanArchitecture.Domain.Entities
{
    public class UserCalendar : Entity
    {
        public string CalendarId { get; private set; }
        public string CalendarName { get; private set; }
        public string TimeZone { get; private set; }
        public string IframeUrl { get; private set; }
        public string Description { get; private set; } // Descripción opcional del calendario

        public Guid UserId { get; private set; }
        public virtual User User { get; private set; }

        public UserCalendar(
            Guid id,
            Guid userId,
            string calendarName,
            string timeZone,
            string calendarId = null,
            string iframeUrl = null,
            string description = null) : base(id)
        {
            UserId = userId;
            CalendarName = calendarName;
            TimeZone = timeZone;
            CalendarId = calendarId;
            IframeUrl = iframeUrl;
            Description = description;
        }

        public void SetCalendarId(string calendarId)
        {
            CalendarId = calendarId;
        }

        public void SetCalendarName(string calendarName)
        {
            CalendarName = calendarName;
        }

        public void SetTimeZone(string timeZone)
        {
            TimeZone = timeZone;
        }

        public void SetIframeUrl(string iframeUrl)
        {
            IframeUrl = iframeUrl;
        }

        public void SetDescription(string description)
        {
            Description = description;
        }
    }
}
