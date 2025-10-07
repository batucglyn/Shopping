using Catalog.Entities;
using MongoDB.Driver;

namespace Catalog.Repositories
{
    public class BrandRepository : IBrandRepository
    {
        private readonly IMongoCollection<Brand> _brands;
        public BrandRepository(IConfiguration config)
        {

            var client = new MongoClient(config["DatabaseSettings:ConnectionString"]);
            var db = client.GetDatabase(config["DatabaseSettings:DatabaseName"]);
            _brands = db.GetCollection<Brand>(config["DatabaseSettings:BrandCollectionName"]);

        }

        public async Task<IEnumerable<Brand>> GetAllBrandsAsync()
        {
            
           return await _brands.Find(_ => true).ToListAsync();
        }

        public async Task<Brand> GetByIdAsync(string id)
        {
            return await _brands.Find(x => x.Id == id).FirstOrDefaultAsync();
        }
    }
}
