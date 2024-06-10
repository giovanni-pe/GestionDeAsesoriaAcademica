using System;
using CleanArchitecture.Application.Queries.Professors.GetProfessorById;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Interfaces.Repositories;
using NSubstitute;

namespace CleanArchitecture.Application.Tests.Fixtures.Queries.Professors;

public sealed class GetProfessorByIdTestFixture : QueryHandlerBaseFixture
{
    public GetProfessorByIdQueryHandler QueryHandler { get; }
    private IProfessorRepository ProfessorRepository { get; }

    public GetProfessorByIdTestFixture()
    {
        ProfessorRepository = Substitute.For<IProfessorRepository>();

        QueryHandler = new GetProfessorByIdQueryHandler(
            ProfessorRepository,
            Bus);
    }

    public Professor SetupProfessor(bool deleted = false)
    {
        var Professor = new Professor(Guid.NewGuid(), "Professor 1", Guid.NewGuid(),"sw123");

        if (deleted)
        {
            Professor.Delete();
        }
        else
        {
            ProfessorRepository.GetByIdAsync(Arg.Is<Guid>(y => y == Professor.Id)).Returns(Professor);
        }


        return Professor;
    }
}