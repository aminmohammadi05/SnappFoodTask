using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Query.Infrastructure.Features.OrderProducts.Commands
{
    public class CreateOrderProductCommandValidator : AbstractValidator<CreateOrderProductCommand>
    {
        public CreateOrderProductCommandValidator()
        {
            RuleFor(p => p.CreationDate)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull().WithMessage("{PropertyName} must not be null.");
            
        }
    }
}
