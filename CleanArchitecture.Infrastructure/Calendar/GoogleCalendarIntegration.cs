using Google.Apis.Auth.OAuth2;
using Google.Apis.Calendar.v3;
using Google.Apis.Calendar.v3.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using CleanArchitecture.Domain.Interfaces;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Interfaces.Repositories;
using CleanArchitecture.Domain.Settings;
using CleanArchitecture.Domain.Enums;
using System.Collections.Generic;
using CleanArchitecture.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq;


namespace CleanArchitecture.Infrastructure.Integration
{
    public class GoogleCalendarIntegration : IGoogleCalendarIntegration
    {
        private readonly UserCalendarSettings _UserCalendarSettings;
        private readonly ICalendarTokenRepository _calendarTokenRepository;
        private readonly IUserCalendarRepository _userCalendarRepository;
        private readonly IUserRepository _userRepository;
       

        public GoogleCalendarIntegration(ICalendarTokenRepository calendarTokenRepository,
            IUserCalendarRepository userCalendarRepository,IUserRepository userRepository)
        {
            _UserCalendarSettings = LoadUserCalendarSettings();
            _calendarTokenRepository = calendarTokenRepository;
            _userCalendarRepository = userCalendarRepository;
            _userRepository = userRepository;
        }

        private UserCalendarSettings LoadUserCalendarSettings()
        {
            string UserCalendarSettingsContent = File.ReadAllText("calendarSettings.json");
            return JsonConvert.DeserializeObject<UserCalendarSettings>(UserCalendarSettingsContent);
        }

