using System;
using CleanArchitecture.Domain.Commands.AdvisoryContracts.CreateAdvisoryContract;
using CleanArchitecture.Domain.Enums;
using CleanArchitecture.Domain.Interfaces.Repositories;
using NSubstitute;

namespace CleanArchitecture.Domain.Tests.CommandHandler.AdvisoryContract.CreateAdvisoryContract;

public sealed class CreateAdvisoryContractCommandTestFixture : CommandHandlerFixtureBase
{
    public CreateAdvisoryContractCommandHandler CommandHandler { get; }

    private IAdvisoryContractRepository AdvisoryContractRepository { get; }
    private IResearchGroupRepository ResearchGroupRepository { get; }
    public CreateAdvisoryContractCommandTestFixture()
    {
        AdvisoryContractRepository = Substitute.For<IAdvisoryContractRepository>();
        ResearchGroupRepository = Substitute.For<IResearchGroupRepository>();

        CommandHandler = new CreateAdvisoryContractCommandHandler(
            Bus,
            UnitOfWork,
            NotificationHandler,
            AdvisoryContractRepository,,
            User);
    }

    public void SetupUser()
    {
        User.GetUserRole().Returns(UserRole.User);
    }

    public void SetupExistingAdvisoryContract(Guid id)
    {
        AdvisoryContractRepository
            .ExistsAsync(Arg.Is<Guid>(x => x == id))
            .Returns(true);
    }
}