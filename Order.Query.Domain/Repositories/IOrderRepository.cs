using Order.Query.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Query.Domain.Repositories
{
    public interface IOrderRepository 
    {
        Task<OrderEntity> CreateAsync(OrderEntity order);
        Task<OrderEntity> UpdateAsync(OrderEntity order);
        Task DeleteAsync(Guid orderId);
        Task<OrderEntity> GetByIdAsync(Guid orderId);
        Task<List<OrderEntity>> ListAllAsync();
        Task<List<OrderEntity>> ListByBuyerAsync(string buyer);
        Task<List<OrderEntity>> ListWithProductsAsync();
        
    }
}
