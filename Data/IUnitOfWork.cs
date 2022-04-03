namespace ProductAPI.Data
{
    public interface IUnitOfWork
    {
        Task CommitAsync();
    }
}