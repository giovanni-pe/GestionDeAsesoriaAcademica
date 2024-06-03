using System;
using System.Threading;
using System.Threading.Tasks;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Enums;
using CleanArchitecture.Domain.Errors;
using CleanArchitecture.Domain.Interfaces;
using CleanArchitecture.Domain.Interfaces.Repositories;
using CleanArchitecture.Domain.Notifications;
using CleanArchitecture.Shared.Events.Appointment;
using MediatR;

namespace CleanArchitecture.Domain.Commands.Appointments.CreateAppointment;

public sealed class CreateAppointmentCommandHandler : CommandHandlerBase,
    IRequestHandler<CreateAppointmentCommand>
{
    private readonly IAppointmentRepository _AppointmentRepository;
    private readonly IUserRepository _UserRepository;
    private readonly IUser _user;

    public CreateAppointmentCommandHandler(
        IMediatorHandler bus,
        IUnitOfWork unitOfWork,
        INotificationHandler<DomainNotification> notifications,
        IAppointmentRepository AppointmentRepository,
        IUserRepository UserRepository,
        IUser user) : base(bus, unitOfWork, notifications)
    {
        _AppointmentRepository = AppointmentRepository;
        _user = user;
        _UserRepository = UserRepository;
    }

    public async Task Handle(CreateAppointmentCommand request, CancellationToken cancellationToken)
    {
        if (!await TestValidityAsync(request))
        {
            return;
        }

         if (_user.GetUserRole() != UserRole.Admin)
         {
             await NotifyAsync(
                 new DomainNotification(
                     request.MessageType,
                     $"No permission to create Appointment {request.AggregateId}",
                     ErrorCodes.InsufficientPermissions));

             return;
         }

        if (await _AppointmentRepository.ExistsAsync(request.AggregateId))
        {
            await NotifyAsync(
                new DomainNotification(
                    request.MessageType,
                    $"There is already a Appointment with Id {request.AggregateId}",
                    DomainErrorCodes.Appointment.AlreadyExists));

            return;
        }

        var Appointment = new Appointment(
            request.AggregateId ,request.ProfessorId,  request.StudentId, request.CalendarId, request.DateTime , request.ProfessorProgress ,  request.StudentProgress);
        _AppointmentRepository.Add(Appointment);

        if (await CommitAsync())
        {
            await Bus.RaiseEventAsync(new AppointmentCreatedEvent(
               Appointment.Id ,Appointment.ProfessorId, Appointment.StudentId, Appointment.CalendarId, Appointment.DateTime , Appointment.ProfessorProgress , Appointment.StudentProgress));
        }
    }
}