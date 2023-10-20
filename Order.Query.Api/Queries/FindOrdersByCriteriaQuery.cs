using CQRS.Core.Queries;

namespace Order.Query.Api.Queries
{
    public class FindOrdersByCriteriaQuery : BaseQuery
    {
        public uint Count { get; set; }
        public uint Price { get; set; }
        public uint Discount { get; set; }
        public DateTime CreationDate { get; set; }
        public string Buyer { get; set; }
    }
}
