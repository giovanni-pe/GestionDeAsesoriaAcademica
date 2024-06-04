using System;
using System.Threading.Tasks;
using CleanArchitecture.Domain.Commands.AdvisoryContracts.UpdateAdvisoryContract;
using CleanArchitecture.Domain.Errors;
using CleanArchitecture.Shared.Events.AdvisoryContract;
using Xunit;

namespace CleanArchitecture.Domain.Tests.CommandHandler.AdvisoryContract.UpdateAdvisoryContract;

public sealed class UpdateAdvisoryContractCommandHandlerTests
{
    private readonly UpdateAdvisoryContractCommandTestFixture _fixture = new();

   /** [Fact]
    public async Task Should_Update_AdvisoryContract()
    {
        var command = new UpdateAdvisoryContractCommand(
            Guid.NewGuid(),Guid.NewGuid(),
            "AdvisoryContract Name");

        _fixture.SetupExistingAdvisoryContract(command.AggregateId);

        await _fixture.CommandHandler.Handle(command, default);

        _fixture
            .VerifyCommit()
            .VerifyNoDomainNotification()
            .VerifyRaisedEvent<AdvisoryContractUpdatedEvent>(x =>
                x.AggregateId == command.AggregateId &&
                x.ResearchGroupId == command.ResearchGroupId);
    }**/

    [Fact]
    public async Task Should_Not_Update_AdvisoryContract_Insufficient_Permissions()
    {
        var command = new UpdateAdvisoryContractCommand(
            Guid.NewGuid(),Guid.NewGuid(),
            "AdvisoryContract Name");

        _fixture.SetupUser();

        await _fixture.CommandHandler.Handle(command, default);

        _fixture
            .VerifyNoCommit()
            .VerifyNoRaisedEvent<AdvisoryContractUpdatedEvent>()
            .VerifyAnyDomainNotification()
            .VerifyExistingNotification(
                ErrorCodes.InsufficientPermissions,
                $"No permission to update AdvisoryContract {command.AggregateId}");
    }

    [Fact]
    public async Task Should_Not_Update_AdvisoryContract_Not_Existing()
    {
        var command = new UpdateAdvisoryContractCommand(
            Guid.NewGuid(),Guid.NewGuid(),
            "AdvisoryContract Name");

        await _fixture.CommandHandler.Handle(command, default);

        _fixture
            .VerifyNoCommit()
            .VerifyNoRaisedEvent<AdvisoryContractUpdatedEvent>()
            .VerifyAnyDomainNotification()
            .VerifyExistingNotification(
                ErrorCodes.ObjectNotFound,
                $"There is no AdvisoryContract with Id {command.AggregateId}");
    }
}