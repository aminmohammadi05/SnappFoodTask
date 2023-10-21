using CQRS.Core.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Common.Events
{
    public class OrderProductAddedEvent : BaseEvent
    {
        public OrderProductAddedEvent() : base(nameof(OrderProductAddedEvent))
        {

        }
        public Guid ProductId { get; set; }
        public uint Count { get; set; }
        public DateTime CreationDate { get; set; }
        public uint CurrentDiscount { get; set; }
        public uint CurrentPrice { get; set; }
        public DateTime EditDate { get; set; }
    }
}
