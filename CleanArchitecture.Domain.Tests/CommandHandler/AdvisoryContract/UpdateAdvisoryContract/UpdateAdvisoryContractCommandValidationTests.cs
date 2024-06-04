using System;
using CleanArchitecture.Domain.Commands.AdvisoryContracts.UpdateAdvisoryContract;
using CleanArchitecture.Domain.Errors;
using Xunit;

namespace CleanArchitecture.Domain.Tests.CommandHandler.AdvisoryContract.UpdateAdvisoryContract;

public sealed class UpdateAdvisoryContractCommandValidationTests :
    ValidationTestBase<UpdateAdvisoryContractCommand, UpdateAdvisoryContractCommandValidation>
{
    public UpdateAdvisoryContractCommandValidationTests() : base(new UpdateAdvisoryContractCommandValidation())
    {
    }

    [Fact]
    public void Should_Be_Valid()
    {
        var command = CreateTestCommand();

        ShouldBeValid(command);
    }

    [Fact]
    public void Should_Be_Invalid_For_Empty_AdvisoryContract_Id()
    {
        var command = CreateTestCommand(Guid.Empty);

        ShouldHaveSingleError(
            command,
            DomainErrorCodes.AdvisoryContract.EmptyId,
            "AdvisoryContract id may not be empty");
    }

    [Fact]
    public void Should_Be_Invalid_For_Empty_AdvisoryContract_Name()
    {
        var command = CreateTestCommand(name: "");

        ShouldHaveSingleError(
            command,
            DomainErrorCodes.AdvisoryContract.EmptyName,
            "Name may not be empty");
    }

    private static UpdateAdvisoryContractCommand CreateTestCommand(
        Guid? id = null,
        string? name = null)
    {
        return new UpdateAdvisoryContractCommand(
            id ?? Guid.NewGuid(), id ?? Guid.NewGuid(),
            name ?? "Test AdvisoryContract");
    }
}