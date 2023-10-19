using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Query.Domain.Entities
{
    [Table("Product")]
    public class ProductEntity
    {
        [Key]
        public Guid ProductId { get; set; }
        public string Title { get; set; }
        public uint InventoryCount { get; set; }
        public uint Price { get; set; }
        public uint Discount { get; set; }

    }
}
