using CQRS.Core.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Common.Events
{
    public class OrderCreatedEvent : BaseEvent
    {
        public OrderCreatedEvent() : base(nameof(OrderCreatedEvent))
        {
        }
        public DateTime CreationDate { get; set; }
        public Guid BuyerId { get; set; }
    }
}
