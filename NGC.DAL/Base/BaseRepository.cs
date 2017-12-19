using System;
using System.Collections.Generic;
using System.Linq;
using NGC.Model;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace NGC.DAL.Base
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        protected readonly MerakiContext context;

        public BaseRepository(MerakiContext context)
        {
            this.context = context;
            context.Database.Migrate();
        }

        public IQueryable<T> QueryAll
        {
            get
            {
                return context.Set<T>();
            }
        }

        public IQueryable<T> All => QueryAll;

        public void Insert(T entity)
        {
            context.Add(entity);
        }
        public void Update(T entity)
        {
            context.Update(entity);
        }
        public void Delete(T entity)
        {
            context.Remove(entity);
        }
        public void Save()
        {
            context.SaveChanges();
        }
        public EntityEntry<T> Entry(T entity)
        {
            return context.Entry(entity);
        }
        public void Attach(T entity)
        {
            context.Attach(entity);
        }

    }
}
