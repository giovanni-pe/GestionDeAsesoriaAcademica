using System.Threading;
using System.Threading.Tasks;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Enums;
using CleanArchitecture.Domain.Errors;
using CleanArchitecture.Domain.Interfaces;
using CleanArchitecture.Domain.Interfaces.Repositories;
using CleanArchitecture.Domain.Notifications;
using CleanArchitecture.Shared.Events.Asesore;
using MediatR;

namespace CleanArchitecture.Domain.Commands.Asesores.CreateAsesore;

public sealed class CreateAsesoreCommandHandler : CommandHandlerBase,
    IRequestHandler<CreateAsesoreCommand>
{
    private readonly IAsesoreRepository _AsesoreRepository;
    private readonly IUser _user;

    public CreateAsesoreCommandHandler(
        IMediatorHandler bus,
        IUnitOfWork unitOfWork,
        INotificationHandler<DomainNotification> notifications,
        IAsesoreRepository AsesoreRepository,
        IUser user) : base(bus, unitOfWork, notifications)
    {
        _AsesoreRepository = AsesoreRepository;
        _user = user;
    }

    public async Task Handle(CreateAsesoreCommand request, CancellationToken cancellationToken)
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
                    $"No permission to create ResearchGroup {request.AggregateId}",
                    ErrorCodes.InsufficientPermissions));

            return;
        }

        if (await _AsesoreRepository.ExistsAsync(request.AggregateId))
        {
            await NotifyAsync(
                new DomainNotification(
                    request.MessageType,
                    $"There is already a Asesore with Id {request.AggregateId}",
                    DomainErrorCodes.Asesore.AlreadyExists));

            return;
        }

        var Asesore = new Asesore(
            request.AggregateId,
            request.Nombre,request.Apellido);

        _AsesoreRepository.Add(Asesore);

        if (await CommitAsync())
        {
            await Bus.RaiseEventAsync(new AsesoreCreatedEvent(
                Asesore.Id,
                Asesore.Nombre));
        }
    }
}