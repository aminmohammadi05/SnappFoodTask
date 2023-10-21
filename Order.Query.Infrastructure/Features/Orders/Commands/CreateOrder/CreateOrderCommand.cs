using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Query.Infrastructure.Features.Orders.Commands.CreateOrder
{
    public class CreateOrderCommand : IRequest<CreateOrderCommandResponse>
    {
        public Guid OrderId { get; set; }
        public DateTime CreationDate { get; set; }
        public Guid BuyerId { get; set; }
    }
}
