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
        private readonly Dictionary<Guid, Tuple<DateTime, uint>> _products = new();
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
        public void AddProduct(Guid productId, uint count)
        {
            if (!_active)
            {
                throw new InvalidOperationException("You cannot add a product to an inactive order.");
            }

            if (count == 0)
            {
                throw new InvalidOperationException($"You cannot add zero products to an order");
            }

            RaiseEvent(new ProductAddedEvent
            {
                Id = _id,
                ProductId = productId,
                CreationDate = DateTime.UtcNow,
                Count = count
            });
        }

        /// <summary>
        /// Apply Adding a product event
        /// </summary>
        /// <param name="event"></param>
        public void Apply(ProductAddedEvent @event)
        {
            _id = @event.Id;
            _products.Add(@event.ProductId, new Tuple<DateTime, uint>(@event.CreationDate, @event.Count));
        }

        /// <summary>
        /// Product count changed
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="count"></param>
        /// <exception cref="InvalidOperationException"></exception>
        public void ProductCountChanged(Guid productId, uint count)
        {
            if (!_active)
            {
                throw new InvalidOperationException("You cannot change the count of product of an inactive Order");
            }
            RaiseEvent(new ProductCountChangedEvent()
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
        public void Apply(ProductCountChangedEvent @event)
        {
            _id = @event.Id;
            _products[@event.ProductId] = new Tuple<DateTime, uint>(@event.EditDate, @event.Count);
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

            //if (!_products[productId].Item2.Equals(username, StringComparison.CurrentCultureIgnoreCase))
            //{
            //    throw new InvalidOperationException("You are not allowed to remove a product that was in another order");
            //}

            RaiseEvent(new ProductRemovedEvent
            {
                Id = _id,
                ProductId = productId
            });
        }

        /// <summary>
        /// Apply product removed event
        /// </summary>
        /// <param name="event"></param>
        public void Apply(ProductRemovedEvent @event)
        {
            _id = @event.Id;
            _products.Remove(@event.ProductId);
        }
        #endregion
    }
}
