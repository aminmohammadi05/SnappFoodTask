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
    public class GetOrdersListQueryHandler : IRequestHandler<GetOrdersListQuery, List<OrderEntity>>
    {
        private readonly IAsyncRepository<OrderEntity> _orderRepository;
        

        public GetOrdersListQueryHandler(IAsyncRepository<OrderEntity> orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<List<OrderEntity>> Handle(GetOrdersListQuery request, CancellationToken cancellationToken)
        {
            return (await _orderRepository.ListAllAsync()).ToList();
        }
    }
}
