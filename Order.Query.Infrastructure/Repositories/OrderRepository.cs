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
    public class OrderRepository : BaseRepository<OrderEntity>,  IOrderRepository
    {
        private readonly DatabaseContext _context;
        private readonly IDistributedCache _distCache;

        public OrderRepository(DatabaseContext context, IDistributedCache distCache) : base(context)
        {
            _context = context;
            _distCache = distCache;
        }

        public async Task<OrderEntity> CreateAsync(OrderEntity order)
        {
           
            _context.Set<OrderEntity>().Add(order);

            _ = await _context.SaveChangesAsync();
            return await _context.Set<OrderEntity>()
                    .Include(p => p.OrderProducts)
                    .FirstOrDefaultAsync(x => x.OrderId == order.OrderId);
        }

        public async Task DeleteAsync(Guid orderId)
        {
            var order = await GetByIdAsync(orderId);

            if (order == null) return;

            _context.Set<OrderEntity>().Remove(order);
            _ = await _context.SaveChangesAsync();
        }

        public async Task<OrderEntity> GetByIdAsync(Guid orderId)
        {
            return await _context.Set<OrderEntity>()
                    .Include(p => p.OrderProducts)
                    .FirstOrDefaultAsync(x => x.OrderId == orderId);
        }

        public async Task<List<OrderEntity>> ListAllAsync()
        {
            
            return await _context.Set<OrderEntity>().AsNoTracking()
                    .Include(p => p.OrderProducts).AsNoTracking()
                    .ToListAsync();
        }

        public async Task<List<OrderEntity>> ListByBuyerAsync(string buyer)
        {
            var cacheKey = $"orders_{buyer}";
            var distResult = await _distCache.GetAsync(cacheKey);
            if (distResult == null)
            {
                var ordersToSerialize = await _context.Set<OrderEntity>().AsNoTracking()
                        .Include(p => p.OrderProducts).AsNoTracking()
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
            return await _context.Set<OrderEntity>().AsNoTracking()
                    .Include(p => p.OrderProducts).AsNoTracking()
                    .Where(x => x.OrderProducts != null && x.OrderProducts.Any())
                    .ToListAsync();
        }

        

        public async Task<OrderEntity> UpdateAsync(OrderEntity order)
        {
            _context.Set<OrderEntity>().Update(order);

            _ = await _context.SaveChangesAsync();
            return await _context.Set<OrderEntity>()
                    .Include(p => p.OrderProducts)
                    .FirstOrDefaultAsync(x => x.OrderId == order.OrderId);
        }
    }
}
