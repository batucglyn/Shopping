using Catalog.Entities;

namespace Catalog.Repositories
{
    public interface ITypeRepository
    {

        Task<IEnumerable<Entities.ProductType>> GetAllAsync();
        Task<Entities.ProductType> GetByIdAsync(string id);




    }
}
