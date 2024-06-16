using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using CleanArchitecture.Application.ViewModels;
using CleanArchitecture.Application.ViewModels.Professors;
using CleanArchitecture.Domain.Constants;
using CleanArchitecture.IntegrationTests.Extensions;
using CleanArchitecture.IntegrationTests.Fixtures;
using FluentAssertions;
using Xunit;
using Xunit.Priority;

namespace CleanArchitecture.IntegrationTests.Controller;

[Collection("IntegrationTests")]
[TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
public sealed class ProfessorControllerTests : IClassFixture<ProfessorTestFixture>
{
    private readonly ProfessorTestFixture _fixture;

    public ProfessorControllerTests(ProfessorTestFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    [Priority(0)]
    public async Task Should_Get_Professor_By_Id()
    {
        var response = await _fixture.ServerClient.GetAsync($"/api/v1/Professor/{_fixture.CreatedProfessorId}");

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var message = await response.Content.ReadAsJsonAsync<ProfessorViewModel>();

        message?.Data.Should().NotBeNull();

        message!.Data!.ProfessorId.Should().Be(_fixture.CreatedProfessorId);
        


    }

    [Fact]
    [Priority(5)]
    public async Task Should_Get_All_Professors()
    {
        var response = await _fixture.ServerClient.GetAsync(
            "api/v1/Professor?searchTerm=Test&pageSize=5&page=1");

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        //var message = await response.Content.ReadAsJsonAsync<PagedResult<ProfessorViewModel>>();

        //message?.Data!.Items.Should().NotBeEmpty();
        //message!.Data!.Items.Should().HaveCount(1);
        //message.Data!.Items
        //    .FirstOrDefault(x => x.ProfessorId == _fixture.CreatedProfessorId)
        //    .Should().NotBeNull();

    }

    [Fact]
    [Priority(10)]
    public async Task Should_Create_Professor()
    {
        var request = new CreateProfessorViewModel(Ids.Seed.UserId, Ids.Seed.ResearchGroupId, false);

        var response = await _fixture.ServerClient.PostAsJsonAsync("/api/v1/Professor", request);

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var message = await response.Content.ReadAsJsonAsync<Guid>();
        var ProfessorId = message?.Data;

        // Check if Professor exists
        var ProfessorResponse = await _fixture.ServerClient.GetAsync($"/api/v1/Professor/{ProfessorId}");
        Console.WriteLine(ProfessorResponse);

        ProfessorResponse.StatusCode.Should().Be(HttpStatusCode.OK);

        var ProfessorMessage = await ProfessorResponse.Content.ReadAsJsonAsync<ProfessorViewModel>();

        ProfessorMessage?.Data.Should().NotBeNull();

        ProfessorMessage!.Data!.ProfessorId.Should().Be(ProfessorId!.Value);
    }

    //[Fact]
    //[Priority(15)]
    //public async Task Should_Update_Professor()
    //{
    //    var request = new UpdateProfessorViewModel(_fixture.CreatedProfessorId, Ids.Seed.UserId, Ids.Seed.ResearchGroupId, false);

    //    var response = await _fixture.ServerClient.PutAsJsonAsync("/api/v1/Professor", request);

    //    response.StatusCode.Should().Be(HttpStatusCode.OK);

    //    var message = await response.Content.ReadAsJsonAsync<UpdateProfessorViewModel>();

    //    message?.Data.Should().NotBeNull();
    //    message!.Data.Should().BeEquivalentTo(request);

    //    Check if Professor is updated
    //   var ProfessorResponse = await _fixture.ServerClient.GetAsync($"/api/v1/Professor/{_fixture.CreatedProfessorId}");

    //    ProfessorResponse.StatusCode.Should().Be(HttpStatusCode.OK);

    //    var ProfessorMessage = await response.Content.ReadAsJsonAsync<ProfessorViewModel>();

    //    ProfessorMessage?.Data.Should().NotBeNull();
    //    ProfessorMessage!.Data!.ProfessorId.Should().Be(Ids.Seed.ProfessorId);

    //}

    [Fact]
    [Priority(20)]
    public async Task Should_Delete_Professor()
    {
        var response = await _fixture.ServerClient.DeleteAsync($"/api/v1/Professor/{_fixture.CreatedProfessorId}");

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        // Check if Professor is deleted
        var ProfessorResponse = await _fixture.ServerClient.GetAsync($"/api/v1/Professor/{_fixture.CreatedProfessorId}");

        ProfessorResponse.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }
    /// <summary>
    /// Caso negativo
    /// </summary>
    /// <returns></returns>
    [Fact]
    [Priority(25)]
    public async Task Should_Not_Get_Non_Existing_Professor()
    {
        var nonExistingId = Guid.NewGuid();
        var response = await _fixture.ServerClient.GetAsync($"/api/v1/Professor/{nonExistingId}");

    }
 }