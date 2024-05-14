using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CleanArchitecture.Domain.Enums;
using CleanArchitecture.Domain.Errors;
using CleanArchitecture.Domain.Interfaces;
using CleanArchitecture.Domain.Interfaces.Repositories;
using CleanArchitecture.Domain.Notifications;
using CleanArchitecture.Shared.Events.Role;
using MediatR;

namespace CleanArchitecture.Domain.Commands.Roles.DeleteRole;

public sealed class DeleteRoleCommandHandler : CommandHandlerBase,
    IRequestHandler<DeleteRoleCommand>
{
    private readonly IRoleRepository _RoleRepository;
    private readonly IUser _user;
    private readonly IUserRepository _userRepository;

    public DeleteRoleCommandHandler(
        IMediatorHandler bus,
        IUnitOfWork unitOfWork,
        INotificationHandler<DomainNotification> notifications,
        IRoleRepository RoleRepository,
        IUserRepository userRepository,
        IUser user) : base(bus, unitOfWork, notifications)
    {
        _RoleRepository = RoleRepository;
        _userRepository = userRepository;
        _user = user;
    }

    public async Task Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
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
                    $"No permission to delete Role {request.AggregateId}",
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

        var RoleUsers = _userRepository
            .GetAll()
            .Where(x => x.RoleId == request.AggregateId);

        _userRepository.RemoveRange(RoleUsers);

        _RoleRepository.Remove(Role);

        if (await CommitAsync())
        {
            await Bus.RaiseEventAsync(new RoleDeletedEvent(Role.Id));
        }
    }
}