using Catalog.DTOs;
using MediatR;

namespace Catalog.Queries.Product
{
    public record class GetProductByIdQuery(string id):IRequest<ProductResponse>
    {

    }
}
