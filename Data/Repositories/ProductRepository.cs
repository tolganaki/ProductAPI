using Microsoft.EntityFrameworkCore;
using ProductAPI.Data.Contexts;
using ProductAPI.Data.Entities;

namespace ProductAPI.Data.Repositories
{
    public class ProductRepository : BaseRepository, IProductRepository
    {
        public ProductRepository(AplicationDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Product>> ListAsync()
        {
            return await _context.Products.ToListAsync();
        }

        public async Task<Product> FindByIdAsync(int id)
        {
            return await _context.Products.FirstOrDefaultAsync(q => q.Id == id);
        }

        public async Task<Product> FindByNameAsync(string name)
        {
            return await _context.Products.FirstOrDefaultAsync(q => q.Name == name);
        }

        public async Task AddAsync(Product product)
        {
            await _context.Products.AddAsync(product);
        }

        public void Update(Product product)
        {
            _context.Products.Update(product);
        }

        public void Delete(Product product)
        {
            _context.Products.Remove(product);
        }
    }
}