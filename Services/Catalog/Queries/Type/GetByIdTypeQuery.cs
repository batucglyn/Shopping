using Catalog.DTOs;
using MediatR;

namespace Catalog.Queries.Type
{
    public record GetByIdTypeQuery(string id):IRequest<TypeResponse>
    {
    }
}
