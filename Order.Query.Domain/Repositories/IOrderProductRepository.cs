using Order.Query.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Query.Domain.Repositories
{
    public interface IOrderProductRepository
    {
        Task CreateAsync(OrderProductEntity product);
        Task UpdateAsync(OrderProductEntity product);
        Task<OrderProductEntity> GetByIdAsync(Guid productId, Guid orderId);
        Task DeleteAsync(Guid productId, Guid orderId);
    }
}
