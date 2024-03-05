using System.Linq.Expressions;

namespace Bulky.DataAccess.Repository.IRepository
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll(string? includeProperies = null);
        T Get(Expression<Func<T,bool>> filter, string? includeProperies = null);
        void Add(T entity);
        void UpdateGeneric(T entity);
        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entities);
    }
}
