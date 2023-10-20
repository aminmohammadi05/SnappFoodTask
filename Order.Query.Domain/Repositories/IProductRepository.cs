using Order.Query.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Query.Domain.Repositories
{
    public interface IProductRepository
    {
        Task CreateAsync(ProductEntity product);
        Task UpdateAsync(ProductEntity product);
        Task<ProductEntity> GetByIdAsync(Guid productId);
        Task DeleteAsync(Guid productId);
    }
}
