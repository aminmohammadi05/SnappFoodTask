using CQRS.Core.Commands;

namespace Order.Command.Api.Commands
{
    public class RemoveOrderProductCommand : BaseCommand
    {
        public Guid ProductId { get; set; }
        public Guid BuyerId { get; set; }
    }
}
