using System;
using CleanArchitecture.Domain.Commands.AdvisoryContracts.CreateAdvisoryContract;
using CleanArchitecture.Domain.Errors;
using Xunit;

namespace CleanArchitecture.Domain.Tests.CommandHandler.AdvisoryContract.CreateAdvisoryContract;

public sealed class CreateAdvisoryContractCommandValidationTests :
    ValidationTestBase<CreateAdvisoryContractCommand, CreateAdvisoryContractCommandValidation>
{
    public CreateAdvisoryContractCommandValidationTests() : base(new CreateAdvisoryContractCommandValidation())
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
            DomainErrorCodes.AdvisoryContract.EmptyCode,
            "Name may not be empty");
    }

    private static CreateAdvisoryContractCommand CreateTestCommand(
        Guid? id = null,
        string? name = null)
    {
        return new CreateAdvisoryContractCommand(
            id ?? Guid.NewGuid(), id ?? Guid.NewGuid(), id ?? Guid.NewGuid(), id ?? Guid.NewGuid(),
            name ?? "Test AdvisoryContract","testcode");
    }
}