﻿using Google.Apis.Auth.OAuth2;
using Google.Apis.Calendar.v3;
using Google.Apis.Calendar.v3.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArchitecture.Api.Services
{
    public static class GoogleCalendarService
    {
        private static readonly string[] Scopes = { CalendarService.Scope.Calendar };
        private static readonly string ApplicationName = "Google Calendar API .NET Quickstart";

        private static async Task<CalendarService> GetCalendarServiceAsync()
        {
            UserCredential credential;

            using (var stream = new FileStream("../Credentials/credentials.json", FileMode.Open, FileAccess.Read))
            {
                string credPath = "../Credentials/token.json";
                credential = await GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    Scopes,
                    "user",
                    CancellationToken.None,
                    new FileDataStore(credPath, true)
                );
            }

            return new CalendarService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName,
            });
        }

        public static async Task<Events> GetUpcomingEventsAsync(int maxResults = 10)
        {
            var service = await GetCalendarServiceAsync();

            EventsResource.ListRequest request = service.Events.List("primary");
            request.TimeMin = DateTime.Now;
            request.ShowDeleted = false;
            request.SingleEvents = true;
            request.MaxResults = maxResults;
            request.OrderBy = EventsResource.ListRequest.OrderByEnum.StartTime;

            return await request.ExecuteAsync();
        }

        public static async Task<string> CreateEventAsync(string summary, string location, string description, DateTime startDateTime, DateTime endDateTime)
        {
            var service = await GetCalendarServiceAsync();

            Event newEvent = new Event()
            {
                Summary = summary,
                Location = location,
                Description = description,
                Start = new EventDateTime()
                {
                    DateTime = startDateTime,
                    TimeZone = "America/Los_Angeles",
                },
                End = new EventDateTime()
                {
                    DateTime = endDateTime,
                    TimeZone = "America/Los_Angeles",
                },
                Recurrence = new String[] { "RRULE:FREQ=DAILY;COUNT=1" },
                Attendees = new EventAttendee[] { },
                Reminders = new Event.RemindersData()
                {
                    UseDefault = false,
                    Overrides = new EventReminder[]
                    {
                        new EventReminder() { Method = "email", Minutes = 24 * 60 },
                        new EventReminder() { Method = "popup", Minutes = 10 },
                    }
                }
            };

            EventsResource.InsertRequest request = service.Events.Insert(newEvent, "primary");
            Event createdEvent = await request.ExecuteAsync();
            return createdEvent.HtmlLink;
        }
          public static async Task<bool> CancelEventAsync(string eventId)
    {
        var service = await GetCalendarServiceAsync();
        try
        {
            var request = service.Events.Delete("primary", eventId);
            await request.ExecuteAsync();
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public static async Task<Events> GetAdvisorCalendarAsync(string advisorEmail)
    {
        var service = await GetCalendarServiceAsync();

        EventsResource.ListRequest request = service.Events.List(advisorEmail);
        request.TimeMin = DateTime.Now;
        request.ShowDeleted = false;
        request.SingleEvents = true;
        request.OrderBy = EventsResource.ListRequest.OrderByEnum.StartTime;

        return await request.ExecuteAsync();
    }

    public static async Task<string> SetNotificationPreferencesAsync(string eventId, EventReminder[] reminders)
    {
        var service = await GetCalendarServiceAsync();

        Event eventToUpdate = await service.Events.Get("primary", eventId).ExecuteAsync();
        eventToUpdate.Reminders = new Event.RemindersData()
        {
            UseDefault = false,
            Overrides = reminders,
        };

        EventsResource.UpdateRequest updateRequest = service.Events.Update(eventToUpdate, "primary", eventId);
        Event updatedEvent = await updateRequest.ExecuteAsync();
        return updatedEvent.HtmlLink;
    }
        public static async Task<string> UpdateEventAsync(string eventId, string summary, string location, string description, DateTime startDateTime, DateTime endDateTime)
        {
            var service = await GetCalendarServiceAsync();

            // Retrieve the event
            Event eventToUpdate = await service.Events.Get("primary", eventId).ExecuteAsync();

            // Update the event details
            eventToUpdate.Summary = summary;
            eventToUpdate.Location = location;
            eventToUpdate.Description = description;
            eventToUpdate.Start = new EventDateTime()
            {
                DateTime = startDateTime,
                TimeZone = "America/Los_Angeles",
            };
            eventToUpdate.End = new EventDateTime()
            {
                DateTime = endDateTime,
                TimeZone = "America/Los_Angeles",
            };
            eventToUpdate.Reminders = new Event.RemindersData()
            {
                UseDefault = false,
            };

            // Update the event
            EventsResource.UpdateRequest updateRequest = service.Events.Update(eventToUpdate, "primary", eventId);
            Event updatedEvent = await updateRequest.ExecuteAsync();
            return updatedEvent.HtmlLink;
        }

    }

}
