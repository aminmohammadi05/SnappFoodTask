using MediatR;
using Order.Query.Domain.Entities;
using Order.Query.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Query.Infrastructure.Features.OrderProducts.Commands
{
    public class CreateOrderProductCommandHandler : IRequestHandler<CreateOrderProductCommand, CreateOrderProductCommandResponse>
    {
        private readonly IAsyncRepository<OrderProductEntity> _orderRepository;

        public CreateOrderProductCommandHandler(IAsyncRepository<OrderProductEntity> orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<CreateOrderProductCommandResponse> Handle(CreateOrderProductCommand request, CancellationToken cancellationToken)
        {
            var createOrderProductCommandResponse = new CreateOrderProductCommandResponse();

            var validator = new CreateOrderProductCommandValidator();
            var validationResult = await validator.ValidateAsync(request);

            if (validationResult.Errors.Count > 0)
            {
                createOrderProductCommandResponse.Success = false;
                createOrderProductCommandResponse.ValidationErrors = new List<string>();
                foreach (var error in validationResult.Errors)
                {
                    createOrderProductCommandResponse.ValidationErrors.Add(error.ErrorMessage);
                }
            }
            if (createOrderProductCommandResponse.Success)
            {
                var order = new OrderProductEntity()
                {
                    Count = request.Count,
                    CreationDate = request.CreationDate,
                    CurrentDiscount = request.CurrentDiscount,
                    CurrentPrice = request.CurrentPrice,
                    EditDate = request.EditDate,
                    OrderId = request.OrderId,
                    ProductId = request.ProductId,

                };
                order = await _orderRepository.AddAsync(order);
                createOrderProductCommandResponse.OrderProduct = order;
            }

            return createOrderProductCommandResponse;
        }
    }
}
