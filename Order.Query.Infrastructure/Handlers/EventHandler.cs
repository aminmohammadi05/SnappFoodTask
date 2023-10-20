using Order.Common.Events;
using Order.Query.Domain.Entities;
using Order.Query.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Query.Infrastructure.Handlers
{
    public class EventHandler : IEventHandler
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IProductRepository _productRepository;

        public EventHandler(IOrderRepository orderRepository, IProductRepository productRepository)
        {
            _orderRepository = orderRepository;
            _productRepository = productRepository;
        }

        public async Task On(OrderCreatedEvent @event)
        {
            var order = new OrderEntity
            {
                OrderId = @event.Id,
                BuyerId = @event.BuyerId,
                CreationDate = @event.CreationDate,
                
            };

            await _orderRepository.CreateAsync(order);
        }

        

        public async Task On(OrderUpdatedEvent @event)
        {
            var order = await _orderRepository.GetByIdAsync(@event.Id);

            if (order == null) return;

            await _orderRepository.UpdateAsync(order);
        }

        public async Task On(ProductAddedEvent @event)
        {
            var product = new ProductEntity
            {
                ProductId = @event.ProductId,
                Title = @event.Title,
                Discount = @event.Discount,
                InventoryCount = @event.InventoryCount,
                Price = @event.Price
            };

            await _productRepository.CreateAsync(product);
        }

        public async Task On(ProductCountChangedEvent @event)
        {
            var product = await _productRepository.GetByIdAsync(@event.ProductId);

            if (product == null) return;

            //product.ProductId = @event.ProductId;
            //product.Co = true;

            await _productRepository.UpdateAsync(product);
        }

        public async Task On(ProductRemovedEvent @event)
        {
            await _productRepository.DeleteAsync(@event.ProductId);
        }

        public async Task On(OrderRemovedEvent @event)
        {
            await _orderRepository.DeleteAsync(@event.Id);
        }
    }
}
