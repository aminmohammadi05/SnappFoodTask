using Microsoft.EntityFrameworkCore;
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
    public class OrderRepository : IOrderRepository
    {
        private readonly DatabaseContextFactory _contextFactory;

        public OrderRepository(DatabaseContextFactory contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task CreateAsync(OrderEntity order)
        {
            using DatabaseContext context = _contextFactory.CreateDbContext();
            context.Orders.Add(order);

            _ = await context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid orderId)
        {
            using DatabaseContext context = _contextFactory.CreateDbContext();
            var order = await GetByIdAsync(orderId);

            if (order == null) return;

            context.Orders.Remove(order);
            _ = await context.SaveChangesAsync();
        }

        public async Task<OrderEntity> GetByIdAsync(Guid orderId)
        {
            using DatabaseContext context = _contextFactory.CreateDbContext();
            return await context.Orders
                    .Include(p => p.Products)
                    .FirstOrDefaultAsync(x => x.OrderId == orderId);
        }

        public async Task<List<OrderEntity>> ListAllAsync()
        {
            using DatabaseContext context = _contextFactory.CreateDbContext();
            return await context.Orders.AsNoTracking()
                    .Include(p => p.Products).AsNoTracking()
                    .ToListAsync();
        }

        public async Task<List<OrderEntity>> ListByBuyerAsync(string buyer)
        {
            using DatabaseContext context = _contextFactory.CreateDbContext();
            return await context.Orders.AsNoTracking()
                    .Include(p => p.Products).AsNoTracking()
                    .Where(x => x.Buyer.UserName.Contains(buyer))
                    .ToListAsync();
        }

        public async Task<List<OrderEntity>> ListWithProductsAsync()
        {
            using DatabaseContext context = _contextFactory.CreateDbContext();
            return await context.Orders.AsNoTracking()
                    .Include(p => p.Products).AsNoTracking()
                    .Where(x => x.Products != null && x.Products.Any())
                    .ToListAsync();
        }

        

        public async Task UpdateAsync(OrderEntity order)
        {
            using DatabaseContext context = _contextFactory.CreateDbContext();
            context.Orders.Update(order);

            _ = await context.SaveChangesAsync();
        }
    }
}
