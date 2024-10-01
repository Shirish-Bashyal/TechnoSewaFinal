namespace Application.Interfaces.Data
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<T> AsyncRepositories<T>()
            where T : class;
        Task<int> Save();
    }
}
