using CQRS.Core.Infrastructure;
using EmptyFiles;
using Microsoft.EntityFrameworkCore;
using Moq;
using Order.Query.Domain.Entities;
using Order.Query.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManager.UnitTest.Mocks
{
    public class RepositoryMocks
    {
        public static Mock<IProductRepository> GetProductRepository()
        {
            var product1 = Guid.Parse("{B0788D2F-8003-43C1-92A4-EDC76A7C5DDE}");
            var product2 = Guid.Parse("{6313179F-7837-473A-A4D5-A5571B43E6A6}");
            var product3 = Guid.Parse("{BF3F3002-7E53-441E-8B76-F6280BE284AA}");
            var product4 = Guid.Parse("{FE98F549-E790-4E9F-AA16-18C2292A2EE9}");

            var products = new List<ProductEntity>
            {
                new ProductEntity
            {
                Discount = 30,
                InventoryCount = 18,
                Price = 350,
                ProductId = product1,
                Title = "Product 1"
            },
            new ProductEntity
            {
                Discount = 5,
                InventoryCount = 14,
                Price = 125,
                ProductId = product2,
                Title = "Product 2"
            },
            new ProductEntity
            {
                Discount = 10,
                InventoryCount = 25,
                Price = 200,
                ProductId = product3,
                Title = "Product 3"
            },
            new ProductEntity
            {
                Discount = 0,
                InventoryCount = 20,
                Price = 100,
                ProductId = product4,
                Title = "Product 4"
            }
        };

            var mockProductRepository = new Mock<IProductRepository>();
            mockProductRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(products);

            mockProductRepository.Setup(repo => repo.CreateAsync(It.IsAny<ProductEntity>())).Returns(
                (ProductEntity product) =>
                {
                    products.Add(product);
                    return products;
                });

            return mockProductRepository;
        }
        public static Mock<IOrderRepository> GetOrderRepository()
        {
            Guid userId = Guid.Parse("{B0788D2F-8003-43C1-92A4-EDC76A7C5DDE}");
            Guid order1 = Guid.Parse("{B0788D2F-8003-43C1-92A4-EDC76A7C5DDE}");
            Guid order2 = Guid.Parse("{B0788D2F-8003-43C1-92A4-EDC76A7C5DDE}");
            Guid order3 = Guid.Parse("{B0788D2F-8003-43C1-92A4-EDC76A7C5DDE}");
            var orders = new List<OrderEntity>()
            {
                new OrderEntity()
                {
                    BuyerId = userId,
                    CreationDate = DateTime.Now,
                    OrderId = order1,
                    OrderProducts = new List<OrderProductEntity>()
                    {
                        new OrderProductEntity()
                        {
                            Count = 1,
                            CurrentDiscount = 0,
                            CreationDate= DateTime.Now,
                            CurrentPrice = 0,
                            EditDate = DateTime.Now,
                            OrderId = order1,
                            ProductId
                        }
                    }
                }
            };

            var mockOrderRepository = new Mock<IOrderRepository>();
            mockOrderRepository.Setup(repo => repo.ListAllAsync()).ReturnsAsync(orders);

            mockOrderRepository.Setup(repo => repo.CreateAsync(It.IsAny<OrderEntity>())).Returns(
                (OrderEntity order) =>
                {
                    orders.Add(order);
                    return orders;
                });

            return mockOrderRepository;
        }
        public static Mock<ICommandDispatcher> GetCommandDispatcher()
        {
            

            var mockCommandDispatcher = new Mock<ICommandDispatcher>();
           

            return mockCommandDispatcher;
        }
    }
}
