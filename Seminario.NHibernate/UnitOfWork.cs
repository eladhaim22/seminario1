using NHibernate;
using Seminario.NHibernate;
using System;
using System.Data;
public interface IUnitOfWork : IDisposable
{
    void Commit();
    void Rollback();
}

public class UnitOfWork : IUnitOfWork
{
    private ISession session;
    private ITransaction transaction;
    public ISession Session { get { return this.session; } }

    public UnitOfWork() { }

    public void OpenSession()
    {
        if (this.session == null || !this.session.IsConnected)
        {
            if (this.session != null)
                this.session.Dispose();

            this.session = SessionFactoryBuilder.OpenSession();
        }
    }

    public void BeginTransation(IsolationLevel isolationLevel = IsolationLevel.ReadCommitted)
    {
        if (this.transaction == null || !this.transaction.IsActive)
        {
            if (this.transaction != null)
                this.transaction.Dispose();

            this.transaction = this.session.BeginTransaction(isolationLevel);
        }
    }

    public void Commit()
    {
        try
        {
            this.transaction.Commit();
        }
        catch
        {
            this.transaction.Rollback();
            throw;
        }
    }

    public void Rollback()
    {
        this.transaction.Rollback();
    }

    public void Dispose()
    {
        if (this.transaction != null)
        {
            this.transaction.Dispose();
            this.transaction = null;
        }

        if (this.session != null)
        {
            this.session.Dispose();
            session = null;
        }
    }
}