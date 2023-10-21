using CQRS.Core.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Common.Events
{
    public class OrderProductRemovedEvent : BaseEvent
    {
        public OrderProductRemovedEvent() : base(nameof(OrderProductRemovedEvent))
        {

        }
        public Guid ProductId { get; set; }
    }
}
