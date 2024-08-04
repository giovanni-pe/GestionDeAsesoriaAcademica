using System;
using CleanArchitecture.Application.ViewModels.UserCalendars;
using MediatR;

namespace CleanArchitecture.Application.Queries.UserCalendars.GetUserCalendarById;

public sealed record GetUserCalendarByIdQuery(Guid UserCalendarId) : IRequest<UserCalendarViewModel?>;