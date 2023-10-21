using CQRS.Core.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Common.Events
{
    public class OrderProductCountChangedEvent : BaseEvent
    {
        public OrderProductCountChangedEvent() : base(nameof(OrderProductCountChangedEvent))
        {

        }
        public Guid ProductId { get; set; }
        public int Count { get; set; }
        public DateTime EditDate { get; set; }
    }
}
