using CQRS.Core.Infrastructure;
using Moq;
using Order.Query.Domain.Entities;
using Order.Query.Domain.Repositories;
using OrderManager.UnitTest.Mocks;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManager.UnitTest.Categories.Commands
{
    public class CreateOrderTest
    {
        private readonly Mock<IAsyncRepository<ProductEntity>> mockProductRepository;
        private readonly Mock<IAsyncRepository<OrderEntity>> mockOrderRepository;
        public CreateOrderTest()
        {
            mockProductRepository = RepositoryMocks.GetProductRepository();
            mockOrderRepository = RepositoryMocks.GetOrderRepository();
        }

        [Fact]
        public async Task CREATE_A_NEW_ORDER_SHOULD_BE_INCREASE_LIST_COUNT_BY_ONE()
        {
            

            var newOrder = await mockOrderRepository.Object.AddAsync(new Order.Query.Domain.Entities.OrderEntity()
            {
                BuyerId = Guid.NewGuid(),
                CreationDate = DateTime.Now,
                OrderId = Guid.NewGuid()
                
            });

            var allOrders = await mockOrderRepository.Object.ListAllAsync();
            allOrders.Count.ShouldBe(5);
            newOrder.ShouldNotBeNull();
        }
    }
}
