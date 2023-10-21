using MediatR;
using Order.Query.Domain.Entities;
using Order.Query.Domain.Repositories;
using Order.Query.Infrastructure.Features.Products.Commands.CreateProduct;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.Query.Infrastructure.Features.Products.Commands.CreateProduct
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, CreateProductCommandResponse>
    {
        private readonly IAsyncRepository<ProductEntity> _productRepository;

        public CreateProductCommandHandler(IAsyncRepository<ProductEntity> productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<CreateProductCommandResponse> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var createProductCommandResponse = new CreateProductCommandResponse();

            var validator = new CreateProductCommandValidator();
            var validationResult = await validator.ValidateAsync(request);

            if (validationResult.Errors.Count > 0)
            {
                createProductCommandResponse.Success = false;
                createProductCommandResponse.ValidationErrors = new List<string>();
                foreach (var error in validationResult.Errors)
                {
                    createProductCommandResponse.ValidationErrors.Add(error.ErrorMessage);
                }
            }
            if (createProductCommandResponse.Success)
            {
                var product = new ProductEntity()
                {
                    Discount = request.Discount,
                    InventoryCount = request.InventoryCount,
                    Price = request.Price,
                    ProductId = request.ProductId,
                    Title = request.Title,

                };
                product = await _productRepository.AddAsync(product);
                createProductCommandResponse.Product = product;
            }

            return createProductCommandResponse;
        }
    }
}
