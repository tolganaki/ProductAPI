using ProductAPI.Data.Contexts;

namespace ProductAPI.Data.Repositories
{
    public abstract class BaseRepository
    {
        protected readonly AplicationDbContext _context;

        public BaseRepository(AplicationDbContext context)
        {
            _context = context;
        }
    }
}