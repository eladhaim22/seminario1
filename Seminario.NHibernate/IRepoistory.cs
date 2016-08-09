namespace Seminario.NHibernate
{
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using Seminario.Model;

    public interface IRepository
    {
        IEntity GetById(int identifier);
        void Add(IEntity entity);
        void Update(IEntity entity);
        void Remove(IEntity entity);
    }

    public interface IRepository<T> : IQueryable<T> where T : class, IEntity
    {
        T GetById(int identifier);
        void Add(T entity);
        void Update(T entity);
        void Remove(T entity);
    }
}
