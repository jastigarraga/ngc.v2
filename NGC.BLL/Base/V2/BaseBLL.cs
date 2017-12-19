using Microsoft.EntityFrameworkCore;
using NGC.Common.Classes.Filters;
using NGC.DAL.Base;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace NGC.BLL.Base.V2
{
    public abstract class BaseBLL<T, IFilter, IRepo> : IBaseBLL<T,IFilter,IRepo> where T : class where IFilter : IMerakiFilter<T> where IRepo : IBaseRepository<T>
    {
        protected readonly IRepo repository;
        public BaseBLL(IRepo repository)
        {
            this.repository = repository;
        }

        public virtual void Delete(T entity)=>repository.Delete(entity);

        public virtual IEnumerable<T> GetAll() => repository.All;

        public virtual IEnumerable<T> GetByFilters(IFilter filter,Expression<Func<T,object>> order = null, params Expression<Func<T,object>>[] includes)
        {
            var result = repository.All.Where(filter.FilterExpression);
            if(includes != null)
            {
                foreach(var include in includes)
                {
                    result = result.Include(include);
                }
            }
            if(order != null)
            {
                result = result.OrderBy(order);
            }
            return result;
        }

        public virtual void Insert(T entity) => repository.Insert(entity);

        public virtual void Save() => repository.Save();

        public virtual void Update(T entity) => repository.Update(entity);
    }
}
