namespace Seminario.NHibernate
{
	using System;
	using System.Collections.Generic;
	using System.Diagnostics.CodeAnalysis;
	using System.Linq;
	using System.Linq.Expressions;
	using global::NHibernate;
	using global::NHibernate.Linq;
	using Seminario.Model;

	public class Repository<T> : IRepository<T>, IRepository where T : class, IEntity
	{
		private readonly UnitOfWork unitOfWork;

		public Repository(UnitOfWork unitOfWork)
		{
			this.unitOfWork = unitOfWork;
		}

		public Type ElementType
		{
			get { return this.QueryableSet.ElementType; }
		}

		public Expression Expression
		{
			get { return this.QueryableSet.Expression; }
		}

		public IQueryProvider Provider
		{
			get { return this.QueryableSet.Provider; }
		}

		protected ISession Session
		{
			get { return this.unitOfWork.Session; }
		}

		protected IQueryable<T> QueryableSet
		{
			get { return this.Session.Query<T>(); }
		}

		public T GetById(int identifier)
		{
			return (T)this.Session.Get(typeof(T), identifier);
		}

		public void Add(T entity)
		{
			try
			{
				this.Session.Save(entity);
			}
			catch (Exception)
			{
				this.unitOfWork.Reset();
				throw;
			}
		}

		public void Remove(T entity)
		{
			this.Session.Delete(entity);
		}

		public void Update(T entity)
		{
			try
			{
				this.Session.Merge(entity);
			}
			catch (Exception)
			{
				this.unitOfWork.Reset();
				throw;
			}
		}

		public IEnumerator<T> GetEnumerator()
		{
			return ((IEnumerable<T>)this.QueryableSet).GetEnumerator();
		}

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
			return ((System.Collections.IEnumerable)this.QueryableSet).GetEnumerator();
		}

		IEntity IRepository.GetById(int identifier)
		{
			return (IEntity)this.Session.Get(typeof(T), identifier);
		}

		public void Add(IEntity entity)
		{
			this.Add((T)(object)entity);
		}

		public void Update(IEntity entity)
		{
			this.Update((T)(object)entity);
		}

		public void Remove(IEntity entity)
		{
			this.Remove((T)(object)entity);
		}
	}
}
