using System;
using System.Threading.Tasks;
using CleanArchitecture.Application.Interfaces;
using CleanArchitecture.Application.Services;
using CleanArchitecture.Application.ViewModels.Appointments;
using CleanArchitecture.Domain.Commands.Appointments.CreateAppointment;
using CleanArchitecture.Domain.Commands.Appointments.DeleteAppointment;
using CleanArchitecture.Domain.Commands.Appointments.UpdateAppointment;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Interfaces;
using com.sun.tools.corba.se.idl.constExpr;
using Microsoft.Extensions.Caching.Distributed;
using Moq;
using Xunit;
using Times = Moq.Times;

public class AppointmentServiceTests
{
    private readonly Mock<IMediatorHandler> _mediatorHandlerMock;
    private readonly Mock<IDistributedCache> _distributedCacheMock;
    private readonly Mock<INotificationService> _notificationServiceMock;
    private readonly IAppointmentService _appointmentService;

    public AppointmentServiceTests()
    {
        _mediatorHandlerMock = new Mock<IMediatorHandler>();
        _distributedCacheMock = new Mock<IDistributedCache>();
        _notificationServiceMock = new Mock<INotificationService>();

        _appointmentService = new AppointmentService(
            _mediatorHandlerMock.Object,
            _distributedCacheMock.Object,
            _notificationServiceMock.Object);
    }

    [Fact]
    public async Task CreateAppointmentAsync_ShouldSendNotification()
    {
        // Arrange
        var createAppointmentViewModel = new CreateAppointmentViewModel
        {
            professorId = Guid.NewGuid(),
            studentId = Guid.NewGuid(),
            calendarId = Guid.NewGuid(),
            dateTime = DateTime.UtcNow,
            professorProgress = "Initial",
            studentProgress = "Initial"
        };

        // Act
        var appointmentId = await _appointmentService.CreateAppointmentAsync(createAppointmentViewModel);

        // Assert
        _mediatorHandlerMock.Verify(m => m.SendCommandAsync(It.IsAny<CreateAppointmentCommand>()), Times.Once);
        _notificationServiceMock.Verify(n => n.SendAppointmentCreatedNotificationAsync(
            It.Is<Guid>(id => id == appointmentId),
            It.Is<DateTime>(dt => dt == createAppointmentViewModel.dateTime),
            It.IsAny<string>()), Times.Once);
    }

    [Fact]
    public async Task UpdateAppointmentAsync_ShouldSendNotification()
    {
        // Arrange
        var updateAppointmentViewModel = new UpdateAppointmentViewModel
        {
            appointmentId = Guid.NewGuid(),
            professorId = Guid.NewGuid(),
            studentId = Guid.NewGuid(),
            calendarId = Guid.NewGuid(),
            dateTime = DateTime.UtcNow,
            professorProgress = "Progress",
            studentProgress = "Progress"
        };

        // Act
        await _appointmentService.UpdateAppointmentAsync(updateAppointmentViewModel);

        // Assert
        _mediatorHandlerMock.Verify(m => m.SendCommandAsync(It.IsAny<UpdateAppointmentCommand>()), Times.Once);
        _notificationServiceMock.Verify(n => n.SendAppointmentUpdatedNotificationAsync(
            It.Is<Guid>(id => id == updateAppointmentViewModel.appointmentId),
            It.Is<DateTime>(dt => dt == updateAppointmentViewModel.dateTime),
            It.IsAny<string>()), Times.Once);
    }

    [Fact]
    public async Task DeleteAppointmentAsync_ShouldSendNotification()
    {
        // Arrange
        var appointmentId = Guid.NewGuid();

        // Act
        await _appointmentService.DeleteAppointmentAsync(appointmentId);

        // Assert
        _mediatorHandlerMock.Verify(m => m.SendCommandAsync(It.IsAny<DeleteAppointmentCommand>()), Times.Once);
        _notificationServiceMock.Verify(n => n.SendAppointmentDeletedNotificationAsync(
            It.Is<Guid>(id => id == appointmentId),
            It.IsAny<string>()), Times.Once);
    }
}
