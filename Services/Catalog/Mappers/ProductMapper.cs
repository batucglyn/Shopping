using Catalog.Commands.Product;
using Catalog.DTOs;
using Catalog.Entities;
using Catalog.Extensions;
using Catalog.Specification;

namespace Catalog.Mappers
{
    public static class ProductMapper
    {


        public static ProductResponse ProductToProductResponse(this Product product)
        {

            return new ProductResponse
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                ImageFile = product.ImageFile,
                CreatedDate = product.CreatedDate,
                Price = product.Price,
                Summary = product.Summary,
                Brand = product.Brand.BrandToResponse(),
                Type = product.Type.TypeToTypeResponse()


            };

        }

        public static IEnumerable<ProductResponse> ProductsToProductsResponseList(this IEnumerable<Product> products)
            => products.Select(p => p.ProductToProductResponse());

        public static IEnumerable<ProductDto> ResponseToDtoList(this IEnumerable<ProductResponse> products)
           => products.Select(p => p.ToDto());

        public static Pagination<ProductResponse> ToResponse(this Pagination<Product> pagination) =>
    new Pagination<ProductResponse>(
        pagination.PageIndex,
        pagination.PageSize,
        pagination.Count,
        pagination.Data.Select(p => p.ProductToProductResponse()).ToList()
    );




        public static Product ToEntity(this CreateProductCommand command, Brand brand, ProductType type)
        {

            return new Product
            {
                Description = command.Description,
                Name = command.Name,
                ImageFile = command.ImageFile,
                Summary = command.Summary,
                Price = command.Price,
                CreatedDate = DateTimeOffset.UtcNow,
                Brand = brand,
                Type = type


            };

        }


        public static Product ToUpdateEntity(this UpdateProductCommand command, Product existing, Brand brand, ProductType type)
        {
            return new Product
            {
                Id = existing.Id,
                Name = command.Name,
                ImageFile = command.ImageFile,
                Summary = command.Summary,
                Price = command.Price,
                CreatedDate = existing.CreatedDate,
                Brand = brand,
                Type = type,
                Description = command.Description



            };


        }



        public static ProductDto ToDto(this ProductResponse product)
        {

            if (product == null) return null;

            return new ProductDto(
        product.Id,
        product.Name,
        product.Summary,
        product.Description,
        product.ImageFile,
        product.Price,
        new BrandDto(product.Brand.Id, product.Brand.Name),
        new TypeDto(product.Type.Id, product.Type.Name),
        DateTimeOffset.UtcNow
            );



        }

        public static UpdateProductCommand ToCommand(this UpdateProductDto dto, string id)
        {
            return new UpdateProductCommand
            {
                Id = id,
                Name = dto.Name,
                Summary = dto.Summary,
                Description = dto.Description,
                ImageFile = dto.ImageFile,
                Price = dto.Price,
                BrandId = dto.BrandId,
                TypeId = dto.TypeId
            };
        }



    }
}
