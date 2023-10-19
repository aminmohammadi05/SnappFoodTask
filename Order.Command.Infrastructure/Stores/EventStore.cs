using CQRS.Core.Domain;
using CQRS.Core.Events;
using CQRS.Core.Exceptions;
using CQRS.Core.Infrastructure;
using CQRS.Core.Producers;
using Order.Command.Domain.Aggregates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Command.Infrastructure.Stores
{
    public class EventStore : IEventStore
    {
        private readonly IEventStoreRepository _eventStoreRepository;
        private readonly IEventProducer _producer;

        public EventStore(IEventStoreRepository eventStoreRepository, IEventProducer producer)
        {
            _eventStoreRepository = eventStoreRepository;
            _producer = producer;
        }


        public async Task<List<BaseEvent>> GetEventsAsync(Guid aggregateId)
        {
            var eventStream = await _eventStoreRepository.FindByAggregateId(aggregateId);
            if (eventStream == null || !eventStream.Any())
            {
                throw new AggregateNotFoundException("Incorrect order id provided");
            }
            return eventStream.OrderBy(x => x.Version).Select(x => x.EventData).ToList();
        }

        public async Task SaveEventsAsync(Guid aggregateId, IEnumerable<BaseEvent> baseEvents, int expectedVersion)
        {
            var eventStream = await _eventStoreRepository.FindByAggregateId(aggregateId);
            if (expectedVersion != -1 && eventStream[^1].Version != expectedVersion)
            {
                throw new ConcurrencyException();
            }
            var version = expectedVersion;
            foreach (var @event in baseEvents)
            {
                version++;
                @event.Version = version;
                var eventType = @event.GetType().Name;

                var eventModel = new EventModel() 
                {
                    TimeStamp = DateTime.UtcNow,
                    AggregateIdentifier = aggregateId,
                    AggregateType = nameof(OrderAggregate),
                    Version = version,
                    EventType = eventType,
                    EventData = @event
                };
                await _eventStoreRepository.SaveAsync(eventModel);
                var topic = Environment.GetEnvironmentVariable("KAFKA_TOPIC");
                await _producer.ProduceAsync(topic, @event);
            }
        }
    }
}
