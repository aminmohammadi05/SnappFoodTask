using CQRS.Core.Commands;

namespace Order.Command.Api.Commands
{
    public class NewOrderCommand : BaseCommand
    {
        public DateTime CreationDate { get; set; }
        public Guid BuyerId { get; set; }
    }
}
