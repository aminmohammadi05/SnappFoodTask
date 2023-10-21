using MediatR;
using Order.Query.Domain.Entities;
using Order.Query.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Query.Infrastructure.Features.OrderProducts.Queries
{
    public class GetOrderProductsListQueryHandler : IRequestHandler<GetOrderProductsListQuery, List<OrderProductEntity>>
    {
        private readonly IAsyncRepository<OrderProductEntity> _orderRepository;
        

        public GetOrderProductsListQueryHandler(IAsyncRepository<OrderProductEntity> orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<List<OrderProductEntity>> Handle(GetOrderProductsListQuery request, CancellationToken cancellationToken)
        {
            return (await _orderRepository.ListAllAsync()).ToList();
        }
    }
}
