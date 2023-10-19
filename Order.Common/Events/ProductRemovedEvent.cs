using CQRS.Core.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Common.Events
{
    public class ProductRemovedEvent : BaseEvent
    {
        public ProductRemovedEvent() : base(nameof(ProductRemovedEvent))
        {

        }
        public Guid ProductId { get; set; }
    }
}
