using CleanArchitecture.Application.Queries.Professors.GetProfessorById;
using CleanArchitecture.Application.Tests.Fixtures.Queries.Professors;
using CleanArchitecture.Domain.Errors;
using FluentAssertions;
using System.Threading.Tasks;
using Xunit;

namespace CleanArchitecture.Application.Tests.Queries.Professors;

public sealed class GetProfessorByIdQueryHandlerTests
{
    private readonly GetProfessorByIdTestFixture _fixture = new();

    //[Fact]
    //public async Task Should_Get_Existing_Professor()
    //{
    //    var Professor = _fixture.SetupProfessor();

    //    var result = await _fixture.QueryHandler.Handle(
    //        new GetProfessorByIdQuery(Professor.Id),
    //        default);

    //    _fixture.VerifyNoDomainNotification();

    //    Professor.Should().BeEquivalentTo(result);
    //}

    [Fact]
    public async Task Should_Not_Get_Deleted_Professor()
    {
        var Professor = _fixture.SetupProfessor(true);

        var result = await _fixture.QueryHandler.Handle(
            new GetProfessorByIdQuery(Professor.Id),
            default);

        _fixture.VerifyExistingNotification(
            nameof(GetProfessorByIdQuery),
            ErrorCodes.ObjectNotFound,
            $"Professor with id {Professor.Id} could not be found");
        result.Should().BeNull();
    }
}