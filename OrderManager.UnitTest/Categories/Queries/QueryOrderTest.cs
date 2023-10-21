using Moq;
using Order.Query.Domain.Entities;
using Order.Query.Domain.Repositories;
using Order.Query.Infrastructure.Features.Orders.Queries.GetOrdersList;
using OrderManager.UnitTest.Mocks;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManager.UnitTest.Categories.Queries
{
    public class QueryOrderTest
    {
        private readonly Mock<IAsyncRepository<OrderEntity>> _mockOrderRepository;

        public QueryOrderTest()
        {
            _mockOrderRepository = RepositoryMocks.GetOrderRepository();
            
        }

        [Fact]
        public async Task GetOrdersListTest()
        {
            var handler = new GetOrdersListQueryHandler(_mockOrderRepository.Object);

            var result = await handler.Handle(new GetOrdersListQuery(), CancellationToken.None);
            

            result.ShouldBeOfType<List<OrderEntity>>();

            result.Count.ShouldBe(4);
        }
    }
}
