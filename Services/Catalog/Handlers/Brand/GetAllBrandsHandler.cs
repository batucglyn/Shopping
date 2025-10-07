using Catalog.DTOs;
using Catalog.Extensions;
using Catalog.Queries.Brand;
using Catalog.Repositories;
using MediatR;
namespace Catalog.Handlers
{
    public class GetAllBrandsHandler : IRequestHandler<GetAllBrandsQuery, IList<BrandResponse>>
    {

       private readonly IBrandRepository _brandRepository;

        public GetAllBrandsHandler(IBrandRepository brandRepository)
        {
            _brandRepository = brandRepository;
        }

        public async Task<IList<BrandResponse>> Handle(GetAllBrandsQuery request, CancellationToken cancellationToken)
        {
            var allBrands = await _brandRepository.GetAllBrandsAsync();

            return allBrands.BrandsToBrandsResponseList();



        }
    }
}
