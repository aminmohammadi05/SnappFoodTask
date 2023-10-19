using CQRS.Core.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Common.Events
{
    public class OrderRemovedEvent : BaseEvent
    {
        public OrderRemovedEvent() : base(nameof(OrderRemovedEvent))
        {

        }
        public DateTime CreationDate { get; set; }
    }
}
