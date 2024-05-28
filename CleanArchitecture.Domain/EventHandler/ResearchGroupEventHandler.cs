using System.Threading;
using System.Threading.Tasks;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Shared.Events.ResearchGroup;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;

namespace CleanArchitecture.Domain.EventHandler;

public sealed class ResearchGroupEventHandler :
    INotificationHandler<AppointmentCreatedEvent>,
    INotificationHandler<ResearchGroupDeletedEvent>,
    INotificationHandler<ResearchGroupUpdatedEvent>
{
    private readonly IDistributedCache _distributedCache;

    public ResearchGroupEventHandler(IDistributedCache distributedCache)
    {
        _distributedCache = distributedCache;
    }

    public Task Handle(AppointmentCreatedEvent notification, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }

    public async Task Handle(ResearchGroupDeletedEvent notification, CancellationToken cancellationToken)
    {
        await _distributedCache.RemoveAsync(
            CacheKeyGenerator.GetEntityCacheKey<ResearchGroup>(notification.AggregateId),
            cancellationToken);
    }

    public async Task Handle(ResearchGroupUpdatedEvent notification, CancellationToken cancellationToken)
    {
        await _distributedCache.RemoveAsync(
            CacheKeyGenerator.GetEntityCacheKey<ResearchGroup>(notification.AggregateId),
            cancellationToken);
    }
}