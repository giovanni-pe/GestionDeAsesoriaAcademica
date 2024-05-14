using System.Threading;
using System.Threading.Tasks;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Shared.Events.Role;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;

namespace CleanArchitecture.Domain.EventHandler;

public sealed class RoleEventHandler :
    INotificationHandler<RoleCreatedEvent>,
    INotificationHandler<RoleDeletedEvent>,
    INotificationHandler<RoleUpdatedEvent>
{
    private readonly IDistributedCache _distributedCache;

    public RoleEventHandler(IDistributedCache distributedCache)
    {
        _distributedCache = distributedCache;
    }

    public Task Handle(RoleCreatedEvent notification, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }

    public async Task Handle(RoleDeletedEvent notification, CancellationToken cancellationToken)
    {
        await _distributedCache.RemoveAsync(
            CacheKeyGenerator.GetEntityCacheKey<Role>(notification.AggregateId),
            cancellationToken);
    }

    public async Task Handle(RoleUpdatedEvent notification, CancellationToken cancellationToken)
    {
        await _distributedCache.RemoveAsync(
            CacheKeyGenerator.GetEntityCacheKey<Role>(notification.AggregateId),
            cancellationToken);
    }
}