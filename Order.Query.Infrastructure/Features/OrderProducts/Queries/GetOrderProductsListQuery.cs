using MediatR;
using Order.Query.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Query.Infrastructure.Features.OrderProducts.Queries
{
    public class GetOrderProductsListQuery : IRequest<List<OrderProductEntity>>
    {
    }
}
