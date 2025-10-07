using Catalog.DTOs;
using Catalog.Extensions;
using Catalog.Queries.Type;
using Catalog.Repositories;
using MediatR;

namespace Catalog.Handlers.Type
{
    public class GetAllTypeHandler : IRequestHandler<GetAllTypesQuery, IList<TypeResponse>>
    {

        private readonly ITypeRepository _typeRepository;

        public GetAllTypeHandler(ITypeRepository typeRepository)
        {
            _typeRepository = typeRepository;
        }

        public async Task<IList<TypeResponse>> Handle(GetAllTypesQuery request, CancellationToken cancellationToken)
        {


            var types = await _typeRepository.GetAllAsync();



         return   types.TypesToTypesResponseList();



        }
    }


}
