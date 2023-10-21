using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Order.Query.Domain.Entities;
using Order.Query.Domain.Repositories;
using Order.Query.Infrastructure.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Query.Infrastructure.Repositories
{
    public class OrderProductRepository : BaseRepository<OrderProductEntity>, IOrderProductRepository
    {
        private readonly DatabaseContext _context;

        public OrderProductRepository(DatabaseContext context) : base(context)
        {
            _context = context;
        }

        public async Task CreateAsync(OrderProductEntity product)
        {
            _context.Set<OrderProductEntity>().Add(product);
            _ = await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid productId, Guid orderId)
        {
            var product = await GetByIdAsync(productId, orderId);

            if (product == null) return;

            _context.Set<OrderProductEntity>().Remove(product);
            _ = await _context.SaveChangesAsync();
        }

        public async Task<OrderProductEntity> GetByIdAsync(Guid productId, Guid orderId)
        {
            return await _context.Set<OrderProductEntity>().FirstOrDefaultAsync(x => x.ProductId == productId && x.OrderId == orderId);
        }

        public async Task UpdateAsync(OrderProductEntity product)
        {
            _context.Set<OrderProductEntity>().Update(product);

            _ = await _context.SaveChangesAsync();
        }
    }
}
