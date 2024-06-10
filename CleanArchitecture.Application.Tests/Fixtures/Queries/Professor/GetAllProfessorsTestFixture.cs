using System;
using System.Collections.Generic;
using CleanArchitecture.Application.Queries.Professors.GetAll;
using CleanArchitecture.Application.SortProviders;
using CleanArchitecture.Domain.Constants;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Interfaces.Repositories;
using MockQueryable.NSubstitute;
using NSubstitute;

namespace CleanArchitecture.Application.Tests.Fixtures.Queries.Professors;

public sealed class GetAllProfessorsTestFixture : QueryHandlerBaseFixture
{
    public GetAllProfessorsQueryHandler QueryHandler { get; }
    private IProfessorRepository ProfessorRepository { get; }

    public GetAllProfessorsTestFixture()
    {
        ProfessorRepository = Substitute.For<IProfessorRepository>();
        var sortingProvider = new ProfessorViewModelSortProvider();

        QueryHandler = new GetAllProfessorsQueryHandler(ProfessorRepository, sortingProvider);
    }

    public Professor SetupProfessor(bool deleted = false)
    {
        var Professor = new Professor(Ids.Seed.ProfessorId, Ids.Seed.UserId, Ids.Seed.ResearchGroupId, false);

        if (deleted)
        {
            Professor.Delete();
        }

        var ProfessorList = new List<Professor> { Professor }.BuildMock();
        ProfessorRepository.GetAllNoTracking().Returns(ProfessorList);

        return Professor;
    }
}