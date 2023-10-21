using CQRS.Core.Commands;

namespace Order.Command.Api.Commands
{
    public class ModifyInventoryCountCommand : BaseCommand
    {
        public Guid ProductId { get; set; }
        /// <summary>
        /// We can add or remove items from inventory, so we need negative numbers
        /// </summary>
        public int Count { get; set; }
    }
}
