using Order.Common.DTOs;

namespace Order.Command.Api.DTOs
{
    public class NewOrderResponse : BaseResponse
    {
        public Guid Id { get; set; }
    }
}
