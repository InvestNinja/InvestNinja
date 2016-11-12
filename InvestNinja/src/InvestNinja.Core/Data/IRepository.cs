using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InvestNinja.Core.Data
{
    public interface IRepository<T> where T : IEntity
    {
        IEnumerable<T> GetAll();

        T GetById(string id);

        void Insert(T entity);

        void Update(T entity);

        void Delete(T entity);

        void Insert(IEnumerable<T> entities);

        void Update(IEnumerable<T> entities);

        void Delete(IEnumerable<T> entities);
    }
}
