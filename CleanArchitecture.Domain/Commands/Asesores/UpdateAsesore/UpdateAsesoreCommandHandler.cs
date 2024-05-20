using System.Threading;
using System.Threading.Tasks;
using CleanArchitecture.Domain.Enums;
using CleanArchitecture.Domain.Errors;
using CleanArchitecture.Domain.Interfaces;
using CleanArchitecture.Domain.Interfaces.Repositories;
using CleanArchitecture.Domain.Notifications;
using CleanArchitecture.Shared.Events.Asesore;
using MediatR;

namespace CleanArchitecture.Domain.Commands.Asesores.UpdateAsesore;

public sealed class UpdateAsesoreCommandHandler : CommandHandlerBase,
    IRequestHandler<UpdateAsesoreCommand>
{
    private readonly IAsesoreRepository _AsesoreRepository;
    private readonly IUser _user;

    public UpdateAsesoreCommandHandler(
        IMediatorHandler bus,
        IUnitOfWork unitOfWork,
        INotificationHandler<DomainNotification> notifications,
        IAsesoreRepository AsesoreRepository,
        IUser user) : base(bus, unitOfWork, notifications)
    {
        _AsesoreRepository = AsesoreRepository;
        _user = user;
    }

    public async Task Handle(UpdateAsesoreCommand request, CancellationToken cancellationToken)
    {
        if (!await TestValidityAsync(request))
        {
            return;
        }

        if (_user.GetUserRole() != UserRole.Admin)
        {
            await NotifyAsync(
                new DomainNotification(
                    request.MessageType,
                    $"No permission to update Asesore {request.AggregateId}",
                    ErrorCodes.InsufficientPermissions));

            return;
        }

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

        Asesore.SetNombre(request.Nombre);

        if (await CommitAsync())
        {
            await Bus.RaiseEventAsync(new AsesoreUpdatedEvent(
                Asesore.Id,
                Asesore.Nombre));
        }
    }
}