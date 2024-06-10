using System;

namespace CleanArchitecture.Application.ViewModels.Appointments;

public sealed record DeleteAppointmentViewModel(Guid appointmentId ,DateTime dateTime);