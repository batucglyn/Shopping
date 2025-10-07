using Catalog.DTOs;
using MediatR;

namespace Catalog.Queries.Brand
{
    public record class GetByIdBrandQuery(string id) : IRequest<BrandResponse>
    {

    }
}
