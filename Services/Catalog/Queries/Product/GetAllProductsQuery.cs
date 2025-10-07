using Catalog.DTOs;
using Catalog.Specification;
using MediatR;

namespace Catalog.Queries.Product
{
    public record class GetAllProductsQuery(CatalogSpecificationParams catalogSpecParams):IRequest<Pagination<ProductResponse>>
    {

    

    }
}
