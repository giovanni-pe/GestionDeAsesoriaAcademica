using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CleanArchitecture.Application.Extensions;
using CleanArchitecture.Application.ViewModels;
using CleanArchitecture.Application.ViewModels.Sorting;
using CleanArchitecture.Application.ViewModels.Appointments;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Interfaces.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Application.Queries.Appointments.GetAll;

public sealed class GetAllAppointmentsQueryHandler :
    IRequestHandler<AppointmentsQuery, PagedResult<AppointmentViewModel>>
{
    private readonly ISortingExpressionProvider<AppointmentViewModel, Appointment> _sortingExpressionProvider;
    private readonly IAppointmentRepository _AppointmentRepository;

    public GetAllAppointmentsQueryHandler(
        IAppointmentRepository AppointmentRepository,
        ISortingExpressionProvider<AppointmentViewModel, Appointment> sortingExpressionProvider)
    {
        _AppointmentRepository = AppointmentRepository;
        _sortingExpressionProvider = sortingExpressionProvider;
    }

    public async Task<PagedResult<AppointmentViewModel>> Handle(
        AppointmentsQuery request,
        CancellationToken cancellationToken)
    {
        var AppointmentsQuery = _AppointmentRepository
    .GetAllNoTracking()
    .IgnoreQueryFilters()
    .Include(x => x.Id) // Include the related entity without the Where clause
    .Where(x => request.IncludeDeleted || !x.Deleted);


        if (!string.IsNullOrWhiteSpace(request.SearchTerm))
        {
            //  AppointmentsQuery = AppointmentsQuery.Where(Appointment =>
            //    Appointment.ResearchGroup.Contains(request.SearchTerm));
        }

        var totalCount = await AppointmentsQuery.CountAsync(cancellationToken);

        AppointmentsQuery = AppointmentsQuery.GetOrderedQueryable(request.SortQuery, _sortingExpressionProvider);

        var Appointments = await AppointmentsQuery
            .Skip((request.Query.Page - 1) * request.Query.PageSize)
            .Take(request.Query.PageSize)
            .Select(Appointment => AppointmentViewModel.FromAppointment(Appointment))
            .ToListAsync(cancellationToken);

        return new PagedResult<AppointmentViewModel>(
            totalCount, Appointments, request.Query.Page, request.Query.PageSize);
    }
}