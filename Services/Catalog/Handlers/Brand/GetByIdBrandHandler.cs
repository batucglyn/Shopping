using Catalog.DTOs;
using Catalog.Entities;
using Catalog.Extensions;
using Catalog.Queries.Brand;
using Catalog.Repositories;
using MediatR;

namespace Catalog.Handlers
{
    public class GetByIdBrandHandler : IRequestHandler<GetByIdBrandQuery, BrandResponse>
    {
        private readonly IBrandRepository _brandRepository;

        public GetByIdBrandHandler(IBrandRepository brandRepository)
        {
            _brandRepository = brandRepository;
        }

        public async Task<BrandResponse> Handle(GetByIdBrandQuery request, CancellationToken cancellationToken)
        {
           Brand? brand= await _brandRepository.GetByIdAsync(request.id);

          

            return brand.BrandToResponse();
        }
    }
}
