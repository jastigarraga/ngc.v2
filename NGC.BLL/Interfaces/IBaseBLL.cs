using NGC.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace NGC.BLL.Interfaces
{
    public interface IBaseBLL<T> where T : Entity
    {
        IEnumerable<T> GetAll();
        void Insert(T entity);
        void Update(T entity);
        void Delete(T entity);

        void Save();
    }
}
