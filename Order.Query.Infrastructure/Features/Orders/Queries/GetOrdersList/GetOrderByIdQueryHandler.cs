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
    public class GetOrderByIdQueryHandler : IRequestHandler<GetOrderByIdQuery, OrderEntity>
    {
        private readonly IAsyncRepository<OrderEntity> _orderRepository;


        public GetOrderByIdQueryHandler(IAsyncRepository<OrderEntity> orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<OrderEntity> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
        {
            return await _orderRepository.GetByIdAsync(request.Id);
        }
    }
}
