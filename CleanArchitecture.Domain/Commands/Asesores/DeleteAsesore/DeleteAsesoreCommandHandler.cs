using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CleanArchitecture.Domain.Enums;
using CleanArchitecture.Domain.Errors;
using CleanArchitecture.Domain.Interfaces;
using CleanArchitecture.Domain.Interfaces.Repositories;
using CleanArchitecture.Domain.Notifications;
using CleanArchitecture.Shared.Events.Asesore;
using MediatR;

namespace CleanArchitecture.Domain.Commands.Asesores.DeleteAsesore;

public sealed class DeleteAsesoreCommandHandler : CommandHandlerBase,
    IRequestHandler<DeleteAsesoreCommand>
{
    private readonly IAsesoreRepository _AsesoreRepository;
   // private readonly IReserchLine _user;
    private readonly IUserRepository _userRepository;

    public DeleteAsesoreCommandHandler(
        IMediatorHandler bus,
        IUnitOfWork unitOfWork,
        INotificationHandler<DomainNotification> notifications,
        IAsesoreRepository AsesoreRepository,
        IUserRepository userRepository,
        IUser user) : base(bus, unitOfWork, notifications)
    {
        _AsesoreRepository = AsesoreRepository;
      //  _userRepository = userRepository;
       // _user = user;
    }

    public async Task Handle(DeleteAsesoreCommand request, CancellationToken cancellationToken)
    {
        if (!await TestValidityAsync(request))
        {
            return;
        }
/*
        if (_user.GetUserRole() != UserRole.Admin)
        {
            await NotifyAsync(
                new DomainNotification(
                    request.MessageType,
                    $"No permission to delete ResearchGroup {request.AggregateId}",
                    ErrorCodes.InsufficientPermissions));

            return;
        }*/

        var Asesore = await _AsesoreRepository.GetByIdAsync(request.AggregateId);

        if (Asesore is null)
        {
            await NotifyAsync(
                new DomainNotification(
                    request.MessageType,
                    $"There is no Asesore with Id {request.AggregateId}",
                    ErrorCodes.ObjectNotFound));

            return;
        }
        /*
                var ResearchGroupUsers = _userRepository
                    .GetAll()
                    .Where(x => x.ResearchGroupId == request.AggregateId);

                _userRepository.RemoveRange(ResearchGroupUsers);*/

        _AsesoreRepository.Remove(Asesore);

        if (await CommitAsync())
        {
            await Bus.RaiseEventAsync(new AsesoreDeletedEvent(Asesore.Id));
        }
    }
}