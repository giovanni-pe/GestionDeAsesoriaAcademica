using System;
using System.Globalization;

namespace CleanArchitecture.Shared.Events.Appointment;

public sealed class AppointmentUpdatedEvent : DomainEvent
{
    public Guid ProfessorId { get; private set; }

  
    public Guid StudentId { get; private set; }
    public Guid CalendarId { get; private set; }

    public AppointmentUpdatedEvent(Guid appointmentId, Guid professorId, Guid studentId, Guid calendarId) : base(appointmentId)
    {
        ProfessorId = professorId;
       
        StudentId = studentId;
        CalendarId = calendarId;
    }
}