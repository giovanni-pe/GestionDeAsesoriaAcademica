using Google.Apis.Calendar.v3.Data;

namespace CleanArchitecture.Domain.Settings;
public sealed class UserCalendarSettings
{
    public string CredentialsPath { get; set; }
    public string TokenPath { get; set; }
    public string User { get; set; }
    public bool FileDataStore { get; set; }
    public string ApplicationName { get; set; }
    public string CalendarId { get; set; }
    public string DefaultTimeZone { get; set; }
    public string Recurrence { get; set; }
    public bool UseDefaultReminders { get; set; }
    public EventReminder[] DefaultReminders { get; set; }
    public bool ShowDeleted { get; set; }
    public bool SingleEvents { get; set; }
    public int MaxResults { get; set; }
    public string OrderBy { get; set; }
    public bool CancelEventSuccess { get; set; }
    public bool CancelEventFailure { get; set; }
    public string ClientId { get; set; }
    public string ClientSecret { get; set; }
}