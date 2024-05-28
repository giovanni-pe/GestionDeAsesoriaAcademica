using System;
using System.Globalization;

namespace CleanArchitecture.Shared.Events.Appointment;

public sealed class AppointmentUpdatedEvent : DomainEvent
{
    public Guid ProfessorId { get; private set; }

    public Guid AppointmentId { get; private set; }
    public Guid StudentId { get; private set; }
    public Guid CalendarId { get; private set; }

    public AppointmentUpdatedEvent(Guid appointmentId, Guid professorId, Guid studentId, Guid calendarId, DateTime dateTime, string professorProgress, string studentProgress) : base(appointmentId)
    {
        ProfessorId = professorId;
        AppointmentId = appointmentId;
        StudentId = studentId;
        CalendarId = calendarId;
    }
}