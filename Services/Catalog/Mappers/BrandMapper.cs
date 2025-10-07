using Catalog.DTOs;
using Catalog.Entities;
using Catalog.Mappers;

namespace Catalog.Extensions;

public static class BrandMapper
{

    public static BrandResponse BrandToResponse(this Brand brand)
    {

        return new BrandResponse
        {
            Id = brand.Id,
            Name = brand.Name
        };



    }

    public static IList<BrandResponse> BrandsToBrandsResponseList(this IEnumerable<Brand> brands)
    {

        return brands.Select(x => x.BrandToResponse()).ToList();

    }


   
    public static BrandDto ToDto(this BrandResponse response)
    {
        return new BrandDto(response.Id, response.Name);
    }



    public static IEnumerable<BrandDto> ToDtoList(this IEnumerable<BrandResponse> brands) => brands.Select(b => b.ToDto());


   





}


