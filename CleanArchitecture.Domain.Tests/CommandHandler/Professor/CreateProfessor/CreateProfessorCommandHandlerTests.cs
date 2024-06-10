using System;
using System.Threading.Tasks;
using CleanArchitecture.Domain.Commands.Professors.CreateProfessor;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Enums;
using CleanArchitecture.Domain.Errors;
using CleanArchitecture.Shared.Events.Professor;
using NSubstitute;
using Xunit;

namespace CleanArchitecture.Domain.Tests.CommandHandler.Professor.CreateProfessor;

public sealed class CreateProfessorCommandHandlerTests
{
    private readonly CreateProfessorCommandTestFixture _fixture = new();

    [Fact]
    public async Task Should_Create_Professor()
    {

        var command = new CreateProfessorCommand(
            Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), false);

        await _fixture.CommandHandler.Handle(command, default);

        _fixture
            .VerifyNoDomainNotification()
            .VerifyCommit()
            .VerifyRaisedEvent<ProfessorCreatedEvent>(x =>
                x.AggregateId == command.AggregateId &&
                x.UserId == command.UserId &&
                x.ResearchGroupId== command.ResearchGroupId &&
                x.IsCoordinator == command.IsCoordinator);
    }

    [Fact]
    public async Task Should_Not_Create_Professor_Insufficient_Permissions()
    {
        _fixture.SetupUser();

        var command = new CreateProfessorCommand(
            Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), false);

        await _fixture.CommandHandler.Handle(command, default);

        _fixture
            .VerifyNoCommit()
            .VerifyNoRaisedEvent<ProfessorCreatedEvent>()
            .VerifyAnyDomainNotification()
            .VerifyExistingNotification(
                ErrorCodes.InsufficientPermissions,
                $"No permission to create Professor {command.AggregateId}");
    }

    [Fact]
    public async Task Should_Not_Create_Professor_Already_Exists()
    {
        var command = new CreateProfessorCommand(
            Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), false);

        _fixture.SetupExistingProfessor(command.AggregateId);

        await _fixture.CommandHandler.Handle(command, default);

        _fixture
            .VerifyNoCommit()
            .VerifyNoRaisedEvent<ProfessorCreatedEvent>()
            .VerifyAnyDomainNotification()
            .VerifyExistingNotification(
                DomainErrorCodes.Professor.AlreadyExists,
                $"There is already a Professor with Id {command.AggregateId}");
    }
}