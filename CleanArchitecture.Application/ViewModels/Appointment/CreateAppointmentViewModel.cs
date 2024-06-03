using System;

namespace CleanArchitecture.Application.ViewModels.Appointments;



public class CreateAppointmentViewModel
{
    public string professorProgress;
    public DateTime dateTime;

    public CreateAppointmentViewModel(Guid professorId, Guid studentId, Guid calendarId, DateTime dateTime, string professorProgress, string studentProgress)
    {

    }

    public Guid professorId { get; }
    public Guid studentId { get; }
    public Guid calendarId { get; }
    public DateTime DateTime { get; }
    public string professorProgressId { get; }
    public string studentProgress { get; }
}
