using Catalog.DTOs;
using MediatR;

namespace Catalog.Queries.Product
{
    public record class GetProductByBrandNameQuery(string brandName):IRequest<IList<ProductResponse>>
    {
    }
}
