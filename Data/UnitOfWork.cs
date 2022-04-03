using ProductAPI.Data.Contexts;

namespace ProductAPI.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AplicationDbContext _context;
        private readonly ILogger<IUnitOfWork> _logger;

        public UnitOfWork(AplicationDbContext context, ILogger<IUnitOfWork> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task CommitAsync()
        {
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(0, ex, "Error while saving changes to DbContrxt");
                throw;
            }
        }
    }
}