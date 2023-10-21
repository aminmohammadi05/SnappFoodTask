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
    public class OrderProductRepository : IOrderProductRepository
    {
        private readonly DatabaseContextFactory _contextFactory;

        public OrderProductRepository(DatabaseContextFactory contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task CreateAsync(OrderProductEntity product)
        {
            using DatabaseContext context = _contextFactory.CreateDbContext();
            context.OrderProducts.Add(product);
            _ = await context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid productId, Guid orderId)
        {
            using DatabaseContext context = _contextFactory.CreateDbContext();
            var product = await GetByIdAsync(productId, orderId);

            if (product == null) return;

            context.OrderProducts.Remove(product);
            _ = await context.SaveChangesAsync();
        }

        public async Task<OrderProductEntity> GetByIdAsync(Guid productId, Guid orderId)
        {
            using DatabaseContext context = _contextFactory.CreateDbContext();
            return await context.OrderProducts.FirstOrDefaultAsync(x => x.ProductId == productId && x.OrderId == orderId);
        }

        public async Task UpdateAsync(OrderProductEntity product)
        {
            using DatabaseContext context = _contextFactory.CreateDbContext();
            context.OrderProducts.Update(product);

            _ = await context.SaveChangesAsync();
        }
    }
}
