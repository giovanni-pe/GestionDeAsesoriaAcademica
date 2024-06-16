using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Google.Apis.Calendar.v3.Data;
using CleanArchitecture.Api.Services;

namespace CleanArchitecture.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CalendarController : ControllerBase
    {
        [HttpGet("events")]
        public async Task<IActionResult> GetUpcomingEvents()
        {
            try
            {
                var events = await GoogleCalendarService.GetUpcomingEventsAsync();
                return Ok(events);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("events")]
        public async Task<IActionResult> CreateEvent([FromBody] EventRequest eventRequest)
        {
            try
            {
                var eventLink = await GoogleCalendarService.CreateEventAsync(
                    eventRequest.Summary,
                    eventRequest.Location,
                    eventRequest.Description,
                    eventRequest.StartDateTime,
                    eventRequest.EndDateTime
                );
                return Ok(new { EventLink = eventLink });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("events/{eventId}")]
        public async Task<IActionResult> CancelEvent(string eventId)
        {
            try
            {
                bool result = await GoogleCalendarService.CancelEventAsync(eventId);
                if (result)
                {
                    return Ok();
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("advisor/{advisorEmail}/calendar")]
        public async Task<IActionResult> GetAdvisorCalendar(string advisorEmail)
        {
            try
            {
                var events = await GoogleCalendarService.GetAdvisorCalendarAsync(advisorEmail);
                return Ok(events);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("events/{eventId}/notifications")]
        public async Task<IActionResult> SetNotificationPreferences(string eventId, [FromBody] EventReminder[] reminders)
        {
            try
            {
                var eventLink = await GoogleCalendarService.SetNotificationPreferencesAsync(eventId, reminders);
                return Ok(new { EventLink = eventLink });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpPost("events/{eventId}")]
        public async Task<IActionResult> UpdateEvent(string eventId, [FromBody] EventRequest eventRequest)
        {
            try
            {
                var eventLink = await GoogleCalendarService.UpdateEventAsync(
                    eventId,
                    eventRequest.Summary,
                    eventRequest.Location,
                    eventRequest.Description,
                    eventRequest.StartDateTime,
                    eventRequest.EndDateTime
                );
                return Ok(new { EventLink = eventLink });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

    }

    public class EventRequest
    {
        public string Summary { get; set; }
        public string Location { get; set; }
        public string Description { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
    }

}
