using Seminario.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Seminario.WebServices
{
    public interface IEntityService<T> 
     where T : Entity
    {
        void Create(T entity);
        void Delete(T entity);
        IQueryable<T> GetAll();
        void Update(T entity);
        T Get(Expression<System.Func<T, bool>> expression);
        IEnumerable<T> GetMany(Expression<System.Func<T, bool>> expression);
        T GetById(int id);
    }
}