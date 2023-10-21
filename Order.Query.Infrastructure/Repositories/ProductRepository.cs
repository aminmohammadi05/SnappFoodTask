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
    public class ProductRepository : IProductRepository
    {
        private readonly DatabaseContextFactory _contextFactory;

        public ProductRepository(DatabaseContextFactory contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task<ProductEntity> CreateAsync(ProductEntity product)
        {
            using DatabaseContext context = _contextFactory.CreateDbContext();
            context.Products.Add(product);

            _ = await context.SaveChangesAsync();
            return await context.Products.FirstOrDefaultAsync(x => x.ProductId == product.ProductId);
        }

        public async Task DeleteAsync(Guid productId)
        {
            using DatabaseContext context = _contextFactory.CreateDbContext();
            var product = await GetByIdAsync(productId);

            if (product == null) return;

            context.Products.Remove(product);
            _ = await context.SaveChangesAsync();
        }

        public async Task<ProductEntity> GetByIdAsync(Guid productId)
        {
            using DatabaseContext context = _contextFactory.CreateDbContext();
            return await context.Products.FirstOrDefaultAsync(x => x.ProductId == productId);
        }
        public async Task<List<ProductEntity>> GetAllAsync()
        {
            using DatabaseContext context = _contextFactory.CreateDbContext();
            return await context.Products.ToListAsync();
        }

        public async Task<ProductEntity> UpdateAsync(ProductEntity product)
        {
            using DatabaseContext context = _contextFactory.CreateDbContext();
            context.Products.Update(product);

            _ = await context.SaveChangesAsync();
            return await context.Products.FirstOrDefaultAsync(x => x.ProductId == product.ProductId);
        }
    }
}
