using Bulky.DataAccess.Data;
using Bulky.DataAccess.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Bulky.DataAccess.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDbContext _db;
        internal DbSet<T> _dbSet;
        public Repository(ApplicationDbContext db)
        {
            _db = db;
            _dbSet = _db.Set<T>();
            _db.Products.Include(u => u.Category).Include(u => u.CategoryId);
        }
        public void Add(T entity)
        {
            _dbSet.Add(entity);
        }

        public T Get(Expression<Func<T, bool>> filter, string? includeProperies = null, bool tracked = false)
        {
            IQueryable<T> query;

            if (tracked)
            {
               query = _dbSet;
               
            }
            else
            {
                query = _dbSet.AsNoTracking();
               
            }

            query = query.Where(filter);
            if (!string.IsNullOrEmpty(includeProperies))
            {
                foreach (var includProp in includeProperies.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includProp);
                }
            }
            return query.FirstOrDefault();
        }

        public IEnumerable<T> GetAll(Expression<Func<T, bool>>? filter = null, string? includeProperies = null)
        {
            IQueryable<T> query = _dbSet;
            if (filter != null) query = query.Where(filter);

            if (!string.IsNullOrEmpty(includeProperies))
            {
                foreach (var includProp in includeProperies.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includProp);
                }
            }
            return query.ToList();
        }

        public void Remove(T entity)
        {
            _dbSet.Remove(entity);
            _db.SaveChanges();
        }

        public void RemoveRange(IEnumerable<T> entities)
        {
            _dbSet.RemoveRange(entities);
            _db.SaveChanges();
        }

        public void UpdateGeneric(T entity)
        {
            _dbSet.Update(entity);
            _db.SaveChanges();
        }
    }
}
