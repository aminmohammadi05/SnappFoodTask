using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Query.Infrastructure.Features.OrderProducts.Commands
{
    public class CreateOrderProductCommand : IRequest<CreateOrderProductCommandResponse>
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
    }
}
