using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Order.Query.Domain.Entities
{
    [Table("Order")]
    public class OrderEntity
    {
        public Guid OrderId { get; set; }
        public Guid BuyerId { get; set; }
        public DateTime CreationDate { get; set; }

        [System.Text.Json.Serialization.JsonIgnore]
        public virtual UserEntity Buyer { get; set; }
        public virtual ICollection<ProductEntity> Products { get; set; }
    }
    [JsonSerializable(typeof(List<OrderEntity>))]
    public partial class CacheSourceGenerationContext : JsonSerializerContext
    {

    }
}
