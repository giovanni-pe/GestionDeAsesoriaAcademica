using System;
using System.Threading.Tasks;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Calendar.v3.Data;

namespace CleanArchitecture.Domain.Interfaces
{
    public interface IGoogleCalendarIntegration
    {
        Task<UserCredential> AuthorizeUserAsync(string userEmail);
        Task<string> CreateCalendarAsync(string calendarName, string timeZone);
        Task<Events> GetUpcomingEventsAsync(string userEmail, int maxResults = 10);
        Task<string> CreateEventAsync(string userEmail, string summary, string location, string description, DateTime startDateTime, DateTime endDateTime);
        Task<bool> CancelEventAsync(string userEmail, string eventId);
        Task<Events> GetAdvisorCalendarAsync(string advisorEmail);
        Task<string> SetNotificationPreferencesAsync(string userEmail, string eventId, EventReminder[] reminders);
        Task<string> CreateStudentAppointmentAsync(string professorEmail, string studentEmail, DateTime startDateTime, DateTime endDateTime, string description);

        Task<string> UpdateEventAsync(string userEmail, string eventId, string summary, string location, string description, DateTime startDateTime, DateTime endDateTime);
    }
}
