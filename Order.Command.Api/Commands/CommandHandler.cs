using CQRS.Core.Handlers;
using Order.Command.Domain.Aggregates;

namespace Order.Command.Api.Commands
{
    public class CommandHandler : ICommandHandler
    {
        private readonly IEventSourcingHandler<OrderAggregate> _eventSourcingHandler;

        public CommandHandler(IEventSourcingHandler<OrderAggregate> eventSourcingHandler)
        {
            _eventSourcingHandler = eventSourcingHandler;
        }

        public async Task HandleAsync(NewOrderCommand command)
        {
            var aggregate = new OrderAggregate(command.Id, command.BuyerId);
            await _eventSourcingHandler.SaveAsync(aggregate);
        }

        public async Task HandleAsync(EditOrderCommand command)
        {
            var aggregate = await _eventSourcingHandler.GetByIdAsync(command.Id);
            //aggregate.
            await _eventSourcingHandler.SaveAsync(aggregate);
        }

        public async Task HandleAsync(RemoveOrderCommand command)
        {
            var aggregate = await _eventSourcingHandler.GetByIdAsync(command.Id);
            aggregate.DeleteOrder(command.BuyerId);

            await _eventSourcingHandler.SaveAsync(aggregate);
        }

        public async Task HandleAsync(AddProductCommand command)
        {
            var aggregate = await _eventSourcingHandler.GetByIdAsync(command.Id);
            aggregate.AddProduct(command.ProductId, command.Count);

            await _eventSourcingHandler.SaveAsync(aggregate);
        }

        public async Task HandleAsync(ChangeProductCountCommand command)
        {
            var aggregate = await _eventSourcingHandler.GetByIdAsync(command.Id);
            aggregate.ProductCountChanged(command.ProductId, command.Count);

            await _eventSourcingHandler.SaveAsync(aggregate);
        }

        public async Task HandleAsync(RemoveProductCommand command)
        {
            var aggregate = await _eventSourcingHandler.GetByIdAsync(command.Id);
            aggregate.RemoveProduct(command.ProductId);

            await _eventSourcingHandler.SaveAsync(aggregate);
        }
    }
}
