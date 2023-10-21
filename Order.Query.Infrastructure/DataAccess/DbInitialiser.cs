using Order.Query.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Query.Infrastructure.DataAccess
{
    public class DbInitialiser
    {
        private readonly DatabaseContext _context;

        public DbInitialiser(DatabaseContext context)
        {
            _context = context;
        }

        public void Run()
        {
            _context.Set<UserEntity>().Add(new UserEntity
            {
                UserId = Guid.NewGuid(),
                UserName = "User1"
            });
            _context.SaveChanges();

            _context.Set<ProductEntity>().Add(new ProductEntity
            {
                Discount = 30,
                InventoryCount = 18,
                Price = 350,
                ProductId = Guid.NewGuid(),
                Title = "Product 1"
            });
            _context.Set<ProductEntity>().Add(new ProductEntity
            {
                Discount = 5,
                InventoryCount = 14,
                Price = 125,
                ProductId = Guid.NewGuid(),
                Title = "Product 2"
            });
            _context.Set<ProductEntity>().Add(new ProductEntity
            {
                Discount = 10,
                InventoryCount = 25,
                Price = 200,
                ProductId = Guid.NewGuid(),
                Title = "Product 3"
            });
            _context.Set<ProductEntity>().Add(new ProductEntity
            {
                Discount = 0,
                InventoryCount = 20,
                Price = 100,
                ProductId = Guid.NewGuid(),
                Title = "Product 4"
            });
            _context.SaveChanges();
        }
    }
}
