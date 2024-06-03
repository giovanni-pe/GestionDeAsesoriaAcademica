using System.Threading;
using System.Threading.Tasks;
using CleanArchitecture.Domain.Enums;
using CleanArchitecture.Domain.Errors;
using CleanArchitecture.Domain.Interfaces;
using CleanArchitecture.Domain.Interfaces.Repositories;
using CleanArchitecture.Domain.Notifications;
using CleanArchitecture.Shared.Events.Appointment;
using MediatR;

namespace CleanArchitecture.Domain.Commands.Appointments.UpdateAppointment;

public sealed class UpdateAppointmentCommandHandler : CommandHandlerBase,
    IRequestHandler<UpdateAppointmentCommand, Unit>
{
    private readonly IAppointmentRepository _appointmentRepository;
    private readonly IUser _user;

    public UpdateAppointmentCommandHandler(
        IMediatorHandler bus,
        IUnitOfWork unitOfWork,
        INotificationHandler<DomainNotification> notifications,
        IAppointmentRepository appointmentRepository,
        IUserRepository userRepository,
        IUser user) : base(bus, unitOfWork, notifications)
    {
        _appointmentRepository = appointmentRepository;
        _user = user;
    }
   
    public async Task<Unit> Handle(UpdateAppointmentCommand request, CancellationToken cancellationToken)
    {
        if (!await TestValidityAsync(request))
        {
            return Unit.Value;
        }

        if (_user.GetUserRole() != UserRole.Admin)
        {
            await NotifyAsync(
                new DomainNotification(
                    request.MessageType,
                    $"No permission to update Appointment {request.AggregateId}",
                    DomainErrorCodes.Appointment.InsufficientPermissions));

            return Unit.Value;
        }

        var appointment = await _appointmentRepository.GetByIdAsync(request.AggregateId);

        if (appointment is null)
        {
            await NotifyAsync(
                new DomainNotification(
                    request.MessageType,
                    $"There is no Appointment with Id {request.AggregateId}",
                    DomainErrorCodes.Appointment.NotFound));

            return Unit.Value;
        }

        appointment.SetDateTime(request.DateTime);
        appointment.SetProfessorProgress(request.ProfessorProgress);
        appointment.SetStudentProgress(request.StudentProgress);

        _appointmentRepository.Update(appointment);

        if (await CommitAsync())
        {
            await Bus.RaiseEventAsync(new AppointmentUpdatedEvent(
                appointment.Id,
                appointment.ProfessorId,
                appointment.StudentId,
                appointment.CalendarId,
                appointment.DateTime,
                appointment.ProfessorProgress,
                appointment.StudentProgress));
        }

        return Unit.Value;
    }
}
