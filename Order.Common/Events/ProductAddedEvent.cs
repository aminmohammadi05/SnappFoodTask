using CQRS.Core.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Common.Events
{
    public class ProductAddedEvent : BaseEvent
    {
        public ProductAddedEvent() : base(nameof(ProductAddedEvent))
        {

        }
        public Guid ProductId { get; set; }
        public uint Count { get; set; }
        public DateTime CreationDate { get; set; }
        public string Title { get; set; }
        public uint Discount { get; set; }
        public uint InventoryCount { get; set; }
        public uint Price { get; set; }
    }
}
