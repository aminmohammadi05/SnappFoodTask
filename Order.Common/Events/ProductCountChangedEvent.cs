using CQRS.Core.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Common.Events
{
    public class ProductCountChangedEvent : BaseEvent
    {
        public ProductCountChangedEvent() : base(nameof(ProductCountChangedEvent))
        {

        }
        public Guid ProductId { get; set; }
    }
}
