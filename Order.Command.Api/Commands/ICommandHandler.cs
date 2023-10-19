namespace Order.Command.Api.Commands
{
    public interface ICommandHandler
    {
        Task HandleAsync(NewOrderCommand command);
        Task HandleAsync(EditOrderCommand command);
        Task HandleAsync(RemoveOrderCommand command);
        Task HandleAsync(AddProductCommand command);
        Task HandleAsync(ChangeProductCountCommand command);
        Task HandleAsync(RemoveProductCommand command);
    }
}
