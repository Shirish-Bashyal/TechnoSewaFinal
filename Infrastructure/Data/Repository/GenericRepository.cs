using System.Linq.Expressions;
using Application.Interfaces.Data;
using Infrastructure.Data.DbContext;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Repository
{
    public class GenericRepository<T> : IGenericRepository<T>
        where T : class
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<T> _db;

        public GenericRepository(ApplicationDbContext context)
        {
            _context = context;
            _db = _context.Set<T>();
        }

        public async Task<T> AddAsync(T entity)
        {
            return (await _db.AddAsync(entity)).Entity;
        }

        public async Task DeleteAsync(T entity)
        {
            _db.Remove(entity);
        }

        public async Task<bool> DoesExists(Expression<Func<T, bool>> filter)
        {
            return await _db.AnyAsync(filter);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _db.ToListAsync();
        }

        public async Task<T> GetByPrimaryKey<TPrimaryKey>(TPrimaryKey id)
        {
            var result = await _db.FindAsync(id);

            if (result is null)
            {
                return null;
            }
            return result;
        }

        public async Task<T> GetWithIncludeAndFilter(
            Expression<Func<T, object>>[] children,
            Expression<Func<T, bool>> filter
        )
        {
            IQueryable<T> query = _db.Where(filter);
            foreach (var child in children)
            {
                query = query.Include(child);
            }

            return await query.FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<T>> GetWithInclude(Expression<Func<T, object>>[] children)
        {
            try
            {
                IQueryable<T> query = _context.Set<T>();

                foreach (var childrens in children)
                {
                    query = query.Include(childrens);
                }
                return query;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task UpdateAsync(T entity)
        {
            _db.Attach(entity);
            _db.Entry(entity).State = EntityState.Modified;
        }

        public async Task<T> GetSingleBySpec(Expression<Func<T, bool>> filter)
        {
            return await _db.FirstOrDefaultAsync(filter);
        }

        public async Task<IEnumerable<T>> GetListBySpec(Expression<Func<T, bool>> filter)
        {
            IQueryable<T> query = _db.Where(filter);
            return await query.ToListAsync();
        }

        public async Task<IEnumerable<T>> GetListWithIncludeAndFilter(
            Expression<Func<T, object>>[] children,
            Expression<Func<T, bool>> filter
        )
        {
            IQueryable<T> query = _db.Where(filter);
            foreach (var child in children)
            {
                query = query.Include(child);
            }

            return await query.ToListAsync();
        }
    }
}
