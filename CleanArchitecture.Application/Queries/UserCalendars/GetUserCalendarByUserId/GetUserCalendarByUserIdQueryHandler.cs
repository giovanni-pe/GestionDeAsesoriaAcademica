using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CleanArchitecture.Application.Extensions;
using CleanArchitecture.Application.ViewModels;
using CleanArchitecture.Application.ViewModels.Sorting;
using CleanArchitecture.Application.ViewModels.UserCalendars;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Interfaces.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using CleanArchitecture.Domain.Enums;

namespace CleanArchitecture.Application.Queries.UserCalendars.GetUserCalendarsByUserId;

public sealed class GetUserCalendarsByUserIdQueryHandler :
    IRequestHandler<GetUserCalendarsByUserIdQuery, PagedResult<UserCalendarViewModel>>
{
    private readonly IUserCalendarRepository _UserCalendarRepository;
    private readonly ISortingExpressionProvider<UserCalendarViewModel, UserCalendar> _sortingExpressionProvider;

    public GetUserCalendarsByUserIdQueryHandler(
        IUserCalendarRepository UserCalendarRepository,
        IProfessorRepository professorRepository,
        ISortingExpressionProvider<UserCalendarViewModel, UserCalendar> sortingExpressionProvider)
    {
        _UserCalendarRepository = UserCalendarRepository;
        _sortingExpressionProvider = sortingExpressionProvider;
    }

    public async Task<PagedResult<UserCalendarViewModel>> Handle(
       GetUserCalendarsByUserIdQuery request,
        CancellationToken cancellationToken)
    {
        var UserCalendarsQuery = _UserCalendarRepository
            .GetAllNoTracking()
            .IgnoreQueryFilters()
            .Include(x => x.User)
            .Where(x => x.UserId == request.UserId&& (request.IncludeDeleted || !x.Deleted) );
        var totalCount = await UserCalendarsQuery.CountAsync(cancellationToken);
        UserCalendarsQuery = UserCalendarsQuery.GetOrderedQueryable(request.SortQuery, _sortingExpressionProvider);

        var UserCalendars = await UserCalendarsQuery
            .Skip((request.PageNumber - 1) * request.PageSize)
            .Take(request.PageSize)
            .Select(UserCalendar => UserCalendarViewModel.FromUserCalendar(UserCalendar))
            .ToListAsync(cancellationToken);

        return new PagedResult<UserCalendarViewModel>(
            totalCount, UserCalendars, request.PageNumber, request.PageSize);
    }
}
