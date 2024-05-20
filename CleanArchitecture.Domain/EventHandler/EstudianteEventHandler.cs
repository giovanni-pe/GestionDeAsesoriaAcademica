using System.Threading;
using System.Threading.Tasks;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Shared.Events.Estudiante;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;

namespace CleanArchitecture.Domain.EventHandler;

public sealed class EstudianteEventHandler :
    INotificationHandler<EstudianteCreatedEvent>,
    INotificationHandler<EstudianteDeletedEvent>,
    INotificationHandler<EstudianteUpdatedEvent>
{
    private readonly IDistributedCache _distributedCache;

    public EstudianteEventHandler(IDistributedCache distributedCache)
    {
        _distributedCache = distributedCache;
    }

    public Task Handle(EstudianteCreatedEvent notification, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }

    public async Task Handle(EstudianteDeletedEvent notification, CancellationToken cancellationToken)
    {
        await _distributedCache.RemoveAsync(
            CacheKeyGenerator.GetEntityCacheKey<Estudiante>(notification.AggregateId),
            cancellationToken);
    }

    public async Task Handle(EstudianteUpdatedEvent notification, CancellationToken cancellationToken)
    {
        await _distributedCache.RemoveAsync(
            CacheKeyGenerator.GetEntityCacheKey<Estudiante>(notification.AggregateId),
            cancellationToken);
    }
}