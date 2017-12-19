using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Linq;

namespace NGC.DAL.Base
{
    public interface IBaseRepository<T> where T : class
    {
        IQueryable<T> All { get; }
        void Insert(T entity);
        void Update(T entity);
        void Delete(T entity);
        void Save();
        EntityEntry<T> Entry(T entity);
        void Attach(T entity);
    }
}
