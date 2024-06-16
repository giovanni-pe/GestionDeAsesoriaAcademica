using System;
using System.Threading.Tasks;
using CleanArchitecture.Application.Interfaces;
using CleanArchitecture.Application.Queries.Appointments.GetAll;
using CleanArchitecture.Application.Queries.Appointments.GetAppointmentById;
using CleanArchitecture.Application.ViewModels;
using CleanArchitecture.Application.ViewModels.Sorting;
using CleanArchitecture.Application.ViewModels.Appointments;
using CleanArchitecture.Domain;
using CleanArchitecture.Domain.Commands.Appointments.CreateAppointment;
using CleanArchitecture.Domain.Commands.Appointments.DeleteAppointment;
using CleanArchitecture.Domain.Commands.Appointments.UpdateAppointment;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Extensions;
using CleanArchitecture.Domain.Interfaces;
using Microsoft.Extensions.Caching.Distributed;
using static CleanArchitecture.Domain.Errors.DomainErrorCodes;
using Appointment = CleanArchitecture.Domain.Entities.Appointment;

namespace CleanArchitecture.Application.Services;

public sealed class AppointmentService : IAppointmentService
{
    private readonly IMediatorHandler _bus;
    private readonly IDistributedCache _distributedCache;

    public AppointmentService(IMediatorHandler bus, IDistributedCache distributedCache)
    {
        _bus = bus;
        _distributedCache = distributedCache;
    }

    public async Task<Guid> CreateAppointmentAsync(CreateAppointmentViewModel Appointment)
    {
        var AppointmentId = Guid.NewGuid();
        await _bus.SendCommandAsync(new CreateAppointmentCommand(
            AppointmentId, Appointment.professorId, Appointment.studentId, Appointment.calendarId,Appointment.dateTime ,  Appointment.professorProgress , Appointment.studentProgress,Appointment.status,Appointment.googleEventId));

        return AppointmentId;
    }

    public async Task UpdateAppointmentAsync(UpdateAppointmentViewModel Appointment)
    {
        await _bus.SendCommandAsync(new UpdateAppointmentCommand(
              Appointment.appointmentId,Appointment.professorId, Appointment.studentId, Appointment.calendarId, Appointment.dateTime, Appointment.professorProgress, Appointment.studentProgress));
    }

    public async Task DeleteAppointmentAsync(Guid AppointmentId)
    {
        await _bus.SendCommandAsync(new DeleteAppointmentCommand(AppointmentId));
    }

    public async Task<AppointmentViewModel?> GetAppointmentByIdAsync(Guid AppointmentId)
    {
        var cachedAppointment = await _distributedCache.GetOrCreateJsonAsync(
            CacheKeyGenerator.GetEntityCacheKey<Appointment>(AppointmentId),
            async () => await _bus.QueryAsync(new GetAppointmentByIdQuery(AppointmentId)),
            new DistributedCacheEntryOptions
            {
                SlidingExpiration = TimeSpan.FromDays(3),
                AbsoluteExpiration = DateTimeOffset.Now.AddDays(30)
            });

        return cachedAppointment;
    }

    public async Task<PagedResult<AppointmentViewModel>> GetAllAppointmentsAsync(
        PageQuery query,
        bool includeDeleted,
        string searchTerm = "",
        SortQuery? sortQuery = null)
    {
        return await _bus.QueryAsync(new AppointmentsQuery(query, includeDeleted, searchTerm, sortQuery));
    }
}