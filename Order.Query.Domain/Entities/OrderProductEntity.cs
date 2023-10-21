using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Query.Domain.Entities
{
    [Table("OrderProduct")]
    public class OrderProductEntity
    {
        public Guid OrderId { get; set; }
        public Guid ProductId { get; set; }
        public uint Count { get; set; }
        /// <summary>
        /// We save current price as to the price maybe change by time
        /// </summary>
        public uint CurrentPrice { get; set; }
        /// <summary>
        /// We save current discount as to the discount value maybe change by time
        /// </summary>
        public uint CurrentDiscount { get; set; }
        /// <summary>
        /// Total price is 
        /// </summary>
        public uint TotalPrice { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime EditDate { get; set; }
        public virtual OrderEntity Order { get; set; }
        public virtual ProductEntity Product { get; set;}
    }
}
