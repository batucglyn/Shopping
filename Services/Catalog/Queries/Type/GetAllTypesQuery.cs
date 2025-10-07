using Catalog.DTOs;
using MediatR;

namespace Catalog.Queries.Type
{
    public record GetAllTypesQuery :IRequest<IList<TypeResponse>>
    {



    }
}
