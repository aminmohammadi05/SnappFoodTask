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
        Task<ProductEntity> CreateAsync(ProductEntity product);
        Task<ProductEntity> UpdateAsync(ProductEntity product);
        Task<ProductEntity> GetByIdAsync(Guid productId);
        Task<List<ProductEntity>> GetAllAsync();
        Task DeleteAsync(Guid productId);
    }
}
