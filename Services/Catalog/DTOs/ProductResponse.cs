using Catalog.Entities;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Catalog.DTOs
{
    public record class ProductResponse
    {


        public string Id { get; init; }
        public string Name { get; init; }
        public string Description { get; init; }
        public string Summary { get; init; }
        public string ImageFile { get; init; }

        public decimal Price { get; init; }

        public DateTimeOffset CreatedDate { get; init; }


        public BrandResponse Brand { get; init; }      
        public TypeResponse Type { get; init; }
    }
}
