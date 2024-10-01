using Application.Interfaces.Data;
using Infrastructure.Data.DbContext;

namespace Infrastructure.Data.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }

        public Dictionary<Type, object> Repositories = new Dictionary<Type, object>();

        public void Dispose() { }

        public async Task<int> Save()
        {
            try
            {
                return (await _context.SaveChangesAsync());
            }
            catch (Exception ex)
            {
                return 0;
            }
            finally
            {
                GC.SuppressFinalize(this);
            }
        }

        public IGenericRepository<T> AsyncRepositories<T>()
            where T : class
        {
            if (Repositories.Keys.Contains(typeof(T)) == true)
            {
                return Repositories[typeof(T)] as IGenericRepository<T>;
            }
            IGenericRepository<T> repo = new ApplicationRepository<T>(_context);
            Repositories.Add(typeof(T), repo);
            return repo;
        }
    }
}
