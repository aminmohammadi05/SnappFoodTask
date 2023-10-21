using CQRS.Core.Commands;

namespace Order.Command.Api.Commands
{
    public class ChangeOrderProductCountCommand : BaseCommand
    {
        public Guid ProductId { get; set; }
        public uint Count { get; set; }
        public uint CurrentPrice { get; set; }
        /// <summary>
        /// We save current discount as to the discount value maybe change by time
        /// </summary>
        public uint CurrentDiscount { get; set; }

        /// <summary>
        /// Total price is 
        /// </summary>
        public uint InventoryCount { get; set; }
        public uint TotalPrice { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime EditDate { get; set; }
        public Guid BuyerId { get; set; }
    }
}
