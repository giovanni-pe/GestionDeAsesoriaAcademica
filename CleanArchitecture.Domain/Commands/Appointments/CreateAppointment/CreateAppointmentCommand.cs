using System;

namespace CleanArchitecture.Domain.Commands.Appointments.CreateAppointment;

public sealed class CreateAppointmentCommand : CommandBase
{
    private static readonly CreateAppointmentCommandValidation s_validation = new();

    public Guid ProfessorId { get; }
    public Guid AppointmentId { get; }
    public Guid StudentId { get; }
    public Guid CalendarId { get;  }
    public DateTime DateTime { get;  }
    public string ProfessorProgress { get;  }
    public string StudentProgress { get;  }

    public CreateAppointmentCommand(Guid appointmentId, Guid professorId, Guid studentId, Guid calendarId, DateTime dateTime, string professorProgress, string studentProgress) : base(appointmentId)
    {
        ProfessorId = professorId;
        AppointmentId = appointmentId;
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