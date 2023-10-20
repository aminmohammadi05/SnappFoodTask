using CQRS.Core.Queries;

namespace Order.Query.Api.Queries
{
    public class FindOrderByIdQuery : BaseQuery
    {
        public Guid Id { get; set; }
    }
}
