using MediatR;
using Order.Query.Domain.Entities;
using Order.Query.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Query.Infrastructure.Features.Orders.Queries.GetOrdersList
{
    public class GetOrderByBuyerQueryHandler : IRequestHandler<GetOrderByBuyerQuery, List<OrderEntity>>
    {
        private readonly IOrderRepository _orderRepository;


        public GetOrderByBuyerQueryHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<List<OrderEntity>> Handle(GetOrderByBuyerQuery request, CancellationToken cancellationToken)
        {
            return await _orderRepository.ListByBuyerAsync(request.user);
        }
    }
}
