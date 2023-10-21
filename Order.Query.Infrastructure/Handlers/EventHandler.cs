using Microsoft.EntityFrameworkCore.Infrastructure;
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
        private readonly IOrderProductRepository _orderProductRepository;

        public EventHandler(IOrderRepository orderRepository, IProductRepository productRepository, IOrderProductRepository orderProductRepository)
        {
            _orderRepository = orderRepository;
            _productRepository = productRepository;
            _orderProductRepository = orderProductRepository;
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

        public async Task On(OrderProductAddedEvent @event)
        {
            var productInv = await _productRepository.GetByIdAsync(@event.ProductId);

            if (productInv == null) return;
            if ((productInv.InventoryCount - @event.Count) < 0) return;

            var product = new OrderProductEntity
            {
                ProductId = @event.ProductId,
               CreationDate = @event.CreationDate,
               Count = @event.Count,
               CurrentDiscount = @event.CurrentDiscount,
               CurrentPrice = @event.CurrentPrice,
               EditDate = @event.EditDate,
               OrderId = @event.Id,
               
            };
            

            await _orderProductRepository.CreateAsync(product);
        }

        public async Task On(OrderProductCountChangedEvent @event)
        {
            var product = await _productRepository.GetByIdAsync(@event.ProductId);

            if (product == null) return;
            if ((product.InventoryCount + @event.Count) < 0) return;

            product.ProductId = @event.ProductId;
            product.InventoryCount = (uint)(product.InventoryCount + @event.Count) ;

            await _productRepository.UpdateAsync(product);
        }

        public async Task On(OrderProductRemovedEvent @event)
        {
            await _productRepository.DeleteAsync(@event.ProductId);
        }

        public async Task On(OrderRemovedEvent @event)
        {
            await _orderRepository.DeleteAsync(@event.Id);
        }
    }
}
