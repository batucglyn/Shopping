using Catalog.Entities;
using Catalog.Specification;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Catalog.Repositories
{
    public class ProductRepository : IProductRepository
    {

        private readonly IMongoCollection<Product> _products;
        private readonly IMongoCollection<Brand> _brands;
        private readonly IMongoCollection<Entities.ProductType> _types;



        public ProductRepository(IConfiguration config)
        {
            var client = new MongoClient(config["DatabaseSettings:ConnectionString"]);
            var db = client.GetDatabase(config["DatabaseSettings:DatabaseName"]);

            _products = db.GetCollection<Product>(config["DatabaseSettings:ProductCollectionName"]);
            _brands = db.GetCollection<Brand>(config["DatabaseSettings:BrandCollectionName"]);
            _types = db.GetCollection<Entities.ProductType>(config["DatabaseSettings:TypeCollectionName"]);

        }

        public async Task<Product?> CreateProduct(Product product)
        {
            await _products.InsertOneAsync(product);
            return product;
        }

        public async Task<bool> DeleteProduct(string id)
        {
            FilterDefinition<Product> filter = Builders<Product>.Filter.Eq(x => x.Id, id);

            Product? existingProduct = (await _products.FindAsync(filter)).FirstOrDefault();
            if (existingProduct == null)
            {
                return false;

            }

            DeleteResult? deleteResult = await _products.DeleteOneAsync(filter);
            return deleteResult.DeletedCount > 0;

        }

        public async Task<IEnumerable<Product>> GetAllProductAsync()
        {
            return await _products.Find(_ => true).ToListAsync();
        }

        public async Task<Brand?> GetBrandByIdAsync(string brandId)
        {
            FilterDefinition<Brand> filter = Builders<Brand>.Filter.Eq(x => x.Id, brandId);


            return (await _brands.FindAsync(filter)).FirstOrDefault();
        }

        public async Task<Product?> GetProductById(string productId)
        {
            FilterDefinition<Product> filter = Builders<Product>.Filter.Eq(x => x.Id, productId);

            return (await _products.FindAsync(filter)).FirstOrDefault();


        }

        public async Task<IEnumerable<Product?>> GetProducstByName(string name)
        {
            var filter = Builders<Product>.Filter.Regex(
             p => p.Name,
              new BsonRegularExpression($".*{name}.*", "i")
             );

            return await _products.Find(filter).ToListAsync();

        }

        public async Task<Pagination<Product>> GetProducts(CatalogSpecificationParams catalogSpecParams)
        {
            var builder = Builders<Product>.Filter;
            var filter = builder.Empty;

            if (!string.IsNullOrEmpty(catalogSpecParams.Search))
            {
                filter &= builder.Where(p => p.Name.ToLower().Contains(catalogSpecParams.Search.ToLower()));
            }

            if (!string.IsNullOrEmpty(catalogSpecParams.BrandId))
            {
                filter &= builder.Eq(p => p.Brand.Id, catalogSpecParams.BrandId);
            }

            if (!string.IsNullOrEmpty(catalogSpecParams.TypeId))
            {
                filter &= builder.Eq(p => p.Type.Id, catalogSpecParams.TypeId);
            }

            var totalItems = await _products.CountDocumentsAsync(filter);
            var data = await ApplyDataFilters(catalogSpecParams, filter);

            return new Pagination<Product>(
                catalogSpecParams.PageIndex,
                catalogSpecParams.PageSize,
                (int)totalItems,
                data
            );
        }

        public async Task<IEnumerable<Product?>> GetProductsByBrand(string name)
        {
            return await _products
              .Find(p => p.Brand.Name.ToLower() == name.ToLower())
              .ToListAsync();


        }

        public async Task<Entities.ProductType?> GetTypeByIdAsync(string typeId)
        {
            FilterDefinition<Entities.ProductType> filter = Builders<Entities.ProductType>.Filter.Eq(x => x.Id, typeId);


            return (await _types.FindAsync(filter)).FirstOrDefault();
        }

        public async Task<bool> UpdateProduct(Product product)
        {
            if (product is null || string.IsNullOrWhiteSpace(product.Id))
                return false;

            var filter = Builders<Product>.Filter.Eq(x => x.Id, product.Id);

            var replaceOneResult = await _products.ReplaceOneAsync(filter, product);

            return replaceOneResult.MatchedCount > 0;



        }


        private async Task<IReadOnlyCollection<Product>> ApplyDataFilters(
            CatalogSpecificationParams catalogSpecParams,
            FilterDefinition<Product> filter)
        {
            var sortDefn = Builders<Product>.Sort.Ascending("Name");

            if (!string.IsNullOrEmpty(catalogSpecParams.Sort))
            {
                sortDefn = catalogSpecParams.Sort switch
                {
                    "priceAsc" => Builders<Product>.Sort.Ascending(p => p.Price),
                    "priceDesc" => Builders<Product>.Sort.Descending(p => p.Price),
                    _ => Builders<Product>.Sort.Ascending(p => p.Name),
                };
            }

            return await _products
                .Find(filter)
                .Sort(sortDefn)
                .Skip(catalogSpecParams.PageSize * (catalogSpecParams.PageIndex - 1))
                .Limit(catalogSpecParams.PageSize)
                .ToListAsync();
        }


    }
}
