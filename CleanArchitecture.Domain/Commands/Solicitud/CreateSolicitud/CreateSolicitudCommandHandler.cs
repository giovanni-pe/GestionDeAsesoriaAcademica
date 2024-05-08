using System.Threading;
using System.Threading.Tasks;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Enums;
using CleanArchitecture.Domain.Errors;
using CleanArchitecture.Domain.Interfaces;
using CleanArchitecture.Domain.Interfaces.Repositories;
using CleanArchitecture.Domain.Notifications;
using MediatR;

namespace CleanArchitecture.Domain.Commands.Solicituds.CreateSolicitud;

public sealed class CreateSolicitudCommandHandler : CommandHandlerBase,
    IRequestHandler<CreateSolicitudCommand>
{
    private readonly ISolicitudRepository _SolicitudRepository;
    private readonly IUser _user;

    public CreateSolicitudCommandHandler(
        IMediatorHandler bus,
        IUnitOfWork unitOfWork,
        INotificationHandler<DomainNotification> notifications,
        ISolicitudRepository SolicitudRepository,
        IUser user) : base(bus, unitOfWork, notifications)
    {
        _SolicitudRepository = SolicitudRepository;
        _user = user;
    }

    public async Task Handle(CreateSolicitudCommand request, CancellationToken cancellationToken)
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
                    $"No permission to create Solicitud {request.AggregateId}",
                    ErrorCodes.InsufficientPermissions));

            return;
        }

        
    }
}