using Catalog.Entities;

namespace Catalog.Repositories
{
    public interface IBrandRepository
    {

        Task<IEnumerable<Brand>> GetAllBrandsAsync();
        Task<Brand>GetByIdAsync(string id);


    }
}
