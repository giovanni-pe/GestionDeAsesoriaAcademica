using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using CleanArchitecture.Application.ViewModels;
using CleanArchitecture.Application.ViewModels.Appointments;
using CleanArchitecture.IntegrationTests.Extensions;
using CleanArchitecture.IntegrationTests.Fixtures;
using FluentAssertions;
using Xunit;
using Xunit.Priority;

namespace CleanArchitecture.IntegrationTests.Controller;

[Collection("IntegrationTests")]
[TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
public sealed class AppointmentControllerTests : IClassFixture<AppointmentTestFixture>
{
    private readonly AppointmentTestFixture _fixture;

    public AppointmentControllerTests(AppointmentTestFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    [Priority(0)]
    public async Task Should_Get_Appointment_By_Id()
    {
        var response = await _fixture.ServerClient.GetAsync($"/api/v1/Appointment/{_fixture.Id}");

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var message = await response.Content.ReadAsJsonAsync<AppointmentViewModel>();

        message?.Data.Should().NotBeNull();

        message!.Data!.Id.Should().Be(_fixture.Id);
        message.Data.ProfessorId.Should().Be(_fixture.ProfessorId);
        message.Data.StudentId.Should().Be(_fixture.StudentId);
        message.Data.CalendarId.Should().Be(_fixture.CalendarId);
        message.Data.DateTime.Should().Be(DateTime.UtcNow);
        message.Data.ProfessorProgress.Should().Be("Estado de la Appointment");
        message.Data.StudentProgress.Should().Be("Asunto de la Appointment");
        
    }

    [Fact]
    [Priority(5)]
    public async Task Should_Get_All_Appointments()
    {
        var response = await _fixture.ServerClient.GetAsync(
            "api/v1/Appointment?searchTerm=Test&pageSize=5&page=1");

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var message = await response.Content.ReadAsJsonAsync<PagedResult<AppointmentViewModel>>();

        message?.Data!.Items.Should().NotBeEmpty();
        message!.Data!.Items.Should().HaveCount(1);
        message.Data!.Items
            .FirstOrDefault(x => x.Id == _fixture.Id)
            .Should().NotBeNull();

    }

    [Fact]
    [Priority(10)]
    public async Task Should_Create_Appointment()
    {
        var request = new CreateAppointmentViewModel(_fixture.Id, _fixture.ProfessorId, _fixture.StudentId, _fixture.CalendarId, DateTime.UtcNow,
            "Estado de la Appointment", // Estado de la Appointment
            "Asunto de la Appointment");


        var response = await _fixture.ServerClient.PostAsJsonAsync("/api/v1/Appointment", request);

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var message = await response.Content.ReadAsJsonAsync<Guid>();
        var AppointmentId = message?.Data;

        // Check if Appointment exists
        var AppointmentResponse = await _fixture.ServerClient.GetAsync($"/api/v1/Appointment/{AppointmentId}");

        AppointmentResponse.StatusCode.Should().Be(HttpStatusCode.OK);

        var AppointmentMessage = await AppointmentResponse.Content.ReadAsJsonAsync<AppointmentViewModel>();

        AppointmentMessage?.Data.Should().NotBeNull();

        AppointmentMessage!.Data!.Id.Should().Be(AppointmentId!.Value);
        AppointmentMessage.Data.Id.Should().Be(request.appointmentId);
        AppointmentMessage.Data.ProfessorId.Should().Be(request.professorId);
        AppointmentMessage.Data.StudentId.Should().Be(request.studentId);
        AppointmentMessage.Data.CalendarId.Should().Be(request.calendarId);
        AppointmentMessage.Data.ProfessorProgress.Should().Be(request.professorProgress);
        AppointmentMessage.Data.StudentProgress.Should().Be(request.studentProgress);
    }

    [Fact]
    [Priority(15)]
    public async Task Should_Update_Appointment()
    {
        var request = new UpdateAppointmentViewModel(_fixture.Id, _fixture.ProfessorId, _fixture.StudentId, _fixture.CalendarId, DateTime.UtcNow,
            "Estado de la Appointment", // Estado de la Appointment
            "Asunto de la Appointment");

        var response = await _fixture.ServerClient.PutAsJsonAsync("/api/v1/Appointment", request);

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var message = await response.Content.ReadAsJsonAsync<UpdateAppointmentViewModel>();

        message?.Data.Should().NotBeNull();
        message!.Data.Should().BeEquivalentTo(request);

        // Check if Appointment is updated
        var AppointmentResponse = await _fixture.ServerClient.GetAsync($"/api/v1/Appointment/{_fixture.Id}");

        AppointmentResponse.StatusCode.Should().Be(HttpStatusCode.OK);

        var AppointmentMessage = await response.Content.ReadAsJsonAsync<AppointmentViewModel>();

        AppointmentMessage?.Data.Should().NotBeNull();

        AppointmentMessage!.Data!.Id.Should().Be(_fixture.Id);
        AppointmentMessage.Data.Id.Should().Be(request.appointmentId);
        AppointmentMessage.Data.ProfessorId.Should().Be(request.professorId);
        AppointmentMessage.Data.StudentId.Should().Be(request.studentId);
        AppointmentMessage.Data.CalendarId.Should().Be(request.calendarId);
        AppointmentMessage.Data.ProfessorProgress.Should().Be(request.professorProgress);
        AppointmentMessage.Data.StudentProgress.Should().Be(request.studentProgress);
    }

    [Fact]
    [Priority(20)]
    public async Task Should_Delete_Appointment()
    {
        var response = await _fixture.ServerClient.DeleteAsync($"/api/v1/Appointment/{_fixture.Id}");

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        // Check if Appointment is deleted
        var AppointmentResponse = await _fixture.ServerClient.GetAsync($"/api/v1/Appointment/{_fixture.Id}");

        AppointmentResponse.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }
}