using Order.Query.Domain.Entities;
using Order.Query.Infrastructure.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Query.Infrastructure.Features.OrderProducts.Commands
{
    public class CreateOrderProductCommandResponse : BaseResponse
    {
        public CreateOrderProductCommandResponse() : base()
        {

        }

        public OrderProductEntity OrderProduct { get; set; } = default!;
    }
}
