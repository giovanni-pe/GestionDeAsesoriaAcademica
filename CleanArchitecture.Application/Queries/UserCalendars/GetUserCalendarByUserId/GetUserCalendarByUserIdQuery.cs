using System;
using CleanArchitecture.Application.ViewModels;
using CleanArchitecture.Application.ViewModels.UserCalendars;
using CleanArchitecture.Application.ViewModels.Sorting;
using MediatR;
using CleanArchitecture.Application.ViewModels.Students;

public sealed record GetUserCalendarsByUserIdQuery(Guid UserId,int status, int PageNumber, int PageSize, SortQuery? SortQuery = null, bool IncludeDeleted = false, string? SearchTerm = null) : IRequest<PagedResult<UserCalendarViewModel>>;
