using Catalog.Entities;
using Catalog.Specification;

namespace Catalog.Repositories
{
    public interface IProductRepository
    {

        Task<IEnumerable<Product>> GetAllProductAsync();
        Task<Pagination<Product>> GetProducts(CatalogSpecificationParams specParams);
        Task<IEnumerable<Product?>> GetProducstByName(string name);
        Task<IEnumerable<Product?>> GetProductsByBrand(string name);

        Task<Product?> GetProductById(string productId);
        Task<Product?> CreateProduct(Product product);
        Task<bool> UpdateProduct(Product product);
        Task<bool> DeleteProduct(string id);
        Task<Brand?> GetBrandByIdAsync(string brandId);
        Task<Entities.ProductType?> GetTypeByIdAsync(string typeId);






    }
}
