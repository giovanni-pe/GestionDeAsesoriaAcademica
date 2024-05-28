using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using CleanArchitecture.Application.ViewModels.Sorting;
using CleanArchitecture.Application.ViewModels.Appointments;
using CleanArchitecture.Domain.Entities;

namespace CleanArchitecture.Application.SortProviders;

public sealed class AppointmentViewModelSortProvider : ISortingExpressionProvider<AppointmentViewModel, Appointment>
{
    private static readonly Dictionary<string, Expression<Func<Appointment, object>>> s_expressions = new()
    {
        { "id", Appointment => Appointment.Id },
        { "ProfessorId", Appointment => Appointment.ProfessorId},
        { "StudentId", Appointment => Appointment.StudentId},
        { "CalendarId", Appointment => Appointment.CalendarId },
        {"CalendarId", Appointment => Appointment.DateTime },
        { "CalendarId", Appointment => Appointment.ProfessorProgress },
        { "CalendarId", Appointment => Appointment.StudentProgress
        }


    };

    public Dictionary<string, Expression<Func<Appointment, object>>> GetSortingExpressions()
    {
        return s_expressions;
    }
}