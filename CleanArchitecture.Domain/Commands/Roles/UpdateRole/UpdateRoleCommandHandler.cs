using System.Threading;
using System.Threading.Tasks;
using CleanArchitecture.Domain.Enums;
using CleanArchitecture.Domain.Errors;
using CleanArchitecture.Domain.Interfaces;
using CleanArchitecture.Domain.Interfaces.Repositories;
using CleanArchitecture.Domain.Notifications;
using CleanArchitecture.Shared.Events.Role;
using MediatR;

namespace CleanArchitecture.Domain.Commands.Roles.UpdateRole;

public sealed class UpdateRoleCommandHandler : CommandHandlerBase,
    IRequestHandler<UpdateRoleCommand>
{
    private readonly IRoleRepository _RoleRepository;
    private readonly IUser _user;

    public UpdateRoleCommandHandler(
        IMediatorHandler bus,
        IUnitOfWork unitOfWork,
        INotificationHandler<DomainNotification> notifications,
        IRoleRepository RoleRepository,
        IUser user) : base(bus, unitOfWork, notifications)
    {
        _RoleRepository = RoleRepository;
        _user = user;
    }

    public async Task Handle(UpdateRoleCommand request, CancellationToken cancellationToken)
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
                    $"No permission to update Role {request.AggregateId}",
                    ErrorCodes.InsufficientPermissions));

            return;
        }

        var Role = await _RoleRepository.GetByIdAsync(request.AggregateId);

        if (Role is null)
        {
            await NotifyAsync(
                new DomainNotification(
                    request.MessageType,
                    $"There is no Role with Id {request.AggregateId}",
                    ErrorCodes.ObjectNotFound));

            return;
        }

        Role.SetName(request.Name);

        if (await CommitAsync())
        {
            await Bus.RaiseEventAsync(new RoleUpdatedEvent(
                Role.Id,
                Role.Name,Role.Description));
        }
    }
}