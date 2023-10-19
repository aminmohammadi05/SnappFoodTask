using CQRS.Core.Commands;

namespace Order.Command.Api.Commands
{
    public class RemoveProductCommand : BaseCommand
    {
        public Guid ProductId { get; set; }
        public Guid BuyerId { get; set; }
    }
}
