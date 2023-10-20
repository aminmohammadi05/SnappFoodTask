using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Query.Domain.Entities
{
    [Table("User")]
    public class UserEntity
    {
       
        public Guid UserId { get; set; }
        public string UserName { get; set; }
        
        public virtual ICollection<OrderEntity> Orders { get; set; }
    }
}
