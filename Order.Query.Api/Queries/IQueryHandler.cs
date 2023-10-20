using Order.Query.Domain.Entities;

namespace Order.Query.Api.Queries
{
    public interface IQueryHandler
    {
        Task<List<OrderEntity>> HandleAsync(FindAllOrdersQuery query);
        Task<List<OrderEntity>> HandleAsync(FindOrderByIdQuery query);
        Task<List<OrderEntity>> HandleAsync(FindOrdersByCriteriaQuery query);
        Task<List<OrderEntity>> HandleAsync(FindOrdersWithProductsQuery query);
    }
}
