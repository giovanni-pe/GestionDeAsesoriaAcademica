using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using CleanArchitecture.Application.ViewModels.Sorting;
using CleanArchitecture.Application.ViewModels.UserCalendars;
using CleanArchitecture.Domain.Entities;

namespace CleanArchitecture.Application.SortProviders
{
    public sealed class UserCalendarViewModelSortProvider : ISortingExpressionProvider<UserCalendarViewModel, UserCalendar>
    {
        private static readonly Dictionary<string, Expression<Func<UserCalendar, object>>> s_expressions = new()
        {
            { "id", calendar => calendar.Id },
            { "userId", calendar => calendar.UserId },
            { "calendarName", calendar => calendar.CalendarName },
            { "timeZone", calendar => calendar.TimeZone },
            { "iframeUrl", calendar => calendar.IframeUrl },
            { "description", calendar => calendar.Description }
        };

        public Dictionary<string, Expression<Func<UserCalendar, object>>> GetSortingExpressions()
        {
            return s_expressions;
        }
    }
}
