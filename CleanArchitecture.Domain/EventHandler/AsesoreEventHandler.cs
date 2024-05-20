using System.Threading;
using System.Threading.Tasks;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Shared.Events.Asesore;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;

namespace CleanArchitecture.Domain.EventHandler;

public sealed class AsesoreEventHandler :
    INotificationHandler<AsesoreCreatedEvent>,
    INotificationHandler<AsesoreDeletedEvent>,
    INotificationHandler<AsesoreUpdatedEvent>
{
    private readonly IDistributedCache _distributedCache;

    public AsesoreEventHandler(IDistributedCache distributedCache)
    {
        _distributedCache = distributedCache;
    }

    public Task Handle(AsesoreCreatedEvent notification, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }

    public async Task Handle(AsesoreDeletedEvent notification, CancellationToken cancellationToken)
    {
        await _distributedCache.RemoveAsync(
            CacheKeyGenerator.GetEntityCacheKey<Asesore>(notification.AggregateId),
            cancellationToken);
    }

    public async Task Handle(AsesoreUpdatedEvent notification, CancellationToken cancellationToken)
    {
        await _distributedCache.RemoveAsync(
            CacheKeyGenerator.GetEntityCacheKey<Asesore>(notification.AggregateId),
            cancellationToken);
    }
}