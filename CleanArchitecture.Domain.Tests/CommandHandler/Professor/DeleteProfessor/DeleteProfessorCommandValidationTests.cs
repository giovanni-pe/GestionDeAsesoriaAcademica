using System;
using CleanArchitecture.Domain.Commands.Professors.DeleteProfessor;
using CleanArchitecture.Domain.Errors;
using Xunit;

namespace CleanArchitecture.Domain.Tests.CommandHandler.Professor.DeleteProfessor;

public sealed class DeleteProfessorCommandValidationTests :
    ValidationTestBase<DeleteProfessorCommand, DeleteProfessorCommandValidation>
{
    public DeleteProfessorCommandValidationTests() : base(new DeleteProfessorCommandValidation())
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

    private static DeleteProfessorCommand CreateTestCommand(Guid? ProfessorId = null)
    {
        return new DeleteProfessorCommand(ProfessorId ?? Guid.NewGuid());
    }
}