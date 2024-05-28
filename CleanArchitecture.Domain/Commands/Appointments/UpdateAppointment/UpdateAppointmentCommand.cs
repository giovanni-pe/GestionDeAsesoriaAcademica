using System;

namespace CleanArchitecture.Domain.Commands.Appointments.UpdateAppointment;

public sealed class UpdateAppointmentCommand : CommandBase
{
    private static readonly UpdateAppointmentCommandValidation s_validation = new();

    public Guid ProfessorId { get; }
   
    public Guid StudentId { get; }
    public Guid CalendarId { get; }
    public DateTime DateTime { get; }
    public string ProfessorProgress { get; }
    public string StudentProgress { get; }
    public UpdateAppointmentCommand(Guid appointmentId, Guid professorId, Guid studentId, Guid calendarId, DateTime dateTime, string professorProgress, string studentProgress) : base(appointmentId)
    {
        ProfessorId = professorId;
        
        StudentId = studentId;
        CalendarId = calendarId;
        DateTime = dateTime;
        ProfessorProgress = professorProgress;
        StudentProgress = studentProgress;
    }

    public override bool IsValid()
    {
        ValidationResult = s_validation.Validate(this);
        return ValidationResult.IsValid;
    }
}