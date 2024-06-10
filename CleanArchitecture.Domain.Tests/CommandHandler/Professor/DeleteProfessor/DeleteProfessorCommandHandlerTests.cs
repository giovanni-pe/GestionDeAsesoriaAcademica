 using System;
using System.Threading.Tasks;
using CleanArchitecture.Domain.Commands.Professors.DeleteProfessor;
using CleanArchitecture.Domain.Errors;
using CleanArchitecture.Shared.Events.Professor;
using Xunit;

namespace CleanArchitecture.Domain.Tests.CommandHandler.Professor.DeleteProfessor;

public sealed class DeleteProfessorCommandHandlerTests
{
    private readonly DeleteProfessorCommandTestFixture _fixture = new();

    [Fact]
    public async Task Should_Delete_Professor()
    {
        var Professor = _fixture.SetupProfessor();

        var command = new DeleteProfessorCommand(Professor.Id);

        await _fixture.CommandHandler.Handle(command, default);

        _fixture
            .VerifyNoDomainNotification()
            .VerifyCommit()
            .VerifyRaisedEvent<ProfessorDeletedEvent>(x => x.AggregateId == Professor.Id);
    }

    [Fact]
    public async Task Should_Not_Delete_Non_Existing_Professor()
    {
        _fixture.SetupProfessor();

        var command = new DeleteProfessorCommand(Guid.NewGuid());

        await _fixture.CommandHandler.Handle(command, default);

        _fixture
            .VerifyNoCommit()
            .VerifyNoRaisedEvent<ProfessorDeletedEvent>()
            .VerifyAnyDomainNotification()
            .VerifyExistingNotification(
                ErrorCodes.ObjectNotFound,
                $"There is no Professor with Id {command.AggregateId}");
    }

   
}