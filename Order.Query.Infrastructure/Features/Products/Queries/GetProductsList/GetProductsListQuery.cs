using MediatR;
using Order.Query.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Query.Infrastructure.Features.Products.Queries.GetProductsList
{
    public class GetProductsListQuery : IRequest<List<ProductEntity>>
    {
    }
}
