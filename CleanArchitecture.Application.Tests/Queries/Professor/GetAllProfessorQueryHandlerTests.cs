using System.Linq;
using System.Threading.Tasks;
using CleanArchitecture.Application.Queries.Professors.GetAll;
using CleanArchitecture.Application.Tests.Fixtures.Queries.Professors;
using CleanArchitecture.Application.ViewModels;
using FluentAssertions;
using Xunit;

namespace CleanArchitecture.Application.Tests.Queries.Professors;

public sealed class GetAllProfessorsQueryHandlerTests
{
    private readonly GetAllProfessorsTestFixture _fixture = new();

    [Fact]
    public async Task Should_Get_Existing_Professor()
    {
        var Professor = _fixture.SetupProfessor();

        var query = new PageQuery
        {
            PageSize = 10,
            Page = 1
        };

        var result = await _fixture.QueryHandler.Handle(
            new ProfessorsQuery(query, false),
            default);

        _fixture.VerifyNoDomainNotification();

        result.PageSize.Should().Be(query.PageSize);
        result.Page.Should().Be(query.Page);
        result.Count.Should().Be(1);

        Professor.Should().BeEquivalentTo(result.Items.First());
    }

    [Fact]
    public async Task Should_Not_Get_Deleted_Professor()
    {
        _fixture.SetupProfessor(true);

        var query = new PageQuery
        {
            PageSize = 10,
            Page = 1
        };

        var result = await _fixture.QueryHandler.Handle(
            new ProfessorsQuery(query, false),
            default);

        result.PageSize.Should().Be(query.PageSize);
        result.Page.Should().Be(query.Page);
        result.Count.Should().Be(0);

        result.Items.Should().HaveCount(0);
    }
}