using Catalog.Entities;
using MongoDB.Driver;
using ZstdSharp.Unsafe;

namespace Catalog.Repositories
{
    public class TypeRepository : ITypeRepository
    {
        private readonly IMongoCollection<Entities.ProductType> _types;
        public TypeRepository(IConfiguration config)
        {
            var client = new MongoClient(config["DatabaseSettings:ConnectionString"]);
            var db = client.GetDatabase(config["DatabaseSettings:DatabaseName"]);
            _types = db.GetCollection<Entities.ProductType>(config["DatabaseSettings:TypeCollectionName"]);
        }



        public async Task<IEnumerable<Entities.ProductType>> GetAllAsync()
        {
            return await _types.Find(_=>true).ToListAsync();
        }

        public async Task<Entities.ProductType> GetByIdAsync(string id)
        {
            return await _types.Find(t=>t.Id==id).FirstOrDefaultAsync();
        }
    }
}
