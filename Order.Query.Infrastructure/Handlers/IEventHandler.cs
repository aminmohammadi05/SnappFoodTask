using Order.Common.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Query.Infrastructure.Handlers
{
    public interface IEventHandler
    {
        Task On(OrderCreatedEvent @event);
        Task On(OrderUpdatedEvent @event);
        Task On(OrderProductAddedEvent @event);
        Task On(OrderProductCountChangedEvent @event);
        Task On(OrderProductRemovedEvent @event);
        Task On(OrderRemovedEvent @event);
    }
}
