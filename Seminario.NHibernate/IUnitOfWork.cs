namespace Seminario.NHibernate
{
    using System;
    using Seminario.Model;

    /// <summary>
    /// Unit of work interface
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {
        IRepository<T> Repository<T>() where T : class, IEntity;
        IRepository Repository(Type entityType);
        void Save();
        void Reset();
        void RegisterSynchronization(ISynchronization synchronization);
    }
}
