using NGC.Common.Classes.Filters;
using NGC.DAL.Base;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace NGC.BLL.Base.V2
{
    public interface IBaseBLL <T, IFilter, IRepo> where T : class where IFilter : IMerakiFilter<T> where IRepo : IBaseRepository<T>
    {
        IEnumerable<T> GetByFilters(IFilter filter, Expression<Func<T, object>> order = null, params Expression<Func<T, object>>[] includes);
        IEnumerable<T> GetAll();
        void Insert(T entity);
        void Update(T entity);
        void Delete(T entity);

        void Save();
    }
}
