using CQRS.Core.Commands;

namespace Order.Command.Api.Commands
{
    public class RemoveOrderCommand : BaseCommand
    {
        public Guid OrderId { get; set; }
        public Guid BuyerId { get; set; }
    }
}
