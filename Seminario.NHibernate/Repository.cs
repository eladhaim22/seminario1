using Seminario.Model;
using Seminario.NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using NHibernate.Linq;

public class Repository<TEntity> : IRepository<TEntity>
        where TEntity : class , IEntity
{
    readonly UnitOfWork nhUnitOfWork;

    public Repository(UnitOfWork nhUnitOfWork)
    {
        this.nhUnitOfWork = nhUnitOfWork;
    }

    public void Add(TEntity entity)
    {
        this.nhUnitOfWork.OpenSession();
        this.nhUnitOfWork.BeginTransation();
        this.nhUnitOfWork.Session.Save(entity);
    }

    public void Remove(TEntity entity)
    {
        this.nhUnitOfWork.OpenSession();
        this.nhUnitOfWork.BeginTransation();
        this.nhUnitOfWork.Session.Delete(entity);
    }

    public void Update(TEntity entity)
    {
        this.nhUnitOfWork.OpenSession();
        this.nhUnitOfWork.BeginTransation();
        this.nhUnitOfWork.Session.Update(entity);
    }

    public IQueryable<TEntity> GetAll()
    {
        this.nhUnitOfWork.OpenSession();
        this.nhUnitOfWork.BeginTransation();
        return this.nhUnitOfWork.Session.Query<TEntity>().AsQueryable();
    }

    public TEntity Get(Expression<System.Func<TEntity, bool>> expression)
    {
        this.nhUnitOfWork.OpenSession();
        this.nhUnitOfWork.BeginTransation();
        return GetMany(expression).SingleOrDefault();
    }

    public IQueryable<TEntity> GetMany(Expression<System.Func<TEntity, bool>> expression)
    {
        this.nhUnitOfWork.OpenSession();
        this.nhUnitOfWork.BeginTransation();
        return GetAll().Where(expression).AsQueryable();
    }

    public TEntity GetById(int id)
    {
        this.nhUnitOfWork.OpenSession();
        this.nhUnitOfWork.BeginTransation();
        return this.nhUnitOfWork.Session.Get<TEntity>(id);
    }
}