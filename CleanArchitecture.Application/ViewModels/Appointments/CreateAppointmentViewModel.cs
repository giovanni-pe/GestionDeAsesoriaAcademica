using System;

public class CreateAppointmentViewModel
{
    public Guid professorId { get; set; }
    public Guid studentId { get; set; }
    public Guid calendarId { get; set; }
    public DateTime dateTime { get; set; }
    public string professorProgress { get; set; }
    public string studentProgress { get; set; }

    public CreateAppointmentViewModel(Guid professorId, Guid studentId, Guid calendarId, DateTime dateTime, string professorProgress, string studentProgress)
    {
        this.professorId = professorId;
        this.studentId = studentId;
        this.calendarId = calendarId;
        this.dateTime = dateTime;
        this.professorProgress = professorProgress;
        this.studentProgress = studentProgress;
    }

    // Constructor sin parámetros para la serialización
    public CreateAppointmentViewModel() { }
}