        public async Task<UserCredential> AuthorizeUserAsync(string userEmail)
        {
            var token = await _userCalendarRepository.GetCalendarTokenByEmailAsync(userEmail);
            

            using (var stream = new FileStream(_UserCalendarSettings.CredentialsPath, FileMode.Open, FileAccess.Read))
            {
                var credential = await GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    new[] { CalendarService.Scope.Calendar },
                    userEmail,
                    CancellationToken.None,
                    new FileDataStore(_UserCalendarSettings.TokenPath, _UserCalendarSettings.FileDataStore)
                );

                return credential;
            }
        }
        private async Task<CalendarService> GetCalendarServiceAsync(string userEmail)
        {
            var credential = await AuthorizeUserAsync(userEmail);
            return new CalendarService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = _UserCalendarSettings.ApplicationName,
            });
        }

        public async Task<string> CreateCalendarAsync(string calendarName, string timeZone)
        {
            var userEmail = "defaultEmail@example.com"; // Cambia esto por la lógica adecuada para obtener el correo electrónico del usuario
            var credential = await AuthorizeUserAsync(userEmail);
            var service = new CalendarService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = _UserCalendarSettings.ApplicationName,
            });

            var calendar = new Calendar
            {
                Summary = calendarName,
                TimeZone = timeZone
            };

            var createdCalendar = await service.Calendars.Insert(calendar).ExecuteAsync();
            return createdCalendar.Id;
        }

        public async Task<Events> GetUpcomingEventsAsync(string userEmail, int maxResults = 10)
        {
            var service = await GetCalendarServiceAsync(userEmail);

            EventsResource.ListRequest request = service.Events.List(_UserCalendarSettings.CalendarId);
            request.TimeMin = DateTime.Now;
            request.ShowDeleted = _UserCalendarSettings.ShowDeleted;
            request.SingleEvents = _UserCalendarSettings.SingleEvents;
            request.MaxResults = maxResults;
            request.OrderBy = EventsResource.ListRequest.OrderByEnum.StartTime;
            return await request.ExecuteAsync();
        }

        public async Task<string> CreateEventAsync(string userEmail, string summary, string location, string description, DateTime startDateTime, DateTime endDateTime)
        {
            var service = await GetCalendarServiceAsync(userEmail);

            Event newEvent = new Event()
            {
                Summary = summary,
                Location = location,
                Description = description,
                Start = new EventDateTime()
                {
                    DateTime = startDateTime,
                    TimeZone = _UserCalendarSettings.DefaultTimeZone,
                },
                End = new EventDateTime()
                {
                    DateTime = endDateTime,
                    TimeZone = _UserCalendarSettings.DefaultTimeZone,
                },
                Recurrence = new String[] { _UserCalendarSettings.Recurrence },
                Attendees = new EventAttendee[] { },
                Reminders = new Event.RemindersData()
                {
                    UseDefault = _UserCalendarSettings.UseDefaultReminders,
                    Overrides = _UserCalendarSettings.DefaultReminders
                }
            };

            EventsResource.InsertRequest request = service.Events.Insert(newEvent, _UserCalendarSettings.CalendarId);
            Event createdEvent = await request.ExecuteAsync();
            return createdEvent.HtmlLink;
        }

        public async Task<bool> CancelEventAsync(string userEmail, string eventId)
        {
            var service = await GetCalendarServiceAsync(userEmail);
            try
            {
                var request = service.Events.Delete(_UserCalendarSettings.CalendarId, eventId);
                await request.ExecuteAsync();
                return _UserCalendarSettings.CancelEventSuccess;
            }
            catch (Exception)
            {
                return _UserCalendarSettings.CancelEventFailure;
            }
        }

        public async Task<Events> GetAdvisorCalendarAsync(string advisorEmail)
        {
            var service = await GetCalendarServiceAsync(advisorEmail);

            EventsResource.ListRequest request = service.Events.List(advisorEmail);
            request.TimeMin = DateTime.Now;
            request.ShowDeleted = _UserCalendarSettings.ShowDeleted;
            request.SingleEvents = _UserCalendarSettings.SingleEvents;
            request.OrderBy = (EventsResource.ListRequest.OrderByEnum)Enum.Parse(typeof(EventsResource.ListRequest.OrderByEnum), _UserCalendarSettings.OrderBy);

            return await request.ExecuteAsync();
        }

        public async Task<string> SetNotificationPreferencesAsync(string userEmail, string eventId, EventReminder[] reminders)
        {
            var service = await GetCalendarServiceAsync(userEmail);

            Event eventToUpdate = await service.Events.Get(_UserCalendarSettings.CalendarId, eventId).ExecuteAsync();
            eventToUpdate.Reminders = new Event.RemindersData()
            {
                UseDefault = false,
                Overrides = reminders,
            };

            EventsResource.UpdateRequest updateRequest = service.Events.Update(eventToUpdate, _UserCalendarSettings.CalendarId, eventId);
            Event updatedEvent = await updateRequest.ExecuteAsync();
            return updatedEvent.HtmlLink;
        }

        public async Task<string> UpdateEventAsync(string userEmail, string eventId, string summary, string location, string description, DateTime startDateTime, DateTime endDateTime)
        {
            var service = await GetCalendarServiceAsync(userEmail);

            Event eventToUpdate = await service.Events.Get(_UserCalendarSettings.CalendarId, eventId).ExecuteAsync();

            eventToUpdate.Summary = summary;
            eventToUpdate.Location = location;
            eventToUpdate.Description = description;
            eventToUpdate.Start = new EventDateTime()
            {
                DateTime = startDateTime,
                TimeZone = _UserCalendarSettings.DefaultTimeZone,
            };
            eventToUpdate.End = new EventDateTime()
            {
                DateTime = endDateTime,
                TimeZone = _UserCalendarSettings.DefaultTimeZone,
            };
            eventToUpdate.Reminders = new Event.RemindersData()
            {
                UseDefault = _UserCalendarSettings.UseDefaultReminders,
            };

            EventsResource.UpdateRequest updateRequest = service.Events.Update(eventToUpdate, _UserCalendarSettings.CalendarId, eventId);
            Event updatedEvent = await updateRequest.ExecuteAsync();
            return updatedEvent.HtmlLink;
        }
        public async Task<string> CreateStudentAppointmentAsync(string professorEmail, string studentEmail, DateTime startDateTime, DateTime endDateTime, string description)
        {
            // Encontrar el usuario (profesor) por correo electrónico
            var professor = await _userRepository.GetByEmailAsync(professorEmail);
            if (professor == null)
            {
                throw new Exception($"No se encontró el usuario (profesor) con el correo {professorEmail}");
            }

            // Obtener el token del calendario del profesor
            var calendarToken = await _userCalendarRepository.GetCalendarTokenByEmailAsync(professorEmail);
            if (calendarToken == null)
            {
                throw new Exception($"No se encontró el token para el usuario con el correo {professorEmail}");
            }

            // Autorizar al usuario
            var service = await GetCalendarServiceAsync(professorEmail);

            // Obtener el calendario del profesor por UserId
            var userCalendars = await _userCalendarRepository.GetCalendarsByUserIdAsync(professor.Id);
            var userCalendar = userCalendars.FirstOrDefault();
            if (userCalendar == null)
            {
                throw new Exception($"No se encontró el calendario para el profesor con el correo {professorEmail}");
            }

            Event newEvent = new Event()
            {
                Summary = "Student Appointment",
                Description = description,
                Start = new EventDateTime()
                {
                    DateTime = startDateTime,
                    TimeZone = _UserCalendarSettings.DefaultTimeZone,
                },
                End = new EventDateTime()
                {
                    DateTime = endDateTime,
                    TimeZone = _UserCalendarSettings.DefaultTimeZone,
                },
                Attendees = new EventAttendee[]
                {
                    new EventAttendee() { Email = professorEmail },
                    new EventAttendee() { Email = studentEmail }
                },
                Reminders = new Event.RemindersData()
                {
                    UseDefault = _UserCalendarSettings.UseDefaultReminders,
                    Overrides = _UserCalendarSettings.DefaultReminders
                }
            };

            EventsResource.InsertRequest request = service.Events.Insert(newEvent, userCalendar.CalendarId);
            Event createdEvent = await request.ExecuteAsync();
            return createdEvent.HtmlLink;
        }
    }
}