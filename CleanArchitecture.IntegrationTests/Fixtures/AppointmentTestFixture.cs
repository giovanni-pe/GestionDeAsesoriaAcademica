using CleanArchitecture.Domain.Commands.Appointments.CreateAppointment;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Infrastructure.Database;
using System;

namespace CleanArchitecture.IntegrationTests.Fixtures;
public sealed class AppointmentTestFixture : TestFixtureBase
{
    public Guid Id { get; } = Guid.NewGuid();
    public Guid ProfessorId { get; } = Guid.NewGuid();
    public Guid StudentId { get; } = Guid.NewGuid();
    public Guid CalendarId { get; } = Guid.NewGuid();

    protected override void SeedTestData(ApplicationDbContext context)
    {
        base.SeedTestData(context);

        context.Appointments.Add(new Appointment(
            Id, ProfessorId, StudentId, CalendarId, DateTime.UtcNow,
            "Estado de la Appointment", 
            "Asunto de la Appointment")); 


        context.SaveChanges();
    }
}
