using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CleanArchitecture.Domain.Commands.Appointments.CreateAppointment;
using CleanArchitecture.Domain.Commands;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Errors;
using CleanArchitecture.Domain.Interfaces;
using CleanArchitecture.Domain.Interfaces.Repositories;
using CleanArchitecture.Domain.Notifications;
using CleanArchitecture.Shared.Events.Appointment;
using MediatR;
using CleanArchitecture.Shared.Events.AdvisoryContract;

namespace CleanArchitecture.Domain.Commands.Appointments.CreateAppointment
{
    public sealed class CreateAppointmentCommandHandler : CommandHandlerBase,
        IRequestHandler<CreateAppointmentCommand>
    {
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly IUserRepository _userRepository;
        private readonly ICalendarTokenRepository _calendarTokenRepository;
        private readonly IUser _user;
        private readonly IGoogleCalendarIntegration _googleCalendarIntegration;

        public CreateAppointmentCommandHandler(
            IMediatorHandler bus,
            IUnitOfWork unitOfWork,
            INotificationHandler<DomainNotification> notifications,
            IAppointmentRepository appointmentRepository,
            IUserRepository userRepository,
            ICalendarTokenRepository calendarTokenRepository,
            IUser user,
            IGoogleCalendarIntegration googleCalendarIntegration) : base(bus, unitOfWork, notifications)
        {
            _appointmentRepository = appointmentRepository;
            _user = user;
            _userRepository = userRepository;
            _calendarTokenRepository = calendarTokenRepository;
            _googleCalendarIntegration = googleCalendarIntegration;
        }

        public async Task Handle(CreateAppointmentCommand request, CancellationToken cancellationToken)
        {
            if (!await TestValidityAsync(request))
            {
                return;
            }

            var professorEmail = request.ProfessorEmail;
            var studentEmail = request.StudentEmail;
            var startDatetime = request.StartDateTime;
            var endDatetime = request.EndDateTime;
            var description = request.Description;

            // Obtener el usuario
            var user = await _userRepository.GetByEmailAsync("giovanni.perez@unas.edu.pe");
            if (user == null)
            {
                await NotifyAsync(
                    new DomainNotification(
                        request.MessageType,
                        "User not found",
                        ErrorCodes.UserNotFound));
                return;
            }

            // Crear evento en Google Calendar
            string googleEventId;
            try
            {
                googleEventId = await _googleCalendarIntegration.CreateStudentAppointmentAsync(professorEmail, studentEmail, startDatetime, endDatetime, description);
            }
            catch (Exception ex)
            {
                await NotifyAsync(
                    new DomainNotification(
                        request.MessageType,
                        $"Failed to create Google Calendar event: {ex.Message}",
                        ErrorCodes.CalendarEventCreationFailed));
                return;
            }

            // Crear entidad de cita
            var appointment = new Appointment(
                request.AggregateId,
                request.ProfessorId,
                request.StudentId,
                request.CalendarId,
                request.DateTime,
                request.ProfessorProgress,
                request.StudentProgress,
                request.Status,
                googleEventId
                ,startDatetime,endDatetime,description);

            // Guardar la cita en el repositorio
            _appointmentRepository.Add(appointment);

            if (await CommitAsync())
            {
                await Bus.RaiseEventAsync(new AppointmentCreatedEvent(
                    appointment.Id,
                    appointment.ProfessorId,
                    appointment.StudentId,
                    appointment.CalendarId,
                    appointment.DateTime,
                    appointment.ProfessorProgress,
                    appointment.StudentProgress));
            }
        }
    }
}
