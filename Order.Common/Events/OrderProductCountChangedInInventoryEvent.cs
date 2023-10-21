using CQRS.Core.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Common.Events
{
    public class OrderProductCountChangedInInventoryEvent : BaseEvent
    {
        public OrderProductCountChangedInInventoryEvent() : base(nameof(OrderProductCountChangedInInventoryEvent))
        {
        }
        public int Count { get; set; }
        public Guid ProductId { get; set; }
    }
}
