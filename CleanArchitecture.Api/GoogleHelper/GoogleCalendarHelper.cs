using CleanArchitecture.Api.Models;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Calendar.v3;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArchitecture.Api.GoogleHelper
{
    public class GoogleCalendarHelper
    {

        public static GoogleCalendar CreateGoogleCalendar(GoogleCalendar request)
        {
            string[] Scopes
            =
            {"https://www.googleapis.com/auth/calendar" };
            string ApplicationName = "Google Canlendar Api";
            UserCredential credential;

            using (var stream = new FileStream(Path.Combine(Directory.GetCurrentDirectory(), "Credencial", "credencial.json"), FileMode.Open, FileAccess.Read))
            {
                string credPath = "token.json";
                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                GoogleClientSecrets.Load(stream).Secrets,
                Scopes, "user",
                CancellationToken.None,
                new FileDataStore(credPath, true)).Result;
            }
            var services = new CalendarService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName,
            });
            
            return request;
        }

    }
}
