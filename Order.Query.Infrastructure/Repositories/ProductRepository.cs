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
    public class ProductRepository : BaseRepository<ProductEntity>, IProductRepository
    {
        private readonly DatabaseContext _context;

        public ProductRepository(DatabaseContext context): base(context)
        {
            _context = context;
        }

        public async Task<ProductEntity> CreateAsync(ProductEntity product)
        {
            
            _context.Set<ProductEntity>().Add(product);

            _ = await _context.SaveChangesAsync();
            return await _context.Set<ProductEntity>().FirstOrDefaultAsync(x => x.ProductId == product.ProductId);
        }

        public async Task DeleteAsync(Guid productId)
        {
            var product = await GetByIdAsync(productId);

            if (product == null) return;

            _context.Set<ProductEntity>().Remove(product);
            _ = await _context.SaveChangesAsync();
        }

        public async Task<ProductEntity> GetByIdAsync(Guid productId)
        {
            return await _context.Set<ProductEntity>().FirstOrDefaultAsync(x => x.ProductId == productId);
        }
        public async Task<List<ProductEntity>> GetAllAsync()
        {
            return await _context.Set<ProductEntity>().ToListAsync();
        }

        public async Task<ProductEntity> UpdateAsync(ProductEntity product)
        {
            _context.Set<ProductEntity>().Update(product);

            _ = await _context.SaveChangesAsync();
            return await _context.Set<ProductEntity>().FirstOrDefaultAsync(x => x.ProductId == product.ProductId);
        }
    }
}
