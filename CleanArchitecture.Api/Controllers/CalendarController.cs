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
