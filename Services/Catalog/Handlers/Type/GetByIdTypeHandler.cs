using Catalog.DTOs;
using Catalog.Extensions;
using Catalog.Queries.Type;
using Catalog.Repositories;
using MediatR;

namespace Catalog.Handlers.Type
{
    public class GetByIdTypeHandler : IRequestHandler<GetByIdTypeQuery, TypeResponse>
    {
        private readonly ITypeRepository _typeRepository;

        public GetByIdTypeHandler(ITypeRepository typeRepository)
        {
            _typeRepository = typeRepository;
        }

        public async Task<TypeResponse> Handle(GetByIdTypeQuery request, CancellationToken cancellationToken)
        {

          var type=  await _typeRepository.GetByIdAsync(request.id);

            return type.TypeToTypeResponse();


        }
    }
}
