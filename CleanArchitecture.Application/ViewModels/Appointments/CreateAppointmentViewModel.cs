using System;

namespace CleanArchitecture.Application.ViewModels.Appointments;

public sealed record CreateAppointmentViewModel(Guid professorId, Guid studentId, Guid calendarId, DateTime dateTime, string professorProgress, string studentProgress,int status,string googleEventId
   , string ProfessorEmail,
string StudentEmail ,
DateTime StartDateTime ,
DateTime EndDateDateTime ,
 string description );
