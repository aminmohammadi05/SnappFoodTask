using CQRS.Core.Domain;
using Order.Common.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Command.Domain.Aggregates
{
    public class OrderAggregate : AggregateRoot
    {
        private bool _active;
        private Guid _buyerId;
        public bool Active { get { return _active; } set { _active = value; } }
        private readonly Dictionary<Guid, Tuple<DateTime, int, uint, uint, DateTime>> _products = new();
        public OrderAggregate()
        {

        }
        #region Order methods
        /// <summary>
        /// Constructor which rises order craetion event
        /// </summary>
        /// <param name="id"></param>
        /// <param name="buyerId"></param>
        public OrderAggregate(Guid id, Guid buyerId)
        {
            RaiseEvent(new OrderCreatedEvent()
            {
                Id = id,
                BuyerId = buyerId,
                CreationDate = DateTime.UtcNow,
            });
        }

        /// <summary>
        /// Apply order creation event
        /// </summary>
        /// <param name="event"></param>
        public void Apply(OrderCreatedEvent @event)
        {
            _id = @event.Id;
            _active = true;
            _buyerId = @event.BuyerId;
        }

        /// <summary>
        /// Delete order by buyer id
        /// </summary>
        /// <param name="buyerId"></param>
        /// <exception cref="InvalidOperationException"></exception>
        public void DeleteOrder(Guid buyerId)
        {
            if (!_active)
            {
                throw new InvalidOperationException("The order has already been removed.");
            }

            if (_buyerId != buyerId)
            {
                throw new InvalidOperationException("You are not allowed to delete an order that was made by someone else!");
            }

            RaiseEvent(new OrderRemovedEvent
            {
                Id = _id
            });
        }
        /// <summary>
        /// Apply order remove event
        /// </summary>
        /// <param name="event"></param>
        public void Apply(OrderRemovedEvent @event)
        {
            _id = @event.Id;
            _active = false;
        }
        #endregion

        #region Product Methods
        /// <summary>
        /// Add Product
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="count"></param>
        /// <exception cref="InvalidOperationException"></exception>
        public void AddProduct(Guid productId, uint count, uint inventoryCount, uint currentDiscount, uint currentPrice)
        {
            if (!_active)
            {
                throw new InvalidOperationException("You cannot add a product to an inactive order.");
            }

            if (count == 0)
            {
                throw new InvalidOperationException($"You cannot add zero products to an order");
            }
            if (inventoryCount == 0)
            {
                throw new InvalidOperationException($"You cannot deduct products from empty inventory");
            }
            if (currentPrice == 0)
            {
                throw new InvalidOperationException($"You cannot add a product with zero price");
            }
            RaiseEvent(new OrderProductCountChangedInInventoryEvent
            {
                Id = _id,
                ProductId = productId,
                Count = (int)count * -1
            });
            RaiseEvent(new OrderProductAddedEvent
            {
                Id = _id,
                ProductId = productId,
                CreationDate = DateTime.UtcNow,
                EditDate = DateTime.UtcNow,
                CurrentDiscount = currentDiscount,
                CurrentPrice = currentPrice,                
                Count = count
            });
        }

        /// <summary>
        /// Apply Adding a product event
        /// </summary>
        /// <param name="event"></param>
        public void Apply(OrderProductAddedEvent @event)
        {
            _id = @event.Id;
            _products.Add(@event.ProductId, new Tuple<DateTime, int, uint, uint, DateTime>(@event.CreationDate, (int)@event.Count, @event.CurrentPrice, @event.CurrentDiscount, @event.EditDate));
        }
        /// <summary>
        /// Modify Product count in inventory
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="count"></param>
        /// <exception cref="InvalidOperationException"></exception>
        public void ModifyProductCountInInventory(Guid productId, int count)
        {
            if (!_active)
            {
                throw new InvalidOperationException("You cannot modify product count through an inactive order.");
            }

            if (count == 0)
            {
                throw new InvalidOperationException($"You cannot add or deduct zero products from inventory");
            }
            

            RaiseEvent(new OrderProductCountChangedInInventoryEvent
            {
                Id = _id,
                ProductId = productId,
                Count = count
            });
        }

        /// <summary>
        /// Apply Adding a product event
        /// </summary>
        /// <param name="event"></param>
        public void Apply(OrderProductCountChangedInInventoryEvent @event)
        {
            _id = @event.Id;
            
        }
        /// <summary>
        /// Product count changed
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="count"></param>
        /// <exception cref="InvalidOperationException"></exception>
        public void ProductCountChanged(Guid productId, int count)
        {
            if (!_active)
            {
                throw new InvalidOperationException("You cannot change the count of product of an inactive Order");
            }
            RaiseEvent(new OrderProductCountChangedEvent()
            {
                Id = _id,
                Count = count,
                EditDate = DateTime.UtcNow,
                ProductId = productId
            });
        }

        /// <summary>
        /// Apply product count changed event
        /// </summary>
        /// <param name="event"></param>
        public void Apply(OrderProductCountChangedEvent @event)
        {
            _id = @event.Id;
            _products[@event.ProductId] = new Tuple<DateTime, int, uint, uint, DateTime>(@event.EditDate, @event.Count, _products[@event.ProductId].Item3, _products[@event.ProductId].Item4, DateTime.UtcNow);
        }

        /// <summary>
        /// Removing a product
        /// </summary>
        /// <param name="productId"></param>
        /// <exception cref="InvalidOperationException"></exception>
        public void RemoveProduct(Guid productId)
        {
            if (!_active)
            {
                throw new InvalidOperationException("You cannot remove a product of an inactive order");
            }

            
            RaiseEvent(new OrderProductCountChangedInInventoryEvent
            {
                Id = _id,
                ProductId = productId,
                Count = _products[productId].Item2
            });
            RaiseEvent(new OrderProductRemovedEvent
            {
                Id = _id,
                ProductId = productId
            });
        }

        /// <summary>
        /// Apply product removed event
        /// </summary>
        /// <param name="event"></param>
        public void Apply(OrderProductRemovedEvent @event)
        {
            _id = @event.Id;
            _products.Remove(@event.ProductId);
        }
        #endregion
    }
}
