using System;
using CleanArchitecture.Domain.Commands.Professors.CreateProfessor;
using CleanArchitecture.Domain.Enums;
using CleanArchitecture.Domain.Interfaces;
using CleanArchitecture.Domain.Interfaces.Repositories;
using CleanArchitecture.Domain.Notifications;
using MediatR;
using NSubstitute;

namespace CleanArchitecture.Domain.Tests.CommandHandler.Professor.CreateProfessor;

public sealed class CreateProfessorCommandTestFixture : CommandHandlerFixtureBase
{
    public CreateProfessorCommandHandler CommandHandler { get; }
    private IProfessorRepository ProfessorRepository { get; }
    public IUserRepository UserRepository { get; }
    private IResearchGroupRepository ResearchGroupRepository { get; }


    public CreateProfessorCommandTestFixture()
    {
        ProfessorRepository = Substitute.For<IProfessorRepository>();
        UserRepository = Substitute.For<IUserRepository>();
        ResearchGroupRepository = Substitute.For<IResearchGroupRepository>();

        CommandHandler = new CreateProfessorCommandHandler(
            Bus,
            UnitOfWork,
            NotificationHandler,
            ProfessorRepository,
            UserRepository,
            ResearchGroupRepository,
            User);
    }

    public void SetupUser()
    {
        User.GetUserRole().Returns(UserRole.User);
    }
    public void SetupExistingProfessor(Guid professorid)
    {
        ProfessorRepository
            .ExistsAsync(Arg.Is<Guid>(x => x == professorid))
            .Returns(true);
    }
}