namespace Order.Command.Api.Commands
{
    public interface ICommandHandler
    {
        Task HandleAsync(NewOrderCommand command);
        Task HandleAsync(EditOrderCommand command);
        Task HandleAsync(RemoveOrderCommand command);
        Task HandleAsync(AddOrderProductCommand command);
        Task HandleAsync(ChangeOrderProductCountCommand command);
        Task HandleAsync(RemoveOrderProductCommand command);
    }
}
