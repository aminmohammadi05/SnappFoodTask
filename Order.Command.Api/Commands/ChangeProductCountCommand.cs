using CQRS.Core.Commands;

namespace Order.Command.Api.Commands
{
    public class ChangeProductCountCommand : BaseCommand
    {
        public Guid ProductId { get; set; }
        public uint Count { get; set; }
        public Guid BuyerId { get; set; }
    }
}
