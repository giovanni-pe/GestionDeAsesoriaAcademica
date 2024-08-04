namespace CleanArchitecture.Domain.Errors;

public static class ErrorCodes
{
    public const string CommitFailed = "COMMIT_FAILED";
    public const string ObjectNotFound = "OBJECT_NOT_FOUND";
    public const string InsufficientPermissions = "UNAUTHORIZED";
    public const string InvalidUserRole = "INVALID_USER_ROLE";
    public const string CalendarTokenNotFound = "CALENDAR_TOKENOTFOUND";
    public const string CalendarAuthorizationFailed = "CALENDAR_AUTHORIZATIONFAILED";
    public const string UserNotFound = "USER_NOT_FOUND";
    public const string DatabaseSaveFailed = "DATABASE_SAVE_FAILED";
    public const string CalendarEventCreationFailed = "CALENDAR_EVENT_CREATION_FAILED";
    public const string StudentNotFound = "STUDENT_NOT_FOUND";
}