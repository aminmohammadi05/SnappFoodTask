using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Order.Query.Domain.Entities;
using Order.Query.Domain.Repositories;
using Order.Query.Infrastructure.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Order.Query.Infrastructure.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly DatabaseContextFactory _contextFactory;
        private readonly IDistributedCache _distCache;

        public OrderRepository(DatabaseContextFactory contextFactory, IDistributedCache distCache)
        {
            _contextFactory = contextFactory;
            _distCache = distCache;
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
            var cacheKey = $"orders_{buyer}";
            var distResult = await _distCache.GetAsync(cacheKey);
            if (distResult == null)
            {
                using DatabaseContext context = _contextFactory.CreateDbContext();
                var ordersToSerialize = await context.Orders.AsNoTracking()
                        .Include(p => p.Products).AsNoTracking()
                        .Where(x => x.Buyer.UserName.Contains(buyer))
                        .ToListAsync();
                var serialized = JsonSerializer.Serialize(ordersToSerialize, CacheSourceGenerationContext.Default.ListOrderEntity);
                await _distCache.SetAsync(cacheKey, Encoding.UTF8.GetBytes(serialized),
                        new DistributedCacheEntryOptions
                        {
                            AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10)
                        });

                return ordersToSerialize;
            }
            var results = JsonSerializer.Deserialize(Encoding.UTF8.GetString(distResult),
                    CacheSourceGenerationContext.Default.ListOrderEntity);

            return results ?? new List<OrderEntity>();

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
