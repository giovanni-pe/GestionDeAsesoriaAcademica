using System;
using CleanArchitecture.Domain.Commands.Professors.CreateProfessor;
using CleanArchitecture.Domain.Commands.Professors.UpdateProfessor;
using CleanArchitecture.Domain.Errors;
using Xunit;

namespace CleanArchitecture.Domain.Tests.CommandHandler.Professor.UpdateProfessor;

public sealed class UpdateProfessorCommandValidationTests :
    ValidationTestBase<UpdateProfessorCommand, UpdateProfessorCommandValidation>
{
    public UpdateProfessorCommandValidationTests() : base(new UpdateProfessorCommandValidation())
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
    //public void Should_Be_Invalid_For_Empty_Professor_Name()
    //{
    //    var command = CreateTestCommand(name: "");

    //    ShouldHaveSingleError(
    //        command,
    //        DomainErrorCodes.Professor.EmptyName,
    //        "Name may not be empty");
    //}

    private static UpdateProfessorCommand CreateTestCommand(
        Guid? professorId = null,
        Guid? userId = null,
        Guid? researchgroupId = null,
        bool? isCoordinator = null)
    {
        return new UpdateProfessorCommand(
            professorId ?? Guid.NewGuid(),
            userId ?? Guid.NewGuid(),
            researchgroupId ?? Guid.NewGuid(),
            isCoordinator ?? false);
    }

}