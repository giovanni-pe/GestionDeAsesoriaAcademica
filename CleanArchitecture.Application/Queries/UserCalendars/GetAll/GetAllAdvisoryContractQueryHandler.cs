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
using System;

namespace CleanArchitecture.Application.Queries.UserCalendars.GetAll;

//public sealed class GetAllUserCalendarsQueryHandler :
//    IRequestHandler<UserCalendarsQuery, PagedResult<UserCalendarViewModel>>
//{
//    private readonly ISortingExpressionProvider<UserCalendarViewModel, UserCalendar> _sortingExpressionProvider;
//    private readonly IUserCalendarRepository _UserCalendarRepository;

//    public GetAllUserCalendarsQueryHandler(
//        IUserCalendarRepository UserCalendarRepository,
//        ISortingExpressionProvider<UserCalendarViewModel, UserCalendar> sortingExpressionProvider)
//    {
//        _UserCalendarRepository = UserCalendarRepository;
//        _sortingExpressionProvider = sortingExpressionProvider;
//    }

//    public async Task<PagedResult<UserCalendarViewModel>> Handle(
//        UserCalendarsQuery request,
//        CancellationToken cancellationToken)
//    {
//        var UserCalendarsQuery = _UserCalendarRepository
//    .GetAllNoTracking()
//    .IgnoreQueryFilters()
//    .Include(x => x.User)
//    .Where(x => request.IncludeDeleted || !x.Deleted);
//  //.Where(x => x.ResearchLineId == (request.researchLineId != Guid.Empty ? request.researchLineId : x.ResearchLineId));



//        if (!string.IsNullOrWhiteSpace(request.SearchTerm))
//        {
//            //  UserCalendarsQuery = UserCalendarsQuery.Where(UserCalendar =>
//            //    UserCalendar.ResearchGroup.Contains(request.SearchTerm));
//        }
//      }

//      }
//        var totalCount = await UserCalendarsQuery.CountAsync(cancellationToken);

//        UserCalendarsQuery = UserCalendarsQuery.GetOrderedQueryable(request.SortQuery, _sortingExpressionProvider);

//        var UserCalendars = await UserCalendarsQuery
//            .Skip((request.Query.Page - 1) * request.Query.PageSize)
//            .Take(request.Query.PageSize)
//            .Select(UserCalendar => UserCalendarViewModel.FromUserCalendar(UserCalendar))
//            .ToListAsync(cancellationToken);

//        return new PagedResult<UserCalendarViewModel>(
//            totalCount, UserCalendars, request.Query.Page, request.Query.PageSize);
//    }
//}
