using MongoDB.Bson.Serialization.Attributes;

namespace Catalog.Entities
{
    public class Brand:BaseEntity
    {

        [BsonElement("Name")]
        public string Name { get; set; }


    }
}
