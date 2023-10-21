using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Query.Infrastructure.Features.Products.Commands.CreateProduct
{
    public class CreateProductCommand : IRequest<CreateProductCommandResponse>
    {
        public Guid ProductId { get; set; }
        public string Title { get; set; }
        public uint InventoryCount { get; set; }
        public uint Price { get; set; }
        public uint Discount { get; set; }
    }
}
