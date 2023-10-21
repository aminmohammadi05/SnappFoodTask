using MediatR;
using Order.Query.Domain.Entities;
using Order.Query.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Query.Infrastructure.Features.Orders.Commands.CreateOrder
{
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, CreateOrderCommandResponse>
    {
        private readonly IAsyncRepository<OrderEntity> _orderRepository;

        public CreateOrderCommandHandler(IAsyncRepository<OrderEntity> orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<CreateOrderCommandResponse> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var createOrderCommandResponse = new CreateOrderCommandResponse();

            var validator = new CreateOrderCommandValidator();
            var validationResult = await validator.ValidateAsync(request);

            if (validationResult.Errors.Count > 0)
            {
                createOrderCommandResponse.Success = false;
                createOrderCommandResponse.ValidationErrors = new List<string>();
                foreach (var error in validationResult.Errors)
                {
                    createOrderCommandResponse.ValidationErrors.Add(error.ErrorMessage);
                }
            }
            if (createOrderCommandResponse.Success)
            {
                var order = new OrderEntity() 
                { 
                BuyerId = request.BuyerId,
                CreationDate = request.CreationDate,
                OrderId = request.OrderId
                
                };
                order = await _orderRepository.AddAsync(order);
                createOrderCommandResponse.Order = order;
            }

            return createOrderCommandResponse;
        }
    }
}
