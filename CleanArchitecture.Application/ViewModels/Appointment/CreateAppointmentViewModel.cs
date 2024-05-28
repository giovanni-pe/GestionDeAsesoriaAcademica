using System;

namespace CleanArchitecture.Application.ViewModels.Appointments;

public sealed record CreateAppointmentViewModel(Guid appointmentId, Guid professorId, Guid studentId, Guid calendarId, DateTime dateTime, string professorProgress, string studentProgress);