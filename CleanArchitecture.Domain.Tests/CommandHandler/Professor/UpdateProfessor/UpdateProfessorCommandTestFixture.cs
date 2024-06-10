using System;
using CleanArchitecture.Domain.Commands.Professors.UpdateProfessor;
using CleanArchitecture.Domain.Enums;
using CleanArchitecture.Domain.Interfaces.Repositories;
using NSubstitute;

namespace CleanArchitecture.Domain.Tests.CommandHandler.Professor.UpdateProfessor;

public sealed class UpdateProfessorCommandTestFixture : CommandHandlerFixtureBase
{
    public UpdateProfessorCommandHandler CommandHandler { get; }

    private IProfessorRepository ProfessorRepository { get; }

    public UpdateProfessorCommandTestFixture()
    {
        ProfessorRepository = Substitute.For<IProfessorRepository>();

        CommandHandler = new UpdateProfessorCommandHandler(
            Bus,
            UnitOfWork,
            NotificationHandler,
            ProfessorRepository,
            User);
    }

    public void SetupUser()
    {
        User.GetUserRole().Returns(UserRole.User);
    }

    public void SetupExistingProfessor(Guid professorid)
    {
        ProfessorRepository
            .GetByIdAsync(Arg.Is<Guid>(x => x == professorid))
            .Returns(new Entities.Professor(professorid, Guid.NewGuid(), Guid.NewGuid(), false));
    }
}