using System.Threading;
using System.Threading.Tasks;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Shared.Events.ResearchGroup;
using CleanArchitecture.Shared.Events.ResearchGroup;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;

namespace CleanArchitecture.Domain.EventHandler
{
    public sealed class ResearchGroupEventHandler :
        
        INotificationHandler<ResearchGroupDeletedEvent>,
        INotificationHandler<ResearchGroupUpdatedEvent>,
        INotificationHandler<ResearchGroupCreatedEvent> // A�adir esta l�nea
    {
        private readonly IDistributedCache _distributedCache;

        public ResearchGroupEventHandler(IDistributedCache distributedCache)
        {
            _distributedCache = distributedCache;
        }

        public Task Handle(ResearchGroupCreatedEvent notification, CancellationToken cancellationToken)
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

        // A�adir este m�todo para manejar ResearchGroupCreatedEvent
 
    }
}
