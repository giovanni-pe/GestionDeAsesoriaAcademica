using System;
using System.Threading.Tasks;
using CleanArchitecture.Domain.Commands.Professors.CreateProfessor;
using CleanArchitecture.Domain.Commands.Professors.UpdateProfessor;
using CleanArchitecture.Domain.Errors;
using CleanArchitecture.Shared.Events.Professor;
using Xunit;

namespace CleanArchitecture.Domain.Tests.CommandHandler.Professor.UpdateProfessor;

public sealed class UpdateProfessorCommandHandlerTests
{
    private readonly UpdateProfessorCommandTestFixture _fixture = new();

    [Fact]
    public async Task Should_Update_Professor()
    {
        var command = new UpdateProfessorCommand(
            Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), false);

        _fixture.SetupExistingProfessor(command.AggregateId);

        await _fixture.CommandHandler.Handle(command, default);

        _fixture
            .VerifyCommit()
            .VerifyNoDomainNotification()
            .VerifyRaisedEvent<ProfessorUpdatedEvent>(x =>
                x.AggregateId == command.AggregateId);
    }

    [Fact]
    public async Task Should_Not_Update_Professor_Insufficient_Permissions()
    {
        var command = new UpdateProfessorCommand(
            Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), false);

        _fixture.SetupUser();

        await _fixture.CommandHandler.Handle(command, default);

        _fixture
            .VerifyNoCommit()
            .VerifyNoRaisedEvent<ProfessorUpdatedEvent>()
            .VerifyAnyDomainNotification()
            .VerifyExistingNotification(
                ErrorCodes.InsufficientPermissions,
                $"No permission to update Professor {command.AggregateId}");
    }

    [Fact]
    public async Task Should_Not_Update_Professor_Not_Existing()
    {
        var command = new UpdateProfessorCommand(
            Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), false);

        await _fixture.CommandHandler.Handle(command, default);

        _fixture
            .VerifyNoCommit()
            .VerifyNoRaisedEvent<ProfessorUpdatedEvent>()
            .VerifyAnyDomainNotification()
            .VerifyExistingNotification(
                ErrorCodes.ObjectNotFound,
                $"There is no Professor with Id {command.AggregateId}");
    }
}