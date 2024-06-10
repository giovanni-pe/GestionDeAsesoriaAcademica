using System;
using CleanArchitecture.Domain.Commands.Professors.CreateProfessor;
using CleanArchitecture.Domain.Commands.Professors.DeleteProfessor;
using CleanArchitecture.Domain.Enums;
using CleanArchitecture.Domain.Interfaces.Repositories;
using NSubstitute;

namespace CleanArchitecture.Domain.Tests.CommandHandler.Professor.DeleteProfessor;

public sealed class DeleteProfessorCommandTestFixture : CommandHandlerFixtureBase
{
    public DeleteProfessorCommandHandler CommandHandler { get; }

    private IProfessorRepository ProfessorRepository { get; }
    private IUserRepository UserRepository { get; }

    public DeleteProfessorCommandTestFixture()
    {
        ProfessorRepository = Substitute.For<IProfessorRepository>();
        UserRepository = Substitute.For<IUserRepository>();

        CommandHandler = new DeleteProfessorCommandHandler(
            Bus,
            UnitOfWork,
            NotificationHandler,
            ProfessorRepository,
            UserRepository,
            User);
    }

    public Entities.Professor SetupProfessor()
    {
        var Professor = new Entities.Professor(Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), false);
        
        ProfessorRepository
            .GetByIdAsync(Arg.Is<Guid>(y => y == Professor.Id))
            .Returns(Professor);

        return Professor;
    }

    public void SetupUser()
    {
        User.GetUserRole().Returns(UserRole.User);
    }
}