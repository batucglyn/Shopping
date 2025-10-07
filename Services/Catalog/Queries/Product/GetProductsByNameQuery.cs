using Catalog.DTOs;
using MediatR;

namespace Catalog.Queries.Product
{
    public record class GetProductsByNameQuery(string name):IRequest<IList<ProductResponse>>
    {

    }
}
