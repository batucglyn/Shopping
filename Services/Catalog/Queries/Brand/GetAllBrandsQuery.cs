using Catalog.DTOs;
using MediatR;

namespace Catalog.Queries.Brand
{
    public record class GetAllBrandsQuery:IRequest<IList<BrandResponse>>
    {
    }
}
