using Catalog.DTOs;
using Catalog.Entities;

namespace Catalog.Extensions
{
    public static class TypeMapper
    {

        public static TypeResponse TypeToTypeResponse(this ProductType type)
        {

            return new TypeResponse
            {
                Id = type.Id,
                Name=type.Name,

            };

        }

        public static IList<TypeResponse> TypesToTypesResponseList(this IEnumerable<ProductType> types)
        {

          
           return types.Select(x => x.TypeToTypeResponse()).ToList();
        }


    
        public static TypeDto ToDto(this TypeResponse response)
        {
            return new TypeDto(response.Id, response.Name);
        }


        public static IEnumerable<TypeDto> ToDtoList(this IEnumerable<TypeResponse> brands) => brands.Select(b => b.ToDto());
    }
}
