using System;
using CleanArchitecture.Domain.Commands.Professors.CreateProfessor;
using CleanArchitecture.Domain.Errors;
using Xunit;

namespace CleanArchitecture.Domain.Tests.CommandHandler.Professor.CreateProfessor;

public sealed class CreateProfessorCommandValidationTests :
    ValidationTestBase<CreateProfessorCommand, CreateProfessorCommandValidation>
{
    public CreateProfessorCommandValidationTests() : base(new CreateProfessorCommandValidation())
    {
    }

    [Fact]
    public void Should_Be_Valid()
    {
        var command = CreateTestCommand();

        ShouldBeValid(command);
    }

    [Fact]
    public void Should_Be_Invalid_For_Empty_Professor_Id()
    {
        var command = CreateTestCommand(Guid.Empty);

        ShouldHaveSingleError(
            command,
            DomainErrorCodes.Professor.EmptyId,
            "Professor id may not be empty");
    }

    //[Fact]
    //public void Should_Be_Invalid_For_Empty_User_Id()
    //{
    //    var command = CreateTestCommand(userId: Guid.Empty);

    //    ShouldHaveSingleError(command, DomainErrorCodes.User.EmptyId);
    //}

    private static CreateProfessorCommand CreateTestCommand(
        Guid? professorId = null,
        Guid? userId = null,
        Guid? researchgroupId = null,
        bool? isCoordinator = null)

    {
        return new CreateProfessorCommand(
            professorId ?? Guid.NewGuid(),
            userId ?? Guid.NewGuid(),
            researchgroupId ?? Guid.NewGuid(),
            isCoordinator ?? false);
    }
}