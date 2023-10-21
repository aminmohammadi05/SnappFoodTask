using MediatR;
using Order.Query.Domain.Entities;
using Order.Query.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Query.Infrastructure.Features.Products.Queries.GetProductsList
{
    public class GetProductsListQueryHandler : IRequestHandler<GetProductsListQuery, List<ProductEntity>>
    {
        private readonly IAsyncRepository<ProductEntity> _orderRepository;


        public GetProductsListQueryHandler(IAsyncRepository<ProductEntity> orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<List<ProductEntity>> Handle(GetProductsListQuery request, CancellationToken cancellationToken)
        {
            return (await _orderRepository.ListAllAsync()).ToList();
        }
    }
}
