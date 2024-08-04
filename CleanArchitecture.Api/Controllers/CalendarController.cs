using CleanArchitecture.Api.Services;
using Google.Apis.Calendar.v3;
using Google.Apis.Calendar.v3.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace CleanArchitecture.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CalendarController : ControllerBase
    {
        private readonly GoogleCalendarService _calendarService;

        public CalendarController()
        {
            _calendarService = new GoogleCalendarService();
        }

        [HttpGet("events/{professorEmail}")]
        public async Task<IActionResult> GetEvents(string professorEmail)
        {
            var events = await _calendarService.GetAdvisorCalendarAsync(professorEmail);
            return Ok(events);
        }

        [HttpPost("createAppointment")]
        public async Task<IActionResult> CreateStudentAppointment([FromBody] StudentAppointmentModel model)
        {
            var eventLink = await _calendarService.CreateStudentAppointmentAsync(model.ProfessorEmail, model.StudentEmail, model.StartDateTime, model.EndDateTime, model.Description);
            return Ok(new { Link = eventLink });
        }

        [HttpPost("cancelEvent")]
        public async Task<IActionResult> CancelEvent([FromBody] CancelEventModel model)
        {
            var success = await _calendarService.CancelEventAsync(model.EventId);
            return success ? Ok() : BadRequest();
        }

        [HttpPost("setNotificationPreferences")]
        public async Task<IActionResult> SetNotificationPreferences([FromBody] SetNotificationPreferencesModel model)
        {
            var eventLink = await _calendarService.SetNotificationPreferencesAsync(model.EventId, model.Reminders);
            return Ok(new { Link = eventLink });
        }

        [HttpPost("updateEvent")]
        public async Task<IActionResult> UpdateEvent([FromBody] UpdateEventModel model)
        {
            var eventLink = await _calendarService.UpdateEventAsync(model.EventId, model.Summary, model.Location, model.Description, model.StartDateTime, model.EndDateTime);
            return Ok(new { Link = eventLink });
        }


    

        public class CreateCalendarModel
        {
            public string Summary { get; set; }
        }
        public class StudentAppointmentModel
        {
            public string ProfessorEmail { get; set; }
            public string StudentEmail { get; set; }
            public DateTime StartDateTime { get; set; }
            public DateTime EndDateTime { get; set; }
            public string Description { get; set; }
        }

        public class CancelEventModel
        {
            public string EventId { get; set; }
        }

        public class SetNotificationPreferencesModel
        {
            public string EventId { get; set; }
            public EventReminder[] Reminders { get; set; }
        }

        public class UpdateEventModel
        {
            public string EventId { get; set; }
            public string Summary { get; set; }
            public string Location { get; set; }
            public string Description { get; set; }
            public DateTime StartDateTime { get; set; }
            public DateTime EndDateTime { get; set; }
        }
    }
}
