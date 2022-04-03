using ProductAPI.Data.Entities;

namespace ProductAPI.Data.Repositories
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> ListAsync();

        Task<Product> FindByIdAsync(int id);

        Task<Product> FindByNameAsync(string name);

        Task AddAsync(Product product);

        void Update(Product product);

        void Delete(Product product);
    }
}