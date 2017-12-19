using NGC.DAL.Base;
using NGC.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace NGC.BLL.Base
{
    public class BaseBLL<T> : Interfaces.IBaseBLL<T> where T : Entity
    {
        protected readonly BaseRepository<T> repository;

        public BaseBLL(MerakiContext context){

            this.repository = new BaseRepository<T>(context);
        }

        public virtual void Delete(T entity)
        {
            repository.Delete(entity);
        }

        public virtual IEnumerable<T> GetAll()
        {
            return repository.QueryAll;
        }

        public virtual void Insert(T entity)
        {
            repository.Insert(entity);
        }
        public virtual void Save()
        {
            repository.Save();
        }

        public virtual void Update(T entity)
        {
            repository.Update(entity);
        }
    }
}
