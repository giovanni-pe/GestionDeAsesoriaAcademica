using RabbitMQ.Client;
using System;

namespace CleanArchitecture.Domain.Commands.Appointments.CreateAppointment
{
    public sealed class CreateAppointmentCommand : CommandBase
    {
        private static readonly CreateAppointmentCommandValidation s_validation = new();

        public Guid ProfessorId { get; set; }
        public Guid AppointmentId { get; set; }
        public Guid StudentId { get; set; }
        public Guid CalendarId { get; set; }
        public DateTime DateTime { get; set; }
        public string ProfessorProgress { get; set; }
        public string StudentProgress { get; set; }
        public int Status { get; set; }
        public string GoogleEventId { get; set; }
        public string   ProfessorEmail { get; set; }
        public string   StudentEmail {  get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
        public string Description { get; set; }
       
        

        public CreateAppointmentCommand(Guid appointmentId, Guid professorId, Guid studentId, Guid calendarId,
            DateTime dateTime, string professorProgress, string studentProgress,int status,string googleEventId,string professorEmail, string studentEmail, DateTime startDateTime, DateTime endDateTime,string description) : base(appointmentId)
        {
            AppointmentId = appointmentId;
            ProfessorId = professorId;
            StudentId = studentId;
            CalendarId = calendarId;
            DateTime = dateTime;
            ProfessorProgress = professorProgress;
            StudentProgress = studentProgress;
            Status = status;
            GoogleEventId = googleEventId;
            ProfessorEmail = professorEmail;
            StudentEmail = studentEmail;
            StartDateTime = startDateTime;
            EndDateTime = endDateTime;
            Description = description;

        }

        public override bool IsValid()
        {
            ValidationResult = s_validation.Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
