using System;
using CleanArchitecture.Domain.Commands.Appointments.CreateAppointment;
using CleanArchitecture.Domain.Enums;
using CleanArchitecture.Domain.Interfaces;
using CleanArchitecture.Domain.Interfaces.Repositories;
using NSubstitute;

namespace CleanArchitecture.Domain.Tests.CommandHandler.Appointment.CreateAppointment
{
    public sealed class CreateAppointmentCommandTestFixture : CommandHandlerFixtureBase
    {
        public CreateAppointmentCommandHandler CommandHandler { get; }

        private IAppointmentRepository AppointmentRepository { get; }
        private ICalendarTokenRepository CalendarTokenRepository { get; }
        private IGoogleCalendarIntegration CalendarIntegration { get; }
        private IUserRepository UserRepository { get; }
        

        public CreateAppointmentCommandTestFixture()
        {
            AppointmentRepository = Substitute.For<IAppointmentRepository>();
            UserRepository = Substitute.For<IUserRepository>();
            CalendarTokenRepository= Substitute.For<ICalendarTokenRepository>();
            CalendarIntegration= Substitute.For<IGoogleCalendarIntegration>();

            CommandHandler = new CreateAppointmentCommandHandler(
                Bus,
                UnitOfWork,
                NotificationHandler,
                AppointmentRepository,
                UserRepository,CalendarTokenRepository,
                User,CalendarIntegration);
        }

        public void SetupUser()
        {
            User.GetUserRole().Returns(UserRole.User);
        }

        public void SetupExistingAppointment(Guid id)
        {
            AppointmentRepository
                .ExistsAsync(Arg.Is<Guid>(x => x == id))
                .Returns(true);
        }
    }
}
