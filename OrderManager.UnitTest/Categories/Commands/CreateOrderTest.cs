using CQRS.Core.Infrastructure;
using Moq;
using Order.Query.Domain.Repositories;
using OrderManager.UnitTest.Mocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManager.UnitTest.Categories.Commands
{
    public class CreateOrderTest
    {
        private readonly Mock<IProductRepository> mockProductRepository;
        private readonly Mock<IOrderRepository> mockOrderRepository;
        public CreateOrderTest()
        {
            mockProductRepository = RepositoryMocks.GetProductRepository();
            mockOrderRepository = RepositoryMocks.GetOrderRepository();
        }

        [Fact]
        public async Task Handle_ValidCategory_AddedToCategoriesRepo()
        {
            

            await mockCommandDispatcher..(new CreateCategoryCommand() { Name = "Test" }, CancellationToken.None);

            var allCategories = await _mockCategoryRepository.Object.ListAllAsync();
            allCategories.Count.ShouldBe(5);
        }
    }
}
