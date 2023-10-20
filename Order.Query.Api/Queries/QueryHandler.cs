using Order.Query.Domain.Entities;
using Order.Query.Domain.Repositories;

namespace Order.Query.Api.Queries
{
    public class QueryHandler : IQueryHandler
    {
        private readonly IOrderRepository _orderRepository;

        public QueryHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<List<OrderEntity>> HandleAsync(FindAllOrdersQuery query)
        {
            return await _orderRepository.ListAllAsync();
        }

        public async Task<List<OrderEntity>> HandleAsync(FindOrderByIdQuery query)
        {
            var post = await _orderRepository.GetByIdAsync(query.Id);
            return new List<OrderEntity> { post };
        }

        public async Task<List<OrderEntity>> HandleAsync(FindOrdersByCriteriaQuery query)
        {
            return await _orderRepository.ListByBuyerAsync(query.Buyer);
        }

        public async Task<List<OrderEntity>> HandleAsync(FindOrdersWithProductsQuery query)
        {
            return await _orderRepository.ListWithProductsAsync();
        }

    }
}
