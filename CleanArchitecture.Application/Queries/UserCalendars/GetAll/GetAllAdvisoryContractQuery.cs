using CleanArchitecture.Application.ViewModels;
using CleanArchitecture.Application.ViewModels.Sorting;
using CleanArchitecture.Application.ViewModels.UserCalendars;
using MediatR;
using System;

namespace CleanArchitecture.Application.Queries.UserCalendars.GetAll;

public sealed record UserCalendarsQuery(Guid researchLineId,DateTime startDate,DateTime endDate,
    PageQuery Query,
    bool IncludeDeleted,
    string SearchTerm = "",
    SortQuery? SortQuery = null) :
    IRequest<PagedResult<UserCalendarViewModel>>;