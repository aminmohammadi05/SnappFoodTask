using Order.Common.DTOs;
using Order.Query.Domain.Entities;

namespace Order.Query.Api.DTOs
{
    public class OrderLookupResponse : BaseResponse
    {
        public List<OrderEntity> Orders { get; set; }
    }
}
