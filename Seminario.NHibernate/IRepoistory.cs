using Seminario.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Seminario.NHibernate
{
    public interface IRepository<TEntity>
    {
        IQueryable<TEntity> GetAll();
        void Add(TEntity entity);
        void Remove(TEntity entity);
        void Update(TEntity entity);
        TEntity Get(Expression<System.Func<TEntity, bool>> expression);
        IQueryable<TEntity> GetMany(Expression<System.Func<TEntity, bool>> expression);
        TEntity GetById(int id);
    }
}
