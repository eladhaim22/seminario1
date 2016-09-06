using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using NHibernate;
using Seminario.Model;
namespace Seminario.NHibernate
{
    public class UnitOfWork : IUnitOfWork
    {
        private static readonly object SessionFactoryLock = new object();

        private static ISessionFactory sessionFactory;

        private IDictionary<Type, object> repositories;

        private ISession session;

        private IList<ISynchronization> synchronizations;

        public UnitOfWork()
        {

        }

        ~UnitOfWork()
        {
            this.Dispose(false);
        }

        internal ISession Session
        {
            get
            {
                if (this.session == null)
                {
                    this.session = this.SessionFactory.OpenSession();
                    this.session.BeginTransaction();
                }

                return this.session;
            }
        }

        private ISessionFactory SessionFactory
        {
            get
            {
                if (sessionFactory == null)
                {
                    lock (SessionFactoryLock)
                    {
                        if (sessionFactory == null)
                        {
                            var connectionStringSettings = ConfigurationManager.ConnectionStrings["MyContext"].ConnectionString;
                            sessionFactory = SessionFactoryBuilder.Build(connectionStringSettings);
                        }
                    }
                }

                return sessionFactory;
            }
        }

        public IRepository<T> Repository<T>() where T : class, IEntity
        {
            if (this.repositories == null)
            {
                this.repositories = new Dictionary<Type, object>();
            }

            object repository;
            if (!this.repositories.TryGetValue(typeof(T), out repository))
            {
                this.repositories[typeof(T)] = repository = new Repository<T>(this);
            }

            return repository as IRepository<T>;
        }

        public IRepository Repository(Type entityType)
        {
            if (this.repositories == null)
            {
                this.repositories = new Dictionary<Type, object>();
            }

            object repository;
            if (!this.repositories.TryGetValue(entityType, out repository))
            {
                var repositoryType = typeof(Repository<>).MakeGenericType(entityType);
                this.repositories[entityType] = repository = Activator.CreateInstance(repositoryType, this);
            }

            return repository as IRepository;
        }

        public void Save()
        {
            try
            {
                this.Session.Flush();

                this.InvokeBeforeCompletionSynchronizations();

                this.Session.Transaction.Commit();

                this.session.BeginTransaction();

                this.InvokeAfterCompletionSynchronizations(true);
            }
            catch (Exception ex)
            {
                this.Reset();
                throw;
            }
        }

        public void Reset()
        {
            if (this.session != null)
            {
                if (this.session.Transaction.IsActive && !this.session.Transaction.WasRolledBack)
                {
                    try
                    {
                        this.session.Transaction.Rollback();
                    }
                    catch (HibernateException ex)
                    {
                    
                    }

                    this.InvokeAfterCompletionSynchronizations(false);
                }

                this.session.Dispose();
                this.session = null;
            }

            this.synchronizations = null;
        }
        
        public void RegisterSynchronization(ISynchronization synchronization)
        {
            if (synchronization == null)
            {
                throw new ArgumentNullException("synchronization");
            }

            if (this.synchronizations == null)
            {
                this.synchronizations = new List<ISynchronization>();
            }

            this.synchronizations.Add(synchronization);
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.Reset();
            }
        }

        private void InvokeBeforeCompletionSynchronizations()
        {
            if (this.synchronizations != null)
            {
                foreach (var synchronization in this.synchronizations)
                {
                    try
                    {
                        synchronization.BeforeCompletion();
                    }
                    catch (Exception ex)
                    {
                        throw;
                    }
                }
            }
        }

        private void InvokeAfterCompletionSynchronizations(bool committed)
        {
            if (this.synchronizations != null)
            {
                foreach (var synchronization in this.synchronizations)
                {
                    try
                    {
                        synchronization.AfterCompletion(committed);
                    }
                    catch (Exception ex)
                    {
                        throw;
                    }
                }

                this.synchronizations = null;
            }
        }
    }
}
