using System.Threading;
using System.Threading.Tasks;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Enums;
using CleanArchitecture.Domain.Errors;
using CleanArchitecture.Domain.Interfaces;
using CleanArchitecture.Domain.Interfaces.Repositories;
using CleanArchitecture.Domain.Notifications;
using CleanArchitecture.Shared.Events.Role;
using MediatR;

namespace CleanArchitecture.Domain.Commands.Roles.CreateRole;

public sealed class CreateRoleCommandHandler : CommandHandlerBase,
    IRequestHandler<CreateRoleCommand>
{
    private readonly IRoleRepository _RoleRepository;
    private readonly IUser _user;

    public CreateRoleCommandHandler(
        IMediatorHandler bus,
        IUnitOfWork unitOfWork,
        INotificationHandler<DomainNotification> notifications,
        IRoleRepository RoleRepository,
        IUser user) : base(bus, unitOfWork, notifications)
    {
        _RoleRepository = RoleRepository;
        _user = user;
    }

    public async Task Handle(CreateRoleCommand request, CancellationToken cancellationToken)
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
                    $"No permission to create Role {request.AggregateId}",
                    ErrorCodes.InsufficientPermissions));

            return;
        }

        if (await _RoleRepository.ExistsAsync(request.AggregateId))
        {
            await NotifyAsync(
                new DomainNotification(
                    request.MessageType,
                    $"There is already a Role with Id {request.AggregateId}",
                    DomainErrorCodes.Role.AlreadyExists));

            return;
        }

        var Role = new Role(
            request.AggregateId,
            request.Name,request.Description);

        _RoleRepository.Add(Role);

        if (await CommitAsync())
        {
            await Bus.RaiseEventAsync(new RoleCreatedEvent(
                Role.Id,
                Role.Name));
        }
    }
}